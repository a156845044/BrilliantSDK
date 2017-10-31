/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是日期操作工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Brilliant.Utility
{
    /// <summary>
    /// 日期工具类
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// 获取指定年月共有多少天
        /// </summary>
        /// <param name="year">年度</param>
        /// <param name="month">月份</param>
        /// <returns>该月天数</returns>
        public static int GetDaysOfMonth(int year, int month)
        {
            int mnext;
            int ynext;
            if (month < 12)
            {
                mnext = month + 1;
                ynext = year;
            }
            else
            {
                mnext = 1;
                ynext = year + 1;
            }
            DateTime dt1 = System.Convert.ToDateTime(year + "-" + month + "-1");
            DateTime dt2 = System.Convert.ToDateTime(ynext + "-" + mnext + "-1");
            TimeSpan diff = dt2 - dt1;
            return diff.Days;
        }

        /// <summary>
        /// 获取指定日期所在周的起止日期
        /// </summary>
        /// <param name="dateTime">指定日期 如：2013-11-11</param>
        /// <param name="dtWeekStart">返回参数：周开始日期</param>
        /// <param name="dtWeekeEnd">返回参数：周结束时间</param>
        public static void GetWeek(DateTime dateTime, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
        {
            int year = dateTime.Year;
            int weekCount = DateHelper.GetWeekOfYear(dateTime);
            DateTime dt = new DateTime(year, 1, 1);
            dt = dt + new TimeSpan((weekCount - 1) * 7, 0, 0, 0);
            dtWeekStart = dt.AddDays(-(int)dt.DayOfWeek + (int)DayOfWeek.Monday);
            dtWeekeEnd = dt.AddDays((int)DayOfWeek.Saturday - (int)dt.DayOfWeek + 1);
        }

        /// <summary>
        /// 获取指定星期中文名称
        /// </summary>
        /// <param name="dayOfWeek">指定星期 如：Monday</param>
        /// <returns>星期中文名称 如：星期一</returns>
        public static string GetWeekName(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Monday);
                case DayOfWeek.Tuesday: return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Tuesday);
                case DayOfWeek.Wednesday: return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Wednesday);
                case DayOfWeek.Thursday: return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Thursday);
                case DayOfWeek.Friday: return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Friday);
                case DayOfWeek.Saturday: return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Saturday);
                case DayOfWeek.Sunday: return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DayOfWeek.Sunday);
                default: return String.Empty;
            }
        }

        /// <summary>
        /// 返回指定日期是当前年的第几周
        /// </summary>
        /// <param name="dateTime">指定日期 如：2013-11-11</param>
        /// <returns>第几周 如:46</returns>
        public static int GetWeekOfYear(DateTime dateTime)
        {
            int firstdayofweek = Convert.ToInt32(Convert.ToDateTime(dateTime.Year.ToString() + "- " + "1-1 ").DayOfWeek);
            int days = dateTime.DayOfYear;
            int daysOutOneWeek = days - (7 - firstdayofweek);
            if (daysOutOneWeek <= 0)
            {
                return 1;
            }
            else
            {
                int weeks = daysOutOneWeek / 7;
                if (daysOutOneWeek % 7 != 0)
                    weeks++;
                return weeks + 1;
            }
        }

        /// <summary>
        /// 获取指定年份一共有多少周
        /// </summary>
        /// <param name="year">年 如：2013</param>
        /// <returns>一共有多少周</returns>
        public static int GetWeekCountOfYear(int year)
        {
            DateTime tempDate = DateTime.Parse(year.ToString() + "-01-01");
            int firstWeek = Convert.ToInt32(tempDate.DayOfWeek); //得到该年的第一天是周几 
            if (firstWeek == 1)
            {
                int countDay = tempDate.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 1;
                return countWeek;
            }
            else
            {
                int countDay = tempDate.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 2;
                return countWeek;
            }
        }

        /// <summary>
        /// 获取农历的年份
        /// </summary>
        /// <param name="date">待传入的公历日期</param>
        /// <returns>年份</returns>
        ///  <remarks>作者：dfq 时间：2014-09-11</remarks>
        public static int GetYearOfChina(DateTime date)
        {
            ChineseLunisolarCalendar china = new ChineseLunisolarCalendar();
            return china.GetYear(date);
        }

        /// <summary>
        /// 获取指定周数的开始日期和结束日期，开始日期为周日
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="weekOfYear">周数</param>
        /// <param name="first">out参数 一周的开始日期</param>
        /// <param name="last">out参数 一周的结束日期</param>
        ///  <remarks>作者：dfq 时间：2014-09-11</remarks>
        public static bool GetDaysOfWeeks(int year, int weekOfYear, out DateTime first, out DateTime last)
        {
            first = DateTime.MinValue;
            last = DateTime.MinValue;
            if (year < 1700 || year > 9999)
            {
                //"年份超限"
                return false;
            }
            if (weekOfYear < 1 || weekOfYear > 53)
            {
                //"周数错误"
                return false;
            }
            DateTime startDay = new DateTime(year, 1, 1);  //该年第一天
            DateTime endDay = new DateTime(year + 1, 1, 1).AddMilliseconds(-1);
            int dayOfWeek = 0;
            if (Convert.ToInt32(startDay.DayOfWeek.ToString("d")) > 0)
                dayOfWeek = Convert.ToInt32(startDay.DayOfWeek.ToString("d"));  //该年第一天为星期几
            if (dayOfWeek == 7) { dayOfWeek = 0; }
            if (weekOfYear == 1)
            {
                first = startDay;
                if (dayOfWeek == 6)
                {
                    last = first;
                }
                else
                {
                    last = startDay.AddDays((6 - dayOfWeek));
                }
            }
            else
            {
                first = startDay.AddDays((7 - dayOfWeek) + (weekOfYear - 2) * 7); //index周的起始日期
                last = first.AddDays(6);
                if (last > endDay)
                {
                    last = endDay;
                }
            }
            if (first > endDay)  //startDayOfWeeks不在该年范围内
            {
                //"输入周数大于本年最大周数";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取指定周数的开始日期和结束日期，开始日期为周一
        /// </summary>
        /// <param name="currentDate">当前传入的日期</param>
        /// <param name="firstDate">out参数 一周的开始日期</param>
        /// <param name="lastDate">out参数 一周的结束日期</param>
        /// <returns>true:表示执行正确 false：传入参数有误</returns>
        /// <remarks>作者:dfq 时间:2014-09-23</remarks>
        public static bool GetDaysOfWeeks(DateTime currentDate, out DateTime firstDate, out DateTime lastDate)
        {
            int year = currentDate.Year;//获取当前传入日期的年份
            int week = GetNewWeekOfYear(currentDate); //获取当前传入日期的周数
            firstDate = DateTime.MinValue;
            lastDate = DateTime.MinValue;
            if (year < 1700 || year > 9999)
            {
                //"年份超限"
                return false;
            }
            if (week < 1 || week > 53)
            {
                //"周数错误"
                return false;
            }
            DateTime startDay = new DateTime(year, 1, 1);  //该年第一天
            DateTime endDay = new DateTime(year + 1, 1, 1).AddMilliseconds(-1);
            int dayOfWeek = dayOfWeek = Convert.ToInt32(startDay.DayOfWeek.ToString("d"));  //该年第一天为星期几

            if (week == 1)//如果为一年中的第一周
            {
                firstDate = startDay;
                if (dayOfWeek == 0)//如果为周日
                {
                    lastDate = firstDate;//第一天为最后一天
                }
                else
                {
                    lastDate = startDay.AddDays((7 - dayOfWeek));
                }
            }
            if (week == 53)//如果为一年中的最后一周
            {
                firstDate = startDay.AddDays((8 - dayOfWeek) + (week - 2) * 7); //week周的起始日期
                lastDate = firstDate.AddDays(6);
            }
            else
            {
                firstDate = startDay.AddDays((8 - dayOfWeek) + (week - 2) * 7); //week周的起始日期
                lastDate = firstDate.AddDays(6);
                if (lastDate > endDay)
                {
                    lastDate = endDay;
                }
            }
            if (firstDate > endDay)  //startDayOfWeeks不在该年范围内
            {
                //"输入周数大于本年最大周数";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取传入日期为一年的中第几周
        /// </summary>
        /// <param name="currentDate">当前日期</param>
        /// <returns>一年的中第几周</returns>
        ///  <remarks>作者:dfq 时间:2014-09-23</remarks>
        public static int GetNewWeekOfYear(DateTime currentDate)
        {
            int year = currentDate.Year;//获取传入日期的年份
            int days = currentDate.DayOfYear;//获取当前日期位于当年的第几天
            DateTime startDay = new DateTime(year, 1, 1);  //该年第一天
            int firstdayofweek = Convert.ToInt32(startDay.DayOfWeek.ToString("d"));  //该年第一天为星期几

            int curDayofWeek = Convert.ToInt32(currentDate.DayOfWeek.ToString("d"));  //获取当前日期为星期几


            //获取一年中第一周的天数
            int firstWeek_Days = 0;
            if (firstdayofweek != 1)//如果一年中的第一天不为星期一
            {
                firstWeek_Days = 7 - firstdayofweek;
            }

            days = days - firstWeek_Days;//去除第一周的天数

            if (days <= 0)//如果小于等于0，则表示为当年的第一周
            {
                return 1;
            }
            else
            {
                int weeks = days / 7;
                if (days % 7 != 0)
                    weeks++;
                if (curDayofWeek == 0)
                    weeks--;

                return weeks + 1;

            }

        }

        /// <summary>
        /// 根据传入的年份和周次获取该周的第一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="week">周次</param>
        /// <returns>该周的第一天</returns>
        /// <remarks>作者:dfq 时间:2014-09-23</remarks>
        public static DateTime GetFirstDayOfWeek(int year, int week)
        {
            DateTime firstDate = new DateTime();

            firstDate = DateTime.MinValue;
            if (year < 1700 || year > 9999)
            {
                //"年份超限"
                return Convert.ToDateTime("1990-01-01");

            }
            if (week < 1 || week > 53)
            {
                //"周数错误"
                return Convert.ToDateTime("1990-01-01");
            }
            DateTime startDay = new DateTime(year, 1, 1);  //该年第一天
            DateTime endDay = new DateTime(year + 1, 1, 1).AddMilliseconds(-1);
            int dayOfWeek = dayOfWeek = Convert.ToInt32(startDay.DayOfWeek.ToString("d"));  //该年第一天为星期几

            if (week == 1)//如果为一年中的第一周
            {
                firstDate = startDay;
            }
            if (week == 53)//如果为一年中的最后一周
            {
                firstDate = startDay.AddDays((8 - dayOfWeek) + (week - 2) * 7); //week周的起始日期
            }
            else
            {
                firstDate = startDay.AddDays((8 - dayOfWeek) + (week - 2) * 7); //week周的起始日期
            }
            if (firstDate > endDay)  //startDayOfWeeks不在该年范围内
            {
                //"输入周数大于本年最大周数";
                return Convert.ToDateTime("1990-01-01");
            }
            return firstDate;
        }

        /// <summary>
        /// 获取指定周数的开始日期和结束日期，开始日期为周一
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="week">周数</param>
        /// <param name="firstDate">out参数 一周的开始日期</param>
        /// <param name="lastDate">out参数 一周的结束日期</param>
        ///  <remarks>作者：dfq 时间：2014-09-25</remarks>
        public static bool GetDaysOfWeek(int year, int week, out DateTime firstDate, out DateTime lastDate)
        {
            firstDate = DateTime.MinValue;
            lastDate = DateTime.MinValue;
            if (year < 1700 || year > 9999)
            {
                //"年份超限"
                return false;
            }
            if (week < 1 || week > 53)
            {
                //"周数错误"
                return false;
            }
            DateTime startDay = new DateTime(year, 1, 1);  //该年第一天
            DateTime endDay = new DateTime(year + 1, 1, 1).AddMilliseconds(-1);
            int dayOfWeek = dayOfWeek = Convert.ToInt32(startDay.DayOfWeek.ToString("d"));  //该年第一天为星期几

            if (week == 1)//如果为一年中的第一周
            {
                firstDate = startDay;
                if (dayOfWeek == 0)//如果为周日
                {
                    lastDate = firstDate;//第一天为最后一天
                }
                else
                {
                    lastDate = startDay.AddDays((7 - dayOfWeek));
                }
            }
            if (week == 53)//如果为一年中的最后一周
            {
                firstDate = startDay.AddDays((8 - dayOfWeek) + (week - 2) * 7); //week周的起始日期
                lastDate = firstDate.AddDays(6);
            }
            else
            {
                firstDate = startDay.AddDays((8 - dayOfWeek) + (week - 2) * 7); //week周的起始日期
                lastDate = firstDate.AddDays(6);
                if (lastDate > endDay)
                {
                    lastDate = endDay;
                }
            }
            if (firstDate > endDay)  //startDayOfWeeks不在该年范围内
            {
                //"输入周数大于本年最大周数";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="brithDay">生日</param>
        /// <param name="now">当前日期</param>
        /// <returns>年龄</returns>
        /// <remarks>作者：dfq 时间：2015-01-16</remarks>
        public static int CalculateAge(DateTime brithDay, DateTime now)
        {
            int age = now.Year - brithDay.Year;
            if (now.Month < brithDay.Month || now.Month == brithDay.Month && now.Day < brithDay.Day)
            {
                age--;
            }
            return age;
        }

        /// <summary>
        /// 计算当前传入日期所在周及年（ISO 8601算法）
        /// Calculates ISO week and year number (ISO year can differ from calendar year)
        /// </summary>
        /// <param name="date">date 待计算的日期</param>
        /// <param name="year">ISO year 待获取的年份</param>
        /// <param name="week">ISO week 待获取的周</param>
        /// http://codebetter.com/petervanooijen/2005/09/26/iso-weeknumbers-of-a-date-a-c-implementation/
        /// <remarks>作者：dfq 时间：2016-12-19</remarks>
        public static void GetWeekNumber(DateTime date, out int year, out int week)
        {
            year = date.Year;
            // Get jan 1st of the year  获取元旦日期
            DateTime startOfYear = new DateTime(year, 1, 1);
            // Get dec 31st of the year 获取年末日期
            DateTime endOfYear = new DateTime(year, 12, 31);
            // ISO 8601 weeks start with Monday  ISO 8601周 从周一开始
            // The first week of a year includes the first Thursday  一年的第一个周四所在的周为第一周
            // DayOfWeek returns 0 for sunday up to 6 for saterday  DayOfWeek返回0为周日 其他数字一一对应为周一到周六
            int[] iso8601Correction = { 6, 7, 8, 9, 10, 4, 5 };
            int nds = date.Subtract(startOfYear).Days + iso8601Correction[(int)startOfYear.DayOfWeek];
            week = nds / 7;
            switch (week)
            {
                case 0:
                    // Return weeknumber of dec 31st of the previous year 
                    // 返回前一年的12月31日所在周
                    GetWeekNumber(startOfYear.AddDays(-1), out year, out week);
                    break;
                case 53:
                    // If dec 31st falls before thursday it is week 01 of next year 
                    // 如果12月31日所在星期小于星期四，则为下一年的第一周
                    if (endOfYear.DayOfWeek < DayOfWeek.Thursday)
                    {
                        week = 1; year += 1;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取指定周的开始日期和结束日期
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="weekOfYear">周次</param>
        /// <param name="fristDate">out 开始日期</param>
        /// <param name="endDate">out 结束日期</param>
        /// <remarks>作者:dfq 时间：2016-12-19</remarks>
        public static void GetDateRangeOfWeek(int year, int weekOfYear, out DateTime fristDate, out DateTime endDate)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)DayOfWeek.Monday - (int)jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            //get first week by ISO
            int firstWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.InvariantCulture.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday);

            // Get jan 1st of the year  获取元旦日期
            DateTime startOfYear = new DateTime(year, 1, 1);
            int years, week = 0;
            GetWeekNumber(startOfYear, out years, out week);
            if (week != 53)
            {
                if (firstWeek <= 1)
                {
                    weekOfYear -= 1;
                }
            }
            fristDate = firstMonday.AddDays(weekOfYear * 7);
            endDate = fristDate;
            GetDaysOfWeeks(fristDate, out fristDate, out endDate);
        }

        /// <summary>
        /// 获取当前日期所在季度起始时间
        /// </summary>
        /// <param name="currentDate">当前日期</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <remarks>作者：dfq 时间：2016-12-19</remarks>
        public static void GetDateRangeOfQuarter(DateTime currentDate, out DateTime startDate, out DateTime endDate)
        {
            int currentMonth = currentDate.Month;//获取当前月
            int currentYear = currentDate.Year;//获取当前年
            DateTime first = DateTime.Now;
            DateTime last = first;


            //判断是否为第一季度（1-3月）
            if (currentMonth >= 1 && currentMonth <= 3)
            {
                first = Convert.ToDateTime(string.Format("{0}-01-01", currentYear));
                last = Convert.ToDateTime(string.Format("{0}-03-31", currentYear));
            }

            //判断是否为第二季度（4-6月）
            if (currentMonth >= 4 && currentMonth <= 6)
            {
                first = Convert.ToDateTime(string.Format("{0}-04-01", currentYear));
                last = Convert.ToDateTime(string.Format("{0}-06-30", currentYear));
            }

            //判断是否为第三季度（7-9月）
            if (currentMonth >= 7 && currentMonth <= 9)
            {
                first = Convert.ToDateTime(string.Format("{0}-07-01", currentYear));
                last = Convert.ToDateTime(string.Format("{0}-09-30", currentYear));
            }

            //判断是否为第四季度（10-12月）
            if (currentMonth >= 10 && currentMonth <= 12)
            {
                first = Convert.ToDateTime(string.Format("{0}-10-01", currentYear));
                last = Convert.ToDateTime(string.Format("{0}-12-31", currentYear));
            }
            startDate = first;
            endDate = last;
        }

        /// <summary>
        /// 获取当前日期所在月的起始时间
        /// </summary>
        /// <param name="currentDate">当前日期</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        ///  <remarks>作者：dfq 时间：2016-12-19</remarks>
        public static void GetDateRangeOfMonth(DateTime currentDate, out DateTime startDate, out DateTime endDate)
        {
            int currentMonth = currentDate.Month;//获取当前月
            int currentYear = currentDate.Year;//获取当前年
            DateTime first = DateTime.Now;
            DateTime last = first;

            startDate = DateTime.Parse(currentDate.ToString("yyyy-MM-01"));
            endDate = startDate.AddMonths(1).AddDays(-1);//本月最后一天;
        }

        /// <summary>
        /// 获取当前日期所在年的起始时间
        /// </summary>
        /// <param name="currentDate">当前日期</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        ///  <remarks>作者：dfq 时间：2016-12-19</remarks>
        public static void GetDateRangeOfYear(DateTime currentDate, out DateTime startDate, out DateTime endDate)
        {
            int currentYear = currentDate.Year;//获取当前年
            startDate = Convert.ToDateTime(string.Format("{0}-01-01", currentYear));
            endDate = Convert.ToDateTime(string.Format("{0}-12-31", currentYear));
        }
    }
}
