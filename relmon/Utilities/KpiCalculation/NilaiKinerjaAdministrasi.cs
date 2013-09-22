using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Utilities.KpiCalculation
{
    public class NilaiKinerjaAdministrasi
    {
        public double LaporanKeuanganBulanan;
        public double LaporanManagemenBulanan;
        public double LaporanKeuanganAudited;
        public double RancanganRKAP;
        public double NKA;
        public string TingkatKinerjaAdministrasi;

        public static double KalkulasiLaporanKeuanganBulanan(int hariKalendar) {
            var LaporanKeuanganBulanan = 0.0;
            if (hariKalendar <= 7)
                LaporanKeuanganBulanan = 2;
            else if (hariKalendar > 7 && hariKalendar <= 9)
                LaporanKeuanganBulanan = 1.5;
            else if (hariKalendar == 10)
                LaporanKeuanganBulanan = 1;
            else if (hariKalendar > 10 && hariKalendar <= 12)
                LaporanKeuanganBulanan = 0;
            else
                LaporanKeuanganBulanan = -1;
            return LaporanKeuanganBulanan;
        }

        public static double KalkulasiLaporanManagemenBulanan(int hariKalendar)
        {
            var LaporanManagemenBulanan = 0.0;
            if (hariKalendar <= 11)
                LaporanManagemenBulanan = 2;
            else if (hariKalendar > 11 && hariKalendar <= 14)
                LaporanManagemenBulanan = 1.5;
            else if (hariKalendar == 15)
                LaporanManagemenBulanan = 1;
            else if (hariKalendar > 15 && hariKalendar <= 20)
                LaporanManagemenBulanan = 0;
            else
                LaporanManagemenBulanan = -1;
            return LaporanManagemenBulanan;
        }

        public static double KalkulasiLaporanKeuanganAudited(int bulan)
        {
            var LaporanKeuanganAudited = 0.0;
            
            if (bulan <= 3)
                LaporanKeuanganAudited = 3;
            else if (bulan == 4)
                LaporanKeuanganAudited = 2;
            else if (bulan == 5)
                LaporanKeuanganAudited = 1;
            else if (bulan == 6)
                LaporanKeuanganAudited = 0;
            else
                LaporanKeuanganAudited = -1;
            return LaporanKeuanganAudited;
        }

        public static double KalkulasiRancanganRKAP(int bulan)
        {
            var RancanganRKAP = 0.0;
            if (bulan <= 8)
                RancanganRKAP = 3;
            else if (bulan == 9)
                RancanganRKAP = 2;
            else if (bulan == 10)
                RancanganRKAP = 1;
            else if (bulan >= 11)
                RancanganRKAP = -1;
            return RancanganRKAP;
        }

        public static double KalkulasiNKA(double LaporanKeuanganBulanan,double LaporanManagemenBulanan,double LaporanKeuanganAudited,double RancanganRKAP)
        {
            var NKA = LaporanKeuanganBulanan + LaporanManagemenBulanan + LaporanKeuanganAudited + RancanganRKAP;
            return NKA;
        }

        public static string  KalkulasiTingkatKinerjaAdministrasi(double NKA) {
            var TingkatKinerjaAdministrasi = "";
            if (NKA < 4)
                TingkatKinerjaAdministrasi = "Tidak Tertib";
            else if (NKA >= 4 && NKA < 7)
                TingkatKinerjaAdministrasi = "Kurang Tertib";
            else if (NKA >= 7)
                TingkatKinerjaAdministrasi = "Tertib";
            return TingkatKinerjaAdministrasi;
        }
    }
}