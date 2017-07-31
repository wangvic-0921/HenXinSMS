using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HengXinSMS.Models
{
    public class GetDate
    {
        string Year;
        string Month;
        string Day;
        string Hour;
        string Minute;
        string Second;
        public GetDate()
        {
            Year = System.DateTime.Now.Year.ToString();
            Month = System.DateTime.Now.Month.ToString();
            Day = System.DateTime.Now.Day.ToString();
            Hour = System.DateTime.Now.Hour.ToString();
            Minute = System.DateTime.Now.Minute.ToString();
            Second = System.DateTime.Now.Second.ToString();
        }
        public string Display()
        {
            string date = Year + "/"+Month+"/" + Day+"-"+Hour+":"+Minute+":"+Second ;
            return date;
        }
        public DateTime Display_first()
        {
            string NowYear = DateTime.Now.ToString("yyyy");
            string NowMouth = DateTime.Now.ToString("MM");
            string date = NowYear + "-" + NowMouth + "-01";
            return Convert.ToDateTime(date);
        }
        public DateTime Display_last()
        {
            string NowYear = DateTime.Now.ToString("yyyy");
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            string NowMouth = DateTime.Now.ToString("MM");
            string sunmdays = Convert.ToString(DateTime.DaysInMonth(year, month));
            string date = NowYear + "-" + NowMouth + "-" + sunmdays;
            return Convert.ToDateTime(date);
        }
        public int days(DateTime d)
        {
            int begin = DateTime.Now.Day;
            int end = d.Day;
            int add_days = end - begin;
            return add_days;
        }
    }
}