using Microsoft.EntityFrameworkCore;


namespace PlacementApplicationNew.Model
{
    public class PlacementAppContext : DbContext
    {
        public PlacementAppContext() { }
        public PlacementAppContext(DbContextOptions<PlacementAppContext> options) : base(options)
        { }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Apply>? Applys { get; set; }
        public DbSet<Role>? Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=PlacementApp;Trusted_Connection=True;");
            }
        }
         
    }
    }




