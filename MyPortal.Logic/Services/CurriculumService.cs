using System;
using System.Linq;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Curriculum;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class CurriculumService : BaseService, ICurriculumService
    {
        private readonly ICurriculumBandRepository _bandRepository;
        private readonly ICurriculumBlockRepository _blockRepository;
        private readonly ICurriculumGroupRepository _groupRepository;
        private readonly ICurriculumBandBlockAssignmentRepository _assignmentRepository;
        private readonly ICurriculumGroupMembershipRepository _groupMembershipRepository;

        public CurriculumService(ICurriculumBandRepository bandRepository, ICurriculumBlockRepository blockRepository,
            ICurriculumGroupRepository groupRepository, ICurriculumBandBlockAssignmentRepository assignmentRepository,
            ICurriculumGroupMembershipRepository groupMembershipRepository) : base("Curriculum object")
        {
            _bandRepository = bandRepository;
            _blockRepository = blockRepository;
            _groupRepository = groupRepository;
            _assignmentRepository = assignmentRepository;
            _groupMembershipRepository = groupMembershipRepository;
        }
        
        public override void Dispose()
        {
            _bandRepository.Dispose();
            _blockRepository.Dispose();
            _groupRepository.Dispose();
            _assignmentRepository.Dispose();
            _groupMembershipRepository.Dispose();
        }

        public async Task CreateBand(params CurriculumBandModel[] bandModels)
        {
            foreach (var bandModel in bandModels)
            {
                if (!await _bandRepository.CheckUniqueCode(bandModel.AcademicYearId, bandModel.Code))
                {
                    throw BadRequest($"Curriculum band with code {bandModel.Code} already exists.");
                }
                
                var band = new CurriculumBand
                {
                    Code = bandModel.Code,
                    Description = bandModel.Description,
                    AcademicYearId = bandModel.AcademicYearId,
                    CurriculumYearGroupId = bandModel.CurriculumYearGroupId
                };
                
                _bandRepository.Create(band);
            }

            await _bandRepository.SaveChanges();
        }

        public async Task UpdateBand(params CurriculumBandModel[] bandModels)
        {
            foreach (var bandModel in bandModels)
            {
                var bandInDb = await _bandRepository.GetByIdWithTracking(bandModel.Id);

                bandInDb.Code = bandModel.Code;
                bandInDb.Description = bandModel.Description;
            }

            await _bandRepository.SaveChanges();
        }

        public async Task DeleteBand(params Guid[] bandIds)
        {
            foreach (var bandId in bandIds)
            {
                await _bandRepository.Delete(bandId);
            }

            await _bandRepository.SaveChanges();
        }

        public async Task CreateBlock(params CreateCurriculumBlockModel[] blockModels)
        {
            foreach (var blockModel in blockModels)
            {
                if (!blockModel.BandIds.Any())
                {
                    throw BadRequest("Curriculum block must be assigned to at least one band.");
                }
                
                var block = new CurriculumBlock
                {
                    Code = blockModel.BlockModel.Code,
                    Description = blockModel.BlockModel.Description
                };

                Guid academicYearId = Guid.Empty;

                for (var i = 0; i < blockModel.BandIds.Length; i++)
                {
                    var band = await _bandRepository.GetById(blockModel.BandIds[i]);

                    if (band == null)
                    {
                        throw NotFound($"Curriculum band not found: {blockModel.BandIds[i]}.");
                    }
                    
                    if (i == 0)
                    {
                        academicYearId = band.AcademicYearId;
                    }
                    else if (band.AcademicYearId != academicYearId)
                    {
                        throw BadRequest("Curriculum blocks cannot span multiple academic years.");
                    }
                    
                    block.BandAssignments.Add(new CurriculumBandBlockAssignment
                    {
                        BlockId = blockModel.BlockModel.Id,
                        BandId = blockModel.BandIds[i]
                    });
                }

                if (!await _blockRepository.CheckUniqueCode(academicYearId, blockModel.BlockModel.Code))
                {
                    throw BadRequest($"Curriculum block with code {blockModel.BlockModel.Code} already exists.");
                }

                _blockRepository.Create(block);
            }

            await _blockRepository.SaveChanges();
        }

        public async Task UpdateBlock(params CurriculumBlockModel[] blockModels)
        {
            foreach (var blockModel in blockModels)
            {
                var blockInDb = await _blockRepository.GetByIdWithTracking(blockModel.Id);

                if (blockInDb == null)
                {
                    throw NotFound("Curriculum block not found.");
                }

                blockInDb.Code = blockModel.Code;
                blockInDb.Description = blockModel.Description;
            }

            await _blockRepository.SaveChanges();
        }

        public async Task DeleteBlock(params Guid[] blockIds)
        {
            foreach (var blockId in blockIds)
            {
                await _blockRepository.Delete(blockId);
            }

            await _blockRepository.SaveChanges();
        }

        public async Task CreateGroup(params CurriculumGroupModel[] groupModels)
        {
            foreach (var groupModel in groupModels)
            {
                var academicYearId = await _blockRepository.GetAcademicYearId(groupModel.BlockId);
                
                if (academicYearId == null)
                {
                    throw NotFound("Academic year not found for block.");
                }

                if (!await _groupRepository.CheckUniqueCode(academicYearId.Value, groupModel.Code))
                {
                    throw BadRequest($"Curriculum group with code {groupModel.Code} already exists.");
                }

                var group = new CurriculumGroup
                {
                    Code = groupModel.Code,
                    Description = groupModel.Description,
                    BlockId = groupModel.BlockId
                };
                
                _groupRepository.Create(group);
            }

            await _groupRepository.SaveChanges();
        }

        public async Task UpdateGroup(params CurriculumGroupModel[] groupModels)
        {
            foreach (var groupModel in groupModels)
            {
                var groupInDb = await _groupRepository.GetById(groupModel.Id);

                if (groupInDb == null)
                {
                    throw NotFound("Curriculum group not found.");
                }

                groupInDb.Code = groupModel.Code;
                groupInDb.Description = groupModel.Description;
            }

            await _groupRepository.SaveChanges();
        }

        public async Task DeleteGroup(params Guid[] groupIds)
        {
            foreach (var groupId in groupIds)
            {
                await _groupRepository.Delete(groupId);
            }

            await _groupRepository.SaveChanges();
        }

        public async Task CreateBandAssignment(params (Guid bandId, Guid blockId)[] bandAssignments)
        {
            foreach (var bandAssignment in bandAssignments)
            {
                var assignment = new CurriculumBandBlockAssignment
                {
                    BandId = bandAssignment.bandId,
                    BlockId = bandAssignment.blockId
                };

                _assignmentRepository.Create(assignment);
            }

            await _assignmentRepository.SaveChanges();
        }

        public async Task DeleteBandAssignment(params Guid[] bandAssignmentIds)
        {
            foreach (var assignmentId in bandAssignmentIds)
            {
                await _assignmentRepository.Delete(assignmentId);
            }

            await _assignmentRepository.SaveChanges();
        }

        public async Task CreateGroupMembership(params (Guid studentId, Guid groupId)[] groupMemberships)
        {
            foreach (var groupMembership in groupMemberships)
            {
                var membership = new CurriculumGroupMembership
                {
                    StudentId = groupMembership.studentId,
                    GroupId = groupMembership.groupId
                };
                
                _groupMembershipRepository.Create(membership);
            }

            await _groupMembershipRepository.SaveChanges();
        }

        public async Task DeleteGroupMembership(params Guid[] membershipIds)
        {
            foreach (var membershipId in membershipIds)
            {
                await _groupMembershipRepository.Delete(membershipId);
            }
            
            await _groupMembershipRepository.SaveChanges();
        }
    }
}