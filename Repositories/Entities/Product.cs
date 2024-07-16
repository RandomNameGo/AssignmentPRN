using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int SupplierId { get; set; }

    public int CategoryId { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    public int YearOfManufacture { get; set; }

    public string? Warranty { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
