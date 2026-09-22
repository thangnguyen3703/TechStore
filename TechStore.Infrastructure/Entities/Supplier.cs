using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("Code", Name = "UQ_Suppliers_Code", IsUnique = true)]
public partial class Supplier
{
    [Key]
    [Column("SupplierID")]
    public int SupplierId { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string? TaxCode { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(200)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<StockIn> StockIns { get; set; } = new List<StockIn>();

    [InverseProperty("Supplier")]
    public virtual ICollection<StockReturn> StockReturns { get; set; } = new List<StockReturn>();
}
