using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Utilities.KpiCalculation
{
    public class NilaiKinerjaPertumbuhan
    {
        public double ASPG;
        public double SALG;
        public double NPMG;
        public double STAG;
        public double NPG;
        public double NKP;
        public string TingkatKinerjaPertumbuhan;

        public double KalkulasiData(double param1, double param2, double param3, double param4)
        {
            return (((param1 - param2) / param3) / ((param1 - param2) / param4) * 100) - 100;
        }

        public double KalkulasiData(double param1, double param2) {
            return (param1 / param2 * 100) - 100;
        }

        public void KalkulasiTingkatKinerjaPertumbuhan()
        {
            if (NKP < 7)
                TingkatKinerjaPertumbuhan = "Kurang Tumbuh";
            else if (NKP >= 7 && NKP <= 13)
                TingkatKinerjaPertumbuhan = "Tumbuh Sedang";
            else if (NKP > 13)
                TingkatKinerjaPertumbuhan = "Tumbuh Tinggi";
        }   
    }
}