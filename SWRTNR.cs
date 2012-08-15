using System;

namespace Application
{
	public class SWRTNR
	{
		public SWRTNR (ref double CL, ref double SA, ref double OC, ref double WP, ref double FC)
		{
			// EPICv0810
			// Translated by Brian Cain
            // This program uses Walter Rawl's method for estimating
			// soil water content at 33 and 1500 kpa
			
			WP = .026+.005*CL+.0158*OC;
      		FC = .2576-.002*SA+.0036*CL+.0299*OC;
			return;
		}
	}
}

