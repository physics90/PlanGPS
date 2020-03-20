namespace DataModelLayer
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class PlanGPSContext : DbContext
    {
        // Your context has been configured to use a 'PlanGPSContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DML.PlanGPSContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PlanGPSContext' 
        // connection string in the application configuration file.

        //public string contextName { get { return Environment.MachineName.ToString() == "" ? "name=PlanGPSContext": "name=PlanGPSContext-HP"; } }
        public PlanGPSContext()
            : base("name=PlanGPSContext")
        {
        }
        //: base("name=PlanGPSContext")
        public virtual DbSet<Analytics> Analytics { get; set; }
        public virtual DbSet<Neurosurgeon> Neurosurgeon { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Physicist> Physicist { get; set; }
        public virtual DbSet<Plan> Plan { get; set; }
        public virtual DbSet<PlanType> PlanType { get; set; }
        public virtual DbSet<RadOnc> RadOnc { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}