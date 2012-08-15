using System;

namespace Epic
{
	public class CAGRO
	{
		public CAGRO ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program calculates the daily increase in plant biomass,
            // root weight, and yield by adjusting the potential values with the
            // active stress contraint.
			
			// The fortran file uses global variables, and needs to be fixed
			// for C# style coding
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double[] XTP = new double[15];
            double XX = PARM.REG[PARM.JJK]*PARM.SHRL;
            double RWL = PARM.RW[PARM.JJK];
            double RGD = PARM.DDM[PARM.JJK]*XX;
            //PARM.DRWX = 0.0;
			foreach (int x in PARM.DRWX){
				PARM.DRWX[x] = 0.0;	
			}
            double X1 = 100.0*PARM.HUI[PARM.JJK];
            PARM.AJHI[PARM.JJK] = PARM.HI[PARM.JJK]*X1/(X1+Math.Exp(PARM.SCRP[2,0]-PARM.SCRP[2,1]*X1));
            double YX = PARM.DM[PARM.JJK]-PARM.DDM[PARM.JJK];
            XX = Math.Max(Math.Pow(10, -5),YX+RGD);
            PARM.DM[PARM.JJK] = XX;
            PARM.DM1[PARM.JJK] = PARM.DM1[PARM.JJK]+RGD;
            double RF = Math.Max(.2,PARM.RWPC[1,PARM.JJK]*(1.0-PARM.HUI[PARM.JJK])+PARM.RWPC[1,PARM.JJK]*PARM.HUI[PARM.JJK]);
            PARM.RW[PARM.JJK] = RF*PARM.DM[PARM.JJK];
            double DRW = PARM.RW[PARM.JJK]-RWL;
            PARM.STL[PARM.JJK] = PARM.DM[PARM.JJK]-PARM.RW[PARM.JJK];
/*!     IF(PARM.STL[PARM.JJK]<X1)THEN
!         XX=X1-PARM.STL[PARM.JJK]
!         STD[PARM.JJK]=STD[JJK]+XX
!         WRITE(KW[1],1)IY,MO,KDA,STD[PARM.JJK],X1,XX
!     END IF
!   1 FORMAT(1X,'!!!!!',3I4,3E16.6)       */
            XX = 0.0;
            double SUM = 0.0;
            double X2 = 2.0*PARM.RZ;
            
            int I;
            for (I = 0; I < PARM.LRD; I++){
                PARM.ISL = PARM.LID[I];
                XTP[PARM.ISL] = (PARM.Z[PARM.ISL]-XX)*Math.Exp(-PARM.PRMT[55]*PARM.Z[PARM.ISL]/PARM.RZ);
                SUM = SUM+XTP[PARM.ISL];
                XX = PARM.Z[PARM.ISL];
            }

            if (PARM.IDC[PARM.JJK]==PARM.NDC[7] || PARM.IDC[PARM.JJK]==PARM.NDC[8] || PARM.IDC[PARM.JJK]==PARM.NDC[10]){
                X1 = PARM.MO*PARM.MO;
                double FALF = PARM.STL[PARM.JJK]*X1*PARM.XMTU[PARM.JJK];
	            PARM.SMM[87,PARM.MO] = PARM.SMM[87,PARM.MO]+FALF;
                PARM.DM[PARM.JJK] = PARM.DM[PARM.JJK]-FALF;
                PARM.STL[PARM.JJK] = PARM.STL[PARM.JJK]-FALF;
                X1 = FALF*1000.0;
                double XZ = PARM.CNY[PARM.JJK]*X1;
                double XY = PARM.CPY[PARM.JJK]*X1;
                double W1 = PARM.CKY[PARM.JJK]*X1;
                //Functions.NCNSTD(FALF,XZ,0);
                PARM.FOP[PARM.LD1] = PARM.FOP[PARM.LD1]+XY;
                PARM.UN1[PARM.JJK] = Math.Max(1.0E-5,PARM.UN1[PARM.JJK]-XZ);
                PARM.UP1[PARM.JJK] = PARM.UP1[PARM.JJK]-XY;
                PARM.UK1[PARM.JJK] = PARM.UK1[PARM.JJK]-W1;
            }
          
