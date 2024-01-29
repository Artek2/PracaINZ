using System; // Base system namespace from .NET framework.
using Microsoft.EntityFrameworkCore.Metadata; // Entity Framework Core metadata namespace for database schema details.
using Microsoft.EntityFrameworkCore.Migrations; // Entity Framework Core migrations namespace for database migrations.

#nullable disable // Directive to disable nullable reference types feature in C# 8.0+

namespace Models.Migrations
{
    // Inheritance from Migration class provided by Entity Framework Core
    public partial class Init : Migration
    {
        // Override of the 'Up' method to define the migration logic for applying changes to the database.
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // AlterDatabase with annotations is specific to Entity Framework Core to modify database-level properties.
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            // CreateTable is a method from Entity Framework Core's migration builder to define a new table.
            migrationBuilder.CreateTable(
                name: "Users",
                // Column definitions using Entity Framework Core's fluent API.
                columns: table => new
                {
                    // Id column definition with MySQL specific annotations for identity generation.
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    // Other fields with annotations to define column types and character sets for MySQL.
                    // ...
                },
                // Constraints like Primary Key definition using Entity Framework Core's API.
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            // Another CreateTable call for a different table with its own columns and constraints.
            migrationBuilder.CreateTable(
                name: "IncomeExpense",
                // Column definitions and annotations similar to the 'Users' table.
                // ...
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeExpense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeExpense_Users_UserId",
                        // ForeignKey configuration with Entity Framework Core methods.
                        // ...
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            // Creation of an index on the UserId column of the IncomeExpense table using Entity Framework Core.
            migrationBuilder.CreateIndex(
                name: "IX_IncomeExpense_UserId",
                table: "IncomeExpense",
                column: "UserId");
        }

        // Override of the 'Down' method to reverse the migration logic if needed.
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // DropTable calls from Entity Framework Core to remove tables if the migration is rolled back.
            migrationBuilder.DropTable(
                name: "IncomeExpense");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
