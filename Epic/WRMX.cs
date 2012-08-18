using System;

namespace Epic
{
    public partial class Functions
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public static void WRMX()
        {
            //EPICv0810
            //Translated by Emily Jordan
            //This file supposedly computes max solar radiation at the earths surface.

            // The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
            * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
            *              and added reference to MODPARM. 
            */

            double XI = PARM.JDA;   //I think XI is a local variable, so I made it a double.
            double SD = .4102 * Math.Sin((XI - 80.25)/PARM.PIT); //SD is also a local variable.
            double CH = -PARM.YTN * Math.Tan(SD); //CH is also probably a local variable.
			
			double H; //Created H as it seemes to be a local variable being given value by the if statements.
			
            if(CH >= 1.0) H = 0.0;
            else
            {
                if(CH <= -1.0) H = 3.1416;
                else H = Math.Acos(CH);

            }

            PARM.DD = 1.0 + .0335 * Math.Sin((XI + 88.2)/PARM.PIT);
            PARM.RAMX = 30.0 * PARM.DD * (H * PARM.YLTS * Math.Sin(SD) + PARM.YLTC * Math.Cos(SD) * Math.Sin(H));
            return;
        }
    }
}


