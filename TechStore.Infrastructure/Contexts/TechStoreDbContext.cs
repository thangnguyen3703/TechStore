using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TechStore.Infrastructure.Entities;

namespace TechStore.Infrastructure.Contexts;

public partial class TechStoreDbContext : DbContext
{
    public TechStoreDbContext(DbContextOptions<TechStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttributeValue> AttributeValues { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchPriceList> BranchPriceLists { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryAttribute> CategoryAttributes { get; set; }

    public virtual DbSet<InventoryStock> InventoryStocks { get; set; }

    public virtual DbSet<InventoryTransaction> InventoryTransactions { get; set; }

    public virtual DbSet<PriceList> PriceLists { get; set; }

    public virtual DbSet<PriceListItem> PriceListItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StockDisposal> StockDisposals { get; set; }

    public virtual DbSet<StockDisposalDetail> StockDisposalDetails { get; set; }

    public virtual DbSet<StockIn> StockIns { get; set; }

    public virtual DbSet<StockInDetail> StockInDetails { get; set; }

    public virtual DbSet<StockOut> StockOuts { get; set; }

    public virtual DbSet<StockOutDetail> StockOutDetails { get; set; }

    public virtual DbSet<StockReturn> StockReturns { get; set; }

    public virtual DbSet<StockReturnDetail> StockReturnDetails { get; set; }

    public virtual DbSet<StockTransfer> StockTransfers { get; set; }

    public virtual DbSet<StockTransferDetail> StockTransferDetails { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VariantAttribute> VariantAttributes { get; set; }

    public virtual DbSet<VariantCost> VariantCosts { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttributeValue>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeValues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AttributeValues_Attributes");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<BranchPriceList>(entity =>
        {
            entity.HasIndex(e => e.BranchId, "UX_BranchPriceLists_DefaultPerBranch")
                .IsUnique()
                .HasFilter("([IsDefault]=(1) AND [IsActive]=(1))");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Branch).WithOne(p => p.BranchPriceList)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchPriceLists_Branches");

            entity.HasOne(d => d.PriceList).WithMany(p => p.BranchPriceLists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchPriceLists_PriceLists");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory).HasConstraintName("FK_Categories_ParentCategory");
        });

        modelBuilder.Entity<CategoryAttribute>(entity =>
        {
            entity.HasOne(d => d.Attribute).WithMany(p => p.CategoryAttributes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryAttributes_Attributes");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryAttributes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryAttributes_Categories");
        });

        modelBuilder.Entity<InventoryStock>(entity =>
        {
            entity.Property(e => e.AvailableQuantity).HasComputedColumnSql("([QuantityOnHand]-[ReservedQuantity])", true);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Variant).WithMany(p => p.InventoryStocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryStocks_ProductVariants");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.InventoryStocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryStocks_Warehouses");
        });

        modelBuilder.Entity<InventoryTransaction>(entity =>
        {
            entity.HasIndex(e => new { e.ReferenceType, e.ReferenceId }, "IX_InventoryTransactions_Reference").HasFilter("([ReferenceID] IS NOT NULL)");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Variant).WithMany(p => p.InventoryTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryTransactions_ProductVariants");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.InventoryTransactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryTransactions_Warehouses");
        });

        modelBuilder.Entity<PriceList>(entity =>
        {
            entity.HasIndex(e => e.PriceType, "UX_PriceLists_DefaultPerType")
                .IsUnique()
                .HasFilter("([IsDefault]=(1) AND [IsActive]=(1))");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<PriceListItem>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.PriceList).WithMany(p => p.PriceListItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceListItems_PriceLists");

            entity.HasOne(d => d.Variant).WithMany(p => p.PriceListItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceListItems_ProductVariants");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Brand).WithMany(p => p.Products).HasConstraintName("FK_Products_Brands");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK_Attributes");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasIndex(e => e.VariantId, "IX_ProductImages_VariantID").HasFilter("([VariantID] IS NOT NULL)");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImages_Products");

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.ProductImages)
                .HasPrincipalKey(p => new { p.VariantId, p.ProductId })
                .HasForeignKey(d => new { d.VariantId, d.ProductId })
                .HasConstraintName("FK_ProductImages_Variant_Product");
        });

        modelBuilder.Entity<ProductSpecification>(entity =>
        {
            entity.HasOne(d => d.Attribute).WithMany(p => p.ProductSpecifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductSpecifications_Attributes");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSpecifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductSpecifications_Products");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductVariants_Products");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<StockDisposal>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Warehouse).WithMany(p => p.StockDisposals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockDisposals_Warehouses");
        });

