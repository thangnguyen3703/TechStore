using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("BrandId", Name = "IX_Products_BrandID")]
[Index("CategoryId", Name = "IX_Products_CategoryID")]
[Index("Name", Name = "IX_Products_Name")]
[Index("Slug", Name = "UQ_Products_Slug", IsUnique = true)]
public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [Column("BrandID")]
    public int? BrandId { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    public string Slug { get; set; } = null!;

    [StringLength(500)]
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    [StringLength(500)]
    public string? Thumbnail { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("Products")]
    public virtual Brand? Brand { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; } = new List<ProductSpecification>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
