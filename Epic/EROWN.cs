using System;

namespace Application
{
	public class EROWN
	{
		public EROWN (ref double Y1)
		{
			//EPIC0810
			//Translated by Heath Yates 
			//This computes potential wind erosion rate for 
			//bare soil given wind speed
			
			//Use parm 
			double YWR;
			double DU10 = U10 * Y10; 
			
			if( DU10 > U10MX(MO))
			{
				U10MX(MO) = DU10;
			}
			
			double USTR = 0.0408 * DU10;
			double X1 = USTR * USTR - USTW;
			
			if( X1 < 0) 
			{
				EROWN = 0; 
			}
			else
			{
				YWR = 0.255 * Math.Pow (X1,1.5);
				EROWN = YWR * ALG;
			}
			return;
		}
	}
}

