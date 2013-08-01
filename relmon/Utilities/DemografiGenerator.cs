using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using relmon.Models;

namespace relmon.Utilities
{
    public class DemografiGenerator
    {
        //
        // GET: /DemografiGenerator/
        private RelmonStoreEntities db = new RelmonStoreEntities();

        private List<Tuple<string, DateTime, DateTime>> demografiLahir()
        {
            List<Tuple<string, DateTime, DateTime>> resultTable = new List<Tuple<string, DateTime, DateTime>>();

            List<int> tahunLahirList = db.users.Select(u => u.tgl_lahir.Value.Year).OrderBy(tahunLahir => tahunLahir).Distinct().ToList();
            foreach (int tahunLahir in tahunLahirList)
            {
                resultTable.Add(new Tuple<string, DateTime, DateTime>(tahunLahir.ToString(), DateTime.Parse("1/1/" + tahunLahir.ToString()), DateTime.Parse("1/1/" + (tahunLahir + 1).ToString())));

            }

            return resultTable;
        }

        private List<Tuple<string, DateTime, DateTime>> demografiUsia()
        {
            List<Tuple<string, DateTime, DateTime>> resultTable = new List<Tuple<string, DateTime, DateTime>>();

            List<int> tahunLahirList = db.users.Select(u => u.tgl_lahir.Value.Year).OrderBy(tahunLahir => tahunLahir).Distinct().ToList();
            resultTable.Add(new Tuple<string, DateTime, DateTime>("< 30",  DateTime.Now.AddYears(-30), DateTime.Now));
            resultTable.Add(new Tuple<string, DateTime, DateTime>("30-40", DateTime.Now.AddYears(-40), DateTime.Now.AddYears(-30)));
            resultTable.Add(new Tuple<string, DateTime, DateTime>("40-50", DateTime.Now.AddYears(-50), DateTime.Now.AddYears(-40)));
            resultTable.Add(new Tuple<string, DateTime, DateTime>("> 50",  DateTime.Now.AddYears(-150), DateTime.Now.AddYears(-50)));

            return resultTable;
        }

