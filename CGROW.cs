using System;

namespace Epic
{
	public class CGROW
	{
		public CGROW (ref double JRT)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program calculates leaf area index, heat units, root depth
            // and temperature strees for the. (I swear this is how it ends in the original file)
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double[] SLA0 = new double[12];
            JRT = 0;
            double X1 = PARM.DM[PARM.JJK]+Math.Pow(10, -10);
            double CPR = PARM.UP1[PARM.JJK]/X1;
            double CNR = PARM.UN1[PARM.JJK]/X1;
            double CKR = Math.Max(Math.Pow(10, -5),PARM.UK1[PARM.JJK]/X1);
            PARM.AJWA[PARM.JJK]=1.0;
            double XPHU = PARM.PHU[PARM.JJK,PARM.IHU[PARM.JJK]];
            double X4 = PARM.TOPC[PARM.JJK]-PARM.TBSC[PARM.JJK];
            double TGX = PARM.TX-PARM.TBSC[PARM.JJK];
            PARM.HU[PARM.JJK] = PARM.HU[PARM.JJK]+Math.Max(0.0,TGX);
            if ((PARM.JDA == PARM.JDHU && PARM.IDC[PARM.JJK]  !=  PARM.NDC[6] && PARM.IDC[PARM.JJK]  !=  PARM.NDC[7] && PARM.IDC[PARM.JJK] != PARM.NDC[9])){
                PARM.HU[PARM.JJK] = XPHU*PARM.PRMT[19];
                PARM.PSTS = Math.Min(0.0,PARM.PSTS);
                PARM.IPST = 0;
            }

            PARM.HUI[PARM.JJK] = PARM.HU[PARM.JJK]/XPHU;
            
            if (PARM.HU[PARM.JJK] > XPHU){
                PARM.WCYD = Math.Max(PARM.WCY[PARM.JJK],PARM.WCYD-PARM.EO*.002);
                if(PARM.IDC[PARM.JJK] == PARM.NDC[3] || PARM.IDC[PARM.JJK] == PARM.NDC[6]){
                  PARM.HU[PARM.JJK]=0.0;
                  JRT=2;
                }
                else{
                    JRT = 1;
                }
                return;
            }

            double F2 = PARM.HUI[PARM.JJK]/(PARM.HUI[PARM.JJK]+Math.Exp(PARM.DLAP[0,PARM.JJK]-PARM.DLAP[1,PARM.JJK]*PARM.HUI[PARM.JJK]));
	        double F3=Math.Sqrt(F2+Math.Pow(10, -10));
			
			double F = 0;
            if(PARM.IDC[PARM.JJK] == PARM.NDC[8] || PARM.IDC[PARM.JJK] == PARM.NDC[10]){
                X1 = PARM.HSM/PARM.AHSM;
	            double F1 = X1/(X1+Math.Exp(PARM.DLAP[0,PARM.JJK]-PARM.DLAP[1,PARM.JJK]*X1));
	            F = F1;
	            PARM.XLAI[PARM.JJK] = Math.Max(.1,PARM.DMLX[PARM.JJK]*PARM.HUI[PARM.JJK]/(PARM.HUI[PARM.JJK]+Math.Exp(PARM.DLAP[0,PARM.JJK]-PARM.DLAP[1,PARM.JJK]*PARM.HUI[PARM.JJK])));
            }

	        if(PARM.IDC[PARM.JJK] != PARM.NDC[7] && PARM.IDC[PARM.JJK] != PARM.NDC[9]) F = F2;
	        if(PARM.IDC[PARM.JJK] == PARM.NDC[8] || PARM.IDC[PARM.JJK] == PARM.NDC[7] || PARM.IDC[PARM.JJK] == PARM.NDC[10]) F3 = PARM.HUI[PARM.JJK];
	        double FF = F-PARM.WLV[PARM.JJK];
            double XX = FF*PARM.XLAI[PARM.JJK];
            double X2 = 1.0;
            double SLAX=0.0;
	        double X3 = PARM.SLAI[PARM.JJK]+.001;
            
			int K1; 
			double SUM;
            if(PARM.IGO > 1){
                SUM=0.0;
                int I;
                for (I = 0; I < PARM.IGO; I++){
                    K1 = PARM.JE[I];
                    if(K1>PARM.MNC) continue;
                    if(PARM.SLAI[K1]>SLAX) SLAX = PARM.SLAI[K1];
                    SUM = SUM+PARM.SLAI[K1];
                }
                if(SLAX<2.0) goto lbl23;
                X2 = X3/SUM;
                goto lbl23;
            }
            SUM = X3;

     lbl23: if (XX > 0){
                X1 = XX*X2*Math.Pow((1.0+PARM.HR1),PARM.PRMT[69]);
	            if(PARM.IDC[PARM.JJK] != PARM.NDC[7] && PARM.IDC[PARM.JJK] != PARM.NDC[8] && PARM.IDC[PARM.JJK] != PARM.NDC[10]) X1 = X1*Math.Sqrt(PARM.WS)*PARM.SHRL;
                PARM.SLAI[PARM.JJK] = PARM.SLAI[PARM.JJK]+X1;
            }
            PARM.WLV[PARM.JJK] = F;
            double RTO = TGX/X4;
            
            if (TGX > 0.0 && RTO < 2.0){
                PARM.REG[PARM.JJK] = Math.Sin(1.5707*RTO);
            }
            else{
                PARM.REG[PARM.JJK] = 0.0;
            }

