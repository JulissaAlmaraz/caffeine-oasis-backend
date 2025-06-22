using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CaffeineOasis.API.Models;

[PrimaryKey("OrderId", "ItemId")]
[Table("order_items")]
public partial class OrderItem
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Key]
    [Column("item_id")]
    public int ItemId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("OrderItems")]
    public virtual MenuItem Item { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;
}
