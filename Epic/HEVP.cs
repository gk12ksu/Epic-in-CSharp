using System;

namespace Epic
{
	public class HEVP
	{
        private static MODPARAM PARM = MODPARAM.Instance;

		public HEVP ()
		{
            // EPICv0810
			// Translated by Brian Cain

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

            // This program estimates daily evapotranspiration. There are
            // four options for computing potential evap(PENMAN-MONTEITH, PENNMAN, PRIESTLEY-TAYLOR, & HARGREAVES)
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double SUM = .01;
            double EPP=0.0;
            double NN=PARM.NBC[PARM.IRO];
            double CHMX=0.0;
            double RV;

            int K;
            double I1;
			int K1;
            for (K = 0; K < NN; K++){
                K1 = PARM.JE[K];
                if (K1 > PARM.MNC) continue;
                SUM = SUM+PARM.SLAI[K1];
                if (PARM.CHT[K1] > CHMX){
                    CHMX = PARM.CHT[K1];
                }
                PARM.EP[K1] = 0.0;
            }
           
            double X1 = Math.Max(.4*SUM,PARM.PRMT[40]*(PARM.CV+.1));
            double EAJ = Math.Exp(-X1);
            if (PARM.SNO > 5.0){
                PARM.ALB = .6;
                EAJ = .5;
            }
            else if (PARM.ICV > 0){
                X1 = 1.0-EAJ;
                PARM.ALB = (.23*X1+PARM.PALB*PARM.FCV)/(PARM.FCV+X1);
                EAJ = Math.Min(EAJ,1.0-PARM.FCV);
            }
            else{
                PARM.ALB = .23*(1.0-EAJ)+PARM.SALB*EAJ;
            }

            double TK = PARM.TX+273.0;
            double RSO = PARM.RAMX;
            double XL = 2.501-(2.2*Math.Pow(10, -3))*PARM.TX;             
            double EA = Functions.ASVP(TK);
            PARM.ED = EA*PARM.RHD;
            PARM.VPD = EA-PARM.ED;
            PARM.SMM[8, PARM.MO] = PARM.SMM[8, PARM.MO] + PARM.VPD;
            PARM.VAR[9] = PARM.VPD;
            double RALB1 = PARM.SRAD*(1.0-PARM.ALB);
            double DLT = EA*(6790.5/TK-5.029)/TK;
            double XX = DLT+PARM.GMA;
            double TK4 = Math.Pow(TK, 4);
            double RBO = (.34-.14*Math.Sqrt(PARM.ED))*(4.9*Math.Pow(10, -9))*TK4;
            double RTO = Math.Min(.99,PARM.SRAD/(RSO+.1));
            double RN = RALB1-RBO*(.9*RTO+.1);
            double X2 = RN*DLT;

            double RAMM, UZZ, ZZ;
            switch(PARM.IET){	
                case (5):
                    //! BAIER-ROBERTSON PET METHOD
                    PARM.EO = Math.Max(0.0,.288*PARM.TMX-.144*PARM.TMN+.139*RSO-4.931);
					break;
                case (4):
                    //! HARGREAVES PET METHOD
                    RAMM = RSO/XL;
                    PARM.EO = Math.Max(0.0,PARM.PRMT[37]*RAMM*(PARM.TX+17.8)*Math.Pow((PARM.TMX-PARM.TMN), PARM.PRMT[12]));
                    PARM.EO = Math.Min(9.0,PARM.EO);
					break;
                case (3):
                    //! PRIESTLEY-TAYLOR PET METHOD
                    RAMM = RALB1/XL;
                    PARM.EO = 1.28*RAMM*DLT/XX;
					break;
                case (2):
                    //! PENMAN PET METHOD
                    double FWV = 2.7+1.63*PARM.U10;
                    double X3 = PARM.GMA*FWV*PARM.VPD;
                    X1 = X2/XL+X3;
                    PARM.EO = X1/XX;
					break;
                case (1):
                    //! PENMAN-MONTEITH PET METHOD
                    double RHO=.01276*PARM.PB/(1.0+.00367*PARM.TX);
                    if (PARM.IGO > 0){
                        if (CHMX < 8.0){
                            UZZ = PARM.U10;
                            ZZ = 10.0;
                        }
                        else{
                            ZZ = CHMX+2.0;
                            UZZ = PARM.U10*Math.Log(ZZ/.0005)/9.9035;
                        }
                        X1 = Math.Log10(CHMX+.01);
                        double Z0 = Math.Pow(10.0, (.997*X1-.883));
                        double ZD = Math.Pow(10.0,(.979*X1-.154));
                        RV = 6.25*(Math.Pow(Math.Log((ZZ-ZD)/Z0), 2/UZZ));
                        X3 = PARM.VPD-PARM.VPTH[PARM.JJK];

                        double FVPD;
                        if (X3 > 0.0){
                            FVPD = Math.Max(.1,1.0-PARM.VPD2[PARM.JJK]*X3);
                        }
                        else{
                            FVPD = 1.0;
                        }
                        double G1 = PARM.GSI[PARM.JJK]*FVPD;
                        double RC = PARM.PRMT[0]/((SUM+.01)*G1*Math.Exp(.00155*(330.0-PARM.CO2)));
                        EPP = PARM.PRMT[73]*(X2+86.66*RHO*PARM.VPD/RV)/(XL*(DLT+PARM.GMA*(1.0+RC/RV)));
                    }
                      
                    RV = 350.0/PARM.U10;
                    PARM.EO = PARM.PRMT[73]*(X2+86.66*RHO*PARM.VPD/RV)/(XL*XX);
                    if (EPP > PARM.EO) PARM.EO = EPP;
					break;

                default:
                    //! HARGREAVES PET METHOD
                    RAMM = RSO/XL;
                    PARM.EO = Math.Max(0.0,PARM.PRMT[37]*RAMM*(PARM.TX+17.8)*Math.Pow((PARM.TMX-PARM.TMN),PARM.PRMT[13]));
                    PARM.EO = Math.Min(9.0,PARM.EO);
					break;
            }

            if (PARM.IET > 1) EPP = Math.Min(PARM.EO, PARM.EO*SUM/3.0);
            if (PARM.IGO > 0){
                XX = EPP/SUM;
                for (K = 0; K < NN; K++){
                    K1 = PARM.JE[K];
                    if (K1 > PARM.MNC) continue;
                    PARM.EP[K1] = PARM.SLAI[K1]*XX;
                }

            }

            PARM.ES = PARM.EO*EAJ;
            double ST0 = RALB1;
            PARM.ES = Math.Min(PARM.ES,PARM.ES*PARM.EO/(PARM.ES+EPP+Math.Pow(10, -10)));
			double NEV;
            if (PARM.SNO >= PARM.ES){
                // Removed Go To Statement

                PARM.SNO = PARM.SNO-PARM.ES;
                NEV = 1;
                XX = Math.Max(0.0,PARM.EO-PARM.ES);
                if (EPP > XX){
                    X1 = XX/EPP;
                    EPP = XX;
                    for (K = 0; K < NN; K++){
                        K1 = PARM.JE[K];
                        if (K1 > PARM.MNC) continue;
                        PARM.EP[K1] = PARM.EP[K1]*X1;
                    }
                }
                PARM.VAR[64] = EPP;
                PARM.SMM[11, PARM.MO] = PARM.SMM[11, PARM.MO] + EPP;
                return;
            }

            XX = PARM.ES-PARM.SNO;
            PARM.ES = PARM.SNO;
            PARM.SNO=0.0;
            double TOT=0.0;

            double XZ, F;
			double Z1, XY = 0.0;
            int J;
            for (J = 0; J < PARM.NBSL; J++){
                PARM.ISL = PARM.LID[J];
                RTO = 1000.0 * PARM.Z[PARM.ISL];
                SUM = XX*RTO/(RTO+Math.Exp(PARM.SCRP[2,1]-PARM.SCRP[2,2]*RTO));
                XZ = PARM.FC[PARM.ISL] - PARM.S15[PARM.ISL];
                if (PARM.ST[PARM.ISL] < PARM.FC[PARM.ISL])
                {
                    F = Math.Exp(PARM.PRMT[11] * (PARM.ST[PARM.ISL] - PARM.FC[PARM.ISL]) / XZ);
                }
                else{
                    F = 1.0;
                }
                ZZ = SUM-PARM.PRMT[60]*TOT-(1.0-PARM.PRMT[60])*PARM.ES;
                PARM.SEV[PARM.ISL] = ZZ * F;
                XY = PARM.PRMT[4] * PARM.S15[PARM.ISL];
                // Removed Go TO statement
                if (PARM.Z[PARM.ISL] > .2)
                {
                    NEV = J;
                    Z1 = PARM.Z[PARM.LID[J-1]];
                    RTO = (.2 - Z1) / (PARM.Z[PARM.ISL] - Z1);
                    X1 = RTO * PARM.ST[PARM.ISL];
                    X2 = RTO*XY;
                    if (X1 - PARM.SEV[PARM.ISL] < X2) PARM.SEV[PARM.ISL] = X1 - X2;
                    PARM.ES = PARM.ES + PARM.SEV[PARM.ISL];
                    PARM.ST[PARM.ISL] = Math.Max(Math.Pow(10, -5), PARM.ST[PARM.ISL] - PARM.SEV[PARM.ISL]);
                    XX = Math.Max(0.0,PARM.EO-PARM.ES);
                    if (EPP > XX){
                        X1 = XX/EPP;
                        EPP = XX;
                        for (K = 0; K < NN; K++){
                            K1 = PARM.JE[K];
                            if (K1 > PARM.MNC) continue;
                            PARM.EP[K1] = PARM.EP[K1]*X1;
                        }
                    }
                    PARM.VAR[64] = EPP;
                    PARM.SMM[11, PARM.MO] = PARM.SMM[11, PARM.MO] + EPP;
                    return;
                }

                if (PARM.ST[PARM.ISL] - PARM.SEV[PARM.ISL] < XY) PARM.SEV[PARM.ISL] = PARM.ST[PARM.ISL] - XY - Math.Pow(10, -5);
                PARM.ES = PARM.ES + PARM.SEV[PARM.ISL];
                PARM.ST[PARM.ISL] = PARM.ST[PARM.ISL] - PARM.SEV[PARM.ISL];
                TOT = SUM;
            }
            J = PARM.NBSL;
            NEV = J;
            Z1 = PARM.Z[PARM.LID[J-1]];
            RTO = (.2 - Z1) / (PARM.Z[PARM.ISL] - Z1);
            X1 = RTO * PARM.ST[PARM.ISL];
            X2 = RTO*XY;
            if (X1 - PARM.SEV[PARM.ISL] < X2) PARM.SEV[PARM.ISL] = X1 - X2;
            PARM.ES = PARM.ES + PARM.SEV[PARM.ISL];
            PARM.ST[PARM.ISL] = Math.Max(Math.Pow(10, -5), PARM.ST[PARM.ISL] - PARM.SEV[PARM.ISL]);
            XX = Math.Max(0.0,PARM.EO-PARM.ES);
            if (EPP > XX){
                X1 = XX/EPP;
                EPP = XX;
                for (K = 0; K < NN; K++){
                    K1 = PARM.JE[K];
                    if (K1 > PARM.MNC) continue;
                    PARM.EP[K1] = PARM.EP[K1]*X1;
                }
            }
            PARM.VAR[64] = EPP;
            PARM.SMM[11, PARM.MO] = PARM.SMM[11, PARM.MO] + EPP;
            return;
		}
	}
}

