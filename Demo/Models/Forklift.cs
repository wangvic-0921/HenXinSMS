using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HengXinSMS.Models
{
    public class Forklift
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Depart { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectNameId { get; set; }
        [Required]
        public string ProjectManager { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]  
        public DateTime ApplicationDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]  
        public DateTime EstimateDate_Use { get; set; }
        [Required]
        public string Location { get; set; }
        [DisplayFormat(DataFormatString = "{0: HH:mm }")]  
        public DateTime EstimateTime_Start { get; set; }
        [DisplayFormat(DataFormatString = "{0: HH:mm }")]  
        public DateTime EstimateTime_End { get; set; }
        [Required]
        public string ServeRequest { get; set; }
        public string NumofForklift { get; set; }
        public string NumofHuman { get; set; }
        [Required]
        public string Purpose { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public string PM_Review { get; set; }
        [Required]
        public string OML_Review { get; set; }
        [Required]
        public string Flag { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Schedule { get; set; }
        public string ApplyId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EmergencePeriod { get; set; }//发生日期
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateRecorded { get; set; }//入账日期日期
        public double   CostofLabor { get; set; }
        public double   CostofForklist { get; set; }
        public double   Other { get; set; }
        public string Remark { get; set; }
        public string Annex { get; set; } 
        public Forklift()
        {
            ApplicationDate = System.DateTime.Now;
            EmergencePeriod = System.DateTime.Now;
            DateRecorded = System.DateTime.Now;
            PM_Review  = "未审核";
            OML_Review = "未审核";
            Flag = "叉车";
            Status = "Active";
            Schedule = "Inactive";
        }
    }
}