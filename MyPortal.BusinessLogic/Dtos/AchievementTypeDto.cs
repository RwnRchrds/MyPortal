using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AchievementTypeDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DefaultPoints { get; set; }
        public bool System { get; set; }
    }
}
