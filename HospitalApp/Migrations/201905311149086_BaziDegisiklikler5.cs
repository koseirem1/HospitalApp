namespace HospitalApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaziDegisiklikler5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "HospitalId", c => c.Int());
            AddColumn("dbo.Appointments", "DepartmentId", c => c.Int());
            CreateIndex("dbo.Appointments", "HospitalId");
            CreateIndex("dbo.Appointments", "DepartmentId");
            AddForeignKey("dbo.Appointments", "DepartmentId", "dbo.Departments", "Id");
            AddForeignKey("dbo.Appointments", "HospitalId", "dbo.Hospitals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "HospitalId", "dbo.Hospitals");
            DropForeignKey("dbo.Appointments", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Appointments", new[] { "DepartmentId" });
            DropIndex("dbo.Appointments", new[] { "HospitalId" });
            DropColumn("dbo.Appointments", "DepartmentId");
            DropColumn("dbo.Appointments", "HospitalId");
        }
    }
}
