using System;

namespace Epic
{
    public partial class Functions
    {
        public static double WRAIN(double R6, double X, double RFSD, double RFSK, double RFVM)
        {
            // EPICv0810
            // Translated by Emily Jordan
            //  THIS SUBPROGRAM COMPUTES DAILY PRECIPITATION AMOUNT FROM SKEWED
            //!   		//  NORMAL DISTRIBUTION.

            //Modified by Paul Cain on 7/30/2012 to fix build errors.

            /* ADDITIONAL CHANGE
            * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class 
            *              and to make the function return double variable to satisfy a call to 
             *              it in Main.
            */

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            // USE PARM
            MODPARAM PARM = MODPARAM.Instance;

            double XLV = (X - R6) * R6 + 1.0; //XLV is a local variable, X is from Parm I hope.
            XLV = (Math.Pow(XLV, 3) - 1.0) * 2.0 / RFSK;
            double WRAIN = XLV * RFSD * RFVM; //Should WRAIN be a local variable? Could not find in MODPARAM.cs
            if (WRAIN < .01) WRAIN = .01;

            return WRAIN;


        }
    }
}
