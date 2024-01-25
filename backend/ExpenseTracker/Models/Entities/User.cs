using Models.Entities.Abstraction; // This using directive would be referencing a custom namespace for base entity classes.

namespace Models.Entities
{
    // Defines a class representing the User entity, inheriting from BaseEntity which would typically include common properties like Id.
    public class User : BaseEntity // BaseEntity is part of the custom abstraction that likely includes common fields like Id.
    {
        // Simple property to hold the user's email. No attributes from any library are applied here.
        public string Email { get; set; } = string.Empty; // Default initialization to prevent null values.

        // Simple property for the user's name. Like Email, this also does not have any library-specific attributes.
        public string Name { get; set; } // Assumes a non-null value will be provided during user creation.

        // Property to store the hash of the user's password, usually set via Identity framework methods in ASP.NET Core Identity.
        public string PasswordHash { get; set; } = string.Empty; // Default initialization to prevent null values.

        // Entity Framework Core feature to define a navigation property for related entities.
        // This establishes a one-to-many relationship with the IncomeExpense entity.
        public virtual List<IncomeExpense> IncomeExpense { get; set; } = null!; // Navigation property for related IncomeExpense records.
    }
}
