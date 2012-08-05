using System;

namespace Epic 
{
	public class TRDST
	{
		public TRDST ()
		{
          //SUBROUTINE TRDST
          //EPIC0810
          //THIS SUBPROGRAM CONVERTS LIVE BIOMASS TO RESIDUE WHEN A CROP IS 
          //KILLED.
          //USE PARM
            
            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

          Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
          PARM.STD[PARM.JJK] = PARM.STD[PARM.JJK] + PARM.STL[PARM.JJK];
          double X1=PARM.DM[PARM.JJK]+Math.Pow(1,-10); //1.E-10
          double XX=PARM.UN1[PARM.JJK]/X1;
          double X3=PARM.UP1[PARM.JJK]/X1;
          double W1=PARM.UK1[PARM.JJK]/X1;
          PARM.STDN[PARM.JJK]=PARM.STDN[PARM.JJK]+XX*PARM.STL[PARM.JJK];
          PARM.STDP=PARM.STDP+X3*PARM.STL[PARM.JJK];
          PARM.STDK=PARM.STDK+W1*PARM.STL[PARM.JJK];
          PARM.STDL=PARM.STDL+PARM.CLG*PARM.STL[PARM.JJK];
          for(int J=1; J < PARM.LRD; J++){
              PARM.ISL=PARM.LID[J];
              X1=PARM.RWT[PARM.ISL,PARM.JJK];
              double X2=X1*XX;
              new NCNSTD(ref X1,ref X2,1);
              PARM.FOP[PARM.ISL]=PARM.FOP[PARM.ISL]+X1*X3;
              PARM.SOLK[PARM.ISL]=PARM.SOLK[PARM.ISL]+X1*W1;
              PARM.RWT[PARM.ISL,PARM.JJK]=0.0;
          }
          PARM.YLD[PARM.JJK]=0.0;
          PARM.DM[PARM.JJK]=0.0;
          PARM.STL[PARM.JJK]=0.0;
          PARM.UN1[PARM.JJK]=0.0;
          PARM.UP1[PARM.JJK]=0.0;
          PARM.UK1[PARM.JJK]=0.0;
          PARM.RW[PARM.JJK]=0.0;
          PARM.RD[PARM.JJK]=0.0;
          return;
       	}
	}
}

