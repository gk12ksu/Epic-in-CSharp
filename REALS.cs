using System;

namespace Epic
{
    public class REALS
    {
        private static MODPARAM PARM = MODPARAM.Instance;
        private static double[,] XRTS = new double[7, 15];
        public REALS()
        {
            // EPIC0810
            // Translated by Paul Cain
            // THIS SUBPROGRAM ALLOWS REAL TIME UPDATES OF SOIL VARIABLES

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
            int IYCD = 1;
            //WRITE(KW(1),120)(ST(LID(L)),L=1,NBSL)
            //WRITE(KW(1),120)(STMP(LID(L)),L=1,NBSL)
            //WRITE(KW(1),120)(WNO3(LID(L)),L=1,NBSL)
            //WRITE(KW(1),120)(WNH3(LID(L)),L=1,NBSL)
            //WRITE(KW(1),120)(AP(LID(L)),L=1,NBSL)
            //WRITE(KW(1),120)(EXCK(LID(L)),L=1,NBSL)
            //WRITE(KW(1),120)(WSLT(LID(L)),L=1,NBSL)
            if (PARM.ISTP == 2)
            {
                Two_loop();
            }
            else if (PARM.ISTP == 0)
            {
                //198 REWIND KW(MSO+3)
                if (PARM.ISX != 0)
                {
                    PARM.ISTP = 1;
                    //WRITE(KW(MSO+3),9)ISTP
                }
                PARM.ISX = 1;
                if (IYCD != 0)
                {
                    for (int I = 2; I <= 8; I++)
                    {
                        PARM.IYS[I] = 1;
                    }
                }
                //WRITE(KW(MSO+3),9)(IYS(I),I=2,8)
                PARM.RZSW = 0.0;
                PARM.PAW = 0.0;
                double Z1 = 0.0;
                for (int J = 1; J <= PARM.NBSL;J++)
                {
                    int L = PARM.LID[J];
                    double X1 = PARM.ST[L] - PARM.S15[L];
                    double X4 = 0.001 * X1 / (PARM.Z[L] - Z1);
                    double X2 = PARM.FC[L] - PARM.S15[L];
                    double X3=X1/X2;
                    PARM.RZSW=PARM.RZSW+X1;
                    PARM.PAW= PARM.PAW+X2;
                    //WRITE(KW(MSO+3),11)Z(L),X1,X4,X2,X3,STMP(L),WNO3(L),WNH3(L),&
                    //&AP(L),EXCK(L),WSLT(L)
                }
                //WRITE(KW(MSO+3),11)RZ,RZSW,PAW
            }
            else
            {
                //READ(KW(MSO+3),9)(IYS(I),I=2,8)
                for (int J = 1; J <= PARM.NBSL; J++)
                {
                    int L = PARM.LID[J];
                    //READ(KW(MSO+3),6)(XRTS(I,L),I=1,7)
                }
                //REWIND KW(MSO+3)
                PARM.ISTP=0;
                //WRITE(KW(MSO+3),9)ISTP
                PARM.ISTP=2;
                Two_loop();
            }
            //196 WRITE(KW(1),120)(ST(LID(L)),L=1,NBSL)
            //    WRITE(KW(1),120)(STMP(LID(L)),L=1,NBSL)
            //    WRITE(KW(1),120)(WNO3(LID(L)),L=1,NBSL)
            //    WRITE(KW(1),120)(WNH3(LID(L)),L=1,NBSL)
            //    WRITE(KW(1),120)(AP(LID(L)),L=1,NBSL)
            //    WRITE(KW(1),120)(EXCK(LID(L)),L=1,NBSL)
            //    WRITE(KW(1),120)(WSLT(LID(L)),L=1,NBSL)
            //    RETURN
            //  6 FORMAT(40X,F10.3,F10.2,5F10.0)
            //  9 FORMAT(20I4)
            // 11 FORMAT(F10.2,F10.1,F10.3,F10.1,2F10.2,5F10.0)
            //120 FORMAT(1X,10F10.2)
        }

        private static void Two_loop()
        {
            for (int J = 1; J <= PARM.NBSL; J++)
            {
                int L = PARM.LID[J];
                if (PARM.IYS[2] == 1)
                {
                    PARM.ST[L] = XRTS[1,L] * (PARM.FC[L]-PARM.S15[L])+PARM.S15[L];
                }
                if (PARM.IYS[3] == 1)
                {
                    PARM.STMP[L] = XRTS[2, L];
                }
                if (PARM.IYS[4] == 1)
                {
                    PARM.WNO3[L] = XRTS[3, L];
                }
                if (PARM.IYS[5] == 1)
                {
                    PARM.WNO3[L] = XRTS[4, L];
                }
                if (PARM.IYS[6] == 1)
                {
                    PARM.AP[L] = XRTS[5, L];
                }
                if (PARM.IYS[7] == 1)
                {
                    PARM.EXCK[L] = XRTS[6, L];
                }
                if (PARM.IYS[8] == 1)
                {
                    PARM.WSLT[L] = XRTS[7, L];
                }
            }
        }
    }

    
}