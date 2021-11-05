using System.Reflection;

namespace AbstractFactory.Business;

public class PurchaseProviderFactoryProvider
{
    private static List<Type> _factories = Assembly
            .GetAssembly(typeof(PurchaseProviderFactoryProvider))!
            .GetTypes()
            .Where(t =>
            {
                // Console.WriteLine("loading factories");
                return typeof(IPurchaseProviderFactory).IsAssignableFrom(t);
            }).ToList();
    
    public IPurchaseProviderFactory CreateFactoryFor(string name)
    {
        var factory = _factories
            .Single(x => x.Name.ToLowerInvariant()
            .Contains(name.ToLowerInvariant()));

        var instance = 
            (Activator.CreateInstance(factory) as IPurchaseProviderFactory)
            ?? throw new InvalidOperationException("Sender country has no purchase provider");
               
        return instance;
     }
}