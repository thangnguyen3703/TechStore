using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("PriceListId", "VariantId", "EffectiveFrom", Name = "IX_PriceListItems_PriceList_Variant")]
[Index("VariantId", Name = "IX_PriceListItems_VariantID")]
public partial class PriceListItem
{
    [Key]
    [Column("PriceListItemID")]
    public long PriceListItemId { get; set; }

    [Column("PriceListID")]
    public int PriceListId { get; set; }

    [Column("VariantID")]
    public int VariantId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal SellingPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CompareAtPrice { get; set; }

    public DateTime EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PriceListId")]
    [InverseProperty("PriceListItems")]
    public virtual PriceList PriceList { get; set; } = null!;

    [ForeignKey("VariantId")]
    [InverseProperty("PriceListItems")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
