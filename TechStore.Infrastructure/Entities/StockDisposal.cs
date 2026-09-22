using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("WarehouseId", Name = "IX_StockDisposals_WarehouseID")]
[Index("DisposalCode", Name = "UQ_StockDisposals_Code", IsUnique = true)]
public partial class StockDisposal
{
    [Key]
    [Column("StockDisposalID")]
    public int StockDisposalId { get; set; }

    [StringLength(50)]
    public string DisposalCode { get; set; } = null!;

    [Column("WarehouseID")]
    public int WarehouseId { get; set; }

    public DateTime DisposalDate { get; set; }

    [StringLength(500)]
    public string Reason { get; set; } = null!;

    [StringLength(1000)]
    public string? Note { get; set; }

    public byte Status { get; set; }

    [Column("CreatedByUserID")]
    [StringLength(450)]
    public string? CreatedByUserId { get; set; }

    [Column("ConfirmedByUserID")]
    [StringLength(450)]
    public string? ConfirmedByUserId { get; set; }

    [Column("CancelledByUserID")]
    [StringLength(450)]
    public string? CancelledByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ConfirmedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    [InverseProperty("StockDisposal")]
    public virtual ICollection<StockDisposalDetail> StockDisposalDetails { get; set; } = new List<StockDisposalDetail>();

    [ForeignKey("WarehouseId")]
    [InverseProperty("StockDisposals")]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
