using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("Slug", Name = "UQ_Categories_Slug", IsUnique = true)]
public partial class Category
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [Column("ParentCategoryID")]
    public int? ParentCategoryId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(150)]
    public string Slug { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; } = new List<CategoryAttribute>();

    [InverseProperty("ParentCategory")]
    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    [ForeignKey("ParentCategoryId")]
    [InverseProperty("InverseParentCategory")]
    public virtual Category? ParentCategory { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
