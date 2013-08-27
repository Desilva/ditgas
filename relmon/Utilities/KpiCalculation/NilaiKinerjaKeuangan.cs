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

        public double KalkulasiData(double pembilang, double penyebut,double satuan) {
            return pembilang / penyebut * satuan;
        }

        public void SumNKK() {
            NKK = ROE + ROI + OPM + NPM + CashRatio + CurrentRatio + CP + ITO + TATO + ETTA + TIER;
        }

        public void KalkulasiTingkatKinerjaKeuangan() {
            if (NKK <= 20)
                TingkatKinerjaKeuangan = "Tidak Sehat";
            else if (NKK > 20 && NKK <= 45)
                TingkatKinerjaKeuangan = "Kurang Sehat";
            else if (NKK > 45)
                TingkatKinerjaKeuangan = "Sehat";
        }   
    }
}