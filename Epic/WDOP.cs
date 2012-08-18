using System;

namespace Epic
{
	public partial class Functions
	{
        private static MODPARAM PARM = MODPARAM.Instance;

		public static void WDOP (ref double ID)
		{
			// EPICv0810
			// Translated by Emily Jordan

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

            /* ADDITIONAL CHANGE
           * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
           */
			
			//THIS FILE IS NOT FINISHED!!!!! NEEDS TRANSLATION ON THE READ/WRITE COMMANDS
			//This program is reading information from a weather report, then writing the
			//info it needs to another section. Lots of read write commands that are going
			//to need to be updated to C#, and some error handling that will need to be 
			//updated in order to be useful. Left it mostly commented out.
			
			//See WREAD for more on the weather reading/writing. I think it might be working
			//with the data this one writes.
			
			// THIS SUBPROGRAM READS THE DAILY WEATHER LIST AND LOCATES THE 
			// SPECIFIED STATION (IWTH) OR THE NEAREST STATION IF IWTH=0.
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			// USE PARM

			double NFL=0; //Needed to be created before the READ statement gives it value.
			double Y=0;
			double X=0;
			double ELEX=0; 
			
			if(PARM.IWTH == 0)
			{
				//double W0 = 1.E20;
                double W0 = 1 * Math.Pow(10, 20);
				while (true){
					//READ(KR(27),*,IOSTAT=NFL)II,OPSCFILE,Y,X,ELEX
                    if (NFL != 0) break;
					double RY = Y / PARM.CLT;
					double XX = Math.Sin(1) * Math.Sin(RY) + Math.Cos(1) * Math.Cos(RY) * Math.Cos((X-PARM.XLOG) / PARM.CLT);
					double D = 6378.8 * Math.Acos(XX);
					double E = Math.Abs(PARM.ELEV - ELEX);
					double W1 = PARM.PRMT[78] * D  + (1.0 - PARM.PRMT[78]) * E; //TPRMT is an array.   W1=PRMT(79)*D+(1.-PRMT(79))*E   
					if(W1 >= W0) continue;
					W0 = W1;
					PARM.FWTH = PARM.OPSCFILE; //Might be problems with this line. It's setting a char array equal to another char array that is a differnt size.
					//OPSCFILE is size 20 char array, FWTH is size 80 char array.
				}
			}
			else
			{
				double II = -1;
				do{
					//READ(KR(27),*,IOSTAT=NFL)II,FWTH
					if(NFL != 0){
						if(PARM.IBAT == 0){
						//WRITE(*,*)'FWTH NO = ',IWTH,' NOT IN DAILY WEATHER LIST FILE'
						//PAUSE
						//As far as I can tell, this block was to show an error message if the weather file
						//wasn't where it was supposed to be. Might want to change over to a windows error
						//message instead of this old code.
						}
						else
						{
							//Good luck and god speed with this section. Hopefully whoever works on this
							//next knows moar Fortran than I do, because I'm not entirely certain what this
							//is doing other than writing stuff to a file somewhere.
							//WRITE(KW(MSO),'(A,A8,A,I4,A)')'!!!!! ',ASTN,' FWTH NO = ',&
							//&IWTH,' NOT IN DAILY WEATHER LIST FILE'
						}
						break; //Code said STOP, guessing they just want to break out of the top if
					}
	              
            
				
				}while(II != PARM.IWTH);
					
					
					//Moar commands that need translating into c# read/write.
				//REWIND KR(27)
				//CALL OPENV(KR(7),FWTH,ID,KW(MSO))
				//CALL WREAD

			}
		}
	}
}

    
    