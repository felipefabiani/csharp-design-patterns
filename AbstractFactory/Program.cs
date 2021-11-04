using AbstractFactory.Business;
using AbstractFactory.Business.Models.Commerce;

namespace AbstractFactory;
public class Program
{
    static void Main()
    {
        var order = CreateOrder();
        var cart = GetShoppingCart(order);
        Finalize(cart);

        static Order CreateOrder()
        {
            Console.Write("Recipient Country: ");
            var recipientCountry = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Sender Country: ");
            var senderCountry = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Total Order Weight: ");
            var totalWeight = Convert.ToInt32(Console.ReadLine()?.Trim() ?? "0");

            var order = new Order
            {
                Recipient = new Address
                {
                    To = "Felipe Fabiani",
                    Country = Enum.Parse<Country>(recipientCountry, true)
                },

                Sender = new Address
                {
                    To = "Someone else",
                    Country = Enum.Parse<Country>(senderCountry, true)
                },

                TotalWeight = totalWeight,
            };

            order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m), 1);
            order.LineItems.Add(new Item("CONSULTING", "Building a website", 100m), 1);
            return order;
        }
        static ShoppingCart GetShoppingCart(Order order) => order switch
        {
            { Sender.Country: Country.Brazil } => new ShoppingCart(order, new BrazilPurchaseProviderFactory()),
            { Sender.Country: Country.Australia } => new ShoppingCart(order, new AustraliaPurchaseProviderFactory()),
            _ => throw new NotSupportedException("Sender country has no purchase provider")
        };
    }

    public static string Finalize(ShoppingCart cart)
    {
        var shippingLabel = cart.Finalize();

        Console.WriteLine(shippingLabel);
        return shippingLabel;
    }
}
