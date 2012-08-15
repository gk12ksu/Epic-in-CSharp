using System;

namespace Epic
{
    public class PSTAPP
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public PSTAPP()
        {
            // EPIC0810
            // Translated by Paul Cain
            // THIS SUBPROGRAM APPLIES PESTICIDES TO CROP CANOPY & SOIL

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            //Translator's Note: For now, I kept all of the array indices as 
            //	the same numbers they were in their original fortran files  
            //	even though C# use base 0 arrays and fortran use base 1 
            //	arrays. I did this to avoid confusion until we have a better
            // 	understanding of how the program works. 

            // USE PARM
            PARM.KP = PARM.LPC[PARM.IRO, PARM.KT];
            double XX = PARM.PSTR[PARM.IRO, PARM.KT] * PARM.HE[PARM.JT1] * 1000.0;
            PARM.SMMP[1, PARM.KP, PARM.MO] = PARM.SMMP[1, PARM.KP, PARM.MO] + XX;
            PARM.TPAC[PARM.JJK, PARM.KP] = PARM.TPAC[PARM.JJK, PARM.KP] + XX;
            PARM.VARP[1, PARM.KP] = XX;
            double X1 = PARM.PCST[PARM.KP] * PARM.PSTR[PARM.IRO, PARM.KT];
            PARM.COST = PARM.COST + X1;
            PARM.SMM[96, PARM.MO] = PARM.SMM[96, PARM.MO] + PARM.PCEM[PARM.KP];
            if (PARM.KFL[20] > 0)
            {
                // Traslator's Note: This write statement appears to write 
                //  to a file, but I'm not sure where. I am therefore
                //  commenting it out for now.
                //	Fortran:	WRITE(KW(20),1)IYR,MO,KDA,PSTN(KP),KDC(JJK),KDP&
                //      (KP),IHC(JT1),NBE(JT1),NBT(JT1),X1,X1,PSTR(IRO,KT)
                //	  PSTS=PSTS-PSTE(IRO,KT)*PRMT(37)
            }
            if (PARM.NOP > 0)
            {
                // Traslator's Note: This write statement appears to write 
                //  to a file, but I'm not sure where. I am therefore
                //  commenting it out for now.
                //  Fortran:    WRITE(KW(1),6)IYR,MO,KDA,PSTN(KP),PSTR(IRO,KT),HE(JT1)
                //      	,PSTE(IRO,KT),PSTS,X1;
            }
            if (PARM.TLD[PARM.JT1] < (1 * Math.Pow(10, -10)))
            {
                X1 = XX * PARM.FGC;
                PARM.PFOL[PARM.KP] = PARM.PFOL[PARM.KP] + X1;
                PARM.PSTZ[PARM.KP, PARM.LD1] = PARM.PSTZ[PARM.KP, PARM.LD1] + XX - X1;
                return;
            }
            else
            {
                for (int K = 1; K <= PARM.NBSL; K++)
                {
                    PARM.ISL = PARM.LID[K];
                    if (PARM.TLD[PARM.JT1] <= PARM.Z[PARM.ISL])
                    {
                        Environment.Exit(1);
                    }
                }
                PARM.PSTZ[PARM.KP, PARM.ISL] = PARM.PSTZ[PARM.KP, PARM.ISL] + XX;
            }
            // Translator's Note: These are the formats used with the 
            //  statements above. I don't know what to do with it these 
            //  I am commenting these out for now.
            //    1 FORMAT(1X,3I4,2X,A16,I6,2X,4I4,F10.2,10X,3F10.2)
            //    6 FORMAT(1X,3I4,2X,A8,2X,'APPL RATE= ',F5.1,'kg/ha',2X,'APPL EFF&
            //      = ',F4.2,2X,'KILL EFF= ',F4.2,2X,'PST IDX= ',E12.4,2X,'COST=',&
            //      F7.0,'$/ha')
        }
    }
}

