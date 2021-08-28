namespace PRDCT.Core.TEST
{
    public static class ButtonsIni
    {

        public static int NumButt = 22; // number of simple buttons

        public static string[] HintButt = new string[NumButt];
        public static string[] SpecButt = new string[NumButt];
        public static byte[] LongButt = new byte[NumButt];
        public static bool SkyBooX;


        public static void ForSunHightButton(char ch, double alt)
        { // SpecButton
            string st = alt.ToString();// Str(alt:5:1, st); // altitude to string
            switch (ch)
            {
                case 'D':
                    SpecButt[21] = "  день  ";
                    HintButt[21] = "высота Солнца " + st + "°";
                    break;
                case 'T':
                    SpecButt[21] = "сумерки ";
                    HintButt[21] = "высота Солнца " + st + "°";
                    break;
                case 'N':
                    SpecButt[21] = "  ночь  ";
                    HintButt[21] = "высота Солнца " + st + "°";
                    break;
            } // case  ch
        }


    }
}
