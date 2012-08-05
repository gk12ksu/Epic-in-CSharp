using System;

namespace Epic
{
	public class TMXL1
	{
		public TMXL1 (double DMX, double T, double W, double X, double Y)
		{
          //SUBROUTINE TMXL1(DMX,T,W,X,Y)
          //Epicv0810
          //Translated by Heath Yates
          //THIS FORTRAN SUBPROGRAM TRANSLATED INTO C# PREVENTS TILLAGE FROM INCREASING TOP LAYER CONTENT
          //BY MIXING
          T=(T*DMX-X+Y)/(DMX-0.01);
          W=X;
          return;
		}
	}
}

