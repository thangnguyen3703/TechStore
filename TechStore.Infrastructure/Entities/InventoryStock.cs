using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("VariantId", Name = "IX_InventoryStocks_VariantID")]
[Index("WarehouseId", "VariantId", Name = "UQ_InventoryStocks_Warehouse_Variant", IsUnique = true)]
public partial class InventoryStock
{
    [Key]
    [Column("InventoryStockID")]
    public int InventoryStockId { get; set; }

    [Column("WarehouseID")]
    public int WarehouseId { get; set; }

    [Column("VariantID")]
    public int VariantId { get; set; }

    public int QuantityOnHand { get; set; }

    public int ReservedQuantity { get; set; }

    public int? AvailableQuantity { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    [ForeignKey("VariantId")]
    [InverseProperty("InventoryStocks")]
    public virtual ProductVariant Variant { get; set; } = null!;

    [ForeignKey("WarehouseId")]
    [InverseProperty("InventoryStocks")]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
