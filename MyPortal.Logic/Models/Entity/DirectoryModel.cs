using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.DataGrid;

namespace MyPortal.Logic.Models.Entity
{
    public class DirectoryModel : BaseModel
    {
        public Guid? ParentId { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public bool Private { get; set; }
        
        public bool StaffOnly { get; set; } 

        public virtual DirectoryModel Parent { get; set; }
        public virtual BulletinModel Bulletin { get; set; }
        public virtual HomeworkModel HomeworkItem { get; set; }
        public virtual HomeworkSubmissionModel HomeworkSubmission { get; set; }
        public virtual PersonModel Person { get; set; }
        public virtual LessonPlanModel LessonPlan { get; set; }
        public virtual AgencyModel Agency { get; set; }

        public DirectoryChildListModel GetListModel()
        {
            return new DirectoryChildListModel(this);
        }
    }
}
