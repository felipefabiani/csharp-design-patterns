using AbstractFactory.Business.Models.Commerce;
using System.Diagnostics.CodeAnalysis;

namespace AbstractFactory.Business.Models.Shipping;
public class AustraliaPostShippingProvider : ShippingProvider
{
    private readonly string clientId;
    private readonly string secret;

    public AustraliaPostShippingProvider(
        string clientId,
        string secret,
        ShippingCostCalculator shippingCostCalculator,
        CustomsHandlingOptions customsHandlingOptions,
        InsuranceOptions insuranceOptions)
    {
        this.clientId = clientId;
        this.secret = secret;

        ShippingCostCalculator = shippingCostCalculator;
        CustomsHandlingOptions = customsHandlingOptions;
        InsuranceOptions = insuranceOptions;
    }

    public override string GenerateShippingLabelFor([NotNull] Order order)
    {
        var shippingId = GetShippingId();

        if (order.Recipient.Country != order.Sender.Country)
        {
            throw new NotSupportedException("International shipping not supported");
        }

        var shippingCost = ShippingCostCalculator?.CalculateFor(
            order.Recipient.Country,
            order.Sender.Country,
            order.TotalWeight);

        return
            $"Shipping Id: {shippingId} {Environment.NewLine}" +
            $"To: {order.Recipient?.To} {Environment.NewLine}" +
            $"Order total: {order.Total} {Environment.NewLine}" +
            $"Tax: {CustomsHandlingOptions?.TaxOptions} {Environment.NewLine}" +
            $"Shipping Cost: {shippingCost}";

        static string GetShippingId() => $"AUS-{Guid.NewGuid()}";
    }

}