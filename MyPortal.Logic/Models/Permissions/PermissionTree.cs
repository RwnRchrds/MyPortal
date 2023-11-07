using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Database.Enums;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Permissions
{
    internal class PermissionTree
    {
        private static List<SystemArea> _systemAreas = new List<SystemArea>
        {
            new("adm", "Admissions")
            {
                ChildAreas = new List<SystemArea>
                {
                    new("adm-app", "Applications")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Applications", PermissionValue.AdmissionsViewApplications),
                            new("Edit", "Edit Applications", PermissionValue.AdmissionsEditApplications)
                        }
                    },
                    new("adm-enq", "Enquiries")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Enquiries", PermissionValue.AdmissionsViewEnquiries),
                            new("Edit", "Edit Enquiries", PermissionValue.AdmissionsEditEnquiries)
                        }
                    },
                    new("adm-int", "Interviews")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Interviews", PermissionValue.AdmissionsViewInterviews),
                            new("Edit", "Edit Interviews", PermissionValue.AdmissionsEditInterviews)
                        }
                    }
                }
            },
            new SystemArea("age", "Agencies")
            {
                Permissions = new List<Permission>
                {
                    new("View", "View Agencies", PermissionValue.AgencyViewAgencies),
                    new("Edit", "Edit Agencies", PermissionValue.AgencyEditAgencies)
                }
            },
            new SystemArea("ass", "Assessment")
            {
                ChildAreas = new List<SystemArea>
                {
                    new("ass-asp", "Aspects")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Aspects", PermissionValue.AssessmentViewAspects),
                            new("Edit", "Edit Aspects", PermissionValue.AssessmentEditAspects)
                        }
                    },
                    new("ass-exm", "Exams")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View Basedata", "View Exam Basedata", PermissionValue.AssessmentViewExamBaseData),
                            new("Edit Basedata", "Edit Exam Basedata", PermissionValue.AssessmentEditExamBaseData),
                            new("Run Assistant", "Run Exam Assistant", PermissionValue.AssessmentRunExamAsst)
                        }
                    },
                    new("ass-grd", "Grade Sets")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Grade Sets", PermissionValue.AssessmentViewGradeSets),
                            new("Edit", "Edit Grade Sets", PermissionValue.AssessmentEditGradeSets)
                        }
                    },
                    new("ass-mks", "Marksheets")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View Templates", "View Marksheet Templates",
                                PermissionValue.AssessmentViewMarksheetTemplates),
                            new("Edit Templates", "Edit Marksheet Templates",
                                PermissionValue.AssessmentEditMarksheetTemplates),
                            new("View Own", "View Own Marksheets", PermissionValue.AssessmentViewOwnMarksheets),
                            new("View All", "View All Marksheets", PermissionValue.AssessmentViewAllMarksheets),
                            new("Edit Own", "Edit Own Marksheets", PermissionValue.AssessmentUpdateOwnMarksheets),
                            new("Edit All", "Edit All Marksheets", PermissionValue.AssessmentUpdateAllMarksheets)
                        }
                    },
                    new("ass-rss", "Result Sets")
                    {
                        Permissions = new List<Permission>
                        {
                            new Permission("View", "View Result Sets", PermissionValue.AssessmentViewResultSets),
                            new Permission("Edit", "Edit Result Sets", PermissionValue.AssessmentEditResultSets)
                        }
                    },
                    new("ass-res", "Results")
                    {
                        Permissions = new List<Permission>
                        {
                            new Permission("View", "View Results", PermissionValue.AssessmentViewResults),
                            new Permission("Edit", "Edit Results", PermissionValue.AssessmentEditResults),
                            new Permission("View Embargoed", "View Embargoed Results",
                                PermissionValue.AssessmentViewEmbargoedResults)
                        }
                    }
                }
            },
            new SystemArea("att", "Attendance")
            {
                ChildAreas = new List<SystemArea>
                {
                    new SystemArea("att-mrk", "Attendance Marks")
                    {
                        Permissions = new List<Permission>
                        {
                            new Permission("View", "View Attendance Marks",
                                PermissionValue.AttendanceViewAttendanceMarks),
                            new Permission("Edit", "Edit Attendance Marks",
                                PermissionValue.AttendanceEditAttendanceMarks),
                            new Permission("Use Restricted", "Use Restricted Codes",
                                PermissionValue.AttendanceUseRestrictedCodes)
                        }
                    }
                }
            },
            new SystemArea("beh", "Behaviour")
            {
                ChildAreas = new List<SystemArea>
                {
                    new("beh-ach", "Achievements")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Achievements", PermissionValue.BehaviourViewAchievements),
                            new("Edit", "Edit Achievements", PermissionValue.BehaviourEditAchievements)
                        }
                    },
                    new("beh-inc", "Incidents")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Incidents", PermissionValue.BehaviourViewIncidents),
                            new("Edit", "Edit Incidents", PermissionValue.BehaviourEditIncidents)
                        }
                    },
                    new("beh-det", "Detentions")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Detentions", PermissionValue.BehaviourViewDetentions),
                            new("Edit", "Edit Detentions", PermissionValue.BehaviourEditDetentions)
                        }
                    },
                    new("beh-exc", "Exclusions")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Exclusions", PermissionValue.BehaviourViewExclusions),
                            new("Edit", "Edit Exclusions", PermissionValue.BehaviourEditExclusions)
                        }
                    },
                    new("beh-rep", "Report Cards")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Report Cards", PermissionValue.BehaviourViewReportCards),
                            new("Edit", "Edit Report Cards", PermissionValue.BehaviourEditReportCards),
                            new("Add/Remove", "Add/Remove Report Cards", PermissionValue.BehaviourAddRemoveReportCards)
                        }
                    }
                }
            },
            new SystemArea("cur", "Curriculum")
            {
                ChildAreas = new List<SystemArea>
                {
                    new("cur-str", "Academic Structure")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View/Edit", "View/Edit Academic Structure",
                                PermissionValue.CurriculumAcademicStructure)
                        }
                    },
                    new("cur-yer", "Academic Years")
                    {
                        Permissions = new List<Permission>
                        {
                            new("Edit", "Edit Academic Years", PermissionValue.CurriculumEditAcademicYears)
                        }
                    },
                    new("cur-cov", "Cover")
                    {
                        Permissions = new List<Permission>
                        {
                            new("Run", "Run Arrange Cover", PermissionValue.CurriculumArrangeCover)
                        }
                    },
                    new("cur-hmw", "Homework")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Homework", PermissionValue.CurriculumViewHomework),
                            new("Edit", "Edit Homework", PermissionValue.CurriculumEditHomework)
                        }
                    },
                    new("cur-pln", "Lesson Plans")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Lesson Plans", PermissionValue.CurriculumViewLessonPlans),
                            new("Edit", "Edit Lesson Plans", PermissionValue.CurriculumEditLessonPlans)
                        }
                    },
                    new("cur-tpc", "Study Topics")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Study Topics", PermissionValue.CurriculumViewStudyTopics),
                            new("Edit", "Edit Study Topics", PermissionValue.CurriculumEditStudyTopics)
                        }
                    }
                },
            },
            new SystemArea("fin", "Finance")
            {
                ChildAreas = new List<SystemArea>
                {
                    new("fin-acc", "Accounts")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Accounts", PermissionValue.FinanceViewAccounts),
                            new("Edit", "Edit Accounts", PermissionValue.FinanceEditAccounts)
                        }
                    },
                    new("fin-prd", "Products")
                    {
                        Permissions = new List<Permission>
                        {
                            new("View", "View Products", PermissionValue.FinanceViewProducts)
                        }
                    }
                }
            },
            new SystemArea("peo", "People"),
            new SystemArea("pro", "Profiles"),
            new SystemArea("sch", "School"),
            new SystemArea("stu", "Student"),
            new SystemArea("sys", "System")
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