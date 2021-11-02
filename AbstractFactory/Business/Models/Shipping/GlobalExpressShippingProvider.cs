using AbstractFactory.Business.Models.Commerce;

namespace AbstractFactory.Business.Models.Shipping;
public class GlobalExpressShippingProvider : ShippingProvider
{
    public override string GenerateShippingLabelFor(Order order) => "GLOBAL-EXPRESS";
}