using System.Text.Json;
using Apilot.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Apilot.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<WorkSpaceEntity> WorkSpaces { get; set; }
    public DbSet<CollectionEntity> Collections { get; set; }
    public DbSet<FolderEntity> Folders { get; set; }
    public DbSet<RequestEntity> Requests { get; set; }
    public DbSet<ResponseEntity> Responses { get; set; }
    public DbSet<EnvironementEntity> Environements { get; set; }
    public DbSet<HistoryEntity> Histories { get; set; }
    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // WorkSpace relationships
        modelBuilder.Entity<WorkSpaceEntity>()
            .HasMany(w => w.Collections)
            .WithOne(c => c.WorkSpaceEntity)
            .HasForeignKey(c => c.WorkSpaceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkSpaceEntity>()
            .HasMany(w => w.Environements)
            .WithOne(e => e.WorkSpaceEntity)
            .HasForeignKey(e => e.WorkSpaceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Collection relationships
        modelBuilder.Entity<CollectionEntity>()
            .HasMany(c => c.Folders)
            .WithOne(f => f.CollectionEntity)
            .HasForeignKey(f => f.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CollectionEntity>()
            .HasMany(c => c.HttpRequests)
            .WithOne(r => r.CollectionEntity)
            .HasForeignKey(r => r.CollectionId)
            .OnDelete(DeleteBehavior.NoAction); // Using NoAction because of potential circular cascade path with Folder

        // Folder relationships
        modelBuilder.Entity<FolderEntity>()
            .HasMany(f => f.HttpRequests)
            .WithOne(r => r.FolderEntity)
            .HasForeignKey(r => r.FolderId)
            .OnDelete(DeleteBehavior.Cascade);

        // HttpRequest relationships
        modelBuilder.Entity<RequestEntity>()
            .HasMany(r => r.Responses)
            .WithOne(r => r.Request)
            .HasForeignKey(r => r.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        // History relationships
        modelBuilder.Entity<HistoryEntity>()
            .HasMany(h => h.Requests)
            .WithMany() // Many-to-many without navigation property back to History
            .UsingEntity(j => j.ToTable("HistoryRequests"));

        // Complex types and owned entities
        modelBuilder.Entity<RequestEntity>()
            .OwnsOne(r => r.Authentication);

        modelBuilder.Entity<ResponseEntity>()
            .OwnsOne(r => r.CookiesEntity);

        // Dictionary properties configuration
        modelBuilder.Entity<EnvironementEntity>()
            .Property(e => e.Variables)
            .HasConversion(
                v => System.Text.Json.JsonSerializer.Serialize(v, new JsonSerializerOptions() ),
                v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, string>());

        modelBuilder.Entity<RequestEntity>()
            .Property(r => r.Headers)
            .HasConversion(
                v => System.Text.Json.JsonSerializer.Serialize(v,  new JsonSerializerOptions()),
                v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v,  new JsonSerializerOptions()) ?? new Dictionary<string, string>());

        modelBuilder.Entity<ResponseEntity>()
            .Property(r => r.Headers)
            .HasConversion(
                v => System.Text.Json.JsonSerializer.Serialize(v,  new JsonSerializerOptions()),
                v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v,  new JsonSerializerOptions()) ?? new Dictionary<string, string>());

        modelBuilder.Entity<AuthenticationEntity>()
            .Property(a => a.AuthData)
            .HasConversion(
                v => System.Text.Json.JsonSerializer.Serialize(v,  new JsonSerializerOptions()),
                v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v,  new JsonSerializerOptions()) ?? new Dictionary<string, string>());
    }

}