﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using reviewsapp.Data;

#nullable disable

namespace reviewsapp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ModelCategory", b =>
                {
                    b.Property<int>("modelId")
                        .HasColumnType("int");

                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.HasKey("modelId", "categoryId");

                    b.HasIndex("categoryId");

                    b.ToTable("ModelCategories");
                });

            modelBuilder.Entity("reviewsapp.models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("reviewsapp.models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("reviewsapp.models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WashDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("reviewsapp.models.ModelOwner", b =>
                {
                    b.Property<int>("modelId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("modelId", "OwnerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("ModelOwners");
                });

            modelBuilder.Entity("reviewsapp.models.OwnerName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Gym")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("ModelId");

                    b.ToTable("OwnerName");
                });

            modelBuilder.Entity("reviewsapp.models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int?>("ModelOwnerOwnerId")
                        .HasColumnType("int");

                    b.Property<int?>("ModelOwnermodelId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.HasIndex("ReviewerId");

                    b.HasIndex("ModelOwnermodelId", "ModelOwnerOwnerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("reviewsapp.models.Reviewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviewers");
                });

            modelBuilder.Entity("ModelCategory", b =>
                {
                    b.HasOne("reviewsapp.models.Category", "Category")
                        .WithMany("ModelCategories")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("reviewsapp.models.Model", "Model")
                        .WithMany("ModelCategories")
                        .HasForeignKey("modelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("reviewsapp.models.ModelOwner", b =>
                {
                    b.HasOne("reviewsapp.models.OwnerName", "Owner")
                        .WithMany("ModelOwners")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("reviewsapp.models.Model", "Model")
                        .WithMany("ModelOwners")
                        .HasForeignKey("modelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("reviewsapp.models.OwnerName", b =>
                {
                    b.HasOne("reviewsapp.models.Country", "Country")
                        .WithMany("OwnerNames")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("reviewsapp.models.Model", null)
                        .WithMany("OwnerNames")
                        .HasForeignKey("ModelId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("reviewsapp.models.Review", b =>
                {
                    b.HasOne("reviewsapp.models.Model", "Model")
                        .WithMany("reviews")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("reviewsapp.models.Reviewer", "Reviewer")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("reviewsapp.models.ModelOwner", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ModelOwnermodelId", "ModelOwnerOwnerId");

                    b.Navigation("Model");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("reviewsapp.models.Category", b =>
                {
                    b.Navigation("ModelCategories");
                });

            modelBuilder.Entity("reviewsapp.models.Country", b =>
                {
                    b.Navigation("OwnerNames");
                });

            modelBuilder.Entity("reviewsapp.models.Model", b =>
                {
                    b.Navigation("ModelCategories");

                    b.Navigation("ModelOwners");

                    b.Navigation("OwnerNames");

                    b.Navigation("reviews");
                });

            modelBuilder.Entity("reviewsapp.models.ModelOwner", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("reviewsapp.models.OwnerName", b =>
                {
                    b.Navigation("ModelOwners");
                });

            modelBuilder.Entity("reviewsapp.models.Reviewer", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
