using System;

namespace Epic
{
    public class OPENF
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public OPENF()
        {
            // EPIC0810
            // Translated by Paul Cain
            // THIS SUBPROGRAM OPENS FILES.

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            //Translator's Note: For now, I kept all of the array indices as 
            //	the same numbers they were in their original fortran files  
            //	even though C# use base 0 arrays and fortran use base 1 
            //	arrays. I did this to avoid confusion until we have a better
            // 	understanding of how the program works. 

            //Translator's Note: I kept all the fortran I/O code in this file 
            //  untranslated and commented out until we decide how to handle 
            //  file I/O.

            // USE PARM
            String[] AXT = new String[29] {".OUT",".ACM",".SUM",".DHY",".DPS",
                ".MFS",".MPS",".ANN",".SOT",".DTP",".MCM",".DCS",".SCO",".ACN",
                ".DCN",".SCN",".DGN",".DWT",".ACY",".ACO",".DSL",".MWC",".ABR",
                ".ATG",".MSW",".APS",".DWC",".DHS",".DGZ"};
            for (int I = 1; I <= (PARM.MSO - 1); I++)
            {
                if (AXT[I] != "    " && PARM.KFL[I] > 0)
                {
                    //OPEN(KW(I),FILE=ASTN//AXT(I));
                }
            }
            return;
        }
    }
}
