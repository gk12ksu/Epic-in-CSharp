using System;

namespace Epic
{
    public class WREAD
    {

        private static MODPARAM PARM = MODPARAM.Instance;

        public WREAD()
        {
            // EPICv0810
            // Translated by Emily Jordan

            //THIS FILE IS NOT FINISHED!!!!! NEEDS TRANSLATION ON THE READ/WRITE COMMANDS

            //See WDOP for more on the weather reading/writing. I think it might writing 
            //the data this one reads.

            //Modified by Paul Cain on 7/30/2012 to fix build errors.

            //THIS SUBPROGRAM READS THE DAILY WEATHER FILE TO THE DAY BEFORE THE
            //SIMULATION BEGINS.


            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            // USE PARM
            double[] MOFD = new double[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }; //This array holds how many days are in a month
            double[] XTP = new double[7];
            int I1 = 0, I2 = 0, I3 = 0;
            // READ(KR(7),2)I3,I2,I1,(XTP(L),L=1,7)

            int J3 = 10000 * I3; //I3 gets it's data from the read statement that I dont' have implemented.
            int J1 = 100 * I2 + J3; //I2 also gets its data from that read statement.
            int II = I1 + J1; //I1 is again from the read above.
            int K1 = I2; //I2 is from the same boat.

            int NT = 0; //Created NT up here, as it is given value later on via a call to SLPYR

            if (II >= PARM.IBDT)
            {
                PARM.IBDT = II;
                PARM.IYR0 = I3;
                //REWIND KR(7)   This one is for rewinding a file back to the beginning. Not certain what the c# equivalent is.
            }
            else //THIS ELSE HAS MANY DO statements that need work done
            {
                //DO
                new ALPYR(ref I3, ref NT, ref PARM.LPYR);
                //DO I2 = K1, 12
                double N1 = MOFD[I2];
                if (I2 == 2) N1 = N1 - NT;
                int J2 = 100 * I2;
                J1 = J2 + J3;
                do
                {
                    I1 = I1 + 1;
                    II = J1 + I1;
                    if (II == PARM.IBDT) return;
                    double NFL = 0; //Create it here so that the read can use and assign to it.
                    //READ(KR(7),1,IOSTAT=NFL)(XTP(L),L=1,7)
                    if (NFL != 0)
                    {
                        //Write statement that will need to be converted to c#
                        //WRITE(KW(MSO),'(T10,A)')'START DATE EXCEEDS&
                        //& WEATHER FILE--WEATHER GENERATED'
                        PARM.NGN = 0;
                        return;
                    }

                } while (I1 < N1);

                K1 = 1;
                I3 = I3 + 1;
                J3 = 10000 * I3;


            }

            return;
            //These are setting formats for something. Still don't know enough FORTRAN to hazard a guess :P
            //1 FORMAT(14X,7F6.0)
            //2 FORMAT(2X,3I4,7F6.0)



        }
    }
}
