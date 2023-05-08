using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltimateBrain.Migrations
{
    /// <inheritdoc />
    public partial class NombreMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "participante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nickName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    puntaje = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__particip__3213E83FA6858DC8", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "pregunta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    textoPregunta = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    respuestas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoria = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    dificultad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ParticipanteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pregunta__3213E83FAEC31AAD", x => x.id);
                    table.ForeignKey(
                        name: "FK_pregunta_participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "participante",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "respuesta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    textoRespuesta = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    isCorrect = table.Column<bool>(type: "bit", nullable: true),
                    preguntaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__respuest__3213E83F8D82F32E", x => x.id);
                    table.ForeignKey(
                        name: "FK__respuesta__pregu__60A75C0F",
                        column: x => x.preguntaId,
                        principalTable: "pregunta",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partida_ParticipanteId",
                table: "Partida",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_pregunta_ParticipanteId",
                table: "pregunta",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_respuesta_preguntaId",
                table: "respuesta",
                column: "preguntaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partida");

            migrationBuilder.DropTable(
                name: "respuesta");

            migrationBuilder.DropTable(
                name: "pregunta");

            migrationBuilder.DropTable(
                name: "participante");
        }
    }
}
