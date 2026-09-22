using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[Index("Name", Name = "UQ_Attributes_Name", IsUnique = true)]
public partial class ProductAttribute
{
    [Key]
    [Column("AttributeID")]
    public int AttributeId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string? DisplayName { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("Attribute")]
    public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();

    [InverseProperty("Attribute")]
    public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; } = new List<CategoryAttribute>();

    [InverseProperty("Attribute")]
    public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; } = new List<ProductSpecification>();

    [InverseProperty("Attribute")]
    public virtual ICollection<VariantAttribute> VariantAttributes { get; set; } = new List<VariantAttribute>();
}
