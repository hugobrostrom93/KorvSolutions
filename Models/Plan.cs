using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KorvSolutions.Models
{
    public class Plan
    {
        public int Id { get; set; } // Primary Key
        public DateTime Date { get; set; }
        public List<PlanProduct> PlanProducts { get; set; } // Navigation property for associated products
    }

    public class PlanProduct
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } // Quantity of this product in the plan

        [ForeignKey("PlanId")]
        public Plan Plan { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}