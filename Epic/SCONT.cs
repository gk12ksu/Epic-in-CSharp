using System;

namespace Epic
{
	public class SCONT
	{
        private static MODPARAM PARM = MODPARAM.Instance;

		public SCONT (double KWX)
		{
		
      //SUBROUTINE SCONT(KWX)
      //Translated by Heath Yates 
      //THIS PROGRAM ADDS SOIL LAYER CONTENTS OF WATER & NUTRIENTS
      //USE PARM
       
      //Modified by Paul Cain on 7/30/2012 to fix build errors.

      double[] ATX = new double[4];
      double[] SWZ = new double[4];
      //Looks like fortran is (re?) initializing all the PARM to zero 
      PARM.SW=0.00;
      PARM.APB=0.0;
      PARM.OCPD=0.0;
      PARM.RZSW=0.0;
      PARM.PAW=0.0;
      PARM.SWRZ=0.0;
      PARM.TEK=0.0;
      PARM.TFK=0.0;
      PARM.TFOP=0.0;
      PARM.TNO2=0.0;
      PARM.TNO3=0.0;
      PARM.TNH3=0.0;
      PARM.TAP=0.0;
      PARM.TP=0.0;
      PARM.TMP=0.0;
      PARM.TRSD=0.0;
      PARM.TSK=0.0;
      PARM.TWN=0.0;
      PARM.TOC=0.0;
      PARM.ZLS=0.0;
      PARM.ZLM=0.0;
      PARM.ZLSL=0.0;
      PARM.ZLSC=0.0;
      PARM.ZLMC=0.0;
      PARM.ZLSLC=0.0;
      PARM.ZLSLNC=0.0;
      PARM.ZBMC=0.0;
      PARM.ZHSC=0.0;
      PARM.ZHPC=0.0;
      PARM.ZLSN=0.0;
      PARM.ZLMN=0.0;
      PARM.ZBMN=0.0;
      PARM.ZHSN=0.0;
      PARM.ZHPN=0.0;
      PARM.TOP=0.0;
      PARM.TNOR=0.0;
      PARM.TSLT=0.0;
      PARM.TSRZ=0.0;
      double XX=0.0;
      PARM.PDSW=0.0;
      PARM.FCSW=0.0;
      if(PARM.BIG>0.0){
          new SAJBD();
      }
      double SUM=0.0;
      double S1=0.0;
      double S2=0.0;
      double NTX=0.0;
      double BL=PARM.ZCS[0];
      int K1;
      int K2; 
      int LZ;
      int L1;
      double RTO;
      int L;
      for(int J=1; J<PARM.NBSL; J++){ 
         L=PARM.LID[J];
         PARM.TNO2=PARM.TNO2+PARM.WNO2[L];
         PARM.TNO3=PARM.TNO3+PARM.WNO3[L];
         PARM.TNH3=PARM.TNH3+PARM.WNH3[L];
         PARM.TEK=PARM.TEK+PARM.EXCK[L];
         PARM.TFK=PARM.TFK+PARM.FIXK[L];
         PARM.TSK=PARM.TSK+PARM.SOLK[L];
         PARM.TSLT=PARM.TSLT+PARM.WSLT[L];
         PARM.WOC[L]=PARM.WBMC[L]+PARM.WHPC[L]+PARM.WHSC[L]+PARM.WLMC[L]+PARM.WLSC[L];
         PARM.ECND[L]=0.15625*PARM.WSLT[L]/PARM.ST[L];
          if(PARM.Z[L]<=PARM.PMX){
              PARM.PDSW=PARM.PDSW+PARM.ST[L]-PARM.S15[L];
              PARM.FCSW=PARM.FCSW+PARM.FC[L]-PARM.S15[L];
              PARM.APB=PARM.APB+PARM.AP[L];
              PARM.OCPD=PARM.OCPD+PARM.WOC[L];
              SUM=SUM+PARM.WT[L];
              K1=J;
              K2=L;
          }
          if(PARM.Z[L]<=PARM.RZ){
              PARM.SWRZ=PARM.SWRZ+PARM.ST[L];
              PARM.TNOR=PARM.TNOR+PARM.WNO3[L];
              PARM.TSRZ=PARM.TSRZ+PARM.WSLT[L];
              LZ=L;
              L1=J;
              PARM.RZSW=PARM.RZSW+PARM.ST[L]-PARM.S15[L];
              PARM.PAW=PARM.PAW+PARM.FC[L]-PARM.S15[L];
          }
          PARM.TAP=PARM.TAP+PARM.AP[L];
          PARM.TOP=PARM.TOP+PARM.OP[L];
          PARM.TMP=PARM.TMP+PARM.PMN[L];
          PARM.TP=PARM.TP+PARM.WP[L];
          PARM.TRSD=PARM.TRSD+PARM.RSD[L];
          PARM.SW=PARM.SW+PARM.ST[L];
          PARM.TFOP=PARM.TFOP+PARM.FOP[L];
          PARM.ZLS=PARM.ZLS+PARM.WLS[L];
          PARM.ZLM=PARM.ZLM+PARM.WLM[L];
          PARM.ZLSL=PARM.ZLSL+PARM.WLSL[L];
          PARM.ZLSC=PARM.ZLSC+PARM.WLSC[L];
          PARM.ZLMC=PARM.ZLMC+PARM.WLMC[L];
          PARM.ZLSLC=PARM.ZLSLC+PARM.WLSLC[L];
          PARM.ZLSLNC=PARM.ZLSLNC+PARM.WLSLNC[L];
          PARM.ZBMC=PARM.ZBMC+PARM.WBMC[L];
          PARM.ZHSC=PARM.ZHSC+PARM.WHSC[L];
          PARM.ZHPC=PARM.ZHPC+PARM.WHPC[L];
          PARM.ZLSN=PARM.ZLSN+PARM.WLSN[L];
          PARM.ZLMN=PARM.ZLMN+PARM.WLMN[L];
          PARM.ZBMN=PARM.ZBMN+PARM.WBMN[L];
          PARM.ZHSN=PARM.ZHSN+PARM.WHSN[L];
          PARM.ZHPN=PARM.ZHPN+PARM.WHPN[L];
          PARM.WON[L]=PARM.WBMN[L]+PARM.WHPN[L]+PARM.WHSN[L]+PARM.WLMN[L]+PARM.WLSN[L];
          if(PARM.KFL[18]==0.0 || KWX>0){
              continue;
          }
          //DETERMINE SOIL TEMP AT .1, .2, .5, 1. m
          RTO=0.001*(PARM.ST[L]-PARM.S15[L])/(PARM.Z[L]-XX);
          if(XX<0.1 && (L)>=0.1){
              ATX[0]=PARM.STMP[L];
              SWZ[0]=RTO;
          }
          if(XX<0.2 && (L)>=0.2){
              ATX[1]=PARM.STMP[L];
              SWZ[1]=RTO;
          }
          if(XX<0.5 && (L)>=0.5){
              ATX[2]=PARM.STMP[L];
              SWZ[2]=RTO;
          }
          if(XX<1.0 && (L)>=1.0){
              ATX[3]=PARM.STMP[L];
              SWZ[3]=RTO;
          } 
          RTO=1000.0*RTO;
          // COMPUTE CONTROL SECTION WATER CONTENT
          if(PARM.Z[L]>PARM.ZCS[0] && BL<PARM.ZCS[2]){
              if(PARM.Z[L]<=PARM.ZCS[1]){
                  S1=S1+RTO*(PARM.Z[L]-BL);
              }
              else{
                  if(BL<PARM.ZCS[1]){      
                      S1=S1+RTO*(PARM.ZCS[1]-BL);
                      BL=PARM.ZCS[1];
                      S2=S2+RTO*(PARM.Z[L]-BL);
                  } 
                  else{
                      if(PARM.Z[L]<=PARM.ZCS[2]){
                          S2=S2+RTO*(PARM.Z[L]-BL);
                      } 
                      else{
                          S2=S2+RTO*(PARM.ZCS[2]-BL);
                      }                                                    
                  }
              }
              BL=PARM.Z[L];
          }        
          XX=PARM.Z[L];
      } 
      if(PARM.KFL[17]>0.0 && KWX==0){
            Console.WriteLine(PARM.IY, PARM.IYR, PARM.MO, PARM.KDA, S1, PARM.PS2, SWZ, ATX);                        
            // For potential debug purposes. Original statement in f90 was WRITE(KW[18],554)IY,IYR,MO,KDA,S1,S2,SWZ,ATX; 
      }
      PARM.TWN=PARM.ZLSN+PARM.ZLMN+PARM.ZBMN+PARM.ZHSN+PARM.ZHPN;
      PARM.TOC=PARM.ZLSC+PARM.ZLMC+PARM.ZBMC+PARM.ZHSC+PARM.ZHPC;
	  PARM.VAR[84]=PARM.TWN0-PARM.TWN-PARM.YN;
	  PARM.SMM[84,PARM.MO]=PARM.SMM[84,PARM.MO]+PARM.TWN0-PARM.TWN-PARM.YN;
	  PARM.TWN0=PARM.TWN;
      if(LZ != L){
          double ZZ=PARM.RZ-PARM.Z[LZ];
          L1=PARM.LID[L1+1];
          RTO=ZZ/(PARM.Z[L1]-PARM.Z[LZ]);
          PARM.RZSW=PARM.RZSW+(PARM.ST[L1]-PARM.S15[L1])*RTO;
          PARM.PAW=PARM.PAW+RTO*(PARM.FC[L1]-PARM.S15[L1]);
          PARM.SWRZ=PARM.SWRZ+PARM.ST[L1]*RTO;
          PARM.TNOR=PARM.TNOR+PARM.WNO3[L1]*RTO;
          PARM.TSRZ=PARM.TSRZ+PARM.WSLT[L1]*RTO;
      }
      PARM.SSW=PARM.SSW+PARM.RZSW;
      if(K1 != PARM.NBSL){
          int KK=PARM.LID[K1+1];
          RTO = (PARM.PMX - PARM.Z[K2]) / (PARM.Z[KK] - PARM.Z[K2]);
          PARM.PDSW = PARM.PDSW + RTO * (PARM.ST[KK] - PARM.S15[KK]);
          PARM.FCSW = PARM.FCSW + RTO * (PARM.FC[KK] - PARM.S15[KK]);
          PARM.APB = PARM.APB + RTO * PARM.AP[KK];
          PARM.OCPD = PARM.OCPD + RTO * PARM.WOC[KK];
          SUM = SUM + RTO * PARM.WT[KK];
      }
      PARM.APBC = 1000.0 * PARM.APB / SUM;
      PARM.OCPD = 0.001 * PARM.OCPD;
      return;
      //554 FORMAT(I4,1X,3I4,2F10.1,4F10.4,4F10.2); This is related to the Console.WriteLine() 
      //END
			
		}
	}
}

