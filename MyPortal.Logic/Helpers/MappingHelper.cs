﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.ListModels;

namespace MyPortal.Logic.Helpers
{
    public static class MappingHelper
    {

        public static IMapper GetBusinessConfig()
        {
            var entityMappings = new Dictionary<Type, Type>
            {
                {typeof(AcademicYear), typeof(AcademicYearModel)},
                {typeof(Achievement), typeof(AchievementModel)},
                {typeof(AchievementType), typeof(AchievementTypeModel)},
                {typeof(Aspect), typeof(AspectModel)},
                {typeof(AspectType), typeof(AspectTypeModel)},
                {typeof(ApplicationRole), typeof(RoleModel)},
                {typeof(ApplicationUser), typeof(UserModel)},
                {typeof(AttendanceCodeMeaning), typeof(AttendanceCodeMeaningModel)},
                {typeof(AttendanceCode), typeof(AttendanceCodeModel)},
                {typeof(AttendanceMark), typeof(AttendanceMarkModel)},
                {typeof(AttendanceWeek), typeof(AttendanceWeekModel)},
                {typeof(Bulletin), typeof(BulletinModel)},
                {typeof(Directory), typeof(DirectoryModel)},
                {typeof(Document), typeof(DocumentModel)},
                {typeof(DocumentType), typeof(DocumentTypeModel)},
                {typeof(Homework), typeof(HomeworkModel)},
                {typeof(House), typeof(HouseModel)},
                {typeof(LessonPlan), typeof(LessonPlanModel)},
                {typeof(LogNote), typeof(LogNoteModel)},
                {typeof(LogNoteType), typeof(LogNoteTypeModel)},
                {typeof(Person), typeof(PersonModel)},
                {typeof(RegGroup), typeof(RegGroupModel)},
                {typeof(SenStatus), typeof(SenStatusModel)},
                {typeof(StaffMember), typeof(StaffMemberModel)},
                {typeof(Student), typeof(StudentModel)},
                {typeof(StudyTopic), typeof(StudyTopicModel)},
                {typeof(Subject), typeof(SubjectModel)},
                {typeof(Task), typeof(TaskModel)},
                {typeof(TaskType), typeof(TaskTypeModel)},
                {typeof(YearGroup), typeof(YearGroupModel)}
            };

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var entityMapping in entityMappings)
                {
                    cfg.CreateMap(entityMapping.Key, entityMapping.Value).ReverseMap();
                }
            });

            return new Mapper(config);
        }
    }
}
