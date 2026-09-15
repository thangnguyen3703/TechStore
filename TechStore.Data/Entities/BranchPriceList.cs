using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("BranchId", Name = "IX_BranchPriceLists_BranchID")]
[Index("PriceListId", Name = "IX_BranchPriceLists_PriceListID")]
public partial class BranchPriceList
{
    [Key]
    [Column("BranchPriceListID")]
    public int BranchPriceListId { get; set; }

    [Column("BranchID")]
    public int BranchId { get; set; }

    [Column("PriceListID")]
    public int PriceListId { get; set; }

    public bool IsDefault { get; set; }

    public DateTime EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("BranchPriceList")]
    public virtual Branch Branch { get; set; } = null!;

    [ForeignKey("PriceListId")]
    [InverseProperty("BranchPriceLists")]
    public virtual PriceList PriceList { get; set; } = null!;
}
