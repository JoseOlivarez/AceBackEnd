using System;
using System.Collections.Generic;

namespace AceBackEnd.Models;

public partial class FuelQuoteForm
{
    public int? GallonsRequested { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public decimal? PricePerGallon { get; set; }

    public string? DeliveryAddress { get; set; }

    public decimal? FuelQuoteTotal { get; set; }

    public decimal? Amount { get; set; }

    public int? ClientId { get; set; }

    public int Id { get; set; }

    public virtual Client? Client { get; set; }
}
