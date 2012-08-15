using System;

namespace Epic
{
	public class TMIX
	{
        private static MODPARAM PARM = MODPARAM.Instance;
        
        public TMIX (double EE, double DMX, double NMIX)
		{
            //SUBROUTINE TMIX(EE,DMX,NMIX)
            // EPIC0810
            // THIS SUBPROGRAM MIXES N,P, AND CROP RESIDUE WITHIN THE PLOW DEPTH
            //ACCORDING TO THE MIXING EFFICIENCY OF THE IMPLEMENT, CALCULATES
            //THE CHANGE IN BULK DENSITY, CONVERTS STANDING RESIDUE TO FLAT
            //RESIDUE, AND ESTIMATES THE IMPLEMENT'S EFFECT ON RIDGE HEIGHT AND
            //INTERVAL.
            //USE PARM

             /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

      double [] TST = new double [58];
      double [] DUM = new double [15];
      double [] XTP = new double [8];
            double [] YTP = new double [8];
      int ISM = 58,J=0,I1=0,J1=0;
      double ZON,XX,X1,X2,ZZ,PMA;
      if(NMIX>0) goto lbl25;
      
      if(DMX<0.0){
      //     MOW, SHRED, ETC
          for(J=1; J < PARM.LC; J++)
          {
              if(PARM.IDC[PARM.JJK]==PARM.NDC[7] || PARM.IDC[PARM.JJK]==PARM.NDC[8] || PARM.IDC[PARM.JJK]==PARM.NDC[10])continue;
              if(PARM.CHT[J]+DMX<0.0)continue;
              XX=(PARM.CHT[J]+DMX)/PARM.CHT[J];
              ZZ=XX*PARM.STD[J];
              double ZO=XX*PARM.STDO;
              double ZL=XX*PARM.STL[J];
              PARM.STD[J]=PARM.STD[J]-ZZ;
              PARM.STDO=PARM.STDO-ZO;
              PARM.STL[J]=PARM.STL[J]-ZL;
              X1=1.0-XX;
              PARM.SLAI[J]=PARM.SLAI[J]*X1;
              PARM.HU[J]=PARM.HU[J]*X1;
              PARM.STDL=PARM.STDL*X1;
              double DX=Math.Min(0.99,ZL/(PARM.DM[J]+Math.Pow(1,-10)));
              X1=ZZ+ZL+ZO;
              ZZ=XX*PARM.STDN[J];
              ZON=XX*PARM.STDON;
              double ZN=DX*PARM.UN1[J];
              PARM.STDN[J]=PARM.STDN[J]-ZZ;
              PARM.STDON=PARM.STDON-ZON;
              X2=ZZ+ZN+ZON;
              new NCNSTD(ref X1,ref X2,0);
              ZZ=XX*PARM.STDP;
              PARM.STDP=PARM.STDP-ZZ;
              double ZOP=XX*PARM.STDOP;
              PARM.STDOP=PARM.STDOP-ZOP;
              double ZP=DX*PARM.UP1[J];
              PARM.FOP[PARM.LD1]=PARM.FOP[PARM.LD1]+ZZ+ZP+ZOP;
              ZZ=XX*PARM.STDK;
              PARM.STDK=PARM.STDK-ZZ;
              double ZOK=XX*PARM.STDOK;
              PARM.STDOK=PARM.STDOK-ZOK;
              double ZK=DX*PARM.UK1[J];
              PARM.SOLK[PARM.LD1]=PARM.SOLK[PARM.LD1]+ZZ+ZK+ZOK;
              PARM.DM[J]=PARM.DM[J]-ZL;
              PARM.UN1[J]=Math.Max(Math.Pow(1,-10),PARM.UN1[J]-ZN);
              PARM.UP1[J]=PARM.UP1[J]-ZP;
              PARM.UK1[J]=Math.Max(Math.Pow(1,-10),PARM.UK1[J]-ZK);
              PARM.CHT[J]=-DMX;
          }
          return;
      }
      if(PARM.Z[PARM.LD1]>=DMX)return;
      PARM.RFSM=0.0;
      PARM.RCF=1.0;
      if(PARM.RHT[PARM.JT1]<PARM.RHT[PARM.JT2]){
          PARM.RHTT=PARM.RHT[PARM.JT1]+(PARM.RHT[PARM.JT2]-PARM.RHT[PARM.JT1])*Math.Exp(-DMX/PARM.TLD[PARM.JT2]);
      }
      else{
          PARM.RHTT=PARM.RHT[PARM.JT1];
          PARM.RGIN=PARM.RIN[PARM.JT1];
      }
      double F=1.0-Math.Exp(-56.9*DMX*EE);
      double SUM=0.0;
      double TOT=0.0;
      for(int K=1; K < PARM.LC; K++)
      {
          X1=PARM.STD[K]*F;
          SUM=SUM+X1;
          PARM.STD[K]=Math.Max(Math.Pow(1,-10),PARM.STD[K]-X1);
          XX=F*PARM.STDN[K];
          TOT=TOT+XX;
          PARM.STDN[K]=Math.Max(Math.Pow(1,-10),PARM.STDN[K]-XX);
          XX=F*PARM.STDP;
          PARM.STDP=Math.Max(Math.Pow(1,-10),PARM.STDP-XX);
          PARM.FOP[PARM.LD1]=PARM.FOP[PARM.LD1]+XX;
          XX=F*PARM.STDK;
          PARM.STDK=Math.Max(Math.Pow(1,-10),PARM.STDK-XX);
          PARM.SOLK[PARM.LD1]=PARM.SOLK[PARM.LD1]+XX;
          XX=F*PARM.STDL;
          PARM.STDL=Math.Max(.1*PARM.STD[K],PARM.STDL-XX);
      }
      XX=PARM.STDO*F;
      PARM.STDO=Math.Max(Math.Pow(1,-10),PARM.STDO-XX);
      X1=SUM+XX;
      ZON=F*PARM.STDON;
      PARM.STDON=Math.Max(Math.Pow(1,-10),PARM.STDON-ZON);
      X2=TOT+ZON;
      new NCNSTD(ref X1,ref X2,0);
      XX=F*PARM.STDOP;
      PARM.STDOP=Math.Max(Math.Pow(1,-10),PARM.STDOP-XX);
      PARM.FOP[PARM.LD1]=PARM.FOP[PARM.LD1]+XX;
      XX=F*PARM.STDOK;
      PARM.STDOK=Math.Max(Math.Pow(1,-10),PARM.STDOK-XX);
      PARM.SOLK[PARM.LD1]=PARM.SOLK[PARM.LD1]+XX;
      PARM.RRUF=Math.Max(1.0,PARM.RR[PARM.JT1]);
      PARM.TLMF=0.0;
   lbl25: for(int I=1; I < ISM; I++){
          TST[I]=0.0;
      }
      XX=0.0;
      XTP[1]=PARM.WLS[PARM.LD1];
      XTP[2]=PARM.WLM[PARM.LD1];
      XTP[3]=PARM.WLSL[PARM.LD1];
      XTP[4]=PARM.WLSC[PARM.LD1];
      XTP[5]=PARM.WLMC[PARM.LD1];
      XTP[6]=PARM.WLSLC[PARM.LD1];
      XTP[7]=PARM.WLSN[PARM.LD1];
      XTP[8]=PARM.WLMN[PARM.LD1];
      for(J=1; J < PARM.NBSL; J++)
      {
          PARM.ISL=PARM.LID[J];
          PARM.UN[PARM.ISL]=PARM.ROK[PARM.ISL];
          ZZ=PARM.Z[PARM.ISL]-XX;
          if(PARM.Z[PARM.ISL]>=DMX) goto lbl7;
          if(NMIX<=0){
              PARM.BDP[PARM.ISL]=PARM.BDP[PARM.ISL]-(PARM.BDP[PARM.ISL]-0.6667*PARM.BD[PARM.ISL])*EE;
              PARM.CLA[PARM.ISL]=PARM.CLA[PARM.ISL]*ZZ;
              PARM.SIL[PARM.ISL]=PARM.SIL[PARM.ISL]*ZZ;
              PARM.ROK[PARM.ISL]=PARM.ROK[PARM.ISL]*ZZ;
          }
          PMA=PARM.PMN[PARM.ISL]+PARM.AP[PARM.ISL];
          DUM[PARM.ISL]=PARM.PSP[PARM.ISL]*PMA;
          PARM.UP[PARM.ISL]=PMA-DUM[PARM.ISL];
    //     EXTRACT THE FRACTION OF MATERIAL TO BE MIXED AND PLACE IN TST
    //     STORAGE
          TST[1]=Functions.EAJL(ref PARM.WNO3[PARM.ISL],ref EE)+TST[1];
    //     TST(2)=EAJL(WHPN(PARM.ISL),EE)+TST(2)
    //     TST(3)=EAJL(WHSN(PARM.ISL),EE)+TST(3)
          TST[4]=Functions.EAJL(ref PARM.WBMN[PARM.ISL],ref EE)+TST[4];
          TST[5]=Functions.EAJL(ref PARM.WLSN[PARM.ISL],ref EE)+TST[5];
          TST[6]=Functions.EAJL(ref PARM.WLMN[PARM.ISL],ref EE)+TST[6];
    //     TST(7)=EAJL(WHPC(PARM.ISL),EE)+TST(7)
    //     TST(8)=EAJL(WHSC(PARM.ISL),EE)+TST(8)
          TST[9]=Functions.EAJL(ref PARM.WBMC[PARM.ISL],ref EE)+TST[9];
          TST[14]=Functions.EAJL(ref PARM.WLS[PARM.ISL],ref EE)+TST[14];
          TST[15]=Functions.EAJL(ref PARM.WLM[PARM.ISL],ref EE)+TST[15];
          TST[16]=Functions.EAJL(ref PARM.WLSL[PARM.ISL],ref EE)+TST[16];
          TST[10]=Functions.EAJL(ref PARM.WLSC[PARM.ISL],ref EE)+TST[10];
          TST[11]=Functions.EAJL(ref PARM.WLMC[PARM.ISL],ref EE)+TST[11];
          TST[12]=Functions.EAJL(ref PARM.WLSLC[PARM.ISL],ref EE)+TST[12];
          if(J==1){
              YTP[1]=PARM.WLS[PARM.LD1];
              YTP[2]=PARM.WLM[PARM.LD1];
              YTP[3]=PARM.WLSL[PARM.LD1];
              YTP[4]=PARM.WLSC[PARM.LD1];
              YTP[5]=PARM.WLMC[PARM.LD1];
              YTP[6]=PARM.WLSLC[PARM.LD1];
              YTP[7]=PARM.WLSN[PARM.LD1];
              YTP[8]=PARM.WLMN[PARM.LD1];
          }
          TST[17]=Functions.EAJL(ref PARM.WP[PARM.ISL],ref EE)+TST[17];
          TST[19]=Functions.EAJL(ref PARM.AP[PARM.ISL],ref EE)+TST[19];
          TST[20]=Functions.EAJL(ref PARM.PMN[PARM.ISL],ref EE)+TST[20];
          TST[21]=Functions.EAJL(ref PARM.FOP[PARM.ISL],ref EE)+TST[21];
          TST[22]=Functions.EAJL(ref PARM.OP[PARM.ISL],ref EE)+TST[22];
          if(NMIX==0){
              TST[23]=Functions.EAJL(ref PARM.CLA[PARM.ISL],ref EE)+TST[23];
              TST[24]=Functions.EAJL(ref PARM.SIL[PARM.ISL],ref EE)+TST[24];
              TST[27]=Functions.EAJL(ref PARM.ROK[PARM.ISL],ref EE)+TST[27];
          }
          TST[25]=Functions.EAJL(ref DUM[PARM.ISL],ref EE)+TST[25];
          TST[26]=Functions.EAJL(ref PARM.UP[PARM.ISL],ref EE)+TST[26];
          TST[28]=Functions.EAJL(ref PARM.WNH3[PARM.ISL],ref EE)+TST[28];
          int I1=29;
          for(int I=1; I < PARM.NDP; I++){
              TST[I1]=Functions.EAJL(ref PARM.PSTZ[I,PARM.ISL],ref EE)+TST[I1];
              I1=I1+1;
          } 
          XX=PARM.Z[PARM.ISL];
      }          
      J=PARM.NBSL;
      DMX=PARM.Z[PARM.LID[PARM.NBSL]];
      goto lbl8;
    lbl7: double RTO=(DMX-XX)/ZZ;
      double RE=RTO*EE;
      if(NMIX==0){
          PARM.BDP[PARM.ISL]=PARM.BDP[PARM.ISL]-(PARM.BDP[PARM.ISL]-0.6667*PARM.BD[PARM.ISL])*RE;
          PARM.CLA[PARM.ISL]=PARM.CLA[PARM.ISL]*ZZ;
          PARM.SIL[PARM.ISL]=PARM.SIL[PARM.ISL]*ZZ;
          PARM.ROK[PARM.ISL]=PARM.ROK[PARM.ISL]*ZZ;
      }
      PMA=PARM.PMN[PARM.ISL]+PARM.AP[PARM.ISL];
      DUM[PARM.ISL]=PARM.PSP[PARM.ISL]*PMA;
      PARM.UP[PARM.ISL]=PMA-DUM[PARM.ISL];
      TST[1]=Functions.EAJL(ref PARM.WNO3[PARM.ISL],ref RE)+TST[1];
//     TST(2)=EAJL(WHPN(PARM.ISL),RE)+TST(2)
//     TST(3)=EAJL(WHSN(PARM.ISL),RE)+TST(3)
      TST[4]=Functions.EAJL(ref PARM.WBMN[PARM.ISL],ref RE)+TST[4];
      TST[5]=Functions.EAJL(ref PARM.WLSN[PARM.ISL],ref RE)+TST[5];
      TST[6]=Functions.EAJL(ref PARM.WLMN[PARM.ISL],ref RE)+TST[6];
//     TST(7)=EAJL(WHPC(PARM.ISL),RE)+TST(7)
//     TST(8)=EAJL(WHSC(PARM.ISL),RE)+TST(8)
      TST[9]=Functions.EAJL(ref PARM.WBMC[PARM.ISL],ref RE)+TST[9];
      TST[10]=Functions.EAJL(ref PARM.WLSC[PARM.ISL],ref RE)+TST[10];
      TST[11]=Functions.EAJL(ref PARM.WLMC[PARM.ISL],ref RE)+TST[11];
      TST[12]=Functions.EAJL(ref PARM.WLSLC[PARM.ISL],ref RE)+TST[12];
      TST[14]=Functions.EAJL(ref PARM.WLS[PARM.ISL],ref RE)+TST[14];
      TST[15]=Functions.EAJL(ref PARM.WLM[PARM.ISL],ref RE)+TST[15];
      TST[16]=Functions.EAJL(ref PARM.WLSL[PARM.ISL],ref RE)+TST[16];
      TST[17]=Functions.EAJL(ref PARM.WP[PARM.ISL],ref RE)+TST[17];
      TST[19]=Functions.EAJL(ref PARM.AP[PARM.ISL],ref RE)+TST[19];
      TST[20]=Functions.EAJL(ref PARM.PMN[PARM.ISL],ref RE)+TST[20];
      TST[21]=Functions.EAJL(ref PARM.FOP[PARM.ISL],ref RE)+TST[21];
      TST[22]=Functions.EAJL(ref PARM.OP[PARM.ISL],ref RE)+TST[22];
      if(NMIX==0){
          TST[23]=Functions.EAJL(ref PARM.CLA[PARM.ISL],ref RE)+TST[23];
          TST[24]=Functions.EAJL(ref PARM.SIL[PARM.ISL],ref RE)+TST[24];
          TST[27]=Functions.EAJL(ref PARM.ROK[PARM.ISL],ref RE)+TST[27];
      }
      TST[25]=Functions.EAJL(ref DUM[PARM.ISL],ref RE)+TST[25];
      TST[26]=Functions.EAJL(ref PARM.UP[PARM.ISL],ref RE)+TST[26];
      TST[28]=Functions.EAJL(ref PARM.WNH3[PARM.ISL],ref RE)+TST[28];
      I1=29;
      for(int I=1; I < PARM.NDP; I++){
          TST[I1]=Functions.EAJL(ref PARM.PSTZ[I,PARM.ISL],ref RE)+TST[I1];
          I1=I1+1;
      }
    lbl8: J1=J-1;
//     COMPUTE MATERIAL PER DEPTH (kg/ha/m)
      for(int I=1; I < ISM; I++)
      {
          TST[I]=TST[I]/DMX;
      }
      XX=0.0;
      for(J=1; J < J1; J++)
      {
          int LL=PARM.LID[J];
          ZZ=PARM.Z[LL]-XX;
    //     DISTRIBUTE MIXED MATERIAL UNIFORMLY THRU PLOW DEPTH
          PARM.WNO3[LL]=TST[1]*ZZ+PARM.WNO3[LL];
    //     WHPN(LL)=TST(2)*ZZ+WHPN(LL)
    //     WHSN(LL)=TST(3)*ZZ+WHSN(LL)
          PARM.WBMN[LL]=TST[4]*ZZ+PARM.WBMN[LL];
          PARM.WLSN[LL]=TST[5]*ZZ+PARM.WLSN[LL];
          PARM.WLMN[LL]=TST[6]*ZZ+PARM.WLMN[LL];
    //     WHPC(LL)=TST(7)*ZZ+WHPC(LL)
    //     WHSC(LL)=TST(8)*ZZ+WHSC(LL)
          PARM.WBMC[LL]=TST[9]*ZZ+PARM.WBMC[LL];
          PARM.WLSC[LL]=TST[10]*ZZ+PARM.WLSC[LL];
          PARM.WLMC[LL]=TST[11]*ZZ+PARM.WLMC[LL];
          PARM.WLSLC[LL]=TST[12]*ZZ+PARM.WLSLC[LL];
          PARM.WLS[LL]=TST[14]*ZZ+PARM.WLS[LL];
          PARM.WLM[LL]=TST[15]*ZZ+PARM.WLM[LL];
          PARM.WLSL[LL]=TST[16]*ZZ+PARM.WLSL[LL];
          if(J==1){
              if(PARM.WLS[LL]>XTP[1])
                  new TMXL1(DMX,TST[14],PARM.WLS[LL],XTP[1],YTP[1]);
              if(PARM.WLM[LL]>XTP[2])
                  new TMXL1(DMX,TST[15],PARM.WLM[LL],XTP[2],YTP[2]);
              if(PARM.WLSL[LL]>XTP[3])
                  new TMXL1(DMX,TST[16],PARM.WLSL[LL],XTP[3],YTP[3]);
              if(PARM.WLSC[LL]>XTP[4])
                  new TMXL1(DMX,TST[10],PARM.WLSC[LL],XTP[4],YTP[4]);
              if(PARM.WLMC[LL]>XTP[5])
                  new TMXL1(DMX,TST[11],PARM.WLMC[LL],XTP[5],YTP[5]);
              if(PARM.WLSLC[LL]>XTP[6])
                  new TMXL1(DMX,TST[12],PARM.WLSLC[LL],XTP[6],YTP[6]);
              if(PARM.WLSN[LL]>XTP[7])
                  new TMXL1(DMX,TST[5],PARM.WLSN[LL],XTP[7],YTP[7]);
              if(PARM.WLMN[LL]>XTP[8])
                  new TMXL1(DMX,TST[6],PARM.WLMN[LL],XTP[8],YTP[8]);
          }
          PARM.WLSLNC[LL]=PARM.WLSC[LL]-PARM.WLSLC[LL];
          PARM.RSD[LL]=0.001*(PARM.WLS[LL]+PARM.WLM[LL]);
          PARM.WP[LL]=TST[17]*ZZ+PARM.WP[LL];
          PARM.AP[LL]=TST[19]*ZZ+PARM.AP[LL];
          PARM.PMN[LL]=TST[20]*ZZ+PARM.PMN[LL];
          PARM.FOP[LL]=TST[21]*ZZ+PARM.FOP[LL];
          PARM.OP[LL]=TST[22]*ZZ+PARM.OP[LL];
          DUM[LL]=TST[25]*ZZ+DUM[LL];
          PARM.UP[LL]=TST[26]*ZZ+PARM.UP[LL];
          if(NMIX==0){
              PARM.ROK[LL]=TST[27]+PARM.ROK[LL]/ZZ;
              PARM.CLA[LL]=TST[23]+PARM.CLA[LL]/ZZ;
              PARM.SIL[LL]=TST[24]+PARM.SIL[LL]/ZZ;
          }
          PARM.WNH3[LL]=TST[28]*ZZ+PARM.WNH3[LL];
          I1=29;
          for(int I=1; I < PARM.NDP; I++)
          {
              PARM.PSTZ[I,LL]=TST[I1]*ZZ+PARM.PSTZ[I,LL];
              I1=I1+1;
          }
          PARM.PSP[LL]=DUM[LL]/(PARM.UP[LL]+DUM[LL]);
          double RX=Math.Min(1.0,(100.0-PARM.ROK[LL])/(100.0-PARM.UN[LL]));
          PARM.FC[LL]=PARM.FC[LL]*RX;
          PARM.S15[LL]=PARM.S15[LL]*RX;
          PARM.PO[LL]=PARM.PO[LL]*RX;
          new SPOFC(ref LL);
          PARM.SAN[LL]=100.0-PARM.CLA[LL]-PARM.SIL[LL];
          PARM.WT[LL]=PARM.BD[LL]*ZZ*Math.Pow(1,4);
          XX=PARM.Z[LL];
      }
      XX=DMX-PARM.Z[PARM.LID[J1]];
      PARM.WNO3[PARM.ISL]=PARM.WNO3[PARM.ISL]+TST[1]*XX;
//     WHPN(ISL)=WHPN(ISL)+TST(2)*XX
//     WHSN(ISL)=WHSN(ISL)+TST(3)*XX
      PARM.WBMN[PARM.ISL]=PARM.WBMN[PARM.ISL]+TST[4]*XX;
      PARM.WLSN[PARM.ISL]=PARM.WLSN[PARM.ISL]+TST[5]*XX;
      PARM.WLMN[PARM.ISL]=PARM.WLMN[PARM.ISL]+TST[6]*XX;
//     WHPC(ISL)=WHPC(ISL)+TST(7)*XX
//     WHSC(ISL)=WHSC(ISL)+TST(8)*XX
      PARM.WBMC[PARM.ISL]=PARM.WBMC[PARM.ISL]+TST[9]*XX;
      PARM.WLSC[PARM.ISL]=PARM.WLSC[PARM.ISL]+TST[10]*XX;
      PARM.WLMC[PARM.ISL]=PARM.WLMC[PARM.ISL]+TST[11]*XX;
      PARM.WLSLC[PARM.ISL]=PARM.WLSLC[PARM.ISL]+TST[12]*XX;
      PARM.WLS[PARM.ISL]=PARM.WLS[PARM.ISL]+TST[14]*XX;
      PARM.WLM[PARM.ISL]=PARM.WLM[PARM.ISL]+TST[15]*XX;
      PARM.WLSL[PARM.ISL]=PARM.WLSL[PARM.ISL]+TST[16]*XX;
      PARM.WLSLNC[PARM.ISL]=PARM.WLSC[PARM.ISL]-PARM.WLSLC[PARM.ISL];
      PARM.RSD[PARM.ISL]=0.001*(PARM.WLS[PARM.ISL]+PARM.WLM[PARM.ISL]);
      PARM.WP[PARM.ISL]=PARM.WP[PARM.ISL]+TST[17]*XX;
      PARM.AP[PARM.ISL]=PARM.AP[PARM.ISL]+TST[19]*XX;
      PARM.PMN[PARM.ISL]=PARM.PMN[PARM.ISL]+TST[20]*XX;
      PARM.FOP[PARM.ISL]=PARM.FOP[PARM.ISL]+TST[21]*XX;
      PARM.OP[PARM.ISL]=PARM.OP[PARM.ISL]+TST[22]*XX;
      DUM[PARM.ISL]=DUM[PARM.ISL]+TST[25]*XX;
      PARM.UP[PARM.ISL]=PARM.UP[PARM.ISL]+TST[26]*XX;
      if(NMIX==0){
          PARM.ROK[PARM.ISL]=PARM.ROK[PARM.ISL]+TST[27]*XX;
          PARM.CLA[PARM.ISL]=PARM.CLA[PARM.ISL]+TST[23]*XX;
          PARM.SIL[PARM.ISL]=PARM.SIL[PARM.ISL]+TST[24]*XX;
      }
      PARM.WNH3[PARM.ISL]=PARM.WNH3[PARM.ISL]+TST[28]*XX;
//     CALL NCONT(VAR(77),VAR(75),VAR(76),VAR(65),VAR(73))
      I1=29;
      for (int I = 1; I < PARM.NDP; I++)
      {
          PARM.PSTZ[I,PARM.ISL]=PARM.PSTZ[I,PARM.ISL]+TST[I1]*XX;
          I1=I1+1;
      }
      PARM.PSP[PARM.ISL] = DUM[PARM.ISL] / (PARM.UP[PARM.ISL] + DUM[PARM.ISL]);
      ZZ = PARM.Z[PARM.ISL] - PARM.Z[PARM.LID[J1]];
      if(NMIX==0){
          PARM.ROK[PARM.ISL]=PARM.ROK[PARM.ISL]/ZZ;
          PARM.CLA[PARM.ISL]=PARM.CLA[PARM.ISL]/ZZ;
          PARM.SIL[PARM.ISL]=PARM.SIL[PARM.ISL]/ZZ;
      }
      if (PARM.UN[PARM.ISL] > 0.0)
      {
          double RX=Math.Min(1.0,(100.0-PARM.ROK[PARM.ISL])/(100.0-PARM.UN[PARM.ISL]));
          PARM.FC[PARM.ISL]=PARM.FC[PARM.ISL]*RX;
          PARM.S15[PARM.ISL]=PARM.S15[PARM.ISL]*RX;
          PARM.PO[PARM.ISL]=PARM.PO[PARM.ISL]*RX;
          new SPOFC(ref PARM.ISL);
      }
      PARM.SAN[PARM.ISL] = 100.0 - PARM.CLA[PARM.ISL] - PARM.SIL[PARM.ISL];
      PARM.WT[PARM.ISL] = PARM.BD[PARM.ISL] * ZZ * Math.Pow(1, 4); ;
      if(EE<0.9)return;
      int LD2 = PARM.LID[2];
      X1=PARM.STL[PARM.JJK]+PARM.STD[PARM.JJK]+PARM.STDO;
      PARM.DM[PARM.JJK]=PARM.DM[PARM.JJK]-PARM.STL[PARM.JJK];
      XX = PARM.STL[PARM.JJK] / (PARM.DM[PARM.JJK] + Math.Pow(1, -10));
      X2=XX*PARM.UN1[PARM.JJK];
      double X3=XX*PARM.UP1[PARM.JJK];
      double W1=XX*PARM.UK1[PARM.JJK];
      double X4=PARM.STDN[PARM.JJK]+PARM.STDON+X2;
      new NCNSTD(ref X1,ref X4,0.0);
      PARM.WLMN[LD2]=PARM.WLMN[LD2]+PARM.WLMN[PARM.LD1];
      PARM.WLMN[PARM.LD1] = 0.0;
      PARM.WLSN[LD2] = PARM.WLSN[LD2] + PARM.WLSN[PARM.LD1];
      PARM.WLSN[PARM.LD1] = 0.0;
      PARM.WLS[LD2] = PARM.WLS[LD2] + PARM.WLS[PARM.LD1];
      PARM.WLS[PARM.LD1] = 0.0;
      PARM.WLM[LD2] = PARM.WLM[LD2] + PARM.WLM[PARM.LD1];
      PARM.WLM[PARM.LD1] = 0.0;
      PARM.WLSL[LD2] = PARM.WLSL[LD2] + PARM.WLSL[PARM.LD1];
      PARM.WLSL[PARM.LD1] = 0.0;
      PARM.WLSC[LD2] = PARM.WLSC[LD2] + PARM.WLSC[PARM.LD1];
      PARM.WLSC[PARM.LD1] = 0.0;
      PARM.WLMC[LD2] = PARM.WLMC[LD2] + PARM.WLMC[PARM.LD1];
      PARM.WLMC[PARM.LD1] = 0.0;
      PARM.WLSLC[LD2] = PARM.WLSLC[LD2] + PARM.WLSLC[PARM.LD1];
      PARM.WLSLC[PARM.LD1] = 0.0;
      PARM.WLSLNC[LD2] = PARM.WLSLNC[LD2] + PARM.WLSLNC[PARM.LD1];
      PARM.WLSLNC[PARM.LD1] = 0.0;
      PARM.FOP[LD2] = PARM.FOP[LD2] + PARM.FOP[PARM.LD1] + PARM.STDP + PARM.STDOP + X3;
      PARM.FOP[PARM.LD1] = 0.0;
      PARM.AP[LD2] = PARM.AP[LD2] + PARM.AP[PARM.LD1];
      PARM.AP[PARM.LD1] = 0.0;
      PARM.SOLK[LD2]=PARM.SOLK[LD2]+PARM.STDK+PARM.STDOK+W1;
      PARM.RSD[PARM.LD1] = 0.0;
      PARM.STL[PARM.JJK]=0.0;
      PARM.STDO=0.0;
      PARM.STD[PARM.JJK]=0.0;
      PARM.STDN[PARM.JJK]=0.0;
      PARM.STDON=0.0;
      PARM.STDL=0.0;
      PARM.STDP=0.0;
      PARM.STDOP=0.0;
      PARM.STDK=0.0;
      PARM.STDOK=0.0;
      PARM.UN1[PARM.JJK]=Math.Max(Math.Pow(1,-10),PARM.UN1[PARM.JJK]-X2);
      PARM.UP1[PARM.JJK]=PARM.UP1[PARM.JJK]-X3;
      PARM.UK1[PARM.JJK]=Math.Max(Math.Pow(1,-10),PARM.UK1[PARM.JJK]-W1);
      return; 
        }
	}
}

