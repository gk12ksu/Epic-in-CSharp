using System;

namespace Epic
{
    public class WTAIX
    {
        public WTAIX()
        {
            //EPICv0810
            //Translated by Emily Jordan
            //Legends claim this file does, "THIS SUBPROGRAM GENERATES TMX AND TMN FOR MISSING RECORDS (999)"

            // The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			//Uses  MODPARAM file for globals.
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
           if(PARM.TMX < 100) PARM.TMN = Math.Min(PARM.TMNM + PARM.TNSD * PARM.WX[1], PARM.TMX - .2*Math.Abs(PARM.TMX));
           else PARM.TMX = Math.Max(PARM.TXXM + PARM.TXSD * PARM.WX[0], PARM.TMN + .2 * MATH.Abs(PARM.TMN));

           return;
   
      
           

        }
    }
}