            if (PARM.IDC[PARM.JJK]==PARM.NDC[3] || PARM.IDC[PARM.JJK]==PARM.NDC[6]){
                double ZZ = .01*Math.Pow((PARM.HUI[PARM.JJK]+.01), 10)*PARM.STL[PARM.JJK];
                PARM.STL[PARM.JJK] = PARM.STL[PARM.JJK]-ZZ;
                PARM.DM[PARM.JJK] = PARM.DM[PARM.JJK]-ZZ;
                PARM.STD[PARM.JJK] = PARM.STD[PARM.JJK]+ZZ;
                PARM.STDL = PARM.STDL+PARM.CLG*ZZ;
                double XY = ZZ*PARM.BN[2,PARM.JJK];
                double XZ = ZZ*PARM.BP[2,PARM.JJK];
                double XW = ZZ*PARM.BK[2,PARM.JJK];
                PARM.STDK = PARM.STDK+XW;
                PARM.STDN[PARM.JJK] = PARM.STDN[PARM.JJK]+XY;
                PARM.STDP = PARM.STDP+XZ;
                PARM.UK1[PARM.JJK] = PARM.UK1[PARM.JJK]-XW;
                PARM.UN1[PARM.JJK] = Math.Max(Math.Pow(10, -5),PARM.UN1[PARM.JJK]-XY);
                PARM.UP1[PARM.JJK] = PARM.UP1[PARM.JJK]-XZ;
                if (PARM.HUI[PARM.JJK] > .6 && PARM.STL[PARM.JJK] < .1) PARM.HU[PARM.JJK] = 0.0;
            }
      
            int J;
			double UTO;
            for (J = 0; J < PARM.LRD; J++){
                PARM.ISL = PARM.LID[J];
                PARM.WNO3[PARM.ISL] = Math.Max(Math.Pow(10, -5),PARM.WNO3[PARM.ISL]-PARM.UN[PARM.ISL]);
                if (DRW < 0.0){
                    UTO = PARM.RWT[PARM.ISL,PARM.JJK]/(RWL+Math.Pow(10, -10));
                    X1 = Math.Pow(10, -10)-DRW*UTO;
                    X2 = Math.Min(PARM.UN1[PARM.JJK],1000.0*PARM.BN[2,PARM.JJK]*X1);
                    //Functions.NCNSTD(X1,X2,1);
                    PARM.UN1[PARM.JJK] = PARM.UN1[PARM.JJK]-X2;
                }
                else{
                    UTO = PARM.PRMT[54]*PARM.U[PARM.ISL]/(PARM.SU+Math.Pow(10, -20))+(1.0-PARM.PRMT[54])*XTP[PARM.ISL]/SUM;
                }

                PARM.ST[PARM.ISL] = PARM.ST[PARM.ISL]-PARM.U[PARM.ISL];
                PARM.AP[PARM.ISL] = PARM.AP[PARM.ISL]-PARM.UP[PARM.ISL];
                PARM.SOLK[PARM.ISL] = PARM.SOLK[PARM.ISL]-PARM.UK[PARM.ISL];
                PARM.DRWX[PARM.ISL] = DRW*(UTO+Math.Pow(10, -10));
                PARM.RWT[PARM.ISL,PARM.JJK] = PARM.RWT[PARM.ISL,PARM.JJK]+PARM.DRWX[PARM.ISL];
            }

            if (PARM.RW[PARM.JJK] <= PARM.RWX[PARM.JJK]) return;
            PARM.RWX[PARM.JJK] = PARM.RW[PARM.JJK];
            PARM.M21 = PARM.MO;
            PARM.K21 = PARM.KDA;
            for (I = 0; I < PARM.LRD; I++){
                PARM.ISL = PARM.LID[I];
                PARM.RWTX[PARM.ISL,PARM.JJK] = PARM.RWT[PARM.ISL,PARM.JJK];
            }
            return;
		}
	}
}

