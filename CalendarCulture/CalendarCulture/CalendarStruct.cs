using CalendarCulture.Resources;
using System;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace CalendarCulture
{/// <summary>
/// کلاس ساختار و منطق تقویم
/// </summary>
    public class CalendarStruct
    {
        private static FrmCalendar _frmCalendar;
        private static DateTime _date;
        public CalendarStruct(FrmCalendar frm, string culture)
        {
            _frmCalendar = frm;
            _date = DateTime.Now;
            CultureInfo info = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
            Detechment();
        }
        /// <summary>
        /// متد تعیین کننده کالچر
        /// </summary>
        private static void Detechment()
        {
            if (CultureInfo.CurrentCulture.Name == "fa-IR")
            {
                Draw(true);
                CreatePersianCalendar();
            }
            else
            {
                Draw(false);
                CreateGregorianCalendar();
            }
        }
        /// <summary>
        /// زمان جاری تقویم را تحویل میدهد
        /// </summary>
        public DateTime ReturnDate()
        {
            return _date;
        }
        /// <summary>
        /// اضافه یا کم کردن ماه 
        /// </summary>
        /// <param name="month">مقدار منفی یا مثبت</param>
        public void AddMonth(sbyte month)
        {
            PersianCalendar pc = new PersianCalendar();
            if (CultureInfo.CurrentCulture.Name == "fa-IR")
            { 
                if (month>0)
                _date = _date.AddDays((pc.GetDaysInMonth(pc.GetYear(_date), pc.GetMonth(_date))));
                else
                    _date = _date.AddDays((pc.GetDaysInMonth(pc.GetYear(_date.AddMonths(month)), pc.GetMonth(_date.AddMonths(month)))) * -1);
            }
            else
            _date = _date.AddMonths(month);

            Detechment();

        }
        /// <summary>
        /// اضافه یا کم کردن سال
        /// </summary>
        /// <param name="year">مقدار منفی یا مثبت</param>
        public void AddYear(sbyte year)
        {
            PersianCalendar pc = new PersianCalendar();
            if (CultureInfo.CurrentCulture.Name == "fa-IR")
            {
                if (year > 0)
                    _date = _date.AddDays((pc.GetDaysInYear(pc.GetYear(_date))));
                else
                    _date = _date.AddDays((pc.GetDaysInYear(pc.GetYear(_date.AddYears(year)))) *-1);
            }
            else
                _date = _date.AddYears(year);

            Detechment();

        }
        /// <summary>
        /// ایجاد تقویم شمسی
        /// </summary>
        private static void CreatePersianCalendar()
        {
            PersianCalendar pc = new PersianCalendar();
         
            var today = pc.GetDayOfMonth(_date);
            var firstDayOfMonth = (byte)(pc.GetDayOfWeek(_date.AddDays(-today + 1)));
            var lblIndex = firstDayOfMonth == 6 ? 1 : firstDayOfMonth + 2;
            var daysOfMonth = (byte)(pc.GetDaysInMonth(pc.GetYear(_date), pc.GetMonth(_date)));
            var lastdayofmonth = (byte)pc.GetDayOfWeek(_date.AddDays(daysOfMonth - today));
            for (int i = 1; i < daysOfMonth + 1; i++)
            {
                var label = _frmCalendar.tableLayoutPanel1.Controls.Find($"label{lblIndex + i - 1}", false);
                label[0].Text = $"{i}";
                if (today == i)
                    label[0].BackColor = Color.GreenYellow;


            }

        }
        /// <summary>
        /// ایجاد تقویم میلادی
        /// </summary>
        private static void CreateGregorianCalendar()
        {
           
            var today = _date.Day;
            var firstDayOfMonth = (byte)(_date.AddDays(-today + 1).DayOfWeek);
            var lblIndex = firstDayOfMonth == 6 ? 1 : firstDayOfMonth + 2;
            var daysOfMonth = (byte)(DateTime.DaysInMonth(_date.Year, _date.Month));
            var lastdayofmonth = (byte)(_date.AddDays(daysOfMonth - today).DayOfWeek);
            for (int i = 1; i < daysOfMonth + 1; i++)
            {
                var label = _frmCalendar.tableLayoutPanel1.Controls.Find($"label{lblIndex + i - 1}", false);
                label[0].Text = $"{i}";
                if (i == today)
                    label[0].BackColor = Color.GreenYellow;


            }
        }
        /// <summary>
        /// نمایش کلیات تقویم
        /// </summary>  
        private static void Draw(bool isPersian)
        {
            foreach (var c in _frmCalendar.tableLayoutPanel1.Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    ((Label)c).Text = string.Empty;
                    ((Label)c).BackColor = Color.FromArgb(224, 224, 224);
                }
            }
            _frmCalendar.LblSaterday.Text = Resource.sat;
            _frmCalendar.LblSunday.Text = Resource.sun;
            _frmCalendar.LblMonday.Text = Resource.mon;
            _frmCalendar.LblTuesday.Text = Resource.tue;
            _frmCalendar.LblWednesday.Text = Resource.wed;
            _frmCalendar.LblThursday.Text = Resource.thu;
            _frmCalendar.LblFriday.Text = Resource.fri;
            _frmCalendar.RbEnglish.Text = Resource.rbenglishtext;
            _frmCalendar.RbPersian.Text = Resource.rbpersiantext;
            
            int month;
            int year;
            if (isPersian)
            {
                PersianCalendar pc = new PersianCalendar();
                month = pc.GetMonth(_date);
                year = pc.GetYear(_date);
                
            }
            else
            {
                month = _date.Month;
                year = _date.Year;
            }

            switch (month)
            {
                case 1:
                    _frmCalendar.LblMonth.Text = year+"   "+Resource.month1;
                    break;
                case 2:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month2;
                    break;
                case 3:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month3;
                    break;
                case 4:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month4;
                    break;
                case 5:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month5;
                    break;
                case 6:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month6;
                    break;
                case 7:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month7;
                    break;
                case 8:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month8;
                    break;
                case 9:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month9;
                    break;
                case 10:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month10;
                    break;
                case 11:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month11;
                    break;
                case 12:
                    _frmCalendar.LblMonth.Text = year + "   " + Resource.month12;
                    break;
                default:
                   
                    break;
            }
        }

        /// <summary>
        /// تابع بازگشتی برای مقدار دهی به تقویم
        /// بدون استفاده در کلاس 
        /// صرفا جهت تست
        /// </summary>
        private static bool RecursiveForeach (FrmCalendar frm, byte lblindex, byte day,byte daysofmonth)
        {
            foreach (var c in frm.tableLayoutPanel1.Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    if (((Label)c).Name.Contains("label"))
                    {
                        if (((Label)c).Name == $"label{lblindex}")
                        {
                            ((Label)c).Text = $"{day}";
                            day++;
                            lblindex++;
                            if (day == daysofmonth+1)
                            {
                                return true;
                            }
                            RecursiveForeach(frm, lblindex, day, daysofmonth);
                        }

                    }

                }
            }
            return true;
        }  
    }
}
