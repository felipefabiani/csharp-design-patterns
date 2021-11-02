using AbstractFactory.Business.Models.Commerce;

namespace AbstractFactory.Business.Models.Shipping.Factories;
public class StandardShippingProviderFactory : ShippingProviderFactory
{
    public override ShippingProvider CreateShippingProvider(Country country) => country switch
    {
        Country.Australia => AustraliaPostShippingProvider(),
        Country.Brazil => BrazilPostalServiceShippingProvider(),
        _ => throw new NotSupportedException("No shipping provider found for origin country")
    };

    private static ShippingProvider AustraliaPostShippingProvider()
    {
        ShippingProvider shippingProvider;
        var shippingCostCalculator = new ShippingCostCalculator(
            internationalShippingFee: 250,
            extraWeightFee: 500)
        {
            ShippingType = ShippingType.Standard
        };

        var customsHandlingOptions = new CustomsHandlingOptions
        {
            TaxOptions = TaxOptions.PrePaid
        };

        var insuranceOptions = new InsuranceOptions
        {
            ProviderHasInsurance = false,
            ProviderHasExtendedInsurance = false,
            ProviderRequiresReturnOnDamange = false
        };

        shippingProvider = new AustraliaPostShippingProvider("CLIENT_ID",
            "SECRET",
            shippingCostCalculator,
            customsHandlingOptions,
            insuranceOptions);
        return shippingProvider;
    }
    private static ShippingProvider BrazilPostalServiceShippingProvider()
    {
        ShippingProvider shippingProvider;
        var shippingCostCalculator = new ShippingCostCalculator(
            internationalShippingFee: 50,
            extraWeightFee: 100)
        {
            ShippingType = ShippingType.Express
        };

        var customsHandlingOptions = new CustomsHandlingOptions
        {
            TaxOptions = TaxOptions.PayOnArrival
        };

        var insuranceOptions = new InsuranceOptions
        {
            ProviderHasInsurance = true,
            ProviderHasExtendedInsurance = false,
            ProviderRequiresReturnOnDamange = false
        };

        shippingProvider = new BrazilPostalServiceShippingProvider("API_KEY",
            shippingCostCalculator,
            customsHandlingOptions,
            insuranceOptions);
        return shippingProvider;
    }
}
