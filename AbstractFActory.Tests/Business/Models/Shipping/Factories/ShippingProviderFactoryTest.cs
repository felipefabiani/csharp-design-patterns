namespace AbstractFactory.Tests.Business.Models.Shipping.Factories;
public class ShippingProviderFactoryTest
{
    private readonly ShippingProviderFactory _sut = new StandardShippingProviderFactory();

    [Fact]
    public void FactoryShippingProvider_Autralia()
    {
        var sut = _sut.CreateShippingProvider(Country.Australia);
        sut.ShouldBeOfType<AustraliaPostShippingProvider>();
    }

    [Fact]
    public void FactoryShippingProvider_Brazil()
    {
        var sut = _sut.CreateShippingProvider(Country.Brazil);
        sut.ShouldBeOfType<BrazilPostalServiceShippingProvider>();
    }
}