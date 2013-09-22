using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Utilities.KpiCalculation
{
    public class NilaiKinerjaKeuangan
    {
        public double ROE;
        public double ROI;
        public double OPM;
        public double NPM;
        public double CashRatio;
        public double CurrentRatio;
        public double CP;
        public double ITO;
        public double TATO;
        public double ETTA;
        public double TIER;
        public double NKK;
        public string TingkatKinerjaKeuangan;

        public static double KalkulasiData(double pembilang, double penyebut,double satuan) {
            return pembilang / penyebut * satuan;
        }

        public static double SumNKK(double ROE, double ROI, double OPM, double NPM, double CashRatio, double CurrentRatio, double CP, double ITO, double TATO, double ETTA, double TIER)
        {
            var NKK = ROE + ROI + OPM + NPM + CashRatio + CurrentRatio + CP + ITO + TATO + ETTA + TIER;
            return NKK;
        }

        public static string KalkulasiTingkatKinerjaKeuangan(double NKK) {
            string TingkatKinerjaKeuangan = "";
            if (NKK <= 20)
                TingkatKinerjaKeuangan = "Tidak Sehat";
            else if (NKK > 20 && NKK <= 45)
                TingkatKinerjaKeuangan = "Kurang Sehat";
            else if (NKK > 45)
                TingkatKinerjaKeuangan = "Sehat";

            return TingkatKinerjaKeuangan;
        }   
    }
}