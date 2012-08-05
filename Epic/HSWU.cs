using System;

namespace Epic
{
	public class HSWU
	{
		public HSWU (ref double CPWU, ref double RGS)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program distritubtes plant evaporation through
            // the root zone and calculates actual plant water use
            // based on soil water availability
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double BLM = PARM.S15[PARM.ISL];
            if (PARM.Z[PARM.ISL] <= .5) BLM = PARM.PRMT[4]*PARM.S15[PARM.ISL];
			if (PARM.ISL != PARM.LD1){
                Functions.CRGBD(ref RGS);
                CPWU = CPWU*RGS;
            }

            double SUM = PARM.EP[PARM.JJK]*(1.0-Math.Exp(-PARM.UB1*PARM.GX/PARM.RD[PARM.JJK]))/PARM.UOB;
            double TOS = 36.0*PARM.ECND[PARM.ISL];
            double XX = Math.Log10(PARM.S15[PARM.ISL]);
            PARM.WTN = Math.Max(5.0,Math.Pow(10.0,(3.1761-1.6576*((Math.Log10(PARM.ST[PARM.ISL])-XX)/(Math.Log10(PARM.FC[PARM.ISL])-XX)))));
            XX = TOS+PARM.WTN;

            if (XX < 5000.0){
                double F = 1.0-XX/(XX+Math.Exp(PARM.SCRP[20,0]-PARM.SCRP[20,1]*XX));
                PARM.U[PARM.ISL] = Math.Min(SUM-CPWU*PARM.SU-(1.0-CPWU)*PARM.UX,PARM.ST[PARM.ISL]-BLM)*RGS*F;
                if (PARM.U[PARM.ISL] < 0.0) PARM.U[PARM.ISL] = 0.0;
            }
            PARM.UX = SUM;
            return;
		}
	}
}

