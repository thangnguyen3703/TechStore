using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("AttributeId", Name = "IX_ProductSpecifications_AttributeID")]
[Index("ProductId", "AttributeId", Name = "UQ_ProductSpecifications_Product_Attribute", IsUnique = true)]
public partial class ProductSpecification
{
    [Key]
    [Column("ProductSpecificationID")]
    public int ProductSpecificationId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("AttributeID")]
    public int AttributeId { get; set; }

    [StringLength(500)]
    public string Value { get; set; } = null!;

    public int DisplayOrder { get; set; }

    [ForeignKey("AttributeId")]
    [InverseProperty("ProductSpecifications")]
    public virtual ProductAttribute Attribute { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductSpecifications")]
    public virtual Product Product { get; set; } = null!;
}
