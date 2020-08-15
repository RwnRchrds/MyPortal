using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.List;

namespace MyPortal.Logic.Models.Entity
{
    public class DirectoryModel
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public bool Private { get; set; }
        public bool StaffOnly { get; set; }

        public virtual DirectoryModel Parent { get; set; }
        public virtual BulletinModel Bulletin { get; set; }
        public virtual HomeworkModel Homework { get; set; }
        public virtual PersonModel Person { get; set; }
        public virtual LessonPlanModel LessonPlan { get; set; }

        public DirectoryChildListModel GetListModel()
        {
            return new DirectoryChildListModel(this);
        }
    }
}
