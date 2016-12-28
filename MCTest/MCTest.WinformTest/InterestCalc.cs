using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest.WinformTest
{
    public class InterestCalc : INotifyPropertyChanged
    {
        public InterestCalc()
        {
            TxtInterestDay = "365";
            TxtAmount = "1";
        }
        public string TxtAmount { get; set; }
        public string TxtDueDate { get; set; }
        public string TxtRate { get; set; }
        public string TxtInterest { get; set; }
        public string TxtInterestDay { get; set; }
        public double? Amount
        {
            get
            {
                double va = 0;
                if (double.TryParse(TxtAmount, out va))
                    return va*100000000;
                return null;
            }
        } 
        public double? DueDate
        {
            get
            {
                double va = 0;
                if (double.TryParse(TxtDueDate, out va))
                    return va ;
                return null;
            }
        } 
        public double? Rate
        {
            get
            {
                double va = 0;
                if (double.TryParse(TxtRate, out va))
                    return va* 0.01;
                return null;
            }
        } 
        public double? Interest
        {
            get
            {
                double va = 0;
                if (double.TryParse(TxtInterest, out va))
                    return va;
                return null;
            }
        }
        public double? InterestDay
        {
            get
            {
                double va = 0;
                if (double.TryParse(TxtInterestDay, out va))
                    return va;
                return null;
            }
        }

        public void Calc()
        {
            //(（金额*利率) / 计息) *期限 = 利息 
            if (Interest == null && Amount != null && Rate != null && InterestDay != null && DueDate != null)
            {
                TxtInterest = String.Format("{0:F}", ((Amount * Rate) / InterestDay) * DueDate);
            }
            if (Interest != null && Amount != null && Rate != null && InterestDay != null && DueDate == null)
            {
                TxtDueDate = Convert.ToString(Math.Round((Convert.ToDouble(Interest / (Amount * Rate / InterestDay))), 2));
            }
            if (Interest != null && Amount == null && Rate != null && InterestDay != null && DueDate != null)
            {
                TxtAmount = String.Format("{0:F}", ((Interest / DueDate) * InterestDay) / Rate / 100000000);
            }
            if (Interest != null && Amount != null && Rate == null && InterestDay != null && DueDate != null)
            {
                TxtRate = String.Format("{0:F}", (((Interest / DueDate) * InterestDay) / Amount * 100));
            }
            if (Interest != null && Amount != null && Rate != null && InterestDay == null && DueDate != null)
            {
                TxtInterestDay = Convert.ToString(Math.Round(Convert.ToDouble((Amount * Rate) / (Interest / DueDate)), 2));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    public class DateCalc
    {
        public string TxtStartDate { get; set; }
        public string TxtEndDate { get; set; }
        public string TxtTotalDays { get; set; }
        public string TxtResult { get; set; }
        public DateTime? StartDate {
            get
            {
                DateTime va;
                if (DateTime.TryParse(TxtStartDate, out va))
                    return va;
                return null;
            }
        }
        public DateTime? EndDate {
            get
            {
                DateTime va;
                if (DateTime.TryParse(TxtEndDate, out va))
                    return va;
                return null;
            }
        }
        public int? TotalDays {
            get
            {
                int va;
                if (int.TryParse(TxtTotalDays, out va))
                    return va;
                return null;
            }
        }
        public void Calc()
        {
            if (StartDate != null & EndDate != null && TotalDays == null)
            {
                TxtTotalDays = GetTotalDays(StartDate.Value, EndDate.Value).ToString();
            }
            if (StartDate != null & EndDate == null && TotalDays != null)
            {
                TxtEndDate = GetEndDate(StartDate.Value, TotalDays.Value).ToString("yyyy/MM/dd");
            }
            if (StartDate == null & EndDate != null && TotalDays != null)
            {
                TxtStartDate = GetStartDate(EndDate.Value, TotalDays.Value).ToString("yyyy/MM/dd");
            }
        }

        public Dictionary<DateTime, bool> GetAllDate()
        {
            var dateDict = new Dictionary<DateTime, bool>();
            var dateFile = AppDomain.CurrentDomain.BaseDirectory + "date.txt";
            var line = "";
            if(File.Exists(dateFile))
            using (StreamReader reader = File.OpenText(dateFile))
            {
                while ((line = reader.ReadLine()) !=  null)
                {
                    if (line.StartsWith("#"))
                    {
                        continue;
                    }
                    var dtxt = line.Substring(0, line.IndexOf(" "));
                    DateTime dt;
                    if (DateTime.TryParse(dtxt, out dt))
                    {
                        var dbool = line.Substring(line.IndexOf(" ")).Trim();
                        if(dbool == "0" || dbool == "1")
                        {
                                var isbool = dbool == "1";
                            if (dateDict.ContainsKey(dt))
                            {
                                dateDict[dt] = isbool;
                            }
                            else
                            {
                                dateDict.Add(dt, isbool);
                            }
                        }
                    }
                }
            }
            return dateDict;
        }

        public DateTime GetEndDate(DateTime startDate, int totalDays)
        {
            StringBuilder sb = new StringBuilder();
            var overrideDate = GetAllDate();
            DateTime endDate = startDate;
            int i = 0;
            var isEnable = true;
            while (i < totalDays || !isEnable)
            {
                endDate = endDate.AddDays(1);
                isEnable = !(endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday);
                var dateChanged = string.Empty;
                if (overrideDate.ContainsKey(endDate))
                {
                    isEnable = overrideDate[endDate];
                    dateChanged = "(节假日调整)";
                }
                i++;
                sb.AppendLine(i + " " + endDate.ToString("yyyy-MM-dd") + " " + endDate.DayOfWeek.ToChinese() + dateChanged);
                if (i == totalDays)
                {
                    sb.AppendLine("--------");
                }
            }
            TxtResult = sb.ToString();
            return endDate;
        }
        public DateTime GetStartDate(DateTime endDate, int totalDays)
        {
            StringBuilder sb = new StringBuilder();
            var overrideDate = GetAllDate();
            DateTime startDate = endDate;
            int i = 0;
            var isEnable = true;
            while (i < totalDays || !isEnable)
            {
                startDate = startDate.AddDays(-1);
                isEnable = !(startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday);
                var dateChanged = string.Empty;
                if (overrideDate.ContainsKey(startDate))
                {
                    isEnable = overrideDate[startDate];
                    dateChanged = "(节假日调整)";
                }
                i++;
                sb.AppendLine(i + " " + startDate.ToString("yyyy-MM-dd") + " " + startDate.DayOfWeek.ToChinese() + dateChanged);

                if (i == TotalDays)
                {
                    sb.AppendLine("--------");
                }
            }
            TxtResult = sb.ToString();
            return startDate;
        }
        public int GetTotalDays(DateTime startDate, DateTime endDate)
        {
            StringBuilder sb = new StringBuilder();
            var overrideDate = GetAllDate(); 
            int i = 0;
            var isEnable = true;
            while (startDate < endDate || !isEnable)
            {
                startDate = startDate.AddDays(1);
                isEnable = !(startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday);
                var dateChanged = string.Empty;
                if (overrideDate.ContainsKey(startDate))
                {
                    isEnable = overrideDate[startDate];
                    dateChanged = "(节假日调整)";
                }
                i++;
                sb.AppendLine(i + " " + startDate.ToString("yyyy-MM-dd") + " " + startDate.DayOfWeek.ToChinese()+dateChanged);

                if (endDate == startDate)
                {
                    sb.AppendLine("--------");
                }
            }
            TxtResult = sb.ToString();
            return i;
        }

    }

    public static class DateExtension
    {
        public static string ToChinese(this DayOfWeek week)
        {
            switch (week)
            {
                  case DayOfWeek.Monday:
                    return "周一";
                  case DayOfWeek.Tuesday:
                    return "周二";
                  case DayOfWeek.Wednesday:
                    return "周三";
                case DayOfWeek.Thursday:
                    return "周四";
                case DayOfWeek.Friday:
                    return "周五";
                case DayOfWeek.Saturday:
                    return "周六";
                case DayOfWeek.Sunday:
                    return "周日";
            }
            return null;
        }
    }
}
