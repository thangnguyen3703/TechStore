using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("Code", Name = "UQ_Branches_Code", IsUnique = true)]
public partial class Branch
{
    [Key]
    [Column("BranchID")]
    public int BranchId { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(150)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string? Address { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(200)]
    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Branch")]
    public virtual BranchPriceList? BranchPriceList { get; set; }

    [InverseProperty("Branch")]
    public virtual Warehouse? Warehouse { get; set; }
}
