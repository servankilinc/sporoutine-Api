using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class AppBaseDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{ 
    public AppBaseDbContext(DbContextOptions<AppBaseDbContext> options): base(options)
    {
    }

    public override DbSet<User> Users { get; set; } 
    public DbSet<RefreshToken> RefreshTokens { get; set; } 

    public DbSet<PhysicalData> PhysicalData { get; set; }
    public DbSet<WeightHistoryData> WeightHistoryData { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<RegionExercise> RegionExercises { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<ProgramExercise> ProgramExercises { get; set; }
    public DbSet<Fulfillment> Fulfillments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<PhysicalData>(pd =>
        {
            pd.ToTable("PhysicalData");
            pd.HasKey(pd => pd.UserId);
            pd.Property(pd => pd.UserId).HasColumnName("UserId");
            pd.Property(pd => pd.Height).HasColumnName("Height");
            pd.Property(pd => pd.Weight).HasColumnName("Weight");
            pd.Property(pd => pd.BodyMassIndex).HasColumnName("BodyMassIndex");

            pd.HasOne(pd => pd.User)
               .WithOne(u => u.PhysicalData)
               .HasForeignKey<PhysicalData>(pd => pd.UserId);
        });
        

        modelBuilder.Entity<WeightHistoryData>(wd =>
        {
            wd.ToTable("WeightHistoryData");
            wd.HasKey(wd => wd.Id);
            wd.HasIndex(wd => new { wd.UserId, wd.AddedDate }).IsUnique();
            wd.Property(wd => wd.UserId).HasColumnName("UserId");
            wd.Property(wd => wd.Weight).HasColumnName("Weight");
            wd.Property(wd => wd.AddedDate).HasColumnName("AddedDate");

            wd.HasOne(wd => wd.User)
               .WithMany(u => u.WeightDataset)
               .HasForeignKey(wd => wd.UserId);
        });

        modelBuilder.Entity<Exercise>(e =>
        {
            e.ToTable("Exercises");
            e.HasKey(p => p.Id); 
            e.Property(p => p.Name).HasColumnName("Name");
            e.Property(p => p.Content).HasColumnName("Content");
            e.Property(p => p.Description).HasColumnName("Description");
            e.Property(p => p.ExerciseType).HasColumnName("ExerciseType");

            e.HasMany(e => e.RegionExercises)
                .WithOne(re => re.Exercise)
                .HasForeignKey(re => re.ExerciseId);
            
            e.HasMany(e => e.ProgramExercises)
                .WithOne(pe => pe.Exercise)
                .HasForeignKey(pe => pe.ExerciseId);
        });

        modelBuilder.Entity<Region>(r =>
        {
            r.ToTable("Regions");
            r.HasKey(r => r.Id);
            r.Property(r => r.Id).HasColumnName("Id");
            r.Property(r => r.Name).HasColumnName("Name");
            r.Property(r => r.Content).HasColumnName("Content"); 

            r.HasMany(r => r.RegionExercises)
                .WithOne(re => re.Region)
                .HasForeignKey(re => re.RegionId);
        });

        modelBuilder.Entity<RegionExercise>(re =>
        {
            re.ToTable("RegionExercises");
            re.HasKey(re => new {re.RegionId, re.ExerciseId}); 
            re.Property(re => re.RegionId).HasColumnName("RegionId");
            re.Property(re => re.ExerciseId).HasColumnName("ExerciseId");

            re.HasOne(re => re.Region)
                .WithMany(r => r.RegionExercises)
                .HasForeignKey(re => re.RegionId);

            re.HasOne(re => re.Exercise)
                .WithMany(e => e.RegionExercises)
                .HasForeignKey(re => re.ExerciseId);
        });

        modelBuilder.Entity<Program>(p =>
        {
            p.ToTable("Programs");
            p.HasKey(p => p.Id);
            p.Property(p => p.UserId).HasColumnName("UserId");
            p.Property(p => p.Name).HasColumnName("Name");
            p.Property(p => p.CreatedDate).HasColumnName("CreatedDate");

            p.HasOne(p => p.User)
                .WithMany(u => u.Programs)
                .HasForeignKey(p => p.UserId);

            p.HasMany(p => p.ProgramExercises)
                .WithOne(pe => pe.Program)
                .HasForeignKey(pe => pe.ProgramId);
        });

        modelBuilder.Entity<ProgramExercise>(pe =>
        {
            pe.ToTable("ProgramExercises");
            pe.HasKey(pe => pe.Id);
            pe.HasIndex(pe => new { pe.ProgramId, pe.ExerciseId, pe.Day }).IsUnique();
            pe.Property(pe => pe.ProgramId).HasColumnName("ProgramId");
            pe.Property(pe => pe.ExerciseId).HasColumnName("ExerciseId");
            pe.Property(pe => pe.AddedDate).HasColumnName("AddedDate");
            pe.Property(pe => pe.Day).HasColumnName("Day");
            pe.Property(pe => pe.NumberOfSets).HasColumnName("NumberOfSets");
            pe.Property(pe => pe.NumberOfRepetition).HasColumnName("NumberOfRepetition");
            pe.Property(pe => pe.Time).HasColumnName("Time");

            pe.HasOne(pe => pe.Program)
                .WithMany(p => p.ProgramExercises)
                .HasForeignKey(pe => pe.ProgramId);

            pe.HasOne(pe => pe.Exercise)
                .WithMany(e => e.ProgramExercises)
                .HasForeignKey(pe => pe.ExerciseId);

           pe.HasMany(pe => pe.Fulfillments)
                .WithOne(f => f.ProgramExercise)
                .HasForeignKey(pe => pe.ProgramExerciseId);
        });

        modelBuilder.Entity<Fulfillment>(f =>
        {
            f.ToTable("Fulfillments"); 
            f.HasKey(f => new { f.ProgramExerciseId, f.CompletionDateIndexer }); 
            f.Property(f => f.ProgramExerciseId).HasColumnName("ProgramExerciseId");
            f.Property(f => f.CompletionDate).HasColumnName("CompletionDate");
            f.Property(f => f.CompletionDateIndexer).HasColumnName("CompletionDateIndexer");
            
            f.HasOne(f => f.ProgramExercise)
                .WithMany(pe => pe.Fulfillments)
                .HasForeignKey(f => f.ProgramExerciseId);
        });


        modelBuilder.Entity<User>(u =>
        {
            u.ToTable("Users");
            u.Property(u => u.FirstName).HasColumnName("FirstName");
            u.Property(u => u.LastName).HasColumnName("LastName");

            u.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId);

            u.HasMany(u => u.Programs)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            u.HasOne(u => u.PhysicalData)
               .WithOne(pd => pd.User)
               .HasForeignKey<PhysicalData>(pd => pd.UserId);

            u.HasMany(u => u.WeightDataset)
               .WithOne(wd => wd.User)
               .HasForeignKey(wd => wd.UserId);
        });
        
        modelBuilder.Entity<RefreshToken>(rt =>
        {
            rt.ToTable("RefreshTokens");
            rt.HasKey(rt => new { rt.UserId, rt.CreatedIp });
            rt.Property(rt => rt.UserId).HasColumnName("UserId");
            rt.Property(rt => rt.CreatedIp).HasColumnName("CreatedIp");
            rt.Property(rt => rt.Token).HasColumnName("Token");
            rt.Property(rt => rt.Expiration).HasColumnName("Expiration");
            rt.Property(rt => rt.CreatedDate).HasColumnName("CreatedDate");
            rt.Property(rt => rt.TTL).HasColumnName("TTL");

            rt.HasOne(rt=> rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId); 
        });

        modelBuilder.Entity<IdentityRole<Guid>>(entity => { entity.ToTable("Roles"); });

        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims"); });

        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins"); });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims"); });

        modelBuilder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles"); });

        modelBuilder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens"); });
    }
}
