using System;
using System.Collections.Generic;

namespace SOUQ2.Models;

public partial class Productimage
{
    public int Id { get; set; }

    public int? Productid { get; set; }

    public string? Images { get; set; }

    public virtual Product? Product { get; set; }
}
