using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
