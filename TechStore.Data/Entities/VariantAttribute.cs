using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[PrimaryKey("VariantId", "AttributeId")]
[Index("AttributeValueId", Name = "IX_VariantAttributes_AttributeValueID")]
public partial class VariantAttribute
{
    [Key]
    [Column("VariantID")]
    public int VariantId { get; set; }

    [Key]
    [Column("AttributeID")]
    public int AttributeId { get; set; }

    [Column("AttributeValueID")]
    public int AttributeValueId { get; set; }

    [ForeignKey("AttributeId")]
    [InverseProperty("VariantAttributes")]
    public virtual ProductAttribute Attribute { get; set; } = null!;

    [ForeignKey("AttributeValueId, AttributeId")]
    [InverseProperty("VariantAttributes")]
    public virtual AttributeValue AttributeValue { get; set; } = null!;

    [ForeignKey("VariantId")]
    [InverseProperty("VariantAttributes")]
    public virtual ProductVariant Variant { get; set; } = null!;
}
