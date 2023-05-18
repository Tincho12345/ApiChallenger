using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiContactos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImagen",
                table: "Contactos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImagen",
                table: "Contactos");
        }
    }
}
