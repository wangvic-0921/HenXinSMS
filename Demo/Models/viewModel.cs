using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace HengXinSMS.Models
{
    public class viewModel
    {


        public DbSet<Forklift> forklifts { get; set; }
        public DbSet<Packaging> packagings { get; set; }
        public DbSet<Transport> transports { get; set; }
        public viewModel(DbSet<Forklift> _forklifts, DbSet<Packaging> _packagings, DbSet<Transport> _transports)
        {
            this.forklifts = _forklifts;
            this.packagings = _packagings;
            this.transports = _transports;
        }
        
    }
}