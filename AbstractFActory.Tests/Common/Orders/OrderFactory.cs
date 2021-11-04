namespace AbstractFactory.Tests.Common.Orders;
public abstract class OrderFactory
{
    protected abstract Order CreateOrder();

    public Order GetOrder()
    {
        var order = CreateOrder();

        order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m), 1);
        order.LineItems.Add(new Item("CONSULTING", "Building a website", 100m), 1);

        return order;
    }
}
