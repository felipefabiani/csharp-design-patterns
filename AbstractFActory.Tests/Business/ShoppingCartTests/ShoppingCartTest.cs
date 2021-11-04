namespace AbstractFactory.Tests.Business.ShoppingCartTests
{
    public class ShoppingCartTest
    {
        [Fact]
        public void FinalizeOrderWithoutPurchaseProvider_ThrowsException()
        {
            var orderFactory = new StandardOrderFactory();

            var order = orderFactory.GetOrder();

            Should.Throw<ArgumentNullException>(() => new ShoppingCart(order, null!))
                .Message.ShouldBe("Value cannot be null. (Parameter 'purchaseProviderFactory')");
        }

        [Fact]
        public void FinalizeOrderWithSwedenPurchaseProvider_GeneratesShippingLabel()
        {
            var orderFactory = new StandardOrderFactory();

            var order = orderFactory.GetOrder();

            var purchaseProvider = new BrazilPurchaseProviderFactory();

            var cart = new ShoppingCart(order, purchaseProvider);

            var label = cart.Finalize();

            label.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public void FinalizeAlreadyFinalizedOrder_ThrowsInvalidOperationException()
        {
            var cart = CreateShoppingCart();
            cart.Finalize();

            Should.Throw<InvalidOperationException>(() => cart.Finalize());

            static ShoppingCart CreateShoppingCart(IPurchaseProviderFactory purchaseProviderFactory = null!)
            {
                var orderFactory = new StandardOrderFactory();

                var order = orderFactory.GetOrder();

                var provider = purchaseProviderFactory ?? new BrazilPurchaseProviderFactory();

                var cart = new ShoppingCart(order, provider);

                return cart;
            }
        }
    }
}
