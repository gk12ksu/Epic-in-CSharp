using System;

namespace Epic
{
	public partial class Functions
	{
		public static double CRGBD (ref double RGS)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program calculates root growth stresses caused by
			// temperature, aluminum toxicity, and soil strength and determines
			// the active constraint on root growth (The minimum stress factor).
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			RGS = 1;
			if (PARM.PRMT[1] > 1.99) return RGS;
			
			int II = 3;
			double F = 0.0;
			double XX = PARM.STMP[PARM.ISL]/PARM.TOPC[PARM.JJK];
			if (XX <= 0 ){
				RGS = 0;
				// Removed GO TO
				PARM.STDA[II, PARM.JJK] = PARM.STDA[II, PARM.JJK] + (1.0-RGS)/PARM.NBSL;
				if(PARM.ISL == PARM.LID[PARM.LRD]) PARM.RGSM = RGS;
				return RGS;
			}
			if (XX < 1) RGS = Math.Sin (1.5708*XX);
			double A0 = 10+(PARM.ALT[PARM.JJK]-1)*20;
			double JRT = 0.0;
			if (PARM.ALS[PARM.ISL] > A0){
				F = (100-PARM.ALS[PARM.ISL])/(100-A0);
				JRT = 0.0;
				//Functions.CFRG(2, II, F, RGS, .1, PARM.JRT);
				if (JRT>0){
					PARM.STDA[II, PARM.JJK] = PARM.STDA[II, PARM.JJK] + (1-RGS)/PARM.NBSL;
					if(PARM.ISL == PARM.LID[PARM.LRD]) PARM.RGSM = RGS;
					return RGS;
				}
			}
			//Functions.SBDSC(PARM.BDP[PARM.ISL], PARM.PRMT[1], F, PARM.ISL, 3);
			XX = PARM.ROK[PARM.ISL];
			// Start here.....
			F = F*(1-XX/(XX+Math.Exp (PARM.SCRP[1, 1]-PARM.SCRP[1, 2]*XX)));
			double tmp = 1.0;
			double tmp2 = 0.1;
			double II_tmp = (double)II;
			Functions.CFRG(ref tmp, ref II_tmp, ref F, ref RGS, ref tmp2, ref JRT);
			return RGS;
		}
	}
}

