using AbstractFactory.Business.Models.Commerce;

namespace AbstractFactory.Business.Models.Shipping;
public abstract class ShippingProvider
{
    public ShippingCostCalculator? ShippingCostCalculator { get; protected set; }
    public CustomsHandlingOptions? CustomsHandlingOptions { get; protected set; }
    public InsuranceOptions? InsuranceOptions { get; protected set; }

    public bool RequireSignature { get; set; }

    public abstract string GenerateShippingLabelFor(Order order);
}

public class InsuranceOptions
{
    public bool ProviderHasInsurance { get; set; }
    public bool ProviderHasExtendedInsurance { get; set; }
    public bool ProviderRequiresReturnOnDamange { get; set; }
}

public class CustomsHandlingOptions
{
    public TaxOptions TaxOptions { get; set; }
}

public class ShippingCostCalculator
{
    private readonly decimal internationalShippingFee;
    private readonly decimal extraWeightFee;

    public ShippingType ShippingType { get; set; }

    public ShippingCostCalculator(decimal internationalShippingFee,
        decimal extraWeightFee,
        ShippingType shippingType = ShippingType.Standard)
    {
        this.internationalShippingFee = internationalShippingFee;
        this.extraWeightFee = extraWeightFee;

        ShippingType = shippingType;
    }

    public decimal CalculateFor(
        Country destinationCountry,
        Country originCountry,
        decimal weight)
    {
        var total = 10m; // Default shipping cost $10


        // International shipping
        if (destinationCountry != originCountry)
        {
            total += internationalShippingFee;
        }

        // Over 5kg
        if (weight > 5m)
        {
            total += extraWeightFee;
        }

        total += ShippingType switch
        {
            ShippingType.Express => 20m,
            ShippingType.NextDay => 50m,
            ShippingType.Standard => 0m,
            _ => 0m
        };

        return total;
    }
}

public enum TaxOptions
{
    PrePaid,
    DutyFree,
    PayOnArrival
}

public enum ShippingType
{
    Standard,
    Express,
    NextDay
}

public enum ShippingStatus
{
    WaitingForPayment,
    ReadyForShippment,
    Shipped
}