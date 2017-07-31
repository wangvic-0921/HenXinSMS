using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace HengXinSMS.Models
{
    public class HenXinSMSContext:DbContext
    {
        public HenXinSMSContext()
            : base("name=HenXinSMSContext")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Forklift> Forklifts { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}