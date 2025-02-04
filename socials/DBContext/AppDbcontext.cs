using Microsoft.EntityFrameworkCore;
using socials.DBContext.Models;
using socials.DBContext.Models.Enums;

namespace socials.DBContext;

public class AppDbcontext(DbContextOptions<AppDbcontext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<BlackToken> BlackTokens { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Community> Communities { get; set; }
    public DbSet<CommunityUser> CommunityUsers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; } 
    
    public DbSet<PostTags> PostTags { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
    public DbSet<EmailQueues> EmailQueues { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CommunityUser>()
            .HasKey(cu => new { cu.CommunityId, cu.UserId });
        
        modelBuilder.Entity<PostLike>()
            .HasKey(pl => new { pl.PostId, pl.UserId });
        
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.AuthorUser)
            .WithMany(u => u.Comments) 
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(p => p.SubCommentsList)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<PostTags>(entity =>
        {
            entity.HasKey(pt => new { pt.PostId, pt.TagId });

            entity.HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);

            entity.HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);
        });
        
        modelBuilder.Entity<Tag>().HasData(
            new Tag
            {
                Id = Guid.Parse("e8f93a49-b93f-47f0-a912-08dbffad6d0e"), Name = "Путешествия",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-10T16:40:53.0123266"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("302d5c0c-5623-4810-a913-08dbffad6d0e"), Name = "Стажировка",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-10T16:40:53.0123261"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("2c4b19f5-511d-4f27-a914-08dbffad6d0e"), Name = "Новости",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T12:40:53.0123256"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("4676b2f4-de54-4fce-a915-08dbffad6d0e"), Name = "Праздники",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T12:40:53.0123252"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"), Name = "Рестораны",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T12:40:53.0123247"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("6cb7fe40-bafe-49bc-a917-08dbffad6d0e"), Name = "Книги",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T12:40:53.0123237"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("75735935-74d3-4fa2-a918-08dbffad6d0e"), Name = "Фотография",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-12T16:40:53.0123232"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("ed1b936e-9c67-4da6-a919-08dbffad6d0e"), Name = "Эстетика",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-12T16:40:53.0123227"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("87a9c38c-0d2d-4a52-a91a-08dbffad6d0e"), Name = "Кулинария",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-14T16:40:53.0123217"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("5aa83ee6-9bb0-4afe-a91b-08dbffad6d0e"), Name = "Музыка",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T14:40:53.0123212"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("6c20f45d-a7d1-4605-a91c-08dbffad6d0e"), Name = "Танцы",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T14:40:53.0123203"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"), Name = "Рисование",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-14T16:40:53.0123198"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("bf1f4b00-cf9c-48e4-a91e-08dbffad6d0e"), Name = "Университет",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-14T16:40:53.0123188"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("9ea305d2-b1f8-405e-a91f-08dbffad6d0e"), Name = "Работа",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T14:40:53.0123183"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("d82c6890-d26d-450b-a920-08dbffad6d0e"), Name = "Прогулка",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T14:40:53.0123178"), DateTimeKind.Utc)
            },
            new Tag
            {
                Id = Guid.Parse("d35c6890-d26d-450b-a920-08dbffad6d0e"), Name = "Увлечения",
                CreateTime = DateTime.SpecifyKind(DateTime.Parse("2024-11-18T14:40:53.0123178"), DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Community>().HasData(
            new Community
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                CreateTime = DateTime.UtcNow,
                Name = "Котята",
                Description = "Публикуем мемы с котами!",
                IsClosed = false,
                SubscribersCount = 1
            },
            new Community
            {
                Id = Guid.Parse("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                CreateTime = DateTime.UtcNow,
                Name = "Секретное сообщество",
                Description = "Одобряем заявку только избранным",
                IsClosed = true,
                SubscribersCount = 1
            },
            new Community
            {
                Id = Guid.Parse("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                CreateTime = DateTime.UtcNow,
                Name = "Книжный клуб",
                Description = "Делимся мнением о прочитанных книгах",
                IsClosed = false,
                SubscribersCount = 1
            },
            new Community
            {
                Id = Guid.Parse("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                CreateTime = DateTime.UtcNow,
                Name = "Самый крутой магазин одежды",
                Description = "Самая модная одежда",
                IsClosed = false,
                SubscribersCount = 1
            }
        );
        
        modelBuilder.Entity<CommunityUser>().HasData(
            new CommunityUser
            {
                CommunityId = Guid.Parse("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                UserId = Guid.Parse("e71da460-f06b-406f-800a-d18712fa9edd"), 
                Role = CommunityRole.Administrator
            },
            new CommunityUser
            {
                CommunityId = Guid.Parse("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                UserId = Guid.Parse("e71da460-f06b-406f-800a-d18712fa9edd"), 
                Role = CommunityRole.Administrator
            },
            new CommunityUser
            {
                CommunityId = Guid.Parse("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                UserId = Guid.Parse("e71da460-f06b-406f-800a-d18712fa9edd"), 
                Role = CommunityRole.Administrator
            },
            new CommunityUser
            {
                CommunityId = Guid.Parse("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                UserId = Guid.Parse("e71da460-f06b-406f-800a-d18712fa9edd"),
                Role = CommunityRole.Administrator
            }
        );
        
    }
}