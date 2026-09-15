using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("ProductId", Name = "IX_ProductImages_ProductID")]
public partial class ProductImage
{
    [Key]
    [Column("ImageID")]
    public int ImageId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("VariantID")]
    public int? VariantId { get; set; }

    [StringLength(500)]
    public string ImageUrl { get; set; } = null!;

    [StringLength(250)]
    public string? AltText { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsPrimary { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductImages")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("VariantId, ProductId")]
    [InverseProperty("ProductImages")]
    public virtual ProductVariant? ProductVariant { get; set; }
}
