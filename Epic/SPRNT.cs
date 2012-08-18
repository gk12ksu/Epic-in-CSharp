using System;

namespace Epic
{
	public partial class Functions
	{
		public static void SPRNT (ref double[] YTP)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program prepares soil data table for output, and
            // converts weights of materials to concentration
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
             * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
             *              and changed the namespace to Epic.
             */
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            YTP = new double[16];
            double XX = 0.0;
            YTP[1] = 0.0;
            YTP[2] = 0.0;
            YTP[3] = 0.0;
            YTP[4] = 0.0;
	        YTP[5] = 0.0;
            int J;
			double X1, X2, WT1, DG;
			int I;
            for (J = 0; J < PARM.NBSL; J++){
                I = PARM.LID[J];
                WT1 = PARM.WT[I]/1000.0;
                PARM.SOIL[0,I] = PARM.AP[I]/WT1;
                PARM.SOIL[1,I] = PARM.PMN[I]/WT1;
                PARM.SOIL[2,I] = PARM.OP[I]/WT1;
                PARM.SOIL[3,I] = PARM.WP[I]/WT1;
                PARM.SOIL[4,I] = PARM.WNO3[I]/WT1;
                PARM.SOIL[13,I] = PARM.SOLK[I]/WT1;
                PARM.SOIL[14,I] = PARM.EXCK[I]/WT1;
                PARM.SOIL[15,I] = PARM.FIXK[I]/WT1;
                PARM.SOIL[5,I] = PARM.WON[I]/WT1;
                PARM.SOIL[6,I] = .1*PARM.WOC[I]/PARM.WT[I];
                DG = (PARM.Z[I]-XX)*1000.0;
                X1 = PARM.ST[I]-PARM.S15[I];
                X2 = PARM.FC[I]-PARM.S15[I];
                PARM.SOIL[16,I] = X1/X2;
                PARM.SOIL[17,I] = X1;
                PARM.SOIL[18,I] = X2;
                PARM.ECND[I] = .15625*PARM.WSLT[I]/PARM.ST[I];
                PARM.SOIL[19,I] = PARM.S15[I]/DG;
                YTP[0] = YTP[1]+PARM.FC[I];
                PARM.SOIL[8,I] = PARM.FC[I]/DG;
                PARM.SOIL[7,I] = PARM.PO[I]/DG;
                PARM.SOIL[12,I] = PARM.BDD[I]*PARM.BD[I];
                YTP[1] = YTP[2]+PARM.ST[I];
                PARM.SOIL[11,I] = PARM.ST[I]/DG;
                YTP[2] = YTP[2]+PARM.S15[I];
                YTP[3] = YTP[3]+PARM.PO[I];
                YTP[4] = YTP[4]+PARM.WT[I];
                XX = PARM.Z[I];
            }
            XX = PARM.Z[PARM.LID[PARM.NBSL]]*1000.0;
            for (I = 0; I < 4; I++){
                YTP[I] = YTP[I]/XX;
            }
            return;
		}
	}
}

