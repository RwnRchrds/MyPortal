using System;
using System.Linq;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Curriculum;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class CurriculumService : BaseService, ICurriculumService
    {
        public async Task CreateBand(params CurriculumBandModel[] bandModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var bandModel in bandModels)
                {
                    if (!await unitOfWork.CurriculumBands.CheckUniqueCode(bandModel.AcademicYearId, bandModel.Code))
                    {
                        throw new InvalidDataException($"Curriculum band with code {bandModel.Code} already exists.");
                    }

                    var band = new CurriculumBand
                    {
                        Code = bandModel.Code,
                        Description = bandModel.Description,
                        AcademicYearId = bandModel.AcademicYearId,
                        CurriculumYearGroupId = bandModel.CurriculumYearGroupId
                    };

                    unitOfWork.CurriculumBands.Create(band);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateBand(params CurriculumBandModel[] bandModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var bandModel in bandModels)
                {
                    var bandInDb = await unitOfWork.CurriculumBands.GetByIdForEditing(bandModel.Id);

                    bandInDb.Code = bandModel.Code;
                    bandInDb.Description = bandModel.Description;
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteBand(params Guid[] bandIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var bandId in bandIds)
                {
                    await unitOfWork.CurriculumBands.Delete(bandId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateBandMembership(params CurriculumBandMembership[] bandMemberships)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var bandMembership in bandMemberships)
                {
                    var membership = new CurriculumBandMembership
                    {
                        StudentId = bandMembership.StudentId,
                        BandId = bandMembership.BandId,
                        StartDate = bandMembership.StartDate,
                        EndDate = bandMembership.EndDate
                    };

                    unitOfWork.CurriculumBandMemberships.Create(membership);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateBlock(params CreateCurriculumBlockModel[] blockModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var blockModel in blockModels)
                {
                    if (!blockModel.BandIds.Any())
                    {
                        throw new InvalidDataException("Curriculum block must be assigned to at least one band.");
                    }

                    var block = new CurriculumBlock
                    {
                        Code = blockModel.BlockModel.Code,
                        Description = blockModel.BlockModel.Description
                    };

                    Guid academicYearId = Guid.Empty;

                    for (var i = 0; i < blockModel.BandIds.Length; i++)
                    {
                        var band = await unitOfWork.CurriculumBands.GetById(blockModel.BandIds[i]);

                        if (band == null)
                        {
                            throw new NotFoundException($"Curriculum band not found: {blockModel.BandIds[i]}.");
                        }

                        if (i == 0)
                        {
                            academicYearId = band.AcademicYearId;
                        }
                        else if (band.AcademicYearId != academicYearId)
                        {
                            throw new InvalidDataException("Curriculum blocks cannot span multiple academic years.");
                        }

                        block.BandAssignments.Add(new CurriculumBandBlockAssignment
                        {
                            BlockId = blockModel.BlockModel.Id,
                            BandId = blockModel.BandIds[i]
                        });
                    }

                    if (!await unitOfWork.CurriculumBlocks.CheckUniqueCode(academicYearId, blockModel.BlockModel.Code))
                    {
                        throw new InvalidDataException(
                            $"Curriculum block with code {blockModel.BlockModel.Code} already exists.");
                    }

                    unitOfWork.CurriculumBlocks.Create(block);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateBlock(params CurriculumBlockModel[] blockModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var blockModel in blockModels)
                {
                    var blockInDb = await unitOfWork.CurriculumBlocks.GetByIdForEditing(blockModel.Id);

                    if (blockInDb == null)
                    {
                        throw new NotFoundException("Curriculum block not found.");
                    }

                    blockInDb.Code = blockModel.Code;
                    blockInDb.Description = blockModel.Description;
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteBlock(params Guid[] blockIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var blockId in blockIds)
                {
                    await unitOfWork.CurriculumBlocks.Delete(blockId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateGroup(params CurriculumGroupModel[] groupModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var groupModel in groupModels)
                {
                    var academicYearId = await unitOfWork.CurriculumBlocks.GetAcademicYearId(groupModel.BlockId);

                    if (academicYearId == null)
                    {
                        throw new NotFoundException("Academic year not found for block.");
                    }

                    if (!await unitOfWork.CurriculumGroups.CheckUniqueCode(academicYearId.Value, groupModel.Code))
                    {
                        throw new InvalidDataException($"Curriculum group with code {groupModel.Code} already exists.");
                    }

                    var group = new CurriculumGroup
                    {
                        Code = groupModel.Code,
                        Description = groupModel.Description,
                        BlockId = groupModel.BlockId
                    };

                    unitOfWork.CurriculumGroups.Create(group);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateGroup(params CurriculumGroupModel[] groupModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var groupModel in groupModels)
                {
                    var groupInDb = await unitOfWork.CurriculumGroups.GetById(groupModel.Id);

                    if (groupInDb == null)
                    {
                        throw new NotFoundException("Curriculum group not found.");
                    }

                    groupInDb.Code = groupModel.Code;
                    groupInDb.Description = groupModel.Description;
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteGroup(params Guid[] groupIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var groupId in groupIds)
                {
                    await unitOfWork.CurriculumGroups.Delete(groupId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateBandAssignment(params (Guid bandId, Guid blockId)[] bandAssignments)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var bandAssignment in bandAssignments)
                {
                    var assignment = new CurriculumBandBlockAssignment
                    {
                        BandId = bandAssignment.bandId,
                        BlockId = bandAssignment.blockId
                    };

                    unitOfWork.CurriculumBandBlockAssignments.Create(assignment);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteBandAssignment(params Guid[] bandAssignmentIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var assignmentId in bandAssignmentIds)
                {
                    await unitOfWork.CurriculumBandBlockAssignments.Delete(assignmentId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateGroupMembership(params CurriculumGroupMembershipModel[] groupMemberships)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var groupMembership in groupMemberships)
                {
                    var membership = new CurriculumGroupMembership
                    {
                        StudentId = groupMembership.StudentId,
                        GroupId = groupMembership.GroupId,
                        StartDate = groupMembership.StartDate,
                        EndDate = groupMembership.EndDate
                    };

                    unitOfWork.CurriculumGroupMemberships.Create(membership);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateGroupMembership(params CurriculumGroupMembershipModel[] groupMemberships)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var groupMembership in groupMemberships)
                {
                    var membershipInDb = await unitOfWork.CurriculumGroupMemberships.GetByIdForEditing(groupMembership.Id);

                    if (membershipInDb == null)
                    {
                        throw new NotFoundException("Group membership not found.");
                    }

                    membershipInDb.StartDate = groupMembership.StartDate;
                    membershipInDb.EndDate = groupMembership.EndDate;
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteGroupMembership(params Guid[] membershipIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var membershipId in membershipIds)
                {
                    await unitOfWork.CurriculumGroupMemberships.Delete(membershipId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}