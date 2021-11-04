using AbstractFactory.Business.Models.Commerce;
using AbstractFactory.Business.Models.Shipping;
using System.Diagnostics.CodeAnalysis;

namespace AbstractFactory.Business;
public class ShoppingCart
{
    private readonly Order _order;
    private readonly IPurchaseProviderFactory _purchaseProviderFactory;

    public ShoppingCart(
        [NotNull] Order order,
        [NotNull] IPurchaseProviderFactory purchaseProviderFactory)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
        _purchaseProviderFactory = purchaseProviderFactory ?? throw new ArgumentNullException(nameof(purchaseProviderFactory));
    }

    public string Finalize()
    {
        if (_order.ShippingStatus == ShippingStatus.ReadyForShippment)
        {
            throw new InvalidOperationException();
        }

        var shippingProvider = _purchaseProviderFactory.CreateShippingProvider(_order);
        var invoice = _purchaseProviderFactory.CreateInvoice(_order);
        invoice.GenerateInvoice();

        var summary = _purchaseProviderFactory.CreateSummary(_order);
        summary.Send();

        _order.ShippingStatus = ShippingStatus.ReadyForShippment;

        return shippingProvider.GenerateShippingLabelFor(_order);

    }
}