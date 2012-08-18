using System;

namespace Epic
{
	public partial class Functions
	{
		public static void HSGCN ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program contains the SCS hydrologic soil group-curve number table
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
             * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
             */
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double[,] CNX = new double[35,4] {{77.0,72.0,67.0,70.0},{65.0,66.0,62.0,65.0},{63.0,63.0,61.0,61.0},{59.0,66.0,58.0,64.0},{55.0,63.0,51.0,68.0},{49.0,39.0,47.0,25.0},{6.0,30.0,45.0,36.0},{25.0,59.0,72.0,74.0},{43.0,39.0,98.0,86.0},{81.0,78.0,79.0,75.0},{74.0,71.0,76.0,75.0},{74.0,73.0,72.0,70.0},{77.0,72.0,75.0,69.0},{73.0,67.0,79.0,69.0},{61.0,67.0,59.0,35.0},{58.0,66.0,60.0,55.0},{74.0,82.0,84.0,65.0},{61.0,98.0,91.0,88.0},{85.0,84.0,82.0,80.0},{78.0,84.0,83.0,82.0},{81.0,79.0,78.0,85.0},{81.0,83.0,78.0,80.0},{76.0,86.0,79.0,74.0},{81.0,75.0,70.0,71.0},{77.0,73.0,70.0,82.0},{87.0,90.0,77.0,74.0},{98.0,94.0,91.0,89.0},{88.0,86.0,82.0,81.0},{88.0,87.0,85.0,84.0},{82.0,81.0,89.0,85.0},{85.0,83.0,83.0,80.0},{89.0,84.0,80.0,88.0},{83.0,79.0,78.0,83.0},{79.0,77.0,86.0,89.0},{92.0,82.0,80.0,98.0}};

            PARM.CN2 = CNX[PARM.LUN, PARM.ISG];
            return;
		}
	}
}

