using AbstractFactory.Business.Models.Shipping;

namespace AbstractFactory.Business.Models.Commerce;
public class Order
{
    public Dictionary<Item, int> LineItems { get; } = new Dictionary<Item, int>();

    public IList<Payment> SelectedPayments { get; } = new List<Payment>();

    public IList<Payment> FinalizedPayments { get; } = new List<Payment>();

    public decimal AmountDue => LineItems.Sum(item => item.Key.Price * item.Value) - FinalizedPayments.Sum(payment => payment.Amount);

    public decimal Total => LineItems.Sum(item => item.Key.Price * item.Value);

    public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.WaitingForPayment;

    public Address Recipient { get; set; } = new();

    public Address Sender { get; set; } = new();

    public decimal TotalWeight { get; set; }
}
