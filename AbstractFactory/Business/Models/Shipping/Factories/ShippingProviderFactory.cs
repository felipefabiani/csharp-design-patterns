using AbstractFactory.Business.Models.Commerce;
namespace AbstractFactory.Business.Models.Shipping.Factories;
public abstract class ShippingProviderFactory
{
    public abstract ShippingProvider CreateShippingProvider(Country country);
    public ShippingProvider GetShippingProvider(Country country)
    {
        var provider = CreateShippingProvider(country);

        if (country == Country.Brazil &&
            provider.InsuranceOptions?.ProviderHasInsurance == true)
        {
            provider.RequireSignature = false;
        }

        return provider;
    }
}
