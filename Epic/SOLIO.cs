using System;

namespace Epic
{
	public partial class Functions
	{
		public static void SOLIO (ref double[] YTP, double L)
		{
			// EPICv0810
			// Translated by Brian Cain
            // This program outputs the soil organic c and n variables daily
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
             * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
             *              and made only the parameters that are modified be pass-by-reference.
             */
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			YTP = new double[16];
//		      WRITE(KW(L),2)(SID(LORG(PARM.LID(J))),J=1,PARM.NBSL),SID(16)
//		      WRITE(KW(L),3)'DEPTH(m)',(PARM.Z(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),4)'POROSITY(m/m)',(SOIL(8,PARM.LID(I)),I=1,PARM.NBSL),YTP(4)
//		      WRITE(KW(L),4)'FC SW(m/m)',(SOIL(9,PARM.LID(I)),I=1,PARM.NBSL),YTP(1)
//		      WRITE(KW(L),4)'WP SW(m/m)',(SOIL(20,PARM.LID(I)),I=1,PARM.NBSL),YTP(3)
//		      WRITE(KW(L),4)'SW(m/m)',(SOIL(12,PARM.LID(I)),I=1,PARM.NBSL),YTP(2)
//		      WRITE(KW(L),3)'SAT COND(mm/h)',(PARM.SATC(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),3)'H SC(mm/h)',(HCL(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),3)'BD 33kpa(t/m3)',(BD(PARM.LID(I)),I=1,PARM.NBSL),YTP(5)
//		      WRITE(KW(L),3)'BD DRY(t/m3)',(SOIL(13,PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'SAND(%)',(PARM.SAN(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'SILT(%)',(SIL(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'PARM.CLAY(%)',(PARM.CLA(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'ROCK(%)',(ROK(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'PH',(PH(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'SM BS(cmol/kg)',(SMB(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'CEC(cmol/kg)',(CEC(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'AL SAT(%)',(ALS(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),5)'CACO3(%)',(CAC(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),6)'LAB P(g/t)',(SOIL(1,PARM.LID(I)),I=1,PARM.NBSL),TAP
//		      WRITE(KW(L),3)'P SORP RTO',(PSP(PARM.LID(I)),I=1,PARM.NBSL)
//		      WRITE(KW(L),6)'MN P AC(g/t)',(SOIL(2,PARM.LID(I)),I=1,PARM.NBSL),TMP
//		      WRITE(KW(L),6)'MN P ST(g/t)',(SOIL(3,PARM.LID(I)),I=1,PARM.NBSL),TOP
//		      WRITE(KW(L),6)'ORG P(g/t)',(SOIL(4,PARM.LID(I)),I=1,PARM.NBSL),TP
//		      WRITE(KW(L),6)'NO3(g/t)',(SOIL(5,PARM.LID(I)),I=1,PARM.NBSL),TNO3
//		      WRITE(KW(L),6)'SOLK(g/t)',(SOIL(14,PARM.LID(I)),I=1,PARM.NBSL),TSK
//		      WRITE(KW(L),6)'EXCK(g/t)',(SOIL(15,PARM.LID(I)),I=1,PARM.NBSL),TEK
//		      WRITE(KW(L),6)'FIXK(g/t)',(SOIL(16,PARM.LID(I)),I=1,PARM.NBSL),TFK
//		      WRITE(KW(L),6)'ORG N(g/t)',(SOIL(6,PARM.LID(I)),I=1,PARM.NBSL),TWN
      		double X1 = .001*PARM.TOC;
//      WRITE(KW(L),3)'ORG C(%)',(SOIL(7,PARM.LID(I)),I=1,PARM.NBSL),X1
//      WRITE(KW(L),3)'CROP RSD(t/ha)',(RSD(PARM.LID(I)),I=1,PARM.NBSL),TRSD
//      WRITE(KW(L),5)'WLS(kg/ha)',(WLS(PARM.LID(I)),I=1,PARM.NBSL),ZLS
//      WRITE(KW(L),5)'WLM(kg/ha)',(WLM(PARM.LID(I)),I=1,PARM.NBSL),ZLM
//      WRITE(KW(L),5)'WLSL(kg/ha)',(WLSL(PARM.LID(I)),I=1,PARM.NBSL),ZLSL
//      WRITE(KW(L),5)'WLSC(kg/ha)',(WLSC(PARM.LID(I)),I=1,PARM.NBSL),ZLSC
//      WRITE(KW(L),5)'WLMC(kg/ha)',(WLMC(PARM.LID(I)),I=1,PARM.NBSL),ZLMC
//      WRITE(KW(L),5)'WLSLC(kg/ha)',(WLSLC(PARM.LID(I)),I=1,PARM.NBSL),ZLSLC
//      WRITE(KW(L),5)'WLSLNC(kg/ha)',(WLSLNC(PARM.LID(I)),I=1,PARM.NBSL),ZLSLNC
//      WRITE(KW(L),5)'WBMC(kg/ha)',(WBMC(PARM.LID(I)),I=1,PARM.NBSL),ZBMC
//      WRITE(KW(L),5)'WHSC(kg/ha)',(WHSC(PARM.LID(I)),I=1,PARM.NBSL),ZHSC
//      WRITE(KW(L),5)'WHPC(kg/ha)',(WHPC(PARM.LID(I)),I=1,PARM.NBSL),ZHPC
      		X1=.001*PARM.TOC;
//      WRITE(KW(L),5)'WOC(kg/ha)',(WOC(PARM.LID(I)),I=1,PARM.NBSL),X1
//      WRITE(KW(L),5)'WLSN(kg/ha)',(WLSN(PARM.LID(I)),I=1,PARM.NBSL),ZLSN
//      WRITE(KW(L),5)'WLMN(kg/ha)',(WLMN(PARM.LID(I)),I=1,PARM.NBSL),ZLMN
//      WRITE(KW(L),5)'WBMN(kg/ha)',(WBMN(PARM.LID(I)),I=1,PARM.NBSL),ZBMN
//      WRITE(KW(L),5)'WHSN(kg/ha)',(WHSN(PARM.LID(I)),I=1,PARM.NBSL),ZHSN
//      WRITE(KW(L),5)'WHPN(kg/ha)',(WHPN(PARM.LID(I)),I=1,PARM.NBSL),ZHPN
//      WRITE(KW(L),6)'WON(kg/ha)',(WON(PARM.LID(I)),I=1,PARM.NBSL),TWN
//      WRITE(KW(L),3)'ECND(mmho/cm)',(ECND(PARM.LID(I)),I=1,PARM.NBSL)
//      WRITE(KW(L),6)'WSLT(kg/ha)',(WSLT(PARM.LID(I)),I=1,PARM.NBSL),TSLT
//      WRITE(KW(L),4)'STFR',(STFR(PARM.LID(I)),I=1,PARM.NBSL)
//      WRITE(KW(L),4)'CGO2(kg/ha)',(CGO2(PARM.LID(I)),I=1,PARM.NBSL)
//      WRITE(KW(L),4)'CGCO2(kg/ha)',(CGCO2(PARM.LID(I)),I=1,PARM.NBSL)
//      WRITE(KW(L),4)'CGN2O(kg/ha)',(CGN2O(PARM.LID(I)),I=1,PARM.NBSL)
			return;
//    2 FORMAT(T52,'SOIL LAYER NO'/T22,16(4X,A4,4X))
//    3 FORMAT(4X,A15,1X,16F12.2)
//    4 FORMAT(4X,A15,1X,16F12.3)
//    5 FORMAT(4X,A15,1X,16F12.1)
//    6 FORMAT(4X,A15,1X,16F12.0)
//      END
		}
	}
}

