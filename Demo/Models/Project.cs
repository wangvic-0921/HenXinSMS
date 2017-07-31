using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HengXinSMS.Models
{
    public class Project
    {
          [Key]
          public int ID { get; set; }
          [Required]
          public string ProjectManager { get; set; }
          [Required]
          public string ProjectName { get; set; }
          [Required]
          public string ProjectNameId { get; set; }
          [Required]
          public string Members { get; set; }
          [Required]
          public DateTime InsertOfTime { get; set; }
          public Project()
          {
              InsertOfTime = System.DateTime.Now;
          }
      
    }
}