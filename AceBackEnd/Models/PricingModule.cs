using System;
using System.Collections.Generic;

namespace AceBackEnd.Models;

public partial class PricingModule
{
    public int PricingModuleId { get; set; }

    public string? Location { get; set; }

    public int? GallonsRequested { get; set; }

    public decimal? CurrentPricePerGallon { get; set; }

    public decimal? CompanyProfitFactor { get; set; }

    public decimal? LocationFactor { get; set; }

    public decimal? RateHistoryFactor { get; set; }

    public decimal? GallonsRequestedFactor { get; set; }

    public decimal? Margin { get; set; }

    public decimal? SuggestedPricePerGallon { get; set; }

    public decimal? TotalAmountDue { get; set; }

    public int? ClientId { get; set; }

    public virtual Client? Client { get; set; }
}
