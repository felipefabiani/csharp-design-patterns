using System;

namespace AbstractFactory.Tests.EndToEnd;
public class Program_StandardOrder
{
    [Fact]
    public void FinalizeOrderInternationalOrder_ThrowsException()
    {
        var orderFactory = new InternationalOrderFactory();
        var order = orderFactory.GetOrder();

        Should.Throw<NotSupportedException>(() => 
            Program.Finalize(new ShoppingCart(order)))
            .Message
            .ShouldBe("No shipping provider found for origin country");
    }   

    [Fact]
    public void FinalizeOrderWithSwedenPurchaseProvider_GeneratesShippingLabel()
    {
        var orderFactory = new StandardOrderFactory();
        var order = orderFactory.GetOrder();
        var label = Program.Finalize(new ShoppingCart(order));

        label.ShouldNotBeNullOrWhiteSpace();
        label.ShouldStartWith("Shipping Id:");
    }
}
