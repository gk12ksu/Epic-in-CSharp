using System;

namespace Epic
{
	public class HSWBL
	{
		public HSWBL (ref double P, ref double Q, ref double ET, ref double SSF, ref double O, ref double RG, ref double SNO, ref double SW, ref double SWW, ref double QINT, ref double RGDL, ref double[] KW, ref int MSO)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program checks the soil water balance at the end of
            // a simulation
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            KW = new double[MSO+3];
            //WRITE(KW(1),'(T10,A)')'SOIL WATER BALANCE'
            double DF = SWW+P-Q-ET-O-SW-SSF+RG-PARM.SNO+QINT-RGDL;
            double PER = 100.0*DF/SW;
            //WRITE(KW(1),1)PER,DF,SWW,P,Q,ET,O,SSF,RG,RGDL,PARM.SNO,QINT,SW
            SWW = SW+PARM.SNO;
            return;    
            //1 FORMAT(5X,'PER =',E13.6,2X,'DF  =',E13.6,2X,'BSW =',E13.6,2X,&
            //&'PCP =',E13.6,2X,'Q   =',E13.6,2X,'ET  =',E13.6/5X,'PRK =',E13.6,&
            //&2X,'SSF =',E13.6,2X,'IRG =',E13.6,2X,'IRDL=',E13.6,2X,'PARM.SNO =',&
            //&E13.6,2X,'QIN =',E13.6,2X/5X,'FSW =',E13.6)
		}
	}
}

