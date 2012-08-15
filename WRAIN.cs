using System;

namespace Epic
{
    public class WRAIN
    {

        private static MODPARAM PARM = MODPARAM.Instance;

        public WRAIN(double R6, double X, double RFSD, double RFSK, double RFVM)
        {
            // EPICv0810
            // Translated by Emily Jordan
            //  THIS SUBPROGRAM COMPUTES DAILY PRECIPITATION AMOUNT FROM SKEWED
            //!   		//  NORMAL DISTRIBUTION.

            //Modified by Paul Cain on 7/30/2012 to fix build errors.

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            // USE PARM


            double XLV = (X - R6) * R6 + 1.0; //XLV is a local variable, X is from Parm I hope.
            XLV = (Math.Pow(XLV, 3) - 1.0) * 2.0 / RFSK;
            double WRAIN = XLV * RFSD * RFVM; //Should WRAIN be a local variable? Could not find in MODPARAM.cs
            if (WRAIN < .01) WRAIN = .01;

            return;


        }
    }
}
