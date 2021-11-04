using AbstractFactory.Business.Models.Commerce;
using AbstractFactory.Business.Models.Commerce.Invoice;
using AbstractFactory.Business.Models.Commerce.Summary;
using AbstractFactory.Business.Models.Shipping;
using AbstractFactory.Business.Models.Shipping.Factories;

namespace AbstractFactory.Business;

public class AustraliaPurchaseProviderFactory : IPurchaseProviderFactory
{
    public IInvoice CreateInvoice(Order order) => new GSTInvoice();

    public ShippingProvider CreateShippingProvider(Order order) =>
        new StandardShippingProviderFactory()
        .CreateShippingProvider(order.Sender.Country);

    public ISummary CreateSummary(Order order) => new CsvSummary();
}
