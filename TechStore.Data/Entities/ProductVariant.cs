using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("ProductId", Name = "IX_ProductVariants_ProductID")]
[Index("Sku", Name = "UQ_ProductVariants_SKU", IsUnique = true)]
[Index("VariantId", "ProductId", Name = "UQ_ProductVariants_Variant_Product", IsUnique = true)]
public partial class ProductVariant
{
    [Key]
    [Column("VariantID")]
    public int VariantId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("SKU")]
    [StringLength(100)]
    public string Sku { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Variant")]
    public virtual ICollection<InventoryStock> InventoryStocks { get; set; } = new List<InventoryStock>();

    [InverseProperty("Variant")]
    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();

    [InverseProperty("Variant")]
    public virtual ICollection<PriceListItem> PriceListItems { get; set; } = new List<PriceListItem>();

    [ForeignKey("ProductId")]
    [InverseProperty("ProductVariants")]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty("ProductVariant")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Variant")]
    public virtual ICollection<StockDisposalDetail> StockDisposalDetails { get; set; } = new List<StockDisposalDetail>();

    [InverseProperty("Variant")]
    public virtual ICollection<StockInDetail> StockInDetails { get; set; } = new List<StockInDetail>();

    [InverseProperty("Variant")]
    public virtual ICollection<StockOutDetail> StockOutDetails { get; set; } = new List<StockOutDetail>();

    [InverseProperty("Variant")]
    public virtual ICollection<StockReturnDetail> StockReturnDetails { get; set; } = new List<StockReturnDetail>();

    [InverseProperty("Variant")]
    public virtual ICollection<StockTransferDetail> StockTransferDetails { get; set; } = new List<StockTransferDetail>();

    [InverseProperty("Variant")]
    public virtual ICollection<VariantAttribute> VariantAttributes { get; set; } = new List<VariantAttribute>();

    [InverseProperty("Variant")]
    public virtual VariantCost? VariantCost { get; set; }
}
