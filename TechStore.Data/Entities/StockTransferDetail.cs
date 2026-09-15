using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("VariantId", Name = "IX_StockTransferDetails_VariantID")]
[Index("StockTransferId", "VariantId", Name = "UQ_StockTransferDetails_Transfer_Variant", IsUnique = true)]
public partial class StockTransferDetail
{
    [Key]
    [Column("StockTransferDetailID")]
    public int StockTransferDetailId { get; set; }

    [Column("StockTransferID")]
    public int StockTransferId { get; set; }

    [Column("VariantID")]
    public int VariantId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? UnitCost { get; set; }

    [ForeignKey("StockTransferId")]
    [InverseProperty("StockTransferDetails")]
    public virtual StockTransfer StockTransfer { get; set; } = null!;

    [ForeignKey("VariantId")]
    [InverseProperty("StockTransferDetails")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
