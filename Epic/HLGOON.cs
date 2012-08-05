using System;

namespace Epic
{
	public class HLGOON
	{
		public HLGOON (ref double JRT)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program estimates inflow, storage, and irrigation from lagoons.
            // Runoff is estimated with 90 CN.
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double X2=10.0*PARM.DALG;
            double DP = .1677*Math.Pow(PARM.VLG, .3333);
            double TW = 18.0*DP;
            double SA = .0001*TW*TW;
            double EV = 6.0*PARM.EO*SA;
            PARM.VLG = PARM.VLG-EV+PARM.COWW;
            EV = EV/X2;
            PARM.SMM[20,PARM.MO] = PARM.SMM[20,PARM.MO]+EV;
            PARM.VAR[20] = EV;
            double XX = PARM.COWW/X2;
            PARM.SMM[21,PARM.MO] = PARM.SMM[21,PARM.MO]+XX;
            PARM.VAR[21] = XX;
            double X1 = PARM.RWO-5.64;

            double QLG;
            if (X1 > 0.0){
                QLG = X1*X1/(PARM.RWO+22.6);
                QLG = 10.0*(QLG*(PARM.DALG-SA)+PARM.RWO*SA);
                PARM.VLG = PARM.VLG+QLG;
                QLG = QLG/X2;
                PARM.SMM[22,PARM.MO] = PARM.SMM[22,PARM.MO]+QLG;
                PARM.VAR[22] = QLG;
            }
            if (PARM.VLG <= PARM.VLGN){
                JRT = 1;
                return;
            }
            if (PARM.VLG > PARM.VLGM){
                X1 = (PARM.VLG-PARM.VLGM)/X2;
                //WRITE(KW(1),4)IYR,MO,KDA,X1
                PARM.SMM[23,PARM.MO] = PARM.SMM[23,PARM.MO]+X1;
                PARM.VAR[23] = X1;
                PARM.VLG = PARM.VLGM;
            }
            if (PARM.RZSW >= PARM.PAW){
                JRT = 1;
                return;
            }

            XX = 10.0*PARM.WSA*PARM.VLGI;
            X1 = XX/X2;
            PARM.SMM[24,PARM.MO] = PARM.SMM[24,PARM.MO]+X1;
            PARM.VAR[24] = X1;
            PARM.VLG = Math.Max(Math.Pow(10, -5),PARM.VLG-XX);
            JRT = 0;
            return;
            //4 FORMAT(T10,'***** LAGOON OVERFLOWED ',3I4,F4.0,' MM')
		}
	}
}

