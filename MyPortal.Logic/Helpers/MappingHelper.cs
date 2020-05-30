using System;
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
        private static Dictionary<Type, Type> _entityMappings = new Dictionary<Type, Type>
        {
            {typeof(AcademicYear), typeof(AcademicYearModel)},
            {typeof(ApplicationRole), typeof(RoleModel)},
            {typeof(ApplicationUser), typeof(UserModel)},
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
            {typeof(YearGroup), typeof(YearGroupModel)}
        };

        public static IMapper GetBusinessConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var entityMapping in _entityMappings)
                {
                    cfg.CreateMap(entityMapping.Key, entityMapping.Value).ReverseMap();
                }
            });

            return new Mapper(config);
        }

        public static Type GetBusinessModelType(Type source)
        {
            Type dest;

            var result = _entityMappings.TryGetValue(source, out dest);

            if (result)
            {
                return dest;
            }

            throw new Exception($"A mapping was not found for the source type {source.Name}.");
        }
    }
}
