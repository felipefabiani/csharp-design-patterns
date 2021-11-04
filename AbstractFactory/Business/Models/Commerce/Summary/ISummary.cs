namespace AbstractFactory.Business.Models.Commerce.Summary;
public interface ISummary
{
    string CreateOrderSummary(Order order);
    void Send();
}
