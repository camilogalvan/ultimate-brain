using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltimateBrain.Migrations
{
    /// <inheritdoc />
    public partial class NuevaRelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pregunta_participante_ParticipanteId",
                table: "pregunta");

            migrationBuilder.AlterColumn<int>(
                name: "ParticipanteId",
                table: "pregunta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Preguntas",
                table: "participante",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__pregunta__participante__60A75C0F",
                table: "pregunta",
                column: "ParticipanteId",
                principalTable: "participante",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__pregunta__participante__60A75C0F",
                table: "pregunta");

            migrationBuilder.DropColumn(
                name: "Preguntas",
                table: "participante");

            migrationBuilder.AlterColumn<int>(
                name: "ParticipanteId",
                table: "pregunta",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_pregunta_participante_ParticipanteId",
                table: "pregunta",
                column: "ParticipanteId",
                principalTable: "participante",
                principalColumn: "id");
        }
    }
}
