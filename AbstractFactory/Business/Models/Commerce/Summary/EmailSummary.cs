namespace AbstractFactory.Business.Models.Commerce.Summary;

public class EmailSummary : ISummary
{
    public string CreateOrderSummary(Order order) => $"This is an email summary";

    public void Send() { }
}
