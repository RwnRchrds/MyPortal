using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyPortal.Logic.Constants
{
    public enum Permission
    {
        #region Admin
        
        [Display(GroupName = "Admin", Description = "Edit Users")]
        EditUsers = 0x10,

        [Display(GroupName = "Admin", Description = "Edit Roles")]
        EditRoles = 0x11,

        #endregion

        #region Assessment

        [Display(GroupName = "Assessment", Description = "Edit Result Sets")]
        EditResultSets = 0x20,

        [Display(GroupName = "Assessment", Description = "View Result Sets")]
        ViewResultSets = 0x21,

        [Display(GroupName = "Assessment", Description = "Edit Results")]
        EditResults = 0x22,

        #endregion

        #region Attendance

        #endregion

        #region Behaviour

        #endregion

        #region Curriculum

        #endregion

        #region Documents

        #endregion

        #region Finance

        #endregion

        #region Pastoral

        #endregion

        #region People

        #endregion

        #region Personnel

        #endregion

        #region Profiles

        #endregion

        #region Staff

        #endregion

        #region Students

        #endregion

        #region System

        #endregion
    }
}
