using System;

namespace Epic
{
	public class EYCC
	{
		public EYCC ()
		{
//     EPIC0810
//     Translated by Heath Yates 
//     THIS SUBPROGRAM ESTIMATES THE USLE C FACTOR BASED ON PLANT POP &
//     BIOMASS & RESIDUE COVER
//     USE PARM
	  double X1=Math.Min(10.0,PRMT[60]*CV);
	  CVF=(0.8*EXP(-X1)+CFMN)*(0.9*(1.0-CVP)+0.1);
      CVF=CVF*EXP(-0.05*ROK(LD1));
      return;

		}
	}
}

