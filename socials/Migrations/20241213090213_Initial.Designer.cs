﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using socials.DBContext;

#nullable disable

namespace socials.Migrations
{
    [DbContext(typeof(AppDbcontext))]
    [Migration("20241213090213_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("socials.DBContext.Models.BlackToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Blacktoken")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("BlackTokens");
                });

            modelBuilder.Entity("socials.DBContext.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<int>("SubComments")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("socials.DBContext.Models.Community", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SubscribersCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Communities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a1b2c3d4-e5f6-0000-1234-567890abcdef"),
                            CreateTime = new DateTime(2024, 12, 13, 9, 2, 12, 380, DateTimeKind.Utc).AddTicks(5140),
                            Description = "Публикуем мемы с котами!",
                            IsClosed = false,
                            Name = "Котята",
                            SubscribersCount = 1
                        },
                        new
                        {
                            Id = new Guid("f0e9d8c7-b6a5-1111-9876-543210fedcba"),
                            CreateTime = new DateTime(2024, 12, 13, 9, 2, 12, 380, DateTimeKind.Utc).AddTicks(5150),
                            Description = "Одобряем заявку только избранным",
                            IsClosed = true,
                            Name = "Секретное сообщество",
                            SubscribersCount = 1
                        },
                        new
                        {
                            Id = new Guid("f0e6d8c9-b6a5-2222-9876-543110fedcba"),
                            CreateTime = new DateTime(2024, 12, 13, 9, 2, 12, 380, DateTimeKind.Utc).AddTicks(5150),
                            Description = "Делимся мнением о прочитанных книгах",
                            IsClosed = false,
                            Name = "Книжный клуб",
                            SubscribersCount = 1
                        },
                        new
                        {
                            Id = new Guid("f0e6d8c9-b6a5-3333-9876-543110fedcba"),
                            CreateTime = new DateTime(2024, 12, 13, 9, 2, 12, 380, DateTimeKind.Utc).AddTicks(5150),
                            Description = "Самая модная одежда",
                            IsClosed = false,
                            Name = "Самый крутой магазин одежды",
                            SubscribersCount = 1
                        });
                });

            modelBuilder.Entity("socials.DBContext.Models.CommunityUser", b =>
                {
                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("CommunityId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CommunityUsers");
                });

            modelBuilder.Entity("socials.DBContext.Models.EmailQueues", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<int>("Retries")
                        .HasColumnType("integer");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("EmailQueues");
                });

            modelBuilder.Entity("socials.DBContext.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<string>("CommunityName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("HasLike")
                        .HasColumnType("boolean");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Likes")
                        .HasColumnType("integer");

                    b.Property<int>("ReadingTime")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("socials.DBContext.Models.PostLike", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("socials.DBContext.Models.PostTags", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("socials.DBContext.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e8f93a49-b93f-47f0-a912-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 10, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3266),
                            Name = "Путешествия"
                        },
                        new
                        {
                            Id = new Guid("302d5c0c-5623-4810-a913-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 10, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3261),
                            Name = "Стажировка"
                        },
                        new
                        {
                            Id = new Guid("2c4b19f5-511d-4f27-a914-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 12, 40, 53, 12, DateTimeKind.Utc).AddTicks(3256),
                            Name = "Новости"
                        },
                        new
                        {
                            Id = new Guid("4676b2f4-de54-4fce-a915-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 12, 40, 53, 12, DateTimeKind.Utc).AddTicks(3252),
                            Name = "Праздники"
                        },
                        new
                        {
                            Id = new Guid("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 12, 40, 53, 12, DateTimeKind.Utc).AddTicks(3247),
                            Name = "Рестораны"
                        },
                        new
                        {
                            Id = new Guid("6cb7fe40-bafe-49bc-a917-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 12, 40, 53, 12, DateTimeKind.Utc).AddTicks(3237),
                            Name = "Книги"
                        },
                        new
                        {
                            Id = new Guid("75735935-74d3-4fa2-a918-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 12, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3232),
                            Name = "Фотография"
                        },
                        new
                        {
                            Id = new Guid("ed1b936e-9c67-4da6-a919-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 12, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3227),
                            Name = "Эстетика"
                        },
                        new
                        {
                            Id = new Guid("87a9c38c-0d2d-4a52-a91a-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 14, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3217),
                            Name = "Кулинария"
                        },
                        new
                        {
                            Id = new Guid("5aa83ee6-9bb0-4afe-a91b-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3212),
                            Name = "Музыка"
                        },
                        new
                        {
                            Id = new Guid("6c20f45d-a7d1-4605-a91c-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3203),
                            Name = "Танцы"
                        },
                        new
                        {
                            Id = new Guid("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 14, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3198),
                            Name = "Рисование"
                        },
                        new
                        {
                            Id = new Guid("bf1f4b00-cf9c-48e4-a91e-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 14, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3188),
                            Name = "Университет"
                        },
                        new
                        {
                            Id = new Guid("9ea305d2-b1f8-405e-a91f-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3183),
                            Name = "Работа"
                        },
                        new
                        {
                            Id = new Guid("d82c6890-d26d-450b-a920-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3178),
                            Name = "Прогулка"
                        },
                        new
                        {
                            Id = new Guid("d35c6890-d26d-450b-a920-08dbffad6d0e"),
                            CreateTime = new DateTime(2024, 11, 18, 14, 40, 53, 12, DateTimeKind.Utc).AddTicks(3178),
                            Name = "Увлечения"
                        });
                });

            modelBuilder.Entity("socials.DBContext.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("socials.DBContext.Models.Comment", b =>
                {
                    b.HasOne("socials.DBContext.Models.User", "AuthorUser")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("socials.DBContext.Models.Comment", "ParentComment")
                        .WithMany("SubCommentsList")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("socials.DBContext.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AuthorUser");

                    b.Navigation("ParentComment");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("socials.DBContext.Models.CommunityUser", b =>
                {
                    b.HasOne("socials.DBContext.Models.Community", "Community")
                        .WithMany("CommunityUsers")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("socials.DBContext.Models.User", "User")
                        .WithMany("Communities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");

                    b.Navigation("User");
                });

            modelBuilder.Entity("socials.DBContext.Models.Post", b =>
                {
                    b.HasOne("socials.DBContext.Models.Community", "Community")
                        .WithMany()
                        .HasForeignKey("CommunityId");

                    b.Navigation("Community");
                });

            modelBuilder.Entity("socials.DBContext.Models.PostLike", b =>
                {
                    b.HasOne("socials.DBContext.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("socials.DBContext.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("socials.DBContext.Models.PostTags", b =>
                {
                    b.HasOne("socials.DBContext.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("socials.DBContext.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("socials.DBContext.Models.Comment", b =>
                {
                    b.Navigation("SubCommentsList");
                });

            modelBuilder.Entity("socials.DBContext.Models.Community", b =>
                {
                    b.Navigation("CommunityUsers");
                });

            modelBuilder.Entity("socials.DBContext.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("socials.DBContext.Models.Tag", b =>
                {
                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("socials.DBContext.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Communities");
                });
#pragma warning restore 612, 618
        }
    }
}
