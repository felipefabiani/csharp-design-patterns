using AbstractFactory.Business.Models.Commerce;
using AbstractFactory.Business.Models.Shipping;
using AbstractFactory.Business.Models.Shipping.Factories;

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
        var shippingProvider = ShippingProviderFactory.CreateShippingProvider(_order.Sender.Country);

        _order.ShippingStatus = ShippingStatus.ReadyForShippment;

        return shippingProvider.GenerateShippingLabelFor(_order);

    }
}