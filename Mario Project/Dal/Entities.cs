using System;

namespace MarioGameSystem.DAL
{
    public class NguoiDung
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Coins { get; set; }
    }

    public class VatPham
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
    }

    public class HoaDon
    {
        public int InvoiceID { get; set; }
        public int UserID { get; set; }
        public DateTime CreateDate { get; set; }
        public int TotalCoins { get; set; }
    }

    public class ChiTietHoaDon
    {
        public int InvoiceID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}