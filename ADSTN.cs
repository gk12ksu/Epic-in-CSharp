using System;

namespace Epic
{
	public partial class Functions
	{
		public static double ADSTN (ref double RN1, ref double RN2)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program computes a standard normal deviate given
			// two random numbers RN1 & RN2
			
			return Math.Sqrt(-2*Math.Log (RN1))*Math.Cos(6.283185*RN2);
		}
	}
}

