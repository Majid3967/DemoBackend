using DemoBackend.Models;

namespace DemoBackend.Interfaces
{
    public interface ICartInterface
    {
        public ICollection<Cart> GetAllCartItems(string userEmail);
        public bool AddCartItem(Cart cart);
        public bool UpdateCartItem(Cart cart);
        public Cart GetCartItem(int cartId);
        public bool DeleteCartItem(Cart cart);
        public bool DeleteAllCartItems(string userEmail);
        public bool CartItemExist(string userEmail,int userItemId);
        public bool CartItemExist(int cartId);
        public int LastItem();
        public bool Save();
    }
}
