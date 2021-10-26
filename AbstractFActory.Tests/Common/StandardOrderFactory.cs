namespace AbstractFactory.Tests.Common;
public class StandardOrderFactory : OrderFactory
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
                Country = Country.Brazil
            },

            TotalWeight = 100
        };

        return order;
    }
}
