namespace AbstractFactory.Tests.Common.Orders;
public class InternationalOrderFactory : OrderFactory
{
    protected override Order CreateOrder()
    {
        var order = new Order
        {
            Recipient = new Address
            {
                To = "Felipe Fabiani",
                Country = Country.Brazil
            },

            Sender = new Address
            {
                To = "Someone else",
                Country = Country.Denmark
            },

            TotalWeight = 100
        };

        return order;
    }
}