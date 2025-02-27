using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.workingTime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data
{
    public class EfContext:DbContext
    {
        //se añade los sets
        public DbSet<Employee> employee {  get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Employer> employers { get; set; }

        public DbSet<FarmSupervisor> farmSupervisors { get; set; }
        public DbSet<Farm> farms { get; set; }
        public DbSet<History> histories { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<PaymentAdvance> paymentAdvances { get; set; }
        public DbSet<WorkingTimeInGroup> workingTimeGroups { get; set; }
        public DbSet<WorkingTime> workingTimes { get; set; }
        public DbSet<WorkingTimeForHour> workingTimeForHours { get; set; }
        public DbSet<WorkingTimeForKilogram> workingTimeForKilograms { get; set; }
        public DbSet<WorkingGroup> workingGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DPH
            /*modelBuilder.Entity<WorkingTime>()
                .HasDiscriminator<string>("WorkingTimeType")
                .HasValue<WorkingTimeForHour>("hour")
                .HasValue<WorkingTimeForKilogram>("kilogram");*/
            //TPT         

            //TPC
            modelBuilder.Entity<WorkingTime>().ToTable("workingTimes");

            modelBuilder.Entity<WorkingTimeForHour>().ToTable("workingTimeForHours").HasBaseType<WorkingTime>();
            modelBuilder.Entity<WorkingTimeForKilogram>().ToTable("workingTimeForKilograms").HasBaseType<WorkingTime>();
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySQL(connectionString: "server=127.0.0.1;port=3307; database=DBCampoDigital; user=root;password=root");
        //UseMySQL(connectionString: "server=database-1-campodigital.cdeyma4u6nl6.us-east-2.rds.amazonaws.com;port=3306; database=campodigital; user=admin;password=contra");

        //optionsBuilder.UseMySQL(connectionString: "server=127.0.0.1;port=3307; database=DBCampoDigital; user=root;password=root");



        //"server=127.0.0.1;port=3307; database=DBCampoDigital; user=root;password=root"
    }
}
