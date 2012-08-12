using System;

namespace Epic
{
	public class GETCL
	{
		public GETCL (ref double[] KW, ref double[] CM)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This command line routine for Microsoft/UNIX/SALFORD
            
            string CM_str;
            KW = new double[200];
            CM = new double[9];
			
			// Which CM is being used here???
			// Refer to fortran file, CM is defined 3 times

            double NNARG = 0.0; //Epic.NARGS(); // What is NARGS? Only appears once in the entire fortran project file
            if (NNARG > 9) NNARG = 9;

            int I;
            for (I = 1; I <= NNARG; I++){
                //GETARG(I-1, CM[I], NCHAR);
				// Retrieve the POS-th argument that was passed on the command line when the containing program was invoked.
            }
            return;
		}
	}
}

