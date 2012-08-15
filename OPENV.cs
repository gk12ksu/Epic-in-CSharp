using System;

namespace Epic
{
    public class OPENV
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public OPENV(int NUM, string FNAM, int ID, int NMSO)
        {
            // EPIC0810
            // Translated by Paul Cain
            // VERIFIES THE EXISTENCE OF A FILE BEFORE OPENING IT

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
            String ADIR, FNM;
            Boolean XMIS;
            int JRT = 0;
            switch (ID)
            {
                case 1:
                    ADIR="C:\\WEATDATA\\";
                    FNM = ADJUSTR(ADIR) + ADJUSTL(FNAM);
                    break;
                case 2:
                    ADIR="C:\\SITE\\";
                    FNM = ADJUSTR(ADIR) + ADJUSTL(FNAM);
                    break;
                case 3:
                    ADIR="C:\\SOIL\\";
                    FNM = ADJUSTR(ADIR) + ADJUSTL(FNAM);
                    break;
                case 4:
                    ADIR="C:\\OPSC\\";
                    FNM = ADJUSTR(ADIR) + ADJUSTL(FNAM);
                    break;
                default:
                    FNM = FNAM;
                    break;
            }
            if (System.IO.File.Exists(FNM))
            {
                //OPEN(NUM,FILE=FNM)
            }
            else
            {
                if (PARM.IBAT == 0)
                {
                    // Translator's Note:
                    // I think this part display a message to the console
                    // then pauses the program until the user resumes it.
                    // See: http://ram3.chem.sunysb.edu/nucwww/helplib/@help/FORTRAN/STATEMENTS/PAUSE.html
                    // Fortran:
                    // WRITE(*,'(/A/)')'File '//TRIM(FNM)//' IS MISSING.'
                    // PAUSE
                    //My version of the two lines above:
                    Console.WriteLine("File " + FNM.Trim() + " IS MISSING");
                    Console.WriteLine("Press any key to resume");
                    Console.ReadLine();
                }
                else
                {
                    //WRITE(NMSO,'(A,A8,1X,A36,A)')' !!!!! ',ASTN,TRIM(FNM),&
                    //&' IS MISSING.'
                    Environment.Exit(1);
                }
            }
            return;
        }

        private static string ADJUSTL(String str)
        {
            String trimmedStr = str.TrimStart();
            int spacesToAdd = str.Length - trimmedStr.Length;
            return trimmedStr.PadRight(str.Length);
        }

        private static string ADJUSTR(String str)
        {
            String trimmedStr = str.TrimEnd();
            return trimmedStr.PadLeft(str.Length);
        }
    }
}