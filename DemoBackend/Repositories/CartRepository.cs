using DemoBackend.Data;
using DemoBackend.Interfaces;
using DemoBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoBackend.Repositories
{
    public class CartRepository : ICartInterface
    {
        private DataContext _context;

        public CartRepository(DataContext context) {
            _context = context;
        }
        public bool AddCartItem(Cart cart)
        {
            _context.Carts.Add(cart);
            return Save();
        }

        public bool CartItemExist(string userEmail,int userItemId)
        {
            return _context.Carts.Any(c => c.UserEmail == userEmail && c.ItemId == userItemId);
        }

        public bool CartItemExist(int cartId)
        {
            return _context.Carts.Any(c => c.CartId == cartId);
        }

        public bool DeleteAllCartItems(string userEmail)
        {
            var deleteItems = _context.Carts.Where(c=> c.UserEmail==userEmail).ToList();
            _context.RemoveRange(deleteItems);
            return Save();
        }

        public bool DeleteCartItem(Cart cart)
        {
            _context.Carts.Remove(cart);
            return Save();
        }

        public ICollection<Cart> GetAllCartItems(string userEmail)
        {
            return _context.Carts.OrderBy(c=>c.CartId).Where(c=>c.UserEmail == userEmail).ToList();
        }

        public Cart GetCartItem(int cartId)
        {
            return _context.Carts.Where(c => c.CartId == cartId).FirstOrDefault();
        }

        public int LastItem()
        {
            Cart? cartItem = _context.Carts.OrderBy(c => c.CartId).LastOrDefault();
            if (cartItem == null)
                return 0;
            return cartItem.ItemId;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCartItem(Cart cart)
        {
            _context.Carts.Update(cart);
            return Save();
        }
    }
}
