using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("StockInDate", Name = "IX_StockIns_Date")]
[Index("SupplierId", Name = "IX_StockIns_SupplierID")]
[Index("WarehouseId", Name = "IX_StockIns_WarehouseID")]
[Index("StockInCode", Name = "UQ_StockIns_Code", IsUnique = true)]
public partial class StockIn
{
    [Key]
    [Column("StockInID")]
    public int StockInId { get; set; }

    [StringLength(50)]
    public string StockInCode { get; set; } = null!;

    [Column("SupplierID")]
    public int SupplierId { get; set; }

    [Column("WarehouseID")]
    public int WarehouseId { get; set; }

    public DateTime StockInDate { get; set; }

    public byte Status { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; }

    [Column("CreatedByUserID")]
    public int? CreatedByUserId { get; set; }

    [Column("ConfirmedByUserID")]
    public int? ConfirmedByUserId { get; set; }

    [Column("CancelledByUserID")]
    public int? CancelledByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ConfirmedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    [InverseProperty("StockIn")]
    public virtual ICollection<StockInDetail> StockInDetails { get; set; } = new List<StockInDetail>();

    [ForeignKey("SupplierId")]
    [InverseProperty("StockIns")]
    public virtual Supplier Supplier { get; set; } = null!;

    [ForeignKey("WarehouseId")]
    [InverseProperty("StockIns")]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
