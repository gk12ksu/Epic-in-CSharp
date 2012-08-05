using System;

namespace Epic
{
    public class WHLRMX
    {

        private static MODPARAM PARM = MODPARAM.Instance;

        public WHLRMX(double ID)
        {
            // EPICv0810
            // Translated by Emily Jordan
            // THIS SUBPROGRAM COMPUTES DAY LENGTH & MAX SOLAR RADIATION AT THE
            //EARTHS SURFACE.

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            /* ADDITIONAL CHANGE
            * 7/31/2012    Modified by Paul Cain to fix build errors
            */

            // USE PARM
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double XI = PARM.JDA; //XI is local variable.
            double SD = .4102 * Math.Sin((XI - 80.25) / PARM.PIT); //SD is also probably a local variable.
            double CH = -PARM.YTN * Math.Tan(SD); //CH also probably local variable.

            double H; //Create H so that is can be stored somehwere when they start using it.

            if (CH >= 1.0) H = 0.0;
            else
            {
                if (CH <= -1.0) H = 3.1416;
                else H = Math.Acos(CH);
            }

            PARM.DD = 1.0 + .0335 * Math.Sin((XI + 88.2) / PARM.PIT);
            PARM.HRLT = 7.72 * H;
            PARM.HR0 = PARM.HRLT;
            PARM.RAMX = 30.0 * PARM.DD * (H * PARM.YLTS * Math.Sin(SD) + PARM.YLTC * Math.Cos(SD) * Math.Sin(H));
            return;

        }
    }
}

