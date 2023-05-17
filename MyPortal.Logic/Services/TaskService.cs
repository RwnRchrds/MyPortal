using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Requests.Person.Tasks;
using MyPortalWeb.Models;
using Task = System.Threading.Tasks.Task;
using TaskStatus = MyPortal.Database.Models.Search.TaskStatus;

namespace MyPortal.Logic.Services
{
    public class TaskService : BaseUserService, ITaskService
    {
        private readonly IUserService _userService;
        private readonly IPersonService _personService;
        private readonly IStaffMemberService _staffMemberService;

        public TaskService(ISessionUser user, IUserService userService, IPersonService personService, IStaffMemberService staffMemberService) : base(user)
        {
            _userService = userService;
            _personService = personService;
            _staffMemberService = staffMemberService;
        }

        public async Task CreateTask(TaskRequestModel task)
        {
            Validate(task);
            
            if (TaskTypes.IsReserved(task.TypeId))
            {
                throw new LogicException("Tasks of this type cannot be created manually.");
            }

            var access = await GetPermissionsForTasksPerson(task.AssignedToId);

            if (access.CanEdit || access.IsAssignee)
            {
                await using var unitOfWork = await User.GetConnection();
            
                var taskToAdd = new Database.Models.Entity.Task
                {
                    Id = Guid.NewGuid(),
                    Title = task.Title,
                    Description = task.Description,
                    AssignedToId = task.AssignedToId,
                    CreatedById = task.AssignedById,
                    CreatedDate = DateTime.Now,
                    DueDate = task.DueDate,
                    TypeId = task.TypeId,
                    Completed = false
                };

                unitOfWork.Tasks.Create(taskToAdd);

                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new PermissionException("You do not have permission to create tasks for this person.");
            }
        }

        public async Task<IEnumerable<TaskTypeModel>> GetTypes(bool personalOnly, bool activeOnly = true)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var taskTypes = await unitOfWork.TaskTypes.GetAll(personalOnly, activeOnly, false);

            return taskTypes.Select(t => new TaskTypeModel(t));
        }

