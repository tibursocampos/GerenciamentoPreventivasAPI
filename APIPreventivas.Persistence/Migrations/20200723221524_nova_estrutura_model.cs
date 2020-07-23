using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPreventivas.Persistence.Migrations
{
    public partial class nova_estrutura_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Supervisores",
                columns: table => new
                {
                    IdFuncionario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(nullable: true),
                    UltimoNome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Permissao = table.Column<int>(nullable: false),
                    ANF = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisores", x => x.IdFuncionario);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    IdFuncionario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(nullable: true),
                    UltimoNome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Permissao = table.Column<int>(nullable: false),
                    Area = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.IdFuncionario);
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
                    IdSite = table.Column<string>(nullable: true),
                    Concluido = table.Column<bool>(nullable: false),
                    DataConclusao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronogramas", x => x.IdCronograma);
                    table.ForeignKey(
                        name: "FK_Cronogramas_Supervisores_IdSupervisor",
                        column: x => x.IdSupervisor,
                        principalTable: "Supervisores",
                        principalColumn: "IdFuncionario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alvos",
                columns: table => new
                {
                    IdAlvo = table.Column<int>(nullable: false),
                    IdCronograma = table.Column<int>(nullable: false),
                    Concluido = table.Column<bool>(nullable: false),
                    DataConclusao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alvos", x => x.IdAlvo);
                    table.ForeignKey(
                        name: "FK_Alvos_Cronogramas_IdAlvo",
                        column: x => x.IdAlvo,
                        principalTable: "Cronogramas",
                        principalColumn: "IdCronograma",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    ANF = table.Column<int>(nullable: false),
                    IdCronograma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.EndId);
                    table.ForeignKey(
                        name: "FK_Sites_Cronogramas_IdCronograma",
                        column: x => x.IdCronograma,
                        principalTable: "Cronogramas",
                        principalColumn: "IdCronograma",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_Atividades_Tecnicos_IdTecnico",
                        column: x => x.IdTecnico,
                        principalTable: "Tecnicos",
                        principalColumn: "IdFuncionario",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Sites_IdCronograma",
                table: "Sites",
                column: "IdCronograma");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Alvos");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "Cronogramas");

            migrationBuilder.DropTable(
                name: "Supervisores");
        }
    }
}
