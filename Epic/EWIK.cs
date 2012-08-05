using System;

namespace Epic
{
	public class EWIK
	{
		public EWIK ()
		{

         //     SUBROUTINE EWIK
         //     Translated by Heath Yates 
         //     THIS SUBPROGRAM ESTIMATES THE SOIL ERODIBILITY FACTOR FOR THE WIND
         //     EROSION EQ.
         //     USE PARM
              if(SAN[LD1]>85.0+.05*CLA[LD1]){
                  WK=1.0;
                  return;
              } 
              if(SAN[LD1]>70.0+CLA[LD1]){
                  WK=0.43;
                  return;
              } 
              if(SIL[LD1]>80.0 && CLA[LD1]<12.0){
                  WK=0.12;
                  return;
              } 
              if(CAC[LD1]>0.0){
                  if(SAN[LD1]<45.0 || CLA[LD1]<20.0 || SIL[LD1]>28.0){
                      WK=0.28;
                      return;
				  }
                  else{
                      WK=0.18;
                      return;
              }  
              if(CLA[LD1]<7.0){
                  if(SIL[LD1]<50.0){
                      WK=0.28;
                      return;
				  }
                  else{
                      WK=0.18;
                      return;
                  }
              }        
              if(CLA[LD1]<20.0){
                  if(SAN[LD1]>52.0){
                      WK=0.28;
                      return;
				  }
                  else{
                      WK=0.18;
                      return;
                  }
              }        
              if(CLA[LD1]<27.0){
                  if(SIL[LD1]<28.0){
                      WK=0.18;
                      return;
				  }
                  else{
                      WK=0.16;
                      return;
                  }
              }        
              if(CLA[LD1]<35.0 && SAN[LD1]<20.0){
                  WK=0.12;
                  return;
              }
              if(CLA[LD1]<35.0){
                  if(SAN[LD1]<45.0){        
                      WK=0.16;
                      return;
				  }
                  else{
                      WK=0.18;
                      return;
                  }
              }        
              if(SAN[LD1]>45.0){
                  WK=0.18;
                  return;
			  }
              else{
                  WK=0.28;
                  return;
              }
              return;
			}

		}
	}
}

