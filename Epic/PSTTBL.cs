using System;

namespace Epic
{
    public class PSTTBL
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public PSTTBL()
        {
            // EPIC0810
            // Translated by Paul Cain
            // THIS SUBPROGRAM READS PESTICIDE TABLE TO DETERMINE PESTICIDE
            // PARAMETERS

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            // USE PARM

            //Translator's Note: For now, I kept all of the array indices as 
            //	the same numbers they were in their original fortran files  
            //	even though C# use base 0 arrays and fortran use base 1 
            //	arrays. I did this to avoid confusion until we have a better
            // 	understanding of how the program works. 
            double[] XTP = new double[8];
            if (PARM.NDP > 0)
            {
                for (int L = 1; L <= PARM.NDP; L++)
                {
                    if (PARM.KDP[L] == PARM.JX[7])
                    {
                        return;
                    }
                }
            }
            PARM.NDP = PARM.NDP + 1;
            PARM.KDP[PARM.NDP] = PARM.JX[7];
            PARM.KDP1[PARM.JX[7]] = PARM.NDP;
            //!     READ PESTICIDE TABLE
            //!  1  PSTN = PESTICIDE NAME
            //!  2  PSOL = PESTICIDE SOLUBILITY (ppm)
            //!  3  PHLS = PESTICIDE HALF LIFE IN SOIL (d)
            //!  4  PHLF = PESTICIDE HALF LIFE ON FOLIAGE (d)
            //!  5  PWOF = PESTICIDE WASH OFF FRACTION
            //!  6  PKOC = PESTICIDE ORGANIC C ADSORPTION COEF
            //!  7  PCST = PESTICIDE COST ($/KG)
            //!  8  PCEM = C EMMISSION/UNIT PESTICIDE(G/G)
            int J1 = -1;

            while (J1 != PARM.JX[7])
            {
                int NFL=0;
                // This reads from a file somewhere but I'm am not sure 
                //	where so I commented it out
                //Fortran: READ(KR(8),1,IOSTAT=NFL)J1,PSTN(NDP),(XTP(L),L=2,8)
                if (NFL != 0)
                {
                    if (PARM.IBAT == 0)
                    {
                        // Translator's Note:
                        // I think this part display a message to the console
                        // then pauses the program until the user resumes it.
                        // See: http://ram3.chem.sunysb.edu/nucwww/helplib/@help/FORTRAN/STATEMENTS/PAUSE.html
                        // Fortran:
                        // WRITE(*,*)'PEST NO = ',JX(7),' NOT IN PEST LIST FILE'
                        // PAUSE
                        //My version of the two lines above:
                        Console.WriteLine("PEST NO = " + PARM.JX[7] + " NOT IN PEST LIST FILE");
                        Console.WriteLine("Press any key to resume");
                        Console.ReadLine();
                    }
                    else
                    {
                        // Traslator's Note: This write statement appears to write 
                        //  to a file, but I'm not sure where. I am therefore
                        //  commenting it out for now.
                        // Fortran: WRITE(KW(MSO),'(A,A8,A,I4,A)')'!!!!! ',ASTN,' PEST NO = ',&
                        //	&JX(7),' NOT IN PEST LIST FILE'
                    }
                    Environment.Exit(1);
                }
            }
            PARM.PSOL[PARM.NDP] = XTP[2];
            PARM.PHLS[PARM.NDP] = XTP[3];
            PARM.PHLF[PARM.NDP] = XTP[4];
            PARM.PWOF[PARM.NDP] = XTP[5];
            PARM.PKOC[PARM.NDP] = XTP[6];
            PARM.PCST[PARM.NDP] = XTP[7];
            PARM.PCEM[PARM.NDP] = XTP[8];
            //The REWIND statement Rewinds the file KR(8) back the beginning.
            //	I don't how to handle it so I commented it out.
            //Fortran: REWIND KR(8)

            //Translator's Note: This format does not seem to be used 
            //anywhere in the file so I commented it out.
            //Fortran: 1 FORMAT(I5,1X,A16,F12.0,3F8.0,F10.0,2F8.0)    
        }
    }
}

