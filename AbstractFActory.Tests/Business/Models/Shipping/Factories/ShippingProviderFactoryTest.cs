namespace AbstractFactory.Tests.Business.Models.Shipping.Factories;
public class ShippingProviderFactoryTest
{
    [Fact]
    public void FactoryShippingProvider_Autralia()
    {
        var sut = ShippingProviderFactory.CreateShippingProvider(Country.Australia);
        sut.ShouldBeOfType<AustraliaPostShippingProvider>();
    }

    [Fact]
    public void FactoryShippingProvider_Brazil()
    {
        var sut = ShippingProviderFactory.CreateShippingProvider(Country.Brazil);
        sut.ShouldBeOfType<BrazilPostalServiceShippingProvider>();
    }
}