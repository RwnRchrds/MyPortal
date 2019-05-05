using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Helpers
{
    public static class AttendanceHelper
    {
        private static readonly MyPortalDbContext _context;

        static AttendanceHelper()
        {
            _context = new MyPortalDbContext();           
        }

        public static AttendanceRegisterCodeMeaning GetMeaning(string code)
        {
            var codeInDb = _context.AttendanceCodes.SingleOrDefault(x => x.Code == code);

            return codeInDb?.AttendanceRegisterCodeMeaning;
        }

        public static bool VerifyRegisterCodes(ListContainer<AttendanceRegisterMark> register)
        {
            var codesVerified = true;
            var codes = _context.AttendanceCodes.ToList();

            foreach (var mark in register.Objects)
            {
                if (codes.All(x => x.Code != mark.Mark))
                {
                    codesVerified = false;
                }
            }

            return codesVerified;
        }
    }
}