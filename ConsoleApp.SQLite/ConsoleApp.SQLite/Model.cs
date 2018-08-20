using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.SQLite
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditEntry>();
            modelBuilder.Ignore<AuditEntry>();

            modelBuilder.Entity<RssBlog>()
                .HasBaseType<Blog>();

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.Url)
                .IsUnique();

            modelBuilder.Entity<Blog>()
                .Property(b => b.Inserted)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Blog>()
                .Property(b => b.LastUpdated)
                .ValueGeneratedOnAddOrUpdate();
        }
    }

    public class AuditEntry
    {
        public int AuditEntryId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Blog Blog { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Url { get; set; }
        
        public DateTime Inserted { get; set; }

        public DateTime LastUpdated { get; set; }
        
        public List<Post> Posts { get; set; }
    }

    public class RssBlog : Blog
    {
        public string RssUrl { get; set; }
    }
}