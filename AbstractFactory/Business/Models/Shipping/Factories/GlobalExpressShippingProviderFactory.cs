using AbstractFactory.Business.Models.Commerce;

namespace AbstractFactory.Business.Models.Shipping.Factories;

public class GlobalExpressShippingProviderFactory : ShippingProviderFactory
{
    public override ShippingProvider CreateShippingProvider(Country country)
        => new GlobalExpressShippingProvider();
}