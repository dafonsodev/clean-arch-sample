using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations;

/// <inheritdoc />
public partial class SeedProducts : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder mb)
    {
        mb.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" +
            "VALUES('Caderno Espiral', 'Caderno Espiral 100 Folhas', 7.45, 50, 'caderno1.jpg', 1)");

        mb.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" +
            "VALUES('Estojo Escolar', 'Estojo Escolar Cinza', 5.65, 70, 'estojo1.jpg', 1)");

        mb.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" +
            "VALUES('Borracha Escolar', 'Borracha Branca Pequena', 3.20, 80, 'borracha1.jpg', 1)");

        mb.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" +
            "VALUES('Calculadora Escolar', 'Calculadora Simples', 15.39, 20, 'calculadora1.jpg', 2)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder mb)
    {
        mb.Sql("DELETE FROM Products");
    }
}
