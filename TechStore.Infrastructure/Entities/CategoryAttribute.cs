using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Infrastructure.Entities;

[PrimaryKey("CategoryId", "AttributeId")]
[Index("AttributeId", Name = "IX_CategoryAttributes_AttributeID")]
public partial class CategoryAttribute
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [Key]
    [Column("AttributeID")]
    public int AttributeId { get; set; }

    public byte AttributeScope { get; set; }

    public bool IsRequired { get; set; }

    public int DisplayOrder { get; set; }

    [ForeignKey("AttributeId")]
    [InverseProperty("CategoryAttributes")]
    public virtual ProductAttribute Attribute { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("CategoryAttributes")]
    public virtual Category Category { get; set; } = null!;
}
