using AbstractFactory.Business.Models.Commerce;
using AbstractFactory.Business.Models.Commerce.Invoice;
using AbstractFactory.Business.Models.Commerce.Summary;
using AbstractFactory.Business.Models.Shipping;
using AbstractFactory.Business.Models.Shipping.Factories;

namespace AbstractFactory.Business;

public class BrazilPurchaseProviderFactory : IPurchaseProviderFactory
{
    public IInvoice CreateInvoice(Order order) => order.Recipient.Country != order.Sender.Country ?
            new NoVatInvoice() :
            new VatInvoice();
    public ShippingProvider CreateShippingProvider(Order order)
    {
        ShippingProviderFactory shippingProviderFactory =
            order.Sender.Country != order.Recipient.Country ?
            new GlobalExpressShippingProviderFactory() :
            new StandardShippingProviderFactory();

        return shippingProviderFactory.GetShippingProvider(order.Sender.Country);
    }

    public ISummary CreateSummary(Order order) => new EmailSummary();
}