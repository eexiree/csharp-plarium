using System;

namespace Task_A.Classes
{
    public class Date
    {
        private Day _day;
        private Month _month;
        private Year _year;

        public int DaysInMonth => Month.DaysInMonth(_month.val, IsCurrentYearLeap);
        public bool IsCurrentYearLeap => Year.IsLeap(_year.val);

        public Date(int day, int month, int year)
        {
            try
            {
                _year = new Year(year);
                _month = new Month(month);
                _day = DaysInMonth >= day && day > 0 ? new Day(day) : throw new ArgumentOutOfRangeException($"The value of day must be in the range between 1..{DaysInMonth}");
            }
            catch(ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Invalid input data\n{ex.Message}");
            }
        }

        public Date(string date)
        {
            string[] extracted = date.Split('.');
            if (extracted.Length > 3)
                throw new ArgumentOutOfRangeException("Invalid input");
            try
            {
                int temp;
                int.TryParse(extracted[2], out temp);
                _year = new Year(temp);
                int.TryParse(extracted[1], out temp);
                _month = new Month(temp);
                int.TryParse(extracted[0], out temp);
                _day = DaysInMonth >= temp && temp > 0 ? new Day(temp) : throw new ArgumentOutOfRangeException($"The value of day must be in the range between 1..{DaysInMonth}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Invalid input data\n{ex.Message}");
            }

        }


        #region Methods
        
        public DayOfWeek GetDayOfWeek()
        {
            return ValueOf(FirstDayOfYear() + DaysBetween(new Date(1, 1, _year.val)));
        }

        public int GetDayOfYear()
        {
            int days = 0;
            for (int i = 1; i < _month.val; i++)
            {
                days += Month.DaysInMonth(i, Year.IsLeap(_year.val));
            }
            return days + _day.val;
        }

        public int DaysBetween(Date other)
        {
            if (Equals(other))
                return 0;
            Date preceding, subsequent;
            if (_year.val > other._year.val)
            {
                subsequent = other;
                preceding = this;
            }
            else if (_year.val < other._year.val)
            {
                subsequent = this;
                preceding = other;
            }
            else return Math.Abs(GetDayOfYear() - other.GetDayOfYear());

            int daysUpToPreceding = subsequent.IsCurrentYearLeap ? 366 - subsequent.GetDayOfYear() : 365 - subsequent.GetDayOfYear();
          
            for(int i = subsequent._year.val; i < preceding._year.val - 1; i++)
            {
                daysUpToPreceding += Year.IsLeap(i) ? 366 : 365;
            }
            return daysUpToPreceding + preceding.GetDayOfYear();
        }

        public static DayOfWeek ValueOf(int index)
        {
            return (DayOfWeek)(index % 7);
        }

        public override string ToString()
        {
            return $"Date is:\t{_day}.{_month}.{_year}\nDays in Month:\t{DaysInMonth}\nIs year leap:\t{IsCurrentYearLeap}\nDay of week is:\t{GetDayOfWeek()}\nDay of year is:\t{GetDayOfYear()}\n";
        }

        public override bool Equals(object obj)
        {
            Date other = obj as Date;
            return _day.val == other._day.val &&
                   _month.val == other._month.val &&
                   _year.val == other._year.val ? true : false;
        }

        private int FirstDayOfYear()
        {
            int dayOfYear = 0;
            for (int i = 1900; i < _year.val; i++)
            {
                if (Year.IsLeap(i))
                    dayOfYear += 2;
                else dayOfYear++;
            }
            return (int)ValueOf(dayOfYear);
        }

        #endregion

        #region Nested Classes

        #region Day
        private class Day 
        {
            public int val
            {
                get;
                private set;
            }

            public Day(int day)
            {
                val = day;
            }

            public override string ToString()
            {
                return val.ToString();
            }
        }
        #endregion

        #region Month
        private class Month
        {
            public int val
            {
                get;
                private set;
            }

            public Month(int month)
            {
                val = month <= 12 && month > 0 ? month : throw new ArgumentOutOfRangeException("The value of month must be in the range between 1..12");
            }

            public static int DaysInMonth(int month, bool IsYearLeap)
            {
                if (month > 12 || month <= 0)
                    throw new ArgumentOutOfRangeException();
                if (month == 2)
                {
                    return IsYearLeap ? 29 : 28;
                }
                else if(month < 8)
                    return (month & 1) == 1 ? 31 : 30;
                else 
                    return (month & 1) == 0 ? 31 : 30;
            }

            public override string ToString()
            {
                return val.ToString();
            }
        }
        #endregion

        #region Year
        private class Year
        {
            public int val
            {
                get;
                private set;
            }

            public static bool IsLeap(int year)
            {
                return year % 4 == 0 && (year % 400 == 0 && year % 1000 == 0 || year % 100 != 0) ? true : false; // Високосный год - каждый 4, кроме тех которые нацело делятся 100 (1700, 1800, 1900...), кроме 2000 и 2400 годов
            }

            public Year(int year)
            {
                val = year >= 1900 ? year : throw new ArgumentOutOfRangeException("The value of year must be greater or equal to 1900");
            }

            public override string ToString()
            {
                return val.ToString();
            }
        }
        #endregion

        #endregion

        public enum DayOfWeek
        {
            Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        }
    }
}
