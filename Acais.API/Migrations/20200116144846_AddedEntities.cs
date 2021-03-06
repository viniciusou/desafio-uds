﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acais.API.Migrations
{
    public partial class AddedEntities : Migration
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
                name: "Sabores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    TempoPreparo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sabores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tamanhos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    TempoPreparo = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tamanhos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TamanhoId = table.Column<Guid>(nullable: false),
                    SaborId = table.Column<Guid>(nullable: false),
                    TempoPreparo = table.Column<int>(nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Sabores_SaborId",
                        column: x => x.SaborId,
                        principalTable: "Sabores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Tamanhos_TamanhoId",
                        column: x => x.TamanhoId,
                        principalTable: "Tamanhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_SaborId",
                table: "Pedidos",
                column: "SaborId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_TamanhoId",
                table: "Pedidos",
                column: "TamanhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoPersonalizacoes");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Personalizacoes");

            migrationBuilder.DropTable(
                name: "Sabores");

            migrationBuilder.DropTable(
                name: "Tamanhos");
        }
    }
}
