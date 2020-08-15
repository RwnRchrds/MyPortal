using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Curriculum;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces
{
    public interface ICurriculumService : IService
    {
        Task CreateBand(params CurriculumBandModel[] bandModels);
        Task UpdateBand(params CurriculumBandModel[] bandModels);
        Task DeleteBand(params Guid[] bandIds);

        Task CreateBlock(params CreateCurriculumBlockModel[] blockModels);
        Task UpdateBlock(params CurriculumBlockModel[] blockModels);
        Task DeleteBlock(params Guid[] blockIds);

        Task CreateGroup(params CurriculumGroupModel[] groupModels);
        Task UpdateGroup(params CurriculumGroupModel[] groupModels);
        Task DeleteGroup(params Guid[] groupIds);

        Task CreateBandAssignment(params (Guid bandId, Guid blockId)[] bandAssignments);
        Task DeleteBandAssignment(params Guid[] bandAssignmentIds);

        Task CreateGroupMembership(params CurriculumGroupMembershipModel[] groupMemberships);
        Task DeleteGroupMembership(params Guid[] membershipIds);
    }
}