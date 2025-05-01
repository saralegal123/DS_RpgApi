using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoMuitosParaMuitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_HABILIDADES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_HABILIDADES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PERSONAGENS_HABILIDADES",
                columns: table => new
                {
                    PersonagemId = table.Column<int>(type: "int", nullable: false),
                    HabilidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PERSONAGENS_HABILIDADES", x => new { x.PersonagemId, x.HabilidadeId });
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_HABILIDADES_HabilidadeId",
                        column: x => x.HabilidadeId,
                        principalTable: "TB_HABILIDADES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_PERSONAGENS_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "TB_PERSONAGENS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_HABILIDADES",
                columns: new[] { "Id", "Dano", "Nome" },
                values: new object[,]
                {
                    { 1, 39, "Adormecer" },
                    { 2, 41, "Congelar" },
                    { 3, 37, "Hipnotizar" }
                });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 95, 11, 150, 34, 19, 58, 192, 94, 114, 14, 13, 160, 215, 72, 68, 154, 190, 134, 232, 21, 4, 57, 39, 204, 38, 150, 192, 132, 217, 132, 150, 205, 118, 209, 28, 20, 70, 81, 144, 49, 239, 147, 241, 226, 3, 61, 53, 65, 153, 38, 131, 91, 45, 189, 96, 83, 19, 190, 128, 48, 246, 149, 242, 133 }, new byte[] { 23, 108, 185, 51, 14, 220, 107, 28, 64, 91, 66, 111, 220, 133, 96, 154, 162, 104, 117, 251, 165, 16, 124, 44, 124, 27, 226, 20, 167, 206, 170, 120, 30, 248, 88, 165, 117, 195, 226, 73, 154, 127, 209, 146, 90, 200, 197, 76, 86, 133, 123, 163, 210, 245, 11, 143, 192, 125, 208, 29, 216, 133, 65, 161, 145, 211, 176, 218, 187, 95, 30, 162, 116, 250, 107, 225, 248, 13, 28, 57, 242, 145, 196, 54, 49, 117, 4, 124, 35, 121, 2, 227, 187, 37, 3, 56, 42, 87, 199, 194, 185, 136, 16, 176, 0, 107, 3, 246, 81, 69, 87, 249, 223, 72, 195, 81, 36, 143, 169, 249, 137, 145, 58, 230, 8, 173, 208, 81 } });

            migrationBuilder.InsertData(
                table: "TB_PERSONAGENS_HABILIDADES",
                columns: new[] { "HabilidadeId", "PersonagemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 3, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 3, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PERSONAGENS_HABILIDADES_HabilidadeId",
                table: "TB_PERSONAGENS_HABILIDADES",
                column: "HabilidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PERSONAGENS_HABILIDADES");

            migrationBuilder.DropTable(
                name: "TB_HABILIDADES");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 253, 199, 49, 98, 70, 110, 106, 85, 40, 152, 143, 132, 90, 161, 23, 249, 88, 20, 56, 157, 229, 211, 97, 3, 253, 49, 50, 133, 180, 1, 71, 82, 78, 152, 15, 239, 221, 137, 49, 237, 175, 117, 3, 74, 86, 37, 239, 1, 1, 226, 89, 218, 115, 40, 246, 196, 37, 237, 25, 165, 84, 137, 157, 37 }, new byte[] { 229, 251, 240, 222, 217, 182, 163, 4, 58, 117, 211, 167, 139, 220, 16, 34, 205, 46, 18, 221, 31, 198, 191, 25, 58, 183, 46, 102, 12, 118, 186, 157, 212, 203, 24, 49, 151, 49, 96, 125, 152, 126, 255, 87, 177, 70, 119, 179, 70, 100, 42, 198, 162, 15, 120, 84, 149, 31, 252, 212, 49, 72, 113, 228, 159, 42, 132, 3, 208, 213, 216, 221, 21, 206, 79, 134, 63, 247, 112, 195, 212, 169, 106, 111, 1, 65, 244, 199, 58, 160, 129, 157, 244, 104, 234, 197, 120, 149, 151, 211, 94, 21, 160, 67, 223, 200, 221, 188, 123, 13, 122, 254, 220, 24, 33, 15, 203, 252, 216, 26, 101, 253, 136, 238, 228, 27, 225, 115 } });
        }
    }
}
