using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTest.WinformTest
{
    public class InterestCalc
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
    }
    public class DateCalc
    {
        public string TxtStartDate { get; set; }
        public string TxtEndDate { get; set; }
        public string TxtTotalDays { get; set; }
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
            var overrideDate = GetAllDate();
            DateTime endDate = startDate;
            int i = 0;
            while (i != totalDays)
            {
                endDate = endDate.AddDays(1);
                var isEnable = !(endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday);
                if (overrideDate.ContainsKey(startDate))
                {
                    isEnable = overrideDate[endDate];
                }

                if (isEnable)
                    i++;
            }
            return endDate;
        }
        public DateTime GetStartDate(DateTime endDate, int totalDays)
        {
            var overrideDate = GetAllDate();
            DateTime startDate = endDate.AddDays(-1);
            int i = 0;
            while (i != totalDays)
            {
                startDate = startDate.AddDays(-1);
                var isEnable = !(startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday);
                if (overrideDate.ContainsKey(startDate))
                {
                    isEnable = overrideDate[startDate];
                }
                if (isEnable)
                    i++;
            }
            return startDate;
        }
        public int GetTotalDays(DateTime startDate, DateTime endDate)
        {
            var overrideDate = GetAllDate(); 
            int i = 0;
            var isEnable = true;
            while (startDate < endDate)
            {
                startDate = startDate.AddDays(1);
                isEnable = !(startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday);
                if (overrideDate.ContainsKey(startDate))
                {
                    isEnable = overrideDate[startDate];
                }
                if (isEnable)
                    i++;
            }
            return i;
        }

    }
}
