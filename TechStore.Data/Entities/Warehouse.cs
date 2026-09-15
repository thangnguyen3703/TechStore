using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("BranchId", Name = "IX_Warehouses_BranchID")]
[Index("Code", Name = "UQ_Warehouses_Code", IsUnique = true)]
public partial class Warehouse
{
    [Key]
    [Column("WarehouseID")]
    public int WarehouseId { get; set; }

    [Column("BranchID")]
    public int BranchId { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(150)]
    public string Name { get; set; } = null!;

    public byte WarehouseType { get; set; }

    public bool IsDefault { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Warehouse")]
    public virtual Branch Branch { get; set; } = null!;

    [InverseProperty("Warehouse")]
    public virtual ICollection<InventoryStock> InventoryStocks { get; set; } = new List<InventoryStock>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<StockDisposal> StockDisposals { get; set; } = new List<StockDisposal>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<StockIn> StockIns { get; set; } = new List<StockIn>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<StockOut> StockOuts { get; set; } = new List<StockOut>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<StockReturn> StockReturns { get; set; } = new List<StockReturn>();

    [InverseProperty("FromWarehouse")]
    public virtual ICollection<StockTransfer> StockTransferFromWarehouses { get; set; } = new List<StockTransfer>();

    [InverseProperty("ToWarehouse")]
    public virtual ICollection<StockTransfer> StockTransferToWarehouses { get; set; } = new List<StockTransfer>();
}
