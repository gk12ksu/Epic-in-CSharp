using System;

namespace Epic
{
	public class ERSC
	{
        //EPICM0810
        //Translated by Heath Yates 
        //This subprogram estimates the rusle factor C daily 
        //Use parms
        //
		public ERSC ()
		{
            double[] NN = NBC[IRO];
            double SUM = 0;
            double TOT = 0;
			double FRUF; 
			double[] FRSD;
			double FBIO;
			double SLR;
			
            for(int I = 2; I < NBSL; I++)
            {
                double[] L = LID[I];
                if(Z[L] > PMX)
                {
                    
                    double[] KK = LID[I - 1];
                    double RTO = (PMX - Z[KK]) / (Z[L] - Z[KK]);
                    TOT = TOT + RTO * RSD[L];
                    for (int K; K < NN; K ++ )
                    {
                        if( JE[K] > MNC) 
                        {
                            continue;
                        }
                        SUM = SUM + RTO * RWT[L, JE[K]];
                    }
                    SUM = SUM / PMX; 
                    TOT = TOT / (PMX - Z[LD1]);
                    PLU = 0.951*RCF*Math.Exp(-0.0451 * SUM - 0.00943 * TOT / Math.Sqrt(RCF));
                    FRUF=Math.Min(1.0,Math.Exp(-.026*(RRUF-6.1)));
                    if( CVRS < 15.0) 
                    {
                         FRSD = Math.Exp(-PMRT[22]*CVRS); //Changed 23 to 22
                    }
                    /*
					else
                    {
                         FRSD;
                    }
                    */
                     FBIO = 1.0 - FGC*Math.Exp(-1*PRMT(25)*CHT(JJK)); //Converted 26 to 25
                     SLR = Math.Max(Math.Pow(10,-10),FRSD*FBIO*FRUF); //Converted 1.E-10 to Math.Pow(10,-10)
                    return; 
                 }
                 
                 if(I > 1)
                 {
                    TOT = TOT + RSD[L];
                 }

                 for(int K = 1; K < NN; K++) //Translated NN as an array, but makes no sense here as an array
                 {
                    SUM = SUM + RWT[L,JE[K]];
                 }
			}
                    SUM = SUM / PMX; 
                    TOT = TOT / (PMX - Z[LD1]);
                    PLU = 0.951*RCF*Math.Exp(-0.0451 * SUM - 0.00943 * TOT / Math.Sqrt(RCF));
                    FRUF=Math.Min(1.0, Math.Exp(-.026*(RRUF-6.1)));
                    if( CVRS < 15.0) 
                    {
                        FRSD = Math.Exp(-PMRT[22]*CVRS); //Changed 23 to 22
                    }
                    /*
					else
                    {
                        FRSD;
                    }
                    */
                    FBIO = 1.0 - FGC*Math.Exp(-PRMT(25)*CHT(JJK)); //Converted 26 to 25
                    SLR = Math.Max(Math.Pow(10,-10),FRSD*FBIO*FRUF); //Converted 1.E-10 to Math.Pow(10,-10)
                    return; 
            }
	}
}