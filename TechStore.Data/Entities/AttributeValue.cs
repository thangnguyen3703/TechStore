using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Data.Entities;

[Index("AttributeId", Name = "IX_AttributeValues_AttributeID")]
[Index("AttributeId", "Value", Name = "UQ_AttributeValues_Attribute_Value", IsUnique = true)]
[Index("AttributeValueId", "AttributeId", Name = "UQ_AttributeValues_ID_Attribute", IsUnique = true)]
public partial class AttributeValue
{
    [Key]
    [Column("AttributeValueID")]
    public int AttributeValueId { get; set; }

    [Column("AttributeID")]
    public int AttributeId { get; set; }

    [StringLength(100)]
    public string Value { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("AttributeId")]
    [InverseProperty("AttributeValues")]
    public virtual ProductAttribute Attribute { get; set; } = null!;

    [InverseProperty("AttributeValue")]
    public virtual ICollection<VariantAttribute> VariantAttributes { get; set; } = new List<VariantAttribute>();
}
