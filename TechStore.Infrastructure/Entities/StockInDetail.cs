using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("VariantId", Name = "IX_StockInDetails_VariantID")]
[Index("StockInId", "VariantId", Name = "UQ_StockInDetails_StockIn_Variant", IsUnique = true)]
public partial class StockInDetail
{
    [Key]
    [Column("StockInDetailID")]
    public int StockInDetailId { get; set; }

    [Column("StockInID")]
    public int StockInId { get; set; }

    [Column("VariantID")]
    public int VariantId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitCost { get; set; }

    [Column(TypeName = "decimal(19, 2)")]
    public decimal? LineTotal { get; set; }

    [ForeignKey("StockInId")]
    [InverseProperty("StockInDetails")]
    public virtual StockIn StockIn { get; set; } = null!;

    [InverseProperty("SourceStockInDetail")]
    public virtual ICollection<StockReturnDetail> StockReturnDetails { get; set; } = new List<StockReturnDetail>();

    [ForeignKey("VariantId")]
    [InverseProperty("StockInDetails")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
