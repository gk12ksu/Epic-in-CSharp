using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program computes the C balance at the end of the
     * simulation.
     * 
     * Last Modified On 7/8/2012
     */
    public class NCBL
    {
        public NCBL(ref double BTC, ref double YOC, ref double VBC, ref double QBC, ref double RSPC, ref double TFOC, ref double RSDC, ref double BURN, ref double DPLC, ref double PLCX, ref double FTC, ref double[] KW, ref int MSO)
        {
            double DF = BTC - YOC - VBC - QBC - RSPC + TFOC + RSDC - BURN + DPLC - FTC - PLCX;
            double PER = 100.0 * DF / FTC;

            //This program writes to KW(1) in the original source
            //file.Write("\n          C BALANCE\n");
            //file.Write("     PER ="+PER+"  DF  ="+DF+"  BTOT="+BTC+"  Y   ="+YOC+"  PRK ="+VBC+"  Q   ="+QBC+"\n");
            //file.Write("     RSPC="+RSPC+"  RSDC="+RSDC+"  TFOC="+TFOC+"  BURN="+BURN+"  DPLC="+DPLC+"  FPLC="+PLCX+"\n");
            //file.Write("     FTOT="+FTC+"\n");

            BTC = FTC;
        }
    }
}