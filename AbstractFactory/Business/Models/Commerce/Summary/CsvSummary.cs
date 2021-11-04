namespace AbstractFactory.Business.Models.Commerce.Summary;

public class CsvSummary : ISummary
{
    public string CreateOrderSummary(Order order) => "This is a CSV summary";

    public void Send() { }
}