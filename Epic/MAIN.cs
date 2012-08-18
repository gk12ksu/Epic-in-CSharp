using System;
using System.IO;
namespace Epic
{
    /*
     *
     * EPICv0810
     * Translated by Brian Dye
     * THIS MODEL IS CALLED EPIC (ENVIRONMENTAL POLICY / INTEGRATED                   
     * CLIMATE). 
     * IT IS A COMPREHENSIVE AGRICULTURAL MANAGEMENT MODEL THAT IS USEFUL             
     * IN SOLVING A VARIETY OF PROBLEMS INVOLVING SUSTAINABLE                         
     * AGRICULTURE, WATER QUALITY, WATER SUPPLY, AND GLOBAL CLIMATE                   
     * & CO2 CHANGE.  THE MODEL SIMULATES MOST COMMONLY USED                          
     * MANAGEMENT PRACTICES LIKE TILLAGE, IRRIGATION, FERTILIZATION,                  
     * PESTICIDE APPLICATION, LIMING, AND FURROW DIKING.                              
     * THE MAIN PROGRAM INITIALIZES VARIABLES, ACCUMULATES                            
     * MONTHLY AND ANNUAL VALUES, AND COMPUTES AVERAGE VALUES FOR THE                 
     * SIMULATION PERIOD. 
     * EPIC0810 COMPLETE 20081001  
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/23/2012
     * 
     * Modified by Paul Cain on 8/16/2012 and 8/17/2012 to fix build errors.
     */
    public class MAIN
    {
        static void Main(string[] args)
        {
            int NFL = 0; //Remove this when the file I/O has been finished
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            //char[] ANM = new char[4];//AGIX = new char[8];
            string AGIX, ANM;
            string A1, A2;
            char[] FCMOD = new char[20], FCROP = new char[20], FFERT = new char[20];
            char[] FMLRN = new char[20], FOPSC = new char[20], FPARM = new char[20], FPEST = new char[20], FPRNT = new char[20];
            char[] FSITE = new char[20], FSOIL = new char[20], FTILL = new char[20], FTR55 = new char[20], FWIDX = new char[20];
            char[] FWIND = new char[20], FWLST = new char[20], FWPM1 = new char[20], FWPM5 = new char[20], VOID = new char[20];
            //char[] WINDFILE = new char[20], WPM1FILE = new char[20];
            string WINDFILE, WPM1FILE;
            //char[] AGSM = new char[80];
            string AGSM;
            int ISIT = 0, INPS = 0, IOPS = 0;

            char[] AYR = new char[150], TITW5 = new char[2];//CM = new char[9];
            string[] CM = new string[9];
            double[,] NXX = new double[3, 5], NYY = new double[3, 5];
            double[] IIX = new double[7], IDIR = new double[4], NY = new double[3];
            int[] JZ = new int[4];
            double[,] XZP = new double[13, 16], SCRX = new double[30, 2];
            double[] XTP = new double[200], XYP = new double[200], ACO2 = new double[30], XTP1 = new double[30], XTP2 = new double[30], YTP = new double[16], UAV0 = new double[12], COCH = new double[6], CSTZ = new double[2];
            double[] PPX = new double[13];
            string[] RFPT = { " 1", "1A", " 2", " 3" };
            char[] ASG = { 'A', 'B', 'C', 'D' };
            double[,] AKX = { { 1.0307, -3.0921, 2.0614 }, { -.040816, 4.1224, -4.0816, }, { .010101, -1.0303, 2.0202 } };
            Functions.ATIMER(0);
            Functions.AHEAD();

            double XM;
            double AAP;

            double RUN = 0;
            PARM.MSO = 30;
            int I, J, HSG;
            double X1, X2, X3, X4, XX;
            /*for (I = 1; I < PARM.KW.Length; I++)
            {
                PARM.KW[I - 1] = I + 50;
            }*/

            PARM.KW[PARM.MSO + 5] = new FileStream("WORKSPACE.DAT", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(PARM.KW[PARM.MSO + 5]);
            //OPEN(KW(MSO+6),FILE='WORKSPACE.DAT') *Opening WORKSPACE.DAT, with file ID KW(MSO+6) - Brian D
            //!1  IBAT = 0 RUNS FROM EPICRUN.DAT *Commented out in original file
            //!          > 0 RUNS BATCH MODE FROM RUNALL.BAT                                   	  
            //READ(KW(MSO+6),'(I4)')IBAT  Reading IBAT's value from WORKSPACE.DAT, IBAT is a global integer variable

            if (PARM.IBAT > 0)
            {
                //CALL OPENV(KW(MSO),'EPICERR.DAT         ',0,KW(MSO)) *Opening EPICERR.DAT with file ID KW(MSO) - Brian D
                I = 1;
                do
                {

                    //READ(KW(MSO),'(A80)',IOSTAT=NFL)TITW5
                    if (NFL != 0)
                        break;
                    I = I + 1;
                    //This loop either reads the last line of the file EPICERR.DAT or is just checking to see if EPICERR.DAT exists/is empty
                    // if its empty/nonexistant then I will be 1 and the following if statement seems to print a header to the file and to the screen. 
                } while (true);
                if (I == 1)
                {
                    //REWIND KW(MSO)
                    //WRITE(KW(MSO),621)IYER,IMON,IDAY,IT1,IT2,IT3                               
                    //WRITE(*,621)IYER,IMON,IDAY,IT1,IT2,IT3                               
                }
            }
            //CALL OPENV(KR(12),'EPICFILE.DAT        ',0,KW(MSO))                                            
            //CALL OPENV(KR(21),'EPICCONT.DAT        ',0,KW(MSO))                                            
            //CALL OPENV(KR(28),'AYEAR.DAT           ',0,KW(MSO))
            //READ(KR(12),509)FSITE,FWPM1,FWPM5,FWIND,FWIDX,FCROP,FTILL,FPEST,FFERT,FSOIL,FOPSC,FTR55,FPARM,FMLRN,FPRNT,FCMOD,FWLST
            // ^-- reading EPICFILE.DAT, each of these local variables are file names that will be opened later on


            /*
                  !  1  NBYR = NUMBER OF YEARS OF SIMULATION                                           
            !  2  IYR0 = BEGINNING YEAR OF SIMULATION                                            
            !  3  IMO0 = MONTH SIMULATION BEGINS                                                 
            !  4  IDA0 = DAY OF MONTH SIMULATION BEGINS                                          
            !  5  IPD  = N1 FOR ANNUAL PRINTOUT                     | N YEAR INTERVA             
            !          = N2 FOR ANNUAL WITH SOIL TABLE              | N=0 SAME AS                
            !          = N3 FOR MONTHLY                             | N=1 EXCEPT                 
            !          = N4 FOR MONTHLY WITH SOIL TABLE             | N=0 PRINTS                 
            !          = N5 FOR MONTHLY WITH SOIL TABLE AT HARVEST  | OPERATIONS                 
            !          = N6 FOR N DAY INTERVAL                                                   
            !          = N7 FOR SOIL TABLE ONLY N DAY INTERVAL                                   
            !          = N8 FOR N DAY INTERVAL RAINFALL DAYS ONLY                                
            !          = N9 FOR N DAY INTERVAL DURING GROWING SEASON                             
            !  6  NGN  = ID NUMBER OF WEATHER VARIABLES INPUT.  RAIN=1,  TEMP=2,                 
            !            RAD=3,  WIND SPEED=4,  REL HUM=5.  IF ANY VARIABLES ARE INP             
            !            RAIN MUST BE INCLUDED.  THUS, IT IS NOT NECESSARY TO SPECIF             
            !            ID=1 UNLESS RAIN IS THE ONLY INPUT VARIABLE.                            
            !            LEAVE BLANK IF ALL VARIABLES ARE GENERATED.  EXAMPLES                   
            !            NGN=1 INPUTS RAIN.                                                      
            !            NGN=23 INPUTS RAIN, TEMP, AND RAD.                                      
            !            NGN=2345 INPUTS ALL 5 VARIABLES.                                        
            !  7  IGN  = NUMBER TIMES RANDOM NUMBER GEN CYCLES BEFORE SIMULATION                 
            !            STARTS.                                                                 
            !  8  IGS0 = 0 FOR NORMAL OPERATION OF WEATHER MODEL.                                
            !          = N NO YRS INPUT WEATHER BEFORE REWINDING (USED FOR REAL                  
            !            TIME SIMULATION).                                                       
            !  9  LPYR = 0 IF LEAP YEAR IS CONSIDERED                                            
            !          = 1 IF LEAP YEAR IS IGNORED                                               
            ! 10  IET  = PET METHOD CODE                                                         
            !          = 1 FOR PENMAN-MONTEITH                                                   
            !          = 2 FOR PENMAN                                                            
            !          = 3 FOR PRIESTLEY-TAYLOR                                                  
            !          = 4 FOR HARGREAVES                                                        
            !          = 5 FOR BAIER-ROBERTSON                                                   
            ! 11  ISCN = 0 FOR STOCHASTIC CURVE NUMBER ESTIMATOR.                                
            !          > 0 FOR RIGID CURVE NUMBER ESTIMATOR.                                     
            ! 12  ITYP = 0 FOR MODIFIED RATIONAL EQ PEAK RATE ESTIMATE.                          
            !          > 0 FOR SCS TR55 PEAK RATE ESTIMATE.                                      
            !          = 1 FOR TYPE 1 RAINFALL PATTERN                                           
            !          = 2     TYPE 1A                                                           
            !          = 3     TYPE 2                                                            
            !          = 4     TYPE 3                                                            
            ! 13  ISTA = 0 FOR NORMAL EROSION OF SOIL PROFILE                                    
            !          = 1 FOR STATIC SOIL PROFILE                                               
            ! 14  IHUS = 0 FOR NORMAL OPERATION                                                  
            !          = 1 FOR AUTOMATIC HEAT UNIT SCHEDULE(PHU MUST BE INPUT AT                 
            !            PLANTING)                                                               
            ! 15  NDUM = NOT USED                                                                  
            ! 16  NVCN = 0 VARIABLE DAILY CN WITH DEPTH SOIL WATER WEIGHTING                     
            !          = 1 VARIABLE DAILY CN WITHOUT DEPTH WEIGHTING                             
            !          = 2 VARIABLE DAILY CN LINEAR CN/SW NO DEPTH WEIGHTING                     
            !          = 3 NON-VARYING CN--CN2 USED FOR ALL STORMS                               
            !          = 4 VARIABLE DAILY CN SMI(SOIL MOISTURE INDEX)                            
            ! 17  INFL = 0 FOR CN ESTIMATE OF Q                                                  
            !          = 1 FOR GREEN & AMPT ESTIMATE OF Q, RF EXP DST, PEAK RF RATE              
            !              SIMULATED.                                                            
            !          = 2 FOR G&A Q, RF EXP DST, PEAK RF INPUT                                  
            !          = 3 FOR G&A Q, RF UNIFORMLY DST, PEAK RF INPUT                            
            ! 18  MASP < 0 FOR MASS ONLY NO PESTICIDE IN .OUT                                    
            !          = 0 FOR MASS ONLY PESTICIDES IN .OUT                                      
            !          > 0 FOR PESTICIDE & NUTRIENT OUTPUT IN MASS & CONCENTRATION               
            ! 19  LBP  = 0 FOR SOL P RUNOFF ESTIMATE USING GLEAMS PESTICIDE APPROACH             
            !          > 0 FOR MODIFIED NONLINEAR APPROACH                                       
            ! 20  NSTP = REAL TIME DAY OF YEAR                                                   
            ! 21  IGMX = # TIMES GENERATOR SEEDS ARE INITIALIZED FOR A SITE.                     
            ! 22  IERT = 0 FOR EPIC ENRICHMENT RATIO METHOD                                      
            !          = 1 FOR GLEAMS ENRICHMENT RATIO METHOD                                    
            ! 23  ICG  = CROP GROWTH BIOMASS CONVERSION OPTION                                   
            !          = 0 FOR TRADITIONAL EPIC RADIATION TO BIOMASS                             
            !          > 0 FOR NEW EXPERIMENTAL WATER USE TO BIOMASS                             
            ! 24  LMS  = 0 APPLIES LIME                                                          
            !          = 1 DOES NOT APPLY LIME                                                   
            ! 25  ICF  = 0 USES RUSLE C FACTOR FOR ALL EROSION EQS                               
            !          > 0 USES EPIC C FACTOR FOR ALL EROSION EQS EXCEPT RUSLE                   
            ! 26  ISW  = 0 FIELD CAP/WILTING PT EST RAWLS METHOD DYNAMIC.                        
            !          = 1 FIELD CAP/WILTING PT EST BAUMER METHOD DYNAMIC.                       
            !          = 2 FIELD CAP/WILTING PT INP RAWLS METHOD DYNAMIC.                        
            !          = 3 FIELD CAP/WILTING PT INP BAUMER METHOD DYNAMIC.                       
            !          = 4 FIELD CAP/WILTING PT EST RAWLS METHOD STATIC.                         
            !          = 5 FIELD CAP/WILTING PT EST BAUMER METHOD STATIC.                        
            !          = 6 FIELD CAP/WILTING PT INP STATIC.                                      
            !          = 7 FIELD CAP/WILTING PT NEAREST NEIGHBOR DYNAMIC                         
            !          = 8 FIELD CAP/WILTING PT NEAREST NEIGHBOR STATIC                          
            ! 27  IRW  = 0 FOR NORMAL RUNS WITH DAILY WEATHER INPUT                              
            !          > 0 FOR CONTINUOUS DAILY WEATHER FROM RUN TO RUN(NO REWIND)               
            ! 28  ICO2 = 0 FOR CONSTANT ATMOSPHERIC CO2                                          
            !          = 1 FOR DYNAMIC ATMOSPHERIC CO2                                           
            !          = 2 FOR INPUTTING CO2                                                     
            ! 29  IDUM = 0 FOR READING DATA FROM WORKING DIRECTORY                               
            !          > 0 FOR READING FROM WEATDATA DIRECTORY                                   
            ! 30  ICOR = 0 FOR NORMAL RUN                                                        
            !          = DAY OF YEAR WHEN WEATHER CORRECTION TO SIMULATE INPUT MO MEANS          
            !            STOPS                                                                   
            ! 31  IDN  = 0 FOR CESAR IZAURRALDE DENITRIFICATION SUBPROGRAM.                         
            !          = 1 FOR ARMEN KEMANIAN DENITRIFICATION SUBPROGRAM                         
            !          = 2 FOR ORIGINAL EPIC DENITRIFICATION SUBPROGRAM                         
            ! 32  NUPC = N AND P PLANT UPTAKE CONCENTRATION CODE                                 
            !          = 0 FOR SMITH CURVE                                                       
            !          > 0 FOR S CURVE                                                           
            ! 33  IOX  = 0 FOR ORIGINAL EPIC OXYGEN/DEPTH FUNCTION                               
            !          > 0 FOR ARMEN KEMANIAN CARBON/CLAY FUNCTION
            ! 34  IDI0 = 0 FOR READING DATA FROM WORKING DIRECTORY
            !          = 1 FOR READING FROM WEATDATA DIRECTORY
            !          = 2 FOR READING FROM WORKING DIRECTORY PLUS 3 OTHER DIRECTORIES
            ! 35  ISAT = 0 FOR READING SATURATED CONDUCTIVITY IN SOIL FILE
            !          > 0 FOR COMPUTING SATURATED CONDUCTIVITY WITH RAWLS METHOD
            ! 36  IAZM = 0 FOR USING INPUT LATITUDES FOR SUBAREAS
            !          > 0 FOR COMPUTING EQUIVALENT LATITUDE BASED ON AZIMUTH 
            !            ORIENTATION OF LAND SLOPE.
            ! 37  IPAT = 0 TURNS OFF AUTO P APPLICATION
            !          > 0 FOR AUTO P APPLICATION
            !     LINE 1/2                                     
             */
            int ICOR=0, INFL0=0, IRW=0, IGMX=0;
            //READ(KR(21),300)NBYR,IYR0,IMO0,IDA0,IPD,NGN,IGN,IGS0,LPYR,IET,ISCN,ITYP,ISTA,IHUS,NDUM,NVCN,INFL0,MASP,LBP,NSTP,IGMX,IERT,ICG,LMS,ICF,ISW,IRW,ICO2,IDUM,ICOR,IDN,NUPC,IOX,IDI0,ISAT,IAZM,IPAT                                        
            // Reads from EPICCONT.DAT to give values to these global variables
            int IDI0=0;
            PARM.MYR = PARM.NBYR;

            switch (IDI0 + 1)
            {
                case 1:
                    for (int x = 0; x <= IDIR.Length; x++)
                        IDIR[x] = 0.0;
                    break;
                case 2:
                    for (int x = 0; x <= IDIR.Length; x++)
                        IDIR[x] = 1.0;
                    break;
                case 3:
                    for (I = 1; I < 4; I++)
                        IDIR[I - 1] = I;
                    break;
            }

            /*CALL OPENV(KR(6),FMLRN,0,KW(MSO)) Many files being opened
            CALL OPENV(KR(2),FPARM,0,KW(MSO))                                                           
            CALL OPENV(KR(5),FPRNT,0,KW(MSO))                                                      
            CALL OPENV(KR(26),FCMOD,0,KW(MSO))                                                          
            CALL OPENV(KR(3),FTILL,IDIR(1),KW(MSO))                                                     
            CALL OPENV(KR(4),FCROP,IDIR(1),KW(MSO))                                                     
            CALL OPENV(KR(8),FPEST,IDIR(1),KW(MSO))                                                     
            CALL OPENV(KR(9),FFERT,IDIR(1),KW(MSO))                                                
            CALL OPENV(KR(10),FTR55,IDIR(1),KW(MSO))                                               
            CALL OPENV(KR(13),FSOIL,IDIR(3),KW(MSO))                                               
            CALL OPENV(KR(15),FOPSC,IDIR(4),KW(MSO))                                               
            CALL OPENV(KR(17),FWPM1,IDIR(1),KW(MSO))                                                    
            CALL OPENV(KR(18),FWPM5,IDIR(1),KW(MSO))                                               
            CALL OPENV(KR(19),FWIND,IDIR(1),KW(MSO))                                                    
            CALL OPENV(KR(20),FWIDX,IDIR(1),KW(MSO))                                               
            CALL OPENV(KR(23),FSITE,IDIR(2),KW(MSO))                                                    
            CALL OPENV(KR(27),FWLST,IDIR(1),KW(MSO))*/
            if (PARM.IBAT > 0)
            {
                //Translator's note: the line below has been commented out 
                //  until we decide how to handle file IO
                //Functions.GETCL(CM, PARM.KW);
                PARM.ASTN = CM[1];
            }
            else
            {
                //CALL OPENV(KR(11),'EPICRUN.DAT         ',0,KW(MSO))                                      
            }
            /*
            !     SCRP = S CURVE SHAPE PARAMETERS (CONSTANTS EXCEPT FOR                          
            !            EXPERIMENTAL PURPOSES)                                                  
            !     LINE 1/30
             */
            //READ(KR(2),239)((SCRP(I,J),J=1,2),I=1,30)    **Reading from file FPARM
            /*                          
            !     MISCELLANEOUS PARAMETERS(CONSTANTS EXCEPT FOR EXPERIMENTAL PURPOSES            
            !     LINE 31/38                                                                     
                  READ(KR(2),303)PRMT                                                            
            !     READ ECONOMIC DATA                                                             
            !  1  COIR = COST OF IRRIGATION WATER ($/mm)                                         
            !  2  COL  = COST OF LIME ($/t)                                                      
            !  3  FULP = COST OF FUEL ($/L)                                                      
            !  4  WAGE = LABOR COST ($/H)                                                        
            !  5  CSTZ = MISCELLANEOUS COST ($/ha)                                               
            !  6  1/2                                                                            
            !     LINE 39 
             */
            //READ(KR(2),303)COIR,COL,FULP,WAGE,CSTZ(1),CSTZ(2)   **Reading the file FPARM            
            //CLOSE(KR(2))                                                                   

            for (I = 1; I <= 2; I++)
            {
                for (J = 1; J <= 30; J++)
                {
                    SCRX[J - 1, I - 1] = PARM.SCRP[J - 1, I - 1];
                }
            }


            for (I = 1; I <= 29; I++)
            {
                if (PARM.SCRP[I - 1, 0] < 1.0E-10) continue;
                X1 = Functions.ASPLT(ref PARM.SCRP[I - 1, 0]);
                X2 = Functions.ASPLT(ref PARM.SCRP[I - 1, 1]);
                Functions.ASCRV(ref PARM.SCRP[I - 1, 0], ref PARM.SCRP[I - 1, 1], X1, X2);
            }
            //!     CQP  = COEFS OF 7TH DEG POLY IN TR55 QP EST                                    
            //READ(KR(10),396)CQP               **reading file FTR55
            /*                                             
            !     COCH = COEFS FOR CHANNEL GEOMETRY X=COCH(N)*WSA**COCH(N+1)                     
            !            X=DEPTH FOR COCH(3) & COCH(4)                                           
            !            X=LENGTH FOR COCH(5) & COCH(6)               
            */
            //READ(KR(10),303)COCH   reading file FTR55                                                        
            //CLOSE(KR(10))                                                                  
            if (COCH[2] < 1.0E-10) COCH[2] = .0208;
            if (COCH[3] < 1.0E-10) COCH[3] = .4;
            if (COCH[4] < 1.0E-10) COCH[4] = .0803;
            if (COCH[5] < 1.0E-10) COCH[5] = .6;
            //READ(KR(28),*,IOSTAT=NFL)AYR            **Reading from file AYEAR.DAT                                                        
            //IF(NFL/=0)REWIND KR(28)                 **If we reached EOF of AYEAR rewind                                     
            //READ(KR(28),*,IOSTAT=NFL)IGY       

            //!     LINE 1/5                                                                       

            //IF(NFL/=0)READ(KR(5),300)(KDC1(I),I=1,NKA)   ** Reading from file FPRNT
            /*                             
            !     KA   = OUTPUT VARIABLE ID NO (ACCUMULATED AND AVERAGE VALUES)                  
            !     SELECT FROM THIS LIST:(UP TO 100 VARIABLES)                                     
            !      1  TMX,  2  TMN,  3  RAD,  4 PRCP,  5 SNOF,  6 SNOM,  7 WSPD,                 
            !      8 RHUM,  9  VPD, 10  PET, 11   ET, 12   EP, 13    Q, 14   CN,                 
            !     15  SSF, 16  PRK, 17 QDRN, 18 IRGA, 19  QIN, 20 TLGE, 21 TLGW,                 
            !     22 TLGQ, 23 TLGF, 24   EI, 25    C, 26 USLE, 27 MUSL, 28  AOF,                 
            !     29 MUSS, 30 MUST, 31 MUSI, 32  WK1, 33 RHTT, 34 RRUF, 35 RGRF,                 
            !     36   YW, 37  YON, 41  MNN, 42   DN, 43 NFIX, 44  HMN, 45 NITR,                 
            !     46 AVOL, 47 DRNN, 48   YP, 50  MNP, 51 PRKP, 52   ER, 53  FNO,                 
            !     54 FNO3, 55 FNH3, 56  FPO, 57  FPL, 58 LIME, 59  TMP, 66 SW10
            */


            for (I = 1; I <= PARM.NKA; I++)
            {
                if (PARM.KDC1[I - 1] <= 0) break;
                PARM.KA[I - 1] = PARM.KDC1[I - 1];
            }
            PARM.NKA = I - 1;
            /*                                                   
            !     JC = OUTPUT VARIABLE ID NO (CONCENTRATION VARIABLES)                           
            !     SELECT FROM THIS LIST: (UP TO 4 VARIABLES)                                     
            !     38 QNO3, 39 SSFN, 40 PRKN, 49  QAP                                             
            !     LINE 6   
            */
            //READ(KR(5),300)(KDC1(I),I=1,NJC)                **Reading file FPRNT
            for (I = 1; I <= PARM.NJC; I++)
            {
                if (PARM.KDC1[I - 1] <= 0) break;
                PARM.JC[I - 1] = PARM.KDC1[I - 1];
            }
            PARM.NJC = I - 1;
            /*                                                            
            !     KS = OUTPUT VARIABLE ID NO (MO STATE VARIABLES)                                
            !     SELECT FROM THIS LIST: (UP TO 27 VARIABLES)                                    
            !     1 TNH3, 2 TNO3, 3 PLAB, 4 UNO3, 5  UPP, 6 RZSW, 7 WTBL, 8 GWST,                
            !     9  STD, 10 RSD, 11 TOC, 12 SNOA                                                
            !     LINE 7/8 
            */
            //READ(KR(5),300)(KDC1(I),I=1,NKS)                 **Reading file FPRNT                                            

            for (I = 1; I <= PARM.NKS; I++)
            {
                if (PARM.KDC1[I - 1] <= 0) break;
                PARM.KS[I - 1] = PARM.KDC1[I - 1];
            }
            PARM.NKS = I - 1;
            /*                                                            
            !     KD = DAILY OUTPUT VARIABLE ID NO                                               
            !     SELECT FROM ACCUMULATED AND AVERAGE LIST (UP TO 40 VARIABLES)                  
            !     LINE 9/10
            */
            //READ(KR(5),300)(KDC1(I),I=1,NKD)                 **Reading file FPRNT                              

            for (I = 1; I <= PARM.NKD; I++)
            {
                if (PARM.KDC1[I - 1] <= 0) break;
                PARM.KD[I - 1] = PARM.KDC1[I - 1];
            }
            PARM.NKD = I - 1;
            /*                                                            
            !     KYA = ANNUAL OUTPUT TO FILE VARIABLE ID NOS(ACCUMULATED                        
            !     AND AVERAGE VALUES)                                                            
            !     SELECT FROM THE KA LIST ABOVE: (UP TO 40 VARIABLES)                            
            !     LINE 11/12 
             */
            //READ(KR(5),300)(KDC1(I),I=1,NKYA)               **Reading file FPRNT                                           

            for (I = 1; I <= PARM.NKYA; I++)
            {
                if (PARM.KDC1[I - 1] <= 0) break;
                PARM.KYA[I - 1] = PARM.KDC1[I - 1];
            }
            PARM.NKYA = I - 1;
            /*                                                         
            !     KFS = MONTHLY FLIPSIM VARIABLES.  SELECT FROM THE ACCUMULATED AND              
            !     AVERAGE LIST ABOVE (UP TO 40 VARIABLES)                                        
            !     LINE 13/14
             */
            //READ(KR(5),300)(KDC1(I),I=1,NFS)                **Reading file FPRNT                                

            for (I = 1; I <= PARM.NFS; I++)
            {
                if (PARM.KDC1[I - 1] <= 0) break;
                PARM.IFS[I - 1] = PARM.KDC1[I - 1];
            }
            PARM.NFS = I - 1;
            PARM.NGN0 = PARM.NGN;
            PARM.ICOR0 = ICOR;

            /*
            !  1  RFN0 = AVE CONC OF N IN RAINFALL (ppm)                                         
            !  2  CO20 = CO2 CONCENTRATION IN ATMOSPHERE (ppm)                                   
            !  3  CNO30= CONC OF NO3 IN IRRIGATION WATER (ppm)                                   
            !  4  CSLT=  CONC OF SALT IN IRRIGATION WATER (ppm)                                  
            !  5  PSTX = PEST DAMAGE SCALING FACTOR (0.-10.)--0. SHUTS OFF PEST                  
            !            DAMAGE FUNCTION. PEST DAMAGE FUNCTION CAN BE REGULATED FROM             
            !            VERY MILD(0.05-0.1) TO VERY SEVERE(1.-10.)                              
            !  6  YWI  = NO Y RECORD MAX .5H RAIN (BLANK IF WI IS NOT                            
            !            INPUT--LINE 19)                                                         
            !  7  BTA  = COEF (0-1)GOVERNING WET-DRY PROBABILITIES GIVEN DAYS                    
            !            OF RAIN (BLANK IF UNKNOWN OR IF W|D PROBS ARE                           
            !            INPUT--LINES 16 & 17)                                                   
            !  8  EXPK = PARAMETER USED TO MODIFY EXPONENTIAL RAINFALL AMOUNT                    
            !            DISTRIBUTION (BLANK IF UNKNOWN OR IF ST DEV & SK CF ARE                 
            !            INPUT--LINES 14 & 15)                                                   
            !  9  FL   = FIELD LENGTH (km)(BLANK IF UNKNOWN                                      
            ! 10  FW   = FIELD WIDTH (km)(BLANK IF UNKNOWN                                       
            ! 11  ANG0 = CLOCKWISE ANGLE OF FIELD LENGTH FROM NORTH (deg)(BLANK IF               
            !            UNKNOWN)                                                                
            ! 12  STD0 = STANDING DEAD CROP RESIDUE (t/ha)(BLANK IF UNKNOWN                      
            ! 13  UXP  = POWER PARAMETER OF MODIFIED EXP DIST OF WIND SPEED (BLANK               
            !            IF UNKNOWN)                                                             
            ! 14  DIAM = SOIL PARTICLE DIAMETER (um)(BLANK IF UNKNOWN                            
            ! 15  ACW  = WIND EROSION CONTROL FACTOR                                             
            !          = 0.0 NO WIND EROSION                                                     
            !          = 1.0 FOR NORMAL SIMULATION                                               
            !          > 1.0 ACCELERATES WIND EROSION (CONDENSES TIME)                           
            ! 16  BIR  = IRRIGATION TRIGGER--3 OPTIONS                                           
            !            1. PLANT WATER STRESS FACTOR (0-1)                                      
            !            2. SOIL WATER TENSION IN TOP 200 mm(> 1 kpa)                            
            !            3. PLANT AVAILABLE WATER DEFICIT IN ROOT ZONE (-mm)                     
            ! 17  EFI  = RUNOFF VOL / VOL IRR WATER APPLIED (BLANK IF IRR=0)                     
            ! 18  VIMX = MAXIMUM ANNUAL IRRIGATION VOLUME ALLOWED (mm)                           
            ! 19  ARMN = MINIMUM SINGLE APPLICATION VOLUME ALLOWED (mm)                          
            ! 20  ARMX = MAXIMUM SINGLE APPLICATION VOLUME ALLOWED (mm)                          
            ! 21  BFT0 = AUTO FERTILIZER TRIGGER--2 OPTIONS                                      
            !            1. PLANT N STRESS FACTOR (0-1)                                          
            !            2. SOIL N CONC IN ROOT ZONE (g/t)                                       
            ! 22  FNP  = FERT APPLICATION VARIABLE--2 MEANINGS                                   
            !            1. APPLICATION RATE AUTO/FIXED (kg/ha)                                  
            !            2. MANURE INPUT TO LAGOON (KG/COW/D) IRR=4                              
            ! 23  FMX  = MAXIMUM ANNUAL N FERTILIZER APPLICATION FOR A CROP(kg/ha)               
            ! 24  DRT  = TIME REQUIRED FOR DRAINAGE SYSTEM TO REDUCE PLANT STRESS(d)             
            !            (BLANK IF DRAINAGE NOT USED)                                            
            ! 25  FDS0 = FURROW DIKE SAFETY FACTOR (0-1.)                                        
            ! 26  PEC0 = CONSERVATION PRACTICE FACTOR(=0.0 ELIMINATES WATER EROSION)             
            ! 27  VLGN = LAGOON VOLUME RATIO--NORMAL / MAXIMUM                                   
            ! 28  COWW = LAGOON INPUT FROM WASH WATER (M**3/COW/D)                               
            ! 29  DDLG = TIME TO REDUCE LAGOON STORAGE FROM MAX TO NORM (d)                      
            ! 30  SOLQ = RATIO LIQUID/TOTAL MANURE APPLIED                                       
            ! 31  GZLM = ABOVE GROUND PLANT MATERIAL GRAZING LIMIT (t/ha)                        
            ! 32  FFED = FRACTION OF TIME HERD IS IN FEEDING AREA
            ! 33  DZ   = LAYER THICKNESS FOR DIFFERENTIAL EQ SOLN TO GAS DIFF EQS(m)                                
            ! 34  DRV  = SPECIFIES WATER EROSION DRIVING EQ.                                     
            !            (0=MUST;  1=AOF;  2=USLE;  3=MUSS;  4=MUSL;  5=MUSI;                    
            !             6=RUSLE;  7=RUSL2)
            ! 35  RST0 = BASE STOCKING RATE (ha/hd)                                                     
            ! 36  BUS  = INPUT PARMS FOR MUSI                                                    
            !            YSD(6)=BUS(1)*QD**BUS(2)*QP**BUS(3)*WSA**BUS(4)*KCPLS                   
            !     LINE 3/6
            */
            double RST0 = 0.0, ANG0 = 0.0, DIAM = 0.0, DRV = 0.0, YWI = 0.0, BTA = 0.0, PEC0 = 0.0, CO20 = 0.0, CNO30 = 0.0, RFN0 = 0.0, SOLQ = 0.0, DDLG = 0.0, BFT0 = 0.0, FNP = 0.0, FDS0 = 0.0, DRT = 0.0;
            //READ(KR(21),303)RFN0,CO20,CNO30,CSLT,PSTX,YWI,BTA,EXPK,FL,FW,ANG0,STD0,UXP,DIAM,ACW,BIR,EFI,VIMX,ARMN,ARMX,BFT0,FNP,FMX,DRT,FDS0,PEC0,VLGN,COWW,DDLG,SOLQ,GZLM,FFED,DZ,DRV,RST0,BUS
            //Reading from File  EPICCONT.DAT
            double BUS0 = PARM.BUS[0];
            if (RST0 < 1.0E-10) RST0 = 5.0;
            PARM.ANG = ANG0 / PARM.CLT;
            if (PARM.DZ < 1.0E-10) PARM.DZ = .1;
            /*                                                            
            !     READ ECONOMIC DATA                                                             
            !  1  COIR = COST OF IRRIGATION WATER ($/m3)                                       
            !  2  COL  = COST OF LIME ($/t)                                                      
            !  3  FULP = COST OF FUEL ($/gal)                                                    
            !  4  WAGE = LABOR COST ($/h)                                                        
            !  5  CSTZ = MISCELLANEOUS COST ($/ha)                                               
            !  6  1/2                                                                            
            !     LINE 7
             */
            //READ(KR(21),303)(XTP(I),I=1,6)              **Reading file EPICCONT.DAT                                           
            if (XTP[0] > 0.0) PARM.COIR = XTP[0];
            if (XTP[1] > 0.0) PARM.COL = XTP[1];
            if (XTP[2] > 0.0) PARM.FULP = XTP[2];
            if (XTP[3] > 0.0) PARM.WAGE = XTP[3];
            if (XTP[4] > 0.0) CSTZ[0] = XTP[4];
            if (XTP[5] > 0.0) CSTZ[1] = XTP[5];
            if (PARM.ARMX < 1.0E-10) PARM.ARMX = 1000.0;
            if (PARM.FMX < 1.0E-10) PARM.FMX = 200.0;
            if (PARM.GZLM < .01) PARM.GZLM = .01;
            if (PARM.UXP < 1.0E-10) PARM.UXP = .5;
            if (DIAM < 1.0E-10) DIAM = 500.0;
            PARM.USTRT = .0161 * Math.Sqrt(DIAM);
            PARM.USTT = PARM.USTRT * PARM.USTRT;

            for (I = 1; I <= 3; I++)
            {
                PARM.PSZ[I - 1] = .411 * PARM.PSZ[I - 1] * PARM.PSZ[I - 1];
            }
            if (PARM.ISW > 6)
            {
                //CALL OPENV(KR(29),'SOIL38K.DAT         ',IDIR(1),KW(MSO))                                          
                //READ(KR(29),528)XAV,XDV,XRG,BRNG,NSX                                              
                PARM.NSNN = (int)(.655 * Math.Pow(PARM.NSX, .493));
                PARM.EXNN = .767 * Math.Pow(PARM.NSX, .049);
                for (I = 1; I <= PARM.NSX; I++)
                {
                    //READ(KR(29),528)(XSP(I,J),J=1,5)                                              
                    PARM.NX[I - 1] = I;
                }
                //CLOSE(KR(29))                                                                     
            }
            Functions.ALLOCATE_PARMS();

            //Probably could initialize HED in modparam itself
            string[] temp = { " TMX", " TMN", " RAD", "PRCP", "SNOF", "SNOM", "WSPD", "RHUM", " VPD", " PET", "  ET", " PEP", "  EP", "   Q", "  CN", " SSF", " PRK", "QDRN", "IRGA", " QIN", "TLGE", "TLGW", "TLGQ", "TLGF", "LGIR", "LGMI", "LGMO", "  EI", " CVF", "USLE", "MUSL", " AOF", "MUSS", "MUST", "RUS2", "RUSL", "RUSC", " WK1", "RHTT", "RRUF", "RGRF", "  YW", " YON", "QNO3", "SSFN", "PRKN", " NMN", " GMN", "  DN", "NFIX", "NITR", "AVOL", "DRNN", "  YP", " QAP", " MNP", "PRKP", "  ER", " FNO", "FNO3", "FNH3", " FPO", " FPL", " FSK", " FCO", "LIME", " TMP", "SW10", "SLTI", "SLTQ", "SLTS", "SLTF", "RSDC", "RSPC", "CLCH", " CQV", " YOC", "YEFK", " QSK", " SSK", " VSK", "SLTV", "MUSI", "IRDL", " HMN", "RNAD", "NIMO", "FALF", " DN2", "RLSF", " REK", "FULU", "DN2O", " FO2", "FCO2", "CFEM", "BURC", "BURN" };
            Array.Copy(temp, PARM.HED, temp.Length);

            //!	CALL DATE_AND_TIME(AYMD,AHMS)                                                      
            //CALL OPENV(KW(MSO+1),'RUN0810.SUM         ',0,KW(MSO))                                          
            //IF(NSTP>0)CALL OPENV(KW(MSO+3),'RTSOIL.DAT          ',0,KW(MSO))                                         
            if (PARM.KFL[PARM.MSO] != 0)
            {
                //WRITE(KW(MSO+1),621)IYER,IMON,IDAY,IT1,IT2,IT3                               
                //WRITE(KW(MSO+1),693)(HED(KYA(J)),J=1,NKYA)                                   
            }
            /*                                                     
            !     SELECT OUTPUT FILES--KFL=0(NO OUTPUT); KFL>0(GIVES OUTPUT FOR                 
            !     SELECTED FILE)                                                                 
            !     1  OUT = STANDARD OUTPUT                                                       
            !     2  ACM = ANNUAL CROPMAN                                                        
            !     3  SUM = AVE ANNUAL SUMMARY                                                    
            !     4  DHY = DAILY HYDROLOGY                                                       
            !     5  DPS = DAILY PESTICIDE                                                       
            !     6  MFS = MONTHLY FLIPSIM                                                       
            !     7  MPS = MONTHLY PESTICIDE                                                     
            !     8  ANN = ANNUAL                                                                
            !     9  SOT = ENDING SOIL TABLE                                                     
            !    10  DTP = DAILY SOIL TEMPERATURE                                                
            !    11  MCM = MONTHLY CROPMAN                                                       
            !    12  DCS = DAILY CROP STRESS                                                     
            !    13  SCO = SUMMARY OPERATION COST                                                
            !    14  ACN = ANNUAL SOIL ORGANIC C & N TABLE                                       
            !    15  DCN = DAILY SOIL ORGANIC C & N TABLE                                        
            !    16  SCN = SUMMARY SOIL ORGANIC C & N TABLE                                      
            !    17  DGN = DAILY GENERAL OUTPUT                                                  
            !    18  DWT = DAILY SOIL WATER IN CONTROL SECTION AND .5M SOIL T                    
            !    19  ACY = ANNUAL CROP YIELD                                                     
            !    20  ACO = ANNUAL COST                                                           
            !    21  DSL = DAILY SOIL TABLE.                                                     
            !    22  MWC = MONTHLY WATER CYCLE + N CYCLE                                         
            !    23  ABR = ANNUAL BIOMASS ROOT WEIGHT                                            
            !    24  ATG = ANNUAL TREE GROWTH.                                                   
            !    25  MSW = MONTHLY OUTPUT TO SWAT.                                               
            !    26  APS = ANNUAL PESTICIDE                                                      
            !    27  DWC = DAILY WATER CYCLE                                                     
            !    28  DHS = DAILY HYDROLOGY/SOIL                                                  
            !    29  DGZ = DAILY GRAZING FILE                                                    
            !   MSO  ERX = ERROR FILE                                                            
            !  MSO+1 RUN0810.SUM = AVE ANNUAL SUMMARY FILE FOR ALL SIMULATIONS IN A              
            !        BATCH.                                                                      
            !  MSO+2     = RTCROP.DAT                                                            
            !  MSO+3     = RTSOIL.DAT                                                            
            !  MSO+4 SGI = SUMMARY GIS FILE                                                      
            !  MSO+5 ANNUAL FILES FOR GIS                                                        
            !     LINE 15/16  
             */
            //READ(KR(5),300)KFL                  **Reading from FPRNT                                                             
            if (PARM.NHC[0] == 0)
            {
                for (I = 1; I <= 26; I++)
                {
                    PARM.NHC[I - 1] = I;
                }
            }
            if (PARM.NDC[0] == 0)
            {
                for (I = 1; I <= 10; I++)
                {
                    PARM.NDC[I - 1] = I;
                }
            }
            //CLOSE(KR(5))                                                                   
            if (PARM.FL < 1.0E-10) PARM.FL = .632;
            if (PARM.FW < 1.0E-10) PARM.FW = .316;
            PARM.NDRV = (int)(DRV + 1.1);
            if (YWI < 1.0E-10) YWI = 10.0;
            if (BTA < 1.0E-10) BTA = .75;
            if (PARM.EXPK < 1.0E-10) PARM.EXPK = 1.3;

            if (PARM.KFL[PARM.NGF - 1] > 0)
            {
                for (I = 1; I <= 150; I++)
                {
                    if (PARM.IYR0 == PARM.IGY[I - 1]) goto lbl739;
                }
                goto lbl519;
            lbl739: int IGYX = I;
                int N1 = PARM.NGF - 1;
                AGSM = AYR[I - 1] + "-" + AYR[I + PARM.NBYR - 2] + ".TXT";
                //OPEN(KW(N1),FILE=AGSM)                                                              
                if (PARM.KFL[PARM.NGF - 1] >= 0)
                {
                    while (true)
                    {
                        //READ(KW(N1),505,IOSTAT=NFL)WINDFILE //Reading a file until EOF                                       
                        //if(NFL!=0)break;
                        break; //temporary break until reading is put in
                    }
                }
                double IGIS = PARM.NGF;
                for (I = 1; I <= PARM.NBYR; I++)
                {
                    AGIX = AYR[IGYX - 1] + ".TXT";
                    IGYX = IGYX + 1;
                    //OPEN(KW(IGIS),FILE=AGIX)                                                            
                    if (PARM.KFL[PARM.NGF - 1] < 0)
                    {
                        IGIS = IGIS + 1;
                    }
                    else
                    {
                        while (true)
                        {
                            //READ(KW(IGIS),505,IOSTAT=NFL)WINDFILE    reads until EOF                                    
                            //IF(NFL/=0)EXIT                                                                
                        }
                    }
                }
            }
            /*
            !  1  ASTN = RUN #                                                                   
            !  2  ISIT = SITE #                                                                  
            !  3  IWP1 = WEATHER STA # FROM KR(17) WPM10810.DAT                                  
            !  4  IWP5 = WEATHER STA # FROM KR(18) WPM10810.DAT                                  
            !  5  IWND = WIND STA # FROM KR(19) WIND0810.DAT                                     
            !  6  INPS = SOIL # FROM TABLE KR(13)                                                
            !  7  IOPS = OP SCHED # FROM TABLE KR(15)                                            
            !  8  IWTH = DAILY WEATHER STA # FROM KR(27) WTHCOM.DAT
             */
            int KK;
        lbl519: while (true)
            {
                int IWP5=0;
                double IWP1=0.0, IWND=0.0, IWTH=0.0;
                if (PARM.IBAT == 0)
                {

                    //READ(KR(11),*,IOSTAT=NFL)ASTN,ISIT,IWP1,IWP5,IWND,INPS,IOPS,IWTH                    **Reading from EPICRUN.DAT
                    //if(NFL/=0) goto lbl219;                                                            
                    if (ISIT == 0) goto lbl219;
                }
                else
                {
                    //WRITE(KW(MSO+6),'(A80,A8,1X,7A8)')CM
                    PARM.ASTN = CM[1];
                    //REWIND KW(MSO+6)
                    //READ(KW(MSO+6),'()')
                    //READ(KW(MSO+6),'(88X,7I8)')IIX

                    ISIT = (int)IIX[0];
                    IWP1 = IIX[1];
                    IWP5 = (int)IIX[2];
                    IWND = IIX[3];
                    INPS = (int)IIX[4];
                    IOPS = (int)IIX[5];
                    IWTH = IIX[6];
                }
                int oldlength = PARM.ASTN.Length;
                PARM.ASTN = PARM.ASTN.Trim();
                PARM.ASTN = PARM.ASTN.PadLeft(oldlength - PARM.ASTN.Length);   //ADJUSTR call                                       
                //WRITE(*,679)ASTN,ISIT,IWP1,INPS,IOPS,IWTH                                           
                //WRITE(KW(1),679)ASTN,ISIT,IWP1,INPS,IOPS,IWTH                                           
                int K19 = 0;
                //CALL OPENF
                //WRITE(KW(1),621)IYER,IMON,IDAY,IT1,IT2,IT3                                                                                                                                                                                                                                  
                //WRITE(KW(1),508)'FSITE',FSITE,'FWPM1',FWPM1,'FWPM5',FWPM5,'FWIND',FWIND,'FWIDX',FWIDX,'FCROP',FCROP,'FTILL',FTILL,'FPEST',FPEST,'FFERT',FFERT,'FSOIL',FSOIL,'FOPSC',FOPSC,'FTR55',FTR55,'FPARM',FPARM,'FMLRN',FMLRN,'FPRNT',FPRNT,'FCMOD',FCMOD,'FWLST',FWLST                  
                WPM1FILE = " ";
                WINDFILE = " ";
                UAV0[0] = 0.0;
                PARM.NYD = 1;
                for (I = 1; I <= 5; I++)
                {
                    PARM.KGN[I - 1] = 0;
                }
                Functions.AINLZ();
                Functions.AINIX();
                for (J = 1; J <= 21; J++)
                {
                    PARM.IDG[J - 1] = J;
                }
                PARM.NGN = PARM.NGN0;
                int IWRT = 0;
                PARM.IRO0 = 1;
                PARM.IRUN = PARM.IRUN + 1;
                //WRITE(KW(1),'(/1X,A)')'-----VARIABLE NAMES'                                    
                //WRITE(KW(1),229)                                                               
                //WRITE(KW(1),287)HED                                                            
                //WRITE(KW(1),245)                                                               
                for (J = 1; J <= 30; J++)
                {
                    //WRITE(KW(1),242)J,PRMT(J),(SCRX(J,I),I=1,2),(SCRP(J,I),I=1,2)                
                }
                for (J = 31; J <= PARM.PRMT.Length; J++)
                {
                    //WRITE(KW(1),242)J,PRMT(J)                                                    
                }
                int II = -1;
                while (II != ISIT)
                {
                    //READ(KR(23),*,IOSTAT=NFL)II,SITEFILE                 
                    if (NFL != 0)
                    {
                        if (PARM.IBAT == 0)
                        {
                            //WRITE(*,*)'SITE NO = ',ISIT,' NOT IN SITE LIST FILE'
                            return;
                        }
                        else
                        {
                            //WRITE(KW(MSO),'(A,A8,A,I4,A)')' !!!!! ',ASTN,'SITE NO = ',ISIT,' NOT IN SITE LIST FILE'
                            goto lbl219;
                        }
                    }
                }
                //REWIND KR(23)                                                                  
                //CALL OPENV(KR(1),SITEFILE,IDIR(2),KW(MSO))                                             
                //!     TITLE=PROBLEM DESCRIPTION(3 LINES)                                             
                //!     LINES 1/3                                                                      
                //READ(KR(1),299)(TITLE(I),I=1,60)                                               
                Functions.APAGE(0);
                PARM.INFL = INFL0 + 1;
                if (IRW == 0 || PARM.IRUN == 1) PARM.IYR = PARM.IYR0;
                PARM.IBDT = PARM.IDA0 + 100 * PARM.IMO0 + 10000 * PARM.IYR;
                Functions.AISPL(ref PARM.IPD, ref PARM.INP);
                if (IWP5 > 0)
                {
                    //READ(KR(20),505)VOID                                                           
                    //READ(KR(20),300)II                                                             
                    II = PARM.IYR - II;
                    for (I = 1; I <= II; I++)
                    {
                        //READ(KR(20),505)VOID                                                         
                    }
                }
                PARM.NOP = 1;
                if (PARM.IPD <= 5 && PARM.INP > 0) PARM.NOP = 0;
                if (PARM.INP == 0) PARM.INP = 1;
                if (PARM.IPD <= 5) PARM.IPYI = PARM.INP;

                /*
                !  1  YLAT = LATITUDE(deg)                                                           
                !  2  XLOG = LONGITUDE(deg)                                                          
                !  3  ELEV = ELEVATION OF WATERSHED (m)                                              
                !  4  APM  = PEAK RATE - EI ADJUSTMENT FACTOR (BLANK IF UNKNOWN)                     
                !  5  CO2X = CO2 CONCENTRATION IN ATMOSPHERE (ppm)--NON ZERO VALUE                   
                !            OVERRIDES CO2 INPUT IN EPICCONT.DAT                                     
                !  6  CNO3X= CONC OF NO3 IN IRRIGATION WATER (ppm)--NON ZERO VALUE                   
                !            OVERRIDES CNO30 INPUT IN EPICCONT.DAT                                   
                !  7  RFNX = AVE CONC OF N IN RAINFALL (ppm)                                         
                !  8  X1   = DUMMY                                                                   
                !  9  X2   = DUMMY                                                                   
                ! 10  SNO0 = WATER CONTENT OF SNOW ON GROUND AT START OF SIMULATION(mm)
                ! 11  AZM  = AZIMUTH ORIENTATION OF LAND SLOPE (DEGREES CLOCKWISE FROM NORTH)              
                !     LINE 4
                 */
                double CO2X = 0.0, CNO3X = 0.0, RFNX = 0.0, APM = 0.0, AZM = 0.0, SNO0 = 0.0;
                //READ(KR(1),304)YLAT,XLOG,ELEV,APM,CO2X,CNO3X,RFNX,X1,X2,SNO0,AZM  
                /*       
                !  1  WSA  = WATERSHED AREA(HA)                                                      
                !  2  CHL  = MAINSTEM CHANNEL LENGTH (km)(BLANK IF UNKNOWN                           
                !  3  CHS  = MAINSTEM CHANNEL SLOPE (m/m)(BLANK IF UNKNOWN                           
                !  4  CHD  = CHANNEL DEPTH (m)                                                       
                !  5  CHN  = MANNINGS N FOR CHANNEL (BLANK IF UNKNOWN)                               
                !  6  SN   = SURFACE N VALUE (BLANK IF UNKNOWN)                                      
                !  7  UPSL = UPLAND SLOPE LENGTH (m)                                                 
                !  8  UPS  = UPLAND SLOPE STEEPNESS (m/m)                                            
                !  9  PEC  = CONSERVATION PRACTICE FACTOR(=0.0 ELIMINATES WATER EROSION)
                ! 10  DTG  = TIME INTERVAL FOR GAS DIFF EQS (H)             
                !     LINE 5  
                 */
                double CHN = 0.0, CHS = 0.0, CHL = 0.0, CHD = 0.0;
                //READ(KR(1),304)WSA,CHL,CHS,CHD,CHN,SN,UPSL,UPS,PEC,DTG                             
                if (PARM.PEC < 1.0E-10) PARM.PEC = PEC0;
                /*                                       
                !     READ MANAGEMENT INFORMATION                                                    
                !  1  IRR  = N0 FOR DRYLAND AREAS          | N = 0 APPLIES MINIMUM OF                
                !          = N1 FROM SPRINKLER IRRIGATION  | VOLUME INPUT, ARMX, & FC-SW             
                !          = N2 FOR FURROW IRRIGATION      | N = 1 APPLIES INPUT VOLUME              
                !          = N3 FOR IRR WITH FERT ADDED    | OR ARMX                                 
                !          = N4 FOR IRR FROM LAGOON        |                                         
                !          = N5 FOR DRIP IRR                                                         
                !  2  IRI  = N DAY APPLICATION INTERVAL FOR AUTOMATIC IRRIGATION                     
                !  3  IFA  = MIN FERT APPL INTERVAL(BLANK FOR USER SPECIFIED)                        
                !  4  IFD  = 0 WITHOUT FURROW DIKES                                                  
                !            1 WITH FURROW DIKES                                                     
                !  5  IDR0 = 0 NO DRAINAGE                                                           
                !          = DEPTH OF DRAINAGE SYSTEM(mm)                                            
                !  6  IDF0 = FERT # FOR AUTO N FERT & FERTIGATION--BLANK DEFAULTS TO                   
                !            ELEMENTAL N
                !  7  MNU  = > 0 AUTO DRY MANURE APPL WITHOUT TRIGGER                                
                !  8  IMW  = MIN INTERVAL BETWEEN AUTO MOW                                           
                !  9  IDFP = FERT # FOR AUTO P FERT--BLANK DEFAULTS TO ELEMENTAL P
                !     LINE 6
                 */
                int IDR0=0;
                //READ(KR(1),300)IRR,IRI,IFA,IFD,IDR0,IDF0,MNU,IMW,IDFP                               
                Functions.AISPL(ref PARM.IRR, ref PARM.IAC);
                PARM.CO2 = CO20;
                if (CO2X > 0.0) PARM.CO2 = CO2X;
                PARM.CNO3I = CNO30;
                if (CNO3X > 0.0) PARM.CNO3I = CNO3X;
                PARM.CNO3I = PARM.CNO3I * .01;
                PARM.RFNC = RFN0;
                if (RFNX > 0.0) PARM.RFNC = RFNX;
                PARM.RFNC = PARM.RFNC * .01;
                if (CHN < 1.0E-10) CHN = .05;
                if (PARM.SN < 1.0E-10) PARM.SN = .15;
                if (APM < 1.0E-10) APM = 1.0;
                if (PARM.DTG < 1.0E-10) PARM.DTG = 1.0;
                PARM.NBDT = (int)(24.0 / PARM.DTG + .9);
                XX = PARM.YLAT / PARM.CLT;
                PARM.SIN1 = Math.Sin(XX);
                PARM.COS1 = Math.Cos(XX);
                PARM.YLTS = PARM.SIN1;
                PARM.YLTC = PARM.COS1;
                PARM.YTN = Math.Tan(XX);
                PARM.YTN1 = PARM.YTN;
                double WSX = 1.0 + PARM.WSA;
                PARM.YSW = .79 * Math.Pow(WSX, .009);
                PARM.WSA1 = 1.586 * Math.Pow(WSX, .12);
                PARM.PMX = PARM.PRMT[15];
                PARM.CFMN = PARM.PRMT[31];
                PARM.SX = Math.Sqrt(PARM.UPS);
                PARM.UPSQ = (1.0 - 2.0 * Math.Exp(-13.86 * PARM.UPS)) / 3.0;
                if (PARM.NGN > 0)
                {
                    if (IRW == 0 || PARM.IRUN == 1)
                    {
                        Functions.WDOP(ref IDIR[0]);
                        PARM.IYR = PARM.IYR0;
                    }
                }
                Functions.ALPYR(ref PARM.IYR, ref PARM.NYD, ref PARM.LPYR);
                PARM.PB = 101.3 - PARM.ELEV * (.01152 - 5.44E-7 * PARM.ELEV);
                if (PARM.YLAT > 0.0)
                {
                    PARM.MOFX = 12;
                }
                else
                {
                    PARM.MOFX = 13;
                }
                PARM.GMA = 6.595E-4 * PARM.PB;
                double CH = .4349 * Math.Abs(PARM.YTN);
                double H;
                if (CH >= 1.0)
                {
                    H = 0.0;
                }
                else
                {
                    H = Math.Acos(CH);
                }
                PARM.HLMN = 7.72 * H;
                PARM.HR0 = PARM.HLMN;


                switch (PARM.NDRV)
                {
                    case 1:
                        PARM.NDVSS = 34;
                        break;
                    case 2:
                        PARM.NDVSS = 32;
                        break;
                    case 3:
                        PARM.NDVSS = 30;
                        break;
                    case 4:
                        PARM.NDVSS = 33;
                        break;
                    case 5:
                        PARM.NDVSS = 31;
                        break;
                    case 6:
                        PARM.NDVSS = 83;
                        break;
                    case 7:
                        PARM.NDVSS = 36;
                        break;
                    case 8:
                        PARM.NDVSS = 35;
                        break;
                }
                double XY2 = .5 / YWI;
                int I1;
                if (IWP5 > 0)
                {
                    I1 = IWP5 - 1;
                    II = 67 * I1;
                    if (II > 0)
                    {

                        for (I = 1; I <= II; I++)
                        {
                            //READ(KR(18),505,IOSTAT=NFL)VOID                                              
                            //if(NFL!=0)break;                                                                   
                        }
                        if (PARM.IBAT == 0)
                        {
                            //WRITE(*,*)'WPM5 NO = ',IWP5,' NOT IN WPM5 FILE'
                            return;
                        }
                        else
                        {
                            //WRITE(KW(MSO),'(A,A8,A,I4,A)')' !!!!! ',ASTN,'WPM5 NO = ',IWP5,' NOT IN WPM5 FILE'
                            goto lbl219;
                        }
                    }
                    //!     LINE 1/2                                                                       
                    //READ(KR(18),505)TITW5
                }
                if (IWP1 == 0)
                {
                    double W0 = 1.0E20;
                    while (true)
                    {
                        double Y = 0.0, X = 0.0, ELEX = 0.0;
                        //READ(KR(17),*,IOSTAT=NFL)II,OPSCFILE,Y,X,ELEX                                     
                        //if(NFL!=0) break;                                                                    
                        double RY = Y / PARM.CLT;
                        XX = PARM.SIN1 * Math.Sin(RY) + PARM.COS1 * Math.Cos(RY) * Math.Cos((X - PARM.XLOG) / PARM.CLT);
                        double D = 6378.8 * Math.Acos(XX);
                        double E = Math.Abs(PARM.ELEV - ELEX);
                        double W1 = PARM.PRMT[78] * D + (1.0 - PARM.PRMT[78]) * E;
                        if (W1 >= W0) continue;
                        W0 = W1;
                        WPM1FILE = PARM.OPSCFILE;
                    }
                }
                else
                {
                    II = -1;
                    while (II != IWP1)
                    {
                        //READ(KR(17),*,IOSTAT=NFL)II,WPM1FILE                        
                        if (NFL != 0)
                        {
                            if (PARM.IBAT == 0)
                            {
                                //WRITE(*,*)'WPM1 NO = ',IWP1,' NOT IN MO WEATHER LIST FILE'
                                return;
                            }
                            else
                            {
                                //WRITE(KW(MSO),'(A,A8,A,I4,A)')' !!!!! ',ASTN,' WPM1 NO = ',IWP1,' NOT IN MO WEATHER LIST FILE'
                                goto lbl219;
                            }
                        }
                    }
                }
                //REWIND KR(17)                                                                  
                //CALL OPENV(KR(24),WPM1FILE,IDIR(1),KW(MSO))                                            
                //!     LINE 1/2                                                                       
                //READ(KR(24),505)VOID                                                           
                //READ(KR(24),505)VOID                                                           
                Functions.APAGE(0);
                //WRITE(KW(1),'(//1X,A/)')'____________________WEATHER DATA________________________'                                                               
                PARM.IYX = PARM.IYR0 - 1880;
                if (PARM.ICO2 == 0)
                {
                    //WRITE(KW(1),'(T10,A)')'STATIC ATMOSPHERIC CO2'
                }
                else
                {
                    if (PARM.ICO2 == 1)
                    {
                        if (PARM.IYX < 25)
                        {
                            PARM.CO2 = 280.33;
                        }
                        else
                        {
                            X1 = PARM.IYX;
                            PARM.CO2 = 280.33 - X1 * (.1879 - X1 * .0077);
                        }
                        //WRITE(KW(1),'(T10,A)')'DYNAMIC ATMOSPHERIC CO2'                              
                    }
                    else
                    {
                        //WRITE(KW(1),'(T10,A)')'INPUT ATMOSPHERIC CO2'                                     
                    }
                }
                //WRITE(KW(1),'(T10,A,F6.0,A)')'CO2 CONC ATMOSPHERE = ',CO2,' ppm'               
                //WRITE(KW(1),'(T10,A,F4.0,A)')'PERIOD OF RECORD P5MX =',YWI,' Y'                
                if (PARM.NGN > 0)
                {
                    Functions.WIGV();
                }
                else
                {
                    //WRITE(KW(1),'(/T10,A)')'**********RAIN, TEMP, RAD, WIND SPEED, & REL HUM ARE GENERATED**********'                                         
                }
                switch (PARM.IET)
                {
                    case 1:
                        //WRITE(KW(1),'(/T10,A)')'**********PENMAN-MONTEITH  EQ USED TO EST POT ET**********'
                        break;
                    case 2:
                        //WRITE(KW(1),'(/T10,A)')'**********PENMAN EQ USED TO EST POT ET**********'   
                        break;
                    case 3:
                        //WRITE(KW(1),'(/T10,A)')'**********PRIESTLEY-TAYLOR EQ USED TO EST POT ET**********'
                        break;
                    case 4:
                        //WRITE(KW(1),'(/T10,A)')'**********HARGREAVES EQ USED TO EST POT ET**********'
                        break;
                    case 5:
                        //WRITE(KW(1),'(/T10,A)')'********** BAIER-ROBERTSON EQ USED TO EST POT ET **********' 
                        break;
                    default:
                        //WRITE(KW(1),'(/T10,A)')'**********HARGREAVES EQ USED TO EST POT ET**********'
                        break;
                }
                double SUM, F, RFMX;
                for (int IW = 1; IW <= 6; IW++)
                {
                    /*                                                                  
                    !  3  OBMX   = AV MO MAX TEMP (C)                                                    
                    !  4  OBMN   = AV MO MIN TEMP (C)                                                    
                    !  5  SDTMX  = MO STANDARD DEV MAX TEMP (C)OR EXTREME MAXIMUM TEMP (C)               
                    !              IF STANDARD DEV IS NOT AVAILABLE (BLANK IF TEMP IS INPUT              
                    !              DAILY)                                                                
                    !  6  SDTMN  = MO STANDARD DEV MIN TEMP (C)OR EXTREME MIN TEMP (C)                   
                    !              IF STANDARD DEV IS NOT AVAILABLE (BLANK IF TEMP IS INPUT              
                    !              DAILY)                                                                
                    !  7  RMO    = AV MO PRECIPITATION (mm)                                              
                    !  8  RST(2) = MONTHLY ST DEV OF DAILY RAINFALL (mm)(BLANK IF UNKNOWN                
                    !              OR IF DAILY PRECIPITATION IS INPUT)                                   
                    !  9  RST(3) = MONTHLY SKEW COEF OF DAILY RAINFALL (BLANK IF UNKNOWN OR              
                    !              DAILY PRECIPITATION IS INPUT)                                         
                    ! 10  PRW(1) = MONTHLY PROBABILITY OF WET DAY AFTER DRY DAY (BLANK IF                
                    !              UNKNOWN OR IF DAILY PRECIPITATION IS INPUT)                           
                    ! 11  PRW(2) = MONTHLY PROBABILITY OF WET DAY AFTER WET DAY (BLANK IF                
                    !              UNKNOWN OR IF DAILY PRECIPITATION IS INPUT)                           
                    ! 12  UAVM   = AV NO DAYS OF PRECIPITATION/MO (BLANK IF PRECIP IS                    
                    !              GENERATED AND IF PRW 1&2 ARE INPUT)                                   
                    ! 13  WI     = 3 OPTIONS--(1)MO MAX .5 H RAIN FOR PERIOD = YWI (mm)                  
                    !                         (2)ALPHA (MEAN .5 H RAIN/MEAN STORM                        
                    !                             AMOUNT)                                                
                    !                         (3)BLANK IF UNKNOWN                                        
                    ! 14  OBSL   = AV MO SOL RAD (MJ/M2 OR LY)(BLANK IF UNKNOWN)                         
                    ! 15  RH     = 3 OPTIONS--(1)AV MO RELATIVE HUMIDITY (FRACTION)                      
                    !                         (2)AV MO DEW POINT TEMP deg C                              
                    !                         (3)BLANK IF UNKNOWN                                        
                    !              USED IN PENMAN OR PENMAN-MONTEITH EQS                                 
                    ! 16  UAV0   = AV MO WIND SPEED (M/S)                                                
                    !     LINES 3/15
                     */
                    if (IW == 1)
                    {
                        //READ(KR(24),310)(OBMX(IW,I),I=1,12)                                            
                        //READ(KR(24),310)(OBMN(IW,I),I=1,12)                                            
                        //READ(KR(24),310)(SDTMX(IW,I),I=1,12)                                           
                        //READ(KR(24),310)(SDTMN(IW,I),I=1,12)                                           
                        //READ(KR(24),310)(RMO(IW,I),I=1,12)                                             
                        //READ(KR(24),310)(RST(2,IW,I),I=1,12)                                           
                        //READ(KR(24),310)(RST(3,IW,I),I=1,12)                                           
                        //READ(KR(24),310)(PRW(1,IW,I),I=1,12)                                           
                        //READ(KR(24),310)(PRW(2,IW,I),I=1,12)                                           
                        //READ(KR(24),310)(UAVM(I),I=1,12)                                               
                        //READ(KR(24),310)(WI(IW,I),I=1,12)                                              
                        //READ(KR(24),310)(OBSL(IW,I),I=1,12)                                            
                        //READ(KR(24),310)(RH(IW,I),I=1,12)                                              
                        //READ(KR(24),310)(UAV0(I),I=1,12)                                    
                        //REWIND KR(24)                                                             
                        //WRITE(KW(1),295)                                                               
                        PARM.TAV[0] = .25 * (PARM.OBMX[IW - 1, 11] + PARM.OBMN[IW - 1, 11] + PARM.OBMX[IW - 1, 0] + PARM.OBMN[IW - 1, 0]);
                        PARM.JT1 = 1;
                        if (PARM.OBMN[IW - 1, 0] > PARM.OBMN[IW - 1, 11]) PARM.JT1 = 12;
                        PARM.TMN = PARM.OBMN[IW - 1, PARM.JT1 - 1];
                        for (I = 2; I <= 12; I++)
                        {
                            I1 = I - 1;
                            PARM.TAV[I - 1] = .25 * (PARM.OBMX[IW - 1, I - 1] + PARM.OBMN[IW - 1, I - 1] + PARM.OBMX[IW - 1, I1 - 1] + PARM.OBMN[IW - 1, I1 - 1]);
                            if (PARM.OBMN[IW - 1, I - 1] > PARM.TMN) continue;
                            PARM.JT1 = I;
                            PARM.TMN = PARM.OBMN[IW - 1, I - 1];
                        }
                    }
                    else
                    {
                        //READ(KR(18),310)(OBMX(IW,I),I=1,12)                                            
                        //READ(KR(18),310)(OBMN(IW,I),I=1,12)                                            
                        //READ(KR(18),310)(SDTMX(IW,I),I=1,12)                                           
                        //READ(KR(18),310)(SDTMN(IW,I),I=1,12)                                           
                        //READ(KR(18),310)(RMO(IW,I),I=1,12)                                             
                        //READ(KR(18),310)(RST(2,IW,I),I=1,12)                                           
                        //READ(KR(18),310)(RST(3,IW,I),I=1,12)                                           
                        //READ(KR(18),310)(PRW(1,IW,I),I=1,12)                                           
                        //READ(KR(18),310)(PRW(2,IW,I),I=1,12)                                           
                        //READ(KR(18),310)(UAVM(I),I=1,12)                                               
                        //READ(KR(18),310)(WI(IW,I),I=1,12)                                              
                        //READ(KR(18),310)(OBSL(IW,I),I=1,12)                                            
                        //READ(KR(18),310)(RH(IW,I),I=1,12)                                              
                        //READ(KR(18),310)(UAV0(I),I=1,12)                                                    
                    }
                    for (I = 1; I <= 12; I++)
                    {
                        if (PARM.RST[1, IW - 1, I - 1] < 1.0E-5 || PARM.RST[2, IW - 1, I - 1] < 1.0E-5) break;
                    }
                    double REXP=0.0;
                    if (I > 12)
                    {
                        PARM.ICDP = 0;
                    }
                    else
                    {
                        PARM.ICDP = 1;
                        SUM = 0.0;
                        for (I = 1; I <= 10000; I++)
                        {
                            XX = Functions.AUNIF(PARM.IDG[2]);
                            SUM = SUM + Math.Pow((-Math.Log(XX)), PARM.EXPK);
                        }
                        REXP = 10100.0 / SUM;
                    }
                    PARM.BIG = 0.0;
                    PARM.V3 = Functions.AUNIF(PARM.IDG[2]);

                    for (I = 1; I <= 12; I++)
                    {
                        I1 = I + 1;
                        XM = PARM.NC[I1 - 1] - PARM.NC[I - 1];
                        PARM.JDA = (int)((PARM.NC[I1 - 1] + PARM.NC[I - 1]) * .5);
                        Functions.WHRL();
                        Functions.WRMX();
                        PARM.SRMX[I - 1] = PARM.RAMX;
                        PARM.THRL[I - 1] = PARM.HRLT;
                        if (PARM.HRLT > PARM.BIG) PARM.BIG = PARM.HRLT;
                        XYP[I - 1] = 0.0;
                        XX = PARM.SDTMX[IW - 1, I - 1] - PARM.SDTMN[IW - 1, I - 1];
                        if (XX > 10.0)
                        {
                            PARM.SDTMX[IW - 1, I - 1] = (PARM.SDTMX[IW - 1, I - 1] - PARM.OBMX[IW - 1, I - 1]) * .25;
                            PARM.SDTMN[IW - 1, I - 1] = (PARM.OBMN[IW - 1, I - 1] - PARM.SDTMN[IW - 1, I - 1]) * .25;
                        }
                        if (PARM.PRW[0, IW - 1, I - 1] > 0.0)
                        {
                            PARM.UAVM[I - 1] = XM * PARM.PRW[0, IW - 1, I - 1] / (1.0 - PARM.PRW[1, IW - 1, I - 1] + PARM.PRW[0, IW - 1, I - 1]);
                        }
                        else
                        {
                            PARM.PRW[0, IW - 1, I - 1] = BTA * (PARM.UAVM[I - 1] + .0001) / XM;
                            PARM.PRW[1, IW - 1, I - 1] = 1.0 - BTA + PARM.PRW[0, IW - 1, I - 1];
                        }
                        PARM.RST[0, IW - 1, I - 1] = PARM.RMO[IW - 1, I - 1] / (PARM.UAVM[I - 1] + .01);

                        if (PARM.OBSL[IW - 1, I - 1] <= 0.0)
                        {
                            X1 = Math.Max(.8, .21 * Math.Sqrt(PARM.OBMX[IW - 1, I - 1] - PARM.OBMN[IW - 1, I - 1]));
                            PARM.OBSL[IW - 1, I - 1] = X1 * PARM.RAMX;
                        }
                        if (PARM.ICDP == 0)
                        {
                            SUM = 0.0;
                            PARM.RFVM = PARM.RST[0, IW - 1, I - 1];
                            PARM.RFSD = PARM.RST[1, IW - 1, I - 1];
                            PARM.RFSK = PARM.RST[2, IW - 1, I - 1];
                            double R6 = PARM.RFSK / 6.0;
                            for (J = 1; J <= 1000; J++)
                            {
                                double V4 = Functions.AUNIF(PARM.IDG[2]);
                                XX = Functions.ADSTN(ref PARM.V3, ref V4);
                                PARM.V3 = V4;
                                double R1 = Functions.WRAIN(R6, XX, PARM.RFSD, PARM.RFSK, PARM.RFVM);
                                SUM = SUM + R1;
                            }
                            PARM.PCF[IW - 1, I - 1] = 1010.0 * PARM.RST[0, IW - 1, I - 1] / SUM;
                        }
                        else
                        {
                            PARM.RST[0, IW - 1, I - 1] = PARM.RST[0, IW - 1, I - 1] * REXP;
                            PARM.PCF[IW - 1, I - 1] = 1.0;
                        }
                    }
                    XYP[0] = PARM.OBMX[IW - 1, 0];
                    PARM.BIG = PARM.OBSL[IW - 1, 0];
                    double UPLM = PARM.RH[IW - 1, 0];
                    RFMX = PARM.RMO[IW - 1, 0];
                    double EXTM = PARM.WI[IW - 1, 0];
                    for (I = 2; I <= 12; I++)
                    {
                        if (PARM.OBSL[IW - 1, I - 1] > PARM.BIG) PARM.BIG = PARM.OBSL[IW - 1, I - 1];
                        if (PARM.RMO[IW - 1, I - 1] > RFMX) RFMX = PARM.RMO[IW - 1, I - 1];
                        if (PARM.RH[IW - 1, I - 1] > UPLM) UPLM = PARM.RH[IW - 1, I - 1];
                        if (PARM.WI[IW - 1, I - 1] > EXTM) EXTM = PARM.WI[IW - 1, I - 1];
                        XYP[0] = XYP[0] + PARM.OBMX[IW - 1, I - 1];
                    }

                    XYP[0] = XYP[0] / 12.0;
                    PARM.RUNT = 1.0;
                    if (PARM.BIG > 100.0) PARM.RUNT = .04184;
                    X3 = .3725 / (XYP[0] + 20.0);
                    SUM = 0.0;

                    for (I = 1; I <= 12; I++)
                    {
                        XM = PARM.NC[I] - PARM.NC[I - 1];
                        PARM.WFT[IW - 1, I - 1] = PARM.UAVM[I - 1] / XM;
                        XYP[1] = XYP[1] + PARM.OBMN[IW - 1, I - 1];
                        XYP[2] = XYP[2] + PARM.RMO[IW - 1, I - 1];
                        XYP[3] = XYP[3] + PARM.UAVM[I - 1];
                        PARM.OBSL[IW - 1, I - 1] = PARM.RUNT * PARM.OBSL[IW - 1, I - 1];
                        XYP[4] = XYP[4] + PARM.OBSL[IW - 1, I - 1];
                        X1 = Math.Max(PARM.RMO[IW - 1, I - 1], 12.7);
                        PARM.TX = .5 * (PARM.OBMX[IW - 1, I - 1] + PARM.OBMN[IW - 1, I - 1]);
                        if (UPLM > 1.0)
                        {
                            PARM.RH[IW - 1, I - 1] = Functions.ASVP(PARM.RH[IW - 1, I - 1] + 273.0) / Functions.ASVP(PARM.TX + 273.0);
                        }
                        else
                        {
                            if (PARM.RH[IW - 1, I - 1] < 1.0E-10)
                            {
                                XX = PARM.OBMX[IW - 1, I - 1] - PARM.OBMN[IW - 1, I - 1];
                                PARM.RH[IW - 1, I - 1] = .9 - .8 * XX / (XX + Math.Exp(5.122 - .1269 * XX));
                            }
                        }
                        X2 = Math.Max(PARM.TX, -1.7);
                        XYP[5] = XYP[5] + Math.Pow(((X1 / 25.4) / (1.8 * X2 + 22.0)), 1.111);
                        X1 = PARM.RMO[IW - 1, I - 1] / (PARM.UAVM[I - 1] + 1.0E-10);
                        if (EXTM < 1.0)
                        {
                            if (EXTM < 1.0E-10) PARM.WI[IW - 1, I - 1] = Math.Max(.05, APM * X3 * (PARM.OBMX[IW - 1, I - 1] + 20.0));
                            XTP[I - 1] = 5.3 * X1 * PARM.WI[IW - 1, I - 1];
                            continue;
                        }
                        F = XY2 / (PARM.UAVM[I - 1] + .01);
                        XTP[I - 1] = PARM.WI[IW - 1, I - 1];
                        PARM.WI[IW - 1, I - 1] = -XTP[I - 1] / Math.Log(F);
                        PARM.WI[IW - 1, I - 1] = APM * PARM.WI[IW - 1, I - 1] / (X1 + 1.0);
                        if (PARM.WI[IW - 1, I - 1] < .1) PARM.WI[IW - 1, I - 1] = .1;
                        if (PARM.WI[IW - 1, I - 1] > .95) PARM.WI[IW - 1, I - 1] = .95;
                        X1 = 1.4 - .0778 * PARM.TX;
                        X2 = .5 + .37 * PARM.TX;
                        X1 = Math.Min(1.0, Math.Min(X1, X2));
                        if (X1 <= 0.0) continue;
                        SUM = SUM + X1 * XM;
                    }
                    XYP[1] = XYP[1] / 12.0;
                    XYP[4] = XYP[4] / 12.0;
                    if (IW > 1)
                    {
                        II = IW - 1;
                        //WRITE(KW(1),571)II,TITW5(1)                                                  
                    }
                    else
                    {
                        //WRITE(KW(1),602)WPM1FILE                                                     
                    }
                    //WRITE(KW(1),321)HED(1),(OBMX(IW,I),I=1,12),XYP(1),HED(1)                       
                    //WRITE(KW(1),321)HED(2),(OBMN(IW,I),I=1,12),XYP(2),HED(2)                       
                    //WRITE(KW(1),224)'SDMX',(SDTMX(IW,I),I=1,12),'SDMX'                             
                    //WRITE(KW(1),224)'SDMN',(SDTMN(IW,I),I=1,12),'SDMN'                             
                    //WRITE(KW(1),243)HED(4),(RMO(IW,I),I=1,12),XYP(3),HED(4)                        
                    //WRITE(KW(1),223)'SDRF',(RST(2,IW,I),I=1,12),'SDRF'                             
                    //WRITE(KW(1),224)'SKRF',(RST(3,IW,I),I=1,12),'SKRF'                             
                    //WRITE(KW(1),225)'PW/D',(PRW(1,IW,I),I=1,12),'PW/D'                             
                    //WRITE(KW(1),225)'PW/W',(PRW(2,IW,I),I=1,12),'PW/W'                             
                    //WRITE(KW(1),321)'DAYP',(UAVM(I),I=1,12),XYP(4),'DAYP'                          
                    //WRITE(KW(1),223)'P5MX',(XTP(I),I=1,12),'P5MX'                                  
                    //WRITE(KW(1),243)HED(3),(OBSL(IW,I),I=1,12),XYP(5),HED(3)                       
                    //WRITE(KW(1),223)'RAMX',SRMX,'RAMX'                                             
                    //WRITE(KW(1),224)'HRLT',THRL,'HRLT'                                             
                    //WRITE(KW(1),224)'RHUM',(RH(IW,I),I=1,12),'RHUM'                                
                    //WRITE(KW(1),224)'ALPH',(WI(IW,I),I=1,12),'ALPH'                                
                    //WRITE(KW(1),224)' PCF',(PCF(IW,I),I=1,12),' PCF'                               
                    if (IWP5 == 0) break;
                }
            lbl568: continue;

                //REWIND KR(18)
                if (IWND == 0)
                {
                    double W0 = 1.0E20;
                    while (true)
                    {
                        double X, Y, ELEX;
                        //READ(KR(19),*,IOSTAT=NFL)II,OPSCFILE,Y,X,ELEX                                     
                        if (NFL != 0) break;
                        double RY = Y / PARM.CLT;
                        XX = PARM.SIN1 * Math.Sin(RY) + PARM.COS1 * Math.Cos(RY) * Math.Cos((X - PARM.XLOG) / PARM.CLT);
                        double D = 6378.8 * Math.Acos(XX);
                        double E = Math.Abs(PARM.ELEV - ELEX);
                        double W1 = PARM.PRMT[78] * D + (1.0 - PARM.PRMT[78]) * E;
                        if (W1 >= W0) continue;
                        W0 = W1;
                        WINDFILE = PARM.OPSCFILE;
                    }
                }
                else
                {
                    II = -1;
                    while (II != IWND)
                    {
                        //READ(KR(19),*,IOSTAT=NFL)II,WINDFILE                                         
                        if (NFL != 0)
                        {
                            if (PARM.IBAT == 0)
                            {
                                //WRITE(*,'(T10,A,I8,A)')'WIND NO = ',IWND,' NOT IN MO WIND LIST FILE'
                                return;
                            }
                            else
                            {
                                //WRITE(KW(MSO),'(A,A8,A,I4,A)')' //!//!//!//!//! ',ASTN,' WIND NO = ',IWND,' NOT IN MO WIND LIST FILE'
                                goto lbl219;
                            }
                        }
                    }
                }
                //REWIND KR(19)                                                                  
                //CALL OPENV(KR(25),WINDFILE,IDIR(1),KW(MSO))                                            
                //!     LINE 1/2                                                                       
                //READ(KR(25),505)VOID                                                           
                //READ(KR(25),505)VOID                                                           
                PARM.SX = PARM.SX / PARM.SN;
                if (PARM.CHL < 1.0E-10) PARM.CHL = COCH[4] * Math.Pow(PARM.WSA, COCH[5]);
                if (CHS < 1.0E-10) CHS = PARM.UPS * Math.Pow(WSX, (-.3));
                if (CHD < 1.0E-10) CHD = COCH[2] * Math.Pow(PARM.WSA, COCH[3]);
                PARM.UPSL = Math.Min(PARM.UPSL, Math.Sqrt(10000.0 * PARM.WSA));
                XM = .3 * PARM.UPS / (PARM.UPS + Math.Exp(-1.47 - 61.09 * PARM.UPS)) + .2;
                PARM.UPSX = PARM.UPSL / 22.127;
                PARM.SL = Math.Pow(PARM.UPSX, XM) * (PARM.UPS * (65.41 * PARM.UPS + 4.56) + .065);
                X1 = 3.0 * Math.Pow(PARM.UPS, .8) + .56;
                PARM.BETA = PARM.UPS / (.0896 * X1);
                double RXM = PARM.BETA / (1.0 + PARM.BETA);
                PARM.RLF = Math.Pow(PARM.UPSX, RXM);
                if (PARM.UPSL > 4.57)
                {
                    if (PARM.UPS > .09)
                    {
                        PARM.RSF = 16.8 * PARM.UPS - .5;
                    }
                    else
                    {
                        PARM.RSF = 10.8 * PARM.UPS + .03;
                    }
                }
                else
                {
                    PARM.RSF = X1;
                }
                if (PARM.ITYP > 0)
                {
                    double SFL;
                    if (PARM.CHL > .1)
                    {
                        SFL = 50.0;
                    }
                    else
                    {
                        if (PARM.CHL > .05)
                        {
                            SFL = 100.0 * (PARM.CHL - .05);
                        }
                        else
                        {
                            SFL = 0.0;
                        }
                    }
                    double TSF = SFL / Math.Min(2160.0, 17712.0 * PARM.SX * PARM.SN);
                    X1 = Math.Max(PARM.CHL - (PARM.UPSL + SFL) * .001, 0.0);
                    PARM.TCC = X1 / (Math.Pow(3.6 * CHD, .66667) * Math.Sqrt(CHS) / CHN);
                    PARM.TCS = .0913 * Math.Pow((PARM.UPSL * PARM.SN), .8) / Math.Pow(PARM.UPS, .4);
                    PARM.TCC = PARM.TCC + TSF;
                    PARM.TC = PARM.TCC + PARM.TCS;
                }
                else
                {
                    PARM.TCS = .0216 * Math.Pow((PARM.UPSL * PARM.SN), .75) / Math.Pow(PARM.UPS, .375);
                    PARM.TCC = 1.75 * PARM.CHL * Math.Pow(CHN, .75) / (Math.Pow(PARM.WSA, .125) * Math.Pow(CHS, .375));
                    X4 = Math.Min(PARM.UPSL / 360.0, PARM.TCS);
                    PARM.TC = X4 + PARM.TCC;
                }
                double YLAZ;
                if (PARM.IAZM > 0)
                {
                    X1 = Math.Asin(PARM.UPS);
                    YLAZ = PARM.YLAT / PARM.CLT;
                    X2 = AZM / PARM.CLT;
                    YLAZ = PARM.CLT * Math.Asin(PARM.UPS * Math.Cos(X2) * Math.Cos(YLAZ) + Math.Cos(X1) * Math.Sin(YLAZ));
                    //WRITE(KW(1),'(T10,A,F8.3)')'EQUIVALENT LATITUDE = ',YLAZ
                }
                else
                {
                    YLAZ = PARM.YLAT;
                }
                XX = YLAZ / PARM.CLT;
                PARM.YLTS = Math.Sin(XX);
                PARM.YLTC = Math.Cos(XX);
                PARM.YTN = Math.Tan(XX);
                PARM.JDHU = 400;
                PARM.WDRM = PARM.HLMN;
                if (PARM.HLMN < 11.0)
                {
                    Functions.ADAJ(ref PARM.NC, ref PARM.JDHU, PARM.JT1, 15.0, (double)PARM.NYD);
                    PARM.WDRM = PARM.PRMT[5] + PARM.HLMN;
                }
                AAP = XYP[2];
                XYP[5] = 115.0 * XYP[5];
                PARM.AVT = (XYP[0] + XYP[1]) * .5;
                //!     UAVM = AV MO WIND SPEED (M/S)(REQUIRED TO SIMULATE WIND                        
                //!            EROSION--ACW>0 LINE 24  AND POTENTIAL ET if PENMAN OR                   
                //!            PENMAN-MONTEITH EQS ARE USED--LINE 4)                                   
                //!     LINE 3                                                                         
                //READ(KR(25),310)UAVM                                                           
                double AWV = 0.0;
                PARM.WB = 0.0;
                for (I = 1; I <= 12; I++)
                {
                    PARM.RNCF[I - 1] = 1.0;
                    PARM.TMNF[I - 1] = 1.0;
                    if (PARM.UAVM[I - 1] < 1.0E-5) PARM.UAVM[I - 1] = UAV0[I - 1];
                    PARM.SMY[I - 1] = 0.0;
                    for (J = 1; J <= 100; J++)
                    {
                        double RN2 = Functions.AUNIF(PARM.IDG[4]);
                        double WV = PARM.UAVM[I - 1] * Math.Pow((-Math.Log(RN2)), PARM.UXP);
                        if (WV < PARM.PRMT[66]) continue;
                        double EV = 193.0 * Math.Exp(1.103 * (WV - 30.0) / (WV + 1.0));
                        PARM.SMY[I - 1] = PARM.SMY[I - 1] + EV;
                    }
                    PARM.WB = PARM.WB + PARM.SMY[I - 1];
                    AWV = AWV + PARM.UAVM[I - 1];
                }
                AWV = AWV / 12.0;
                PARM.WCF = Math.Pow((3.86 * Math.Pow(AWV, 3) / Math.Pow(XYP[5], 2)), .3484);
                if (PARM.PRMT[39] > 0.0 && PARM.IRR == 0)
                {
                    X1 = Math.Max(1.0, PARM.AVT);
                    PARM.CLF = Math.Sqrt(AAP / (X1 * PARM.PRMT[39]));
                }
                else
                {
                    PARM.CLF = 1.0;
                }
                //WRITE(KW(1),'(T10,A,F7.3)')'CLIMATIC FACTOR = ',CLF                            
                //!     DIR  = AV MO FRACTION OF WIND FROM 16 DIRECTIONS (BLANK UNLESS                 
                //!            WIND EROSION IS SIMULATED--ACW>0 LINE 24).                              
                for (J = 1; J <= 16; J++)
                {
                    //!     LINES 4/19                                                                     
                    //READ(KR(25),310)(DIR(I,J),I=1,12)                                            
                    if (PARM.DIR[0, J - 1] > 0.0) continue;
                    for (I = 1; I <= 12; I++)
                    {
                        PARM.DIR[I - 1, J - 1] = 1.0;
                    }
                }
                //REWIND KR(25)                                                                  
                //WRITE(KW(1),'(/T20,A,A12)')'WIND = ',WINDFILE                                  
                //WRITE(KW(1),321)HED(7),(UAVM(I),I=1,12),AWV,HED(7)                             
                if (PARM.ICDP == 0)
                {
                    //WRITE(KW(1),'(/T10,A)')'RAINFALL DIST IS SKEWED NORMAL'                           
                }
                else
                {
                    //WRITE(KW(1),'(/T10,A,F5.2)')'RAINFALL DIST IS EXP--PARM = ',EXPK             
                    //WRITE(KW(1),'(/T10,A,F5.3)')'WET-DRY PROB COEF = ',BTA                       
                }
                PARM.AHSM = Functions.CAHU(1, 365.0, 0.0, 1.0);
                //WRITE(KW(1),221)SUM,AHSM                                                       
                //WRITE(KW(1),'(//1X,A)')'-----WIND EROSION DATA'                                
                //WRITE(KW(1),285)FL,FW,ANG0,UXP,DIAM,ACW                                        
                for (I = 1; I <= 12; I++)
                {
                    if (PARM.UAVM[0] > 0.0)
                    {
                        Functions.AEXINT(ref PARM.UXP, out SUM);
                        PARM.UAVM[I - 1] = PARM.UAVM[I - 1] / SUM;
                    }

                    for (J = 2; J <= 16; J++)
                    {
                        PARM.DIR[I - 1, J - 1] = PARM.DIR[I - 1, J - 1] + PARM.DIR[I - 1, J - 1 - 1];
                    }
                    for (J = 1; J <= 16; J++)
                    {
                        PARM.DIR[I - 1, J - 1] = PARM.DIR[I - 1, J - 1] / PARM.DIR[I - 1, 16 - 1];
                    }
                }
                PARM.TX = (PARM.OBMX[0, PARM.MO - 1] + PARM.OBMN[0, PARM.MO - 1]) / 2.0;
                PARM.ST0 = PARM.OBSL[0, PARM.MO - 1];
                PARM.DST0 = PARM.TX;
                Functions.APAGE(0);
                //WRITE(KW(1),'(//1X,A/)')'____________________GENERAL INFORMATION______________________'                                                          
                FileStream KRX = PARM.KR[15];
                if (PARM.NSTP > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'REAL TIME SIMULATION MODE'                                 
                    //READ(KW(MSO+3),300)ISTP                                                           
                    if (PARM.ISTP > 0)
                    {
                        KRX = PARM.KR[21];
                        //CALL OPENV(KR(22),'RTOPSC.DAT          ',0,KW(MSO))                                      
                    }
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'NORMAL SIMULATION MODE'                               
                }
                if (PARM.PRMT[49] > 0.0)
                {
                    //WRITE(KW(1),'(T10,A)')'DYNAMIC TECHNOLOGY'                                        
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'STATIC TECHNOLOGY'                                         
                }
                //WRITE(KW(1),301)NBYR,IYR0,IMO0,IDA0                                            
                if (PARM.LPYR > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'LEAP YEAR IGNORED'                                         
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'LEAP YEAR CONSIDERED'                                 
                }
                //WRITE(KW(1),365)WSA,YLAT,XLOG,ELEV,CHL,CHS,CHN,CHD,UPSL,UPS,AZM,SN                 
                //WRITE(KW(1),'(T10,A,A4)')'WATER EROSION FACTORS--DRIVING EQ = ',HED(NDVSS)                                                                     
                if (PARM.NDRV == 6)//WRITE(KW(1),'(T15,A,4E13.5)')'MUSI COEFS = ',BUS                    
                    PARM.BUS[0] = BUS0 * Math.Pow(WSX, PARM.BUS[3]);
                //WRITE(KW(1),405)SL,RXM,RLF,RSF,TC                                              
                //WRITE(KW(1),'(T10,A)')'DAILY RUNOFF ESTIMATION'                                
                switch (PARM.INFL)
                {
                    case 1:
                        //WRITE(KW(1),'(T15,A)')'NRCS CURVE NUMBER EQ'  
                        break;
                    case 2:
                        //WRITE(KW(1),'(T15,A/T15,A)')'GREEN & AMPT EQ','RF EXP DST--PEAK RF RATE SIM'    
                        break;
                    case 3:
                        //WRITE(KW(1),'(T15,A/T15,A)')'GREEN & AMPT EQ','RF EXP DST--PEAK RF RATE INPUT'   
                        break;
                    case 4:
                        //WRITE(KW(1),'(T15,A/T15,A)')'GREEN & AMPT EQ','RF UNIF DST--PEAK RF RATE INP'   
                        break;
                }
                switch (PARM.NVCN + 1)
                {
                    case 1:
                        //WRITE(KW(1),'(T15,A)')'VARIABLE CN DEPTH/SOIL-WATER WEIGHT'  
                        break;
                    case 2:
                        //WRITE(KW(1),'(T15,A)')'VARIABLE CN NO DP/SW WEIGHT'  
                        break;
                    case 3:
                        //WRITE(KW(1),'(T15,A)')'VARIABLE CN LINEAR NO DP/SW WEIGHT'   
                        break;
                    case 4:
                        //WRITE(KW(1),'(T15,A)')'CONSTANT CN'    
                        break;
                    case 5:
                        //WRITE(KW(1),'(T15,A)')'VARIABLE CN RETN PAR INDEX NO DP/SW WEIGHT'  
                        break;
                }
                if (PARM.ISCN == 0)
                {
                    //WRITE(KW(1),'(T15,A)')'DAILY CN--STOCHASTIC'                                      
                }
                else
                {
                    //WRITE(KW(1),'(T15,A)')'DAILY CN--DETERMINISTIC'                              
                }
                if (PARM.ITYP > 0)
                {
                    //WRITE(KW(1),'(T10,A,A2)')'PEAK RATE EST WITH TR55--RF TYPE =',RFPT(ITYP)                                                                  
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'PEAK RATE EST WITH MOD RATIONAL EQ'                   
                }
                if (PARM.IERT > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'GLEAMS ENRICHMENT RATIO'                                   
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'EPIC ENRICHMENT RATIO'                                
                }
                if (PARM.ICG > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'WATER USE-BIOMASS CONVERSION'                         
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'RADIATION-BIOMASS CONVERSION'                         
                }
                if (PARM.ICF == 0)
                {
                    //WRITE(KW(1),'(T10,A)')'RUSLE C FACTOR USED FOR ALL EROS EQS'                      
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'EPIC C FACTOR USED EXCEPT FOR RUSLE'                  
                }
                switch (PARM.IDN + 1)
                {
                    case 1:
                        //WRITE(KW(1),'(T10,A,F5.2,A)')'IZAURRALDE DNIT DZ=',DZ,' M' 
                        break;
                    case 2:
                        //WRITE(KW(1),'(T10,A)')'KEMANIAN DNIT'    
                        break;
                    case 3:
                        //WRITE(KW(1),'(T10,A)')'EPIC DNIT'    
                        break;
                }
                if (PARM.NUPC > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'N & P UPTAKE CONC S CURVE'                                 
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'N & P UPTAKE CONC SMITH CURVE'                        
                }
                if (PARM.IOX > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'O2=F(C/CLA)'                                               
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'EPIC O2=F(Z)'                                         
                }
                //WRITE(KW(1),233)APM,SNO0,RFN0,CNO3I,CSLT                                       
                if (PARM.MASP > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'NUTRIENT & PESTICIDE OUTPUT (MASS & CONC)'                                   
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'NUTRIENT & PESTICIDE OUTPUT (MASS)'
                }
                if (PARM.LBP > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'MODIFIED NONLINEAR EQ SOL P RUNOFF'                        
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'GLEAMS PESTICIDE EQ SOL P RUNOFF'                     
                }
            //WRITE(KW(1),330)COL,COIR,FULP,WAGE,CSTZ(1),CSTZ(2)

        lbl531: Functions.AINLZ();
                Functions.AINIX();
                PARM.IYX = PARM.IYR0 - 1880;
                PARM.SNO = SNO0;

                for (J = 1; J <= 21; J++)
                {
                    PARM.IDG[J - 1] = J;
                }
                Functions.APAGE(0);
                /*                                                      
                !     RANDOM NUMBER GENERATOR ID NUMBERS                                             
                !     IDG = 1 DETERMINES WET AND DRY DAYS                                            
                !         = 2 RELATES WEATHER VARIABLES TO RAIN                                      
                !         = 3 RAINFALL AMOUNT                                                        
                !         = 4 RAINFALL ENERGY (EI)- PEAK RUNOFF RATE (QP)                            
                !         = 5 WIND SPEED                                                             
                !         = 6 WIND DIRECTION                                                         
                !         = 7 RELATIVE HUMIDITY                                                      
                !         = 8 RUNOFF CURVE NUMBER                                                    
                !         = 9 WITHIN DAY WIND SPEED DIST
                 */

                if (PARM.IGN > 0)
                {

                    for (J = 1; J <= 20; J++)
                    {

                        int RN = (int)Functions.AUNIF(21);
                        II = 100 * PARM.IGN * RN;
                        for (KK = 1; KK <= II; KK++)
                        {
                            XX = Functions.AUNIF(21);
                        }
                        PARM.IX[J - 1] = PARM.IX[20];
                    }
                    Functions.AISHFL();
                }
                for (J = 1; J <= 21; J++)
                {
                    PARM.IX0[J - 1] = PARM.IX[J - 1];
                }
                //WRITE(KW(1),297)IGN,(IX(IDG(I)),I=1,10),(IDG(I),I=1,10)                        
                PARM.V3 = Functions.AUNIF(PARM.IDG[2]);
                PARM.V1 = Functions.AUNIF(PARM.IDG[1]);
                PARM.BIG = Math.Max(.2, PARM.PRMT[23]);
                Functions.ADAJ(ref PARM.NC, ref PARM.IBD, PARM.IMO0, PARM.IDA0, PARM.NYD);
                PARM.JDA = PARM.IBD;
                PARM.MO = 1;
                Functions.AXMON(ref PARM.IBD, ref PARM.MO);
                PARM.MO1 = PARM.MO;
                XX = 0.0;
                PARM.RZ = 2.0;
                II = -1;
                while (II != INPS)
                {
                    //READ(KR(13),*,IOSTAT=NFL)II,SOILFILE                                         
                    if (NFL != 0)
                    {
                        if (PARM.IBAT == 0)
                        {
                            //WRITE(*,'(T10,A,I8,A)')'SOIL NO = ',INPS,' NOT IN SOIL LIST FILE'
                            return;
                        }
                        else
                        {
                            //WRITE(KW(MSO),'(A,A8,A,I4,A)')' !!!!! ',ASTN,'SOIL NO = ',INPS,' NOT IN SOIL LIST FILE'
                            goto lbl219;
                        }
                    }
                }
                //REWIND KR(13)                                                                  
                //CALL OPENV(KR(14),SOILFILE,IDIR(3),KW(MSO))                                            
                //READ(KR(14),505)VOID
                /*                                                 
                !     READ SOIL DATA                                                                 
                !  1  SALB = SOIL ALBEDO                                                             
                !  2  HSG  = HYDROLOGIC SOIL GROUP--1.=A; 2.=B; 3.=C; 4.=D                           
                !  3  FFC  = FRACTION OF FIELD CAP FOR INITAL WATER STORAGE (BLANK IF                
                !            UNKNOWN)                                                                
                !  4  WTMN = MIN DEPTH TO WATER TABLE(m)(BLANK IF UNKNOWN                            
                !  5  WTMX = MAX DEPTH TO WATER TABLE (m)(BLANK IF UNKNOWN                           
                !  6  WTBL = INITIAL WATER TABLE HEIGHT(m) (BLANK IF UNKNOWN)                        
                !  7  GWST = GROUNDWATER STORAGE (mm)                                                
                !  8  GWMX = MAXIMUM GROUNDWATER STORAGE (mm)                                        
                !  9  RFT0 = GROUNDWATER RESIDENCE TIME(d)(BLANK IF UNKNOWN)                         
                ! 10  RFPK = RETURN FLOW / (RETURN FLOW + DEEP PERCOLATION)                          
                ! 11  TSLA = NUMBER OF SOIL LAYERS AFTER SPLITTING (3-15)                            
                !          = 0 NO SPLITTING OCCURS INITIALLY                                         
                ! 12  XIDP = 0. FOR CALCAREOUS SOILS AND NON CALCAREOUS                              
                !               WITHOUT WEATHERING INFORMATION                                       
                !          = 1. FOR NON CACO3 SLIGHTLY WEATHERED                                     
                !          = 2. NON CACO3 MODERATELY WEATHERED                                       
                !          = 3. NON CACO3 HIGHLY WEATHERED                                           
                !          = 4. INPUT PSP OR ACTIVE + STABLE MINERAL P (kg/ha)                       
                ! 13  RTN0 = NUMBER YEARS OF CULTIVATION AT START OF SIMULATION.                     
                ! 14  XIDK = 1 FOR KAOLINITIC SOIL GROUP                                             
                !          = 2 FOR MIXED SOIL GROUP                                                  
                !          = 3 FOR SMECTITIC SOIL GROUP                                              
                ! 15  ZQT  = MINIMUM THICKNESS OF MAXIMUM LAYER (m)(SPLITTING                        
                !            STOPS WHEN ZQT IS REACHED)                                              
                ! 16  ZF   = MINIMUM PROFILE THICKNESS(m)--STOPS SIMULATION.                         
                ! 17  ZTK  = MINIMUM LAYER THICKNESS FOR BEGINNING SIMULATION LAYER                  
                !            SPLITTING--MODEL SPLITS FIRST LAYER WITH THICKNESS GREATER              
                !            THAN ZTK(m); IF NONE EXIST THE THICKEST LAYER IS SPLIT.                 
                ! 18  FBM  = FRACTION OF ORG C IN BIOMASS POOL(.03-.05)                              
                ! 19  FHP  = FRACTION OF HUMUS IN PASSIVE POOL(.3-.7)                                
                ! 20  XCC  = CODE WRITTEN AUTOMATICALLY BY .SOT (NOT USER INPUT)                     
                !     LINE 2/3
                 */
                double FFC, XCC, FBM, FHP, RTN0, TSLA, ZTK, RFT0;
                int XIDP, XIDK;
                //READ(KR(14),303)SALB,HSG,FFC,WTMN,WTMX,WTBL,GWST,GWMX,RFT0,RFPK,TSLA,XIDP,RTN0,XIDK,ZQT,ZF,ZTK,FBM,FHP,XCC                                     
                double NCC = XCC;
                if (PARM.GWST < 1.0E-10) PARM.GWST = 25.0;
                if (PARM.GWMX < 1.0E-10) PARM.GWMX = 50.0;
                if (PARM.WTMX < 1.0E-5)
                {
                    PARM.WTMN = 50.0;
                    PARM.WTMX = 100.0;
                    PARM.WTBL = 75.0;
                }
                PARM.IDSP = (int)(XIDP + 1.1); //Adding a double to two integer values...?
                if (FBM < 1.0E-10) FBM = .04;
                if (FHP < 1.0E-10) FHP = .7 - .3 * Math.Exp(-.0277 * RTN0);
                int IDSK = (int)Math.Max(XIDK, 1.0);
                if (FFC < 1.0E-10) FFC = AAP / (AAP + Math.Exp(9.043 - .002135 * AAP));
                for (I = 1; I <= PARM.MSL; I++)
                {
                    PARM.WNH3[I - 1] = 0.0;
                    PARM.SEV[I - 1] = 0.0;
                    PARM.U[I - 1] = 0.0;
                    PARM.LORG[I - 1] = I;
                    PARM.LID[I - 1] = I;
                    for (J = 1; J <= PARM.MPS; J++)
                    {
                        PARM.PSTZ[J - 1, I - 1] = 0.0;
                    }
                }
                for (int x = 0; x <= PARM.PFOL.Length; x++)
                    PARM.PFOL[x] = 0.0;
                double MXLA = TSLA;
                if (PARM.ZQT < 1.0E-10) PARM.ZQT = .1;
                if (PARM.ZF < 1.0E-10) PARM.ZF = .1;
                if (ZTK < 1.0E-10) ZTK = .15;
                PARM.RFTT = RFT0;
                if (RFT0 < 1.0E-10) PARM.RFTT = 10.0;
                /*                                             
                !     THE SOIL IS DIVIDED VERTICALLY INTO LAYERS (MAX OF 10 LAYERS                   
                !     OF USER SPECIFIED THICKNESS)                                                   
                !  4  Z    = DEPTH TO BOTTOM OF LAYERS(m)                                            
                !  5  BD   = BULK DENSITY (T/M**3)                                                   
                !  6  U    = SOIL WATER CONTENT AT WILTING POINT (1500 kpa)(m/m)                     
                !            (BLANK IF UNKNOWN)                                                      
                !  7  FC   = WATER CONTENT AT FIELD CAPACITY (33kpa)(m/m)                            
                !            (BLANK IF UNKNOWN)                                                      
                !  8  SAN  = % SAND                                                                  
                !  9  SIL  = % SILT                                                                  
                ! 10  WN   = INITIAL ORGANIC N CONC (g/t)        (BLANK IF UNKNOWN)                  
                ! 11  PH   = SOIL PH                                                                 
                ! 12  SMB  = SUM OF BASES (cmol/kg)              (BLANK IF UNKNOWN)                  
                ! 13  WOC  = ORGANIC CARBON CONC(%)                                                  
                ! 14  CAC  = CALCIUM CARBONATE (%)                                                   
                ! 15  CEC  = CATION EXCHANGE CAPACITY (cmol/kg) (BLANK IF UNKNOWN)                   
                ! 16  ROK  = COARSE FRAGMENTS (% VOL)           (BLANK IF UNKNOWN)                   
                ! 17  CNDS = INITIAL NO3 CONC (g/t)             (BLANK IF UNKNOWN)                   
                ! 18  PKRZ = INITIAL LABILE P CONC (g/t)        (BLANK IF UNKNOWN)                   
                ! 19  RSD  = CROP RESIDUE(t/ha)                 (BLANK IF UNKNOWN)                   
                ! 20  BDD  = BULK DENSITY (OVEN DRY)(T/M**3)    (BLANK IF UNKNOWN)                   
                ! 21  PSP  = P SORPTION RATIO <1.               (BLANK IF UNKNOWN)                   
                !          = ACTIVE & STABLE MINERAL P (kg/ha) >1.                                   
                ! 22  SATC = SATURATED CONDUCTIVITY (mm/h)      (BLANK IF UNKNOWN)                   
                ! 23  HCL  = LATERAL HYDRAULIC CONDUCTIVITY(mm/h)(BLANK IF UNKNOWN)                  
                ! 24  WP   = INITIAL ORGANIC P CONC (g/t)       (BLANK IF UNKNOWN)                   
                ! 25  EXCK = EXCHANGEABLE K CONC (g/t)                                               
                ! 26  ECND = ELECTRICAL COND (mmHO/CM)                                               
                ! 27  STFR = FRACTION OF STORAGE INTERACTING WITH NO3 LEACHING                       
                !                                               (BLANK IF UNKNOWN)                   
                ! 28  ST   = INITIAL SOIL WATER STORAGE (FRACTION OF FIELD CAP)                      
                ! 29  CPRV = FRACTION INFLOW PARTITIONED TO VERTICLE CRACK OR PIPE FLOW              
                ! 30  CPRH = FRACTION INFLOW PARTITIONED TO HORIZONTAL CRACK OR PIPE                 
                !            FLOW                                                                    
                ! 31  WLS  = STRUCTURAL LITTER(kg/ha)           (BLANK IF UNKNOWN)                   
                ! 32  WLM  = METABOLIC LITTER(kg/ha)            (BLANK IF UNKNOWN)                   
                ! 33  WLSL = LIGNIN CONTENT OF STRUCTURAL LITTER(kg/ha)(B I U)                       
                ! 34  WLSC = CARBON CONTENT OF STRUCTURAL LITTER(kg/ha)(B I U)                       
                ! 35  WLMC = C CONTENT OF METABOLIC LITTER(kg/ha)(B I U)                             
                ! 36  WLSLC= C CONTENT OF LIGNIN OF STRUCTURAL LITTER(kg/ha)(B I U)                  
                ! 37  WLSLNC=N CONTENT OF LIGNIN OF STRUCTURAL LITTER(kg/ha)(BIU)                    
                ! 38  WBMC = C CONTENT OF BIOMASS(kg/ha)(BIU)                                        
                ! 39  WHSC = C CONTENT OF SLOW HUMUS(kg/ha)(BIU)                                     
                ! 40  WHPC = C CONTENT OF PASSIVE HUMUS(kg/ha)(BIU)                                  
                ! 41  WLSN = N CONTENT OF STRUCTURAL LITTER(kg/ha)(BIU)                              
                ! 42  WLMN = N CONTENT OF METABOLIC LITTER(kg/ha)(BIU)                               
                ! 43  WBMN = N CONTENT OF BIOMASS(kg/ha)(BIU)                                        
                ! 44  WHSN = N CONTENT OF SLOW HUMUS(kg/ha)(BIU)                                     
                ! 45  WHPN = N CONTENT OF PASSIVE HUMUS(kg/ha)(BIU)
                ! 46  CGO2 = O2 CONC IN GAS PHASE (g/m3 OF SOIL AIR)
                ! 47  CGCO2= CO2 CONC IN GAS PHASE (g/m3 OF SOIL AIR)
                ! 48  CGN2O= N2O CONC IN GAS PHASE (g/m3 OF SOIL AIR)                                  
                ! 49  OBC  = OBSERVED C CONTENT AT END OF SIMULATION (t/ha)                          
                !     LINES 4/49
                 */

                //READ(KR(14),625)Z,BD,U,(FC(J),J=1,15),SAN,SIL,WON,PH,SMB,WOC,CAC,CEC,ROK,CNDS,PKRZ,RSD,BDD,PSP,SATC,HCL,WP,EXCK,ECND,STFR,ST,CPRV,CPRH,WLS,WLM,WLSL,WLSC,WLMC,WLSLC,WLSLNC,(WBMC(J),J=1,15),WHSC,WHPC,WLSN,WLMN,WBMN,WHSN,WHPN
                for (int x = 0; x <= PARM.OBC.Length; x++)
                    PARM.OBC[x] = 0.0;
                if (PARM.IDN == 0)
                {
                    //READ(KR(14),625)(CGO2(J),J=1,15),(CGCO2(J),J=1,15),(CGN2O(J),J=1,15),OBC 
                }
                else
                {
                    for (int x = 0; x <= PARM.CGO2.Length; x++)
                        PARM.CGO2[x] = 0.0;
                    for (int x = 0; x <= PARM.CGCO2.Length; x++)
                        PARM.CGCO2[x] = 0.0;
                    for (int x = 0; x <= PARM.CGN2O.Length; x++)
                        PARM.CGN2O[x] = 0.0;
                }
                int L = 1;
                double XCB = .2;
                SUM = 0.0;
                double SOCF = 0.0;
                int LZ = 1;
                int K = 1;
                double PZW = 0.0;
                double TPAW = 0.0;
                double DG1 = 0.0; 
                int K1;

                for (J = 1; J <= PARM.MSL; J++)
                {
                    if (PARM.Z[J - 1] < 1.0E-10) break;
                    PARM.CLA[J - 1] = 100.0 - PARM.SAN[J - 1] - PARM.SIL[J - 1];
                    double DG = 1000.0 * (PARM.Z[J - 1] - XX);
                    PARM.OBC[J - 1] = 1000.0 * PARM.OBC[J - 1];
                    SOCF = SOCF + PARM.OBC[J - 1];
                    Functions.SBDSC(PARM.BD[J - 1], PARM.PRMT[1], ref F, J, 1);
                    Functions.SDST(ref PARM.RSD, DG, DG1, .01, .01, J, PARM.MSL);
                    Functions.SDST(ref PARM.PKRZ, DG, DG, 20.0, .001, J, PARM.MSL);
                    Functions.SDST(ref PARM.CNDS, DG, DG, 10.0, .001, J, PARM.MSL);
                    Functions.SDST(ref PARM.EXCK, DG, DG, 10.0, .001, J, PARM.MSL);
                    if (PARM.STFR[J - 1] < 1.0E-10) PARM.STFR[J - 1] = 1.0;
                    PARM.TRSD = PARM.TRSD + PARM.RSD[J - 1];
                    double ZD = .25 * (XX + PARM.Z[J - 1]);
                    F = ZD / (ZD + Math.Exp(-.8669 - 2.0775 * ZD));
                    PARM.STMP[J - 1] = F * (PARM.AVT - PARM.TX) + PARM.TX;
                    if (PARM.WOC[J - 1] < 1.0E-5) PARM.WOC[J - 1] = XCB * Math.Exp(-.001 * DG);
                    XCB = PARM.WOC[J - 1];
                    double XZ = PARM.WOC[J - 1] * .0172;
                    double ZZ = 1.0 - XZ;
                    PARM.BDM[J - 1] = ZZ / (1.0 / PARM.BD[J - 1] - XZ / .224);
                    if (PARM.BDM[J - 1] < 1.0)
                    {
                        PARM.BDM[J - 1] = 1.0;
                        PARM.BD[J - 1] = 1.0 / (ZZ + XZ / .224);
                    }
                    PARM.WT[J - 1] = PARM.BD[J - 1] * DG * 10.0;
                    DG1 = DG;
                    double WT1 = PARM.WT[J - 1] / 1000.0;
                    X1 = 10.0 * PARM.WOC[J - 1] * PARM.WT[J - 1];
                    PARM.WOC[J - 1] = X1;

                    if (PARM.WON[J - 1] > 0.0)
                    {
                        PARM.WON[J - 1] = WT1 * PARM.WON[J - 1];
                        KK = 0;
                    }
                    else
                    {
                        PARM.WON[J - 1] = .1 * PARM.WOC[J - 1];
                        KK = 1;
                    }
                    if (NCC == 0)
                    {
                        double WBM = FBM * X1;
                        PARM.WBMC[J - 1] = WBM;
                        double RTO;
                        if (KK == 0)
                        {
                            RTO = PARM.WON[J - 1] / PARM.WOC[J - 1];
                        }
                        else
                        {
                            RTO = .1;
                        }
                        PARM.WBMN[J - 1] = RTO * PARM.WBMC[J - 1];
                        double WHP = FHP * (X1 - WBM);
                        double WHS = X1 - WBM - WHP;
                        PARM.WHSC[J - 1] = WHS;
                        PARM.WHSN[J - 1] = RTO * PARM.WHSC[J - 1];
                        PARM.WHPC[J - 1] = WHP;
                        PARM.WHPN[J - 1] = RTO * PARM.WHPC[J - 1];
                        X1 = PARM.RSD[J - 1];
                        if (J == 1) X1 = X1 + PARM.STD0;
                        PARM.WLM[J - 1] = 500.0 * X1;
                        PARM.WLS[J - 1] = PARM.WLM[J - 1];
                        PARM.WLSL[J - 1] = .8 * PARM.WLS[J - 1];
                        PARM.WLMC[J - 1] = .42 * PARM.WLM[J - 1];
                        PARM.WLMN[J - 1] = .1 * PARM.WLMC[J - 1];
                        PARM.WLSC[J - 1] = .42 * PARM.WLS[J - 1];
                        PARM.WLSLC[J - 1] = .8 * PARM.WLSC[J - 1];
                        PARM.WLSLNC[J - 1] = .2 * PARM.WLSC[J - 1];
                        PARM.WLSN[J - 1] = PARM.WLSC[J - 1] / 150.0;
                        PARM.WOC[J - 1] = PARM.WOC[J - 1] + PARM.WLSC[J - 1] + PARM.WLMC[J - 1];
                        PARM.WON[J - 1] = PARM.WON[J - 1] + PARM.WLSN[J - 1] + PARM.WLMN[J - 1];
                    }
                    PARM.FOP[J - 1] = PARM.RSD[J - 1] * 1.1;
                    PARM.SEV[3] = PARM.SEV[3] + PARM.FOP[J - 1];
                    PARM.PMN[J - 1] = 0.0;
                    if (PARM.WP[J - 1] > 0.0)
                    {
                        PARM.WP[J - 1] = WT1 * PARM.WP[J - 1];
                    }
                    else
                    {
                        PARM.WP[J - 1] = .125 * PARM.WON[J - 1];
                    }
                    PARM.PO[J - 1] = 1.0 - PARM.BD[J - 1] / 2.65;
                    ZZ = .5 * (XX + PARM.Z[J - 1]);
                    X2 = .1 * PARM.WOC[J - 1] / PARM.WT[J - 1];
                    X1 = Math.Min(.8 * PARM.CLA[J - 1], 5.0 + 2.428 * X2 + 1.7 * ZZ);
                    PARM.CEC[J - 1] = Math.Max(PARM.CEC[J - 1], X1);
                    switch (PARM.ISW + 1)
                    {
                        case 1:
                        case 5:
                            Functions.SWRTNR(ref PARM.CLA[J - 1], ref PARM.SAN[J - 1], ref X2, ref PARM.U[J - 1], ref PARM.FC[J - 1]);
                            break;
                        case 2:
                        case 6:
                            PARM.CEM[J - 1] = Math.Max(.1, PARM.CEC[J - 1] - 2.428 * X2 - 1.7 * ZZ);
                            Functions.SWRTNB(PARM.CEM[J - 1], PARM.CLA[J - 1], X2, PARM.SAN[J - 1], ref PARM.U[J - 1],ref PARM.FC[J - 1], ZZ);
                            break;
                        case 8:
                        case 9:
                            Functions.SWNN(PARM.CLA[J - 1],PARM.SAN[J - 1], X2, ref PARM.U[J - 1], ref PARM.FC[J - 1]);
                            break;
                    }
                    if (PARM.ROK[J - 1] > 99.0) PARM.ROK[J - 1] = 90.0;
                    double XY = 1.0 - PARM.ROK[J - 1] * .01;
                    PARM.U[J - 1] = PARM.U[J - 1] * XY;
                    XY = XY * DG;
                    PARM.FC[J - 1] = PARM.FC[J - 1] * XY;
                    PARM.CAP = PARM.CAP + PARM.FC[J - 1];
                    PARM.S15[J - 1] = PARM.U[J - 1] * DG;
                    PARM.PO[J - 1] = PARM.PO[J - 1] * XY;
                    Functions.SPOFC(ref J);
                    if (PARM.ST[J - 1] < 1.0E-10 && NCC == 0) PARM.ST[J - 1] = FFC;
                    PARM.ST[J - 1] = PARM.ST[J - 1] * (PARM.FC[J - 1] - PARM.S15[J - 1]) + PARM.S15[J - 1];
                    PARM.SEV[0] = PARM.SEV[0] + XY;
                    PARM.SEV[2] = PARM.SEV[2] + PARM.WT[J - 1];
                    if (PARM.CEC[J - 1] < 1.0E-20) PARM.CEC[J - 1] = PARM.UPS * PARM.SATC[J - 1];
                    double BSA;

                    if (PARM.CEC[J - 1] > 0.0)
                    {
                        if (PARM.CAC[J - 1] > 0.0) PARM.SMB[J - 1] = PARM.CEC[J - 1];
                        if (PARM.SMB[J - 1] > PARM.CEC[J - 1]) PARM.SMB[J - 1] = PARM.CEC[J - 1];
                        BSA = PARM.SMB[J - 1] * 100.0 / (PARM.CEC[J - 1] + 1.0E-20);
                        if (PARM.PH[J - 1] > 5.6)
                        {
                            PARM.ALS[J - 1] = 0.0;
                        }
                        else
                        {
                            X1 = .1 * PARM.WOC[J - 1] / PARM.WT[J - 1];
                            PARM.ALS[J - 1] = 154.2 - 1.017 * BSA - 3.173 * X1 - 14.23 * PARM.PH[J - 1];
                            if (PARM.ALS[J - 1] < 0.0)
                            {
                                PARM.ALS[J - 1] = 0.0;
                            }
                            else
                            {
                                if (PARM.ALS[J - 1] > 95.0) PARM.ALS[J - 1] = 95.0;
                            }
                        }
                    }
                    else
                    {
                        PARM.CEC[J - 1] = PARM.PH[J - 1];
                        PARM.SMB[J - 1] = PARM.CEC[J - 1];
                        PARM.ALS[J - 1] = 0.0;
                    }
                    switch (PARM.IDSP)
                    {
                        case 1:
                            if (PARM.CAC[J - 1] > 0.0)
                            {
                                PARM.PSP[J - 1] = .58 - .0061 * PARM.CAC[J - 1];
                                PARM.BPC[J - 1] = .00076;
                            }
                            else
                            {
                                PARM.PSP[J - 1] = .5;
                                PARM.BPC[J - 1] = Math.Exp(-1.77 * PARM.PSP[J - 1] - 7.05);
                            }
                            break;
                        case 2:
                            PARM.PSP[J - 1] = .02 + .0104 * PARM.PKRZ[J - 1];
                            PARM.BPC[J - 1] = Math.Exp(-1.77 * PARM.PSP[J - 1] - 7.05);
                            break;
                        case 3:
                            PARM.PSP[J - 1] = .0054 * BSA + .116 * PARM.PH[J - 1] - .73;
                            PARM.BPC[J - 1] = Math.Exp(-1.77 * PARM.PSP[J - 1] - 7.05);
                            break;
                        case 4:
                            PARM.PSP[J - 1] = .46 - .0916 * Math.Log(PARM.CLA[J - 1]);
                            PARM.BPC[J - 1] = Math.Exp(-1.77 * PARM.PSP[J - 1] - 7.05);
                            break;
                        case 5:
                            if (PARM.PSP[J - 1] < 1.0)
                            {
                                if (PARM.CAC[J - 1] > 0.0)
                                {
                                    PARM.PSP[J - 1] = .58 - .0061 * PARM.CAC[J - 1];
                                    PARM.BPC[J - 1] = .00076;
                                }
                                else
                                {
                                    PARM.PSP[J - 1] = .5;
                                    PARM.BPC[J - 1] = Math.Exp(-1.77 * PARM.PSP[J - 1] - 7.05);
                                }
                            }
                            else
                            {
                                PARM.PMN[J - 1] = .2 * PARM.PSP[J - 1];
                                PARM.PSP[J - 1] = PARM.PKRZ[J - 1] / (PARM.PMN[J - 1] + PARM.PKRZ[J - 1]);
                            }
                            break;
                    }
                    if (PARM.PSP[J - 1] < .05) PARM.PSP[J - 1] = .05;
                    if (PARM.PSP[J - 1] > .75) PARM.PSP[J - 1] = .75;
                    if (PARM.PMN[J - 1] < 1.0E-5) PARM.PMN[J - 1] = PARM.PKRZ[J - 1] * (1.0 - PARM.PSP[J - 1]) / PARM.PSP[J - 1];
                    PARM.OP[J - 1] = 4.0 * PARM.PMN[J - 1];

                    switch (IDSK)
                    {
                        case 1:
                            PARM.SOLK[J - 1] = Math.Max(.05 * PARM.EXCK[J - 1], .052 * PARM.EXCK[J - 1] - .12);
                            PARM.FIXK[J - 1] = 374.0 + 236.0 * PARM.CLA[J - 1];
                            break;
                        case 2:
                            PARM.SOLK[J - 1] = .026 * PARM.EXCK[J - 1] + .5;
                            PARM.FIXK[J - 1] = 1781.0 + 316.0 * PARM.CLA[J - 1];
                            break;
                        case 3:
                            PARM.SOLK[J - 1] = .026 * PARM.EXCK[J - 1] + .5;
                            PARM.FIXK[J - 1] = 1781.0 + 316.0 * PARM.CLA[J - 1];
                            break;
                    }
                    PARM.EQKS[J - 1] = PARM.SOLK[J - 1] / PARM.EXCK[J - 1];
                    PARM.EQKE[J - 1] = PARM.EXCK[J - 1] / PARM.FIXK[J - 1];
                    PARM.PMN[J - 1] = PARM.PMN[J - 1] * WT1;
                    PARM.OP[J - 1] = PARM.OP[J - 1] * WT1;
                    PARM.AP[J - 1] = PARM.PKRZ[J - 1] * WT1;
                    PARM.TAP = PARM.TAP + PARM.AP[J - 1];
                    PARM.WSLT[J - 1] = 6.4 * PARM.ECND[J - 1] * PARM.ST[J - 1];
                    PARM.TSLT = PARM.TSLT + PARM.WSLT[J - 1];
                    PARM.TMP = PARM.TMP + PARM.PMN[J - 1];
                    PARM.EXCK[J - 1] = PARM.EXCK[J - 1] * WT1;
                    PARM.SOLK[J - 1] = PARM.SOLK[J - 1] * WT1;
                    PARM.FIXK[J - 1] = PARM.FIXK[J - 1] * WT1;
                    PARM.WNO3[J - 1] = PARM.CNDS[J - 1] * WT1;
                    PARM.TNO3 = PARM.TNO3 + PARM.WNO3[J - 1];
                    if (PARM.Z[J - 1] <= PARM.RZ)
                    {
                        PARM.RZSW = PARM.RZSW + PARM.ST[J - 1] - PARM.S15[J - 1];
                        PARM.PAW = PARM.PAW + PARM.FC[J - 1] - PARM.S15[J - 1];
                        LZ = J;
                    }
                    if (PARM.BDD[J - 1] < 1.0E-5) PARM.BDD[J - 1] = PARM.BD[J - 1];
                    PARM.BDP[J - 1] = PARM.BD[J - 1];
                    PARM.BDD[J - 1] = PARM.BDD[J - 1] / PARM.BD[J - 1];
                    PARM.TOP = PARM.TOP + PARM.OP[J - 1];
                    PARM.TP = PARM.TP + PARM.WP[J - 1];
                    PARM.TSK = PARM.TSK + PARM.SOLK[J - 1];
                    PARM.TEK = PARM.TEK + PARM.EXCK[J - 1];
                    PARM.TFK = PARM.TFK + PARM.FIXK[J - 1];
                    PARM.ZLS = PARM.ZLS + PARM.WLS[J - 1];
                    PARM.ZLM = PARM.ZLM + PARM.WLM[J - 1];
                    PARM.ZLSL = PARM.ZLSL + PARM.WLSL[J - 1];
                    PARM.ZLSC = PARM.ZLSC + PARM.WLSC[J - 1];
                    PARM.ZLMC = PARM.ZLMC + PARM.WLMC[J - 1];
                    PARM.ZLSLC = PARM.ZLSLC + PARM.WLSLC[J - 1];
                    PARM.ZLSLNC = PARM.ZLSLNC + PARM.WLSLNC[J - 1];
                    PARM.ZBMC = PARM.ZBMC + PARM.WBMC[J - 1];
                    PARM.ZHSC = PARM.ZHSC + PARM.WHSC[J - 1];
                    PARM.ZHPC = PARM.ZHPC + PARM.WHPC[J - 1];
                    PARM.ZLSN = PARM.ZLSN + PARM.WLSN[J - 1];
                    PARM.ZLMN = PARM.ZLMN + PARM.WLMN[J - 1];
                    PARM.ZBMN = PARM.ZBMN + PARM.WBMN[J - 1];
                    PARM.ZHSN = PARM.ZHSN + PARM.WHSN[J - 1];
                    PARM.ZHPN = PARM.ZHPN + PARM.WHPN[J - 1];

                    if (K <= 3)
                    {
                        TPAW = TPAW + PARM.FC[J - 1] - PARM.S15[J - 1];

                        for (K1 = K; K1 <= 3; K++)
                        {
                            if (TPAW < PARM.WCS[K1 - 1]) break;
                            PARM.ZCS[K1 - 1] = XX + (PARM.Z[J - 1] - XX) * ((PARM.WCS[K1 - 1] - PZW) / (TPAW - PZW));
                        }
                        K = K1;
                    }
                    PZW = TPAW;
                    XX = PARM.Z[J - 1];
                }


                if (J > PARM.MSL)
                {
                    PARM.NBSL = 10;
                }
                else
                {
                    int L1 = LZ + 1;
                    PARM.NBSL = J - 1;
                    if (L1 != J)
                    {
                        double ZZ = PARM.RZ - PARM.Z[LZ - 1];
                        double RTO = ZZ / (PARM.Z[L1 - 1] - PARM.Z[LZ - 1]);
                        PARM.RZSW = PARM.RZSW + (PARM.ST[L1 - 1] - PARM.S15[L1 - 1]) * RTO;
                        PARM.PAW = PARM.PAW + RTO * (PARM.FC[L1 - 1] - PARM.S15[L1 - 1]);
                    }
                }
                PARM.LRD = PARM.NBSL;
                if (MXLA < PARM.NBSL) MXLA = PARM.NBSL;
                PARM.LD1 = 1;
                if (PARM.Z[0] < .01)
                {
                    PARM.Z[0] = .01;
                }
                else
                {
                    if (PARM.Z[0] > .01)
                    {
                        PARM.NBSL = PARM.NBSL + 1;
                        for (J = PARM.NBSL; J >= 2; J--)
                        {
                            PARM.LID[J - 1] = PARM.LID[J - 2];
                        }
                        PARM.LID[0] = PARM.NBSL;
                        PARM.LD1 = PARM.NBSL;
                        PARM.LORG[PARM.NBSL - 1] = 1;
                        double RTO = .01 / PARM.Z[0];
                        Functions.SPLA(1, 1, PARM.NBSL, 0, RTO);
                        PARM.Z[PARM.NBSL - 1] = .01;
                    }
                }
                while (PARM.NBSL < MXLA)
                {
                    int L1 = PARM.LID[0];
                    double ZMX = 0.0;
                    int MXZ = 2;
                    for (J = 2; J <= PARM.NBSL; J++)
                    {
                        L = PARM.LID[J - 1];
                        double ZZ = PARM.Z[L - 1] - PARM.Z[L1 - 1];
                        if (ZZ >= ZTK)
                        {
                            MXZ = J;
                            goto lbl130;
                        }
                        else
                        {
                            if (ZZ > ZMX + .01)
                            {
                                ZMX = ZZ;
                                MXZ = J;
                            }
                        }
                        L1 = L;
                    }
                    L = PARM.LID[MXZ - 1];
                    L1 = PARM.LID[MXZ - 2];
                lbl130: PARM.NBSL = PARM.NBSL + 1;
                    Functions.SPLA(L, L1, PARM.NBSL, 1, .5);
                    for (J = PARM.NBSL; J >= MXZ; J--)
                    {
                        PARM.LID[J - 1] = PARM.LID[J - 2];
                    }
                    PARM.LID[MXZ - 1] = PARM.NBSL;
                    PARM.LORG[PARM.NBSL - 1] = PARM.LORG[L - 1];
                }

                for (I = 1; I <= 13; I++)
                {
                    for (J = 1; J <= 16; J++)
                    {
                        XZP[I - 1, J - 1] = 0.0;
                    }
                }
                double Z1 = 0.0;
                int MXP;
                for (J = 1; J <= PARM.NBSL; J++)
                {
                    L = PARM.LID[J - 1];
                    double ZZ = PARM.Z[L - 1] - Z1;
                    ACO2[L - 1] = PARM.CGO2[L - 1];
                    XTP1[L - 1] = PARM.CGCO2[L - 1];
                    XTP2[L - 1] = PARM.CGN2O[L - 1];
                    if (NCC > 0)
                    {
                        PARM.WOC[L - 1] = PARM.WLSC[L - 1] + PARM.WLMC[L - 1] + PARM.WBMC[L - 1] + PARM.WHSC[L - 1] + PARM.WHPC[L - 1];
                        PARM.WON[L - 1] = PARM.WLSN[L - 1] + PARM.WLMN[L - 1] + PARM.WBMN[L - 1] + PARM.WHSN[L - 1] + PARM.WHPN[L - 1];
                    }
                    else
                    {
                        PARM.WLSC[L - 1] = .42 * PARM.WLS[L - 1];
                        PARM.WLMC[L - 1] = .42 * PARM.WLM[L - 1];
                        PARM.WLSLC[L - 1] = .42 * PARM.WLSL[L - 1];
                        PARM.WLSLNC[L - 1] = PARM.WLSC[L - 1] - PARM.WLSLC[L - 1];
                    }
                    XZP[0, L - 1] = PARM.WHSC[L - 1];
                    XZP[1, L - 1] = PARM.WHPC[L - 1];
                    XZP[2, L - 1] = PARM.WLSC[L - 1];
                    XZP[3, L - 1] = PARM.WLMC[L - 1];
                    XZP[4, L - 1] = PARM.WBMC[L - 1];
                    XZP[5, L - 1] = PARM.WOC[L - 1];
                    XZP[6, L - 1] = PARM.WHSN[L - 1];
                    XZP[7, L - 1] = PARM.WHPN[L - 1];
                    XZP[8, L - 1] = PARM.WLSN[L - 1];
                    XZP[9, L - 1] = PARM.WLMN[L - 1];
                    XZP[10, L - 1] = PARM.WBMN[L - 1];
                    XZP[11, L - 1] = PARM.WON[L - 1];
                    XZP[12, L - 1] = PARM.WOC[L - 1] / PARM.WON[L - 1];
                    PARM.SOL[0, L - 1] = PARM.WHSC[L - 1];
                    PARM.SOL[1, L - 1] = PARM.WHPC[L - 1];
                    PARM.SOL[2, L - 1] = PARM.WLSC[L - 1];
                    PARM.SOL[3, L - 1] = PARM.WLMC[L - 1];
                    PARM.SOL[4, L - 1] = PARM.WBMC[L - 1];
                    PARM.SOL[5, L - 1] = PARM.WOC[L - 1];
                    PARM.SOL[6, L - 1] = PARM.WHSN[L - 1];
                    PARM.SOL[7, L - 1] = PARM.WHPN[L - 1];
                    PARM.SOL[8, L - 1] = PARM.WLSN[L - 1];
                    PARM.SOL[9, L - 1] = PARM.WLMN[L - 1];
                    PARM.SOL[10, L - 1] = PARM.WBMN[L - 1];
                    PARM.SOL[11, L - 1] = PARM.WON[L - 1];
                    PARM.SOL[12, L - 1] = PARM.PMN[L - 1];
                    PARM.SOL[13, L - 1] = PARM.WP[L - 1];
                    PARM.SOL[14, L - 1] = PARM.OP[L - 1];
                    PARM.SOL[15, L - 1] = PARM.EXCK[L - 1];
                    PARM.SOL[16, L - 1] = PARM.FIXK[L - 1];
                    PARM.SOL[17, L - 1] = PARM.ST[L - 1];
                    PARM.SOL[18, L - 1] = PARM.WLS[L - 1];
                    PARM.SOL[19, L - 1] = PARM.WLM[L - 1];
                    PARM.SOL[20, L - 1] = PARM.WLSL[L - 1];
                    PARM.SOL[21, L - 1] = PARM.WLSLC[L - 1];
                    PARM.SOL[22, L - 1] = PARM.WLSLNC[L - 1];
                    if (PARM.Z[L - 1] <= PARM.PMX)
                    {
                        SUM = SUM + PARM.WT[L - 1];
                        PARM.APB = PARM.APB + PARM.AP[L - 1];
                        PARM.OCPD = PARM.OCPD + PARM.WOC[L - 1];
                        PARM.PDSW = PARM.PDSW + PARM.ST[L - 1] - PARM.S15[L - 1];
                        PARM.FCSW = PARM.FCSW + PARM.FC[L - 1] - PARM.S15[L - 1];
                        MXP = J;
                    }
                    PARM.WNO2[L - 1] = 0.0;
                    PARM.WN2O[L - 1] = 0.0;
                    Z1 = PARM.Z[L - 1];
                }
                double PMX;
                if (MXP == PARM.NBSL)
                {
                    PMX = PARM.Z[PARM.LID[PARM.NBSL - 1] - 1];
                }
                else
                {
                    int L1 = PARM.LID[MXP];
                    X1 = 0.0;
                    if (MXP > 0) X1 = PARM.Z[PARM.LID[MXP - 1] - 1];
                    double RTO = (PMX - X1) / (PARM.Z[L1 - 1] - X1);
                    SUM = SUM + PARM.WT[L1 - 1] * RTO;
                    PARM.APB = PARM.APB + PARM.AP[L1 - 1] * RTO;
                    PARM.OCPD = PARM.OCPD + PARM.WOC[L1 - 1] * RTO;
                    PARM.PDSW = PARM.PDSW + RTO * (PARM.ST[L1 - 1] - PARM.S15[L1 - 1]);
                    PARM.FCSW = PARM.FCSW + RTO * (PARM.FC[L1 - 1] - PARM.S15[L1 - 1]);
                }
                double WPMX = .001 * SUM;
                //!     OCPD=.1*OCPD/SUM                                                               
                PARM.APBC = PARM.APB / WPMX;
                PARM.OCPD = .001 * PARM.OCPD;
                PARM.ABD = 1.0E-4 * PARM.SEV[2] / XX;
                PARM.TWN = PARM.ZLSN + PARM.ZLMN + PARM.ZBMN + PARM.ZHSN + PARM.ZHPN;
                PARM.TOC = PARM.ZLSC + PARM.ZLMC + PARM.ZBMC + PARM.ZHSC + PARM.ZHPC;
                PARM.TWN0 = PARM.TWN;
                XZP[0, 15] = PARM.ZHSC;
                XZP[1, 15] = PARM.ZHPC;
                XZP[2, 15] = PARM.ZLSC;
                XZP[3, 15] = PARM.ZLMC;
                XZP[4, 15] = PARM.ZBMC;
                XZP[5, 15] = PARM.TOC;
                XZP[6, 15] = PARM.ZHSN;
                XZP[7, 15] = PARM.ZHPN;
                XZP[8, 15] = PARM.ZLSN;
                XZP[9, 15] = PARM.ZLMN;
                XZP[10, 15] = PARM.ZBMN;
                XZP[11, 15] = PARM.TWN;
                XZP[12, 15] = PARM.TOC / PARM.TWN;
                PARM.NBCL = (int)(PARM.Z[PARM.LID[PARM.NBSL - 1] - 1] / PARM.DZ + .95);
                if (PARM.NBCL > 30)
                {
                    PARM.NBCL = 30;
                    PARM.DZ = PARM.Z[PARM.LID[PARM.NBSL - 1] - 1] / 30.0;
                }
                else
                {
                    if (PARM.NBCL < PARM.NBSL)
                    {
                        PARM.NBCL = PARM.NBSL;
                        X1 = PARM.NBCL;
                        PARM.DZ = PARM.Z[PARM.LID[PARM.NBSL - 1] - 1] / X1;
                    }
                }
                double TOT = 0.0;
                for (I = 1; I <= PARM.NBCL; I++)
                {
                    TOT = TOT + PARM.DZ;
                    PARM.ZC[I - 1] = TOT;
                }

                Functions.AINTRI(ref ACO2, ref PARM.CGO2, ref PARM.NBSL, ref PARM.NBCL);
                Functions.AINTRI(ref XTP1, ref PARM.CGCO2, ref PARM.NBSL, ref PARM.NBCL);
                Functions.AINTRI(ref XTP2, ref PARM.CGN2O, ref PARM.NBSL, ref PARM.NBCL);
                PARM.IUN = PARM.NBCL - 1;
                Functions.SPRNT(ref YTP);
                //WRITE(KW(1),'(//1X,A/)')'____________________SOIL DATA____________________'                                                                      
                //WRITE(KW(1),'(T10,A,A12)')'SOIL = ',SOILFILE                                   
                //WRITE(KW(1),290)SALB,TSLA,ZQT,ZF,ZTK,FBM,FHP,RTN0,XIDP,XIDK,PMX,OCPD                                                                           
                //WRITE(KW(1),351)WTMN,WTMX,WTBL,GWST,GWMX,RFTT                                  
                //if (PARM.ISTA > 0) WRITE(KW(1),'(T10,A)')'STATIC SOIL PROFILE'                          
                switch (PARM.ISW + 1)
                {
                    case 1:
                        //WRITE(KW(1),'(T10,A)')'FC/WP EST RAWLS METHOD DYNAMIC'  
                        break;
                    case 2:
                        //WRITE(KW(1),'(T10,A)')'FC/WP EST BAUMER METHOD DYNAMIC'  
                        break;
                    case 3:
                        //WRITE(KW(1),'(T10,A)')'FC/WP INP RAWLS METHOD DYNAMIC'  
                        break;
                    case 4:
                        //WRITE(KW(1),'(T10,A)')'FC/WP INP BAUMER METHOD DYNAMIC'    
                        break;
                    case 5:
                        //WRITE(KW(1),'(T10,A)')'FC/WP EST RAWLS METHOD STATIC'        
                        break;
                    case 6:
                        //WRITE(KW(1),'(T10,A)')'FC/WP EST BAUMER METHOD STATIC'       
                        break;
                    case 7:
                        //WRITE(KW(1),'(T10,A)')'FC/WP INP STATIC'                    
                        break;
                    case 8:
                        //WRITE(KW(1),'(T10,A)')'FC/WP INP NEAREST NEIGHBOR METHOD DYNAMIC'   
                        break;
                    case 9:
                        //WRITE(KW(1),'(T10,A)')'FC/WP INP NEAREST NEIGHBOR METHOD STATIC'   
                        break;
                }
                if (PARM.ISAT > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'SAT COND ESTIMATED WITH RAWLS METHOD'
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'SAT COND INPUT'
                }
                PARM.SATK = PARM.SATC[PARM.LID[1]];
                II = -1;

                while (II != IOPS)
                {
                    //READ(KR(15),*,IOSTAT=NFL)II,OPSCFILE                                         
                    if (NFL != 0)
                    {
                        if (PARM.IBAT == 0)
                        {
                            //WRITE(*,*)'OPS NO = ',IOPS,' NOT IN OPSC LIST FILE'
                            return;
                        }
                        else
                        {
                            //WRITE(KW(MSO),'(A,A8,A,I4,A)')' !!!!! ',ASTN,' OPS NO = ',IOPS,' NOT IN OPSC LIST FILE'
                            goto lbl219;
                        }
                    }
                }
                //REWIND KR(15)                                                                  
                //CALL OPENV(KR(16),OPSCFILE,IDIR(4),KW(MSO))                                            
                //!     LINE 1                                                                         
                //READ(KR(16),505)VOID                                                           
                //!     LINE 2                                                                         
                //READ(KR(16),300)LUN,IAUI  

                if (PARM.IAUI == 0) PARM.IAUI = 500;
                PARM.ISG = HSG;
                Functions.HSGCN();
                Functions.HCNSLP(ref PARM.CN2, ref X3);
                //WRITE(KW(1),390)ASG(ISG),LUN,CN2,X3,SCRP(30,1),SCRP(30,2),SCRP(4,1),SCRP(4,2);                                                                      
                PARM.CN2 = X3;
                PARM.CN0 = PARM.CN2;
                Functions.SOLIO(ref YTP, 1);
                //REWIND KR(14)                                                                  
                YTP[1] = YTP[1] * XX * 1000.0;
                PARM.SW = YTP[1];
                double SWW = YTP[1] + PARM.SNO;
                PARM.SLT0 = PARM.TSLT;
                if (XX < 1.0) PARM.SMX = PARM.SMX * Math.Sqrt(XX);
                PARM.SCI = Math.Max(3.0, PARM.SMX * (1.0 - PARM.RZSW / PARM.PAW));
                Functions.APAGE(1);
                for (int x = 0; x <= PARM.RNMN.Length; x++)
                    PARM.RNMN[x] = 0.0;

                if (IWRT == 0)
                {
                    IWRT = 1;
                    for (I = 2; I <= PARM.MSO; I++)
                    {
                        if (PARM.KFL[I - 1] == 0) continue;
                        if (I == 9)
                        {
                            XCC = 1.0;
                            X1 = 0.0;
                            //WRITE(KW(9),523)SOILFILE,IYER,IMON,IDAY                                        
                            //WRITE(KW(9),303)SALB,HSG,FFC,WTMN,WTMX,WTBL,GWST,GWMX,RFT0,RFPK,TSLA,XIDP,X1,XIDK,ZQT,ZF,ZTK,FBM,FHP,XCC                                       
                            continue;
                        }
                        //WRITE(KW(I),621)IYER,IMON,IDAY,IT1,IT2,IT3                                     
                        //WRITE(KW(I),'(T10,A8)')ASTN                                                    
                        //WRITE(KW(I),286)IRUN,IRO0,IGN                                                  
                        //WRITE(KW(I),592)SITEFILE                                                       
                        //WRITE(KW(I),592)WPM1FILE                                                       
                        //WRITE(KW(I),592)WINDFILE                                                       
                        //WRITE(KW(I),592)SOILFILE                                                       
                        //WRITE(KW(I),592)OPSCFILE                                                       
                        if (I == 14) Functions.SOCIOA(PARM.IYR0, 1, 1);
                        if (I == 15) Functions.SOCIOD(1);
                    }
                }

                //if(KFL(2)>0) WRITE(KW(2),557)HED(4),HED(10),HED(11),HED(14),HED(16),HED(17),HED(29),HED(33),HED(42),HED(48),HED(47),HED(50),HED(51),HED(52),HED(49),HED(43),HED(44),HED(45),HED(46),HED(56),HED(54),HED(55),HED(57),HED(66)                                                   
                //if(KFL(19)>0.AND.K19==0)WRITE(KW(19),551)                                           
                K19 = 1;
                //if(KFL(8)>0)WRITE(KW(8),558)(HED(KYA(J1)),J1=1,NKYA)                           
                //if(KFL(13)>0)WRITE(KW(13),583)                                                 
                //if(KFL(17)>0)WRITE(KW(17),826)(HED(KD(J1)),J1=1,NKD),'TNO3','NO31','PRK1','LN31',' HUI',' LAI','BIOM','YLDF','UNO3',(HEDS(I),I=23,28)                                                                       
                if (PARM.KFL[17] > 0)
                {
                    if (PARM.NGN > 0)
                    {
                        A1 = "INPUT WEATHER";
                        A2 = PARM.FWTH;
                    }
                    else
                    {
                        A1 = "GENERATED WEATHER";
                        A2 = " ";
                    }
                    //WRITE(KW(18),671)ASTN,YLAT,YLAZ,AZM,UPS,ZCS,A1,A2                              
                }
                //if(KFL(4)>0)WRITE(KW(4),669)                                                   
                //if(KFL(20)>0)WRITE(KW(20),583)                                                 
                //if(KFL(24)>0)WRITE(KW(24),726)                                                      
                //if(KFL(26)>0)WRITE(KW(26),729)                                                      
                //if(KFL(29)>0)WRITE(KW(29),727)                                                      
                if (PARM.KFL[22] > 0)
                {
                    //WRITE(KW(23),722)(SID(LORG(LID(J))),J=1,NBSL),SID(16)                               
                    //WRITE(KW(23),723)'DEPTH(m)',(Z(LID(I)),I=1,NBSL)                                    
                }
                //WRITE(KW(1),'(//1X,A/)')'____________________MANAGEMENT DATA_____________________'                                                               
                //WRITE(KW(1),'(T10,A,A20)')'OPSC = ',OPSCFILE
                double NCOW = PARM.WSA / RST0;
                if (PARM.IRR == 0)
                {
                    PARM.VIMX = 0.0;
                    //WRITE(KW(1),'(T10,A)')'DRYLAND AGRICULTURE'                                    
                    goto lbl142;
                }
                else
                {
                    if (PARM.VIMX < 1.0E-10) PARM.VIMX = 2000.0;
                    if (PARM.IRR == 4)
                    {
                        //WRITE(KW(1),331)                                                               
                        goto lbl137;
                    }
                }
                if (PARM.BIR < 0.0)
                {
                    //WRITE(KW(1),331)                                                               
                    //WRITE(KW(1),412)BIR,IRI                                                        
                    goto lbl137;
                }
                if (PARM.BIR > 0.0)
                {
                    //WRITE(KW(1),331)                                                               
                    if (PARM.BIR > 1.0)
                    {
                        //WRITE(KW(1),355)BIR,IRI                                                      
                    }
                    else
                    {
                        //WRITE(KW(1),360)BIR,IRI                                                      
                    }
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'USER SPECIFIED IRRIGATION'
                }

            lbl137: continue;//WRITE(KW(1),354)VIMX,ARMN,ARMX  

                if (PARM.IAC == 0)
                {
                    //WRITE(KW(1),'(T15,A)')'VARIABLE APPL VOLUMES'                                
                }
                else
                {
                    //WRITE(KW(1),'(T15,A)')'FIXED APPL VOLUMES'                                   
                }
                double VLGB, WTMB;
                switch (PARM.IRR)
                {
                    case 1:
                        //WRITE(KW(1),'(T10,A)')'SPRINKLER IRRIGATION'     
                        break;
                    case 2:
                        //WRITE(KW(1),'(T10,A)')'FURROW IRRIGATION'      
                        break;
                    case 3:
                        //WRITE(KW(1),'(T10,A)')'IRRIGATION WITH FERT ADDED'                       
                        //WRITE(KW(1),'(T15,A,F6.3)')'RUNOFF RATIO = ',EFI 
                        break;
                    case 5:
                        //WRITE(KW(1),'(T10,A)')'DRIP IRRIGATION'  
                        break;
                    case 4:
                        X3 = PARM.COWW * NCOW;
                        X1 = RFMX - 12.7;
                        double QLG = X1 * X1 / (RFMX + 50.8);
                        PARM.DALG = SOLQ * PARM.WSA;
                        X1 = 10.0 * PARM.DALG;
                        double QWW = 30.0 * X3 / X1;
                        PARM.VLGM = QLG + QWW;
                        PARM.VLGN = PARM.VLGN * PARM.VLGM;
                        //WRITE(KW(1),445)DALG,VLGN,VLGM,DDLG,COWW                                 
                        PARM.COWW = X3;
                        VLGB = PARM.VLGN;
                        PARM.VLGN = X1 * PARM.VLGN;
                        PARM.VLGM = X1 * PARM.VLGM;
                        PARM.VLG = PARM.VLGN;
                        PARM.CFNP = 100.0;
                        PARM.WTMU = PARM.CFNP * PARM.VLG;
                        WTMB = PARM.WTMU;
                        PARM.VLGI = (PARM.VLGM - PARM.VLGN) / (DDLG + 1.0E-5);
                        PARM.FNPI = NCOW * PARM.FNP * PARM.FFED;
                        PARM.AFLG = 365.0 * PARM.FNPI / PARM.WSA;
                        PARM.AILG = .1 * (AAP * PARM.DALG + 365.0 * X3) / PARM.WSA;
                        PARM.COWW = X3;
                        goto lbl433;
                    default:
                }
            lbl142: if (BFT0 < 1.0E-10)
                {
                    PARM.IAUF = 0;
                    if (PARM.MNU == 0)
                    {
                        //WRITE(KW(1),'(T10,A)')'USER SCHEDULED FERT'                                    
                        goto lbl433;
                    }
                }
                //WRITE(KW(1),'(T10,A/T15,A,I3,A)')'AUTO SCHEDULED FERT','MIN APPL INTERVAL = ',IFA,' D'                                                           
                PARM.IAUF = 1;
                if (BFT0 > 1.0)
                {
                    //WRITE(KW(1),'(T15,A,F4.0,A)')'SOIL NO3 CONC TRIGGER = ',BFT0,' g/t'                                                                       
                }
                else
                {
                    //WRITE(KW(1),'(T15,A,F4.2)')'PLANT STRESS TRIGGER = ',BFT0                    
                }
                if (FNP > 1.0)
                {
                    //WRITE(KW(1),'(T15,A,F8.1,A)')'FIXED RATE = ',FNP,' kg/ha'                    
                }
                else
                {
                    //WRITE(KW(1),'(T15,A)')'VARIABLE RATE'                                        
                }
            lbl433: //WRITE(KW(1),'(T15,A,F7.0,A)')'Math.Max N FERT/CROP = ',FMX,' kg/ha'
                if (PARM.IPAT > 0)
                {
                    //WRITE(KW(1),'(T10,A)')'AUTO P FERT'
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'MANUAL P FERT'
                }
                //WRITE(KW(1),436)NCOW,GZLM,FFED                                                 
                PARM.NII = PARM.IRI;
                PARM.FDSF = FDS0;
                if (FDS0 < 1.0E-10) PARM.FDSF = .9;
                PARM.IDR = IDR0;
                if (IDR0 > 0)
                {
                    X1 = .001 * IDR0;
                    for (I = 1; I <= PARM.NBSL; I++)
                    {
                        L = PARM.LID[I - 1];
                        if (PARM.Z[L - 1] > X1) break; ;
                    }
                    PARM.IDR = L;
                    PARM.HCLN = PARM.HCL[L - 1];
                    PARM.HCL[L - 1] = Math.Max(10.0 * PARM.SATC[L - 1], (PARM.PO[L - 1] - PARM.S15[L - 1]) / (24.0 * DRT));
                    PARM.HCLD = PARM.HCL[L - 1];
                    //WRITE(KW(1),327)L,DRT,HCL(L)                                                   
                    if (DRT < PARM.RFTT) PARM.RFTT = DRT;
                }
                if (PARM.IFD > 0)
                {
                    //WRITE(KW(1),'(T10,A,F5.2)')'FURROW DIKE SYSTEM SAFETY FACTOR = ',FDSF 
                }
                //WRITE(KW(1),'(T10,A,F5.3)')'USLE P FACTOR = ',PEC                              
                PARM.RFTT = 1.0 - Math.Exp(-1.0 / PARM.RFTT);
                PARM.FDSF = PARM.FDSF * 1000.0;
                if (PARM.LMS == 0)
                {
                    //WRITE(KW(1),'(T10,A)')'LIME APPLIED AS NEEDED'                               
                }
                else
                {
                    //WRITE(KW(1),'(T10,A)')'NO LIME APPLICATIONS'                                 
                }
                PARM.HU[0] = 0.0;
                int IBGN = 1;
                PARM.JJK = 1;
                I = 1;
                int IY1 = 1;
                K1 = 1;
            //WRITE(KW(1),'(/1X,A)')'-----OPERATION SCHEDULE'                                
            lbl189: int NCRP = PARM.IGO;
                I1 = I - 1;
                K = 1;
                //WRITE(KW(1),'(/T10,A,I2)')'YR',I                                                    
                PARM.KI = 0;
                PARM.KF = 0;
                PARM.KP = 0;
                double HU0 = 0.0;
                if (PARM.JDHU < 366) PARM.HU[PARM.JJK - 1] = 0.0;
                if (PARM.IGO > 0)
                {
                    for (J = 1; J <= PARM.MNC; J++)
                    {
                        if (PARM.NHU[J - 1] == 0) continue;
                        PARM.LY[I - 1, K - 1] = PARM.NHU[J - 1];
                        K = K + 1;
                    }
                    J = 0;
                }
                //WRITE(KW(1),316)
                J = 0;
                int JJK0;
                while (true)
                {
                    J = J + 1;
                    //!     READ OPERATION SCHEDULE                                                        
                    //!  1  JX(1)= YR OF OPERATION                                                         
                    //!  2  JX(2)= MO OF OPERATION                                                         
                    //!  3  JX(3)= DAY OF OPERATION                                                        
                    //!  4  JX(4)= EQUIPMENT ID NO                                                         
                    //!  5  JX(5)= TRACTOR ID NO                                                           
                    //!  6  JX(6)= CROP ID NO                                                              
                    //!  7  JX(7)= XMTU--TIME FROM PLANTING TO MATURITY (Y)(FOR TREE                       
                    //!            CROPS AT PLANTING ONLY).                                                
                    //!          = TIME FROM PLANTING TO HARVEST (Y)(HARVEST ONLY)                         
                    //!          = PESTICIDE ID NO (FOR PESTICIDE APPLICATION ONLY)                        
                    //!          = FERTILIZER ID NO (FOR FERTILIZER APPLICATION ONLY)                      
                    //!  8  OPV1 = POTENTIAL HEAT UNITS FOR PLANTING (BLANK if UNKNOWN)                    
                    //!          = APPLICATION VOLUME (mm)FOR IRRIGATION                                   
                    //!          = FERTILIZER APPLICATION RATE (kg/ha) = 0 FOR VARIABLE RATE               
                    //!          = PEST CONTROL FACTOR FOR PEST APPLICATION (FRACTION OF PESTS             
                    //!            CONTROLLED)                                                             
                    //!  9  OPV2 = LINE NUMBER FOR SCS HYDROLOGIC SOIL GROUP/RUNOFF CURVE                  
                    //!            NUMBER TABLE                                                            
                    //!          = SCS CURVE NUMBER (NEGATIVE)                                             
                    //!          = PESTICIDE APPLICATION RATE (kg/ha)                                      
                    //!  10 OPV3 = PLANT WATER STRESS FACTOR(0-1); SOIL WATER TENSION(>1kpa);              
                    //!            OR PLANT AVAILABLE WATER DEFICIT IN ROOT ZONE(-mm)TO                    
                    //!            TRIGGER AUTO PARM.IRR. (0. OR BLANK DOES NOT CHANGE TRIGGER)                 
                    //!  11 OPV4 = RUNOFF VOL/VOL IRRIGATION WATER APPLIED                                 
                    //!  12 OPV5 = PLANT POPULATION (PLANTS/M**2 OR PLANTS/HA if p/m2<1.)                  
                    //!            (FOR PLANTING ONLY)                                                     
                    //!  13 OPV6 = Math.Max ANNUAL N FERTILIZER APPLIED TO A CROP (0. OR BLANK                  
                    //!            DOES NOT CHANGE FMX; > 0 SETS NEW FMX)(FOR PLANTING ONLY                
                    //!  14 OPV7 = TIME OF OPERATION AS FRACTION OF GROWING SEASON (ENTER                  
                    //!            EARLIEST POSSIBLE MO & DAY -- JX(2) & JX(3))
                    //!  15 OPV8 = MINIMUM USLE C FACTOR
                    //!  16 OPV9 = MOISTURE CONTENT OF GRAIN REQUIRED FOR HARVEST                            
                    //!     LINE 3/L                                                                       
                    if (I == 1 || J > 1)
                    {
                        //READ(KRX,235,IOSTAT=NFL)JX,(OPV(L),L=1,9)
                    }
                    if (NFL != 0) break;

                    if (I == 1 && J == 1) JJK0 = PARM.JX[5];
                    if (PARM.JX[0] != IY1) break;
                    Functions.TILTBL();
                    PARM.LT[I - 1, J - 1] = PARM.NDT;
                    PARM.JH[I - 1, J - 1] = PARM.JX[5];
                    int IJ = PARM.LT[I - 1, J - 1];
                    Functions.ADAJ(ref PARM.NC, ref PARM.ITL[I - 1, J - 1], PARM.JX[1], PARM.JX[2], 1);
                    X4 = PARM.TLD[IJ - 1] * 1000.0;
                    int I3 = PARM.IHC[IJ - 1];
                    if (IBGN < PARM.ITL[I - 1, J - 1]) goto lbl419;
                    if (IBGN == PARM.ITL[I - 1, J - 1]) goto lbl422;
                    double BASE;
                    if (PARM.IGO > 0) PARM.HU[PARM.JJK - 1] = PARM.HU[PARM.JJK - 1] + Functions.CAHU(IBGN, 365, BASE, 0) / (PARM.PHU[PARM.JJK - 1, PARM.IHU[PARM.JJK - 1] - 1] + 1.0);
                    IBGN = 1; ;
                lbl419: if (PARM.IGO > 0) PARM.HU[PARM.JJK - 1] = PARM.HU[PARM.JJK - 1] + Functions.CAHU(IBGN,PARM.ITL[I - 1, J - 1],BASE, 0) / (PARM.PHU[PARM.JJK - 1, PARM.IHU[PARM.JJK - 1] - 1] + 1.0);
                    HU0 = HU0 + Functions.CAHU(IBGN,PARM.ITL[I - 1, J - 1], 0.0, 1) / PARM.AHSM;
                    IBGN = PARM.ITL[I - 1, J - 1];
                lbl422: if (PARM.OPV[6] > 0.0) goto lbl420;
                    if (PARM.IHUS == 0) goto lbl420;
                    if (PARM.IGO == 0) goto lbl441;
                    if (PARM.IDC[PARM.JJK - 1] == PARM.NDC[0] || PARM.IDC[PARM.JJK - 1] == PARM.NDC[1] || PARM.IDC[PARM.JJK - 1] == PARM.NDC[3] || PARM.IDC[PARM.JJK - 1] == PARM.NDC[4] || PARM.IDC[PARM.JJK - 1] == PARM.NDC[8]) goto lbl423;
                lbl441: PARM.HUSC[I - 1, J - 1] = HU0;
                    goto lbl421;
                lbl423: PARM.HUSC[I - 1, J - 1] = PARM.HU[PARM.JJK - 1];
                    goto lbl421;
                lbl420: PARM.HUSC[I - 1, J - 1] = PARM.OPV[6];
                    double JRT;
                lbl421: Functions.INIFP(I3, I, J, (int)JRT);
                    X1 = Math.Max(0.0, PARM.COOP[IJ - 1]);
                    X2 = Math.Max(0.0, PARM.COTL[IJ - 1]);
                    //!     PRINTOUT OPERATION SCHEDULE                                                    
                    //WRITE(KW(1),317)I,JX(2),JX(3),TIL(IJ),I3,JX(4),JX(5),X1,X2,EMX(IJ),RR(IJ),X4,FRCP(IJ),RHT(IJ),RIN(IJ),DKH(IJ),DKI(IJ),HE(IJ),ORHI(IJ),CN2,BIR,EFI,HUSC(I,J)                                                     
                    X1 = 0.0;
                    if (PARM.TLD[IJ - 1] > PARM.BIG) PARM.BIG = PARM.TLD[IJ - 1];
                    if (I3 != PARM.NHC[4] && I3 != PARM.NHC[5]) goto lbl180;
                    NCRP = NCRP + 1;
                    PARM.IGO = PARM.IGO + 1;
                    Functions.CPTBL();
                    PARM.NBCX[I - 1, PARM.JJK - 1] = PARM.NBCX[I - 1, PARM.JJK - 1] + 1;
                    BASE = PARM.TBSC[PARM.JJK - 1];
                    PARM.IPL = PARM.ITL[I - 1, J - 1] + 365 * I1;
                    PARM.IPLD[0, PARM.JJK - 1] = PARM.IPL;
                    PARM.LY[I - 1, K - 1] = PARM.JJK;
                    PARM.NHU[PARM.JJK - 1] = PARM.JJK;
                    K = K + 1;
                    X1 = PARM.SDW[PARM.JJK - 1] * PARM.CSTS[PARM.JJK - 1];
                    X2 = X1 + X2;
                    //WRITE(KW(1),236)X1,CPNM(JJK)                                                   
                    goto lbl584;
                lbl180: if (I3 != PARM.NHC[0] && I3 != PARM.NHC[1] && I3 != PARM.NHC[2]) goto lbl584;
                    if (PARM.KDC1[PARM.JX[5] - 1] == 0) goto lbl584;
                    PARM.JJK = PARM.KDC1[PARM.JX[5] - 1];
                    if (I3 == PARM.NHC[0])
                    {
                        PARM.IHV = PARM.ITL[I - 1, J - 1] + 365 * I1;
                        PARM.IHVD[0, PARM.JJK - 1] = PARM.IHV;
                        PARM.NHU[PARM.JJK - 1] = 0;
                        PARM.IGO = Math.Max(0, PARM.IGO - 1);
                        if (PARM.NBCX[I - 1, PARM.JJK - 1] == 0) PARM.NBCX[I - 1, PARM.JJK - 1] = 1;
                    }
                    PARM.HU[PARM.JJK - 1] = 0.0;
                    PARM.LYR[I - 1, J - 1] = Math.Max(0, PARM.JX[6]);
                //WRITE(KW(1),236)HU(JJK),CPNM(JJK)                                              
                lbl584: if (PARM.KFL[12] > 0)
                    {
                        //WRITE(KW(13),582)I,JX(2),JX(3),TIL(IJ),JX(6),I3,JX(4),JX(5),X2,COOP(IJ),X1
                    }
                }

                PARM.ITL[I - 1, J - 1] = 367;
                PARM.HUSC[I - 1, J - 1] = 10.0;
                PARM.NBC[I - 1] = NCRP;
                int J1 = J - 1;
                PARM.NTL[I - 1] = J1;
                PARM.NPST[I - 1] = PARM.KP;
                PARM.LT[I - 1, J - 1] = 1;
                PARM.JH[I - 1, J - 1] = 0;
                PARM.CND[I - 1, J - 1] = PARM.CN2;
                PARM.QIR[I - 1, J - 1] = PARM.EFI;
                PARM.TIR[I - 1, J - 1] = PARM.BIR;
                PARM.CFRT[I - 1, J - 1] = PARM.CFMN;
                PARM.HWC[I - 1, J - 1] = 0.0;
                PARM.MO = 1;
                if (PARM.KI == 0) goto lbl185;
                //WRITE(KW(1),247)                                                               
                for (L = 1; L <= J1; J++)
                {
                    int J2 = PARM.LT[I - 1, L - 1];
                    if (PARM.IHC[J2 - 1] != PARM.NHC[7]) continue;
                    PARM.JDA = PARM.ITL[I - 1, L - 1];
                    if (PARM.NYD == 0) PARM.JDA = PARM.JDA + 1;
                    Functions.AXMON(ref PARM.JDA, ref PARM.MO);
                    Functions.AICL();
                    double XZ = PARM.COIR * PARM.VIRR[I - 1, L - 1];
                    double XY = Math.Max(0.0, PARM.COTL[J2 - 1]) + XZ;
                    //!     PRINTOUT IRRIGATION SCHEDULE                                                   
                    //WRITE(KW(1),230)I,MO,KDA,VIRR(I,L),XY,HUSC(I,L)                                
                    if (PARM.KFL[12] > 0)
                    {
                        //WRITE(KW(13),503)I,MO,KDA,JH(I,L),IHC(J2),XY,XZ,VIRR(I,L)
                    }
                }
                PARM.MO = 1;
            lbl185: if (PARM.KF == 0) goto lbl187;
                //WRITE(KW(1),328)                                                               
                int JJ = 367;
                KK = 0;
                for (L = 1; L <= J1; L++)
                {
                    int J2 = PARM.LT[I - 1, L - 1];
                    if (PARM.IHC[J2 - 1] != PARM.NHC[8]) continue;
                    X1 = Math.Max(0.0, PARM.COTL[J2 - 1]);
                    PARM.JDA = PARM.ITL[I - 1, L - 1];
                    if (PARM.NYD == 0) PARM.JDA = PARM.JDA + 1;
                    if (PARM.JDA == JJ && PARM.NBT[J2 - 1] == 0 && PARM.NBE[J2 - 1] == KK) X1 = 0.0;
                    JJ = PARM.JDA;
                    KK = PARM.NBE[J2 - 1];
                    Functions.AXMON(ref PARM.JDA, ref PARM.MO);
                    Functions.AICL();
                    int M = PARM.LFT[I - 1, L - 1];
                    double XZ = PARM.FCST[M - 1] * PARM.WFA[I - 1, L - 1];
                    double XY = X1 + XZ;
                    //!     PRINTOUT FERTILIZER SCHEDULE                                                   
                    //WRITE(KW(1),329)I,MO,KDA,FTNM(M),KDF(M),NBE(J2),NBT(J2),XY,WFA(I,L),TLD(J2),FN(M),FNH3(M),FNO(M),FP(M),FPO(M),FK(M),HUSC(I,L)                
                    if (PARM.KFL[12] > 0)
                    {
                        //WRITE(KW(13),510)I,MO,KDA,FTNM(M),JH(I,L),KDF(M),IHC(J2),NBE(J2),NBT(J2),XY,XZ,WFA(I,L)           
                    }
                }
                PARM.MO = 1;
            lbl187: JJ = 367;
                KK = 0;
                if (PARM.KP > 0)
                {
                    //WRITE(KW(1),377)                                                               
                    for (L = 1; L <= J1; L++)
                    {
                        int J2 = PARM.LT[I - 1, L - 1];
                        if (PARM.IHC[J2 - 1] != PARM.NHC[6]) continue;
                        X1 = Math.Max(0.0, PARM.COTL[J2 - 1]);
                        PARM.JDA = PARM.ITL[I - 1, L - 1];
                        if (PARM.NYD == 0) PARM.JDA = PARM.JDA + 1;
                        if (PARM.JDA == JJ && PARM.NBT[J2 - 1] == 0 && PARM.NBE[J2 - 1] == KK) X1 = 0.0;
                        JJ = PARM.JDA;
                        KK = PARM.NBE[J2 - 1];
                        Functions.AXMON(ref PARM.JDA, ref PARM.MO);
                        Functions.AICL();
                        int M = PARM.LPC[I - 1, L - 1];
                        double XZ = PARM.PCST[M - 1] * PARM.PSTR[I - 1, L - 1];
                        double XY = X1 + XZ;
                        //!     PRINTOUT PESTICIDE SCHEDULE                                                    
                        //WRITE(KW(1),378)I,MO,KDA,PSTN(M),KDP(M),NBE(J2),NBT(J2),XY,PSTR(I,L),PSTE(I,L),HUSC(I,L)                                                      
                        if (PARM.KFL[12] > 0)
                        {
                            //WRITE(KW(13),501)I,MO,KDA,PSTN(M),JH(I,L),KDP(M),IHC(J2),NBE(J2),NBT(J2),XY,XZ,PSTR(I,L)    
                        }
                    }
                }
                if (NFL == 0 && PARM.JX[0] > 0)
                {
                    I = PARM.JX[0];
                    IY1 = I;
                    goto lbl189;
                }
                int K2 = 1;
                //REWIND KR(16)                           ;                                       
                PARM.NRO = IY1;
                PARM.IGSD = 0;
                if (PARM.NSTP > 0 && PARM.NSTP < 366) PARM.IGSD = PARM.NRO;
                PARM.JX[3] = PARM.IAUI;
                PARM.JX[4] = 0;
                Functions.TILTBL();
                PARM.IAUI = PARM.NDT;
                //WRITE(KW(1),715)TIL(NDT),TLD(NDT)                                                   
                //!     if(IAUF==0)goto lbl689                                                           
                PARM.JX[3] = 261;
                PARM.JX[4] = 12;
                Functions.TILTBL();
                PARM.IAUF = PARM.NDT;
                if (PARM.LMS == 0)
                {
                    PARM.JX[3] = 267;
                    PARM.JX[4] = 12;
                    Functions.TILTBL();
                    PARM.IAUL = PARM.NDT;
                    //WRITE(KW(1),714)TIL(NDT),TLD(NDT)
                }
                L = 1;
                if (BFT0 > 0.0)
                {
                    PARM.IDFT[0] = PARM.IDF0;
                    if (PARM.IDFT[0] == 0)
                    {
                        PARM.IDFT[0] = 52;
                        PARM.IDFT[1] = 52;
                    }
                    else
                    {
                        PARM.IDFT[1] = PARM.IDF0;
                    }
                    for (K = 1; K <= 2; K++)
                    {
                        PARM.JX[6] = PARM.IDFT[K - 1];
                        Functions.NFTBL(ref L);
                        PARM.IDFT[K - 1] = L;
                    }
                    //WRITE(KW(1),716)TIL(IAUF),TLD(IAUF),FTNM(IDFT(1))
                }
                if (PARM.IPAT > 0)
                {
                    if (PARM.IDFP == 0) PARM.IDFP = 53;
                    PARM.JX[6] = PARM.IDFP;
                    Functions.NFTBL(ref L);
                    PARM.IDFP = L;
                }
                for (I = 1; I <= PARM.NRO; I++)
                {
                    if (PARM.NBC[I - 1] == 0) PARM.NBC[I - 1] = 1;
                    if (PARM.LY[I - 1, 1 - 1] > 0) continue;
                    I1 = I - 1;
                    if (I1 == 0) I1 = PARM.NRO;
                    PARM.LY[I - 1, 1 - 1] = PARM.LY[I1 - 1, PARM.NBC[I1 - 1] - 1];
                }
                if (PARM.IGO > 0)
                {
                    PARM.NBCX[0, PARM.LY[PARM.NRO - 1, PARM.NBC[PARM.NRO - 1] - 1] - 1] = PARM.NBCX[0, PARM.LY[PARM.NRO - 1, PARM.NBC[PARM.NRO - 1] - 1] - 1] + 1;
                    int NN = PARM.NBC[0];
                    for (J = 1; J <= PARM.MNC; J++)
                    {
                        if (PARM.NHU[J - 1] == 0) continue;
                        for (I = 1; I <= NN; I++)
                        {
                            if (PARM.LY[0, I - 1] == J) break;
                        }
                        if (I <= NN) continue;
                        PARM.NBC[0] = PARM.NBC[0] + 1;
                        for (L = PARM.NBC[0]; L >= 2; L--)
                        {
                            int L1 = L - 1;
                            PARM.LY[0, L - 1] = PARM.LY[0, L1 - 1];
                        }
                        PARM.LY[0, 0] = PARM.NHU[J - 1];
                    }
                }
                //!     ANM = CROP NAME                                                                
                //!     X1  = GRAIN PRICE ($/t)                                                        
                //!     X2  = FORAGE PRICE ($/t)                                                       
                //!     LINE 8/27
                while (true)
                {

                    //READ(KR(26),630,IOSTAT=NFL)ANM,X1,X2                                                      
                    if (NFL != 0) break;
                    if (ANM == "") break;
                    for (J = 1; J <= PARM.LC; J++)
                    {
                        if (ANM == PARM.CPNM[J - 1]) goto lbl684;
                    }
                    continue;
                lbl684: PARM.PRYG[J - 1] = X1;
                    PARM.PRYF[J - 1] = X2;
                }
                //REWIND KR(26)                                                                  
                //!     PRINTOUT CROP PARAMETERS                                                       
                Functions.APAGE(1);
                //WRITE(KW(1),348)                                                               
                //WRITE(KW(1),248)(CPNM(I),I=1,LC)                                               
                //WRITE(KW(1),49)'WA  ',(WA(I),I=1,LC)                                           
                //WRITE(KW(1),53)'WUB ',(WUB(I),I=1,LC)                                          
                //WRITE(KW(1),53)'HI  ',(HI(I),I=1,LC)                                           
                //WRITE(KW(1),49)'TOPT',(TOPC(I),I=1,LC)                                         
                //WRITE(KW(1),49)'TBAS',(TBSC(I),I=1,LC)                                         
                //WRITE(KW(1),35)'GMHU',(GMHU(I),I=1,LC)                                         
                //WRITE(KW(1),53)'DMLA',(DMLA(I),I=1,LC)                                         
                //WRITE(KW(1),53)'DLAI',(DLAI(I),I=1,LC)                                         
                //WRITE(KW(1),63)'LAP1',(DLAP(1,I),I=1,LC)                                       
                //WRITE(KW(1),63)'LAP2',(DLAP(2,I),I=1,LC)                                       
                //WRITE(KW(1),63)'PPL1',(PPLP(1,I),I=1,LC)                                       
                //WRITE(KW(1),63)'PPL2',(PPLP(2,I),I=1,LC)                                       
                //WRITE(KW(1),63)'FRS1',(FRST(1,I),I=1,LC)                                       
                //WRITE(KW(1),63)'FRS2',(FRST(2,I),I=1,LC)                                       
                //WRITE(KW(1),53)'RLAD',(RLAD(I),I=1,LC)                                         
                //WRITE(KW(1),53)'RBMD',(RBMD(I),I=1,LC)                                         
                //WRITE(KW(1),49)'ALT ',(ALT(I),I=1,LC)                                          
                //WRITE(KW(1),53)'CAF ',(CAF(I),I=1,LC)                                          
                //WRITE(KW(1),78)'GSI ',(GSI(I),I=1,LC)                                          
                //WRITE(KW(1),53)'WAC2',(WAC2(2,I),I=1,LC)                                       
                //WRITE(KW(1),49)'WAVP',(WAVP(I),I=1,LC)                                         
                //WRITE(KW(1),49)'VPTH',(VPTH(I),I=1,LC)                                         
                //WRITE(KW(1),53)'VPD2',(VPD2(I),I=1,LC)                                         
                //WRITE(KW(1),49)'SDW ',(SDW(I),I=1,LC)                                          
                //WRITE(KW(1),53)'HMX ',(HMX(I),I=1,LC)                                          
                //WRITE(KW(1),53)'RDMX',(RDMX(I),I=1,LC)                                         
                //WRITE(KW(1),63)'RWP1',(RWPC(1,I),I=1,LC)                                       
                //WRITE(KW(1),63)'RWP2',(RWPC(2,I),I=1,LC)                                       
                //WRITE(KW(1),78)'CNY ',(CNY(I),I=1,LC)                                          
                //WRITE(KW(1),78)'CPY ',(CPY(I),I=1,LC)                                          
                //WRITE(KW(1),78)'CKY ',(CKY(I),I=1,LC)                                          
                //WRITE(KW(1),78)'WSYF',(WSYF(I),I=1,LC)                                         
                //WRITE(KW(1),53)'PST ',(PST(I),I=1,LC)                                          
                //WRITE(KW(1),53)'CSTS',(CSTS(I),I=1,LC)                                         
                //WRITE(KW(1),53)'PRYG',(PRYG(I),I=1,LC)                                         
                //WRITE(KW(1),53)'PRYF',(PRYF(I),I=1,LC)                                         
                //WRITE(KW(1),53)'WCYS',(WCY(I),I=1,LC)                                          
                //WRITE(KW(1),78)'BN1 ',(BN(1,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BN2 ',(BN(2,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BN3 ',(BN(3,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BP1 ',(BP(1,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BP2 ',(BP(2,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BP3 ',(BP(3,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BK1 ',(BK(1,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BK2 ',(BK(2,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BK3 ',(BK(3,I),I=1,LC)                                         
                //WRITE(KW(1),63)'BW1 ',(BWD(1,I),I=1,LC)                                        
                //WRITE(KW(1),63)'BW2 ',(BWD(2,I),I=1,LC)                                        
                //WRITE(KW(1),63)'BW3 ',(BWD(3,I),I=1,LC)                                        
                //WRITE(KW(1),63)'STX1',(STX(1,I),I=1,LC)                                        
                //WRITE(KW(1),63)'STX2',(STX(2,I),I=1,LC)                                        
                //WRITE(KW(1),63)'BLG1',(BLG(1,I),I=1,LC)                                        
                //WRITE(KW(1),63)'BLG2',(BLG(2,I),I=1,LC)                                        
                //WRITE(KW(1),78)'FTO ',(FTO(I),I=1,LC)                                          
                //WRITE(KW(1),78)'FLT ',(FLT(I),I=1,LC)                                          
                //WRITE(KW(1),281)(IDC(I),I=1,LC)                                                
                PARM.IPL = 0;
                int LRG = 0;
                for (I = 1; I <= PARM.LC; I++)
                {
                    PARM.NHU[I - 1] = PARM.IHU[I - 1];
                    if (PARM.NHU[I - 1] > LRG) LRG = PARM.NHU[I - 1];
                    if (PARM.RDMX[I - 1] > PARM.RZ) PARM.RZ = PARM.RDMX[I - 1];
                    if (PARM.IDC[I - 1] == PARM.NDC[6] || PARM.IDC[I - 1] == PARM.NDC[7] || PARM.IDC[I - 1] == PARM.NDC[9]) PARM.XMTU[I - 1] = (1.0 - Math.Exp(-PARM.FTO[I - 1] / PARM.XMTU[I - 1])) / 144.0;
                    PARM.BLG[2, I - 1] = PARM.BLG[1, I - 1];
                    PARM.BLG[0, I - 1] = PARM.BLG[0, I - 1] / PARM.BLG[1, I - 1];
                    PARM.BLG[1, I - 1] = .99;
                    Functions.ASCRV(ref PARM.BLG[0, I - 1], ref PARM.BLG[1, I - 1], .5, 1.0);
                    if (PARM.NUPC == 0)
                    {
                        Functions.NCONC(ref PARM.BN[0, I - 1], ref PARM.BN[1, I - 1], ref PARM.BN[2, I - 1], ref PARM.BN[3, I - 1]);
                        Functions.NCONC(ref PARM.BP[0, I - 1], ref PARM.BP[1, I - 1], ref PARM.BP[2, I - 1], ref PARM.BP[3, I - 1]);
                    }
                    else
                    {
                        PARM.BN[3, I - 1] = PARM.BN[0, I - 1];
                        X1 = PARM.BN[0, I - 1] - PARM.BN[2, I - 1];
                        PARM.BN[0, I - 1] = 1.0 - (PARM.BN[1, I - 1] - PARM.BN[2, I - 1]) / X1;
                        PARM.BN[1, I - 1] = 1.0 - .00001 / X1;
                        Functions.ASCRV(ref PARM.BN[0, I - 1], ref PARM.BN[1, I - 1], .5, 1.0);
                        PARM.BP[3, I - 1] = PARM.BP[0, I - 1];
                        X1 = PARM.BP[0, I - 1] - PARM.BP[2, I - 1];
                        PARM.BP[0, I - 1] = 1.0 - (PARM.BP[1, I - 1] - PARM.BP[2, I - 1]) / X1;
                        PARM.BP[1, I - 1] = 1.0 - .00001 / X1;
                        Functions.ASCRV(ref PARM.BP[0, I - 1], ref PARM.BP[1, I - 1], .5, 1.0);
                    }
                    for (K = 1; K <= 3; K++)
                    {
                        XTP[K - 1] = 0.0;
                        for (J = 1; J <= 3; J++)
                        {
                            XTP[K - 1] = XTP[K - 1] + PARM.BK[J - 1, I - 1] * AKX[K - 1, J - 1];
                        }
                    }
                    PARM.BK[0, I - 1] = XTP[0];
                    PARM.BK[1, I - 1] = XTP[1];
                    PARM.BK[3, I - 1] = XTP[2];
                    PARM.IHU[I - 1] = 1;
                    X1 = Functions.ASPLT(ref PARM.DLAP[0, I - 1]) * .01;
                    X2 = Functions.ASPLT(ref PARM.DLAP[1, I - 1]) * .01;
                    Functions.ASCRV(ref PARM.DLAP[0, I - 1], ref PARM.DLAP[1, I - 1], X1, X2);
                    X1 = Functions.ASPLT(ref PARM.FRST[0, I - 1]);
                    X2 = Functions.ASPLT(ref PARM.FRST[1, I - 1]);
                    Functions.ASCRV(ref PARM.FRST[0, I - 1], ref PARM.FRST[1, I - 1], X1, X2);
                    PARM.WAC2[0, I - 1] = PARM.WA[I - 1] * .01;
                    X2 = Functions.ASPLT(ref PARM.WAC2[1, I - 1]);
                    Functions.ASCRV(ref PARM.WAC2[0, I - 1], ref PARM.WAC2[1, I - 1], 330.0, X2);
                    X2 = Functions.ASPLT(ref PARM.VPD2[I - 1]);
                    PARM.VPD2[I - 1] = (1.0 - PARM.VPD2[I - 1]) / (X2 - PARM.VPTH[I - 1]);
                    PARM.UNA[I - 1] = PARM.PRMT[38] * PARM.BN[2, I - 1] * PARM.WA[I - 1] * PARM.PRMT[27];
                    PARM.ULYN[I - 1] = PARM.UNA[I - 1];
                    PARM.BLYN[I - 1] = 0.0;
                }
                for (int x = 0; x < PARM.SMMC.GetUpperBound(0) + 1; x++)
                    for (int y = 0; y < PARM.SMMC.GetUpperBound(1) + 1; y++)
                        for (int z = 0; z < PARM.SMMC.GetUpperBound(2) + 1; z++)
                            PARM.SMMC[x, y, z] = 0.0;
                for (J = 1; J <= LRG; J++)
                {
                    //WRITE(KW(1),78)'POP ',(POP(I,J),I=1,LC)                                      
                    //WRITE(KW(1),53)'MXLA',(PPLA(I,J),I=1,LC)                                     
                }
                for (J = 1; J <= LRG; J++)
                {
                    //WRITE(KW(1),35)'PHU ',(PHU(I,J),I=1,LC)                                      
                }
                Functions.APAGE(1);
                //WRITE(KW(1),348)                                                               
                //WRITE(KW(1),248)(CPNM(I),I=1,LC)                                               
                //WRITE(KW(1),78)'BN1 ',(BN(1,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BN2 ',(BN(2,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BN3 ',(BN(3,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BN4 ',(BN(4,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BP1 ',(BP(1,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BP2 ',(BP(2,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BP3 ',(BP(3,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BP4 ',(BP(4,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BK1 ',(BK(1,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BK2 ',(BK(2,I),I=1,LC)                                         
                //WRITE(KW(1),78)'BK3 ',(BK(3,I),I=1,LC)                                         
                //WRITE(KW(1),63)'LAP1',(DLAP(1,I),I=1,LC)                                       
                //WRITE(KW(1),63)'LAP2',(DLAP(2,I),I=1,LC)                                       
                //WRITE(KW(1),63)'FRS1',(FRST(1,I),I=1,LC)                                       
                //WRITE(KW(1),63)'FRS2',(FRST(2,I),I=1,LC)                                       
                //WRITE(KW(1),63)'WAC1',(WAC2(1,I),I=1,LC)                                       
                //WRITE(KW(1),63)'WAC2',(WAC2(2,I),I=1,LC)                                       
                //WRITE(KW(1),63)'PPC1',(PPCF(1,I),I=1,LC)                                       
                //WRITE(KW(1),63)'PPC2',(PPCF(2,I),I=1,LC) 

                if (PARM.NDP > 0)
                {
                    //WRITE(KW(1),'(//1X,A/)')'____________________PESTICIDE DATA____________________'                                                                 
                    //WRITE(KW(1),382)                                                               
                    //!     PRINTOUT PESTICIDE DATA                                                        
                    for (I = 1; I <= PARM.NDP; I++)
                    {
                        //WRITE(KW(1),380)PSTN[I-1],PSOL[I-1],PHLS[I-1],PHLF[I-1],PWOF[I-1],PKOC[I-1],PCST[I-1]                                                                      
                        PARM.PSOL[I - 1] = PARM.PSOL[I - 1] * 10.0;
                        PARM.PHLS[I - 1] = 1.0 - Math.Exp(-.693 / PARM.PHLS[I - 1]);
                        PARM.PHLF[I - 1] = 1.0 - Math.Exp(-.693 / PARM.PHLF[I - 1]);
                    }
                }
                PARM.JD = PARM.LY[PARM.NRO - 1, PARM.NBC[PARM.NRO - 1] - 1];
                PARM.ICCD = 0;
                PARM.IRTC = 1;
                I = 1;
                if (PARM.NBC[PARM.NRO - 1] > 1) goto lbl634;
                if (PARM.IHVD[0, PARM.JD - 1] == 0) goto lbl677;
                if (PARM.IPLD[0, PARM.JD - 1] < PARM.IHVD[0, PARM.JD - 1]) goto lbl677;
            lbl634: int N1 = PARM.NSTP + 365 * (PARM.NRO - 1);
                for (I = 1; I <= PARM.NBC[PARM.NRO - 1]; I++)
                {
                    if (N1 < PARM.IHVD[0, PARM.LY[PARM.NRO - 1, I - 1] - 1]) goto lbl677;
                }
                goto lbl678;
            lbl677: PARM.ICCD = 1;
                PARM.IRTC = I;
            lbl678: int XLC = PARM.LC;
                X1 = PARM.STDO / XLC;
                SUM = 0.0;
                for (J = 1; J <= PARM.LC; J++)
                {
                    PARM.STDN[J - 1] = 8.29 * PARM.STD0;
                    SUM = SUM + PARM.STDN[J - 1];
                    PARM.STDP = 1.04 * PARM.STD0;
                    PARM.STDK = 8.29 * PARM.STD0;
                    PARM.STDL = .1 * PARM.STD0;
                    PARM.STD[J - 1] = X1;
                }
                if (PARM.RZ > XX) PARM.RZ = XX;
                if (PARM.BIG > XX) PARM.BIG = XX;
                PARM.BTN = PARM.TWN + PARM.TNO3 + SUM;
                PARM.BTNX = PARM.BTN;
                double BTC = PARM.TOC;
                PARM.BTP = PARM.TAP + PARM.TMP + PARM.TOP + PARM.TP + PARM.SEV[3] + PARM.STDP;
                PARM.BTK = PARM.TSK + PARM.TEK + PARM.TFK + PARM.STDK;
                KK = PARM.NTL[0];
                PARM.KC = 1;
                for (PARM.KT = 1; PARM.KT <= KK; PARM.KT++)
                {
                    if (PARM.ITL[0, PARM.KT - 1] >= PARM.IBD) goto lbl200;
                    II = PARM.IHC[PARM.LT[0, PARM.KT - 1] - 1];
                    if (II == PARM.NHC[0]) PARM.KC = PARM.KC + 1;
                }
                PARM.KT = PARM.NTL[0];
            lbl200: PARM.JT2 = PARM.LT[0, PARM.KT - 1];
                //!     if(NGN>0)Functions.WREAD                                                            
                PARM.BFT = BFT0;
                if (BFT0 > 1.0) PARM.BFT = 10.0 * BFT0 * PARM.ABD * PARM.RZ;
                PARM.UB1 = PARM.RZ * PARM.PRMT[53];
                PARM.UOB = 1.0 - Math.Exp(-PARM.UB1);
                PARM.AWC = 0.0;
                PARM.AQV = 0.0;
                PARM.ARF = 0.0;
                PARM.ALB = PARM.SALB;
                if (PARM.SNO > 5.0) PARM.ALB = .6;
                for (int x = 0; x < PARM.U.Length; x++)
                    PARM.U[x] = 0.0;
                for (int x = 0; x < PARM.SSF.Length; x++)
                    PARM.SSF[x] = 0.0;
                PARM.IGO = 0;
                PARM.JJK = PARM.KDC1[JJK0 - 1];
                PARM.KC = 0;
                for (int x = 0; x < PARM.IPLD.GetUpperBound(0) + 1; x++)
                    for (int y = 0; y < PARM.IPLD.GetUpperBound(1) + 1; x++)
                        PARM.IPLD[x, y] = 0;
                PARM.MO = PARM.MO1;
                PARM.JDA = PARM.IBD - 1;
                if (PARM.JDA <= 0) PARM.JDA = 365;
                Functions.WHRL();
                Functions.WRMX();                                                          
                PARM.HR0 = PARM.HRLT;
                //!     BEGIN ANNUAL SIMULATION LOOP                                                   
                PARM.IRO = PARM.IRO0 - 1;
            lbl533: Functions.BSIM();                                                                      
                if (PARM.ISTP == 1) goto lbl219;
                PARM.IY = Math.Max(1, PARM.IY - 1);
                int XYR = PARM.IY;
                Functions.APAGE(1);                                                                 
                if (PARM.KFL[8] == 0) goto lbl585;
            //WRITE(KW(9),625)(Z(LID(I)),I=1,NBSL)                                           
            //WRITE(KW(9),625)(BD(LID(I)),I=1,NBSL)                                          
            //WRITE(KW(9),625)(SOIL(20,LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),625)(SOIL(9,LID(I)),I=1,NBSL)                                      
            //WRITE(KW(9),625)(SAN(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),625)(SIL(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),695)(SOIL(6,LID(I)),I=1,NBSL)                                      
            //WRITE(KW(9),625)(PH(LID(I)),I=1,NBSL)                                          
            //WRITE(KW(9),625)(SMB(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),625)(SOIL(7,LID(I)),I=1,NBSL)                                      
            //WRITE(KW(9),625)(CAC(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),625)(CEC(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),625)(ROK(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),625)(SOIL(5,LID(I)),I=1,NBSL)                                      
            //WRITE(KW(9),625)(SOIL(1,LID(I)),I=1,NBSL)                                      
            //WRITE(KW(9),625)(RSD(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),625)(SOIL(13,LID(I)),I=1,NBSL)                                     
            //WRITE(KW(9),625)(PSP(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),625)(SATC(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),625)(HCL(LID(I)),I=1,NBSL)                                              
            //WRITE(KW(9),625)(SOIL(4,LID(I)),I=1,NBSL)                                      
            //WRITE(KW(9),625)(SOIL(15,LID(I)),I=1,NBSL)                                     
            //WRITE(KW(9),625)(ECND(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),625)(STFR(LID(I)),I=1,NBSL)                                             
            //WRITE(KW(9),625)(SOIL(17,LID(I)),I=1,NBSL)                                     
            //WRITE(KW(9),695)(CPRV(LID(I)),I=1,NBSL)                                             
            //WRITE(KW(9),695)(CPRH(LID(I)),I=1,NBSL)                                             
            //WRITE(KW(9),695)(WLS(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),695)(WLM(LID(I)),I=1,NBSL)                                         
            //WRITE(KW(9),695)(WLSL(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),695)(WLSC(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),695)(WLMC(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),695)(WLSLC(LID(I)),I=1,NBSL)                                       
            //WRITE(KW(9),695)(WLSLNC(LID(I)),I=1,NBSL)                                      
            //WRITE(KW(9),695)(WBMC(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),695)(WHSC(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),695)(WHPC(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),625)(WLSN(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),625)(WLMN(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),625)(WBMN(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),695)(WHSN(LID(I)),I=1,NBSL)                                        
            //WRITE(KW(9),695)(WHPN(LID(I)),I=1,NBSL)
            //WRITE(KW(9),625)(CGO2(LID(I)),I=1,NBSL)
            //WRITE(KW(9),625)(CGCO2(LID(I)),I=1,NBSL)
            //WRITE(KW(9),625)(CGN2O(LID(I)),I=1,NBSL)
            //WRITE(KW(9),625)(OBC(LID(I)),I=1,NBSL)
            //WRITE(KW(9),625)(SOIL(18,LID(I)),I=1,NBSL)                                     
            //WRITE(KW(9),625)(SOIL(19,LID(I)),I=1,NBSL)                                     
            lbl585: //WRITE(KW(1),'(//1X,A/)')'____________________FINAL SOIL DATA_____________________'                                                               
                Functions.SOLIO(ref YTP,1);                                                              
                Functions.SCONT(1);                                                                          
                //WRITE(KW(1),'(/T10,A,F7.1,A)')'ERODED SOIL THICKNESS = ',THK,' mm'             
                //WRITE(KW(1),'(/T10,A,F7.2,A)')'FINAL WATER CONTENT OF SNOW = ',SNO,' mm'                                                                         
                //!     PRINTOUT WATER BALANCE       
                //Paul Cain's Note: The line below is commented out until we decide how to handle file IO                                      
                //Functions.HSWBL(ref PARM.SM[3], ref PARM.SM[13], ref PARM.SM[10], ref PARM.SM[15], ref PARM.SM[16], ref PARM.SM[18], ref PARM.SNO, ref PARM.SW, SWW, ref PARM.SM[19], ref PARM.SM[83], ref PARM.KW, ref PARM.MSO);                                                          
                if (PARM.IRR == 4)
                {
                    PARM.VLG = .1 * PARM.VLG / (PARM.DALG + 1.0E-10);
                    //Paul Cain's Note: The line below is commented out until we decide how to handle file IO   
                    //Functions.HLGB(ref PARM.SM[22],ref PARM.SM[20],ref PARM.SM[23],ref PARM.SM[77],ref PARM.VLG,ref VLGB,ref PARM.SM[21],ref PARM.KW,ref PARM.MSO);                
                    Functions.NLGB(ref PARM.SM[78],ref PARM.SM[79],ref WTMB,ref PARM.WTMU,ref PARM.KW,ref PARM.MSO);                                    
                }
                PARM.RNO3 = PARM.SM[3] * PARM.RFNC;
                SUM = 0.0;
                TOT = 0.0;
                double ADD = 0.0;
                double AD1 = 0.0;
                for (J = 1; J <= PARM.LC; J++)
                {
                    TOT = TOT + PARM.UP1[J - 1];
                    ADD = ADD + PARM.UK1[J - 1];
                    SUM = SUM + PARM.UN1[J - 1];
                    AD1 = AD1 + PARM.STDN[J - 1];
                }
            //TODO:
                double FTN = PARM.TNO2 + PARM.TNO3 + PARM.TWN + AD1 + PARM.STDON + SUM + PARM.TNH3;
                //Paul Cain's Note: The line below is commented out until we decide how to handle file IO       
                //Functions.NBL(ref PARM.BTN, ref PARM.RNO3, ref PARM.SM[42], ref PARM.SM[43], ref PARM.SM[44], ref PARM.SM[45], ref PARM.SM[48], ref PARM.SM[58], ref PARM.TYN, ref PARM.SM[51], ref PARM.SM[59], ref PARM.SM[60], ref PARM.SM[49], ref PARM.SM[97], FTN, 1, ref PARM.KW, ref PARM.MSO);                                      
                //WRITE(KW(1),636)TNO2,TNO3,TNH3,TWN,AD1,STDON,SUM
                PARM.PLCX = 1000.0 * PARM.PLCX;
                PARM.DPLC = 1000.0 * PARM.DPLC;
                //Paul Cain's Note: The line below is commented out until we decide how to handle file IO       
                //Functions.NCBL(BTC, ref PARM.SM[76], ref PARM.SM[74], ref PARM.SM[75], ref PARM.SM[73], ref PARM.SM[64], ref PARM.SM[72], ref PARM.SM[96], ref PARM.DPLC, ref PARM.PLCX, ref PARM.TOC, ref PARM.KW, ref PARM.MSO);
                //WRITE(KW(1),637)ZLSC,ZLMC,ZBMC,ZHSC,ZHPC                                     
                double FTP = PARM.TAP + PARM.TOP + PARM.TMP + PARM.TP + PARM.TFOP + PARM.STDP + PARM.STDOP + TOT;
                //Paul Cain's Note: The line below is commented out until we decide how to handle file IO       
                //Functions.NBL(ref PARM.BTP,0.0, ref PARM.SM[53], ref PARM.SM[54],0.0, ref PARM.SM[56],0.0, ref PARM.SM[61], ref PARM.TYP,0.0, ref PARM.SM[62],0.0,0.0,0.0,FTP,2, ref PARM.KW, ref PARM.MSO);                                                            
                //WRITE(KW(1),638)TAP,TOP,TMP,TP,TFOP,STDP,STDOP,TOT                             
                double FTK = PARM.TSK + PARM.TEK + PARM.TFK + PARM.STDK + PARM.STDOK + ADD;
                //Paul Cain's Note: The line below is commented out until we decide how to handle file IO       
                //Functions.NBL(ref PARM.BTK,0.0, ref PARM.SM[77], ref PARM.SM[78], ref PARM.SM[79], ref PARM.SM[80],0.0,0.0, ref PARM.TYK,0.0, ref PARM.SM[63],0.0,0.0,0.0,FTK,3, ref PARM.KW, ref PARM.MSO);                                                            
                //WRITE(KW(1),639)TSK,TEK,TFK,STDK,STDOK,ADD                                     
                //Paul Cain's Note: The line below is commented out until we decide how to handle file IO
                //Functions.SLTB(ref PARM.SM[68], ref PARM.SM[71], ref PARM.SM[81], ref PARM.SM[70], ref PARM.SM[69], ref PARM.SLT0, ref PARM.TSLT, ref PARM.KW, ref PARM.MSO);                
                int XQP = PARM.NQP;
                int XCN = PARM.JCN;
                XX = XQP + .01;
                PARM.PRSD = PARM.PRSD - PARM.PRAV * PARM.PRAV / XX;
                PARM.PRAV = PARM.PRAV / XX;
                if (PARM.PRSD > 0.0) PARM.PRSD = Math.Sqrt(PARM.PRSD / XX);
                PARM.CYSD = PARM.CYSD - PARM.CYAV * PARM.CYAV / XX;
                PARM.CYAV = PARM.CYAV / XX;
                if (PARM.CYSD > 0.0) PARM.CYSD = Math.Sqrt(PARM.CYSD / XX);
                PARM.QPS = PARM.QPS / XX;
                PARM.TCAV = PARM.TCAV / XX;
                PARM.SM[57] = PARM.SM[57] / XX;
                //!     DETERMath.MinE AVE ANNUAL VALUES                                                    
                PARM.SMY[12] = 0.0;
                SUM = 0.0;
                TOT = 0.0;
                ADD = 0.0;
                double AD2 = 0.0;
                for (I = 1; I <= 12; I++)
                {
                    X1 = PARM.IHRL[I - 1];
                    if (X1 > 0.0)
                    {
                        PARM.THRL[I - 1] = PARM.THRL[I - 1] / X1;
                        PARM.SRMX[I - 1] = PARM.SRMX[I - 1] / X1;
                        PARM.TXMX[I - 1] = PARM.TXMX[I - 1] / X1;
                        PARM.TXMN[I - 1] = PARM.TXMN[I - 1] / X1;
                        PARM.TSR[I - 1] = PARM.TSR[I - 1] / X1;
                    }
                    PARM.TR[I - 1] = PARM.TR[I - 1] / XYR;
                    PARM.TSN[I - 1] = PARM.TSN[I - 1] / XYR;
                    PARM.TSY[I - 1] = PARM.TSY[I - 1] / XYR;
                    PARM.RSY[I - 1] = PARM.RSY[I - 1] / XYR;
                    PARM.TYW[I - 1] = PARM.TYW[I - 1] / XYR;
                    PARM.TQ[I - 1] = PARM.TQ[I - 1] / XYR;
                    PARM.W[I - 1] = PARM.W[I - 1] / (PARM.TEI[I - 1] + 1.0E-20);
                    PARM.RCM[I - 1] = PARM.RCM[I - 1] / (PARM.TEI[I - 1] + 1.0E-20);
                    PARM.TAL[I - 1] = PARM.TAL[I - 1] / PARM.CX[I - 1];
                    PARM.CX[I - 1] = PARM.CX[I - 1] / XYR;
                    PARM.TEI[I - 1] = PARM.TEI[I - 1] / XYR;
                    PARM.SMY[I - 1] = PARM.SRD[I - 1] / XYR;
                    PARM.SET[I - 1] = PARM.SET[I - 1] / XYR;
                    PARM.TET[I - 1] = PARM.TET[I - 1] / XYR;
                    PARM.ASW[I - 1] = PARM.ASW[I - 1] / XYR;
                    PARM.QIN[I - 1] = PARM.QIN[I - 1] / XYR;
                    PARM.TSTL[I - 1] = PARM.TSTL[I - 1] / XYR;
                    PARM.TRHT[I - 1] = PARM.TRHT[I - 1] / XYR;
                    PARM.TAMX[I - 1] = PARM.TAMX[I - 1] / XYR;
                    TOT = TOT + PARM.QIN[I - 1];
                    ADD = ADD + PARM.TAMX[I - 1];
                    SUM = SUM + PARM.ASW[I - 1];
                    PARM.SMY[12] = PARM.SMY[12] + PARM.SMY[I - 1];
                    AD2 = AD2 + PARM.CX[I - 1];
                }
                PARM.SM[14] = PARM.SM[14] / (XCN + 1.0E-10);
                X1 = PARM.SM[27] + 1.0E-10;
                PARM.SM[28] = PARM.SM[28] / X1;
                PARM.SM[36] = PARM.SM[36] / X1;
                PARM.SM[89] = PARM.SM[89] / X1;
                PARM.SM[90] = PARM.SM[90] / X1;
                double SRF2 = PARM.SM[3] * PARM.SM[3];
                SUM = SUM / 12.0;
                for (K = 1; K <= 14; K++)
                {
                    PARM.SM[K - 1] = PARM.SM[K - 1] / XYR;
                }
                for (K = 16; K <= 28; K++)
                {
                    PARM.SM[K - 1] = PARM.SM[K - 1] / XYR;
                }
                for (K = 30; K <= 36; K++)
                {
                    PARM.SM[K - 1] = PARM.SM[K - 1] / XYR;
                }
                for (K = 38; K <= 57; K++)
                {
                    PARM.SM[K - 1] = PARM.SM[K - 1] / XYR;
                }
                for (K = 59; K <= PARM.NSM; K++)
                {
                    PARM.SM[K - 1] = PARM.SM[K - 1] / XYR;
                }
                if (PARM.NDP == 0 || PARM.MASP < 0 || PARM.NBYR == 1) goto lbl212;
                Functions.APAGE(1);                                                                  
                //WRITE(KW(1),'(//1X,A/)')'______________PESTICIDE SUMMARY TABLE________________'                                                                  
                //WRITE(KW(1),460)                                                               
                NY[0] = 1;
                NY[1] = .1 * XYR + 1.5;
                NY[2] = .5 * XYR + 1.5;
                int N2;
                for (K = 1; K <= PARM.NDP; K++)
                {
                    //WRITE(KW(1),462)PSTN(K)                                                        
                    for (I = 1; I <= 5; I++)
                    {
                        for (J = 1; J <= PARM.NBYR; J++)
                        {
                            PARM.NX[J - 1] = J;
                            XYP[J - 1] = PARM.APY[I - 1, K - 1, J - 1];
                            if (XYP[J - 1] <= 1.0E-4)
                            {
                                PARM.APY[I - 1, K - 1, J - 1] = 0.0;
                                PARM.AYB[I - 1, K - 1, J - 1] = 0.0;
                            }
                            XTP[J - 1] = PARM.APQ[I - 1, K - 1, J - 1];
                            if (XTP[J - 1] > 1.0E-4) continue;
                            PARM.APQ[I - 1, K - 1, J - 1] = 0.0;
                            PARM.AQB[I - 1, K - 1, J - 1] = 0.0;
                        }
                        Functions.ASORT1(ref XTP,ref PARM.NX,ref PARM.NBYR);
                        NXX[0, I - 1] = PARM.NX[(int)NY[0] - 1];
                        NXX[1, I - 1] = PARM.NX[(int)NY[1] - 1];
                        NXX[2, I - 1] = PARM.NX[(int)NY[2] - 1];
                        Functions.ASORT1(ref XYP, ref PARM.NX, ref PARM.NBYR);                                                       
                        NYY[0, I - 1] = PARM.NX[(int)NY[0] - 1];
                        NYY[1, I - 1] = PARM.NX[(int)NY[1] - 1];
                        NYY[2, I - 1] = PARM.NX[(int)NY[2] - 1];
                    }
                    //!     PRINTOUT PESTICIDE FREQ SUMMARY                                                
                    for (N2 = 1; N2 <= 3; N2++)
                    {
                        //WRITE(KW(1),463)(APQ(I,K,NXX(N2,I)),I=1,5)                                   
                        //WRITE(KW(1),464)(AQB(I,K,NXX(N2,I)),I=1,5)                                   
                        //WRITE(KW(1),465)(APY(I,K,NYY(N2,I)),I=1,5)                                   
                        //WRITE(KW(1),466)(AYB(I,K,NYY(N2,I)),I=1,5)                                   
                        if (N2 == 1)
                        {
                            //WRITE(KW(1),474) 
                        }
                        if (N2 == 2)
                        {
                            //WRITE(KW(1),473)                                                         
                        }
                    }

                }
                //WRITE(KW(1),'(/1X,A)')'-----AVE ANNUAL VALUES (g/ha)'                          
                for (K = 1; K <= PARM.NDP; K++)
                {
                    for (L = 1; L <= 7; L++)
                    {
                        PARM.SMAP[L - 1, K - 1] = PARM.SMAP[L - 1, K - 1] / XYR;
                    }
                    PARM.SMAP[9, K - 1] = PARM.SMAP[9, K - 1] / XYR;
                }
                I1 = 0;
                K1 = 0;
            lbl418: I1 = I1 + 10;
                N1 = Math.Min(I1, PARM.NDP);
                K2 = K1 + 1;
                N2 = Math.Min(10, PARM.NDP - K1);
                //!     PRINTOUT PESTICIDE SUMMARY                                                     
                //WRITE(KW(1),383)(PSTN(K),K=K2,N1)                                              
                if (PARM.MASP == 0) goto lbl467;
                //WRITE(KW(1),384)HEDP(1),(SMAP(1,K),K=K2,N1)                                    
                I = 1;
                for (K = K2; K <= N1; K++)
                {
                    PPX[I - 1] = PARM.SMAP[1, K - 1];
                    Functions.ACOUT(ref PPX[I - 1], PARM.SM[13], 1.0);                                                
                    I = I + 1;
                }
                //WRITE(KW(1),387)HEDP(2),(PPX(I),I=1,N2)                                        
                I = 1;
                for (K = K2; K <= N1; K++)
                {
                    PPX[I - 1] = PARM.SMAP[2, K - 1];
                    Functions.ACOUT(ref PPX[I - 1], PARM.SM[16],1.0);                                                 
                    I = I + 1;
                }
                //WRITE(KW(1),387)HEDP(3),(PPX(I),I=1,N2)                                        
                I = 1;
                for (K = K2; K <= N1; K++)
                {
                    PPX[I - 1] = PARM.SMAP[3, K - 1];
                    Functions.ACOUT(ref PPX[I - 1], PARM.SM[15],1.0);                                                 
                    I = I + 1;
                }
                //WRITE(KW(1),387)HEDP(4),(PPX(I),I=1,N2)                                        
                for (L = 5; L <= 7; L++)
                {
                    //WRITE(KW(1),384)HEDP(L),(SMAP(L,K),K=K2,N1)                                  
                }
                I = 1;
                for (K = K2; K <= N1; K++)
                {
                    PPX[I - 1] = PARM.SMAP[9, K - 1];
                    Functions.ACOUT(ref PPX[I - 1], PARM.SM[17],1.0);                                                 
                    I = I + 1;
                }
                //WRITE(KW(1),387)HEDP(10),(PPX(I),I=1,N2)                                       
                goto lbl469;
            lbl467: for (L = 1; L <= 7; L++)
                {
                    //WRITE(KW(1),470)HEDP(L),(SMAP(L,K),K=K2,N1)                                  
                }
            //WRITE(KW(1),470)HEDP(10),(SMAP(10,K),K=K2,N1)                                  
            lbl469: if (N1 >= PARM.NDP) goto lbl212;
                K1 = I1;
                goto lbl418;
            lbl212: PPX[0] = PARM.SM[43];
                PPX[1] = PARM.SM[44];
                PPX[2] = PARM.SM[45];
                PPX[3] = PARM.SM[54];
                if (PARM.MASP > 0)
                {
                    Functions.ACOUT(ref PPX[0], PARM.SM[13],1000.0);                                              
                    Functions.ACOUT(ref PPX[1], PARM.SM[15],1000.0);                                              
                    Functions.ACOUT(ref PPX[2], PARM.SM[16],1000.0);                                              
                    Functions.ACOUT(ref PPX[3], PARM.SM[13],1000.0);                                              
                }
                PARM.TYN = PARM.TYN / XYR;
                PARM.TYP = PARM.TYP / XYR;
                PARM.TYC = PARM.TYC / XYR;
                double X6 = CSTZ[0] + CSTZ[1];
                X1 = PARM.CST1 / XYR + X6;
                X4 = PARM.CSO1 / XYR;
                X2 = PARM.VALF1 / XYR;
                X3 = X2 - X1;
                double SM1 = 0.0;
                for (K = 1; K <= 6; K++)
                {
                    XTP[K - 1] = PARM.ISIX[K - 1];
                    SM1 = SM1 + XTP[K - 1];
                }
                for (K = 1; K <= 6; K++)
                {
                    XTP[K - 1] = XTP[K - 1] / SM1;
                }
                Functions.APAGE(1);                                                                 
                //WRITE(KW(1),350)                                                               
                //WRITE(KW(1),661)(XTP(K),K=1,6)                                                 
                if (PARM.DARF > 0.0 && XYR > 1.0)
                {
                    PARM.DARF = PARM.DARF - SRF2 / XYR;
                    if (PARM.DARF > 0.0) PARM.DARF = Math.Sqrt(PARM.DARF / (XYR - 1.0));
                }
                else
                {
                    PARM.DARF = 0.0;
                }
                //WRITE(KW(1),662)BARF,SARF,DARF                                                 
                //WRITE(KW(1),323)PRB,PRAV,PRSD,QPQB,QPS,NQP                                     
                if (PARM.TCMN > 1.0E10) PARM.TCMN = 0.0;
                //WRITE(KW(1),417)TCAV,TCMN,TCMX                                                 
                //WRITE(KW(1),448)CYAV,CYMX,CYSD                                                 
                AD1 = 0.0;
                for (K = 1; K <= 10; K++)
                {
                    AD1 = AD1 + PARM.CNDS[K - 1];
                }
                for (K = 1; K <= 10; K++)
                {
                    PARM.CNDS[K - 1] = PARM.CNDS[K - 1] / AD1;
                }
                //WRITE(KW(1),234)(CNDS(K),K=1,10)                                               
                //WRITE(KW(1),303)RUSM                                                                
                if (PARM.KFL[15] == 0) goto lbl710;
                for (J = 1; J <= 6; J++)
                {
                    XTP[J - 1] = 0.0;
                    for (I = 1; I <= PARM.NBSL; I++)
                    {
                        PARM.ISL = PARM.LID[I - 1];
                        PARM.SMS[J - 1, PARM.ISL - 1] = PARM.SMS[J - 1, PARM.ISL - 1] / (PARM.SMS[10, PARM.ISL - 1] + 1.0E-5);
                        XTP[J - 1] = XTP[J - 1] + PARM.SMS[J - 1, PARM.ISL - 1];
                    }
                }
                for (J = 7; J <= 10; J++)
                {
                    XTP[J - 1] = 0.0;
                    for (I = 1; I <= PARM.NBSL; I++)
                    {
                        PARM.ISL = PARM.LID[I - 1];
                        PARM.SMS[J - 1, PARM.ISL - 1] = PARM.SMS[J - 1, PARM.ISL - 1] / XYR;
                        XTP[J - 1] = XTP[J - 1] + PARM.SMS[J - 1, PARM.ISL - 1];
                    }
                }
                for (I = 1; I <= PARM.NBSL; I++)
                {
                    PARM.ISL = PARM.LID[I - 1];
                    XYP[PARM.ISL - 1] = PARM.WOC[PARM.ISL - 1] - XZP[5, PARM.ISL - 1];
                    YTP[PARM.ISL - 1] = PARM.WON[PARM.ISL - 1] - XZP[11, PARM.ISL - 1];
                }
                XYP[15] = PARM.TOC - XZP[5, 15];
                YTP[15] = PARM.TWN - XZP[11, 15];
                //WRITE(KW(16),635)(SID(J),J=1,MSL),SID(MSL+1)                        
                PARM.XNS = PARM.NBSL;
                for (J = 1; J <= 6; J++)
                {
                    XTP[J - 1] = XTP[J - 1] / PARM.XNS;
                }
                int MS1 = PARM.MSL + 1;
                //WRITE(KW(16),581)'   Z',(Z(LID(I)),I=1,MSL),Z(LID(MSL))                        
                //WRITE(KW(16),581)' SWF',(SMS(1,LID(I)),I=1,MSL),XTP(1)                         
                //WRITE(KW(16),581)'TEMP',(SMS(2,LID(I)),I=1,MSL),XTP(2)                         
                //WRITE(KW(16),581)'SWTF',(SMS(3,LID(I)),I=1,MSL),XTP(3)                         
                //WRITE(KW(16),581)'TLEF',(SMS(4,LID(I)),I=1,MSL),XTP(4)                         
                //WRITE(KW(16),581)'SPDM',(SMS(5,LID(I)),I=1,MSL),XTP(5)                         
                //WRITE(KW(16),576)'RSDC',(SMS(7,LID(I)),I=1,MSL),XTP(7)                         
                //WRITE(KW(16),576)'RSPC',(SMS(8,LID(I)),I=1,MSL),XTP(8)                         
                //WRITE(KW(16),576)'RNMN',(SMS(9,LID(I)),I=1,MSL),XTP(9)                         
                //WRITE(KW(16),576)'DNO3',(SMS(10,LID(I)),I=1,MSL),XTP(10)                       
                //WRITE(KW(16),576)'HSC0',(XZP(1,LID(I)),I=1,MSL),XZP(1,MS1)                     
                //WRITE(KW(16),576)'HSCF',(WHSC(LID(I)),I=1,MSL),ZHSC                            
                //WRITE(KW(16),576)'HPC0',(XZP(2,LID(I)),I=1,MSL),XZP(2,MS1)                     
                //WRITE(KW(16),576)'HPCF',(WHPC(LID(I)),I=1,MSL),ZHPC                            
                //WRITE(KW(16),576)'LSC0',(XZP(3,LID(I)),I=1,MSL),XZP(3,MS1)                     
                //WRITE(KW(16),576)'LSCF',(WLSC(LID(I)),I=1,MSL),ZLSC                            
                //WRITE(KW(16),576)'LMC0',(XZP(4,LID(I)),I=1,MSL),XZP(4,MS1)                     
                //WRITE(KW(16),576)'LMCF',(WLMC(LID(I)),I=1,MSL),ZLMC                            
                //WRITE(KW(16),576)'BMC0',(XZP(5,LID(I)),I=1,MSL),XZP(5,MS1)                     
                //WRITE(KW(16),576)'BMCF',(WBMC(LID(I)),I=1,MSL),ZBMC                            
                //WRITE(KW(16),576)'WOC0',(XZP(6,LID(I)),I=1,MSL),XZP(6,MS1)                     
                //WRITE(KW(16),576)'WOCF',(WOC(LID(I)),I=1,MSL),TOC                              
                //WRITE(KW(16),576)'DWOC',(XYP(LID(I)),I=1,MSL),XYP(MS1)                         
                //WRITE(KW(16),576)'OBCF',(OBC(LID(I)),I=1,MSL),SOCF                             
                //WRITE(KW(16),576)'HSN0',(XZP(7,LID(I)),I=1,MSL),XZP(7,MS1)                     
                //WRITE(KW(16),576)'HSNF',(WHSN(LID(I)),I=1,MSL),ZHSN                            
                //WRITE(KW(16),576)'HPN0',(XZP(8,LID(I)),I=1,MSL),XZP(8,MS1)                     
                //WRITE(KW(16),576)'HPNF',(WHPN(LID(I)),I=1,MSL),ZHPN                            
                //WRITE(KW(16),576)'LSN0',(XZP(9,LID(I)),I=1,MSL),XZP(9,MS1)                     
                //WRITE(KW(16),576)'LSNF',(WLSN(LID(I)),I=1,MSL),ZLSN                            
                //WRITE(KW(16),576)'LMN0',(XZP(10,LID(I)),I=1,MSL),XZP(10,MS1)                   
                //WRITE(KW(16),576)'LMNF',(WLMN(LID(I)),I=1,MSL),ZLMN                            
                //WRITE(KW(16),576)'BMN0',(XZP(11,LID(I)),I=1,MSL),XZP(11,MS1)                   
                //WRITE(KW(16),576)'BMNF',(WBMN(LID(I)),I=1,MSL),ZBMN                            
                //WRITE(KW(16),576)'WON0',(XZP(12,LID(I)),I=1,MSL),XZP(12,MS1)                   
                //WRITE(KW(16),576)'WONF',(WON(LID(I)),I=1,MSL),TWN                              
                //WRITE(KW(16),576)'DWON',(YTP(LID(I)),I=1,MSL),YTP(MS1)                         
                //WRITE(KW(16),581)'C/N0',(XZP(13,LID(I)),I=1,MSL),XZP(13,MS1)


                for (I = 1; I <= 100; I++)
                {
                    XTP[I - 1] = 0.0;
                }
                for (I = 1; I <= PARM.NBSL; I++)
                {
                    PARM.ISL = PARM.LID[I - 1];
                    XTP[PARM.ISL - 1] = PARM.WOC[PARM.ISL - 1] / PARM.WON[PARM.ISL - 1];
                }
                XTP[MS1 - 1] = PARM.TOC / PARM.TWN;
            //WRITE(KW(16),581)'C/NF',(XTP(LID(I)),I=1,MSL),XTP(MSL+1)                       
            lbl710: Functions.APAGE(1);
                //WRITE(KW(1),350)                                                               
                //WRITE(KW(1),295)                                                               
                //WRITE(KW(1),319)IY                                                             
                //!     PRINTOUT SUMMARY MONTHLY                                                       
                //WRITE(KW(1),321)HED(1),TXMX,SM(1),HED(1)                                       
                //WRITE(KW(1),321)HED(2),TXMN,SM(2),HED(2)                                       
                //WRITE(KW(1),243)HED(4),TR,SM(4),HED(4)                                         
                //WRITE(KW(1),321)'DAYP',(SMY(I),I=1,13),'DAYP'                                  
                //WRITE(KW(1),243)HED(17),TSN,SM(17),HED(17)                                     
                //WRITE(KW(1),243)HED(14),TQ,SM(14),HED(14)                                      
                //WRITE(KW(1),321)'RZSW',ASW,SUM,'RZSW'                                          
                //WRITE(KW(1),311)HED(28),TEI,SM(28),HED(28)                                     
                //WRITE(KW(1),224)'ALPH',TAL,'ALPH'                                              
                //WRITE(KW(1),321)HED(29),W,SM(29),HED(29)                                       
                //WRITE(KW(1),325)HED(37),RCM,SM(37),HED(37)                                     
                //WRITE(KW(1),321)HED(NDVSS),TSY,SM(NDVSS),HED(NDVSS)                            
                //WRITE(KW(1),321)HED(36),RSY,SM(36),HED(36)                                     
                //WRITE(KW(1),321)HED(7),TET,SM(7),HED(7)                                        
                //WRITE(KW(1),224)HED(7),U10MX,HED(7)                                            
                //WRITE(KW(1),243)'DAYW',TAMX,ADD,'DAYW'                                         
                //WRITE(KW(1),243)HED(39),TRHT,SM(39),HED(39)                                    
                //WRITE(KW(1),243)'DAYQ',CX,AD2,'DAYQ'                                           
                //WRITE(KW(1),224)HEDC(7),TSTL,HEDC(7)                                           
                //WRITE(KW(1),311)HED(42),TYW,SM(42),HED(42)                                     
                //WRITE(KW(1),321)HED(20),QIN,TOT,HED(20)                                        
                //WRITE(KW(1),311)HED(10),SET,SM(10),HED(10)                                     
                //WRITE(KW(1),311)HED(3),TSR,SM(3),HED(3)                                        
                //WRITE(KW(1),224)'HRLT',THRL,'HRLT'                                             
                //WRITE(KW(1),'(/1X,A)')'-----AVE ANNUAL VALUES'                                 
                //WRITE(KW(1),293)IY,(HED(KA(K)),SM(KA(K)),K=5,NKA),'COST',X1,'RTRN',X2
                //WRITE(KW(1),369)(HED(JC(K)),PPX(K),K=1,NJC)                                    
                PARM.SMY[2] = 0.0;
                PARM.SMY[3] = 0.0;
                PARM.SMY[0] = PARM.SM[58] + PARM.SM[59] + PARM.SM[60];
                PARM.SMY[1] = PARM.SM[61] + PARM.SM[62];
                PARM.SMY[4] = 1000.0 * PARM.AP[PARM.LD1 - 1] / PARM.WT[PARM.LD1 - 1];
                X1 = PARM.LC;
                X6 = X6 / X1;
                for (K = 1; K <= PARM.LC; K++)
                {
                    if (PARM.NCR[K - 1] == 0) continue;
                    XX = Math.Min(PARM.NCR[K - 1], PARM.IY);
                    X3 = PARM.TETG[K - 1] + .01;
                    PARM.TETG[K - 1] = 1000.0 * (PARM.TYL1[K - 1] + PARM.TYL2[K - 1]) / X3;
                    PARM.TYL1[K - 1] = PARM.TYL1[K - 1] / XX;
                    PARM.TYL2[K - 1] = PARM.TYL2[K - 1] / XX;
                    PARM.TYLN[K - 1] = PARM.TYLN[K - 1] / XX;
                    PARM.TYLP[K - 1] = PARM.TYLP[K - 1] / XX;
                    PARM.TYLC[K - 1] = PARM.TYLC[K - 1] / XX;
                    PARM.TYLK[K - 1] = PARM.TYLK[K - 1] / XX;
                    PARM.SMY[2] = PARM.SMY[2] + PARM.TYLN[K - 1];
                    PARM.SMY[3] = PARM.SMY[3] + PARM.TYLP[K - 1];
                    PARM.TDM[K - 1] = PARM.TDM[K - 1] / XX;
                    PARM.THU[K - 1] = PARM.THU[K - 1] / XX;
                    if (PARM.IDC[K - 1] == PARM.NDC[2] || PARM.IDC[K - 1] == PARM.NDC[5] || PARM.IDC[K - 1] == PARM.NDC[6] || PARM.IDC[K - 1] == PARM.NDC[7] || PARM.IDC[K - 1] == PARM.NDC[9]) XX = PARM.IY;
                    PARM.TVAL[K - 1] = PARM.TVAL[K - 1] / XX;
                    if (PARM.KG[K - 1] > 0) XX = XX + 1.0;
                    PARM.PSTM[K - 1] = PARM.PSTM[K - 1] / XX;
                    PARM.TCSO[K - 1] = PARM.TCSO[K - 1] / XX;
                    PARM.TCST[K - 1] = PARM.TCST[K - 1] / XX + X6;
                    PARM.TFTK[K - 1] = PARM.TFTK[K - 1] / XX;
                    PARM.TFTN[K - 1] = PARM.TFTN[K - 1] / XX;
                    PARM.TFTP[K - 1] = PARM.TFTP[K - 1] / XX;
                    PARM.TFTK[K - 1] = PARM.TFTK[K - 1] / XX;
                    PARM.TRA[K - 1] = PARM.TRA[K - 1] / XX;
                    PARM.TRD[K - 1] = PARM.TRD[K - 1] / XX;
                    PARM.TVIR[K - 1] = PARM.TVIR[K - 1] / XX;
                    PARM.TIRL[K - 1] = PARM.TIRL[K - 1] / XX;
                    PARM.TCAW[K - 1] = PARM.TCAW[K - 1] / XX;
                    PARM.TCRF[K - 1] = PARM.TCRF[K - 1] / XX;
                    PARM.TCQV[K - 1] = PARM.TCQV[K - 1] / XX;
                    X3 = X3 / XX;
                    for (L = 1; L <= 4; L++)
                    {
                        PARM.TSFC[L - 1, K - 1] = PARM.TSFC[L - 1, K - 1] / XX;
                        PARM.STDA[L - 1, K - 1] = PARM.STDA[L - 1, K - 1] / XX;
                    }
                    for (L = 5; L <= 7; L++)
                    {
                        PARM.TSFC[L - 1, K - 1] = PARM.TSFC[L - 1, K - 1] / XX;
                    }
                    X1 = PARM.TVAL[K - 1] - PARM.TCST[K - 1];
                    X2 = PARM.TVAL[K - 1] - PARM.TCSO[K - 1];
                    //!     PRINTOUT CROP SUMMARY                                                          
                    //WRITE(KW(1),326)CPNM(K),TYL1(K),TYL2(K),TDM(K),TYLN(K),TYLP(K),TYLK(K),TYLC(K),TFTN(K),TFTP(K),TFTK(K),TVIR(K),TIRL(K),TCAW(K),X3,TETG(K),TRA(K),THU(K),PSTM(K),TCST(K),TCSO(K),TVAL(K),X1,X2                           
                    //WRITE(KW(1),577)(TSFC(L,K),L=1,7),(STDA(L,K),L=1,3)                            
                }
                //!     PRINTOUT SUMMARY                                                               
                if (IWP5 > 0)//REWIND KR(20)                                                        
                    if (PARM.KFL[2] > 0)
                    {
                        //WRITE(KW(3),578)(TITLE(I),I=21,35),IYER,IMON,IDAY,IY                              
                        //WRITE(KW(3),560)HED(4),HED(10),HED(11),HED(14),HED(16),HED(17),HED(29),HED(NDVSS),HED(42),HED(48),HED(47),HED(50),HED(51),HED(52),HED(49),HED(43),HED(44),HED(45),HED(46),HED(56),HED(54),HED(55),HED(57),HED(66),HED(77)                                                 
                        //WRITE(KW(3),498)SM(4),SM(10),SM(11),SM(14),SM(16),SM(17),SM(29),SM(NDVSS),SM(42),SM(48),SM(47),SM(50),SM(51),SM(52),SM(49),SM(43),SM(44),SM(45),SM(46),SM(56),SM(54),SM(55),SM(57),SM(66),SM(77),(PSTN(K),SMAP(1,K),K=1,10),(CPNM(K),TYL1(K),TYL2(K),TDM(K),TYLN(K),TYLP(K),TYLC(K),TFTN(K),TFTP(K),TVIR(K),TIRL(K),TETG(K),TCAW(K),TCRF(K),TCQV(K),THU(K),PHU(K,IHU(K)),TCST(K),TCSO(K),TVAL(K),PSTM(K),(TSFC(L,K),L=1,7),(STDA(L,K),L=1,3),K=1,LC)
                    }
                if (PARM.KFL[PARM.MSO] > 0)
                {
                    //WRITE(KW(MSO+1),694)ASTN,CPNM(1),TYL1(1),(SM(KYA(J)),J=1,NKYA)
                }
                if (PARM.KFL[PARM.NGF - 1] > 0)
                {
                    X2 = PARM.SM[48] + PARM.SM[51];
                    X3 = PARM.SM[42] + PARM.SM[43] + PARM.SM[44] + PARM.SM[45];
                    for (K = 1; K <= 8; K++)
                    {
                        PARM.SMGS[K - 1] = PARM.SMGS[K - 1] / XYR;
                    }
                    PARM.SMGS[1] = PARM.SMGS[1] / 365.25;
                    //WRITE(KW(NGF-1),512)XLOG,YLAT,SMGS(1),TETG(1),SMGS(3),SMGS(4),SM(19),SMGS(5),SM(11),SMGS(6),SMGS(7),TRA(1),TYLN(1),SMGS(2),X2,X3,SM(42),SM(NDVSS),SM(3),TDM(1),(TSFC(L,1),L=1,7),SM(46),SM(43),SM(47),SM(49),SM(50),SM(85),SM(51),SM(52),SM(53),SM(54),SM(56),SM(57),SM(58),SM(59),SM(60),SM(61),SM(62),SM(63),TYLP(1),TYLK(1),SM(77),SM(4),SM(17),SM(14),SM(5),SM(6),SM(10),SM(12),SM(13),SM(16),SM(18),SM(15),SM(20),SMGS(8),SM(68)
                }
                if (PARM.NBSL < 3 || PARM.ZF < 1.0E-10) goto lbl507;
                //!  1  JZ(1) = NUMBER OF Y FOR SECOND THRU LAST SIMULATION.                           
                //!  2  JZ(2) = 0 FOR NORMAL EROSION OF SOIL PROFILE                                   
                //!           = 1 FOR STATIC SOIL PROFILE                                              
                //!  3  JZ(3) = ID NUMBER OF WEATHER VARIABLES INPUT.  RAIN=1,  TEMP=2,                
                //!            RAD=3,  WIND SPEED=4,  REL HUM=5.  if ANY VARIABLES ARE INP             
                //!            RAIN MUST BE INCLUDED.  THUS, IT IS NOT NECESSARY TO SPECIF             
                //!            ID=1 UNLESS RAIN IS THE ONLY INPUT VARIABLE.                            
                //!            LEAVE BLANK if ALL VARIABLES ARE GENERATED.  EXAMPLES                   
                //!            NGN=1 INPUTS RAIN.                                                      
                //!            NGN=23 INPUTS RAIN, TEMP, AND RAD.                                      
                //!            NGN=2345 INPUTS ALL 5 VARIABLES.                                        
                //!  4  JZ(4) = DAILY WEATHER STA # FROM KR(27) WTHCOM.DAT                             
                //READ(KR(6),300)JZ                                                              
                if (JZ[0] == 0) goto lbl507;
                PARM.NBYR = JZ[N1 - 1];
                PARM.ISTA = JZ[1];
                PARM.NGN = JZ[2];
                IWTH = JZ[4];
                PARM.IPY = 1;
                if (ICOR == 0) goto lbl203;
                for (K = 1; K <= 12; K++)
                {
                    if (PARM.TR[K - 1] > 0.0) PARM.RNCF[K - 1] = PARM.RMO[0, K - 1] / PARM.TR[K - 1];
                    PARM.TMNF[K - 1] = (PARM.OBMX[0, K] - PARM.OBMN[0, K - 1]) / (PARM.TXMX[K - 1] - PARM.TXMN[K - 1]);
                    PARM.TMXF[K - 1] = PARM.OBMX[0, K - 1] - PARM.TXMX[K - 1];
                    if (PARM.NGN > 0) continue;
                    if (ICOR <= PARM.NC[K] - PARM.NYD) break;
                }
                ICOR = 0;
            lbl203: Functions.ARESET();
                if (PARM.NGN == 0)
                {
                    //WRITE(KW(1),'(/T10,A)')'**********RAIN, TEMP, RAD, WIND SPEED, REL HUM ARE GENERATED**********'                                                
                    goto lbl533;
                }
                if (IRW == 0)
                {
                    //REWIND KR(7)                                                                   
                    Functions.WDOP(ref IDIR[0]);
                    //!     IYR=IYR0
                }
                //WRITE(KW(1),293)                                                               
                Functions.WIGV();
                goto lbl533;
            lbl507: PARM.IGN = PARM.IGN + 100;
                //REWIND KR(6)                                                                   
                if (PARM.IGN < IGMX * 100) goto lbl538;
                PARM.IRO0 = PARM.IRO0 + 1;
                PARM.IGN = 0;
                if (PARM.IRO0 > PARM.NRO || IGMX == 0) goto lbl532;
            lbl538: //REWIND KR(1)                                                                   
                if (PARM.NGN0 > 0)
                {
                    //REWIND KR(7)  
                }
                goto lbl531;
            lbl532: //CLOSE(KR(1))                                                                   
                if (IRW == 0)//CLOSE(KR(7))                                                         
                    for (I = 2; I <= 32; I++)
                    {
                        //CLOSE(KW(I))                                                                 
                    }
                Functions.ATIMER(1);
                //CLOSE(KW(1))
                if (PARM.IBAT > 0) goto lbl219;
            lbl740: continue;
            lbl219: for (I = 1; I <= PARM.KR.Length; I++)
                {
                    //CLOSE(KR(I))                                                                 
                }
                for (I = 2; I <= PARM.KW.Length; I++)
                {
                    //CLOSE(KW(I))                                                                 
                }


            }
        }
    }
}