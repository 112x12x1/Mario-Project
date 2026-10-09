using System;
using System.Collections.Generic;
using System.Linq;

namespace MarioGameSystem.DAL
{
    public class CartItem
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }

    public class ShopRepository
    {
        public bool ExecutePurchase(int userId, List<CartItem> cart)
        {
            try
            {
                if (cart == null || cart.Count == 0) return false;
                int totalCost = cart.Sum(i => i.Quantity * i.UnitPrice);
                return totalCost > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}