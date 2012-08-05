using System;

namespace Epic
{
	public class EWEMHKS
	{

        private static MODPARAM PARM = MODPARAM.Instance;

		public EWEMHKS (double JRT)
		{
        
               
   //     EPIC0810
   //     Translated by Heath Yates

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

   //     THIS SUBPROGRAM ESTIMATES DAILY SOIL LOSS CAUSED BY WIND EROSION,
   //     GIVEN THE AVERAGE WIND SPEED AND DIRECTION.
   //      USE PARM
          JRT=0;
          double RF;
          double X1;
          double RK;

          if(PARM.U10>PARM.PRMT[67])
		  {
				goto five;
		  }
        six:
		  JRT=1;
          return; 
        five:
		  double WD=193.0*Math.Exp(1.103*(PARM.U10-30.0)/(PARM.U10+1.0));
          double BT=PARM.PI2/4.9+PARM.TH-PARM.ANG;
          double ALG=PARM.FL*PARM.FW/(PARM.FL*Math.Abs(Math.Cos(BT))+PARM.FW*Math.Abs(Math.Sin(BT)));
          if(PARM.RGIN>0.0)              
          {
              X1=1.0+PARM.RHTT;
              RK=.004*X1*X1/PARM.RGIN;
          
              if(RK<2.27)
              {
                  RF=1.0;
              }
              else
              {
                  if(RK<89.0)
                  {
                      RF=1.125-.153*Math.Log(RK);
                  }
                  else
                  {
                      RF=0.336*Math.Pow(Math.E,(0.00324*RK));
                  }
              }
          }
          else
          {
            RF=1.0;
          } 
          PARM.VAC=1000.0*(PARM.VAC+PARM.BWD[3,PARM.JD]*PARM.RSD[PARM.LD1]);
          if(PARM.VAC>4000.0)
		  {
				goto six;
		  }
          PARM.VF=0.2533*Math.Pow (PARM.VAC,1.363);
          double BV=1.0+PARM.VF*(8.9303E-5+PARM.VF*(8.5074E-9-PARM.VF*1.5888E-13));
          double AV=Math.Pow(Math.E,(PARM.VF*(-7.5935E-4-PARM.VF*(4.7416E-8-PARM.VF*2.9476E-13))));
          double E2=695.0*PARM.WK*RF;
          double XL=1.5579E6*Math.Pow (E2,-1.2571)*Math.Exp(-.001558*E2);
          double AA=ALG*1000.0/XL;
          double F=(AA/(AA+Math.Exp(-3.2388-1.6241*AA)));
          double XX=Math.Pow(F,0.3484) + PARM.WCF - 1.0;
          if(XX <= 0.0) 
		  {
				goto six;
		  }
          double E4=(XX*Math.Pow (Math.Pow (E2,0.3484),2.8702));
          double E5 = AV * Math.Pow(E4, BV);
          double YW=E5*WD/PARM.WB;
          return;
       
        }
	}
}

