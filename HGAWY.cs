using System;

namespace Epic
{
	public class HGAWY
	{
		public HGAWY (ref double A, ref double PT, ref double Q1, ref double RX)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program solves the green and AMPT infiltration EQ
            // iteratively to obtain result at DT/2
            
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double F1 = PT-PARM.QD;
            double ZI = PARM.SATK*(PARM.SCN/F1+1.0);
            Q1=0.0;
            if (RX <= ZI){
                // Removed Go To Statement
                PARM.QD = PARM.QD+Q1;
                //WRITE(KW(1),2)I,PARM.SATK,RX,ZI,F1,Q1,PT,PARM.QD
                return;
            }

            double QL = A*(RX-ZI)/RX;
            double GB = QL;
            double GL = 0.0;
            double FF = F1-GB;
            ZI = PARM.SATK*(PARM.SCN/FF+1.0);
            double QB = A*(RX-ZI)/RX;

            int I;
            double B2, B1, G1, Z1, QG, GQ;
            for (I = 0; I < 10; I++){
                B2 = (QL-QB)/(GL-GB);
                B1 = QL-B2*GL;
                G1 = B1/(1.0-B2);
                FF = F1-G1;
                ZI = PARM.SATK*(PARM.SCN/FF+1.0);
                Q1 = A*(RX-ZI)/RX;
                GQ = Q1-G1;

                if (Math.Abs(GQ/G1) < .001) break;

                if (GQ > 0.0){
                    GL = G1;
                    QL = Q1;
                }
                else{
                    GB = G1;
                    QB = Q1;
                }
            }
            PARM.QD = PARM.QD+Q1;
            //WRITE(KW(1),2)I,PARM.SATK,RX,ZI,F1,Q1,PT,PARM.QD
            return;
            // FORMAT....
		}
	}
}

