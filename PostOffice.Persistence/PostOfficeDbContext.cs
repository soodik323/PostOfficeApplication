using Microsoft.EntityFrameworkCore;
using PostOffice.Application.Contracts.Persistence;
using PostOffice.Domain.Entities;

namespace PostOffice.Persistence
{
    public class PostOfficeDbContext : DbContext, IDataContext
    {
        public PostOfficeDbContext(DbContextOptions<PostOfficeDbContext> options) : base(options)
        {

        }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Bag> Bags { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //Configure primary key
            builder.Entity<Shipment>().HasKey(pc => new { pc.Id });
            builder.Entity<Bag>().HasKey(pc => new { pc.Id });
            builder.Entity<Parcel>().HasKey(pc => new { pc.Id });
            

            //Configure relationships

            //One to Many 
            builder.Entity<Bag>()
                .HasOne(p => p.Shipment)
                .WithMany(b => b.Bags)
                .HasForeignKey(p => p.ShipmentId);

            //StructureGroup One to Many 
            builder.Entity<Parcel>()
                .HasOne(p => p.Bag)
                .WithMany(b => b.Parcels)
                .HasForeignKey(p => p.BagId);

        }
        
    }
}
