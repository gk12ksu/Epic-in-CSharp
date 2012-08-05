using System;

namespace Epic
{
	public class ESLOS
	{
		public ESLOS (double JRT)
		{

            //Epic0810
            //Translated by Heath Yates 
            //This is translated from a subprogram from fortran calculates the thickness of soil removed
            //by each erosion event, moves the top layer into the second layer by a dist 
            //equal the eroded thickness, and adusts the top layer soil properties by interpolation. When the 
            //second layer is reduced to a thickness of 10 mm, it is places into the third layer. 

            //Use parm 
            //X1, X2, Z1, Z2, ZZ are not defined as far as author determined in modparam
            
            double WFN[X1,X2,Z1,Z2,ZZ]=(Z1*X1+Z2*X2)/ZZ; //Team could not determine what this was 
            double K=.1*YERO/BD[LD1];
	        double THK=THK+TK;
            double TK=Math.Min(TK*.001,Z[LD1]);
            double J=LID[NBSL];
            double W1=Z[LD1]-TK;
            double W2=TK;
            double WT[LD1]=WT[LD1]-YERO;
            if(Z(LID[2])-Z[LD1]-TK>.01) goto  10;
            //REMOVE LAYER 2 AND PLACE SMALL REMAINING CONTENTS IN LAYER 3
            double ISL=LID[3];
            double LD2=LID[2];
            double Z1=Z[LD2]-Z[LD1];
            double Z2=Z[ISL]-Z[LD2];
            double VV=Z1+Z2
            double ZZ=.01/VV;
            double PSP[ISL]=WFN[PSP[LD2],PSP[ISL],Z1,Z2,VV];
            double BDM[ISL]=WFN[BDM[LD2],BDM[ISL],Z1,Z2,VV];
            double CLA[ISL]=WFN[CLA[LD2],CLA[ISL],Z1,Z2,VV];
            double SIL[ISL]=WFN[SIL[LD2],SIL[ISL],Z1,Z2,VV];
            double SAN[ISL]=100.-CLA[ISL]-SIL[ISL];
            double ROK[ISL]=WFN[ROK[LD2],ROK[ISL],Z1,Z2,VV];
            double SATC[ISL]=WFN[SATC[LD2],SATC[ISL],Z1,Z2,VV];
            double PH[ISL]=WFN[PH[LD2],PH[ISL],Z1,Z2,VV];
            double WT[ISL]=WT[ISL]+WT[LD2];
            double BD[ISL]=.0001*WT[ISL]/VV;
            double WNO3[ISL]=WNO3[ISL]+WNO3[LD2A];
            double WHPN[ISL]=WHPN[ISL]+WHPN[LD2];
            double WHSN[ISL]=WHSN[ISL]+WHSN[LD2];
            double WBMN[ISL]=WBMN[ISL]+WBMN[LD2];
            double WLSN[ISL]=WLSN[ISL]+WLSN[LD2];
            double WLMN[ISL]=WLMN[ISL]+WLMN[LD2];
            double WHPC[ISL]=WHPC[ISL]+WHPC[LD2];
            double WHSC[ISL]=WHSC[ISL]+WHSC[LD2];
            double WBMC[ISL]=WBMC[ISL]+WBMC[LD2];
            double WLS[ISL]=WLS[ISL]+WLS[LD2];
            double WLM[ISL]=WLM[ISL]+WLM[LD2];
            double WLSL[ISL]=WLSL[ISL]+WLSL[LD2];
            double WLSC[ISL]=WLSC[ISL]+WLSC[LD2];
            double WLMC[ISL]=WLMC[ISL]+WLMC[LD2];
            double WLSLC[ISL]=WLSLC[ISL]+WLSLC[LD2];
            double WLSLNC[ISL]=WLSC[ISL]-WLSLC[ISL];
            double WP[ISL]=WP[ISL]+WP[LD2];
            double RSD[ISL]=.001*(WLS[ISL]+WLM[ISL]);
            double AP[ISL]=AP[ISL]+AP[LD2];
            double PMN[ISL]=PMN[ISL]+PMN[LD2];
            double FOP[ISL]=FOP[ISL]+FOP[LD2];
            double OP[ISL]=OP[ISL]+OP[LD2];
            double S15[ISL]=S15[ISL]+S15[LD2];
            double FC[ISL]=FC[ISL]+FC[LD2];
            double PO[ISL]=PO[ISL]+PO[LD2];
            Epic.SPOFC[ISL];
            double ST[ISL]=ST[ISL]+ST[LD2];
            double LORG[LD1]=LORG[LD2];
            //SPLIT LAYER NEAREST SURFACE WITH THICKNESS > 0.15 M IN HALF
            double ZMX=0.0;
            double L1=LD1;
            double MXZ=LD2;
            for (int J = 3; J < NBSL; J++)
            {
                ISL=LID[J];
                ZZ=Z[ISL]-Z[L1];
                if(ZZ > .15) 
                {
                    double MXZ = J;
                    double ZMX = ZZ;
                    goto 7;
                }
                double L1=ISL;
                if(ZZ <= ZMX + .01)
                {
                    continue;
                }
                double ZMX=ZZ;
                double MXZ=J;
            } 
            double ISL=LID[MXZ];
            double L1=LID[MXZ-1];
            if(ZMX > ZQT) 
            {
                goto 7;
            }
            double NBSL=NBSL-1;
            if (NBSL <= 2) 
            {
                double JRT = 1; 
                return;
            }
            for (int J = 2; J < NBSL; J++)
            {
                LID[J]=LID[J+1];
            }
            goto 10;
            7 MX1=MXZ-1;
            if(MX1>2)
            {

               for(int J = 2; J < MX1; J++)
               {
                  LID[J]=LID[J+1];
            
               } 
            } 
            double LID[MX1]=LD2;
            double LORG[LD2]=LORG[LID[MXZ]];
            Epic.SPLA[ISL,L1,LD2,1,.5];
            //ADJUST LAYER 1 BY INTERPOLATING BETWEEN LAYER 2 USING ERODED THICK
           10  double ISL=LID[2];
           PSP[LD1]=WFN[PSP[LD1],PSP[ISL],W1,W2,Z[LD1]];
           BDM[LD1]=WFN[BDM[LD1],BDM[ISL],W1,W2,Z[LD1]];
           CLA[LD1]=WFN[CLA[LD1],CLA[ISL],W1,W2,Z[LD1]];
           SIL[LD1]=WFN[SIL[LD1],SIL[ISL],W1,W2,Z[LD1]];
           SAN[LD1]=100.-CLA[LD1]-SIL[LD1];
           BD[LD1]=WFN[BD[LD1],BD[ISL],W1,W2,Z[LD1]];
           SATC[LD1]=WFN[SATC[LD1],SATC[ISL],W1,W2,Z[LD1]];
           //HCL(LD1)=SATC(LD1)*UPS
           PH[LD1]=WFN[PH[LD1],PH[ISL],W1,W2,Z[LD1]];
           double XX=Z[ISL]-Z[LD1];
           double GX=XX-TK;
           double RTO=GX/XX;
           RX=1.;
           if(ROK[LD1]>0.)
           {
              X3=ROK[LD1];
              ROK[LD1]=WFN[ROK[LD1],ROK[ISL],W1,W2,Z[LD1]];
              RX=(100.-ROK[LD1])/(100.-X3);
           } 
          W1=W1/Z[LD1];
          W2=W2/XX;
          S15[LD1]=WFN[S15[LD1],S15[ISL],W1,W2,RX];
          FC[LD1]=WFN[FC[LD1],FC[ISL],W1,W2,RX];
          PO[ISL]=PO[ISL]*RTO;
          S15[ISL]=S15[ISL]*RTO;
          FC[ISL]=FC[ISL]*RTO;
          Epic.SPOFC[ISL];
          WT[ISL]=WT[ISL]*RTO;
          PO[LD1]=WFN[PO[LD1],PO[ISL],W1,W2,RX];
          Epic.SPOFC[LD1];
          WNO3[LD1)=WNO3[LD1]+EAJL[WNO3[ISL],W2];
          WHPN[LD1]=WHPN[LD1]+EAJL[WHPN[ISL],W2];
          WHSN[LD1]=WHSN[LD1]+EAJL[WHSN[ISL],W2];
          WBMN[LD1]=WBMN[LD1]+EAJL[WBMN[ISL],W2];
          WLSN[LD1]=WLSN[LD1]+EAJL[WLSN[ISL],W2];
          WLMN[LD1]=WLMN[LD1]+EAJL[WLMN[ISL],W2];
          WHPC[LD1]=WHPC[LD1]+EAJL[WHPC[ISL],W2];
          WHSC[LD1]=WHSC[LD1]+EAJL[WHSC[ISL],W2];
          WBMC[LD1]=WBMC[LD1]+EAJL[WBMC[ISL],W2];
          WLS[LD1]=WLS[LD1]+EAJL[WLS[ISL],W2];
          WLM[LD1]=WLM[LD1]+EAJL[WLM[ISL],W2];
          WLSL[LD1]=WLSL[LD1]+EAJL[WLSL[ISL],W2];
          WLSC[LD1]=WLSC[LD1]+EAJL[WLSC[ISL],W2];
          WLMC[LD1]=WLMC[LD1]+EAJL[WLMC[ISL],W2];
          WLSLC[LD1]=WLSLC[LD1]+EAJL[WLSLC[ISL],W2];
          WLSLNC[LD1]=WLSC[LD1]-WLSLC[LD1];
          WP[LD1]=WP[LD1]+EAJL[WP[ISL],W2];
          RSD[LD1]=.001*[WLS[LD1]+WLM[LD1]];
          AP[LD1]=AP[LD1]+EAJL[AP[ISL],W2];
          PMN[LD1]=PMN[LD1]+EAJL[PMN[ISL],W2];
          FOP[LD1]=FOP[LD1]+EAJL[FOP[ISL],W2];
          OP[LD1]=OP[LD1]+EAJL[OP[ISL],W2];
          SOLK[LD1]=SOLK[LD1]+EAJL[SOLK[ISL],W2];
          EXCK[LD1]=EXCK[LD1]+EAJL[EXCK[ISL],W2];
          FIXK[LD1]=FIXK[LD1]+EAJL[FIXK[ISL],W2];
          ST[LD1]=ST[LD1]+EAJL[ST[ISL],W2];
          double XY=.01*[1.-.01*ROK[LD1]];
          XX=XY;
          WT[LD1]=BD[LD1]*100.;
          ABD=WT[LD1];
          for(int J = 2; J < NBSL; J++)
          {
          DO J=2,NBSL
              ISL=LID[J];
              Z[ISL]=Z[ISL]-TK;
              ABD=ABD+WT[ISL];
          } 
          XX=Z[LID[NBSL]];
          if(XX<=ZF)           
              JRT=1;
              return;
          if(BIG>=XX)
              BIG=XX;
              PMX=XX;
              ABD=ABD*1.E-4/Z[LID[NBSL]];
          NBCL=Z[LID[NBSL]]/DZ+1.;
          return;
		}
	}
}
