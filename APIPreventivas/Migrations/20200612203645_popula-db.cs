using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPreventivas.Migrations
{
    public partial class populadb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO supervisores VALUES('Tadeu','Mariano','tadeu@teste.com','1234',1,0)");
            mb.Sql("INSERT INTO supervisores VALUES('Maria','Neves','maria@teste.com','1234',1,2)");
            mb.Sql("INSERT INTO tecnicos VALUES('Tiago','Rodrigues','tiago@teste.com','1234',2,1)");
            mb.Sql("INSERT INTO tecnicos VALUES('Danilo','Machado','danilo@teste.com','1234',2,1)");
            mb.Sql("INSERT INTO tecnicos VALUES('Lucio','Freitas','lucio@teste.com','1234',2,2)");
            mb.Sql("INSERT INTO tecnicos VALUES('Luis','Alves','luis@teste.com','1234',2,2)");
            mb.Sql("INSERT INTO sites VALUES('MGVGA_0001','VGGT01','MG5001','NLVGGT01','Varginha',12,4)");
            mb.Sql("INSERT INTO sites VALUES('MGVGA_0002','VGAS02','MG5002','NLVGAS02','Varginha',12,4)");
            mb.Sql("INSERT INTO sites VALUES('MGLAV_0001','LVVG03','MG5003','NLLVVG03','Lavras',12,4)");
            mb.Sql("INSERT INTO sites VALUES('MGLAV_0002','LVVG04','MG5004','NLLVVG04','Lavras',12,5)");
            mb.Sql("INSERT INTO sites VALUES('MGPSA_0001','PAVG12','MG5012','NLPAVG12','Pouso Alegre',12,4)");
            mb.Sql("INSERT INTO sites VALUES('MGPSA_0002','PAVG13','MG5013','NLPAVG13','Pouso Alegre',12,4)");
            mb.Sql("INSERT INTO sites VALUES('MGPCS_0001','POVG08','MG5008','NLPOVG08','Poços de Caldas',12,4)");
            mb.Sql("INSERT INTO sites VALUES('MGPCS_0002','POVG62','MG5062','NLPOVG62','Poços de Caldas',12,4)");
            mb.Sql("INSERT INTO cronogramas VALUES(1,3,2020,0)");
            mb.Sql("INSERT INTO cronogramas VALUES(3,4,2020,0)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento) VALUES(1,1,'MGVGA_0001',3)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento) VALUES(1,1,'MGVGA_0002',3)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento) VALUES(1,1,'MGLAV_0001',4)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento) VALUES(1,1,'MGLAV_0002',4)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento) VALUES(3,2,'MGPSA_0001',5)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento) VALUES(3,2,'MGPSA_0002',5)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento) VALUES(4,2,'MGPCS_0001',6)");
            mb.Sql("INSERT INTO alvos(id_cronograma, id_supervisor, Site_end_id, Id_tecnico_prog_aterramento) VALUES(4,2,'MGPCS_0002',6)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(5,3)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(6,3)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(7,4)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(8,4)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(9,5)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(10,5)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(11,6)");
            mb.Sql("INSERT INTO tecnicosalvos VALUES(12,6)");
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
