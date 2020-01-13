using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acais.API.Migrations
{
    public partial class PersonalizacoesPedidoPersonalizacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personalizacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Produto = table.Column<string>(nullable: false),
                    TempoPreparo = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalizacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoPersonalizacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PedidoId = table.Column<Guid>(nullable: false),
                    PersonalizacaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoPersonalizacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoPersonalizacoes_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoPersonalizacoes_Personalizacoes_PersonalizacaoId",
                        column: x => x.PersonalizacaoId,
                        principalTable: "Personalizacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPersonalizacoes_PedidoId",
                table: "PedidoPersonalizacoes",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoPersonalizacoes_PersonalizacaoId",
                table: "PedidoPersonalizacoes",
                column: "PersonalizacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoPersonalizacoes");

            migrationBuilder.DropTable(
                name: "Personalizacoes");
        }
    }
}
