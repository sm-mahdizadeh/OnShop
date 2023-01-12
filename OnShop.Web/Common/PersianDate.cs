using System;
using System.Globalization;
using System.Linq;

namespace OnShop.Web.Common
{
    public sealed class PersianDate
    {
        readonly PersianCalendar _perCal;
        DateTime _cal;
        readonly string[] _daynames = new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه" };
        readonly string[] Months = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        
        public int Second { get { return _perCal.GetSecond(_cal); } }
        public int Minute { get { return _perCal.GetMinute(_cal); } }
        public int Hour { get { return _perCal.GetHour(_cal); } }
        public int Day { get { return _perCal.GetDayOfMonth(_cal); } }
        public int Month { get { return _perCal.GetMonth(_cal); } }
        public int Year { get { return _perCal.GetYear(_cal); } }

        public int DayOfYear { get { return _perCal.GetDayOfYear(_cal); } }
        public DayOfWeek DayOfWeek { get { return _perCal.GetDayOfWeek(_cal); } }
        public int WeekofYear { get { return _perCal.GetWeekOfYear(_cal, CalendarWeekRule.FirstDay, DayOfWeek.Saturday); } }
        public bool IsLeapYear { get { return _perCal.IsLeapYear(Year, PersianCalendar.PersianEra); } }
        public string Dayname { get { return _daynames[(_perCal.GetDayOfWeek(_cal).GetHashCode())]; } }


        public string Date { get { return string.Concat(Year, "/", Format(Month), "/", Format(Day)); } }
        public string Time { get { return string.Concat(Format(Hour), ":", Format(Minute), ":", Format(Second)); } }
        public string DateTimeString { get { return string.Concat(Date, "-", Time); } }

        #region public pethod
        public DateTime DateTime { get { return _cal; } }
        #endregion

        #region private methods
        private static string Format(int digit)
        {
            if (digit < 10)
                return string.Concat("0", digit);
            else
                return digit.ToString();
        }
        private static string[] SplitDate(string Date)
        {
            return Date.Split('/');
        }

        private static string[] SplitTime(string Time)
        {
            return Time.Split(':');
        }

        private static string[] SplitDateTime(string DateTime)
        {
            return DateTime.Split('-');
        }
        #endregion



        public void InitializeCalender()
        {
            _cal = DateTime.Now;


        }
        public void InitializeCalender(int _year, int _month, int _day, int _hour, int _minutes, int _second)
        {

            _cal = _perCal.ToDateTime(_year, _month, _day, _hour, _minutes, _second, 0);

        }
        public void InitializeCalender(string PersianDate)
        {
            string[] dates = new string[] { "0", "0", "0" };
            string[] times = new string[] { "0", "0", "0" };
            if (PersianDate.Contains('-'))
            {
                var temp = SplitDateTime(PersianDate);
                dates = SplitDate(temp[0]);
                times = SplitTime(temp[1]);
            }
            else
            {
                dates = SplitDate(PersianDate);
            }
            if (dates.Length == 3 && times.Length == 3)
                InitializeCalender(
                          int.Parse(dates[0]),
                          int.Parse(dates[1]),
                          int.Parse(dates[2]),
                          int.Parse(times[0]),
                          int.Parse(times[1]),
                          int.Parse(times[2]));
            else if (dates.Length == 3 && times.Length == 2)
                InitializeCalender(
                          int.Parse(dates[0]),
                          int.Parse(dates[1]),
                          int.Parse(dates[2]),
                          int.Parse(times[0]),
                          int.Parse(times[1]),
                          0);
            else
                InitializeCalender();


        }
        public void InitializeCalender(DateTime EnglishDate)
        {

            _cal = EnglishDate;

        }



