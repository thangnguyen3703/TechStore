using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("VariantId", Name = "IX_StockReturnDetails_VariantID")]
public partial class StockReturnDetail
{
    [Key]
    [Column("StockReturnDetailID")]
    public int StockReturnDetailId { get; set; }

    [Column("StockReturnID")]
    public int StockReturnId { get; set; }

    [Column("VariantID")]
    public int VariantId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? UnitCost { get; set; }

    [Column("SourceOrderDetailID")]
    public int? SourceOrderDetailId { get; set; }

    [Column("SourceStockInDetailID")]
    public int? SourceStockInDetailId { get; set; }

    [ForeignKey("SourceStockInDetailId")]
    [InverseProperty("StockReturnDetails")]
    public virtual StockInDetail? SourceStockInDetail { get; set; }

    [ForeignKey("StockReturnId")]
    [InverseProperty("StockReturnDetails")]
    public virtual StockReturn StockReturn { get; set; } = null!;

    [ForeignKey("VariantId")]
    [InverseProperty("StockReturnDetails")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
