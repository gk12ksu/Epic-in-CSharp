using System;

namespace Epic
{
	public partial class Functions
	{
		public static double ATRI (ref double BLM, ref double QMN, ref double UPLM, ref int KK)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program generates numbers from triangular distribution
			// given x axis points at start and end and peak y value
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			double U3 = QMN-BLM;
			double RN = Functions.AUNIF(PARM.IDG[KK]);
			double Y = 2/(UPLM-BLM);
			double B2 = UPLM-QMN;
			double B1 = RN/Y;
			double X1 = Y*U3/2;
			
			double ATRI_ans;
			if (RN > X1){
				ATRI_ans = UPLM - Math.Sqrt(B2*B2-2*B2*(B1-.5*U3));	
			}
			else{
				ATRI_ans = Math.Sqrt(2*B1*U3)+BLM;
			}
			
			if (KK != 7 && KK != 4) return ATRI_ans;
			
			double AMN = (UPLM+QMN+BLM)/3;
			ATRI_ans = ATRI_ans*QMN/AMN;
			if (ATRI_ans >= 1) ATRI_ans = .99;
			return ATRI_ans;
		}
	}
}

