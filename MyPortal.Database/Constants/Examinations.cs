using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Constants
{
    public class Examinations
    {
        private static string SeatColumns = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GetRoomColumnHeader(int columnNumber)
        {
            if (columnNumber < 0 || columnNumber > 25)
            {
                throw new ArgumentException("Invalid column number.");
            }

            return SeatColumns[columnNumber].ToString();
        }
    }
}
