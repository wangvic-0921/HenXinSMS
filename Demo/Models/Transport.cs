using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HengXinSMS.Models
{
    public class Transport
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Depart { get; set; }
        [Required]
        public string ProjectManager { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectNameId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ApplicationDate { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string ContractId { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string ValueofGoods { get; set; }
        [Required]
        public string Insurance { get; set; }
        [Required]
        public double   InsuranceofAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EstimateDate_Use { get; set; }
        [DisplayFormat(DataFormatString = "{0: HH:mm }")]
        public DateTime EstimateTime_Start { get; set; }
        [DisplayFormat(DataFormatString = "{0: HH:mm }")]
        public DateTime EstimateTime_End { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EstimateDate_CompleteTime { get; set; }
        [Required]
        public string TransportRequest { get; set; }
        public string NumofTruck { get; set; }
        public string TypeofTransport { get; set; }
        public string Remark{ get; set; }
        public string ApplyId { get; set; }
        public string Bl { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EmergencePeriod { get; set; }
        public string QuantityOfGoods { get; set; }
        public string Weight{ get; set; }
        public string Consignee { get; set; }
        public string Cargo { get; set; }
        public string Address{ get; set; }
        public string Phone{ get; set; }
        public string Fax { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateRecorded { get; set; }
        public double  AirFreight { get; set; }
        public double OceanFreight { get; set; }
        public double InlandTransportationCharge { get; set; }
        public double AdministrativeCharge { get; set; }
        public double CustomsTransferFee { get; set; }
        public double DrawsheetCost { get; set; }
        public double ServiceCharge { get; set; }
        public double SupervisionFeeForCar { get; set; }
        public double StorageChargeInSHANGHAI { get; set; }
        public double CustomsCharges { get; set; }
        public double InspectionCharges { get; set; }
        public double TriInspectionCharges { get; set; }
        public double Chargeback { get; set; }
        public double StorageChargeInWUXI { get; set; }
        public double CustomsAgentFee { get; set; }
        public double InspectionFee { get; set; }
        public double InspectionServiceCharge { get; set; }
        public double BookingFee { get; set; }
        public double THC { get; set; }
        public double DocumentCharge { get; set; }
        public double TelexReleaseSurchage { get; set; }
        public double NetworkSupervisionFee { get; set; }
        public double BlFee { get; set; }
        public double BayonetRegisterFee  { get; set; }
        public double PackingFee { get; set; }
        public double Tariff { get; set; }
        public double AddValueTax { get; set; }
        public double LinkageTax { get; set; }
        public double DemurrageCharge { get; set; }
        public double OverdueFine { get; set; }
        public double CarPeledgeFee { get; set; }
        public double Fine { get; set; }
        public double DRC { get; set; }
        public double CCC { get; set; }
        public double CO { get; set; }
        public double AccountingCheckFee { get; set; }
        public double Other { get; set; }
        public double AllTransportation { get; set; }
        public double HangdingFee { get; set; }
        public double SpecialCharge { get; set; }
        public double Poundage { get; set; }
        public double Total { get; set; }
        public Transport()
        {
            ApplicationDate = System.DateTime.Now;
            EmergencePeriod = System.DateTime.Now;
            DateRecorded = System.DateTime.Now;
            PM_Review  = "未审核";
            OML_Review = "未审核";
            Flag = "运输";
            Status = "Active";
            Schedule = "Inactive";
            AllTransportation = AirFreight + OceanFreight + InlandTransportationCharge + AdministrativeCharge + InsuranceofAmount;
            HangdingFee=CustomsTransferFee+DrawsheetCost+ServiceCharge+SupervisionFeeForCar+StorageChargeInSHANGHAI+CustomsCharges+InspectionCharges+TriInspectionCharges+Chargeback+StorageChargeInWUXI+CustomsAgentFee+InspectionFee+InspectionServiceCharge+BookingFee+THC+DocumentCharge+TelexReleaseSurchage+NetworkSupervisionFee+BlFee+BayonetRegisterFee+PackingFee+Tariff+AddValueTax+LinkageTax+Other;
            SpecialCharge = OverdueFine + Fine + DemurrageCharge + CarPeledgeFee;
            Poundage=DRC+CCC+CO+AccountingCheckFee;
            Total = AllTransportation + HangdingFee + SpecialCharge + Poundage;
        }
       
    }
}