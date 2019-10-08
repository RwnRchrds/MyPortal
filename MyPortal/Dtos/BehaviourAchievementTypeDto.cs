using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// Category of achievement.
    /// </summary>
    
    public class BehaviourAchievementTypeDto
    {

        public int Id { get; set; }

        
        public string Description { get; set; }

        public int DefaultPoints { get; set; }

        public bool System { get; set; }

        
        
    }
}