        modelBuilder.Entity<StockDisposalDetail>(entity =>
        {
            entity.HasOne(d => d.StockDisposal).WithMany(p => p.StockDisposalDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockDisposalDetails_StockDisposals");

            entity.HasOne(d => d.Variant).WithMany(p => p.StockDisposalDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockDisposalDetails_ProductVariants");
        });

        modelBuilder.Entity<StockIn>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Supplier).WithMany(p => p.StockIns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockIns_Suppliers");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.StockIns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockIns_Warehouses");
        });

        modelBuilder.Entity<StockInDetail>(entity =>
        {
            entity.Property(e => e.LineTotal).HasComputedColumnSql("(CONVERT([decimal](19,2),[Quantity]*[UnitCost]))", true);

            entity.HasOne(d => d.StockIn).WithMany(p => p.StockInDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockInDetails_StockIns");

            entity.HasOne(d => d.Variant).WithMany(p => p.StockInDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockInDetails_ProductVariants");
        });

        modelBuilder.Entity<StockOut>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_StockOuts_OrderID").HasFilter("([OrderID] IS NOT NULL)");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Warehouse).WithMany(p => p.StockOuts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockOuts_Warehouses");
        });

        modelBuilder.Entity<StockOutDetail>(entity =>
        {
            entity.Property(e => e.LineCost).HasComputedColumnSql("(CONVERT([decimal](19,4),[Quantity]*isnull([UnitCost],(0))))", true);

            entity.HasOne(d => d.StockOut).WithMany(p => p.StockOutDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockOutDetails_StockOuts");

            entity.HasOne(d => d.Variant).WithMany(p => p.StockOutDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockOutDetails_ProductVariants");
        });

        modelBuilder.Entity<StockReturn>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_StockReturns_OrderID").HasFilter("([OrderID] IS NOT NULL)");

            entity.HasIndex(e => e.SupplierId, "IX_StockReturns_SupplierID").HasFilter("([SupplierID] IS NOT NULL)");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Supplier).WithMany(p => p.StockReturns).HasConstraintName("FK_StockReturns_Suppliers");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.StockReturns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockReturns_Warehouses");
        });

        modelBuilder.Entity<StockReturnDetail>(entity =>
        {
            entity.HasIndex(e => e.SourceStockInDetailId, "IX_StockReturnDetails_SourceStockIn").HasFilter("([SourceStockInDetailID] IS NOT NULL)");

            entity.HasOne(d => d.SourceStockInDetail).WithMany(p => p.StockReturnDetails).HasConstraintName("FK_StockReturnDetails_SourceStockIn");

            entity.HasOne(d => d.StockReturn).WithMany(p => p.StockReturnDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockReturnDetails_StockReturns");

            entity.HasOne(d => d.Variant).WithMany(p => p.StockReturnDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockReturnDetails_ProductVariants");
        });

        modelBuilder.Entity<StockTransfer>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.FromWarehouse).WithMany(p => p.StockTransferFromWarehouses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransfers_FromWarehouse");

            entity.HasOne(d => d.ToWarehouse).WithMany(p => p.StockTransferToWarehouses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransfers_ToWarehouse");
        });

        modelBuilder.Entity<StockTransferDetail>(entity =>
        {
            entity.HasOne(d => d.StockTransfer).WithMany(p => p.StockTransferDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransferDetails_StockTransfers");

            entity.HasOne(d => d.Variant).WithMany(p => p.StockTransferDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransferDetails_ProductVariants");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Branch).WithMany(p => p.Users).HasConstraintName("FK_Users_Branches");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRoles_Roles"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRoles_Users"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_UserRoles_RoleID");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<int>("RoleId").HasColumnName("RoleID");
                    });
        });

        modelBuilder.Entity<VariantAttribute>(entity =>
        {
            entity.HasOne(d => d.Attribute).WithMany(p => p.VariantAttributes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VariantAttributes_Attributes");

            entity.HasOne(d => d.Variant).WithMany(p => p.VariantAttributes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VariantAttributes_ProductVariants");

            entity.HasOne(d => d.AttributeValue).WithMany(p => p.VariantAttributes)
                .HasPrincipalKey(p => new { p.AttributeValueId, p.AttributeId })
                .HasForeignKey(d => new { d.AttributeValueId, d.AttributeId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VariantAttributes_AttributeValues");
        });

        modelBuilder.Entity<VariantCost>(entity =>
        {
            entity.Property(e => e.VariantId).ValueGeneratedNever();
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Variant).WithOne(p => p.VariantCost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VariantCosts_ProductVariants");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasIndex(e => e.BranchId, "UX_Warehouses_DefaultPerBranch")
                .IsUnique()
                .HasFilter("([IsDefault]=(1) AND [IsActive]=(1))");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Branch).WithOne(p => p.Warehouse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouses_Branches");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
