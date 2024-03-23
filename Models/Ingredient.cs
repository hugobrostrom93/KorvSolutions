namespace KorvSolutions.Models
{
    public class Ingredient
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public int BatchNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Supplier { get; set; }
        public int DayNo { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; } // Foreign Key
        public Product Product { get; set; } // Navigation Property
    }
}

