using System;

namespace Epic
{
	public class SLTB
	{
		public SLTB (ref double SLI, ref double SLF, ref double SLL, ref double SLS, ref double SLQ, ref double SLB, ref double SLE, ref double KW, ref double MSO)
		{
            // EPICv0810
		    // Translated by Brian Cain
            // This program is the salt balance

            //WRITE(KW(1),2)
            double DF = SLB+SLI+SLF-SLL-SLS-SLQ-SLE;
            double PER = 100.0*DF/(SLE+.0001);
            //WRITE(KW(1),1)PER,DF,SLB,SLI,SLF,SLL,SLS,SLQ,SLE
            return;
            /*1 FORMAT(5X,'PER =',E13.6,2X,'DF  =',E13.6,2X,'BTOT=',E13.6,2X,&
            &'IRR =',E13.6,2X,'FERT=',E13.6,2X,'PRK =',E13.6/5X,'SSF =',E13.6,&
            &2X,'Q   =',E13.6,2X,'FTOT=',E13.6)
            2 FORMAT(/T10,'SALT BALANCE')
            END*/
		}
	}
}

