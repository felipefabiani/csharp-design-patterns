using System.Text;

namespace AbstractFactory.Business.Models.Commerce.Invoice;

public class NoVatInvoice : IInvoice
{
    public byte[] GenerateInvoice() => Encoding
        .Default
        .GetBytes("Hello world from NO VAT Invoice");
}