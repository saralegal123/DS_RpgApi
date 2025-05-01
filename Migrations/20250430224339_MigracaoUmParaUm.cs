using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoUmParaUm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disputas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vitorias",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonagemId",
                table: "TB_ARMAS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonagemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonagemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 3,
                column: "PersonagemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 4,
                column: "PersonagemId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 5,
                column: "PersonagemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 6,
                column: "PersonagemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 7,
                column: "PersonagemId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 253, 199, 49, 98, 70, 110, 106, 85, 40, 152, 143, 132, 90, 161, 23, 249, 88, 20, 56, 157, 229, 211, 97, 3, 253, 49, 50, 133, 180, 1, 71, 82, 78, 152, 15, 239, 221, 137, 49, 237, 175, 117, 3, 74, 86, 37, 239, 1, 1, 226, 89, 218, 115, 40, 246, 196, 37, 237, 25, 165, 84, 137, 157, 37 }, new byte[] { 229, 251, 240, 222, 217, 182, 163, 4, 58, 117, 211, 167, 139, 220, 16, 34, 205, 46, 18, 221, 31, 198, 191, 25, 58, 183, 46, 102, 12, 118, 186, 157, 212, 203, 24, 49, 151, 49, 96, 125, 152, 126, 255, 87, 177, 70, 119, 179, 70, 100, 42, 198, 162, 15, 120, 84, 149, 31, 252, 212, 49, 72, 113, 228, 159, 42, 132, 3, 208, 213, 216, 221, 21, 206, 79, 134, 63, 247, 112, 195, 212, 169, 106, 111, 1, 65, 244, 199, 58, 160, 129, 157, 244, 104, 234, 197, 120, 149, 151, 211, 94, 21, 160, 67, 223, 200, 221, 188, 123, 13, 122, 254, 220, 24, 33, 15, 203, 252, 216, 26, 101, 253, 136, 238, 228, 27, 225, 115 } });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                principalTable: "TB_PERSONAGENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Disputas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Vitorias",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 166, 48, 80, 11, 229, 100, 60, 227, 33, 50, 217, 47, 131, 200, 74, 199, 66, 101, 118, 19, 2, 112, 172, 143, 120, 173, 112, 19, 176, 38, 152, 180, 23, 7, 118, 142, 216, 25, 231, 231, 151, 171, 191, 42, 99, 91, 175, 5, 109, 132, 60, 136, 237, 234, 219, 237, 29, 184, 236, 236, 43, 104, 179, 200 }, new byte[] { 92, 3, 198, 125, 174, 26, 68, 196, 86, 251, 13, 197, 12, 117, 211, 137, 249, 78, 132, 251, 69, 151, 52, 60, 82, 80, 167, 142, 236, 27, 189, 8, 186, 96, 248, 148, 88, 93, 44, 173, 58, 26, 33, 113, 136, 170, 173, 62, 241, 74, 27, 32, 50, 68, 110, 45, 221, 194, 212, 103, 226, 38, 30, 58, 158, 51, 80, 74, 179, 76, 22, 47, 79, 76, 235, 170, 199, 227, 243, 41, 29, 23, 43, 141, 44, 95, 99, 220, 111, 67, 134, 39, 153, 39, 123, 150, 163, 215, 44, 244, 244, 67, 155, 50, 213, 225, 78, 108, 7, 204, 47, 148, 160, 215, 202, 231, 55, 43, 167, 169, 197, 60, 183, 22, 209, 22, 53, 203 } });
        }
    }
}
