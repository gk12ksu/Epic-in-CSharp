using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program predicts daily C loss, given soil loss and
     * enrichment ratio.
     * 
     * This file has had its array indicies shifted for C#
     * This file has had its go to statements removed.
     * Last Modified On 7/8/2012
     */
    public class NCQYL
    {
        public NCQYL()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            
            double Y1 = PARM.WBMC[PARM.LD1 - 1];
            double Y4 = PARM.PKRZ[PARM.LD1 - 1];
            double QBC = 0.0;
            double VBC = 0.0;
            double YBC = 0.0;
            double YOC = 0.0;
            double TOT = PARM.WHPC[PARM.LD1 - 1] + PARM.WHSC[PARM.LD1 - 1] + PARM.WLMC[PARM.LD1 - 1] + PARM.WLSC[PARM.LD1 - 1];
            double X1 = 1.0 - PARM.YEW;
            YOC = PARM.YEW * TOT;
            PARM.WHSC[PARM.LD1 - 1] = PARM.WHSC[PARM.LD1 - 1] * X1;
            PARM.WHPC[PARM.LD1 - 1] = PARM.WHPC[PARM.LD1 - 1] * X1;
            PARM.WLS[PARM.LD1 - 1] = PARM.WLS[PARM.LD1 - 1] * X1;
            PARM.WLM[PARM.LD1 - 1] = PARM.WLM[PARM.LD1 - 1] * X1;
            PARM.WLSL[PARM.LD1 - 1] = PARM.WLSL[PARM.LD1 - 1] * X1;
            PARM.WLSC[PARM.LD1 - 1] = PARM.WLSC[PARM.LD1 - 1] * X1;
            PARM.WLMC[PARM.LD1 - 1] = PARM.WLMC[PARM.LD1 - 1] * X1;
            PARM.WLSLC[PARM.LD1 - 1] = PARM.WLSLC[PARM.LD1 - 1] * X1;
            PARM.WLSLNC[PARM.LD1 - 1] = PARM.WLSC[PARM.LD1 - 1] - PARM.WLSLC[PARM.LD1 - 1];
            
            double V;
            if (!(Y1 < .01))
            {

                double DK = .0001 * PARM.PRMT[20] * PARM.WOC[PARM.LD1 - 1];
                X1 = PARM.PO[PARM.LD1 - 1] - PARM.S15[PARM.LD1 - 1];
                double XX = X1 + DK;
                V = PARM.QD + Y4;
                double X3 = 0.0;

                if (!(V < Math.Pow(10, -10)))
                {

                    X3 = Y1 * (1.0 - Math.Exp(-V / XX));
                    double CO = X3 / (Y4 + PARM.PRMT[43] * PARM.QD);
                    double CS = PARM.PRMT[43] * CO;
                    VBC = CO * Y4;
                    Y1 = Y1 - X3;
                    QBC = CS * PARM.QD;

                    //COMPUTE WBMC LOSS WITH SEDIMENT

                    if (!(PARM.YEW < Math.Pow(10, -10)))
                    {
                        CS = DK * Y1 / XX;
                        YBC = PARM.YEW * CS;
                    }
                }
            }

            PARM.WBMC[PARM.LD1 - 1] = Y1 - YBC;      

            for (int L = 2; L < PARM.NBSL; L++)
            {
                PARM.ISL = PARM.LID[L - 1];
                Y1 = PARM.WBMC[PARM.ISL - 1] + VBC;
                VBC = 0.0;
                if (Y1 >= .01)
                {
                    V = PARM.PKRZ[PARM.ISL - 1];
                    if (V > 0.0) 
                        VBC=Y1 * (1.0 - Math.Exp(-V / (PARM.PO[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1] + .0001 * PARM.PRMT[20] * PARM.WOC[PARM.ISL - 1])));
                }
                PARM.WBMC[PARM.ISL - 1] = Y1 - VBC;
            }
            PARM.SMM[74, PARM.MO - 1] = PARM.SMM[74, PARM.MO - 1] + VBC;
            PARM.VAR[74] = VBC;
            PARM.SMM[75, PARM.MO - 1] = PARM.SMM[75, PARM.MO - 1] + QBC;
            PARM.VAR[75] = QBC;
            YOC = YOC + YBC;
            PARM.SMM[76, PARM.MO - 1] = PARM.SMM[76, PARM.MO - 1] + YOC;
            PARM.VAR[76] = YOC;


        }
    }
}