        public PersianDate()
        {
            _perCal = new PersianCalendar();
            InitializeCalender();
        }
        public PersianDate(string DateTime)
        {
            _perCal = new PersianCalendar();
            InitializeCalender(DateTime);
        }
        public PersianDate(int year, int month, int day, int hour, int minutes, int second)
        {
            _perCal = new PersianCalendar();
            InitializeCalender(year, month, day, hour, minutes, second);
        }
        public PersianDate(DateTime EnglishDate)
        {
            _perCal = new PersianCalendar();
            InitializeCalender(EnglishDate);
        }
        public PersianDate(PersianDate perDate)
        {
            _perCal = new PersianCalendar();
            InitializeCalender(perDate._cal);
        }

        public override string ToString()
        {
            return DateTimeString;
        }
        public string ToString(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                return DateTimeString;

            return format
                .Replace("yyyy", $"{Year}")
                .Replace("MMMM", Months[Month - 1])
                .Replace("dddd", _daynames[((int)DayOfWeek) - 1])
                .Replace("yyy", $"{Year}")
                .Replace("MMM", Months[Month - 1])
                .Replace("ddd", _daynames[((int)DayOfWeek) - 1])
                .Replace("yy", Year.ToString()[2..])
                .Replace("MM", $"{Month:00}")
                .Replace("dd", $"{Day:00}")
                .Replace("hh", $"{Hour:00}")
                .Replace("mm", $"{Minute:00}")
                .Replace("ss", $"{Second:00}")
                .Replace("M", $"{Month}")
                .Replace("d", $"{Day}")
                .Replace("h", $"{Hour}")
                .Replace("m", $"{Minute}")
                .Replace("s", $"${Second}");
        }

        public void Addseconds(int seconds)
        {
            _cal = _cal.AddSeconds(seconds);
        }
        public void Addminutes(int minutes)
        {
            _cal = _cal.AddMinutes(minutes);
        }
        public void Addhour(int hours)
        {
            _cal = _cal.AddHours(hours);
        }
        public void Adddays(int days)
        {
            _cal = _cal.AddDays(days);
        }
        public void Addmonths(int months)
        {
            _cal = _cal.AddMonths(months);
        }
        public void Addyears(int years)
        {
            _cal = _cal.AddYears(years);
        }

        public PersianDate Clone()
        {
            return new PersianDate(this);
        }

        public static PersianDate operator +(PersianDate d, TimeSpan t)
        {
            d._cal += t;
            return d;
        }
        public static PersianDate operator -(PersianDate d, TimeSpan t)
        {
            d._cal -= t;
            return d;
        }
        public static TimeSpan operator -(PersianDate d1, PersianDate d2)
        {

            return (d1._cal - d2._cal);
        }
        public static bool operator ==(PersianDate d1, PersianDate d2)
        {
            return (d1._cal == d2._cal);
        }
        public static bool operator !=(PersianDate d1, PersianDate d2)
        {
            return (d1._cal != d2._cal);
        }
        public static bool operator >(PersianDate d1, PersianDate d2)
        {
            return (d1._cal > d2._cal);
        }
        public static bool operator >=(PersianDate d1, PersianDate d2)
        {
            return (d1._cal >= d2._cal);
        }
        public static bool operator <(PersianDate d1, PersianDate d2)
        {
            return (d1._cal < d2._cal);
        }
        public static bool operator <=(PersianDate d1, PersianDate d2)
        {
            return (d1._cal <= d2._cal);
        }

        public static PersianDate Parse(string s)
        {
            return (new PersianDate(s));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return this.DateTime.Ticks.ToString().GetHashCode();
        }
    }
    public static class DateExtensions
    {
        public static PersianDate ToPersian(this DateTime dtt)
        {
            return new PersianDate(dtt);
        }
        public static PersianDate ToPersian(this DateTime? dtt)
        {
            if (dtt == null)
                return null;
            return new PersianDate(dtt.Value);
        }
        public static string ToPersian(this DateTime dtt, string format)
        {
            PersianDate dt = new PersianDate(dtt);
            return dt.ToString(format);
        }
        public static string ToPersian(this DateTime? dtt, string format)
        {
            if (dtt == null)
                return null;
            PersianDate dt = new PersianDate(dtt.Value);
            return dt.ToString(format);
        }
    }
}
