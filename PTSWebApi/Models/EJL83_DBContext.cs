using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PTSWebApi.Models
{
    public partial class EJL83_DBContext : DbContext
    {
        public EJL83_DBContext()
        {
        }

        public EJL83_DBContext(DbContextOptions<EJL83_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PtsComment> PtsComment { get; set; }
        public virtual DbSet<PtsDosageAmount> PtsDosageAmount { get; set; }
        public virtual DbSet<PtsDosageType> PtsDosageType { get; set; }
        public virtual DbSet<PtsFieldBlock> PtsFieldBlock { get; set; }
        public virtual DbSet<PtsPlant> PtsPlant { get; set; }
        public virtual DbSet<PtsPlantType> PtsPlantType { get; set; }
        public virtual DbSet<PtsProduct> PtsProduct { get; set; }
        public virtual DbSet<PtsProductCategory> PtsProductCategory { get; set; }
        public virtual DbSet<PtsResultEntry> PtsResultEntry { get; set; }
        public virtual DbSet<PtsResultFormat> PtsResultFormat { get; set; }
        public virtual DbSet<PtsTreamentProduct> PtsTreamentProduct { get; set; }
        public virtual DbSet<PtsTreatment> PtsTreatment { get; set; }
        public virtual DbSet<PtsTreatmentComment> PtsTreatmentComment { get; set; }
        public virtual DbSet<PtsTreatmentImage> PtsTreatmentImage { get; set; }
        public virtual DbSet<PtsTreatmentType> PtsTreatmentType { get; set; }
        public virtual DbSet<PtsTrialBlock> PtsTrialBlock { get; set; }
        public virtual DbSet<PtsTrialGroup> PtsTrialGroup { get; set; }
        public virtual DbSet<PtsTrialObservation> PtsTrialObservation { get; set; }
        public virtual DbSet<PtsTrialType> PtsTrialType { get; set; }
        public virtual DbSet<PtsTrialTypeDosageType> PtsTrialTypeDosageType { get; set; }
        public virtual DbSet<PtsUnitType> PtsUnitType { get; set; }
        public virtual DbSet<PtsUser> PtsUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "db_owner");

            modelBuilder.Entity<PtsComment>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("PTS_Comment");

                entity.Property(e => e.ChangedDate).HasColumnType("datetime");

                entity.Property(e => e.CommentText)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PtsComment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_Comment_UserId");
            });

            modelBuilder.Entity<PtsDosageAmount>(entity =>
            {
                entity.HasKey(e => e.DosageAmountId);

                entity.ToTable("PTS_DosageAmount");

                entity.HasOne(d => d.DosageType)
                    .WithMany(p => p.PtsDosageAmount)
                    .HasForeignKey(d => d.DosageTypeId)
                    .HasConstraintName("fk_DosageAmount_DosageTypeId");

                entity.HasOne(d => d.UnitType)
                    .WithMany(p => p.PtsDosageAmount)
                    .HasForeignKey(d => d.UnitTypeId)
                    .HasConstraintName("fk_DosageAmount_UnitTypeId");
            });

            modelBuilder.Entity<PtsDosageType>(entity =>
            {
                entity.HasKey(e => e.DosageTypeId);

                entity.ToTable("PTS_DosageType");

                entity.Property(e => e.DosageName).HasMaxLength(200);
            });

            modelBuilder.Entity<PtsFieldBlock>(entity =>
            {
                entity.HasKey(e => e.FieldBlockId);

                entity.ToTable("PTS_FieldBlock");

                entity.HasIndex(e => new { e.BlockChar, e.YearCreated })
                    .HasName("UQ_PTS_FieldBlock")
                    .IsUnique();

                entity.Property(e => e.BlockChar)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.BlockDescription).HasColumnType("text");
            });

            modelBuilder.Entity<PtsPlant>(entity =>
            {
                entity.HasKey(e => e.PlantId);

                entity.ToTable("PTS_Plant");

                entity.Property(e => e.PlantName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.PlantType)
                    .WithMany(p => p.PtsPlant)
                    .HasForeignKey(d => d.PlantTypeId)
                    .HasConstraintName("fk_Plant_PlantTypeId");
            });

            modelBuilder.Entity<PtsPlantType>(entity =>
            {
                entity.HasKey(e => e.PlantTypeId);

                entity.ToTable("PTS_PlantType");

                entity.Property(e => e.PlantTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PtsProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("PTS_Product");

                entity.HasIndex(e => new { e.ProductName, e.ProductOwner })
                    .HasName("uq_product_nameandowner")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductOwner).HasMaxLength(100);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.PtsProduct)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .HasConstraintName("fk_product_ProductCategoryId");
            });

            modelBuilder.Entity<PtsProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProductCategoryId);

                entity.ToTable("PTS_ProductCategory");

                entity.HasIndex(e => e.ProductCategoryName)
                    .HasName("UQ__PTS_Prod__CE9F88B523150941")
                    .IsUnique();

                entity.Property(e => e.ProductCategoryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PtsResultEntry>(entity =>
            {
                entity.HasKey(e => e.ResultEntryId);

                entity.ToTable("PTS_ResultEntry");

                entity.HasIndex(e => new { e.ResultFormatId, e.DosageAmountId, e.TrialGroupId })
                    .HasName("uq_ResultEntry_AllIds")
                    .IsUnique();

                entity.HasOne(d => d.DosageAmount)
                    .WithMany(p => p.PtsResultEntry)
                    .HasForeignKey(d => d.DosageAmountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ResultEntry_DosageAmountId");

                entity.HasOne(d => d.ResultFormat)
                    .WithMany(p => p.PtsResultEntry)
                    .HasForeignKey(d => d.ResultFormatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ResultEntry_ResultFormatId");

                entity.HasOne(d => d.TrialGroup)
                    .WithMany(p => p.PtsResultEntry)
                    .HasForeignKey(d => d.TrialGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ResultEntry_TrialGroupId");
            });

            modelBuilder.Entity<PtsResultFormat>(entity =>
            {
                entity.HasKey(e => e.ResultFormatId);

                entity.ToTable("PTS_ResultFormat");

                entity.Property(e => e.ResultFormatDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ResultFormatTitle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.TrialBlock)
                    .WithMany(p => p.PtsResultFormat)
                    .HasForeignKey(d => d.TrialBlockId)
                    .HasConstraintName("fk_ResultFormat_TrialBlockId");

                entity.HasOne(d => d.UnitType)
                    .WithMany(p => p.PtsResultFormat)
                    .HasForeignKey(d => d.UnitTypeId)
                    .HasConstraintName("fk_ResultFormat_UnitTypeId");
            });

            modelBuilder.Entity<PtsTreamentProduct>(entity =>
            {
                entity.HasKey(e => e.TreamentProductId);

                entity.ToTable("PTS_TreamentProduct");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PtsTreamentProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TreamentProduct_ProductId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.PtsTreamentProduct)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TreamentProduct_TreatmentTypeId");
            });

            modelBuilder.Entity<PtsTreatment>(entity =>
            {
                entity.HasKey(e => e.TreatmentId);

                entity.ToTable("PTS_Treatment");

                entity.Property(e => e.TreatmentDate).HasColumnType("date");

                entity.Property(e => e.TreatmentStage).HasMaxLength(255);

                entity.HasOne(d => d.TreatmentType)
                    .WithMany(p => p.PtsTreatment)
                    .HasForeignKey(d => d.TreatmentTypeId)
                    .HasConstraintName("fk_Treatment_TreatmentTypeId");

                entity.HasOne(d => d.TreatmentTypeNavigation)
                    .WithMany(p => p.PtsTreatment)
                    .HasForeignKey(d => d.TreatmentTypeId)
                    .HasConstraintName("fk_Treatment_TrialGroupId");
            });

            modelBuilder.Entity<PtsTreatmentComment>(entity =>
            {
                entity.HasKey(e => e.TreatmentCommentId);

                entity.ToTable("PTS_TreatmentComment");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.PtsTreatmentComment)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TreatmentComment_CommentId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.PtsTreatmentComment)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TreatmentComment_TreatmentId");
            });

            modelBuilder.Entity<PtsTreatmentImage>(entity =>
            {
                entity.HasKey(e => e.TreatmentImageId);

                entity.ToTable("PTS_TreatmentImage");

                entity.Property(e => e.Caption).HasMaxLength(350);

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.PtsTreatmentImage)
                    .HasForeignKey(d => d.TreatmentId)
                    .HasConstraintName("fk_TreatmentImage_TreatmentId");
            });

            modelBuilder.Entity<PtsTreatmentType>(entity =>
            {
                entity.HasKey(e => e.TreatmentTypeId);

                entity.ToTable("PTS_TreatmentType");

                entity.Property(e => e.TreatmentTypeName).HasMaxLength(100);
            });

            modelBuilder.Entity<PtsTrialBlock>(entity =>
            {
                entity.HasKey(e => e.TrialBlockId);

                entity.ToTable("PTS_TrialBlock");

                entity.Property(e => e.TrialBlockDescription).HasColumnType("text");

                entity.HasOne(d => d.FieldBlock)
                    .WithMany(p => p.PtsTrialBlock)
                    .HasForeignKey(d => d.FieldBlockId)
                    .HasConstraintName("fk_trialblock_FieldBlockId");

                entity.HasOne(d => d.TrialType)
                    .WithMany(p => p.PtsTrialBlock)
                    .HasForeignKey(d => d.TrialTypeId)
                    .HasConstraintName("fk_trialblock_TrialTypeId");
            });

            modelBuilder.Entity<PtsTrialGroup>(entity =>
            {
                entity.HasKey(e => e.TrialGroupId);

                entity.ToTable("PTS_TrialGroup");

                entity.HasOne(d => d.FieldBlock)
                    .WithMany(p => p.PtsTrialGroup)
                    .HasForeignKey(d => d.FieldBlockId)
                    .HasConstraintName("fk_TrialGroup_FieldBlockId");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.PtsTrialGroup)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("fk_TrialGroup_PlantId");
            });

            modelBuilder.Entity<PtsTrialObservation>(entity =>
            {
                entity.HasKey(e => e.TrialObservationId);

                entity.ToTable("PTS_TrialObservation");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.PtsTrialObservation)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("fk_TrialObservation_CommentId");

                entity.HasOne(d => d.TrialGroup)
                    .WithMany(p => p.PtsTrialObservation)
                    .HasForeignKey(d => d.TrialGroupId)
                    .HasConstraintName("fk_TrialObservation_TrialGroupId");
            });

            modelBuilder.Entity<PtsTrialType>(entity =>
            {
                entity.HasKey(e => e.TrialTypeId);

                entity.ToTable("PTS_TrialType");

                entity.Property(e => e.TrialTypeDescription).HasColumnType("text");

                entity.Property(e => e.TrialTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<PtsTrialTypeDosageType>(entity =>
            {
                entity.HasKey(e => e.TrialTypeDosageTypeId);

                entity.ToTable("PTS_TrialTypeDosageType");

                entity.HasIndex(e => new { e.TrialTypeId, e.DosageTypeId })
                    .HasName("uq_TrialTypeDosageType_TrialTypeId")
                    .IsUnique();

                entity.HasOne(d => d.DosageType)
                    .WithMany(p => p.PtsTrialTypeDosageType)
                    .HasForeignKey(d => d.DosageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TrialTypeDosageType_DosageType");

                entity.HasOne(d => d.TrialType)
                    .WithMany(p => p.PtsTrialTypeDosageType)
                    .HasForeignKey(d => d.TrialTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_TrialTypeDosageType_TrialTypeId");
            });

            modelBuilder.Entity<PtsUnitType>(entity =>
            {
                entity.HasKey(e => e.UnitTypeId);

                entity.ToTable("PTS_UnitType");

                entity.Property(e => e.UnitTypeName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<PtsUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("PTS_User");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(60);
            });
        }
    }
}
