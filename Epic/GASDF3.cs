using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program solves the gas diffusion equation
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/6/2012
     */
    public class GASDF3
    {
        public GASDF3()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double[] DPRC = new double[30], DPRN = new double[30], DPRO = new double[30], YTP = new double[30];
            double[] XTP1 = new double[30], XTP2 = new double[30], XTP3 = new double[30], XTP4 = new double[30];
            double[] XTP5 = new double[30], XTP6 = new double[30];
            double GASC = 8.3145, DCAO = .064, DCAC = .05, DCAN = .051;
            double CUPO = 279.0, CLOO = 240.0, CUPC = .18, CLOC = 10.0, CUPN = .00018, CLON = 1.0;
            double PPO2 = .21, PPCO2 = 380.0 * Math.Pow(10, -6), PPN2O = 312.0 * Math.Pow(10, -9);
            double DZX = 1000.0 * PARM.DZ;
            double SM1 = 0.0;

            for (int J = 1; J < PARM.NBSL; J++)
            {
                int L = PARM.LID[J - 1];
                YTP[L - 1] = PARM.RSPC[L - 1];
                XTP1[L - 1] = PARM.WNO3[L - 1];
                XTP2[L - 1] = PARM.WNO2[L - 1];
                XTP3[L - 1] = PARM.WN2O[L - 1];
                XTP4[L - 1] = PARM.WBMC[L - 1];
                XTP5[L - 1] = PARM.RWT[L - 1, PARM.JJK - 1];
                XTP6[L - 1] = Math.Max(Math.Pow(10, -10), PARM.DRWX[L - 1]);
                SM1 = SM1 + PARM.WNO3[L - 1] + PARM.WNO2[L - 1] + PARM.WN2O[L - 1];
            }

            Epic.AINTRI(PARM.PO, PARM.TPOR, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(PARM.ST, PARM.VWC, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(PARM.STMP, PARM.SOT, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(YTP, PARM.RSPC, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(XTP1, PARM.WNO3, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(XTP2, PARM.WNO2, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(XTP3, PARM.WN2O, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(XTP4, PARM.WBMC, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(XTP5, PARM.RWTZ, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(XTP6, PARM.DRWX, PARM.NBSL, PARM.NBCL);
            Epic.AINTRI(PARM.FC, PARM.VFC, PARM.NBSL, PARM.NBCL);

            double ZZ = DZX;
            double Z1 = 1000.0 * (PARM.Z[PARM.LID[PARM.NBSL - 1] - 1] - PARM.ZC[PARM.NBCL - 2]);
            if (Z1 < Math.Pow(10, -5)) PARM.NBCL = PARM.NBCL - 1;

            for (int I = 1; I < PARM.NBCL; I++)
            {
                if (I == PARM.NBCL)
                    ZZ = 1000.0 * (PARM.Z[PARM.LID[PARM.NBSL - 1] - 1] - PARM.ZC[PARM.NBCL - 2]);
                PARM.TPOR[I - 1] = PARM.TPOR[I - 1] / ZZ;
                PARM.VWC[I - 1] = PARM.VWC[I - 1] / ZZ;
                PARM.VFC[I - 1] = PARM.VFC[I - 1] / ZZ;
                double ABST = PARM.SOT[I - 1] + 273.15;
                PARM.AFP[I - 1] = Math.Max(Math.Pow(10, -5), PARM.TPOR[I - 1] - PARM.VWC[I - 1]);
                double HKF = .018 / (GASC * ABST);

                //THIS IS THE MILLINGTON-QUIRCK COEFF
                double EPS = Math.Pow(PARM.AFP[I - 1], 3.333) / Math.Pow(PARM.TPOR[I - 1], 2);

                //DIFFUSION COEFFICIENT IN SOIL
                double DCSO = DCAO * EPS;
                double X1 = .01 * ABST;
                double XXO = Math.Exp(-66.7354 + 87.4755 / X1 + 24.4526 * Math.Log(X1));
                PARM.HKPO[I - 1] = HKF * (1.0 / XXO - 1.0);
                DPRO[I - 1] = DCSO * PARM.AFP[I - 1] / (PARM.TPOR[I - 1] + PARM.VWC[I - 1] * (1.0 / PARM.HKPO[I - 1] - 1.0)) * Math.Pow((ABST / 273.15), 2);

                //FOR CO2
                double DCSC = DCAC * EPS;

                //HERE WE OBTAIN HENRYS CONST AS A FUNCT OF SOIL T DIRECTLY
                //UNITS ARE kpa M3 MOL-1
                double XXC = ((Math.Pow(-2.13308, 9) * ABST + 0.00000223) * ABST - 0.00077777) * ABST + 0.09098;
                PARM.HKPC[I - 1] = HKF * (1.0 / XXC - 1.0);
                DPRC[I - 1] = DCSC * PARM.AFP[I - 1] / (PARM.TPOR[I - 1] + PARM.VWC[I - 1] * (1.0 / PARM.HKPC[I - 1] - 1.0)) * Math.Pow((ABST / 273.15), 1.75);
                //FOR N2O
                double DCSN = DCAN * EPS;
                double XXN = Math.Exp(-60.7467 + 88.828 / X1 + 21.2531 * Math.Log(X1));
                PARM.HKPN[I - 1] = HKF * (1.0 / XXN - 1.0);
                DPRN[I - 1] = DCSN * PARM.AFP[I - 1] / (PARM.TPOR[I - 1] + PARM.VWC[I - 1] * (1.0 / PARM.HKPN[I - 1] - 1.0)) * Math.Pow((ABST / 273.15), 1.75);
            }

            if (PARM.IY == 1.0 && PARM.IDA == PARM.IBD)
                Epic.CCONI();

            //SET TO 0. LAYER ARRAYS OF DAILY PRODUCTION AND CONSUMPTION
            //OF GASES (O2, CO2, N2O, N2 AND N2O+N2 [DDENIT])
            for (int I = 1; I < PARM.NBCL; I++)
            {
                PARM.DO2CONS[I - 1] = 0.0;
                PARM.DCO2GEN[I - 1] = 0.0;
                PARM.DN2OG[I - 1] = 0.0;
                PARM.DN2G[I - 1] = 0.0;
            }
            PARM.DFO2 = 0.0;
            PARM.DFCO2 = 0.0;
            double DFN2O = 0.0;
            double TIME = 0.0;

            //TIME LOOP TO CALCULATE GENERATION AND CONSUMPTION OF
            //GASES, GAS TRANSPORT, AND FLUX AT THE SURFACE
            for (int IT = 1; IT < PARM.NBDT; IT++)
            {
                TIME = TIME + PARM.DTG;
                //GAS FLUXES AT THE SURFACE (RE-INITIALIZED TO ZERO EVERY HOUR)
                PARM.GFO2 = 0.0;
                PARM.GFCO2 = 0.0;
                PARM.GFN2O = 0.0;
                //CALCULATE GENERATION AND CONSUMPTION OF GASES
                Epic.NDNITCI();
                //RE-CALCULATE GAS CONCENTRATION IN LIQUID AND AIR PHASES
                //PRODUCTION AND CONSUMPTION OF GASES
                Epic.NCCONC();
                //MOVE O2 AND STORE VALUES OF GAS CONC. IN 2-DIM ARRAY
                Epic.GASTRANS(PARM.CGO2, DPRO, CUPO, CLOO, 1, DCAO);
                for (int I = 1; I < PARM.NBCL; I++)
                {
                    PARM.TDAO[IT - 1, I - 1] = PARM.CGO2[I - 1];
                }
                //GFCO2=HOURLY O2 FLUX AT THE SOIL SURFACE (G/M2)
                PARM.DFO2 = PARM.DFO2 + PARM.GFO2;
                //MOVE CO2 AND STORE VALUES OF GAS CONC. IN 2-DIM ARRAY
                Epic.GASTRANS(PARM.CGCO2, DPRC, CUPC, CLOC, 2, DCAC);
                for (int I = 1; I < PARM.NBCL; I++)
                {
                    PARM.TDAC[IT - 1, I - 1] = PARM.CGCO2[I - 1];
                }
                //GFCO2=HOURLY CO2 FLUX AT THE SOIL SURFACE (G/M2)
                PARM.DFCO2 = PARM.DFCO2 + PARM.GFCO2;
                //MOVE N2O AND STORE VALUES OF GAS CONC. IN 2-DIM ARRAY
                Epic.GASTRANS(PARM.CGN2O, DPRN, CUPN, CLON, 3, DCAN);
                for (int J = 1; J < PARM.NBCL; J++)
                {
                    PARM.TDAN[IT - 1, J - 1] = PARM.CGN2O[J - 1];
                }
                //CALCULATE DAILY SUM
                DFN2O = DFN2O + PARM.GFN2O;
                //RECALCULATE GAS CONCENTRATIONS IN LIQUID AND GAS PHASES
                for (int J = 1; J < PARM.NBCL; J++)
                {
                    PARM.AO2C[J - 1] = PARM.CGO2[J - 1] * PARM.AFP[J - 1] + PARM.CLO2[J - 1] * PARM.VWC[J - 1];
                    PARM.ACO2C[J - 1] = PARM.CGCO2[J - 1] * PARM.AFP[J - 1] + PARM.CLCO2[J - 1] * PARM.VWC[J - 1];
                    PARM.AN2OC[J - 1] = PARM.CGN2O[J - 1] * PARM.AFP[J - 1] + PARM.CLN2O[J - 1] * PARM.VWC[J - 1];
                }
                Epic.NCCONC();
            }

            PARM.SN2O = 0.0;
            PARM.SN2 = 0.0;
            PARM.SDN = 0.0;

            for (int J = 1; J < PARM.NBCL; J++)
            {
                //AFP=TPOR(J)-VWC(J) Commented out in the source file (Brian Dye)
                PARM.WN2O[J - 1] = PARM.WN2O[J - 1] - PARM.DN2OG[J - 1];
                PARM.SN2 = PARM.SN2 + PARM.DN2G[J - 1];
                PARM.SN2O = PARM.SN2O + PARM.DN2OG[J - 1];
                PARM.SDN = PARM.SDN + PARM.DN2OG[J - 1] + PARM.DN2G[J - 1];
                XTP1[J - 1] = PARM.WNO3[J - 1];
                XTP2[J - 1] = PARM.WNO2[J - 1];
                XTP3[J - 1] = PARM.WN2O[J - 1];
                XTP4[J - 1] = PARM.WBMC[J - 1];

            }

            Epic.AINTRO(XTP1, PARM.WNO3, PARM.NBSL, PARM.NBCL);
            Epic.AINTRO(XTP2, PARM.WNO2, PARM.NBSL, PARM.NBCL);
            Epic.AINTRO(XTP3, PARM.WN2O, PARM.NBSL, PARM.NBCL);
            Epic.AINTRO(XTP4, PARM.WBMC, PARM.NBSL, PARM.NBCL);
            //DFN2O=DFN2O+GFN2O Commneted out in the source file (Brian Dye)
            double SM2 = 0.0;

            for (int J = 1; J < PARM.NBSL; J++)
            {
                int L = PARM.LID[J - 1];
                SM2 = SM2 + PARM.WNO3[L - 1] + PARM.WNO2[L - 1] + PARM.WN2O[L - 1];
            }
            double DF = SM1 - SM2 - PARM.SDN;
            if (Math.Abs(DF) > .01)
            {
                //WRITE(KW(1),3)IY,MO1,KDA,SM1,SDN,DFN2O,SN2O,SN2,SM2,DF original write statement
                //file.Write("    GAS XBAL " + IY + "" + MO1 + "" + KDA + "  BTOT=" + SM1 + "  DNIT=" + SDN + "  FN2O=" + DFN2O + "  SN2O=" + SN2O + "  SN2=" + SN2);
                //file.Write("\n                            FTOT=" + SM2 + "DF=" + DF + "\n");
            }
            return;
        }
    }
}