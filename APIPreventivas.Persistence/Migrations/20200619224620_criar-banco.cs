using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPreventivas.Migrations
{
    public partial class criarbanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    End_id = table.Column<string>(nullable: false),
                    Site_gsm = table.Column<string>(nullable: true),
                    Site_3g = table.Column<string>(nullable: true),
                    Site_lte = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    ANF = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.End_id);
                });

            migrationBuilder.CreateTable(
                name: "Supervisores",
                columns: table => new
                {
                    Id_funcionario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Primeiro_nome = table.Column<string>(nullable: true),
                    Ultimo_nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Permissao = table.Column<int>(nullable: false),
                    ANF = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisores", x => x.Id_funcionario);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    Id_funcionario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Primeiro_nome = table.Column<string>(nullable: true),
                    Ultimo_nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Permissao = table.Column<int>(nullable: false),
                    Area = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.Id_funcionario);
                });

            migrationBuilder.CreateTable(
                name: "Cronogramas",
                columns: table => new
                {
                    Id_cronograma = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_supervisor = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Ano = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronogramas", x => x.Id_cronograma);
                    table.ForeignKey(
                        name: "FK_Cronogramas_Supervisores_Id_supervisor",
                        column: x => x.Id_supervisor,
                        principalTable: "Supervisores",
                        principalColumn: "Id_funcionario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alvos",
                columns: table => new
                {
                    Id_alvo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_cronograma = table.Column<int>(nullable: false),
                    Id_supervisor = table.Column<int>(nullable: false),
                    Site_end_id = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Data_conclusao = table.Column<DateTime>(nullable: true),
                    Id_tecnico_prog_aterramento = table.Column<int>(nullable: true),
                    Data_tecnico_prog_aterramento = table.Column<DateTime>(nullable: true),
                    Id_tecnico_exec_aterramento = table.Column<int>(nullable: true),
                    Data_tecnico_exec_aterramento = table.Column<DateTime>(nullable: true),
                    Id_tecnico_prog_bateria = table.Column<int>(nullable: true),
                    Data_tecnico_prog_bateria = table.Column<DateTime>(nullable: true),
                    Id_tecnico_exec_bateria = table.Column<int>(nullable: true),
                    Data_tecnico_exec_bateria = table.Column<DateTime>(nullable: true),
                    Id_tecnico_prog_infra = table.Column<int>(nullable: true),
                    Data_tecnico_prog_infra = table.Column<DateTime>(nullable: true),
                    Id_tecnico_exec_infra = table.Column<int>(nullable: true),
                    Data_tecnico_exec_infra = table.Column<DateTime>(nullable: true),
                    Id_tecnico_prog_acesso = table.Column<int>(nullable: true),
                    Data_tecnico_prog_acesso = table.Column<DateTime>(nullable: true),
                    Id_tecnico_exec_acesso = table.Column<int>(nullable: true),
                    Data_tecnico_exec_acesso = table.Column<DateTime>(nullable: true),
                    Id_tecnico_prog_mw = table.Column<int>(nullable: true),
                    Data_tecnico_prog_mw = table.Column<DateTime>(nullable: true),
                    Id_tecnico_exec_mw = table.Column<int>(nullable: true),
                    Data_tecnico_exec_mw = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alvos", x => x.Id_alvo);
                    table.ForeignKey(
                        name: "FK_Alvos_Cronogramas_Id_cronograma",
                        column: x => x.Id_cronograma,
                        principalTable: "Cronogramas",
                        principalColumn: "Id_cronograma",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alvos_Sites_Site_end_id",
                        column: x => x.Site_end_id,
                        principalTable: "Sites",
                        principalColumn: "End_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TecnicosAlvos",
                columns: table => new
                {
                    Id_tecnico = table.Column<int>(nullable: false),
                    Id_alvo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecnicosAlvos", x => new { x.Id_alvo, x.Id_tecnico });
                    table.ForeignKey(
                        name: "FK_TecnicosAlvos_Alvos_Id_alvo",
                        column: x => x.Id_alvo,
                        principalTable: "Alvos",
                        principalColumn: "Id_alvo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TecnicosAlvos_Tecnicos_Id_tecnico",
                        column: x => x.Id_tecnico,
                        principalTable: "Tecnicos",
                        principalColumn: "Id_funcionario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alvos_Id_cronograma",
                table: "Alvos",
                column: "Id_cronograma");

            migrationBuilder.CreateIndex(
                name: "IX_Alvos_Site_end_id",
                table: "Alvos",
                column: "Site_end_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cronogramas_Id_supervisor",
                table: "Cronogramas",
                column: "Id_supervisor");

            migrationBuilder.CreateIndex(
                name: "IX_TecnicosAlvos_Id_tecnico",
                table: "TecnicosAlvos",
                column: "Id_tecnico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TecnicosAlvos");

            migrationBuilder.DropTable(
                name: "Alvos");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "Cronogramas");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Supervisores");
        }
    }
}
