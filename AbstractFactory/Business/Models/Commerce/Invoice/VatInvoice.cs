using System.Text;

namespace AbstractFactory.Business.Models.Commerce.Invoice;

public class VatInvoice : IInvoice
{
    public byte[] GenerateInvoice() => Encoding
        .Default
        .GetBytes("Hello world from VAT Invoice");
}
