using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace relmon.Utilities.KpiCalculation
{
    public class NilaiTingkatKesehatan
    {
        public string TingkatKesehatan;

        public static string HitungNilaiTingkatKesehatan(double NKA, double NKP, double NKK)
        {
            double total = NKA + NKP + NKK;
            var TingkatKesehatan = "";
            if (total <= 10)
                TingkatKesehatan = "C";
            else if (total > 10 && total <= 20)
                TingkatKesehatan = "CC";
            else if (total > 20 && total <= 30)
                TingkatKesehatan = "CCC";
            else if (total > 30 && total <= 40)
                TingkatKesehatan = "B";
            else if (total > 40 && total <= 50)
                TingkatKesehatan = "BB";
            else if (total > 50 && total <= 65)
                TingkatKesehatan = "BBB";
            else if (total > 65 && total <= 80)
                TingkatKesehatan = "A";
            else if (total > 80 && total <= 95)
                TingkatKesehatan = "AA";
            else
                TingkatKesehatan = "AAA";

            return TingkatKesehatan;
        }
    }
}