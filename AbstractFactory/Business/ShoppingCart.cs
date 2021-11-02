using AbstractFactory.Business.Models.Commerce;
using AbstractFactory.Business.Models.Shipping;
using AbstractFactory.Business.Models.Shipping.Factories;

namespace AbstractFactory.Business;
public class ShoppingCart
{
    private readonly Order _order;
    private readonly ShippingProviderFactory _shippingProviderFactory;

    public ShoppingCart(Order order, ShippingProviderFactory shippingProviderFactory)
    {
        _order = order;
        _shippingProviderFactory = shippingProviderFactory;
    }

    public string Finalize()
    {
        var shippingProvider = _shippingProviderFactory.GetShippingProvider(_order.Sender.Country);

        _order.ShippingStatus = ShippingStatus.ReadyForShippment;

        return shippingProvider.GenerateShippingLabelFor(_order);

    }
}