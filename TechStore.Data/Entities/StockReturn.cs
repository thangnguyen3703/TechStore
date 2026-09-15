using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("WarehouseId", Name = "IX_StockReturns_WarehouseID")]
[Index("ReturnCode", Name = "UQ_StockReturns_Code", IsUnique = true)]
public partial class StockReturn
{
    [Key]
    [Column("StockReturnID")]
    public int StockReturnId { get; set; }

    [StringLength(50)]
    public string ReturnCode { get; set; } = null!;

    [Column("WarehouseID")]
    public int WarehouseId { get; set; }

    [Column("SupplierID")]
    public int? SupplierId { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    public byte ReturnType { get; set; }

    public DateTime ReturnDate { get; set; }

    [StringLength(500)]
    public string? Reason { get; set; }

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

    [InverseProperty("StockReturn")]
    public virtual ICollection<StockReturnDetail> StockReturnDetails { get; set; } = new List<StockReturnDetail>();

    [ForeignKey("SupplierId")]
    [InverseProperty("StockReturns")]
    public virtual Supplier? Supplier { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("StockReturns")]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
