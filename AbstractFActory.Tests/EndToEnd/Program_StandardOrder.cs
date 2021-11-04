namespace AbstractFactory.Tests.EndToEnd;
public class Program_StandardOrder
{
    [Fact]
    public void FinalizeOrderWithSwedenPurchaseProvider_GeneratesShippingLabel()
    {
        var orderFactory = new StandardOrderFactory();
        var order = orderFactory.GetOrder();
        var label = Program.Finalize(new ShoppingCart(order, new BrazilPurchaseProviderFactory()));

        label.ShouldNotBeNullOrWhiteSpace();
        label.ShouldStartWith("Shipping Id:");
    }
}
