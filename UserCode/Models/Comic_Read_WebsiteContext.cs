using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserCode.Models
{
    public partial class Comic_Read_WebsiteContext : DbContext
    {
        public Comic_Read_WebsiteContext()
        {
        }

        public Comic_Read_WebsiteContext(DbContextOptions<Comic_Read_WebsiteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<AuthorDetail> AuthorDetails { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryAccount> CategoryAccounts { get; set; } = null!;
        public virtual DbSet<Chapter> Chapters { get; set; } = null!;
        public virtual DbSet<Comic> Comics { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Posting> Postings { get; set; } = null!;
        public virtual DbSet<WordDetail> WordDetails { get; set; } = null!;
        public virtual DbSet<WordToxic> WordToxics { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Comic_Read_Website;Integrated Security=True", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountImage)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName).HasMaxLength(40);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassWord)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CategoryAcc)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CategoryAccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__Categor__31EC6D26");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AuthorName).HasMaxLength(50);
            });

            modelBuilder.Entity<AuthorDetail>(entity =>
            {
                entity.HasKey(e => new { e.ComicId, e.AuthorId })
                    .HasName("pk_AuthorDetail");

                entity.ToTable("AuthorDetail");

                entity.Property(e => e.ComicId)
                    .HasColumnName("ComicID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorDetails)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AuthorDet__Autho__2E1BDC42");

                entity.HasOne(d => d.Comic)
                    .WithMany(p => p.AuthorDetails)
                    .HasForeignKey(d => d.ComicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AuthorDet__Comic__47DBAE45");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(100);
            });

            modelBuilder.Entity<CategoryAccount>(entity =>
            {
                entity.HasKey(e => e.CategoryAccId)
                    .HasName("pk_CategoryAccount");

                entity.ToTable("CategoryAccount");

                entity.Property(e => e.CategoryAccName).HasMaxLength(50);
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.ToTable("Chapter");

                entity.Property(e => e.ChapterId)
                    .HasColumnName("ChapterID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ChapterName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ComicId).HasColumnName("ComicID");

                entity.Property(e => e.UpdateDay).HasColumnType("datetime");

                entity.HasOne(d => d.Comic)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.ComicId)
                    .HasConstraintName("FK__Chapter__ComicID__45F365D3");
            });

            modelBuilder.Entity<Comic>(entity =>
            {
                entity.Property(e => e.ComicId)
                    .HasColumnName("ComicID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ComicBanner)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ComicName).HasMaxLength(100);

                entity.Property(e => e.ComicStatus).HasMaxLength(20);

                entity.Property(e => e.DateSummitted).HasColumnType("datetime");

                entity.Property(e => e.Describe).HasMaxLength(250);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Comics)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comics__AccountI__44FF419A");

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Comics)
                    .UsingEntity<Dictionary<string, object>>(
                        "ComicCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("CategoryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Comic-Cat__Categ__2F10007B"),
                        r => r.HasOne<Comic>().WithMany().HasForeignKey("ComicId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Comic-Cat__Comic__46E78A0C"),
                        j =>
                        {
                            j.HasKey("ComicId", "CategoryId").HasName("pk_Comic-Category");

                            j.ToTable("Comic-Category");

                            j.IndexerProperty<Guid>("ComicId").HasColumnName("ComicID").HasDefaultValueSql("(newid())");

                            j.IndexerProperty<int>("CategoryId").HasColumnName("CategoryID");
                        });
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.CommnentId)
                    .HasName("pk_Comment");

                entity.ToTable("Comment");

                entity.Property(e => e.CommnentId).HasColumnName("CommnentID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ChapterId).HasColumnName("ChapterID");

                entity.Property(e => e.CommnentContent).HasMaxLength(200);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__Account__286302EC");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("FK__Comment__Chapter__2D27B809");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ChapterId).HasColumnName("ChapterID");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl1)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl10)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl11)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl12)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl13)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl14)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl2)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl3)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl4)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl5)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl6)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl7)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl8)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl9)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("FK__Image__ChapterID__49C3F6B7");
            });

            modelBuilder.Entity<Posting>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("pk_Posting");

                entity.ToTable("Posting");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PostContent).HasMaxLength(300);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Postings)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Posting__Account__29572725");
            });

            modelBuilder.Entity<WordDetail>(entity =>
            {
                entity.HasKey(e => new { e.WordToxicId, e.CommnentId })
                    .HasName("pk_WordDetails");

                entity.Property(e => e.WordToxicId)
                    .HasColumnName("WordToxicID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CommnentId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CommnentID");

                entity.Property(e => e.Content).HasMaxLength(200);

                entity.HasOne(d => d.Commnent)
                    .WithMany(p => p.WordDetails)
                    .HasForeignKey(d => d.CommnentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WordDetai__Commn__300424B4");

                entity.HasOne(d => d.WordToxic)
                    .WithMany(p => p.WordDetails)
                    .HasForeignKey(d => d.WordToxicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WordDetai__WordT__30F848ED");
            });

            modelBuilder.Entity<WordToxic>(entity =>
            {
                entity.ToTable("WordToxic");

                entity.Property(e => e.WordToxicId)
                    .HasColumnName("WordToxicID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.WordToxicName).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
