using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Enums;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Requests.Curriculum;

namespace MyPortal.Logic.Models.Permissions
{
    internal class PermissionTree
    {
        private static List<SystemArea> _systemAreas = new List<SystemArea>
        {
            new ("adm", "Admissions")
            {
                ChildAreas = new List<SystemArea>
                {
                    new ("adm-app", "Applications")
                    {
                        Permissions = new List<Permission>
                        {
                            new ("View", "View Applications", PermissionValue.AdmissionsViewApplications),
                            new ("Edit", "Edit Applications", PermissionValue.AdmissionsEditApplications)
                        }
                    },
                    new ("adm-enq", "Enquiries")
                    {
                        Permissions = new List<Permission>
                        {
                            new ("View", "View Enquiries", PermissionValue.AdmissionsViewEnquiries),
                            new ("Edit", "Edit Enquiries", PermissionValue.AdmissionsEditEnquiries)
                        }
                    },
                    new ("adm-int", "Interviews")
                    {
                        Permissions = new List<Permission>()
                        {
                            new ("View", "View Interviews", PermissionValue.AdmissionsViewInterviews),
                            new ("Edit", "Edit Interviews", PermissionValue.AdmissionsEditInterviews)
                        }
                    }
                }
            },
            new SystemArea("age", "Agencies")
            {
                Permissions = new List<Permission>
                {
                    new ("View", "View Agencies", PermissionValue.AgencyViewAgencies),
                    new ("Edit", "Edit Agencies", PermissionValue.AgencyEditAgencies)
                }
            },
            new SystemArea("ass", "Assessment")
            {
                ChildAreas = new List<SystemArea>
                {
                    new ("ass-asp", "Aspects"),
                    new ("ass-exm", "Exams"),
                    new ("ass-grd", "Grade Sets"),
                    new ("ass-mks", "Marksheets"),

                }
            },
            new SystemArea("att", "Attendance"),
            new SystemArea("beh", "Behaviour"),
            new SystemArea("cur", "Curriculum"),
            new SystemArea("fin", "Finance"),
            new SystemArea("peo", "People"),
            new SystemArea("pro", "Profiles"),
            new SystemArea("sch", "School"),
            new SystemArea("stu", "Student"),
            new SystemArea("sys", "System")
        };

        private static List<Permission> _permissions = new List<Permission>
        {

        };

        public static TreeNode Create(byte[] permissions)
        {
            var root = TreeNode.CreateRoot("#", "Permissions");

            foreach (var systemArea in _systemAreas)
            {
                root.Children.Add(new TreeNode
                {
                    Id = systemArea.Id,
                    Text = systemArea.Name,
                    Children = GetChildren(systemArea, permissions)
                });
            }

            return root;
        }

        private static List<TreeNode> GetChildren(SystemArea systemArea, byte[] permissions)
        {
            var currentPermissions = new BitArray(permissions);

            var childAreas = systemArea.ChildAreas.Select(c => new TreeNode
            {
                Id = c.Id,
                Text = c.Name,
                Children = GetChildren(c, permissions)
            }).ToList();

            var childPermissions = systemArea.Permissions.Select(p => new TreeNode
            {
                Id = p.Value.ToString(),
                Text = p.ShortName,
                State = new TreeNodeState
                {
                    Opened = false,
                    Disabled = false,
                    Selected = currentPermissions[p.Value]
                }
            }).ToList();

            return childAreas.Union(childPermissions).ToList();
        }
    }
}
