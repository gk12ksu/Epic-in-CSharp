using System;

namespace Epic
{
	public class WGN
	{
		public WGN ()
		{
			// EPICv0810
			// Translated by Emily Jordan
			
            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */
						
			// THIS SUBPROGRAM SIMULATES DAILY PRECIPITATION, MAXIMUM AND MINUMUM
			// AIR TEMPERATURE, SOLAR RADIATION AND RELATIVE HUMIDITY.  ALSO
			// ALSO PROVIDES AND OPTIONS TO SIMULATE VARIOUS COMBINATIONS
			// GIVEN DAILY PRECIPITATION.
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			// USE PARM
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			double[,] A = new double [,] { {.594,.454,-.004} , {.076,.261,-.037} , {-.018,-.129,.222}};
			double[,] B = new double [,] { { .767,.304,.274 } , {0.0,.692,-.33} ,{0.0,0.0,0.873}};
			double[] XX = new double[3];
			double[] E = new double[3];
			
			double XXX = .5 * (PARM.TMXM - PARM.TMNM); //Did not find XXX in the modparam file.
			PARM.III = 1; //III was in the modparam.cs file
			double Z2 = PARM.WFT[PARM.NWI - 1, PARM.MO - 1]; //WFT is an array.
			double YY = .9 * Z2; //Did not find YY in modparam
			PARM.TXXM = PARM.TMXM + XXX * Z2; //Did find TXXM in modparam
			
			PARM.RHM = (PARM.RH[PARM.NWI - 1, PARM.MO - 1] - YY) / (1.0 - YY); //RH, RHM, and NWI were in modparam. Did the standard -1 for teh array.
			
			if(PARM.RHM < .05) PARM.RHM = .5 * PARM.RH[PARM.NWI - 1, PARM.MO - 1];
			PARM.RM = PARM.SRAM / (1.0 - .25 * Z2);
			
			if(PARM.RFV > 0.0)
			{
				PARM.TXXM = PARM.TXXM - XXX;
				PARM.RM = .75 * PARM.RM;
				PARM.RHM = PARM.RHM * 0.1 + 0.9;			
			}
			
			for(int I = 1; I <= 3; I++)
			{
				double V2 = Functions.AUNIF(PARM.IDG[1]);
				E[I-1] = Functions.ADSTN(ref PARM.V1, ref V2);
				PARM.V1 = V2;			
			}
			
			for(int I = 1; I <= 3; I++)
			{
				PARM.WX[I - 1] = 0.0;
				XX[I - 1] = 0.0;
				for(int J = 1; J <= 3; J++)
				{
					PARM.WX[I - 1] = PARM.WX[I - 1] + B[I - 1,J - 1] * E[J - 1]; //WX is global array, others were created in here.
					XX[I - 1] = XX[I - 1] + A[I - 1,J - 1] * PARM.XIM[J - 1]; //I know XIM is a global array. 
				}
			
			}
			
			for(int I = 1; I <= 3; I++)
			{
				PARM.WX[I - 1] = PARM.WX[I - 1] + XX[I - 1];
				PARM.XIM[I - 1] = PARM.WX[I - 1];		
			}
			

			return;		
		}
	}
}

