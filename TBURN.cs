using System;

namespace Epic
{
	public class TBURN
	{
		public TBURN ()
		{
          //SUBROUTINE TBURN
          //EPIC0810
          //Translated by Heath Yates  
          //THIS SUBPROGRAM BURNS ALL STANDING AND FLAT CROP RESIDUE.
          //USE PARM
          Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
          double X2; 
          double SUM;
          double RTO; 
          double X1;
          double X3;
          PARM DM;
          PARM STL;
          PARM UNL;
          PARM WLS;
          PARM WLSN;
          PARM RSD;
          PARM SDD;
          PARM WLSLNC;
          PARM LD1;
          PARM WLMN;
          PARM WLSC;
          PARM STDON;
          PARM STDO;
          PARM FOP;
          PARM STDOP;
          PARM VAR;
	      X2=1.0-PRMT[49];
          SUM=0.0;
          for(int J=1;J < LC; J++){
              RTO=MIN(0.99,STL[J]/(DM[J]+Math.Pow(1,-10)));//1.E-10)
              X1=PRMT[49]*STL[J];
              DM[J]=DM[J]-X1;
              STL[J]=STL[J]-X1;
              X3=PRMT[49]*UN1[J]*RTO;
              UN1[J]=UN1[J]-X3;
              STD[J]=STD[J]*X2;
              X1=PRMT[49]*STDN[J];
              STDN[J]=STDN[J]-X1;
              SUM=SUM+X1+X3;
          }
          WLS[LD1]=WLS[LD1]*X2;
          WLM[LD1]=WLM[LD1]*X2;
          X1=PRMT[49]*WLSN[LD1];
          WLSN[LD1]=WLSN[LD1]-X1;
          X3=PRMT[49]*WLMN[LD1];
          WLMN[LD1]=WLMN[LD1]-X3;
          SUM=SUM+X1+X3;
          WLSL[LD1]=WLSL[LD1]*X2;
          X1=PRMT[49]*WLSC[LD1];
          WLSC[LD1]=WLSC[LD1]-X1;
          X3=PRMT[49]*WLMC[LD1];
          WLMC[LD1]=WLMC[LD1]-X3;
          WLSLC[LD1]=WLSLC[LD1]*X2;
          WLSLNC[LD1]=WLSC[LD1]-WLSLC[LD1];
          SMM[97,MO]=SMM[97,MO]+X1+X3;
          SMM[98,MO]=SMM[98,MO]+SUM;
          VAR[98]=SUM;
          RSD[LD1]=0.001*(WLS[LD1]+WLM[LD1]);
          STDO=0.0;
          STDON=0.0;
          X1=STDP*PRMT[49];
          FOP[LD1]=FOP[LD1]+X1+STDOP;
          STDP=STDP-X1;
          STDOP=0.0;
          return;
		}
	}
}

