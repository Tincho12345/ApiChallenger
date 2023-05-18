using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiContactos.Migrations
{
    /// <inheritdoc />
    public partial class Prueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImagen",
                table: "Contactos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "UrlImagen",
                table: "Contactos",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
