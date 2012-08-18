using System;

namespace Epic
{
	public partial class Functions
	{
		public static void SOCIOA (int IYR1,int MZ,int KK)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program outputs the soil organic c and n variables
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
             * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
             *              and added paramaters.
             */
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			/*WRITE(KW(14),30)IYR1,MZ,KK,CO2
		      WRITE(KW(14),2)(SID(LORG(PARM.LID(J))),J=1,PARM.NBSL),SID(16)
		      WRITE(KW(14),3)'DEPTH(m)',(PARM.Z(PARM.LID(I)),I=1,PARM.NBSL)
		      WRITE(KW(14),3)'BD 33kpa(t/m3)',(BD(PARM.LID(I)),I=1,PARM.NBSL)
		      WRITE(KW(14),1)'SAND(%)',(PARM.SAN(PARM.LID(I)),I=1,PARM.NBSL)
		      WRITE(KW(14),1)'SILT(%)',(SIL(PARM.LID(I)),I=1,PARM.NBSL)
		      WRITE(KW(14),1)'PARM.CLAY(%)',(PARM.CLA(PARM.LID(I)),I=1,PARM.NBSL)
		      WRITE(KW(14),1)'ROCK(%)',(ROK(PARM.LID(I)),I=1,PARM.NBSL)
		      WRITE(KW(14),1)'WLS(kg/ha)',(WLS(PARM.LID(I)),I=1,PARM.NBSL),ZLS
		      WRITE(KW(14),1)'WLM(kg/ha)',(WLM(PARM.LID(I)),I=1,PARM.NBSL),ZLM
		      WRITE(KW(14),1)'WLSL(kg/ha)',(WLSL(PARM.LID(I)),I=1,PARM.NBSL),ZLSL
		      WRITE(KW(14),1)'WLSC(kg/ha)',(WLSC(PARM.LID(I)),I=1,PARM.NBSL),ZLSC
		      WRITE(KW(14),1)'WLMC(kg/ha)',(WLMC(PARM.LID(I)),I=1,PARM.NBSL),ZLMC
		      WRITE(KW(14),1)'WLSLC(kg/ha)',(WLSLC(PARM.LID(I)),I=1,PARM.NBSL),ZLSLC
		      WRITE(KW(14),1)'WLSLNC(kg/ha)',(WLSLNC(PARM.LID(I)),I=1,PARM.NBSL),ZLSLNC
		      WRITE(KW(14),1)'WBMC(kg/ha)',(WBMC(PARM.LID(I)),I=1,PARM.NBSL),ZBMC
		      WRITE(KW(14),4)'WHSC(kg/ha)',(WHSC(PARM.LID(I)),I=1,PARM.NBSL),ZHSC
		      WRITE(KW(14),4)'WHPC(kg/ha)',(WHPC(PARM.LID(I)),I=1,PARM.NBSL),ZHPC
		      X1=.001*TOC
		      WRITE(KW(14),4)'WOC(kg/ha)',(WOC(PARM.LID(I)),I=1,PARM.NBSL),X1
		      WRITE(KW(14),1)'WLSN(kg/ha)',(WLSN(PARM.LID(I)),I=1,PARM.NBSL),ZLSN
		      WRITE(KW(14),1)'WLMN(kg/ha)',(WLMN(PARM.LID(I)),I=1,PARM.NBSL),ZLMN
		      WRITE(KW(14),1)'WBMN(kg/ha)',(WBMN(PARM.LID(I)),I=1,PARM.NBSL),ZBMN
		      WRITE(KW(14),4)'WHSN(kg/ha)',(WHSN(PARM.LID(I)),I=1,PARM.NBSL),ZHSN
		      WRITE(KW(14),4)'WHPN(kg/ha)',(WHPN(PARM.LID(I)),I=1,PARM.NBSL),ZHPN
		      WRITE(KW(14),4)'WON(kg/ha)',(WON(PARM.LID(I)),I=1,PARM.NBSL),TWN
		      WRITE(KW(14),4)'CFEM(kg/ha)',SMY(96)
		      RETURN
		    1 FORMAT(1X,A14,16F12.1)
		    2 FORMAT(T52,'SOIL LAYER NO'/T18,16(4X,A4,4X))
		    3 FORMAT(1X,A14,16F12.2)
		    4 FORMAT(1X,A14,16F12.0)
		   30 FORMAT(//T10,3I4,5X,'ATMOS CO2 =',F5.0,' ppm')
      		END*/
			return;
		}
	}
}

