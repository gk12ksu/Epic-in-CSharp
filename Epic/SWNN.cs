using System;

namespace Epic 
{
	public partial class Functions
	{
		public static void SWNN (double CL, double SA, double OC, ref double W1, ref double F3)
		{
        //    SUBROUTINE SWNN(CL,SA,OC,W1,F3)
        //    EPIC0810
        //    Translated by Heath Yates  
        //    THIS FORTRAN SUBPROGRAM TRANSLATED IN C# ESTIMATES FC AND WP USING NEAREST NEIGHBOR 
        //    APPROACH.
        //    USE PARM

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

            /* ADDITIONAL CHANGE
             * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
             *              and changed whether some paramters are pass-by-reference so that only 
             *              the parameters that are actually modified are declared as pass-by-
             *              reference.
             */

        	  Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
              double[] DXS = new double[PARM.NSX];
              double[] XTP = new double[3];              
              double X1;
              double X3;
              XTP[1]=(SA-PARM.XAV[1])/PARM.XDV[1];
              XTP[2]=(CL-PARM.XAV[2])/PARM.XDV[2];
              XTP[3]=(OC-PARM.XAV[3])/PARM.XDV[3];
              XTP[1]=XTP[1]*PARM.BRNG/PARM.XRG[1];
              XTP[2]=XTP[2]*PARM.BRNG/PARM.XRG[2];
              XTP[3]=XTP[3]*PARM.BRNG/PARM.XRG[3];
              int I1=1;
              for(int I=1; I < PARM.NSX; I++){
                  DXS[I]=0.0;
                  for( int K=1; K < 3; K++){
                      DXS[I]=DXS[I]+Math.Pow((PARM.XSP[I,K]-XTP[K]),2);
                  }
                  DXS[I]=Math.Sqrt(DXS[I]);
                  PARM.NX[I1]=I;
                  I1=I1+1;
              }
              I1=I1-1;
              new ASORT3(ref DXS, ref PARM.NX, ref PARM.NSX);
              int N1 = Math.Min(I1, PARM.NSNN);
              double SUM=0.0;
              for(int I=1; I < N1; I++){
                  X1 = 0.01 * PARM.XSP[PARM.NX[I], 4];
                  X3 = X1 + PARM.XSP[PARM.NX[I], 5];
                  for(int K=1; K < 3; K++){
                      XTP[K] = PARM.XDV[K] * PARM.XSP[PARM.NX[I], K] * PARM.XRG[K] / PARM.BRNG + PARM.XAV[K];
                  }
                  SUM = SUM + DXS[I];
              }
              double TOT=0.0;
              for(int I=1; I < N1; N1++){
                  DXS[I] = Math.Pow((SUM / DXS[I]), PARM.EXNN);
                  TOT=TOT+DXS[I];
              }
              W1=0.0;
              F3=0.0;
              for(int I=1; I < N1; N1++){
                  DXS[I]=DXS[I]/TOT;
                  X1 = 0.01 * PARM.XSP[PARM.NX[I], 4];
                  X3 = X1 + PARM.XSP[PARM.NX[I], 5];
                  W1=W1+DXS[I]*X1;
                  F3=F3+DXS[I]*X3;
              }
              return; 
            //1 FORMAT(10F10.4)
            //2 FORMAT(2I10,11F10.5)
		}
	}
}

