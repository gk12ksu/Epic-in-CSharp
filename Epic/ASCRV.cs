using System;

namespace Epic
{
	public partial class Functions
	{
		public static double ASCRV (ref double X1, ref double X2, double X3, double X4)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program computes S Curve params given 2 (x,y) points
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
             * 8/16/2012    Modified by Paul Cain to make two parameters pass by 
             *              value instead of pass by reference because they did 
             *              not need to be pass by reference and Main passes them 
             *              literals. 
             */
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			double XX = Math.Log (X3/X1-X3);
			X2 = (XX-Math.Log (X4/X2-X4))/(X4-X3);
			X1 = XX+X3*X2;
			return XX;
		}
	}
}

