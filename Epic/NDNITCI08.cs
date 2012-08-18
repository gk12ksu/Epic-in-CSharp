using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This loop calculates the production and consumption of O2, CO2, and
     * N2O by soil layer within one hour. It also updates pools of NO3 and
     * NO2 factor to convert mass (kg/ha for a given soil layer) to
     * gas concentration (g/m3 soil)
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     * 
     * The source code of NDNITCI08 seems to be identicle to NDNITCI
     */
    public partial class Functions
    {
        public static void NDNITCI()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double B1 = 70.0, B2 = 140.0, B3 = 720.0, COX = .032, DAO2 = 2.0E-5, O2MW = 32.0, XN = 2.07E16;
            double A = .002, B = 1.04, C = .5, XKN5 = 10.0, XKN3 = 5.0, XKN1 = 0.0005, FD = .19, VWCR = 0.0, RGF = .547, RMF = .01785;

            double TOGM = .1 / PARM.DZ;
/*  Commented out in original source
!     AD1=0.
!     AD2=0.
!     AD4=0.
!     AD5=0.
!     AD6=0.
!     SM1=0.
!     SM2=0.
!     SM3=0.
*/
            for (int J = 1; J <= PARM.NBCL; J++)
            {
                //AD1=AD1+WNO3(J)+WNO2(J)
                //EA=TOTAL ELECTRONS ACCEPTED BY O2 AND N OXIDES
                double EA = 0.0;
                //EAD=TOTAL DEFICIT OF ELECTRONS
                double EAD = 0.0;
                //UUE=UNUSED ELECTRONS (MOL E / (M2 H))
                double UUE = 0.0;
                PARM.EAO2 = 0.0;
                double EAO2R = 0.0;
                double ESRR = 0.0;
                double ESMR = PARM.RSPC[J - 1] / B3;
                double SUM = 0.0;
                double VWCE = Math.Min(0.999, (PARM.VWC[J - 1] - VWCR) / (PARM.TPOR[J - 1] - VWCR));
                double X4 = PARM.SOT[J - 1]+273.15;
                double X3 = .001 *(Math.Pow((Math.Pow(VWCE, (-1.0 / C)) - 1.0), (1.0 / B))) / A;
                double DW = Math.Max(1.01E-6, 1.0E-6 + 8.0E-6 * Math.Pow(X3, (-0.945703126)));
                double DAO2TC = DAO2 * Math.Pow((X4 / 293.15), 6);
                double XKT = 1.5708E-10 * XN * PARM.WBMC[J - 1] * DAO2TC * DW / (DW - 1.0E-6);
                double QTB = XKT * (PARM.CLO2[J - 1] - COX) - ESMR;
                double QTC = XKT * COX * PARM.CLO2[J - 1];
                double O2M = (QTB + Math.Sqrt(QTB * QTB + 4.0 * XKT * QTC)) / (2.0 * XKT);
                PARM.EAO2 = ESMR * O2M / (O2M + COX);
                double X1;
                if (PARM.RWTZ[J - 1] > 0.0)
                {
                    //NEW DERIVATION FOR ELECTRON SUPPLY DUE TO ROOT RESPIRATION
                    //SEE MODEL DOCUMENTATION
                    //ROOT RESPIRED C (KG C HA-1 D-1)
                    X1 = (RGF / (1.0 - RGF)) * Math.Max(0.0, PARM.DRWX[J - 1]) + RMF * PARM.RWTZ[J - 1];
                    double RRTC = .42 * X1; 
	                //ESRR=MOLE E- M-2 H-1 FROM ROOT RESPIRATION - USE ESRR
                    ESRR = 5.833E-4 * X1;
	  	            double RRC = ESRR * B3; 
	                double XKTR = 54.573 * DAO2TC * PARM.RWTZ[J - 1];
	                double QTBR = XKTR * (PARM.CLO2[J - 1] - COX) - ESRR;
	                double QTCR = XKTR * COX * PARM.CLO2[J - 1];            
                    //SOLVE QUADRATIC EQN FOR O2M AND O2MR
                    double O2R = (QTBR + Math.Sqrt(QTBR * QTBR + 4.0 * XKTR * QTCR)) / (2.0 * XKTR);
                    //ELECTRONS FROM MICROBE AND ROOT RESPIRATION ACCEPTED BY O2
                    EAO2R = ESRR * O2R / (O2R + COX);
                }

                SUM = PARM.EAO2 + EAO2R;
                double ESD = ESMR + ESRR - SUM;
                //ELECTRONS AVAILABLE FOR DENITRIFICATION
                ESD = FD * ESD;
                UUE = UUE + ESMR + ESRR - ESD;
                //COMPETITION FOR ELECTRONS AMONG OXIDES OF N
                //CALCULATE WEIGHING FACTORS FIRST
                X1 = PARM.DZ * PARM.VWC[J - 1] * 10.0;
                double CNO3 = Math.Max(1.0E-5, PARM.WNO3[J - 1] / X1);
                double CNO2 = Math.Max(1.0E-5, PARM.WNO2[J - 1] / X1);
                double WN5 = 5.0 * CNO3 / (XKN5 + CNO3);
                double WN3 = 3.0 * CNO2 / (XKN3 * (1.0 + CNO3 / XKN5) + CNO2);
                double WN1 = 1.0 * PARM.CLN2O[J - 1] / (XKN1 * (1.0 + CNO2 / XKN3) + PARM.CLN2O[J - 1]);
                //CALCULATE THE RATES OF REDUCTION OF OXIDES OF N
                X1 = Math.Max(1.0E-10, PARM.WNO3[J - 1] / B1);
                double EAN5 = Math.Min(X1, ESD * WN5 / (WN5 + WN3 + WN1));
                X1 = Math.Max(1.0E-10, PARM.WNO2[J - 1] / B1);
                double EAN3 = Math.Min(X1, ESD * WN3 / (WN5 + WN3 + WN1));
                X1 = Math.Max(1.0E-10, PARM.WN2O[J - 1] / B2);
                double EAN1 = Math.Min(X1, ESD * WN1 / (WN5 + WN3 + WN1));
                //THESE ARE THE RESULTS BY LAYER AT THE END OF ONE HOUR
                //IF NOT ALL ELECTRONS CAN BE ACCEPTED BY O2 (ESD>0.)
                //TOTAL ELECTRONS ACCEPTED AND TRANSFORMATIONS OF N OXIDES
                //DO WE NEED THIS?
                EA = EA + PARM.EAO2 + EAN5 + EAN3 + EAN1;
                EAD = EAD + EAN5 + EAN3 + EAN1;
                //LIQUID POOLS
                PARM.WNO3[J - 1] = PARM.WNO3[J - 1] - EAN5 * B1;
                PARM.WNO2[J - 1] = PARM.WNO2[J - 1] - (EAN3 - EAN5) * B1;
                //GAS POOLS
                //N2O AND N2
                //GENN2O CALCULATES HOW MUCH N2O IS GENERATED (kg/ha)
	            double GENN2O = EAN3 * B1 - EAN1 * B2;
                //DN2OG(J) ACCUMULATES N2O GENERATED DURING A DAY (kg/ha)
                //IN LAYER J
	            PARM.DN2OG[J - 1] = PARM.DN2OG[J - 1] + GENN2O;
                //WN2O(J) UPDATES THE N2O POOL (kg/ha). *WN2O(J)=DN2O(J)*
                PARM.WN2O[J - 1] = PARM.WN2O[J - 1] + GENN2O;
                //AN2OC(J) CONVERTS MASS OF N2O INTO CONCENTRATION (G/M3)
	            PARM.AN2OC[J - 1] = Math.Max(0.0, PARM.WN2O[J - 1] * TOGM);
                //WN2G CALCULATES THE MASS OF N2 GENERATED (kg/ha)
                double WN2G = EAN1 * B2;
                //DN2G(J) ACCUMULATES N2 GENERATED DURING A DAY (kg/ha)
                //IN LAYER J
                PARM.DN2G[J - 1] = PARM.DN2G[J - 1] + WN2G;
                //O2CONS= O2 CONSUMED (kg/ha)
                //(MOL E/M2*1/4*MOL C/MOL E*12 G C/MOL C*10.)
                //FACTOR 10. ABOVE IS TO CONVERT G/M2 TO kg/ha
                double O2CONS = 2.5 * SUM * O2MW;
                //DO2CONS ACCUMULATES O2 CONSUMED DAILY 
                //IN LAYER J (kg/ha)
                PARM.DO2CONS[J - 1] = PARM.DO2CONS[J - 1] + O2CONS;
                //AO2C(J) RECALCULATES O2 CONCENTRATION IN LAYER (G/M3)
                PARM.AO2C[J - 1] = Math.Max(0.0, PARM.AO2C[J - 1] - O2CONS * TOGM);
                //CO2GEN IS CO2 GENERATED (kg/ha)
                //(MOL E/M2*1/4*MOL C/MOL E*12 G C/MOL C*10.)
                //FACTOR 10. ABOVE IS TO CONVERT G/M2 TO kg/ha
                double CO2GEN = SUM*30.0;
                //DCO2GEN(J) ACCUMULATES CO2 GENERATED DAILY 
                //IN LAYER J (kg/ha)
                PARM.DCO2GEN[J - 1] = PARM.DCO2GEN[J - 1] + CO2GEN;
                //ACO2C(J) RECALCULATES CO2 CONCENTRATION IN LAYER (G/M3)
                PARM.ACO2C[J - 1] = Math.Max(0.0, PARM.ACO2C[J - 1] + CO2GEN * TOGM);
                //AD2=AD2+WNO3(J)+WNO2(J)+GENN2O+WN2G

            }

            //DF=AD2-AD1
            //IF(ABS(DF)>.001)WRITE(KW(1),1)IY,MO,KDA,AD1,AD2,DF
            //1 FORMAT(1X,'NDNITCI',3I4,3E16.6) 


        }
    }
}