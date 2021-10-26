using AbstractFactory.Business.Models.Commerce;
using AbstractFactory.Business.Models.Shipping;

namespace AbstractFactory.Business;
public class ShoppingCart
{
    private readonly Order _order;

    public ShoppingCart(Order order)
    {
        _order = order;
    }

    public string Finalize()
    {
        var shippingProvider = CreateShippingProvider();

        _order.ShippingStatus = ShippingStatus.ReadyForShippment;

        return shippingProvider.GenerateShippingLabelFor(_order);

        ShippingProvider CreateShippingProvider() => _order.Sender switch
        {
            { Country: Country.Australia } => AustraliaPostShippingProvider(),
            { Country: Country.Brazil } => BrazilPostalServiceShippingProvider(),
            _ => throw new NotSupportedException("No shipping provider found for origin country")
        };

        static ShippingProvider AustraliaPostShippingProvider()
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
        static ShippingProvider BrazilPostalServiceShippingProvider()
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
}