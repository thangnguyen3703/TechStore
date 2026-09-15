using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("VariantId", Name = "IX_StockDisposalDetails_VariantID")]
[Index("StockDisposalId", "VariantId", Name = "UQ_StockDisposalDetails_Disposal_Variant", IsUnique = true)]
public partial class StockDisposalDetail
{
    [Key]
    [Column("StockDisposalDetailID")]
    public int StockDisposalDetailId { get; set; }

    [Column("StockDisposalID")]
    public int StockDisposalId { get; set; }

    [Column("VariantID")]
    public int VariantId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? UnitCost { get; set; }

    [ForeignKey("StockDisposalId")]
    [InverseProperty("StockDisposalDetails")]
    public virtual StockDisposal StockDisposal { get; set; } = null!;

    [ForeignKey("VariantId")]
    [InverseProperty("StockDisposalDetails")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
