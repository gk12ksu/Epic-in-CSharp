using System;

namespace Epic
{
	public class TRIDIAG
	{
		public TRIDIAG (double[] B, double[] D, double[] A, double[] C, int N)
		{
          //SUBROUTINE TRIDIAG(B,D,A,C,N)
          //Translated by Heath Yates 

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

          //FORTRAN SUBPROGRAM TRANSLATED INTO C# TRIDIAG USES THE THOMAS ALHORITHM
          //D()...SOLUTION RETURNED AS C()
          //B()...BELOW DIAGONAL ELEMENTS
          //D()...DIAGONAL ELEMENTS
          //A()...ABOVE DIAGONAL ELEMENTS
          //C()...RIGHT HAND SIDE
          A = new double[N];
          B = new double[N];
          C = new double[N];
          D = new double[N];
          double R; 
          int J;
          //FORWARD ELIMINATION
          for(int I=2; I < N; I++){
              R=B[I]/D[I-1];
              D[I]=D[I]-R*A[I-1];
              C[I]=C[I]-R*C[I-1];
          }
          //BACK SUBSTITUTION
          C[N]=C[N]/D[N];
          for(int I=2; I < N; I++){
              J=N-I+1;
              C[J]=(C[J]-A[J]*C[J+1])/D[J];
          }           
          return;
		}
	}
}