        private List<string> demografiGolongan()
        {
            List<string> resultTable = new List<string>();

            resultTable.AddRange(new string[] { "0D", "P3", "P2", "P1", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15" });

            return resultTable;
        }

        private List<Tuple<string, string>> demografiPendidikan()
        {
            List<Tuple<string, string>> resultTable = new List<Tuple<string, string>>();

            //resultTable.Add(new Tuple<string, string>("SD", "SD"));
            //resultTable.Add(new Tuple<string, string>("SLTP", "SLTP"));
            resultTable.Add(new Tuple<string, string>("SLTA", "SLTA"));
            resultTable.Add(new Tuple<string, string>("D1+D2", "Diploma I&II"));
            resultTable.Add(new Tuple<string, string>("D3", "Diploma III"));
            resultTable.Add(new Tuple<string, string>("D4", "Diploma IV"));
            resultTable.Add(new Tuple<string, string>("S1", "S1-Perguruan Tinggi"));
            resultTable.Add(new Tuple<string, string>("S2", "S2-Pasca Sarjana"));
            resultTable.Add(new Tuple<string, string>("S3", "S3-Pasca/Doktor"));

            return resultTable;
        }

        private List<Tuple<string, DateTime, DateTime>> demografiMPPK()
        {
            List<Tuple<string, DateTime, DateTime>> resultTable = new List<Tuple<string, DateTime, DateTime>>();

            int tahunSekarang = DateTime.Now.Year;
            for (int i=0; i<5; i++)
            {
                resultTable.Add(new Tuple<string, DateTime, DateTime>((tahunSekarang + i).ToString(), DateTime.Parse("1/1/" + (tahunSekarang + i).ToString()), DateTime.Parse("1/1/" + (tahunSekarang + i + 1).ToString())));

            }
            //resultTable.Add(new Tuple<string, DateTime, DateTime>("2010-2011", DateTime.Parse("1/1/2010"), DateTime.Parse("1/1/2012")));
            //resultTable.Add(new Tuple<string, DateTime, DateTime>("2012", DateTime.Parse("1/1/2012"), DateTime.Parse("1/1/2013")));
            //resultTable.Add(new Tuple<string, DateTime, DateTime>("2013", DateTime.Parse("1/1/2013"), DateTime.Parse("1/1/2014")));
            //resultTable.Add(new Tuple<string, DateTime, DateTime>("2014", DateTime.Parse("1/1/2014"), DateTime.Parse("1/1/2015")));
            //resultTable.Add(new Tuple<string, DateTime, DateTime>("2015", DateTime.Parse("1/1/2015"), DateTime.Parse("1/1/2016")));
            //resultTable.Add(new Tuple<string, DateTime, DateTime>("2016-2020", DateTime.Parse("1/1/2016"), DateTime.Parse("1/1/2021")));

            return resultTable;
        }

        //Tabel X : Jumlah, Tabel Y : Jabatan [V] / Cost Center [ ]
        //Chart : N/A
        public Tuple<TableContainer, List<ChartContainer>> getDemografiOrganisasi(int companyId)
        {
            TableContainer resultTable = new TableContainer();
            List<ChartContainer> resultChart = new List<ChartContainer>();
            
            //isi header ================================================================================= //
            List<Dictionary<string, string>> oneRowHeader = new List<Dictionary<string, string>>();

            Dictionary<string, string> oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Fungsi";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Formasi";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "colspan=2"; oneCellHeader["text"] = DateTime.Now.ToString("dd MMM");
            oneRowHeader.Add(oneCellHeader);

            resultTable.tableHeader.Add(oneRowHeader);

            oneRowHeader = new List<Dictionary<string, string>>();

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = ""; oneCellHeader["text"] = "Estab.";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = ""; oneCellHeader["text"] = "Vacant";
            oneRowHeader.Add(oneCellHeader);

            resultTable.tableHeader.Add(oneRowHeader);

            //isi content table ========================================================================== //
            resultTable.cellsName.Add("fungsi");
            resultTable.cellsName.Add("formasi");
            resultTable.cellsName.Add("estab");
            resultTable.cellsName.Add("vacant");

            var listCostCentre = (from p in db.users_jabatan 
                                  join q in db.users_jabatan on p.cost_centre equals q.id
                                  where (p.company_id == companyId) && (p.id > 0)
                                  orderby q.nama
                                  select new
                                  {
                                      nama = q.nama,
                                      id = q.id
                                  }//select distinct cost centrenya
                                  ).Distinct().ToList();

            Dictionary<string, string> oneRow = new Dictionary<string, string>();
            int jumlahCSS = 0;
            int jumlahEstab = 0;
            int jumlahVacant = 0;
            foreach (var costCentre in listCostCentre)
            {
                int countCostCentre = (from o in db.users
                                        join p in db.users_jabatan on o.rm_role equals p.id
                                        join q in db.users_jabatan on p.cost_centre equals q.id
                                        where q.id == costCentre.id
                                        where (p.company_id == companyId) && (p.id > 0)
                                        select q.nama
                                    ).Count();

                List<int> listIdJabatan = (from o in db.users_jabatan
                                   where o.cost_centre == costCentre.id
                                      select o.id
                                      ).ToList();
                int estab = 0;
                int vacant = 0;
                foreach (int idJabatan in listIdJabatan)
                {
                    int jmlUserDariJabatan = db.users.Where(u => u.rm_role == idJabatan).Count();
                    if (jmlUserDariJabatan > 0) // kalo ada employee yg kerja di jabatan ini
                    {
                        estab += jmlUserDariJabatan;
                    }
                    else // kalo jabatan kosong
                    {
                        // vacant++;
                        countCostCentre++;
                    }
                }
                vacant = countCostCentre - estab;
                
                jumlahCSS += countCostCentre;
                jumlahEstab += estab;
                jumlahVacant += vacant;

                oneRow = new Dictionary<string, string>();
                oneRow["fungsi"] = costCentre.nama;
                oneRow["class-fungsi"] = "";
                oneRow["formasi"] = countCostCentre.ToString();
                oneRow["class-formasi"] = "";
                oneRow["estab"] = estab.ToString();
                oneRow["class-estab"] = "";
                oneRow["vacant"] = vacant.ToString();
                oneRow["class-vacant"] = "";

                resultTable.tableContent.Add(oneRow);
            }

            //isi footer table =========================================================================== //
            oneRow = new Dictionary<string, string>();
            oneRow["fungsi"] = "Total";
            oneRow["class-fungsi"] = "total";
            oneRow["formasi"] = jumlahCSS.ToString();
            oneRow["class-formasi"] = "total";
            oneRow["estab"] = jumlahEstab.ToString();
            oneRow["class-estab"] = "total";
            oneRow["vacant"] = jumlahVacant.ToString();
            oneRow["class-vacant"] = "total";

            resultTable.tableContent.Add(oneRow);

            return new Tuple<TableContainer, List<ChartContainer>>(resultTable, resultChart);
        }

        //Tabel X : Golongan, Tabel Y : Lahir
        //Chart X : Golongan, Chart Y : Usia
        public Tuple<TableContainer, List<ChartContainer>> getDemografiLahirGolongan(int companyId)
        {
            TableContainer resultTable = new TableContainer();
            List<ChartContainer> resultChart = new List<ChartContainer>();
            List<Tuple<string, DateTime, DateTime>> dLahir = demografiLahir();
            List<Tuple<string, DateTime, DateTime>> dUsia = demografiUsia();
            List<string> dGolongan = demografiGolongan();

            //isi header ================================================================================= //
            List<Dictionary<string, string>> oneRowHeader = new List<Dictionary<string, string>>();

            Dictionary<string, string> oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Tahun Lahir";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "colspan=" + dGolongan.Count(); oneCellHeader["text"] = "Golongan Upah";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Total";
            oneRowHeader.Add(oneCellHeader);

            resultTable.tableHeader.Add(oneRowHeader);

            oneRowHeader = new List<Dictionary<string, string>>();

            foreach (var golongan in dGolongan)
            {
                oneCellHeader = new Dictionary<string, string>();
                oneCellHeader["span"] = ""; oneCellHeader["text"] = golongan;
                oneRowHeader.Add(oneCellHeader);
            }

            resultTable.tableHeader.Add(oneRowHeader);

            ChartContainer singleChart = new ChartContainer();
            singleChart.title = "Usia VS Golongan";
            singleChart.legendTitle = "Usia Pekerja";
            singleChart.xTitle = "Golongan Pekerja";
            singleChart.yTitle = "Jumlah Pekerja";

            //isi content table ========================================================================== //
            Dictionary<string, string> oneRow = new Dictionary<string, string>();
            Dictionary<string, int> totalCol = new Dictionary<string, int>();
            Dictionary<string, int> totalRow = new Dictionary<string, int>();
            Dictionary<string, string> footerRow = new Dictionary<string, string>();

            //inisialisasi variabel yg nyimpen total dalem satu row, sekalian inisalisasi nama2 cell
            resultTable.cellsName.Add("tahunlahir");
            foreach (var golongan in dGolongan)
            {
                totalCol[golongan] = 0;
                resultTable.cellsName.Add("golongan" + golongan);
                singleChart.categories.Add(golongan);
            }
            resultTable.cellsName.Add("totalkanan");

            //inisialisasi variabel yg nyimpen total dalem satu column
            foreach (var lahir in dLahir)
            {
                totalRow[lahir.Item1] = 0;
            }

            foreach (var lahir in dLahir) //ngisi tabel ke bawah
            {
                oneRow = new Dictionary<string, string>();
                oneRow["tahunlahir"] = lahir.Item1;
                oneRow["class-tahunlahir"] = "";

                foreach (var golongan in dGolongan) //ngisi tabel ke kanan
                {
                    int countEmployee = (from o in db.users
                                         join p in db.users_jabatan on o.rm_role equals p.id
                                         where ((companyId == 1)?o.gol_upah_ap:o.gol_upah_persero) == golongan
                                         where ((o.tgl_lahir >= lahir.Item2) && (o.tgl_lahir < lahir.Item3))
                                         where p.company_id == companyId
                                         select p.nama
                                        ).Count();
                    totalCol[golongan] += countEmployee;
                    totalRow[lahir.Item1] += countEmployee;

                    oneRow["golongan" + golongan] = (countEmployee > 0)?countEmployee.ToString():"";
                    oneRow["class-golongan" + golongan] = "";
                }
                
                oneRow["totalkanan"] = totalRow[lahir.Item1].ToString();
                oneRow["class-totalkanan"] = "total";

                if (totalRow[lahir.Item1] > 0)
                    resultTable.tableContent.Add(oneRow);
            }

            foreach (var usia in dUsia) //ngisi chart ke bawah
            {
                List<int> seriesValue = new List<int>();

                foreach (var golongan in dGolongan) //ngisi chart ke kanan
                {
                    int countEmployee = (from o in db.users
                                         join p in db.users_jabatan on o.rm_role equals p.id
                                         where ((companyId == 1) ? o.gol_upah_ap : o.gol_upah_persero) == golongan
                                         where ((o.tgl_lahir >= usia.Item2) && (o.tgl_lahir < usia.Item3))
                                         where p.company_id == companyId
                                         select p.nama
                                        ).Count();
                    seriesValue.Add(countEmployee);
                }
                singleChart.series.Add(new Tuple<string, List<int>>(usia.Item1, seriesValue));
            }

            //isi footer table =========================================================================== //
            int totalSemua = 0;
            oneRow = new Dictionary<string, string>();
            oneRow["tahunlahir"] = "Total";
            foreach (var golongan in dGolongan)
            {
                oneRow["golongan" + golongan] = totalCol[golongan].ToString();
                oneRow["class-golongan" + golongan] = "total";
                totalSemua += totalCol[golongan];
                singleChart.seriesSum.Add(totalCol[golongan]);
            }
            oneRow["totalkanan"] = totalSemua.ToString();
            oneRow["class-tahunlahir"] = "total";
            oneRow["class-totalkanan"] = "total";

            resultTable.tableContent.Add(oneRow);
            resultChart.Add(singleChart);

            return new Tuple<TableContainer, List<ChartContainer>>(resultTable, resultChart);
        }

        //Tabel X : Pendidikan, Tabel Y : Lahir
        //Chart X : Pendidikan, Chart Y : Usia
        public Tuple<TableContainer, List<ChartContainer>> getDemografiLahirPendidikan(int companyId)
        {
            TableContainer resultTable = new TableContainer();
            List<ChartContainer> resultChart = new List<ChartContainer>();
            List<Tuple<string, DateTime, DateTime>> dLahir = demografiLahir();
            List<Tuple<string, DateTime, DateTime>> dUsia = demografiUsia();
            List<Tuple<string, string>> dPendidikan = demografiPendidikan();

            //isi header ================================================================================= //
            List<Dictionary<string, string>> oneRowHeader = new List<Dictionary<string, string>>();

            Dictionary<string, string> oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Tahun Lahir";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "colspan=" + dPendidikan.Count(); oneCellHeader["text"] = "Pendidikan Terakhir";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Total";
            oneRowHeader.Add(oneCellHeader);

            resultTable.tableHeader.Add(oneRowHeader);

            oneRowHeader = new List<Dictionary<string, string>>();

            foreach (var pendidikan in dPendidikan)
            {
                oneCellHeader = new Dictionary<string, string>();
                oneCellHeader["span"] = ""; oneCellHeader["text"] = pendidikan.Item1;
                oneRowHeader.Add(oneCellHeader);
            }

            resultTable.tableHeader.Add(oneRowHeader);

            ChartContainer singleChart = new ChartContainer();
            singleChart.title = "Usia VS Pendidikan";
            singleChart.legendTitle = "Usia Pekerja";
            singleChart.xTitle = "Pendidikan Pekerja";
            singleChart.yTitle = "Jumlah Pekerja";

            //isi content table ========================================================================== //
            Dictionary<string, string> oneRow = new Dictionary<string, string>();
            Dictionary<string, int> totalCol = new Dictionary<string, int>();
            Dictionary<string, int> totalRow = new Dictionary<string, int>();
            Dictionary<string, string> footerRow = new Dictionary<string, string>();

            //inisialisasi variabel yg nyimpen total dalem satu row, sekalian inisalisasi nama2 cell
            resultTable.cellsName.Add("tahunlahir");
            foreach (var pendidikan in dPendidikan)
            {
                totalCol[pendidikan.Item1] = 0;
                resultTable.cellsName.Add("pendidikan" + pendidikan.Item1);
                singleChart.categories.Add(pendidikan.Item1);
            }
            resultTable.cellsName.Add("totalkanan");

            //inisialisasi variabel yg nyimpen total dalem satu column
            foreach (var lahir in dLahir)
            {
                totalRow[lahir.Item1] = 0;
            }

            foreach (var lahir in dLahir) //ngisi tabel ke bawah
            {
                oneRow = new Dictionary<string, string>();
                oneRow["tahunlahir"] = lahir.Item1;
                oneRow["class-tahunlahir"] = "";

                foreach (var pendidikan in dPendidikan) //ngisi tabel ke kanan
                {
                    int countEmployee = (from o in db.users
                                         join p in db.users_jabatan on o.rm_role equals p.id
                                         where o.pendidikan_terakhir == pendidikan.Item2
                                         where ((o.tgl_lahir >= lahir.Item2) && (o.tgl_lahir < lahir.Item3))
                                         where p.company_id == companyId
                                         select p.nama
                                        ).Count();
                    totalCol[pendidikan.Item1] += countEmployee;
                    totalRow[lahir.Item1] += countEmployee;

                    oneRow["pendidikan" + pendidikan.Item1] = (countEmployee > 0) ? countEmployee.ToString() : "";
                    oneRow["class-pendidikan" + pendidikan.Item1] = "";
                }

                oneRow["totalkanan"] = totalRow[lahir.Item1].ToString();
                oneRow["class-totalkanan"] = "total";

                if (totalRow[lahir.Item1] > 0)
                    resultTable.tableContent.Add(oneRow);
            }

            foreach (var usia in dUsia) //ngisi chart ke bawah
            {
                List<int> seriesValue = new List<int>();

                foreach (var pendidikan in dPendidikan) //ngisi chart ke kanan
                {
                    int countEmployee = (from o in db.users
                                         join p in db.users_jabatan on o.rm_role equals p.id
                                         where o.pendidikan_terakhir == pendidikan.Item2
                                         where ((o.tgl_lahir >= usia.Item2) && (o.tgl_lahir < usia.Item3))
                                         where p.company_id == companyId
                                         select p.nama
                                        ).Count();
                    seriesValue.Add(countEmployee);
                }
                singleChart.series.Add(new Tuple<string, List<int>>(usia.Item1, seriesValue));
            }

            //isi footer table =========================================================================== //
            int totalSemua = 0;
            oneRow = new Dictionary<string, string>();
            oneRow["tahunlahir"] = "Total";
            foreach (var pendidikan in dPendidikan)
            {
                oneRow["pendidikan" + pendidikan.Item1] = totalCol[pendidikan.Item1].ToString();
                oneRow["class-pendidikan" + pendidikan.Item1] = "total";
                totalSemua += totalCol[pendidikan.Item1];
                singleChart.seriesSum.Add(totalCol[pendidikan.Item1]);
            }
            oneRow["totalkanan"] = totalSemua.ToString();
            oneRow["class-tahunlahir"] = "total";
            oneRow["class-totalkanan"] = "total";

            resultTable.tableContent.Add(oneRow);
            resultChart.Add(singleChart);

            return new Tuple<TableContainer, List<ChartContainer>>(resultTable, resultChart);
        }

        //Tabel X : Pendidikan, Tabel Y : Golongan
        //Chart X : Golongan, Chart Y : Pendidikan
        public Tuple<TableContainer, List<ChartContainer>> getDemografiGolonganPendidikan(int companyId)
        {
            TableContainer resultTable = new TableContainer();
            List<ChartContainer> resultChart = new List<ChartContainer>();
            List<string> dGolongan = demografiGolongan();
            List<Tuple<string, string>> dPendidikan = demografiPendidikan();

            //isi header ================================================================================= //
            List<Dictionary<string, string>> oneRowHeader = new List<Dictionary<string, string>>();

            Dictionary<string, string> oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Golongan Upah";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "colspan=" + dPendidikan.Count(); oneCellHeader["text"] = "Pendidikan Terakhir";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Total";
            oneRowHeader.Add(oneCellHeader);

            resultTable.tableHeader.Add(oneRowHeader);

            oneRowHeader = new List<Dictionary<string, string>>();

            foreach (var pendidikan in dPendidikan)
            {
                oneCellHeader = new Dictionary<string, string>();
                oneCellHeader["span"] = ""; oneCellHeader["text"] = pendidikan.Item1;
                oneRowHeader.Add(oneCellHeader);
            }

            resultTable.tableHeader.Add(oneRowHeader);

            ChartContainer singleChart = new ChartContainer();
            singleChart.title = "Golongan VS Pendidikan";
            singleChart.legendTitle = "Strata Pendidikan";
            singleChart.xTitle = "Golongan Pekerja";
            singleChart.yTitle = "Jumlah Pekerja";

            //isi content table ========================================================================== //
            Dictionary<string, string> oneRow = new Dictionary<string, string>();
            Dictionary<string, int> totalCol = new Dictionary<string, int>();
            Dictionary<string, int> totalRow = new Dictionary<string, int>();
            Dictionary<string, string> footerRow = new Dictionary<string, string>();
            Dictionary<string, List<int>> seriesValue = new Dictionary<string, List<int>>();

            //inisialisasi variabel yg nyimpen total dalem satu row, sekalian inisalisasi nama2 cell
            resultTable.cellsName.Add("golongan");
            foreach (var pendidikan in dPendidikan)
            {
                totalCol[pendidikan.Item1] = 0;
                resultTable.cellsName.Add("pendidikan" + pendidikan.Item1);
                seriesValue[pendidikan.Item1] = new List<int>();
            }
            resultTable.cellsName.Add("totalkanan");

            //inisialisasi variabel yg nyimpen total dalem satu column
            foreach (var golongan in dGolongan)
            {
                totalRow[golongan] = 0;
                singleChart.categories.Add(golongan);
            }

            foreach (var golongan in dGolongan) //ngisi tabel ke bawah
            {
                oneRow = new Dictionary<string, string>();
                oneRow["golongan"] = golongan;
                oneRow["class-golongan"] = "";

                foreach (var pendidikan in dPendidikan) //ngisi tabel ke kanan
                {
                    int countEmployee = (from o in db.users
                                         join p in db.users_jabatan on o.rm_role equals p.id
                                         where o.pendidikan_terakhir == pendidikan.Item2
                                         where ((companyId == 1) ? o.gol_upah_ap : o.gol_upah_persero) == golongan
                                         where p.company_id == companyId
                                         select p.nama
                                        ).Count();
                    totalCol[pendidikan.Item1] += countEmployee;
                    totalRow[golongan] += countEmployee;

                    seriesValue[pendidikan.Item1].Add(countEmployee);

                    oneRow["pendidikan" + pendidikan.Item1] = (countEmployee > 0) ? countEmployee.ToString() : "";
                    oneRow["class-pendidikan" + pendidikan.Item1] = "";
                }

                singleChart.seriesSum.Add(totalRow[golongan]);
                oneRow["totalkanan"] = totalRow[golongan].ToString();
                oneRow["class-totalkanan"] = "total";
                resultTable.tableContent.Add(oneRow); 

            }

            //isi footer table =========================================================================== //
            int totalSemua = 0;
            oneRow = new Dictionary<string, string>();
            oneRow["golongan"] = "Total";
            foreach (var pendidikan in dPendidikan)
            {
                oneRow["pendidikan" + pendidikan.Item1] = totalCol[pendidikan.Item1].ToString();
                oneRow["class-pendidikan" + pendidikan.Item1] = "total";
                totalSemua += totalCol[pendidikan.Item1];
                singleChart.series.Add(new Tuple<string, List<int>>(pendidikan.Item1, seriesValue[pendidikan.Item1]));
            }
            oneRow["totalkanan"] = totalSemua.ToString();
            oneRow["class-golongan"] = "total";
            oneRow["class-totalkanan"] = "total";

            resultTable.tableContent.Add(oneRow);
            resultChart.Add(singleChart);

            return new Tuple<TableContainer, List<ChartContainer>>(resultTable, resultChart);
        }

        //Tabel X : MPPK, Tabel Y : Golongan
        //Chart X : Golongan, Chart Y : MPPK
        public Tuple<TableContainer, List<ChartContainer>> getDemografiGolonganMPPK(int companyId)
        {
            TableContainer resultTable = new TableContainer();
            List<ChartContainer> resultChart = new List<ChartContainer>();
            List<string> dGolongan = demografiGolongan();
            List<Tuple<string, DateTime, DateTime>> dMPPK = demografiMPPK();

            //isi header ================================================================================= //
            List<Dictionary<string, string>> oneRowHeader = new List<Dictionary<string, string>>();

            Dictionary<string, string> oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Golongan Upah";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Jumlah Pek. Saat Ini";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "colspan=" + dMPPK.Count(); oneCellHeader["text"] = "Jumlah Pekerja MPPK Pada Tahun";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2 colspan=2"; oneCellHeader["text"] = "Total Pekerja MPPK s/d Tahun " + dMPPK.Last().Item1;
            oneRowHeader.Add(oneCellHeader);

            resultTable.tableHeader.Add(oneRowHeader);

            oneRowHeader = new List<Dictionary<string, string>>();

            foreach (var MPPK in dMPPK)
            {
                oneCellHeader = new Dictionary<string, string>();
                oneCellHeader["span"] = ""; oneCellHeader["text"] = MPPK.Item1;
                oneRowHeader.Add(oneCellHeader);
            }

            resultTable.tableHeader.Add(oneRowHeader);

            //isi content table ========================================================================== //
            Dictionary<string, string> oneRow = new Dictionary<string, string>();
            Dictionary<string, int> totalCol = new Dictionary<string, int>();
            Dictionary<string, int> totalRow = new Dictionary<string, int>();
            Dictionary<string, int> cellValue = new Dictionary<string, int>();
            Dictionary<string, string> footerRow = new Dictionary<string, string>();
            Dictionary<string, List<int>> seriesValue = new Dictionary<string, List<int>>();
            
            //inisialisasi variabel yg nyimpen total dalem satu row, sekalian inisalisasi nama2 cell
            resultTable.cellsName.Add("golongan");
            resultTable.cellsName.Add("jumlahpek");
            foreach (var MPPK in dMPPK)
            {
                totalCol[MPPK.Item1] = 0;
                resultTable.cellsName.Add("MPPK" + MPPK.Item1);
                seriesValue[MPPK.Item1] = new List<int>();
            }
            resultTable.cellsName.Add("totalkanan");
            resultTable.cellsName.Add("persenkanan");

            //inisialisasi variabel yg nyimpen total dalem satu column
            int totalSemuaPek = 0;
            foreach (var golongan in dGolongan)
            {
                totalRow[golongan] = 0;
                totalSemuaPek += (from o in db.users
                                  join p in db.users_jabatan on o.rm_role equals p.id
                                  where o.gol_upah_persero == golongan
                                  where p.company_id == companyId
                                  select p.nama
                                 ).Count();
            }

            totalCol["jumlahpek"] = 0;
            foreach (var golongan in dGolongan) //ngisi tabel ke bawah
            {
                oneRow = new Dictionary<string, string>();
                oneRow["golongan"] = golongan;
                oneRow["class-golongan"] = "";

                int jumlahPek = (from o in db.users
                                 join p in db.users_jabatan on o.rm_role equals p.id
                                 where o.gol_upah_persero == golongan
                                 where p.company_id == companyId
                                 select p.nama
                                        ).Count();
                oneRow["jumlahpek"] = jumlahPek.ToString();
                oneRow["class-jumlahpek"] = "";
                totalCol["jumlahpek"] += jumlahPek;

                foreach (var MPPK in dMPPK) //ngisi tabel ke kanan
                {
                    int countEmployee = (from o in db.users
                                         join p in db.users_jabatan on o.rm_role equals p.id
                                         where ((o.tgl_mppk >= MPPK.Item2) && (o.tgl_mppk < MPPK.Item3))
                                         where ((companyId == 1) ? o.gol_upah_ap : o.gol_upah_persero) == golongan
                                         where p.company_id == companyId
                                         select p.nama
                                        ).Count();
                    totalCol[MPPK.Item1] += countEmployee;
                    totalRow[golongan] += countEmployee;
                    cellValue[golongan + MPPK.Item1] = countEmployee;

                    seriesValue[MPPK.Item1].Add(countEmployee);

                    oneRow["MPPK" + MPPK.Item1] = (countEmployee > 0) ? countEmployee.ToString() : "";
                    oneRow["class-MPPK" + MPPK.Item1] = "";
                }

                oneRow["totalkanan"] = totalRow[golongan].ToString();
                oneRow["class-totalkanan"] = "";
                oneRow["persenkanan"] = (totalSemuaPek != 0) ? ((float)totalRow[golongan] / totalSemuaPek * 100).ToString("0.00") + "%" : "-";
                oneRow["class-persenkanan"] = "";
                resultTable.tableContent.Add(oneRow);
            }

            //isi footer table =========================================================================== //
            int totalSemua = 0;
            oneRow = new Dictionary<string, string>();
            oneRow["golongan"] = "Total";
            oneRow["class-golongan"] = "total";
            oneRow["jumlahpek"] = totalCol["jumlahpek"].ToString();
            oneRow["class-jumlahpek"] = "total";
            foreach (var MPPK in dMPPK)
            {
                oneRow["MPPK" + MPPK.Item1] = totalCol[MPPK.Item1].ToString();
                oneRow["class-MPPK" + MPPK.Item1] = "total";
                totalSemua += totalCol[MPPK.Item1];

                ChartContainer singleChart = new ChartContainer();
                singleChart.title = "Jumlah Pekerja MPPK Pada Tahun " + MPPK.Item1;
                singleChart.legendTitle = "Tahun";
                singleChart.xTitle = "Golongan Pekerja";
                singleChart.yTitle = "Jumlah Pekerja";
                foreach (var golongan in dGolongan)
                {
                    singleChart.categories.Add(golongan);
                    singleChart.seriesSum.Add(cellValue[golongan + MPPK.Item1]);
                }
                singleChart.series.Add(new Tuple<string, List<int>>(MPPK.Item1, seriesValue[MPPK.Item1]));
                resultChart.Add(singleChart);
            }
            oneRow["totalkanan"] = totalSemua.ToString();
            oneRow["class-totalkanan"] = "total";
            oneRow["persenkanan"] = (totalCol["jumlahpek"] != 0) ? ((float)totalSemua / totalCol["jumlahpek"] * 100).ToString("0.00") + "%" : "-";
            oneRow["class-persenkanan"] = "total";

            resultTable.tableContent.Add(oneRow);
            
            return new Tuple<TableContainer, List<ChartContainer>>(resultTable, resultChart);
        }

        //Tabel X : MPPK, Tabel Y : Pendidikan
        //Chart X : Pendidikan, Chart Y : MPPK
        public Tuple<TableContainer, List<ChartContainer>> getDemografiPendidikanMPPK(int companyId)
        {
            TableContainer resultTable = new TableContainer();
            List<ChartContainer> resultChart = new List<ChartContainer>();
            List<Tuple<string, string>> dPendidikan = demografiPendidikan();
            List<Tuple<string, DateTime, DateTime>> dMPPK = demografiMPPK();

            //isi header ================================================================================= //
            List<Dictionary<string, string>> oneRowHeader = new List<Dictionary<string, string>>();

            Dictionary<string, string> oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Pendidikan Terakhir";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2"; oneCellHeader["text"] = "Jumlah Pek. Saat Ini";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "colspan=" + dMPPK.Count(); oneCellHeader["text"] = "Jumlah Pekerja MPPK Pada Tahun";
            oneRowHeader.Add(oneCellHeader);

            oneCellHeader = new Dictionary<string, string>();
            oneCellHeader["span"] = "rowspan=2 colspan=2"; oneCellHeader["text"] = "Total Pekerja MPPK s/d Tahun " + dMPPK.Last().Item1;
            oneRowHeader.Add(oneCellHeader);

            resultTable.tableHeader.Add(oneRowHeader);

            oneRowHeader = new List<Dictionary<string, string>>();

            foreach (var MPPK in dMPPK)
            {
                oneCellHeader = new Dictionary<string, string>();
                oneCellHeader["span"] = ""; oneCellHeader["text"] = MPPK.Item1;
                oneRowHeader.Add(oneCellHeader);
            }

            resultTable.tableHeader.Add(oneRowHeader);

            //isi content table ========================================================================== //
            Dictionary<string, string> oneRow = new Dictionary<string, string>();
            Dictionary<string, int> totalCol = new Dictionary<string, int>();
            Dictionary<string, int> totalRow = new Dictionary<string, int>();
            Dictionary<string, int> cellValue = new Dictionary<string, int>();
            Dictionary<string, string> footerRow = new Dictionary<string, string>();
            Dictionary<string, List<int>> seriesValue = new Dictionary<string, List<int>>();

            //inisialisasi variabel yg nyimpen total dalem satu row, sekalian inisalisasi nama2 cell
            resultTable.cellsName.Add("pendidikan");
            resultTable.cellsName.Add("jumlahpek");
            foreach (var MPPK in dMPPK)
            {
                totalCol[MPPK.Item1] = 0;
                resultTable.cellsName.Add("MPPK" + MPPK.Item1);
                seriesValue[MPPK.Item1] = new List<int>();
            }
            resultTable.cellsName.Add("totalkanan");
            resultTable.cellsName.Add("persenkanan");

            //inisialisasi variabel yg nyimpen total dalem satu column
            int totalSemuaPek = 0;
            foreach (var pendidikan in dPendidikan)
            {
                totalRow[pendidikan.Item1] = 0;
                totalSemuaPek += (from o in db.users
                                 join p in db.users_jabatan on o.rm_role equals p.id
                                 where o.pendidikan_terakhir == pendidikan.Item2
                                 where p.company_id == companyId
                                 select p.nama
                                 ).Count();
            }

            totalCol["jumlahpek"] = 0;
            foreach (var pendidikan in dPendidikan) //ngisi tabel ke bawah
            {
                oneRow = new Dictionary<string, string>();
                oneRow["pendidikan"] = pendidikan.Item1;
                oneRow["class-pendidikan"] = "";

                int jumlahPek = (from o in db.users
                                 join p in db.users_jabatan on o.rm_role equals p.id
                                 where o.pendidikan_terakhir == pendidikan.Item2
                                 where p.company_id == companyId
                                 select p.nama
                                        ).Count();
                oneRow["jumlahpek"] = jumlahPek.ToString();
                oneRow["class-jumlahpek"] = "";
                totalCol["jumlahpek"] += jumlahPek;

                foreach (var MPPK in dMPPK) //ngisi tabel ke kanan
                {
                    int countEmployee = (from o in db.users
                                         join p in db.users_jabatan on o.rm_role equals p.id
                                         where ((o.tgl_mppk >= MPPK.Item2) && (o.tgl_mppk < MPPK.Item3))
                                         where o.pendidikan_terakhir == pendidikan.Item2
                                         where p.company_id == companyId
                                         select p.nama
                                        ).Count();
                    totalCol[MPPK.Item1] += countEmployee;
                    totalRow[pendidikan.Item1] += countEmployee;
                    cellValue[pendidikan.Item1 + MPPK.Item1] = countEmployee;

                    seriesValue[MPPK.Item1].Add(countEmployee);

                    oneRow["MPPK" + MPPK.Item1] = (countEmployee > 0) ? countEmployee.ToString() : "";
                    oneRow["class-MPPK" + MPPK.Item1] = "";
                }

                oneRow["totalkanan"] = totalRow[pendidikan.Item1].ToString();
                oneRow["class-totalkanan"] = "";
                oneRow["persenkanan"] = (totalSemuaPek != 0) ? ((float)totalRow[pendidikan.Item1] / totalSemuaPek * 100).ToString("0.00") + "%" : "-";
                oneRow["class-persenkanan"] = "";
                resultTable.tableContent.Add(oneRow);
            }

            //isi footer table =========================================================================== //
            int totalSemua = 0;
            oneRow = new Dictionary<string, string>();
            oneRow["pendidikan"] = "Total";
            oneRow["class-pendidikan"] = "total";
            oneRow["jumlahpek"] = totalCol["jumlahpek"].ToString();
            oneRow["class-jumlahpek"] = "total";
            foreach (var MPPK in dMPPK)
            {
                oneRow["MPPK" + MPPK.Item1] = totalCol[MPPK.Item1].ToString();
                oneRow["class-MPPK" + MPPK.Item1] = "total";
                totalSemua += totalCol[MPPK.Item1];

                ChartContainer singleChart = new ChartContainer();
                singleChart.title = "Jumlah Pekerja MPPK Pada Tahun " + MPPK.Item1;
                singleChart.legendTitle = "Tahun";
                singleChart.xTitle = "Pendidikan Pekerja";
                singleChart.yTitle = "Jumlah Pekerja";
                foreach (var pendidikan in dPendidikan)
                {
                    singleChart.categories.Add(pendidikan.Item1);
                    singleChart.seriesSum.Add(cellValue[pendidikan.Item1 + MPPK.Item1]);
                }
                singleChart.series.Add(new Tuple<string, List<int>>(MPPK.Item1, seriesValue[MPPK.Item1]));
                resultChart.Add(singleChart);
            }
            oneRow["totalkanan"] = totalSemua.ToString();
            oneRow["class-totalkanan"] = "total";
            oneRow["persenkanan"] = (totalCol["jumlahpek"] != 0) ? ((float)totalSemua / totalCol["jumlahpek"] * 100).ToString("0.00") + "%" : "-";
            oneRow["class-persenkanan"] = "total";

            resultTable.tableContent.Add(oneRow);

            return new Tuple<TableContainer, List<ChartContainer>>(resultTable, resultChart);
        }

    }
}
