using System;

namespace Epic
{
	public partial class Functions
	{
		public static void SOCIOD (int KK)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program outputs the soil organic c and n variables daily
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
             * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
             *              and added paramaters.
             */
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
            //
           /*WRITE(KW(15),30)IYR,MO,KK
              WRITE(KW(15),2)(SID(LORG(PARM.LID(J))),J=1,PARM.NBSL),SID(16)
              WRITE(KW(15),3)(PARM.Z(PARM.LID(I)),I=1,PARM.NBSL)
              WRITE(KW(15),21)(SOIL(12,PARM.LID(I)),I=1,PARM.NBSL),RZSW
              WRITE(KW(15),27)(STMP(PARM.LID(I)),I=1,PARM.NBSL)
              WRITE(KW(15),28)(RSD(PARM.LID(I)),I=1,PARM.NBSL),TRSD
              WRITE(KW(15),1)(RSPC(PARM.LID(I)),I=1,PARM.NBSL),TRSP
              WRITE(KW(15),4)(RNMN(PARM.LID(I)),I=1,PARM.NBSL),SNMN
              RETURN
            1 FORMAT(T5,'CO2 LOSS(kg/ha)',T20,16F8.3)
            2 FORMAT(T52,'SOIL LAYER NO'/T18,16(4X,A4))
            3 FORMAT(T5,'DEPTH(m)',T20,16F8.2)
            4 FORMAT(T5,'NET MN(kg/ha)',T20,16F8.2)
           21 FORMAT(T5,'SW(m/m)',T20,16F8.3)
           27 FORMAT(T5,'TEMP(C)',T20,16F8.2)
           28 FORMAT(T5,'RSD(t/ha)',T20,16F8.2)
           30 FORMAT(//T10,3I4)
              END*/
            return;
		}
	}
}

