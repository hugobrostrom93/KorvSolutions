namespace KorvSolutions.Models
{
    public class Product
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public List<PlanProduct> PlanProducts { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}