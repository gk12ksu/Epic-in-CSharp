using System;

namespace Epic
{
	public partial class Functions
	{
        private static MODPARAM PARM = MODPARAM.Instance;

		public static void SWRTNB (double CM, double CL, double OC, double SA, ref double WC15, ref double WC3RD, double ZZ)
		{
    //     SUBROUTINE SWRTNB(CM,CL,OC,SA,WC15,WC3RD,ZZ)
    //     EPIC0810
    //     Translated by Heath Yates  

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

            /* ADDITIONAL CHANGE
             * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
             *              and changed whether some paramters are pass-by-reference so that only 
             *              the parameters that are actually modified are declared as pass-by-
             *              reference.
             */

    //     THIS FORTRAN SUBPROGRAM TRANSLATED INTO C# USES OTTO BAUMER'S METHOD FOR ESTIMATING SOIL
    //     WATER CONTENT AT 33 AND 1500 kpa.
          double SI=100.0-CL-SA;
          double OM=1.72*OC;
          double BV; 
          double BW;
          double BO;
          double APD; 
          double CE;
          double X1;
          double X2;
          double VOMO;
          double VODF; 
          double A1; 
          double BDX;
          double SDF;
          double VFS;
          double CF3;
          double SAF;
          double WC15G;
          double SICL; 
          double CF1;
          double W3RDG;
          double OAIR;
          double WSV;
          double DBD;
          double DBS;
          double WSG;
		  double CA;
          double CAAF;
          if(ZZ<.25){
              BV=1.085;
              BW=1.035;
              BO=1.9;
          }
          else{
              if(ZZ<1.0){
                  BV=1.0;
                  BW=1.0;
                  BO=1.0;
              }
              else{
                  BV=0.915;
                  BW=0.96;
                  BO=0.1;
              }    
          }          
          APD=100.0/(37.74+0.3366*OM);
          CE=CM+2.428*OC+1.7*ZZ;
          CA=Math.Min(0.8,CE/CL);
          X1=CL*CL;
          X2=CA*CA;
          if(CL<10.0){
              CAAF=0.099*X2*X1;
          }
          else{
              CAAF=9.9*X2;
          }
          VOMO=BV*(42.84+1.37*OM+0.00294*X1+CAAF+0.0036*X1*X2-0.0852*SA-0.316*CL*CA);
          VODF = Math.Max(0.01, BV * (0.277 + 0.16 * OM - 2.72 * CA * X2 + 0.268 * CL * CA + 0.00546 * X1 * CA - 0.00184 * X1 * X2));
          WC15G=0.71+0.45*OM+0.336*CL+0.117*CL*Math.Pow(CA,1.5); //Mono says debug gives Unexpected symbol warning for WC15G
          VFS=0.1*SA;
          SICL=CL+0.3333*(SI+VFS);
          if(SICL<15.0){
              CF1=1.0;
              goto five;
          }
          if(SICL<30.0){
              CF1=2.0-0.0667*SICL;
          }
          else{
              CF1=0.0;
          }    
          five:
          A1=14.94+3.8*X2-0.137*SA;
          BDX=APD*(1.0-0.01*VOMO);
          SDF=SA-VFS;
          CF3=37.74*SDF/((100.0-SDF)/BDX+0.3774*SDF);
          SAF=1.0-0.005*CF3*(CF1+1.0);
          W3RDG=BW*(A1*SAF+WC15G+0.746*OM);
          OAIR=BO*(3.8+0.00018*X1-0.03365*SA+0.126*CL*CA+0.000025*OM*SA*SA);
          WSV=VOMO*(1.0-0.01*OAIR);
          WSG=WSV/BDX;
          DBD=APD*(1.0-0.01*(VOMO-VODF));
          DBS=(BDX-DBD)/WSG;
          WC3RD=0.01*W3RDG*(DBD+DBS*W3RDG);
          WC15=0.01*WC15G*(DBD+DBS*WC15G);
          return;
		}
	}
}
