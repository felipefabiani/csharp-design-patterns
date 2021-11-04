using System.Text;

namespace AbstractFactory.Business.Models.Commerce.Invoice;

public class GSTInvoice : IInvoice
{
    public byte[] GenerateInvoice() => Encoding
        .Default
        .GetBytes("Hello world from GST Invoice");
}
