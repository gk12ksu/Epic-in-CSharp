using System;

namespace Epic
{
	public partial class Functions
	{
		public static double EAJL (ref double X, ref double Y)
		{
			//EPICv0810
			//Translated by Heath Yates 
			//This program is called by ESLOS? to calculate the amount of 
			//material added to the top layer and removed from the second 
			//layer. 
			double EAJL = X * Y;
			X = X - EAJL;
            return EAJL; //Return to ESLOS? 
		}
	}
}

