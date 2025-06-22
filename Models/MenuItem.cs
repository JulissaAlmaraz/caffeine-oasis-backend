using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CaffeineOasis.API.Models;

[Table("menu_items")]
public partial class MenuItem
{
    [Key]
    [Column("item_id")]
    public int ItemId { get; set; }

    [Column("item_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string ItemName { get; set; } = null!;

    [Column("description")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column("category")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Category { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
