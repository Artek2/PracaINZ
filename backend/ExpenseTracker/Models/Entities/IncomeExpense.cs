using Models.Entities.Abstraction; // Custom namespace likely containing base classes or interfaces
using System.ComponentModel.DataAnnotations; // From System.ComponentModel.Annotations for validation attributes

namespace Models.Entities
{
    // Defines a class representing a common structure for Income and Expense entities.
    public class IncomeExpense : BaseEntity // BaseEntity likely provided by the custom Models.Entities.Abstraction
    {
        [StringLength(50)] // DataAnnotations attribute specifying the maximum length of the string.
        public string Title { get; set; } = string.Empty; // Title property with a default empty string.

        // No attribute here indicates that this is a simple property without additional data annotations.
        public decimal Amount { get; set; } // Represents a monetary value.

        [StringLength(50)] // DataAnnotations attribute for maximum length.
        public string Type { get; set; } = string.Empty; // Type of the transaction (e.g., income or expense).

        // No attribute here since DateOnly is a struct introduced in .NET 6 representing a date without a time component.
        public DateOnly Date { get; set; } // The date associated with the income or expense.

        [StringLength(50)] // DataAnnotations attribute for maximum length.
        public string Category { get; set; } = string.Empty; // Category for grouping transactions.

        [StringLength(255)] // DataAnnotations attribute for maximum length.
        public string Description { get; set; } = string.Empty; // Description for additional details.

        // No attribute needed as this is a foreign key property for a relational database using Entity Framework.
        public int UserId { get; set; } // Foreign key relating to the User entity.

        // Entity Framework core feature to establish a navigation property for related entities.
        public virtual User User { get; set; } = null!; // The User entity this income/expense belongs to.
    }
}
