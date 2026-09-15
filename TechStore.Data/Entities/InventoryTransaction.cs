using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("WarehouseId", "VariantId", "CreatedAt", Name = "IX_InventoryTransactions_Warehouse_Variant")]
public partial class InventoryTransaction
{
    [Key]
    [Column("InventoryTransactionID")]
    public long InventoryTransactionId { get; set; }

    [Column("WarehouseID")]
    public int WarehouseId { get; set; }

    [Column("VariantID")]
    public int VariantId { get; set; }

    public byte TransactionType { get; set; }

    public int QuantityChange { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? UnitCost { get; set; }

    [Column(TypeName = "decimal(18, 4)")]
    public decimal? AverageUnitCostAfter { get; set; }

    public int BalanceAfter { get; set; }

    [StringLength(30)]
    public string? ReferenceType { get; set; }

    [Column("ReferenceID")]
    public int? ReferenceId { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }

    [Column("CreatedByUserID")]
    [StringLength(450)]
    public string? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("VariantId")]
    [InverseProperty("InventoryTransactions")]
    public virtual ProductVariant Variant { get; set; } = null!;

    [ForeignKey("WarehouseId")]
    [InverseProperty("InventoryTransactions")]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
