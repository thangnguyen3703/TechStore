using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("VariantId", Name = "IX_StockOutDetails_VariantID")]
[Index("StockOutId", "VariantId", Name = "UQ_StockOutDetails_StockOut_Variant", IsUnique = true)]
public partial class StockOutDetail
{
    [Key]
    [Column("StockOutDetailID")]
    public int StockOutDetailId { get; set; }

    [Column("StockOutID")]
    public int StockOutId { get; set; }

    [Column("VariantID")]
    public int VariantId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? UnitCost { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? LineCost { get; set; }

    [ForeignKey("StockOutId")]
    [InverseProperty("StockOutDetails")]
    public virtual StockOut StockOut { get; set; } = null!;

    [ForeignKey("VariantId")]
    [InverseProperty("StockOutDetails")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
