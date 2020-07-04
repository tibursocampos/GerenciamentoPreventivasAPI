using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPreventivas.Migrations
{
    public partial class alvo_siteEndId_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alvos_Sites_Site_end_id",
                table: "Alvos");

            migrationBuilder.AlterColumn<string>(
                name: "Site_end_id",
                table: "Alvos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alvos_Sites_Site_end_id",
                table: "Alvos",
                column: "Site_end_id",
                principalTable: "Sites",
                principalColumn: "End_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alvos_Sites_Site_end_id",
                table: "Alvos");

            migrationBuilder.AlterColumn<string>(
                name: "Site_end_id",
                table: "Alvos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Alvos_Sites_Site_end_id",
                table: "Alvos",
                column: "Site_end_id",
                principalTable: "Sites",
                principalColumn: "End_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
