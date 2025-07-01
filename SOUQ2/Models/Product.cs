using System;
using System.Collections.Generic;

namespace SOUQ2.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Catid { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Cat { get; set; }

    public virtual ICollection<Productimage> Productimages { get; set; } = new List<Productimage>();
}
