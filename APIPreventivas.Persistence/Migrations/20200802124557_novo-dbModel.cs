using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPreventivas.Persistence.Migrations
{
    public partial class novodbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    EndId = table.Column<string>(nullable: false),
                    SiteGsm = table.Column<string>(nullable: true),
                    Site3g = table.Column<string>(nullable: true),
                    SiteLte = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    ANF = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.EndId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<int>(nullable: false),
                    PrimeiroNome = table.Column<string>(nullable: true),
                    UltimoNome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    ANF = table.Column<int>(nullable: false),
                    Permissao = table.Column<int>(nullable: false),
                    Area = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Cronogramas",
                columns: table => new
                {
                    IdCronograma = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSupervisor = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Concluido = table.Column<bool>(nullable: false),
                    DataConclusao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronogramas", x => x.IdCronograma);
                    table.ForeignKey(
                        name: "FK_Cronogramas_Usuarios_IdSupervisor",
                        column: x => x.IdSupervisor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alvos",
                columns: table => new
                {
                    IdAlvo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCronograma = table.Column<int>(nullable: false),
                    IdSite = table.Column<string>(nullable: true),
                    Concluido = table.Column<bool>(nullable: false),
                    DataConclusao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alvos", x => x.IdAlvo);
                    table.ForeignKey(
                        name: "FK_Alvos_Cronogramas_IdCronograma",
                        column: x => x.IdCronograma,
                        principalTable: "Cronogramas",
                        principalColumn: "IdCronograma",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alvos_Sites_IdSite",
                        column: x => x.IdSite,
                        principalTable: "Sites",
                        principalColumn: "EndId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    IdAtividade = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlvo = table.Column<int>(nullable: false),
                    IdTecnico = table.Column<int>(nullable: false),
                    TipoAtividade = table.Column<int>(nullable: false),
                    DataProgramacao = table.Column<DateTime>(nullable: false),
                    DataConclusao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.IdAtividade);
                    table.ForeignKey(
                        name: "FK_Atividades_Alvos_IdAlvo",
                        column: x => x.IdAlvo,
                        principalTable: "Alvos",
                        principalColumn: "IdAlvo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atividades_Usuarios_IdTecnico",
                        column: x => x.IdTecnico,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alvos_IdCronograma",
                table: "Alvos",
                column: "IdCronograma");

            migrationBuilder.CreateIndex(
                name: "IX_Alvos_IdSite",
                table: "Alvos",
                column: "IdSite",
                unique: true,
                filter: "[IdSite] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_IdAlvo",
                table: "Atividades",
                column: "IdAlvo");

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_IdTecnico",
                table: "Atividades",
                column: "IdTecnico");

            migrationBuilder.CreateIndex(
                name: "IX_Cronogramas_IdSupervisor",
                table: "Cronogramas",
                column: "IdSupervisor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Alvos");

            migrationBuilder.DropTable(
                name: "Cronogramas");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
