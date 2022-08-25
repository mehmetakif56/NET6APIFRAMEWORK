using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTBS.Migrations
{
    public partial class vw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            create or alter view GorevAtamalar as
              SELECT gk.BirlesimId,
                 gk.StenografId,
		         i.IzinTuru,
		         gk.GorevBasTarihi,
		         gk.GorevBitisTarihi,
		         DATEADD(MINUTE, -60, MIN(k.GorevBasTarihi)) MinKomTarihi,
		         DATEADD(MINUTE, 45, MAX(k.GorevBitisTarihi)) AS MaxKomTarihi,
                 MAX(k.BirlesimId) KOMSIYON,
		         g.GidenGrupSaatUygula,
		         g.GidenGrupSaat
          FROM GorevAtamaGenelKurul gk
           left outer join StenoIzin i on i.StenografId =gk.StenografId and gk.GorevBasTarihi>=BaslangicTarihi and gk.GorevBasTarihi<=BitisTarihi
           left outer join GorevAtamaKomisyon k on k.StenografId =gk.StenografId and k.GorevBasTarihi>=GetDate()-3
           left outer join Stenograf s on s.Id =gk.StenografId
           left outer join GrupDetay g on g.GrupId =s.GrupId and g.GidenGrupPasif =0 and g.IsDeleted=0
           where gk.GorevBasTarihi>=GetDate()-3
           group by gk.BirlesimId,gk.StenografId,i.IzinTuru,gk.GorevBasTarihi,gk.GorevBitisTarihi,g.GidenGrupSaatUygula,g.GidenGrupSaat;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
