using System;

namespace Epic
{
	public class TLOP
	{
		public TLOP (double CSTX, double COX, double JRT)
		{
          //SUBROUTINE TLOP(CSTX,COX,JRT)
          //EPIC0810
          //Translated by Heath Yates 
          //THIS FORTRAN SUBPROGRAM TRANSLATED INTO C# CONTROLS ALL TILLAGE OPERATIONS INCLUDING PLANTING
          //HARVESTING, AND AUTOMATIC FERTILIZER APPLICATIONS AT PLANTING.
          //USE PARM
          Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
          double YTP = new double[16];
          double FNPP[X]=DMLA[PARM.JJK]*X/(X+EXP(PPCF[1,PARM.JJK]-PPCF[2,PARM.JJK]*X));
          double JRT=0;
          double II=IHC[PARM.JT1];
          double NN=NBC[PARM.IRO];
          double N1=Math.Max(1,NCP[PARM.JJK]);
          double X1=CND[PARM.IRO,PARM.KT];
          double X2; 
          double II;
          double X3;
          double X4;
          double X5;
          double X6;
          double X7;
          double X8;
          double X9;
          double Y1;
          double Y2;
          double Y3;
          double Y4;
          double Y5;
          double X11;
          double Z1;
          double Z2;
          double Z3;
          double Z4;
          double I2;
          double XX;
          if(Math.Abs(X1-PARM.CN0)>0.0]{
              X2=PARM.SMX;
              Epic.HCNSLP[X1,X3];
              PARM.CN0=X1;
              PARM.CN2=X1;
              PARM.SCI=PARM.SMX*PARM.SCI/X2;
          }
          if(II==PARM.NHC(1] || II==PARM.NHC[2] || II==PARM.NHC[3] || II==PARM.NHC[19];
          || II==PARM.NHC[22])goto lbl10;
          if(II==PARM.NHC[5])goto lbl61;
          if(II==PARM.NHC[6])goto lbl53;
          if(II==PARM.NHC[7] || II==PARM.NHC[8))goto lbl57;
          if(II==PARM.NHC[10])goto lbl51;
          if(II==PARM.NHC[11])goto lbl52;
          if(II==PARM.NHC[12] || II==PARM.NHC[13])goto lbl56;
          if(II==PARM.NHC[14])goto lbl59;
          if(II==PARM.NHC[23])goto lbl63;
          if(II==PARM.NHC[24])goto lbl64;
          goto lbl6;
   lbl51: CSTX=-CSTX*PARM.YLD1[N1,PARM.JJK]/(1.0-PARM.WCY[PARM.JJK]);
          COX=CSTX;
          goto lbl57;
   lbl52: CSTX=-CSTX*PARM.YLD[PARM.JJK]/(1.-PARM.WCY[PARM.JJK]);
          COX=CSTX;
          goto lbl57;
   lbl56: if(PARM.ICUS[PARM.JT1]==0)goto lbl57;
          CSTX=-CSTX*PARM.YLD[PARM.JJK]/(1.0-PARM.WCY[PARM.JJK]);
          COX=CSTX;
          goto lbl57;
   lbl53: PARM.IDRL=1;
   lbl61: PARM.ISL=PARM.LID[2];
          for(int K=1; K < NN; K++){
              I2=PARM.LY[PARM.IRO,K];
              if(PARM.JH[PARM.IRO,PARM.KT]==KDC[I2])goto lbl27;
          }
          goto lbl26;
   lbl27: if(PARM.KG[I2]>0)goto lbl26;
          if(STMP[PARM.ISL]<TBSC[I2]+2.0 && PARM.MO<12){
              KOMP[PARM.KT]=0;
              JRT=1;
              return;
          }
          PARM.AWC=RZSW;
          PARM.AQV=0.0;
          PARM.ARF=0.0;
          PARM.IGO=PARM.IGO+1;
          for(int KC=1; KC < NN; KC++){
              if(JE[KC]>MNC)break;
          }
          PARM.JE[KC]=I2;
          PARM.JJK=I2;
          PARM.KG[PARM.JJK]=1;
          PARM.JP[PARM.JJK]=0;
          PARM.IYH[PARM.JJK]=1;
          PARM.GSEP=0.0;
          PARM.GSVP=0.0;
          PARM.SRA=0.0;
          PARM.SWH[PARM.JJK]=0.0;
          PARM.SWP[PARM.JJK]=0.0;
          PARM.ACET[PARM.JJK]=0.0;
          PARM.XDLAI[PARM.JJK]=DLAI[PARM.JJK];
          PARM.XDLA0[PARM.JJK]=0.0;
          PARM.WCYD=0.3;
          PARM.STDO=PARM.STDO+PARM.STD[PARM.JJK];
          PARM.STDOK=PARM.STDOK+PARM.STDK;
          PARM.STDON=PARM.STDON+PARM.STDN[PARM.JJK];
          PARM.STDOP=PARM.STDOP+PARM.STDP;
          PARM.STD[PARM.JJK]=0.0;
          PARM.STDK=0.0;
          PARM.STDL=0.0;
          PARM.STDN[PARM.JJK]=0.0;
          PARM.STDP=0.0;
          PARM.RD[PARM.JJK]=TLD[PARM.JT1];
          PARM.ROSP=RIN[PARM.JT1];
          PARM.HU[PARM.JJK]=0.0;
          PARM.DM[PARM.JJK]=PARM.SDW[PARM.JJK]*5.E-4;
          PARM.DM1[PARM.JJK]=PARM.DM[PARM.JJK];
          PARM.RW[PARM.JJK]=0.4*PARM.DM[PARM.JJK];
          PARM.RWT[PARM.ISL,PARM.JJK]=PARM.RW[PARM.JJK];
          PARM.CHT[PARM.JJK]=0.0;
          PARM.PPL0[PARM.JJK]=PARM.POP[PARM.JJK,PARM.IHU[PARM.JJK]];
          PARM.XLAI[PARM.JJK]=PARM.FNPP[PARM.PPL0[PARM.JJK]];
          PARM.DMLX[PARM.JJK]=PARM.XLAI[PARM.JJK];
          PARM.X1=SDW[PARM.JJK]*PARM.CSTS[PARM.JJK];
          PARM.COST=PARM.COST+X1;
          PARM.SMM[96,PARM.MO]=SMM[96,PARM.MO]+PARM.CCEM[PARM.JJK]*SDW[PARM.JJK];
          PARM.LRD=2;
          PARM.JPL[PARM.JJK]=1;
          if(NCP[PARM.JJK]==0)NCP[PARM.JJK]=1;
          N1=Math.Max(1,NCP[PARM.JJK]);
          PARM.IPLD[N1,PARM.JJK]=IYR*10000+PARM.MO*100+PARM.KDA;
          PARM.IHVD[N1,PARM.JJK]=0;
          //if(NOP>0)Console.WriteLine(KW[1],32)IYR,MO,KDA,CPNM[JJK],CV,X1
          //if(KFL[20]>0)Console.WriteLine(KW[20],49)IYR,MO,KDA,TIL[JT1],KDC[JJK],II,&
          //&NBE[JT1],NBT[JT1],X1,X1,SDW[JJK]
    lbl6: EE=PARM.EMX[PARM.JT1];
          PPL0[PARM.JJK]=(1.-FPOP[PARM.JT1])*PPL0[PARM.JJK];
          XLAI[PARM.JJK]=FNPP[PPL0[PARM.JJK]);
          DMLX[PARM.JJK]=XLAI[PARM.JJK];
          DMX=TLD[PARM.JT1];
          if(II/=PARM.NHC[19] && II/=PARM.NHC[2])Epic.TMIX[EE,DMX,0];
          if(DMX>BIG)TLD[PARM.JT1]=BIG;
          if(II==PARM.NHC[15]){;
              SATC(PARM.LID[2]]=PRMT[33];
          } 
          else{
              if(II==PARM.NHC[16])SATC[PARM.LID[2]]=SATK;
          }
   lbl57: if(IDR>0){
              if(II==PARM.NHC[25]){
                  HCL[IDR]=HCLN;
              } 
          else{
                  if(II==PARM.NHC[26])HCL[IDR]=HCLD;
              }
          }
          //if(KFL[20]>0)Console.WriteLine(KW[20],50)IYR,MO,KDA,TIL[JT1],KDC[JJK],II, NBE[JT1],NBT[JT1],CSTX,COX,FULU[JT1]
          SMM[92,PARM.MO]=SMM[92,PARM.MO]+FULU[PARM.JT1] ;
          if(II==PARM.NHC[2] || II==PARM.NHC[3] || II==PARM.NHC[19])goto lbl26;
    lbl7: XX=TLD[PARM.JT1]*1000.0;
          //if(NOP>0)Console.WriteLine(KW[1],28)IYR,MO,KDA,TIL[JT1],XX,XHSM,CSTX
          if(II/=PARM.NHC[17] && II/=PARM.NHC[18])goto lbl26;
          if(II/=PARM.NHC[18]){
              DHT=DKH[PARM.JT1];
              DKHL=DHT;
              DKIN=DKI[PARM.JT1];
              ifD=1;
          } 
          else{
              ifD=0;
          }
          //if(NOP>0)Console.WriteLine(KW[1],30)DHT,DKIN,XHSM
          goto lbl26;
   lbl59: Epic.TBURN;
          goto lbl7;
   lbl63: ICV=1;
          goto lbl57;
   lbl64: ICV=0;
          goto lbl57;
   lbl10: for(int K=1; K < NN; K++){
              if(JE[K]>MNC)continue;
              if(PARM.JH[PARM.IRO,PARM.KT]==KDC[JE[K]])break;
          }
          if(K>NN){
              CSTX=0.0;
              COX=0.0;
              JRT=1;
              return;
          }
          PARM.JJK=JE[K];
          if(IDC[PARM.JJK]==NDC[7] || IDC[PARM.JJK]==NDC[8] || IDC[PARM.JJK]==NDC[10]){
              if(IYH[PARM.JJK]/=PARM.LYR[PARM.IRO,PARM.KT] && PARM.LYR[PARM.IRO,PARM.KT]/=1){
                  KOMP[PARM.KT]=1;
                  return;
              }
          }
          N1=Math.Max(1,NCP[PARM.JJK]);
          PARM.JHV=K;
          KHV=1;
          if(II==PARM.NHC[1])goto lbl22;
          if(II/=PARM.NHC[2] && II/=PARM.NHC[19] && II/=PARM.NHC[22]){
              if(IHT[PARM.JT1]>0)goto lbl26;
              IHT[PARM.JT1]=1;
          }
          HVWC=HWC[PARM.IRO,PARM.KT)                                                                 ;
          if(HVWC>0.0 && PARM.WCYD>HVWC && O<PARM.MOFX){
              JRT=1;
              KOMP[PARM.KT]=0;
              return;
          }
          if(JP[PARM.JJK]==0){
              JP[PARM.JJK]=1;
              if(II/=PARM.NHC[3])NCR[PARM.JJK]=NCR[PARM.JJK]+1;
          }
          HUF[PARM.JJK]=Math.Max(HUF[PARM.JJK],HU[PARM.JJK]);
          DMF[N1,PARM.JJK]=DM1[PARM.JJK];
          TRA[PARM.JJK]=SRA+TRA[PARM.JJK];
          if(RD[PARM.JJK]>RDF[PARM.JJK])RDF[PARM.JJK]=RD[PARM.JJK];
          X9=DM[PARM.JJK]+0.001;
          X2=UN1[PARM.JJK]/X9;
          X7=0.001*X2;
          X3=UP1[PARM.JJK]/X9;
          X8=UK1[PARM.JJK]/X9;
          XX=PARM.STD[PARM.JJK]+Math.Pow(1,-10);
          RNR=PARM.STDN[PARM.JJK]/XX;
          RPR=PARM.STDP/XX;
          RKR=PARM.STDK/XX;
          PARM.STDL=CLG*XX;
          RLR=Math.Min(0.8,PARM.STDL/(PARM.STD[PARM.JJK]+Math.Pow(1,-10)));
          if(ORHI[PARM.JT1]<Math.Pow(1,-10)){
              if(IDC[PARM.JJK]==NDC[8]){
                  F=1.;
              } 
              else{
                  XX=100.*SWH[PARM.JJK]/(SWP[PARM.JJK]+Math.Pow(1,-10));
                  F=XX/(XX+EXP(SCRP[10,1]-SCRP[10,2]*XX));
              }
              XX=Math.Max(APARM.JHI[PARM.JJK]-WSYF[PARM.JJK],0.0);
              FT=Math.Max(0.1,1.0+PRMT[50]*(IYR-2000));
              X1=Math.Min(F*XX+WSYF[PARM.JJK],0.9*DM[PARM.JJK]/(STL[PARM.JJK]+Math.Pow(1,-10)))*FT;
              if(IDC[PARM.JJK]==NDC[8]){
                  X2=PRMT[76]/PARM.AWC;
                  X1=Math.Min(HI[PARM.JJK],X1*X2);
              }
              X2=1000.0*CNY[PARM.JJK]*Math.Pow((X7/BN[3,PARM.JJK]),0.1);
              X3=1000.0*CPY[PARM.JJK]*Math.Pow((.001*X3/BP[3,PARM.JJK]),0.1);
              goto lbl17;
          }
          if(II/=PARM.NHC[19] && II/=PARM.NHC[22])goto lbl16;
          if(IDC[PARM.JJK]==NDC[7] || IDC[PARM.JJK]==NDC[8] || IDC[PARM.JJK]==NDC[10]){
              PARM.KTT=0;
              goto lbl26;
          }
          KOMP[PARM.KT]=0;
          PARM.KTT=PARM.KT;
          if(II==PARM.NHC[22]){
              XX=CHT[PARM.JJK]-HMO[PARM.JT1];
              if(XX<0.001 || NMW<IMW)return;
              X1=XX/CHT[PARM.JJK];
              ZZ=HMO[PARM.JT1]/CHT[PARM.JJK];
              if(PARM.STD[PARM.JJK]<0.001)YZ=0.0;
              NMW=0;
              goto lbl45;
          }
          if(AGPM<GZLM)return;
          GCOW=WSA/RSTK[PARM.IRO,PARM.KT];
          XX=GCOW*ORHI[PARM.JT1]/(WSA*HE[PARM.JT1]);
          X1=Math.Min(XX/AGPM,0.9);
          goto lbl17;
   lbl16: X1=ORHI[PARM.JT1];
          if(TLD[PARM.JT1]<=0.0)goto lbl17;
          Epic.THVRT[YY,X3,X1,X6,X7,N1);
          goto lbl25;
   lbl17: ZZ=Math.Max(0.01,1.0-X1);
          YZ=X1*PARM.STD[PARM.JJK];
   lbl45: XZ=X1*STL[PARM.JJK];
          HIF[N1,PARM.JJK]=X1;
          APARM.JHI[PARM.JJK]=0.0;
          CHT[PARM.JJK]=CHT[PARM.JJK]*ZZ;
          HU[PARM.JJK]=HU[PARM.JJK]*PRMT[69];
          SLAI[PARM.JJK]=SLAI[PARM.JJK]*ZZ;
          STL[PARM.JJK]=STL[PARM.JJK]*ZZ;
          PARM.STD[PARM.JJK]=Math.Max(Math.Pow(1,-10),PARM.STD[PARM.JJK]*ZZ);
          PARM.STDL=PARM.STDL*ZZ;
          Epic.PESTF;
          TPSF[N1,PARM.JJK]=TPSF[N1,PARM.JJK]+PSTF[PARM.JJK];
          NPSF[N1,PARM.JJK]=NPSF[N1,PARM.JJK]+1;
          PARM.YLD[PARM.JJK]=XZ*HE[PARM.JT1]*PSTF[PARM.JJK];
          YLSD=YZ*HE[PARM.JT1];
          Y4=YZ*RNR;
          Y5=YZ*RPR;
          Y6=YZ*RKR;
          PARM.STDN[PARM.JJK]=Math.Max(Math.Pow(1,-10),PARM.STDN[PARM.JJK]-Y4);
          PARM.STDP=Math.Max(Math.Pow(1,-10),PARM.STDP-Y5);
          PARM.STDK=Math.Max(Math.Pow(1,-10),PARM.STDK-Y6);
          PARM.STDL=Math.Max(PARM.STDL-YZ*RLR,0.1*PARM.STD[PARM.JJK]);
          X6=PSTF[PARM.JJK];
          X4=Math.Min(XZ*X2,UN1[PARM.JJK]);
          X5=Math.Min(XZ*X3,UP1[PARM.JJK]);
          X11=XZ-PARM.YLD[PARM.JJK]+YZ-YLSD;
          Z2=YLSD*RNR;
          Z3=YLSD*RPR;
          Z4=YLSD*RKR;
          YLN=Math.Min(0.9*(UN1[PARM.JJK]+PARM.STDN[PARM.JJK]),PARM.YLD[PARM.JJK]*X2+Z2);
          YLP=Math.Min(0.9*(UP1[PARM.JJK]+PARM.STDP),PARM.YLD[PARM.JJK]*X3+Z3);
          YLC=0.42*(YLSD+PARM.YLD[PARM.JJK]);
          X10=X4-YLN+Y4;
          Epic.NCNSTD[X11,X10,0];
          PARM.FOP[LD1]=PARM.FOP[LD1]+X5-YLP+Y5;
          YY=PARM.YLD[PARM.JJK]+YLSD;
          PARM.YLD[PARM.JJK]=YY;
          if(ORHI[PARM.JT1]>0.0){
              YLD2[N1,PARM.JJK]=YLD2[N1,PARM.JJK]+YY;
              X11=YY*PRYF[PARM.JJK];
          } 
          else{
              if(IDC[PARM.JJK]==NDC[9]){
                  PARM.YLD1[N1,PARM.JJK]=PARM.YLD1[N1,PARM.JJK]+FTO[PARM.JJK]*YY;
                  YLD2[N1,PARM.JJK]=YLD2[N1,PARM.JJK]+PARM.YLD1[N1,PARM.JJK]*(1.0/FLT[PARM.JJK]-1.0);
              } 
              else{
                  PARM.YLD1[N1,PARM.JJK]=PARM.YLD1[N1,PARM.JJK]+YY;
                  X10=YY*PRYG[PARM.JJK];
              }            
          }
          X3=RW[PARM.JJK];
          PARM.RWF[N1,PARM.JJK]=X3;
          PARM.JD=PARM.JJK;
          PARM.SRAF=SRA;
          PARM.SRA=0.0;
          PARM.UN1[PARM.JJK]=Math.Max(Math.Pow(1,-10),UN1[PARM.JJK]-X4);
          PARM.UP1[PARM.JJK]=PARM.UP1[PARM.JJK]-X5;
          PARM.DM[PARM.JJK]=PARM.DM[PARM.JJK]-XZ;
          PARM.YLNF[N1,PARM.JJK]=PARM.YLNF[N1,PARM.JJK]+YLN;
          PARM.YLPF[N1,PARM.JJK]=PARM.YLPF[N1,PARM.JJK]+YLP;
          PARM.YLKF[N1,PARM.JJK]=YLKF[N1,PARM.JJK]+YLK;
          PARM.YLCF[N1,PARM.JJK]=YLCF[N1,PARM.JJK]+YLC;
          goto lbl25;
   lbl22: if(IPD==5){
              Epic.SPRNT[YTP];
              //Console.WriteLine(KW[1],'(T5,A)')'SOIL DATA'
              Epic.SOLIO(YTP,1);
          }
          Epic.TRDST;
          PARM.NCP[PARM.JJK]=Math.Min(NBCX[PARM.IRO,PARM.JJK],PARM.NCP[PARM.JJK]+1);
          if(PARM.YLD1[N1,PARM.JJK]+YLD2[N1,PARM.JJK]<Math.Pow(1,-10))NCR[PARM.JJK]=NCR[PARM.JJK]+1;
          PARM.JE[PARM.JHV]=PARM.MNC+1;
          PARM.GSEF=PARM.GSEP;
          PARM.SMGS[4]=PARM.SMGS[4]+PARM.GSEF;
          PARM.GSEP=0.0;
          PARM.GSVF=PARM.GSVP;
          PARM.SMGS[6]=PARM.SMGS[6]+PARM.GSVF;
          PARM.GSVP=0.0;
          PARM.IGO=Math.Max(0,PARM.IGO-1);
          PARM.KG[PARM.JJK]=0;
          PARM.IYH[PARM.JJK]=0;
          PARM.JPL[PARM.JJK]=0;
          PARM.HU[PARM.JJK]=0.0;
          PARM.HUI[PARM.JJK]=0.0;
          PARM.HSM=0.0;
          PARM.SLAI[PARM.JJK]=0.0;
          PARM.WLV[PARM.JJK]=0.0;
          PARM.ANA[PARM.JJK]=0.0;
          PARM.NFA=0;
          PARM.NII=PARM.IRI;
          PARM.CSTF[N1,PARM.JJK]=PARM.COST;
          PARM.CSOF[N1,PARM.JJK]=PARM.COST-PARM.CSFX;
          PARM.COST=0.0;
          PARM.CSFX=0.0;
          PARM.IHU[PARM.JJK]=PARM.IHU[PARM.JJK]+1;
          if(IHU[PARM.JJK]>NHU[PARM.JJK])IHU[PARM.JJK]=1;
          PARM.CAW[PARM.N1,PARM.JJK]=PARM.AWC;
          PARM.CQV[PARM.N1,PARM.JJK]=PARM.AQV;
          PARM.CRF[PARM.N1,PARM.JJK]=PARM.ARF ;
          PARM.ETG[PARM.N1,PARM.JJK]=PARM.ACET[PARM.JJK]+PARM.ETG[PARM.N1,PARM.JJK];
          PARM.PSTS=0.0;
          PARM.IPST=0;
          PARM.WS=1.0;
          PARM.FGC=0.0;
          PARM.U=0.0;
          PARM.UN=0.0;
          goto lbl6;
   lbl25: PARM.TYN=PARM.TYN+PARM.YLN;
          PARM.TYP=PARM.TYP+PARM.YLP;
          PARM.TYK=PARM.TYK+PARM.YLK;
          PARM.TYC=PARM.TYC+PARM.YLC;
          PARM.IHVD[N1,PARM.JJK]=IYR*10000+PARM.MO*100+PARM.KDA;
          if(PARM.ICUS[PARM.JT1]/=0.0 && PARM.CSTX<=0.0){
              PARM.CSTX=-PARM.CSTX*PARM.YLD[PARM.JJK];
              PARM.COX=PARM.CSTX;
          }
          //if(NOP>0.0AND.II/=NHC[19])Console.WriteLine(KW[1],29)IYR,MO,KDA,TIL[JT1],&
          //CPNM[JD],YY,YLSD,AGPM,X9,X3,X1,X6,X7,WCYD,XHSM,CSTX
          //goto lbl6
   lbl26: //if(NOP>0.0AND.II==NHC[19])Console.WriteLine(KW[1],62)IYR,MO,KDA,TIL[JT1],&
          //CPNM[JJK],YLD[JJK],YLSD,AGPM,STL[JJK],X3,X1,XHSM
          //if(KFL[29]>0.0AND.II==NHC[19])Console.WriteLine(KW[29],31)IYR,MO,KDA,TIL[JT1],&
          //CPNM[JJK],DM[JJK],X3,SLAI[JJK],STL[JJK],AGPM,X1,YLD[JJK],YLSD,XHSM 
          return;
       //28 //FORMAT(1X,3I4,2X,A8,2X,'DPTH = ',F5.0,'mm',2X,'HUSC = ',F4.2,2X&
       //&,'COST=',F7.0,'$/ha')
       //29 //FORMAT(1X,3I4,2X,A8,2X,A4,2X,'YLD=',F7.2,'t/ha',2X,'YLSD=',F7.2,&
         //&'t/ha'2X,'AGPM=',F7.2,'t/ha',2X,'BIOM=',F7.2,'t/ha',2X,'RW=',F5.2,&
         //&'t/ha',2X,'HI=',F6.2,2X,'PSTF=',F4.2,2X,'NCN=',F5.3,'G/G',2X,'WCY=',&
         //&F4.2,2X,'HUSC=',F4.2,2X,'COST=',F7.0,'$/ha')
       //30 //FORMAT('+',T45,'DKH=',F5.0,'MM',2X,'DKI=',F6.2,'M',2X,'HUSC=',F4.2)
       //31 //FORMAT(1X,3I4,2X,A8,2X,A4,9F10.3)
       //32 //FORMAT(1X,3I4,2X,A4,2X,'RSD = ',F5.1,'T',2X,'COST=',F7.0,'$/ha')
       //49 //FORMAT(1X,3I4,2X,A8,8X,I6,6X,3I4,F10.2,10X,3F10.2)
       //50 //FORMAT(1X,3I4,2X,A8,8X,I6,6X,3I4,2F10.2,20X,F10.2)
       //62 //FORMAT(1X,3I4,2X,A8,2X,A4,2X,'YLD=',F7.4,'t/ha',2X,'YSD=',F7.2,&
          //&'t/ha',2X,'AGPM=',F7.2,'t/ha',2X,'STL=',F7.2,'t/ha',2X'RWT=',F7.2,&
         //&'t/ha',2X,'HI=',F7.3,2X,'HUSC=',F4.2)
		}
	}
}
