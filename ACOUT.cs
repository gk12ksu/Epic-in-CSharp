using System;


namespace Epic
{
	class acout
	{
		public acout(ref double XX, ref double QQ, ref double GKG)
		{
			// EPIC0810
			// Translated by Brian Cain
			// No description of what this file does located in the fortran file
			// Documentation on what this file is actually doing is needed
			// 1.D-4, 1.D-3, 1.D3 means double precision
			// 1.D-4 == 1*10^-4 .... ect
			double X1, X2, X3, XI;
		
			if (XX < (1*Math.Pow(10,-10))) return;
		
			X1 = .1*GKG*XX/(QQ+(1*Math.Pow(10,-1)));
			double N2;
			if (X1 < 1000)
			{
				int x;
				for (x = 0; x < 4; x++){
					N2 = X1;
					if (N2 > 0) break;
					X1 = X1*(1*Math.Pow(10,3));
				}
				double I = Math.Min (x, 4);
				XI = I;
			}
			else{
				XI = 0;
				X1 = .001*X1;
			}
			N2 = XX+.5;
			X2 = N2;
			double N1 = X1+.5;
			X1 = N1;
			X3 = ((1*Math.Pow(10,-4))*X1) + (1*Math.Pow(10, -4))*XI;
			XX = X2 + X3;
			return;
		}
	}
}
