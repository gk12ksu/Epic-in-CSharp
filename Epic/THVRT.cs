using System;

namespace Epic
{
	public class THVRT
	{
		public THVRT (double YY, double X3, double X1, double X6, double X7, double N1)
		{
          //SUBROUTINE THVRT(YY,X3,X1,X6,X7,N1)
          //EPIC0810
          //Translated by Heath Yates 
          //THIS FORTRAN SUBPROGRAM TRANSLATED INTO C# HARVESTS ROOT CROPS.
          //USE PARM
          Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
          
          PARM.JD=PARM.JJK;
          X1=PARM.ORHI[PARM.JT1];
          double XX=PARM.DM[PARM.JJK];
          X3=PARM.RW[PARM.JJK];
          double XZ=X1*X3;
          Epic.PESTF;
          X6=PARM.PSTF[PARM.JJK];
          PARM.TPSF[N1,PARM.JJK]=PARM.TPSF[N1,PARM.JJK]+X6;
          PARM.NPSF[N1,PARM.JJK]=PARM.NPSF[N1,PARM.JJK]+1;
          YY=XZ*PARM.HE[PARM.JT1]*X6;
          double X12=PARM.UN1[PARM.JJK]/XX;
          double X13=PARM.UP1[PARM.JJK]/XX;
          double X8=PARM.UK1[PARM.JJK]/XX;
          double X9=0.9*STL[PARM.JJK];
          double X10=X12*X9;
          double AD1=X10;
          Epic.NCNSTD[X9,X10,0];
          double U2=X13*X9 ;
          double AD2=U2;
          PARM.FOP[LD1]=PARM.FOP[LD1]+U2;
          PARM.SOLK[LD1]=PARM.SOLK[LD1]+PARM.UK1[PARM.JJK];
          PARM.YLN=YY*X12;
          PARM.YLP=YY*X13;
          PARM.YLC=0.42*YY;
          PARM.YLK=YY*X8;
          XX=X1*(1.0-PARM.HE[PARM.JT1]);
          PARM.RW[PARM.JJK]=0.0;
          for(int J=1;J < LRD; J++){
              PARM.ISL=PARM.LID[J];
              X11=PARM.RWT[ISL,PARM.JJK]*XX;
              PARM.RWT[ISL,PARM.JJK]=PARM.RWT[ISL,PARM.JJK]*(1.0-X1);
              X10=X11*X12;
              AD1=AD1+X10;
              Epic.NCNSTD[X11,X10,1];
              U2=X11*X13;
              AD2=AD2+U2 ;
              PARM.FOP[ISL]=PARM.FOP[ISL]+U2;
              PARM.RW[PARM.JJK]=PARM.RW[PARM.JJK]+PARM.RWT[ISL,PARM.JJK];
          }
          PARM.YLD[PARM.JJK]=YY;
          PARM.YLD1[N1,PARM.JJK]=PARM.YLD1[N1,PARM.JJK]+YY;
          PARM.YLNF[N1,PARM.JJK]=PARM.YLNF[N1,PARM.JJK]+PARM.YLN;
          PARM.YLPF[N1,PARM.JJK]=PARM.YLPF[N1,PARM.JJK]+PARM.YLP;
          PARM.YLCF[N1,PARM.JJK]=PARM.YLCF[N1,PARM.JJK]+PARM.YLC;
          PARM.YLKF[N1,PARM.JJK]=PARM.YLKF[N1,PARM.JJK]+PARM.YLK;
          X7=0.001*X12;
          PARM.STL[PARM.JJK]=STL[PARM.JJK]-X9;
          PARM.DM[PARM.JJK]=PARM.RW[PARM.JJK]+STL[PARM.JJK];
          PARM.UN1[PARM.JJK]=UN1[PARM.JJK]-PARM.YLN-AD1;
          PARM.UP1[PARM.JJK]=UP1[PARM.JJK]-PARM.YLP-AD2;
          PARM.UK1[PARM.JJK]=UK1[PARM.JJK]-PARM.YLK;
          PARM.HU[PARM.JJK]=HU[PARM.JJK]*0.1;
          PARM.SLAI[PARM.JJK]=0.05;
          return;
		}
	}
}