            if (PARM.SLAI[PARM.JJK]<.05) goto lbl8;
            PARM.CHT[PARM.JJK] = Math.Max(PARM.CHT[PARM.JJK],PARM.HMX[PARM.JJK]*F3);
            if (PARM.HUI[PARM.JJK]>PARM.XDLAI[PARM.JJK] && PARM.HRLT>PARM.WDRM && PARM.XDLA0[PARM.JJK]>0.0) goto lbl6;
            PARM.XDLA0[PARM.JJK] = 1.0-PARM.XDLAI[PARM.JJK];
            SLA0[PARM.JJK] = PARM.SLAI[PARM.JJK];
            goto lbl9;

      lbl6: XX = (1.0-PARM.HUI[PARM.JJK])/PARM.XDLA0[PARM.JJK];
            if (XX > Math.Pow(10, -5)){
                XX = Math.Log10(XX);
            }
            else{
                XX = -5.0;
            }

            if (PARM.IDC[PARM.JJK] == PARM.NDC[7] || PARM.IDC[PARM.JJK] == PARM.NDC[8] || PARM.IDC[PARM.JJK] == PARM.NDC[10]) goto lbl7;
      
            RTO = PARM.RLAD[PARM.JJK]*XX;
            if(RTO<-10.0) RTO = -10.0;
            PARM.SLAI[PARM.JJK] = Math.Min(PARM.SLAI[PARM.JJK],SLA0[PARM.JJK]*Math.Pow(10.0, RTO));
            PARM.SLAI[PARM.JJK] = Math.Max(.05,PARM.SLAI[PARM.JJK]);

      lbl7: RTO = PARM.RBMD[PARM.JJK]*XX;
            if(RTO<-10.0) RTO = -10.0;
            PARM.AJWA[PARM.JJK] = Math.Pow(10, RTO);
            goto lbl9;

      lbl8: PARM.SLAI[PARM.JJK]=.05;


      lbl9: double XX1 = Math.Max(PARM.RD[PARM.JJK],2.5*PARM.RDMX[PARM.JJK]*PARM.HUI[PARM.JJK]);
			XX = Math.Max (XX1, PARM.CHT[PARM.JJK]);
			double temp = Math.Min (PARM.Z[PARM.LID[PARM.NBSL]],XX);
            PARM.RD[PARM.JJK] = Math.Min(PARM.RDMX[PARM.JJK],temp);
            PARM.FGC = SUM/(SUM+Math.Exp(PARM.SCRP[22,0]-PARM.SCRP[22,1]*SUM));
            PARM.CLG = PARM.BLG[2,PARM.JJK]*PARM.HUI[PARM.JJK]/(PARM.HUI[PARM.JJK]+Math.Exp(PARM.BLG[0,PARM.JJK]-PARM.BLG[1,PARM.JJK]*PARM.HUI[PARM.JJK]));
            PARM.SHRL=1.0;
            double FHR=0.0;

            if (PARM.HRLT + Math.Pow(10, -5) > PARM.WDRM){
                PARM.SRA = PARM.SRA+PARM.SRAD;
                PARM.GSVP = PARM.GSVP+PARM.VPD;
            }
            else{
                PARM.SHRL = 0.0;
                FHR = 1.0-PARM.HRLT/PARM.WDRM;
            }

            if (PARM.IDC[PARM.JJK] == PARM.NDC[7]) goto lbl15; 
            if (PARM.TMN>-1.0) goto lbl12;
            XX = Math.Abs(PARM.TMN);
            F = XX/(XX+Math.Exp(PARM.FRST[0,PARM.JJK]-PARM.FRST[1,PARM.JJK]*XX));
            F = Math.Max(F,FHR);
            goto lbl13;

     lbl12: if (PARM.SHRL>0.0) goto lbl15;
            F = FHR;


     lbl13: if (PARM.STL[PARM.JJK]>0.0 && PARM.IDC[PARM.JJK] != PARM.NDC[7] && PARM.IDC[PARM.JJK] != PARM.NDC[9]){
                XX = F*PARM.STL[PARM.JJK];
                PARM.STL[PARM.JJK] = PARM.STL[PARM.JJK]-XX;
                PARM.DM[PARM.JJK] = PARM.DM[PARM.JJK]-XX;
                PARM.STD[PARM.JJK] = PARM.STD[PARM.JJK]+XX;
                PARM.STDL = PARM.STDL+PARM.CLG*XX;
                double XY = XX*CNR;
                double XZ = XX*CPR;
                double XW = XX*CKR;
                PARM.STDK = PARM.STDK+XW;
                PARM.STDN[PARM.JJK] = PARM.STDN[PARM.JJK]+XY;
                PARM.STDP = PARM.STDP+XZ;
                PARM.UK1[PARM.JJK] = PARM.UK1[PARM.JJK]-XW;
                PARM.UN1[PARM.JJK] = Math.Max(Math.Pow(10, -5),PARM.UN1[PARM.JJK]-XY);
                PARM.UP1[PARM.JJK] = PARM.UP1[PARM.JJK]-XZ;
            }
            PARM.SLAI[PARM.JJK] = Math.Max(.02,PARM.SLAI[PARM.JJK]*(1.0-F));

     lbl15: if(PARM.REG[PARM.JJK]>0.0) return;
            double N1 = PARM.NCP[PARM.JJK];
            PARM.SF[4,(int)N1,PARM.JJK] = PARM.SF[4,(int)N1,PARM.JJK]+1.0;
            PARM.SFMO[4,PARM.JJK] = PARM.SFMO[4,PARM.JJK]+1.0;
            PARM.BLYN[PARM.JJK] = PARM.BLYN[PARM.JJK]+1.0;
            PARM.SMMC[16,PARM.JJK,PARM.MO] = PARM.SMMC[16,PARM.JJK,PARM.MO]+1.0;
            PARM.VARC[16,PARM.JJK] = 0.0;
            JRT = 1;
            return;
		}
	}
}

