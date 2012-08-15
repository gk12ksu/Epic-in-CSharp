using System;

namespace Epic
{
	public class WIGV
	{
		public WIGV ()
		{
			// EPICv0810
			// Translated by Emily Jordan
			
						
			// THIS SUBPROGRAM WRITES THE NAMES OF THE INPUT WEATHER VARIABLES.
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			// USE PARM
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

			
			int K = 1; //Might be a local variable. Made an int for the switch statement.
			int N1 = PARM.NGN; //NI might be a local variable. Made an int for the switch statement.
			double N2; //Added in the creation of N2 as otherwise it would be used without being declared later in the AISPL call.
			
			//WRITE(KW(1),673)FWTH
			
			for(int J = 4; J >= 1; J--)
			{
				Epic.AISPL(N1, N2); //I think this is what it wanted for CALL AISPL(N1,N2) 
				if(N1 == 0) 
				{
					switch(K)
					{
						case 1:
							//here's the format for the next write line   222 FORMAT(/T10,'**********RAIN IS INPUT**********')
							//WRITE(KW(1),222)
						case 2:
							//here's the format line for this write line  356 FORMAT(/T10,'**********RAIN,',1X,A4,', ARE INPUT**********')
							//WRITE(KW(1),356)HED(KDT2(1))
						case 3:
							//here's the format line 357 FORMAT(/T10,'**********RAIN,',2(1X,A4,','),' ARE INPUT**********')
							//WRITE(KW(1),357)(HED(KDT2(J)),J=1,2)
						case 4:
							//Here's the format line 358 FORMAT(/T10,'**********RAIN,',3(1X,A4,','),' ARE INPUT**********')
							//WRITE(KW(1),358)(HED(KDT2(J)),J=1,3)
						case 5:
							//Here's the format line  359 FORMAT(/T10,'**********RAIN,',4(1X,A4,','),' ARE INPUT**********')
							//WRITE(KW(1),359)(HED(KDT2(J)),J=1,4)
					
					}
					break;
				}
				
				KGN[N1 - 1] = 1;
				switch(N1)
				{
					case 1:
						N1 = N2;
						break;
					case 2:
						PARM.KDT2[K - 1] = 67;
					case 3:
						PARM.KDT2[K - 1] = 3;
					case 4:
						PARM.KDT2[K - 1] = 7;
					case 5:
                        PARM.KDT2[K - 1] = 8;
					default:
				}
				
				K = K + 1;
				N1 = N2;

			}
			
			switch(K)
					{
						case 1:
							//here's the format for the next write line   222 FORMAT(/T10,'**********RAIN IS INPUT**********')
							//WRITE(KW(1),222)
						case 2:
							//here's the format line for this write line  356 FORMAT(/T10,'**********RAIN,',1X,A4,', ARE INPUT**********')
							//WRITE(KW(1),356)HED(KDT2(1))
						case 3:
							//here's the format line 357 FORMAT(/T10,'**********RAIN,',2(1X,A4,','),' ARE INPUT**********')
							//WRITE(KW(1),357)(HED(KDT2(J)),J=1,2)
						case 4:
							//Here's the format line 358 FORMAT(/T10,'**********RAIN,',3(1X,A4,','),' ARE INPUT**********')
							//WRITE(KW(1),358)(HED(KDT2(J)),J=1,3)
						case 5:
							//Here's the format line  359 FORMAT(/T10,'**********RAIN,',4(1X,A4,','),' ARE INPUT**********')
							//WRITE(KW(1),359)(HED(KDT2(J)),J=1,4)
					
					}

			return;		
		}
	}
}
