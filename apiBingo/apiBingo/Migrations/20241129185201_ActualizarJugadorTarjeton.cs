using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiBingo.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarJugadorTarjeton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tarjeton",
                table: "Jugadores");

            migrationBuilder.RenameColumn(
                name: "TarjetonJson",
                table: "Jugadores",
                newName: "TarjetonSerialized");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TarjetonSerialized",
                table: "Jugadores",
                newName: "TarjetonJson");

            migrationBuilder.AddColumn<int[,]>(
                name: "Tarjeton",
                table: "Jugadores",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }
    }
}
