using System;

namespace Epic
{
	public class CSTRS
	{
		public CSTRS ()
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program estimates plant stress factors caused by limited
			// N, P, Air, and water and determines the active constraint
			// (Minimum stress factor -- N, P, Water, or Temperature). Calls
			// NFIX and NFERT (Automatic Fertilizer Option)
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			double[] JFS = new double[7] {13.0, 14.0, 15.0, 16.0, 17.0, 18.0, 19.0};
			double J3 = 5.0;
			PARM.VARC[16, PARM.JJK] = PARM.REG[PARM.JJK];
			if (PARM.EP[PARM.JJK] > 0){
				if (PARM.RZSW > 0){
					PARM.WS = 100*PARM.RZSW/PARM.PAW;
					PARM.WS = PARM.WS/(PARM.WS+Math.Exp (PARM.SCRP[10, 0]-PARM.SCRP[10, 1]*PARM.WS));
				}
				else{
					PARM.WS = 0;
				}
				//PARM.WS = (1-PARM.PRMT[34])*PARM.WP+PARM.PRMT(34)*PARM.SU/(PARM.EP[PARM.JJK]+1*Math.Pow (10, -10));
			}
			PARM.VARC[12, PARM.JJK] = PARM.WS;
			PARM.WFX = 0;
			if(PARM.IDC[PARM.JJK] == PARM.NDC[1] || PARM.IDC[PARM.JJK] == PARM.NDC[2] || PARM.IDC[PARM.JJK] == PARM.NDC[3] || PARM.IDC[PARM.JJK] == PARM.NDC[10]){
				//Functions.NFIX();
			}
			//Functions.NAJN(PARM.UN, PARM.WNO3, PARM.UNO3, PARM.SUN, 1.0, 0);
			double X1 = PARM.SUN/(PARM.UNO3+1*Math.Pow (10, -10));
			PARM.UNO3 = PARM.SUN+PARM.WFX;
			PARM.UN1[PARM.JJK] = PARM.UN1[PARM.JJK] + PARM.UNO3;
			if (PARM.UPP > PARM.SUP){
				//Functions.NAJN(PARM.UP, PARM.AP, PARM.UPP, PARM.SUP, 1.0, 1);	
			}
			double X2 = Math.Min (1.0, (PARM.SUP/(PARM.UPP+1*Math.Pow (10, -10))));
			PARM.UPP = PARM.SUP;
			PARM.UP1[PARM.JJK] = PARM.UP1[PARM.JJK]+PARM.UPP;
			//Functions.NANJ(PARM.UK, PARM.SOLK, PARM.UPK, PARM.SUK, 1.0, 0);
			double X3 = PARM.SUK/(PARM.UPK+1*Math.Pow (10, -10));
			PARM.UPK = PARM.SUK;
			PARM.UK1[PARM.JJK] = PARM.UK1[PARM.JJK] + PARM.UPK;
			//Functions.NUTS(PARM.UN1[PARM.JJK],PARM.UN2[PARM.JJK],PARM.SN);
			PARM.SN = Math.Max (X1, PARM.SN);
			PARM.VARC[13, PARM.JJK] = PARM.SN;
			//Functions.NUTS(PARM.UP1[PARM.JJK], PARM.UP2[PARM.JJK], PARM.SP);
			PARM.SP = Math.Max(X2, PARM.SP);
			PARM.VARC[14, PARM.JJK] = PARM.SP;
			//Epic.Nuts(UK1[PARM.JJK], UK2[PARM.JJK], SK);
			//SK = Math.Max (X3, SK);
			PARM.VARC[15, PARM.JJK] = PARM.SK;
			PARM.VARC[17, PARM.JJK] = PARM.SAT;
			X1 = .15625*PARM.TSRZ/PARM.SWRZ;
			double XX = X1-PARM.STX[1, PARM.JJK];
			if (XX > 0){
				PARM.SSLT = Math.Max (0, 1-PARM.STX[0, PARM.JJK]*XX);	
			}
			else{
				PARM.SSLT = 1;
			}
			PARM.VARC[18, PARM.JJK] = PARM.SSLT;
			double JRT = 0;
			double tmp = 6.0;
			double tmp2 = 0.0;
			JRT = Functions.CFRG(ref tmp, ref J3, ref PARM.SAT, ref PARM.REG[PARM.JJK], ref tmp2, ref JRT);
			
