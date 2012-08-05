using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program writes the lagoon manure balance
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/11/2012
     */
    public class NLGB
    {
        public NLGB(ref double WTI, ref double WTO, ref double WTB, ref double WTE, ref double KW, ref double MSO)
        {
            double DF = WTB + WTI - WTO - WTE;
            double PER = 200.0 * DF / (WTB + WTE);

            //This program writes to file KW(1)
            //file.Write("LAGOON MANURE BALANCE\n");
            //file.Write("     PER ="+PER+"  DF  ="+DF+"  WTB ="+WTB+"  WTI ="+WTI+"  WTO ="+WTO+"  WTE ="+WTE+"\n");

        }
    }
}