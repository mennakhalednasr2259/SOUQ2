using System;
using System.Collections.Generic;

namespace SOUQ2.Models;

public partial class Cart
{
    public int Id { get; set; }

    public string? Userid { get; set; }

    public int? Productid { get; set; }

    public int? Qty { get; set; }

    public virtual Product? Product { get; set; }
}
