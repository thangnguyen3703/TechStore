using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("FromWarehouseId", Name = "IX_StockTransfers_FromWarehouse")]
[Index("ToWarehouseId", Name = "IX_StockTransfers_ToWarehouse")]
[Index("TransferCode", Name = "UQ_StockTransfers_Code", IsUnique = true)]
public partial class StockTransfer
{
    [Key]
    [Column("StockTransferID")]
    public int StockTransferId { get; set; }

    [StringLength(50)]
    public string TransferCode { get; set; } = null!;

    [Column("FromWarehouseID")]
    public int FromWarehouseId { get; set; }

    [Column("ToWarehouseID")]
    public int ToWarehouseId { get; set; }

    public DateTime TransferDate { get; set; }

    public byte Status { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; }

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

    [ForeignKey("FromWarehouseId")]
    [InverseProperty("StockTransferFromWarehouses")]
    public virtual Warehouse FromWarehouse { get; set; } = null!;

    [InverseProperty("StockTransfer")]
    public virtual ICollection<StockTransferDetail> StockTransferDetails { get; set; } = new List<StockTransferDetail>();

    [ForeignKey("ToWarehouseId")]
    [InverseProperty("StockTransferToWarehouses")]
    public virtual Warehouse ToWarehouse { get; set; } = null!;
}
