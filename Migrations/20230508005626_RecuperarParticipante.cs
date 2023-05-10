using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltimateBrain.Migrations
{
    /// <inheritdoc />
    public partial class RecuperarParticipante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__pregunta__participante__60A75C0F",
                table: "pregunta");

            migrationBuilder.DropTable(
                name: "Partida");

            migrationBuilder.DropIndex(
                name: "IX_pregunta_ParticipanteId",
                table: "pregunta");

            migrationBuilder.DropColumn(
                name: "ParticipanteId",
                table: "pregunta");

            migrationBuilder.RenameColumn(
                name: "Preguntas",
                table: "participante",
                newName: "Pregunta_resuelta");

            migrationBuilder.CreateTable(
                name: "pregunta_resuelta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    id_pregunta = table.Column<int>(type: "int", nullable: true),
                    participanteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PreguntaResuelta__3213E83F8D2F70D6", x => x.id);
                    table.ForeignKey(
                        name: "FK__partida__partici__6383C8BA",
                        column: x => x.participanteId,
                        principalTable: "participante",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pregunta_resuelta_participanteId",
                table: "pregunta_resuelta",
                column: "participanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pregunta_resuelta");

            migrationBuilder.RenameColumn(
                name: "Pregunta_resuelta",
                table: "participante",
                newName: "Preguntas");

            migrationBuilder.AddColumn<int>(
                name: "ParticipanteId",
                table: "pregunta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Partida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipanteId = table.Column<int>(type: "int", nullable: true),
                    Puntaje = table.Column<int>(type: "int", nullable: true),
                    Tiempo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partida_participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "participante",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pregunta_ParticipanteId",
                table: "pregunta",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Partida_ParticipanteId",
                table: "Partida",
                column: "ParticipanteId");

            migrationBuilder.AddForeignKey(
                name: "FK__pregunta__participante__60A75C0F",
                table: "pregunta",
                column: "ParticipanteId",
                principalTable: "participante",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
