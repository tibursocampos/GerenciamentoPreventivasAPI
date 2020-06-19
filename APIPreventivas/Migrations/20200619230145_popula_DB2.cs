using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPreventivas.Migrations
{
    public partial class popula_DB2 : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO cronogramas VALUES(1,3,2020,0)");
            mb.Sql("INSERT INTO cronogramas VALUES(2,4,2020,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento, Status) VALUES(1,1,'MGVGA_0001',1,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento, Status) VALUES(1,1,'MGVGA_0002',1,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento, Status) VALUES(1,1,'MGLAV_0001',2,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento, Status) VALUES(1,1,'MGLAV_0002',2,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento, Status) VALUES(2,2,'MGPSA_0001',3,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento, Status) VALUES(2,2,'MGPSA_0002',3,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento, Status) VALUES(2,2,'MGPCS_0001',4,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento, Status) VALUES(2,2,'MGPCS_0002',4,0)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(1,1)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(1,2)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(2,3)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(2,4)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(3,5)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(3,6)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(4,7)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(4,8)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM supervisores");
            mb.Sql("DELETE FROM tecnicos");
            mb.Sql("DELETE FROM sites");
            mb.Sql("DELETE FROM cronogramas");
            mb.Sql("DELETE FROM alvos");
        }
    }
}
