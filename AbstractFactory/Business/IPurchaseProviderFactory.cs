using AbstractFactory.Business.Models.Commerce;
using AbstractFactory.Business.Models.Commerce.Invoice;
using AbstractFactory.Business.Models.Commerce.Summary;
using AbstractFactory.Business.Models.Shipping;

namespace AbstractFactory.Business;

public interface IPurchaseProviderFactory
{
    ShippingProvider CreateShippingProvider(Order order);
    IInvoice CreateInvoice(Order order);
    ISummary CreateSummary(Order order);
}