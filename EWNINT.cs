using System;

namespace Epic
{
	public class EWNINT
	{
		public EWNINT ()
		{

    //     EPIC0810
    //     Translated by Heath Yates
    //     THIS SUBPROGRAM ESTIMATES DAILY POTENTIAL WIND EROSION FOR A BARE
    //     SOIL BY INTEGRATING EROSION RATES WITH TIME GIVEN THE WIND SPEED
    //     DISTRIBUTION
    //     USE PARM
          double WW=MIN(0.5,ATRI[0.1,0.35,0.6,9]*UAVM[MO]/U10);
          double SUM=0.6424*Math.Pow(WW,(-0.1508))*Math.Exp(0.4336*WW);
          double DU10=USTRT/0.0408;
          double Y1=DU10/U10;
          double XX=LOG10[Y1]/WW;
          double XY; 
          double X2; 
          double Y2;
          double Z2;
          double XZ;
          double X1;
          

          if(XX>1.3){
              YW=0.0;
              return;
          } 
          if(XX<-3.0) goto one;
          XX=Math.Pow(10,XX);
          goto six;
		one: X1=1.0;
          goto four;
		six: X1=Math.Exp(-XX);
		four: double DX= 0.1;
          YW=0.0;
          double Z1=0.0;
          while(DX>Math.Pow(1.0,-4)){
              XY=0.0;
              while(XY<0.1){
                  X2=X1-DX;
                  if(X2<=0.0) break;
                  Y2=Math.Pow((-Math.Log(X2)),WW)/SUM;
                  Z2=EROWN(Y2);
                  XY=(Y1+Y2)*DX;
                  XZ=(Z2+Z1)*DX;
                  YW=YW+XZ;
                  X1=X2;
                  Y1=Y2;
                  Z1=Z2;
              } 
              DX=DX*0.5;
          } 
          YW=0.5*YW;
          return;

		}
	}
}

