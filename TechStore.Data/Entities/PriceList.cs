using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("Code", Name = "UQ_PriceLists_Code", IsUnique = true)]
public partial class PriceList
{
    [Key]
    [Column("PriceListID")]
    public int PriceListId { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(150)]
    public string Name { get; set; } = null!;

    public byte PriceType { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("PriceList")]
    public virtual ICollection<BranchPriceList> BranchPriceLists { get; set; } = new List<BranchPriceList>();

    [InverseProperty("PriceList")]
    public virtual ICollection<PriceListItem> PriceListItems { get; set; } = new List<PriceListItem>();
}
