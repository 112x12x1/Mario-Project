using System;
using System.Collections.Generic;
using MarioGameSystem.DAL;

namespace MarioGameSystem.BLL
{
    public class ShopService
    {
        private readonly ShopRepository shopRepo;

        public ShopService()
        {
            shopRepo = new ShopRepository();
        }

        public bool ProcessPurchase(int userId, List<CartItem> cartItems, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (userId <= 0)
            {
                errorMessage = "Invalid user account.";
                return false;
            }

            if (cartItems == null || cartItems.Count == 0)
            {
                errorMessage = "Cart is empty.";
                return false;
            }

            foreach (var item in cartItems)
            {
                if (item.ItemID <= 0 || item.Quantity <= 0 || item.UnitPrice < 0)
                {
                    errorMessage = "Invalid cart item attributes.";
                    return false;
                }
            }

            try
            {
                bool success = shopRepo.ExecutePurchase(userId, cartItems);
                if (!success)
                {
                    errorMessage = "Transaction failed.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "System error: " + ex.Message;
                return false;
            }
        }
    }
}