			// removed GO TO Statements
			int N1;
			if (JRT > 0){
				XX = 1-PARM.REG[PARM.JJK];
				N1 = PARM.NCP[PARM.JJK];
				PARM.SFMO[(int)J3,PARM.JJK] = PARM.SFMO[(int)J3,PARM.JJK]+XX;
				PARM.SF[(int)J3,N1,PARM.JJK] = PARM.SF[(int)J3,N1,PARM.JJK]+XX;
				PARM.CGSF[(int)J3,PARM.JJK] = XX;
				PARM.BLYN[PARM.JJK] = PARM.BLYN[PARM.JJK]+1;
				int J1 = (int)JFS[(int)J3];
				PARM.SMMC[J1,PARM.JJK,PARM.MO] = PARM.SMMC[J1,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[J1,PARM.JJK] = PARM.REG[PARM.JJK];
				PARM.SMMC[20,PARM.JJK,PARM.MO] = PARM.SMMC[20,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[20,PARM.JJK] = PARM.REG[PARM.JJK];
				return;
			}
			if (PARM.ICG == 0){
				//Functions.CFRG(1, J3, PARM.WS, PARM.REG[PARM.JJK], 0, JRT);
			}
			if (JRT > 0){
				XX = 1-PARM.REG[PARM.JJK];
				N1 = PARM.NCP[PARM.JJK];
				PARM.SFMO[(int)J3,PARM.JJK] = PARM.SFMO[(int)J3,PARM.JJK]+XX;
				PARM.SF[(int)J3,N1,PARM.JJK] = PARM.SF[(int)J3,N1,PARM.JJK]+XX;
				PARM.CGSF[(int)J3,PARM.JJK] = XX;
				PARM.BLYN[PARM.JJK] = PARM.BLYN[PARM.JJK]+1;
				int J1 = (int)JFS[(int)J3];
				PARM.SMMC[J1,PARM.JJK,PARM.MO] = PARM.SMMC[J1,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[J1,PARM.JJK] = PARM.REG[PARM.JJK];
				PARM.SMMC[20,PARM.JJK,PARM.MO] = PARM.SMMC[20,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[20,PARM.JJK] = PARM.REG[PARM.JJK];
				return;
			}
			//Functions.CFRG(3, J3, PARM.SP, PARM.REG[PARM.JJK], 0, JRT);
			if (JRT > 0){
				XX = 1-PARM.REG[PARM.JJK];
				N1 = PARM.NCP[PARM.JJK];
				PARM.SFMO[(int)J3,PARM.JJK] = PARM.SFMO[(int)J3,PARM.JJK]+XX;
				PARM.SF[(int)J3,N1,PARM.JJK] = PARM.SF[(int)J3,N1,PARM.JJK]+XX;
				PARM.CGSF[(int)J3,PARM.JJK] = XX;
				PARM.BLYN[PARM.JJK] = PARM.BLYN[PARM.JJK]+1;
				int J1 = (int)JFS[(int)J3];
				PARM.SMMC[J1,PARM.JJK,PARM.MO] = PARM.SMMC[J1,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[J1,PARM.JJK] = PARM.REG[PARM.JJK];
				PARM.SMMC[20,PARM.JJK,PARM.MO] = PARM.SMMC[20,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[20,PARM.JJK] = PARM.REG[PARM.JJK];
				return;
			}
			//
			//
			//Functions.CFRG(7, J3, PARM.SSLT, PARM.REG[PARM.JJK], 0, JRT);
			if (JRT > 0){
				XX = 1-PARM.REG[PARM.JJK];
				N1 = PARM.NCP[PARM.JJK];
				PARM.SFMO[(int)J3,PARM.JJK] = PARM.SFMO[(int)J3,PARM.JJK]+XX;
				PARM.SF[(int)J3,N1,PARM.JJK] = PARM.SF[(int)J3,N1,PARM.JJK]+XX;
				PARM.CGSF[(int)J3,PARM.JJK] = XX;
				PARM.BLYN[PARM.JJK] = PARM.BLYN[PARM.JJK]+1;
				int J1 = (int)JFS[(int)J3];
				PARM.SMMC[J1,PARM.JJK,PARM.MO] = PARM.SMMC[J1,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[J1,PARM.JJK] = PARM.REG[PARM.JJK];
				PARM.SMMC[20,PARM.JJK,PARM.MO] = PARM.SMMC[20,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[20,PARM.JJK] = PARM.REG[PARM.JJK];
				return;
			}
			double ZZ = PARM.REG[PARM.JJK];
			tmp = 2.0;
			Functions.CFRG(ref tmp, ref J3, ref PARM.SN, ref ZZ, ref PARM.REG[PARM.JJK], ref JRT);
			if (JRT == 0){
				XX = 1-PARM.REG[PARM.JJK];
				N1 = PARM.NCP[PARM.JJK];
				PARM.SFMO[(int)J3,PARM.JJK] = PARM.SFMO[(int)J3,PARM.JJK]+XX;
				PARM.SF[(int)J3,N1,PARM.JJK] = PARM.SF[(int)J3,N1,PARM.JJK]+XX;
				PARM.CGSF[(int)J3,PARM.JJK] = XX;
				PARM.BLYN[PARM.JJK] = PARM.BLYN[PARM.JJK]+1;
				int J1 = (int)JFS[(int)J3];
				PARM.SMMC[J1,PARM.JJK,PARM.MO] = PARM.SMMC[J1,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[J1,PARM.JJK] = PARM.REG[PARM.JJK];
				PARM.SMMC[20,PARM.JJK,PARM.MO] = PARM.SMMC[20,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[20,PARM.JJK] = PARM.REG[PARM.JJK];
				return;
			}
			PARM.REG[PARM.JJK] = ZZ;
			if(PARM.IDC[PARM.JJK] == PARM.NDC[1] || PARM.NDC[PARM.JJK] == PARM.NDC[2] || PARM.IDC[PARM.JJK] == PARM.NDC[3] || PARM.IDC[PARM.JJK] == PARM.NDC[10]){
				XX = 1-PARM.REG[PARM.JJK];
				N1 = PARM.NCP[PARM.JJK];
				PARM.SFMO[(int)J3,PARM.JJK] = PARM.SFMO[(int)J3,PARM.JJK]+XX;
				PARM.SF[(int)J3,N1,PARM.JJK] = PARM.SF[(int)J3,N1,PARM.JJK]+XX;
				PARM.CGSF[(int)J3,PARM.JJK] = XX;
				PARM.BLYN[PARM.JJK] = PARM.BLYN[PARM.JJK]+1;
				int J1 = (int)JFS[(int)J3];
				PARM.SMMC[J1,PARM.JJK,PARM.MO] = PARM.SMMC[J1,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[J1,PARM.JJK] = PARM.REG[PARM.JJK];
				PARM.SMMC[20,PARM.JJK,PARM.MO] = PARM.SMMC[20,PARM.JJK,PARM.MO]+XX;
				PARM.VARC[20,PARM.JJK] = PARM.REG[PARM.JJK];
				return;
			}
			if (PARM.BFT > PARM.SN && PARM.NFA >= PARM.IFA){
				//Functions.NFERT(3, PARM.IAUF);	
			}
		}
	}
}

