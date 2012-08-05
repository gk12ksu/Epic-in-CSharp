using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program computes the N & P balances at the end of the
     * simulation.
     * 
     * Last Modified On 7/8/2012
     */
    public class NBL
    {
        public NBL(ref double BTN, ref double RN, ref double YON, ref double QNO3, ref double SSFN, ref double PRKN, ref double DN, ref double TFO, ref double YLN, ref double VOL, ref double FNO3, ref double FNH3, ref double FX, ref double BURN, ref double FTN, ref int KBL, ref int[] KW, ref int MSO)
        {
            double DF = BTN + RN - YON - QNO3 - SSFN - PRKN - DN - YLN - VOL + FNO3 + FNH3 - FTN + FX - BURN + TFO;
            double PER = 100.0 * DF / FTN;

            switch (KBL)
            {
                case 1:
                    //Writes to file KW(1) in original source code
                    //file.Write("          N BALANCE\n");
                    //file.Write("     PER ="+PER+"  DF  ="+DF+"  BTOT="+BTN+"  PCP ="+RN+"  Y   ="+YON+"  Q   ="+QNO3+"\n);
                    //file.Write("     SSF ="+SSFN+"  PRK ="+PRKN+"  DNIT="+DN+"  YLD ="+YLN+"  VOL ="+VOL+"  FNO3="+FNO3+"\n);
                    //file.Write("     FNH3="+FNH3+"  FIX ="+FX+"  FORG="+TFO+"  BURN="+BURN+"  FTOT="+FTN+"\n");
                    break;
                case 2:
                    //file.Write("          P BALANCE\n");
                    //file.Write("     PER ="+PER+"  DF  ="+DF+"  BTOT="+BTN+"  Y   ="+YON+"  Q   ="+QNO3+"  PRK ="+PRKN+"\n");
                    //file.Write("     YLD ="+YLN+"  FPML="+FNO3+"  FPO ="+TFO+"  ETOT="+FTN+"\n"); 
                    break;
                case 3:
                    //file.Write("          K BALANCE\n");
                    //file.Write("     PER ="+PER+"  DF  ="+DF+"  BTOT="+BTN+"  Y   ="+YON+"  Q   ="+QNO3+"  SSF ="+SSFN+"\n");
                    //file.Write("     PRK ="+PRKN+"  YLD ="+YLN+"  FKM ="+FNO3+"  ETOT="+FTN+"\n");
                    break;
            }


        }
    }
}