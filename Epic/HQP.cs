using System;

namespace Epic
{
	public partial class Functions
	{
		public static double HQP (ref double X1, ref double[,,] CQP, ref int ITP, ref int INT)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program has no description
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            CQP = new double[8,17,4];
            double HQP = CQP[0,INT,ITP]+X1*(CQP[1,INT,ITP]+X1*(CQP[2,INT,ITP]+X1*(CQP[3,INT,ITP]+X1*(CQP[4,INT,ITP]+X1*(CQP[5,INT,ITP]+X1*(CQP[6,INT,ITP]+X1*CQP[7,INT,ITP]))))));
            return HQP;
		}
	}
}

