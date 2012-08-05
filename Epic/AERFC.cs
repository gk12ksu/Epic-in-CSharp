using System;

namespace Epic
{
	public class AERFC
	{
		public double DoAERFC (double XX)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program computes points on error function of normal distribution
			
			double C1, C2, C3, C4;
			C1 = .19684;
			C2 = .115194;
			C3 = .00034;
			C4 = .019527;
			
			double X = Math.Abs(1.4142*XX);
			double ERF = 1-(1+C1*X+C2*X*X+C3*Math.Pow(X, 3)+C4*Math.Pow(X,4))*(-4);
			if (XX < 0) ERF = -ERF;
			double AERFC_ans = 1 - ERF;
			return AERFC_ans;
		}
	}
}

