using System;

namespace Epic{
	public class EWER
	{
		public EWER (double JRT)
		{
         
    //     EPIC0810
    //     Translated by Heath Yates  
    //     THIS SUBPROGRAM ESTIMATES DAILY SOIL LOSS CAUSED BY WIND EROSION,
    //     GIVEN THE AVERAGE WIND SPEED AND DIRECTION.
    //      USE PARM
          JRT=0;
          if(U10<PRMT[67])
		  {
              JRT=1;
              return;
		  }
              BT= PI2/4.0+TH-ANG;
          ALG=FL*FW/(FL*Math.Abs(BT)+FW*Math.Abs(Math.Sin(BT)));
          RRF=11.9*(1.0-Math.Exp(-Math.Power((RRUF/9.8),1.3)));
          double RIF=Math.Abs(Math.Sin(BT))*(1.27*Math.Power(RHTT,0.52));
          RFB=Math.Max(1,RRF+RIF);
          double RFC=0.77*Math.Power(1.002,RHTT);
          RGRF=1.0;
          double X1=Math.Power((10.0/RFB),RFC);
          if(X1<10.0) 
	      {
				RGRF=1.0-Math.Exp(-X1);
		  }
          X1=VAC;
          X1=Math.Min(10.0,X1+BWD[3,JD]*RSD[LD1]);
          VF=1.0-X1/(X1+Math.Exp(SCRP[13,1]-SCRP[13,2]*X1));
          ALG=1.0-Math.Exp(-ALG/0.07);
          double RTO=ST[LD1]/S15[LD1];
          SMM[68,MO]=SMM[68,MO]+ST[LD1];
          VAR[68]=ST[LD1];
          NWDA=NWDA+1;
          USTW=USTT+0.5*RTO*RTO;
          double ROKF=Math.Exp(-0.047*ROK[LD1]);
          Epic.EWNINT;
          double WK1=WK*Math.Exp(-0.001*TLMF);
          SMM[38,MO]=SMM[38,MO]+WK1;
          VAR[38]=WK1;
          YW=8640.0*YW*RGRF*VF*ROKF*WK1;
          TLMF=TLMF+YW;
          return;

		}
	}
}

