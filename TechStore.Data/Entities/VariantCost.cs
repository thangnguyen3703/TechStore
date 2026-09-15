using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

public partial class VariantCost
{
    [Key]
    [Column("VariantID")]
    public int VariantId { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal AverageUnitCost { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    [ForeignKey("VariantId")]
    [InverseProperty("VariantCost")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
