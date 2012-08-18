using System;

namespace Epic
{
	public partial class Functions
	{
        private int I3;
        private int p;
        private int p_2;
        private double NT;

		public static double ALPYR (ref int IYR, ref int NYD, ref int LPYR)
		{
			// EPICv0810
			// Translated by Brian Cain
			// No formal description from original file

            /* ADDITIONAL CHANGE
           * 8/17/2012    Modified by Paul Cain by changing this method 
           *              to a static method so that the method conforms 
           *              to a its call in Main.
           */
			
			NYD = 1;
			if (IYR%100 != 0){
				if (IYR%4 != 0) return NYD;	
			}
			else{
				if (IYR%400 != 0) return NYD;
			}
			NYD = LPYR;
			return NYD;
		}
	}
}

