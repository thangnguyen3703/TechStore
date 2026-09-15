using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("StockOutDate", Name = "IX_StockOuts_Date")]
[Index("WarehouseId", Name = "IX_StockOuts_WarehouseID")]
[Index("StockOutCode", Name = "UQ_StockOuts_Code", IsUnique = true)]
public partial class StockOut
{
    [Key]
    [Column("StockOutID")]
    public int StockOutId { get; set; }

    [StringLength(50)]
    public string StockOutCode { get; set; } = null!;

    [Column("WarehouseID")]
    public int WarehouseId { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    public DateTime StockOutDate { get; set; }

    public byte StockOutType { get; set; }

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

    [InverseProperty("StockOut")]
    public virtual ICollection<StockOutDetail> StockOutDetails { get; set; } = new List<StockOutDetail>();

    [ForeignKey("WarehouseId")]
    [InverseProperty("StockOuts")]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
