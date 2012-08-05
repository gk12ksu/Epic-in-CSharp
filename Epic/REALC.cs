using System;

namespace Epic
{
    public class REALC
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public REALC()
        {
            // EPIC0810
            // Translated by Paul Cain
            // THIS SUBPROGRAM ALLOWS REAL TIME UPDATES OF CROP VARIABLES

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
            double[,] XRTC = new double[8, 12];

            //Translator's Note: I'm leaving the line below untranslated and
            //  commented out because X and FNPP are not global but they are
            //  not declared locally anywhere either.
            //FNPP(X)=DMLA(JJK)*X/(X+EXP(PPCF(1,JJK)-PPCF(2,JJK)*X))
            //WRITE(KW(1),170)IY,MO,KDA
            new PESTF();
            //WRITE(KW(1),174)CPNM(JJK),WS,SN,SP,SK,VAR(72),SSLT,PSTF(JJK)
            //WRITE(KW(1),191)DM(JJK),SLAI(JJK),PPL0(JJK),UN1(JJK),UP1(JJK),UK1&
            //&(JJK),PRYG(JJK),PRYF(JJK)
            if (PARM.ISTP == 2)
            {
                if (PARM.IYS[1] != 0)
                {
                    PARM.DM[PARM.JJK] = XRTC[1, PARM.JJK];
                    PARM.SLAI[PARM.JJK] = XRTC[2, PARM.JJK];
                    PARM.PPL0[PARM.JJK] = XRTC[3, PARM.JJK];
                    //Translator's Note: I'm leaving the line below untranslated and
                    //  commented out because FNPP doesn't seem to be declared globally 
                    //  or locally.
                    //XLAI(JJK)=FNPP(PPL0(JJK))
                    PARM.UN1[PARM.JJK] = XRTC[4, PARM.JJK];
                    PARM.UP1[PARM.JJK] = XRTC[5, PARM.JJK];
                    PARM.UK1[PARM.JJK] = XRTC[6, PARM.JJK];
                    PARM.PRYG[PARM.JJK] = XRTC[7, PARM.JJK];
                    PARM.PRYF[PARM.JJK] = XRTC[8, PARM.JJK];
                }
                    //WRITE(KW(1),191)DM(JJK),SLAI(JJK),PPL0(JJK),UN1(JJK),UP1(JJK),&
                    //&UK1(JJK),PRYG(JJK),PRYF(JJK)      
                    return;
            }
            if (PARM.ISTP == 0)
            {
                new OPENV(PARM.KW[PARM.MSO+2],"RTCROP.DAT          ",0,PARM.KW[PARM.MSO]);
                //WRITE(KW(MSO+2),3)IYS(1)
                //WRITE(KW(MSO+2),2)DM(JJK),SLAI(JJK),PPL0(JJK),UN1(JJK),UP1(JJK),&
                //&UK1(JJK),PRYG(JJK),PRYF(JJK)
                PARM.ISX = 1;
            }
            else
            {
                new OPENV(PARM.KW[PARM.MSO+2],"RTCROP.DAT          ",0,PARM.KW[PARM.MSO]);
                //READ(KW(MSO+2),3)IYS(1)
                //READ(KW(MSO+2),2)(XRTC(I,JJK),I=1,8)
            }
            //CLOSE(KW(MSO+2))   
            return;
            //  2 FORMAT(10F8.3)
            //  3 FORMAT(I4)
            //170 FORMAT(///'*****UPDATE  YR = ',I2,'  MO = ',I2,'  DA = ',I2)
            //174 FORMAT(5X,A4,10F7.2)
            //191 FORMAT(5X,'BIOM=',F8.3,' t/ha   LAI=',F5.2,3X,'PPOP=',F6.1,'UN=',&
            //    F5.1,' kg/ha UP=',F5.1,' kg/ha',3X,'UK=',F5.1,' kg/ha',3X,'PRYG=',&
            //    F5.0,'$/t',3X,'PRYF=',F5.0,'$/t')
        }
    }
}
