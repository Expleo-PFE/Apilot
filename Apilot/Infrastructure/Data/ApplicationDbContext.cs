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
    public DbSet<EnvironmentEntity> Environments { get; set; }
    public DbSet<HistoryEntity> Histories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

      
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
        
        modelBuilder.Entity<WorkSpaceEntity>()
            .HasMany(w => w.Histories)
            .WithOne(e => e.WorkSpace)
            .HasForeignKey(e => e.WorkSpaceId)
            .OnDelete(DeleteBehavior.Cascade);

       
        modelBuilder.Entity<CollectionEntity>()
            .HasMany(c => c.Folders)
            .WithOne(f => f.CollectionEntity)
            .HasForeignKey(f => f.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CollectionEntity>()
            .HasMany(c => c.HttpRequests)
            .WithOne(r => r.CollectionEntity)
            .HasForeignKey(r => r.CollectionId)
            .OnDelete(DeleteBehavior.Cascade); 

       
        modelBuilder.Entity<FolderEntity>()
            .HasMany(f => f.HttpRequests)
            .WithOne(r => r.FolderEntity)
            .HasForeignKey(r => r.FolderId)
            .OnDelete(DeleteBehavior.NoAction);

       
        modelBuilder.Entity<RequestEntity>()
            .HasMany(r => r.Responses)
            .WithOne(r => r.Request)
            .HasForeignKey(r => r.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

       
        modelBuilder.Entity<HistoryEntity>()
            .OwnsMany(r => r.Requests, req =>
            {
             
                req.Property(a => a.Url);
                req.Property(a => a.Body);
                req.Property(a => a.HttpMethod);
              
                req.Property(a => a.Headers)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                        v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, string>());
                
                req.OwnsOne(r => r.Authentication, authentication =>
                {
                    authentication.Property(a => a.AuthType);

                    authentication.Property(a => a.AuthData)
                        .HasConversion(
                            v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                            v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, string>());
                });
            });
        
        modelBuilder.Entity<RequestEntity>()
            .OwnsOne(r => r.Authentication, authentication =>
            {
             
                authentication.Property(a => a.AuthType);
                
                
                authentication.Property(a => a.AuthData)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                        v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, string>());
            });

        modelBuilder.Entity<ResponseEntity>()
            .OwnsOne(r => r.CookiesEntity);

       
        modelBuilder.Entity<EnvironmentEntity>()
            .Property(e => e.Variables)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, string>());

        modelBuilder.Entity<RequestEntity>()
            .Property(r => r.Headers)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, string>());

        modelBuilder.Entity<ResponseEntity>()
            .Property(r => r.Headers)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, string>());

        
        
        modelBuilder.Entity<WorkSpaceEntity>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<CollectionEntity>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<FolderEntity>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<RequestEntity>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<ResponseEntity>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<EnvironmentEntity>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<HistoryEntity>().HasQueryFilter(p => !p.IsDeleted);
      
    }
}