        public async Task<bool> IsTaskOwner(Guid taskId, Guid userId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var task = await unitOfWork.Tasks.GetById(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            if (task.CreatedById == userId)
            {
                return true;
            }

            var person = await unitOfWork.People.GetByUserId(userId);

            if (person != null && task.AssignedToId == person.Id)
            {
                return task.AllowEdit;
            }

            return false;
        }
        
        private async Task<TaskModel> CheckAccessAndReturn(MyPortal.Database.Models.Entity.Task task)
        {
            if (task.AssignedToId.HasValue)
            {
                var access = await GetPermissionsForTasksPerson(task.AssignedToId.Value);

                if (access.CanView || access.IsAssignee)
                {
                    return new TaskModel(task);
                }
                
                throw new PermissionException("You do not have permission to view this task.");
            }

            return new TaskModel(task);
        }

        public async Task<TaskModel> GetTaskById(Guid taskId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var task = await unitOfWork.Tasks.GetById(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task not found.");
            }

            return await CheckAccessAndReturn(task);
        }

        public async Task<TaskReminderModel> GetExistingReminder(Guid taskId, Guid userId)
        {
            await using var unitOfWork = await User.GetConnection();

            var reminders = await unitOfWork.TaskReminders.GetRemindersByUser(userId);

            var reminder = reminders.FirstOrDefault(r => r.TaskId == taskId);

            if (reminder != null)
            {
                return new TaskReminderModel(reminder);
            }

            return null;
        }

        public async Task CreateTaskReminder(TaskReminderRequestModel model)
        {
            await using var unitOfWork = await User.GetConnection();

            var reminders = await unitOfWork.TaskReminders.GetRemindersByUser(model.UserId);

            var existingReminder = reminders.FirstOrDefault(r => r.TaskId == model.TaskId);

            if (existingReminder != null)
            {
                throw new LogicException("A reminder already exists for this task.");
            }

            var newReminder = new TaskReminder
            {
                Id = Guid.NewGuid(),
                TaskId = model.TaskId,
                UserId = model.UserId,
                RemindTime = model.RemindTime
            };
            
            unitOfWork.TaskReminders.Create(newReminder);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTaskReminder(Guid reminderId, TaskReminderRequestModel model)
        {
            await using var unitOfWork = await User.GetConnection();

            var existingReminder = await unitOfWork.TaskReminders.GetById(reminderId);

            if (existingReminder == null)
            {
                throw new NotFoundException("Task reminder not found.");
            }

            existingReminder.RemindTime = model.RemindTime;

            await unitOfWork.TaskReminders.Update(existingReminder);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTaskReminder(Guid reminderId)
        {
            await using var unitOfWork = await User.GetConnection();

            await unitOfWork.TaskReminders.Delete(reminderId);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTask(Guid taskId, TaskRequestModel task)
        {
            Validate(task);

            if (await CanUpdateTask(taskId))
            {
                await using var unitOfWork = await User.GetConnection();

                var taskInDb = await unitOfWork.Tasks.GetById(taskId);

                if (taskInDb == null)
                {
                    throw new NotFoundException("Task not found.");
                }

                if (taskInDb.System)
                {
                    throw new LogicException("Tasks of this type cannot be updated manually.");
                }

                taskInDb.Title = task.Title;
                taskInDb.Description = task.Description;
                taskInDb.DueDate = task.DueDate;
                taskInDb.TypeId = task.TypeId;

                await unitOfWork.Tasks.Update(taskInDb);

                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new PermissionException("You do not have permission to update this task.");
            }
        }

        public async Task DeleteTask(Guid taskId)
        {
            if (await CanUpdateTask(taskId))
            {
                await using var unitOfWork = await User.GetConnection();

                await unitOfWork.Tasks.Delete(taskId);

                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new PermissionException("You do not have permission to delete this task.");
            }
        }

        public async Task SetCompleted(Guid taskId, bool completed)
        {
            if (await CanUpdateTask(taskId))
            {
                await using var unitOfWork = await User.GetConnection();
            
                var taskInDb = await unitOfWork.Tasks.GetById(taskId);

                if (taskInDb.System)
                {
                    throw new LogicException("Tasks of this type cannot be updated manually.");
                }

                taskInDb.Completed = completed;
                taskInDb.CompletedDate = DateTime.Now;

                await unitOfWork.Tasks.Update(taskInDb);

                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new PermissionException("You do not have permission to edit this task.");
            }
        }

        public async Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null)
        {
            var access = await GetPermissionsForTasksPerson(personId);

            if (access.CanView || access.IsAssignee)
            {
                if (searchOptions == null)
                {
                    searchOptions = new TaskSearchOptions { Status = TaskStatus.Active };
                }
            
                await using var unitOfWork = await User.GetConnection();

                var tasks = await unitOfWork.Tasks.GetByAssignedTo(personId, searchOptions);

                return tasks.Select(t => new TaskModel(t));
            }
            
            throw new PermissionException("You do not have permission to access tasks for this person.");
        }

        private async Task<TaskAccessModel> GetPermissionsForTasksPerson(Guid personId)
        {
            var response = new TaskAccessModel {PersonId = personId};

            var user = await _userService.GetUserById(User.GetUserId());

            if (user.PersonId.HasValue && user.PersonId.Value == personId)
            {
                response.IsAssignee = true;
            }

            var person = await _personService.GetPersonWithTypesById(personId);

            if (person.PersonTypes.StaffId.HasValue)
            {
                await GetPermissionsForTasksStaffUser(response, user, person);
            }

            if (person.PersonTypes.StudentId.HasValue)
            {
                await GetPermissionsForTasksStudentUser(response, user, person);
            }

            if (person.PersonTypes.ContactId.HasValue)
            {
                await GetPermissionsForTasksContactUser(response, user, person);
            }

            return response;
        }

        private async Task<TaskAccessModel> GetPermissionsForTasksStaffUser(TaskAccessModel response, UserModel user,
            PersonSearchResultModel person)
        {
            if (await User.HasPermission(_userService, PermissionRequirement.RequireAll,
                    PermissionValue.PeopleViewAllStaffTasks))
            {
                response.CanView = true;
            }

            if (await User.HasPermission(_userService, PermissionRequirement.RequireAll,
                    PermissionValue.PeopleEditAllStaffTasks))
            {
                response.CanEdit = true;
            }
            
            var canViewManagedStaffTasks = await User.HasPermission(_userService, PermissionRequirement.RequireAll,
                PermissionValue.PeopleViewManagedStaffTasks);

            var canEditManagedStaffTasks = await User.HasPermission(_userService, PermissionRequirement.RequireAll,
                PermissionValue.PeopleEditManagedStaffTasks);

            if (canViewManagedStaffTasks || canEditManagedStaffTasks)
            {
                if (user.PersonId.HasValue && person.Person.Id.HasValue)
                {
                    var staffMember = await _staffMemberService.GetByPersonId(person.Person.Id.Value);
                    var userPerson = await _staffMemberService.GetByPersonId(user.PersonId.Value);

                    if (staffMember is { Id: { } } && userPerson is { Id: { } })
                    {
                        if (await _staffMemberService.IsLineManager(staffMember.Id.Value, 
                                userPerson.Id.Value))
                        {
                            response.CanView = canViewManagedStaffTasks;
                            response.CanEdit = canEditManagedStaffTasks;
                        }
                    }
                }
            }

            return response;
        }

        private async Task<TaskAccessModel> GetPermissionsForTasksContactUser(TaskAccessModel response, UserModel user,
            PersonSearchResultModel person)
        {
            response.CanView = await User.HasPermission(_userService, PermissionRequirement.RequireAll,
                PermissionValue.PeopleViewContactTasks);
            response.CanEdit = await User.HasPermission(_userService, PermissionRequirement.RequireAll,
                PermissionValue.PeopleEditContactTasks);

            return response;
        }

        private async Task<TaskAccessModel> GetPermissionsForTasksStudentUser(TaskAccessModel response, UserModel user,
            PersonSearchResultModel person)
        {
            response.CanView = await User.HasPermission(_userService, PermissionRequirement.RequireAll,
                PermissionValue.StudentViewStudentTasks);
            response.CanEdit = await User.HasPermission(_userService, PermissionRequirement.RequireAll,
                PermissionValue.StudentEditStudentTasks);

            return response;
        }
        
        private async Task<bool> CanUpdateTask(Guid taskId)
        {
            var userId = User.GetUserId();
            
            var task = await GetTaskById(taskId);

            if (task.CreatedById != userId && task.AssignedToId.HasValue)
            {
                var accessResponse = await GetPermissionsForTasksPerson(task.AssignedToId.Value);

                if (accessResponse.CanEdit)
                {
                    return true;
                }

                if (accessResponse.IsAssignee)
                {
                    return task.CreatedById == userId || task.AllowEdit;
                }
            }

            return false;
        }
    }
}
