using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class GuitarsAndMoreDBContext : DbContext
    {
        public GuitarsAndMoreDBContext()
        {
        }

        public User Login(string email, string pswd)
        {
            User user = this.Users
                //.Include(us => us.UserContacts)
                //.ThenInclude(uc => uc.ContactPhones)
                .Where(u => u.Email == email && u.Pass == pswd).FirstOrDefault();

            return user;
        }

        public GuitarsAndMoreDBContext(DbContextOptions<GuitarsAndMoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<ModelReview> ModelReviews { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFavoritePost> UserFavoritePosts { get; set; }
        public virtual DbSet<UserReview> UserReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=GuitarsAndMoreDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.Area1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Area");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Category1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Category");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Gender1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Gender");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProducerId).HasColumnName("ProducerID");
            });

            modelBuilder.Entity<ModelReview>(entity =>
            {
                entity.Property(e => e.ModelReviewId).HasColumnName("ModelReviewID");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.ModelReview1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("ModelReview");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ModelReviews)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelReviews_ModelID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ModelReviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelReviews_UserID");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.Pdescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("PDescription");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.TownId).HasColumnName("TownID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_CategoryID");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Post_ModelID");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK_Post_ReviewID");

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.TownId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_TownID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_UserID");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.Property(e => e.ProducerId).HasColumnName("ProducerID");

                entity.Property(e => e.Producer1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Producer");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.Property(e => e.TownId).HasColumnName("TownID");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.Town1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Town");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK__Towns__AreaID__48CFD27E");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D105341106A5DE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FavBand).HasMaxLength(255);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.VerPassword).HasMaxLength(255);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_GenderID");
            });

            modelBuilder.Entity<UserFavoritePost>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserId });

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.UserFavoritePosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFavoritePosts_PostID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFavoritePosts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFavoritePosts_UserID");
            });

            modelBuilder.Entity<UserReview>(entity =>
            {
                entity.Property(e => e.UserReviewId).HasColumnName("UserReviewID");

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserReview1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("UserReview");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.UserReviewSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserReviews_SellerID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserReviewUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserReviews_UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
