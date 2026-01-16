using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin.Migrations
{
    /// <inheritdoc />
    public partial class test6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Proyectos",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Proyectos",
                newName: "FechaCreacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Proyectos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Proyectos",
                newName: "CreationDate");
        }
    }
}
