using Microsoft.EntityFrameworkCore;
using socials.DBContext.Models.GAR;

namespace socials.DBContext;
public partial class GARContext : DbContext
{
    public GARContext()
    {
    }

    public GARContext(DbContextOptions<GARContext> options)
        : base(options)
    {
    }
    public virtual DbSet<AdressObject> AsAddrObjs { get; set; }
    public virtual DbSet<Hierarchy> AsAdmHierarchies { get; set; }
    public virtual DbSet<House> AsHouses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdressObject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Addr_Objs");

            entity.ToTable("as_addr_obj", "fias");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Isactive)
                .HasColumnName("isactive");
            entity.Property(e => e.Level)
                .HasColumnName("level");
            entity.Property(e => e.Name)
                .HasColumnName("name");
            entity.Property(e => e.Objectguid)
                .HasColumnName("objectguid");
            entity.Property(e => e.Objectid)
                .HasColumnName("objectid");
            entity.Property(e => e.Typename)
                .HasColumnName("typename");
        });

        modelBuilder.Entity<Hierarchy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Adm_Hier");

            entity.ToTable("as_adm_hierarchy", "fias");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Isactive)
                .HasColumnName("isactive");
            entity.Property(e => e.Objectid)
                .HasColumnName("objectid");
            entity.Property(e => e.Parentobjid)
                .HasColumnName("parentobjid");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Houses");

            entity.ToTable("as_houses", "fias");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Addnum1)
                .HasColumnName("addnum1");
            entity.Property(e => e.Addnum2)
                .HasColumnName("addnum2");
            entity.Property(e => e.Addtype1)
                .HasColumnName("addtype1");
            entity.Property(e => e.Addtype2)
                .HasColumnName("addtype2");
            entity.Property(e => e.Housenum)
                .HasColumnName("housenum");
            entity.Property(e => e.Isactive)
                .HasColumnName("isactive");
            entity.Property(e => e.Objectguid)
                .HasColumnName("objectguid");
            entity.Property(e => e.Objectid)
                .HasColumnName("objectid");
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}