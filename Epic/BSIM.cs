using System;

namespace Epic
{
	public class BSIM
	{
		public BSIM ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program drives the daily simulation for any number of years

			// The fortran file uses global variables, and needs to be fixed
			// for C# style coding

            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			double JRT = 0;
			int K = 0;
			double II = 0.0;
			int L1 = 0;
            char ANMX;
            double[] PPX;
            string[,] HD28 = new string[15, 10] {{"  Z1","  Z2","  Z3","  Z4","  Z5","  Z6","  Z7","  Z8","  Z9"," Z10"},{" Z11"," Z12"," Z13"," Z14"," Z15"," SW1"," SW2"," SW3"," SW4"," SW5"},{" SW6"," SW7"," SW8"," SW9","SW10","SW11","SW12","SW13","SW14","SW15"},{" WU1"," WU2"," WU3"," WU4"," WU5"," WU6"," WU7"," WU8"," WU9","WU10"},{"WU11","WU12","WU13","WU14","WU15"," EV1"," EV2"," EV3"," EV4"," EV5"},{" EV6"," EV7"," EV8"," EV9","EV10","EV11","EV12","EV13","EV14","EV15"},{" PK1"," PK2"," PK3"," PK4"," PK5"," PK6"," PK7"," PK8"," PK9","PK10"},{"PK11","PK12","PK13","PK14","PK15"," SF1"," SF2"," SF3"," SF4"," SF5"},{" SF6"," SF7"," SF8"," SF9","SF10","SF11","SF12","SF13","SF14","SF15"},{" N31"," N32"," N33"," N34"," N35"," N36"," N37"," N38"," N39","N310"},{"N311","N312","N313","N314","N315"," PARM.UN1"," UN2"," UN3"," UN4"," UN5"},{" UN6"," UN7"," UN8"," UN9","UN10","UN11","UN12","UN13","UN14","UN15"},{" LN1"," LN2"," LN3"," LN4"," LN5"," LN6"," LN7"," LN8"," LN9","LN10"},{"LN11","LN12","LN13","LN14","LN15","  T1","  T2","  T3","  T4","  T5"},{"  T6","  T7","  T8","  T9"," T10"," T11"," T12"," T13"," T14"," T15"}};
            double[,] KDT = new double[12, 12];
            double[] KTP = new double[7];
            int[] MNST = new int[7];
            double [,,] XTP = new double[4, 12, 12];
            double [] YTP = new double[16];
            PPX = new double[13];

            double AIR, SAIR, TSMQ, TSMY;
            AIR = SAIR = TSMQ = TSMY = (4 * 0.0); // I have no idea why they did this
            MNST = new int[7] {1, 2, 3, 4, 5, 6, 7};
            double INO, KTF;
            INO = KTF = (2*0); // I have no idea why they did this
            //PARM.PMAV[FMO1,FMO2,X1,X2] = (FMO1*X1+FMO2*X2)/30.0; // There is no def for FMO1 and FMO2.
            double PLC0 = 0.0;

            //if (PARM.KFL[5] > 0) continue; //WRITE(KW[5],471)HEDP
            PARM.JP[PARM.JJK] = 0;
            //if (PARM.KFL[10] > 0) continue; //WRITE(KW[10],517)
            double TILG = 0.0;
            //if (PARM.KFL[11] > 0) continue; //WRITE(KW[11],526)HEDS[6],HED[4],HED[11],HED[14],HED[17],HED[16];
            //if (PARM.KFL[12] > 0) continue; //WRITE(KW[12],527)
//!     IF(PARM.KFL[6]>0)WRITE(KW[6],477)(HED[PARM.KA[I]],I=1,PARM.NKA)
	        //if (PARM.KFL[6] > 0) //WRITE(KW[6],477)(HED[IFS[J]],J=1,NFS),(HEDS[I],I=4,6)
	        //if (PARM.KFL[22] > 0) continue; //WRITE(KW[22],520)HED[4],HED[10],HED[11],HED[13],HED[14],(HED[I],I=16,20),(HEDS[I],I=6,8),'RNO3',(HED[I],I=43,46),HED[49],HED[89],HED[52],HED[85],HED[50],(HED[I],I=59,61),'PARM.UNO3',' YLN','PARM.CPNM',' PARM.YLD','TOTN'
            //if (PARM.KFL[27] > 0) continue; //WRITE(KW[27],516)HED[4],HED[10],HED[11],HED[13],HED[14],(HED[I],I=16,20),(HEDS[I],I=6,8)
            //if (PARM.KFL[28] > 0) continue; //WRITE(KW[28],731)HED[4],HED[10],HED[11],(HED[I],I=13,20],(HEDS[I],I=6,8),((HD28[I,J],I=1,PARM.NBSL),J=1,10)
            double SILG = PARM.AILG;
            double J2 = 0;
            int J1 = 1;
	        double IGIS = PARM.NGF;
	        PARM.DPLC=0.0;
	        //DO 87 IY=1,NBYR DO 87?
            PARM.JDA = PARM.IBD;
	        //KDT = 0.0;
			foreach (int x in KDT){
				foreach (int y in KDT){
					KDT[x, y] = 0.0;	
				}
			}
	        double SWGS = 0.0;
	        PARM.HSM = 0.0;
	        //if (PARM.IBAT == 0) continue; //WRITE(*,156)PARM.IY,NBYR
			
			double X1;
            if (PARM.ICO2 == 1){
                if (PARM.IYX < 25){
                    PARM.CO2 = 280.33;
                }
                else{
                    X1 = PARM.IYX;
                    PARM.CO2 = 280.33-X1*(.1879-X1*.0077);
                }
            }
            
			double IRLX;
            PARM.IRO = PARM.IRO+1;
            if (PARM.IRO > PARM.NRO) PARM.IRO = 1;
            if (PARM.IRO < PARM.NRO){
                IRLX = 0;
            }
            else{
                PARM.IRL = PARM.IRL+1;
                IRLX = PARM.IRL;
            }

            double NN = PARM.NBC[PARM.IRO];
            double NN1 = PARM.NTL[PARM.IRO];
            
            int I, J;
            for (I = 0; I < PARM.LC; I++){
                PARM.WA[I] = 100.0*PARM.CO2/(PARM.CO2+Math.Exp(PARM.WAC2[0,I]-PARM.CO2*PARM.WAC2[1,I]));
            }

            //if (PARM.KFL[7]>0) continue; //WRITE(KW[7],474)IRUN,IRO0,PARM.IGN,PARM.IY

            for (I = 0; I < PARM.MNC; I++){
                PARM.JE[I] = PARM.MNC+1;
                if (PARM.KG[I] == 0) continue;
                for (J = 0; J < NN; J++){
                    // This loop seems worthless
                    if (I == PARM.LY[PARM.IRO,J]) break;
                }
                PARM.JE[J] = I;
            }

            SILG = TILG+SILG;
            PARM.AILG = SILG/PARM.IY;
            TILG = 0.0;
            double TFLG = 0.0;
            //PARM.KOMP = 0;
			foreach (int x in PARM.KOMP){
				PARM.KOMP[x] = 0;	
			}
            PARM.IGZ = 0;
            PARM.KTT = 0;
            double KTMX = 1;
            double JJ = PARM.IBD-1;
            PARM.JT1 = PARM.LT[PARM.IRO,PARM.KT];
            //Functions.APAGE(1); // All this file does is write to file, so it hasn't been translated
            //WRITE(KW[1],582)PARM.CO2
            //WRITE(KW[1],94)
            PARM.ND = 366-PARM.NYD;
            PARM.JP[1] = 0;
            PARM.JP[2] = 0;
            double IPC = 1;

            if (PARM.IPAT > 0){
                if (PARM.APBC < 20.0){
                    PARM.APMU = 2.25*(30.0-PARM.APBC);
                    PARM.JJK = PARM.LY[PARM.IRO,1];
                    //if (PARM.APMU > 45.0) Functions.NFERT(5,PARM.JT1);
                }
            }

            X1 = .2+.3*Math.Exp(-.0256*PARM.SAN[PARM.LD1]*(1.0-.01*PARM.SIL[PARM.LD1]));
            double X2 = Math.Pow((PARM.SIL[PARM.LD1]/(PARM.CLA[PARM.LD1]+PARM.SIL[PARM.LD1])),.3);
            double X5 = .1*PARM.WOC[PARM.LD1]/PARM.WT[PARM.LD1];
            
			double X3;
            if (X5 > 5.0){
                X3 = .75;
            }
            else{
                X3 = 1.0-.25*X5/(X5+Math.Exp(3.718-2.947*X5));
            }

            double XX = 1.0-.01*PARM.SAN[PARM.LD1];
            double X4 = 1.0-.7*XX/(XX+Math.Exp(-5.509+22.899*XX));
            PARM.EK = X1*X2*X3*X4;
            PARM.USL = PARM.EK*PARM.SL*PARM.PEC;
            double SUM = (PARM.SAN[PARM.LD1]*.0247-PARM.SIL[PARM.LD1]*3.65-PARM.CLA[PARM.LD1]*6.908)/100.0;
            double DG = Math.Exp(SUM);
            PARM.REK = 9.811*(.0034+.0405*Math.Exp(-.5*Math.Pow(((Math.Log10(DG)+1.659)/.7101),2)));
            PARM.RSK = PARM.REK*PARM.PEC*PARM.RSF;
            PARM.RSLK = PARM.RSK*PARM.RLF;
            //if (PARM.ISTA == 0 || PARM.IY == 1) Functions.EWIK();
            double NT1 = 0;
			
			double NFL = 0; // NFL does not exist, and yet the fortran file uses it some how
            if (PARM.IWP5 == 0){
                //PARM.IWIX = 1;
				foreach (int x in PARM.IWIX){
					PARM.IWIX[x] = 1;	
				}
            }
            else{
                //READ(KR[20],533,IOSTAT=NFL)PARM.IWIX
	            if(NFL != 0){
	                //PARM.IWIX = 1;
					foreach (int x in PARM.IWIX){
						PARM.IWIX[x] = 1;	
					}
                }
                else{
                    for (I = 0; I < 11; I++){
                        if (PARM.IWIX[I] != 0){
                            PARM.IWIX[I] = PARM.IWIX[I]+1;
                            continue;
                        }
                        PARM.IWIX[I] = 1;
                    }
                }
            }


//!     BEGIN DAILY SIMULATION.
            //DO 84 IDA=PARM.IBD,PARM.ND
            Functions.AICL();
            double XDA = PARM.KDA;
            PARM.NMW = PARM.NMW+1;
            PARM.NWI = PARM.IWIX[PARM.MO];
            PARM.ISIX[PARM.NWI] = PARM.ISIX[PARM.NWI]+1;
            double IP15 = PARM.IDA+15;
			double MOP = 0.0; // MOP does not exist in modparam so I don't see how it's used again....
            Functions.AXMON(ref IP15, ref MOP);
            PARM.DFX = 0.0;
            if (IP15 > PARM.ND) IP15 = IP15-PARM.ND;
            double FMO2 = Math.Min(30.0, IP15-PARM.NC[(int)MOP]);
            double FMO1 = 30.0-FMO2;
            double I1 = MOP-1;
            if (I1 < 1) I1 = 12;
            double I2 = PARM.IWIX[(int)I1];
            double I3 = PARM.IWIX[(int)MOP];
			
			// PMAV doesn't exist, and again there is no clear def for it.
			
            //PARM.RFVM = PARM.PMAV(FMO1,FMO2,RST[0,I2,I1],RST[0,I3,MOP]);
            //RFSD = PARM.PMAV(FMO1,FMO2,RST[1,I2,I1],RST[1,I3,MOP]);
            //RFSK = PARM.PMAV(FMO1,FMO2,RST[2,I2,I1],RST[2,I3,MOP]);
            //PRWM = PARM.PMAV(FMO1,FMO2,PRW[LW,I2,I1],PRW[LW,I3,MOP]);
            //PARM.TMXM = PARM.PMAV(FMO1,FMO2,OBMX[I2,I1],OBMX[I3,MOP]);
            //PARM.TMNM = PARM.PMAV(FMO1,FMO2,OBMN[I2,I1],OBMN[I3,MOP]);
            //TXSD = PARM.PMAV(FMO1,FMO2,SDTMX[I2,I1],SDTMX[I3,MOP]);
            //TNSD = PARM.PMAV(FMO1,FMO2,SDTMN[I2,I1],SDTMN[I3,MOP]);
            //PARM.SRAM = PARM.PMAV(FMO1,FMO2,OBSL[I2,I1],OBSL[I3,MOP]);
            IPC = Math.Max(IPC,PARM.IDA);
            double XDA1 = 31.0-XDA;
            double LNS = PARM.LID[PARM.NBSL];
            PARM.YW = 0.0;
            PARM.REP = 0.0;
            PARM.ER = 1.0;
            //PARM.VAR = 0.0;
			foreach (int x in PARM.VAR){
				PARM.VAR[x] = 0.0;	
			}
            //PARM.CGSF = 0.0;
			foreach (int x in PARM.CGSF){
				foreach (int y in PARM.CGSF){
					PARM.CGSF[x, y] = 0.0;	
				}
			}
            PARM.BCV = 1.0;
            PARM.RCF = .9997*PARM.RCF;
            if (PARM.CV < 10.0) PARM.BCV = PARM.CV/(PARM.CV+Math.Exp(PARM.SCRP[4,0]-PARM.SCRP[4,1]*PARM.CV));
            PARM.SNOF = 0.0;

            if (PARM.SNO > 0.0){
                PARM.SNOF = PARM.SNO/(PARM.SNO+Math.Exp(PARM.SCRP[16,0]-PARM.SCRP[16,1]*PARM.SNO));
                PARM.BCV = Math.Max(PARM.SNOF,PARM.BCV);
            }

            PARM.BCV = PARM.BCV*.85;
            if (PARM.NGN == 0) goto lbl11;
            if (PARM.IGSD == 0) goto lbl504;
            if (PARM.IY != PARM.IGSD) goto lbl504;
            if (PARM.IDA > PARM.NSTP) goto lbl508;

/*!     READ DAILY WEATHER IF NOT GENERATED
!  1  PARM.SRAD   = PARM.SOLAR RADIAION(MJ/M2 OR PARM.LY)(BLANK TO GENERATE
!  2  PARM.TMX  = MAX TEMP(C)
!  3  PARM.TMN  = MIN TEMP(C)
!  4  PARM.RFV  = RAINFALL(mm)(999. TO GENERATE OCCURRENCE & AMOUNT;
!            -1. TO GENERATE AMOUNT GIVEN OCCURRENCE)
!  5  PARM.RHD  = RELATIVE HUMIDITY (FRACTION)(BLANK TO GENERATE
!  6  PARM.U10  = WIND VELOCITY (M/S)(BLANK TO GENERATE
!  7  X1   = ATMOSPHERIC PARM.CO2 CONC (ppm)*/

    lbl504: //READ(KR[7],103,IOSTAT=NFL)PARM.SRAD,PARM.TMX,PARM.TMN,PARM.RFV,PARM.RHD,PARM.U10,X1,PARM.REP
            if (NFL != 0) goto lbl508;
            if (X1 > 0.0 && PARM.ICO2 == 2) PARM.CO2 = X1;
            PARM.SRAD = PARM.SRAD*PARM.RUNT;
            if (PARM.RHD > 1.0) PARM.RHD = .01*PARM.RHD;
            double III = 0;
            goto lbl507;

    lbl508: PARM.NGN = 0;
            //WRITE(KW[1],'(/T10,3I4,A,A)')PARM.IYR,PARM.MO,PARM.KDA,' RAIN, TEMP, RAD, WIND'&,' SPEED, & REL HUM ARE GENERATED'
            goto lbl11;

    lbl507: if (PARM.RFV<0.0){
                //Functions.WRWD(1);
                PARM.RFV = PARM.RFV*PARM.RNCF[PARM.MO];
            }

            if (PARM.KGN[2] > 0){
                if (Math.Abs(PARM.TMX-PARM.TMN) > 0.0){
                    if (PARM.TMX>100.0 || PARM.TMN > 100.0){
                        //Functions.WGN();
                        //Functions.WTAIX();
                        goto lbl18;
                    }
                    else{
                        if (PARM.TMX > PARM.TMN) goto lbl8;
                    }
                }
            }
            //Functions.WGN();
            //Functions.WTAIR();

     lbl18: X1 = PARM.TMX-PARM.TMN;
            PARM.TMX = PARM.TMX+PARM.TMXF[PARM.MO];
            PARM.TMN = PARM.TMX-PARM.TMNF[PARM.MO]*X1;

      lbl8: if (PARM.RFV>900.0) {}//Functions.WRWD(0);
            if (PARM.SRAD<1.0E-5 || PARM.KGN[3]==0){
                if (III == 0) {} //Functions.WGN();
                //Functions.WSOLRA();
            }
            if (PARM.RHD < Math.Pow(10,-5) || PARM.RHD > 900.0 || PARM.KGN[5] == 0){
                if (III == 0) {}//Functions.WGN();
                //Functions.WRLHUM();
            }
            if (PARM.U10 > 0 && PARM.U10 < 900.0 && PARM.KGN[4] > 0) goto lbl13;
            goto lbl12;

     lbl11: PARM.U10 = 0.0;
            PARM.RHD = 0.0;
            //Functions.WRWD(0);
            PARM.RFV = PARM.RFV*PARM.RNCF[PARM.MO];
            //Functions.WGN();
            //Functions.WTAIR();
            X1 = PARM.TMX-PARM.TMN;
            PARM.TMX = PARM.TMX+PARM.TMXF[PARM.MO];
            PARM.TMN = PARM.TMX-PARM.TMNF[PARM.MO]*X1;
            //Functions.WPARM.SOLRA();
            //Functions.WRLHUM();

     lbl12: //Functions.WNSPD();
				
				
     lbl13: if (PARM.ACW>0 && PARM.SNO < 10.0){
                //Functions.WNDIR();
                //Functions.EWER(JRT);
                if (JRT == 0){
                    PARM.YW = PARM.YW*PARM.ACW;
                    PARM.SMM[40,PARM.MO] = PARM.SMM[40,PARM.MO]+PARM.RGRF;
                    PARM.VAR[40] = PARM.RGRF;
                }
            }

            //Functions.WHRL();
            //Functions.WRMX();
            PARM.IHRL[PARM.MO] = PARM.IHRL[PARM.MO]+1;
            PARM.THRL[PARM.MO] = PARM.THRL[PARM.MO]+PARM.HRLT;
            PARM.SRMX[PARM.MO] = PARM.SRMX[PARM.MO]+PARM.RAMX;

            if (PARM.IAZM>0){
                PARM.SRAM = PARM.RAMX*Math.Max(.8,.21*Math.Sqrt(PARM.TMXM-PARM.TMNM));
                //Functions.WGN();
                //Functions.WSOLRA();
            }
            PARM.QNO3 = 0.0;
            PARM.SMM[6,PARM.MO] = PARM.SMM[6,PARM.MO]+PARM.U10;
            PARM.VAR[6] = PARM.U10;
            PARM.SMM[7,PARM.MO] = PARM.SMM[7,PARM.MO]+PARM.RHD;
            PARM.VAR[7] = PARM.RHD;
            PARM.TX = (PARM.TMN+PARM.TMX)/2.0;
            if ( PARM.TX > 0.0) PARM.HSM = PARM.HSM+PARM.TX;
            //Functions.SOLT();
            double LD2 = PARM.LID[2];
            PARM.SMM[66,PARM.MO] = PARM.SMM[66,PARM.MO]+PARM.STMP[(int)LD2];
            PARM.VAR[66] = PARM.STMP[(int)LD2];
            PARM.YP=0.0;
            PARM.YN = 0.0;
            PARM.QAP=0.0;

            if (PARM.RFV>0.0){
                PARM.SRD[PARM.MO] = PARM.SRD[PARM.MO]+1.0;
                PARM.SMM[3,PARM.MO] = PARM.SMM[3,PARM.MO]+PARM.RFV;
                PARM.VAR[3] = PARM.RFV;
                PARM.ARF = PARM.ARF+PARM.RFV;
                PARM.TSNO = 0.0;
            }

            PARM.SMM[0,PARM.MO] = PARM.SMM[0,PARM.MO]+PARM.TMX;
            PARM.RFNC = PARM.RFNC*PARM.RFV;
            PARM.RNO3 = PARM.RFNC;
            PARM.VAR[0] = PARM.TMX;
            PARM.SMM[1,PARM.MO] = PARM.SMM[2,PARM.MO]+PARM.TMN;
            PARM.VAR[1] = PARM.TMN;
            PARM.SMM[2,PARM.MO] = PARM.SMM[2,PARM.MO]+PARM.SRAD;
            PARM.VAR[2] = PARM.SRAD;
            PARM.SML = 0.0;
            //PARM.YSD = 0.0;
			foreach (int x in PARM.YSD){
				PARM.YSD[x] = 0.0;	
			}
            PARM.YERO = 0.0;
            PARM.CVF = 0.0;
            PARM.QP = 0.0;
            PARM.QD = 0.0;
            PARM.EI = 0.0;
            PARM.RWO = 0.0;
            PARM.SNMN = 0.0;
            PARM.VSLT = 0.0;
            PARM.YLN = 0.0;
            PARM.AL5 = .02083;
            double RFRR = 0.0;
            PARM.CRKF = 0.0;
            //PARM.SSF = 0.0;
			foreach (int x in PARM.SSF){
				PARM.SSF[x] = 0.0;	
			}
            PARM.TSNO = PARM.TSNO+1.0;
            
            if (PARM.TX <= 0.0){
                double DSNO = PARM.RFV;
                PARM.SNO = PARM.SNO+DSNO;
                PARM.SMM[4,PARM.MO] = PARM.SMM[4,PARM.MO]+PARM.RFV;
                PARM.VAR[4] = PARM.RFV;
                PARM.RFV = 0.0;
                goto lbl580;
            }

            if (PARM.SNO > 0.0 && PARM.SRAD > 10.0) //Functions.HSNOM();
            PARM.RWO = PARM.RFV+PARM.SML;
            PARM.SMM[5,PARM.MO] = PARM.SMM[5,PARM.MO]+PARM.SML;
            PARM.VAR[5] = PARM.SML;
            if (PARM.RWO < Math.Pow(10, -10)) goto lbl580;
            if (PARM.RFV > Math.Pow(10, -10)) //Functions.HRFEI();
            PARM.RFV = PARM.RWO;
            PARM.VAR[27] = PARM.EI;
            X1 = PARM.CLA[PARM.LD1];
            X5 = .1*PARM.WOC[PARM.LD1]/PARM.WT[PARM.LD1];
            X2 = Math.Max(50.0,63.0+62.7*Math.Log(X5)+15.7*X1-.25*X1*X1);
            RFRR = Math.Pow((PARM.RFV/X2), .6);
            //Functions.HVOLQ();
            PARM.RFSM = PARM.RFSM+PARM.RFV;
            PARM.JCN = PARM.JCN+1;
            PARM.SMM[14,PARM.MO] = PARM.SMM[14,PARM.MO]+PARM.CN;
            PARM.VAR[14] = PARM.CN;

            if (PARM.QD > 1.0){
                PARM.NQP = PARM.NQP+1;
                PARM.VAR[94] = PARM.QP;
                PARM.PRAV = PARM.PRAV+PARM.QP;
                PARM.PRSD = PARM.PRSD+PARM.QP*PARM.QP;
                if (PARM.QP > PARM.PRB) PARM.PRB = PARM.QP;
                double QPQ = PARM.QP/PARM.QD;
                if (QPQ > PARM.QPQB) PARM.QPQB = QPQ;
                PARM.QPS = PARM.QPS+QPQ;
                PARM.TCAV = PARM.TCAV+PARM.TC;
                if (PARM.TC > PARM.TCMX){
                    PARM.TCMX = PARM.TC;
                }
                else{
                    if (PARM.TC < PARM.TCMN) PARM.TCMN = PARM.TC;
                }
            }
        //!     COMPUTE SEDIMENT PARM.YLD
            if (PARM.ISTA == 0 && PARM.LUN != 35) {} //Functions.EYSED();

    lbl580: XX = Math.Exp(-RFRR);
            PARM.RRUF = Math.Max(1.0,PARM.RRUF*XX);
            PARM.SMM[39,PARM.MO] = PARM.SMM[39,PARM.MO]+PARM.RRUF;
            PARM.VAR[39] = PARM.RRUF;
            XX = Math.Exp(-.1*PARM.YSD[2]);
            PARM.RHTT = Math.Max(.001,PARM.RHTT*XX);
            PARM.DHT = Math.Max(.001,PARM.DHT*XX);
            if (PARM.DHT/(PARM.DKHL+Math.Pow(10, -5)) < .7) PARM.DHT = PARM.DKHL;
            PARM.SMM[38,PARM.MO] = PARM.SMM[38,PARM.MO]+PARM.RHTT;
            PARM.VAR[38] = PARM.RHTT;
            PARM.YERO = PARM.YSD[PARM.NDRV]+PARM.YW;
            X1 = .9*PARM.WT[PARM.LD1]; 
			
			double RTO;
            if (PARM.YERO > X1){
                RTO = X1/PARM.YERO;
                PARM.YSD[PARM.NDRV] = PARM.YSD[PARM.NDRV]*RTO;
                PARM.YW = PARM.YW*RTO;
                PARM.YERO = X1;
            }

            PARM.SMM[31,PARM.MO] = PARM.SMM[31,PARM.MO]+PARM.YSD[1];
            PARM.VAR[31] = PARM.YSD[1];
            PARM.SMM[32,PARM.MO] = PARM.SMM[32,PARM.MO]+PARM.YSD[3];
            PARM.VAR[32] = PARM.YSD[3];
            PARM.SMM[30,PARM.MO] = PARM.SMM[30,PARM.MO]+PARM.YSD[4];
            PARM.VAR[30] = PARM.YSD[4];
            PARM.VAR[57] = PARM.ER;
            PARM.SMM[82,PARM.MO] = PARM.SMM[82,PARM.MO]+PARM.YSD[5];
            PARM.VAR[82] = PARM.YSD[5];
            double CY = Math.Pow(10, 5)*PARM.YSD[PARM.NDRV]/(PARM.QD+Math.Pow(10, -5));
            double WYAV = PARM.CYAV+CY;
            PARM.CYSD = PARM.CYSD+CY*CY;
            if (CY > PARM.CYMX) PARM.CYMX = CY;
            PARM.SMM[29,PARM.MO] = PARM.SMM[29,PARM.MO]+PARM.YSD[2];
            PARM.VAR[29] = PARM.YSD[2];
            PARM.VAR[28] = PARM.CVF;
            PARM.SMM[34,PARM.MO] = PARM.SMM[34,PARM.MO]+PARM.YSD[7];
            PARM.VAR[34] = PARM.YSD[7];
            PARM.SMM[35,PARM.MO] = PARM.SMM[35,PARM.MO]+PARM.YSD[6];
            PARM.VAR[35] = PARM.YSD[6];
            PARM.SMM[41,PARM.MO] = PARM.SMM[41,PARM.MO]+PARM.YW;
            PARM.VAR[41] = PARM.YW;
            PARM.RFV = PARM.RFV+SAIR;
            PARM.GWST = Math.Max(0.0,PARM.GWST+PARM.RFV-PARM.PRMT[14]);
            if (PARM.GWST > PARM.GWMX) PARM.GWST = PARM.GWMX;
            PARM.RZSW = PARM.RZSW+SAIR;
            AIR = 0.0;
            SAIR = 0.0;
            //Functions.HPURK();
            PARM.PKRZ[(int)LNS] = PARM.PKRZ[(int)LNS]+PARM.CRKF;
            PARM.SMM[16,PARM.MO] = PARM.SMM[16,PARM.MO]+PARM.PKRZ[(int)LNS];
            PARM.VAR[16] = PARM.PKRZ[(int)LNS];
            PARM.SMM[15,PARM.MO] = PARM.SMM[15,PARM.MO]+PARM.SST;
            PARM.VAR[15] = PARM.SST;
            //Functions.HEVP();
            PARM.SMM[9,PARM.MO] = PARM.SMM[9,PARM.MO]+PARM.EO;
            PARM.VAR[9] = PARM.EO;
            double AET = PARM.ES;
            PARM.ADRF = (XDA*(PARM.SMM[3,PARM.MO]-PARM.SMM[13,PARM.MO])+XDA1*PARM.PMORF)/31.0;
            if (PARM.WTMN < PARM.Z[(int)LNS]) {}//Functions.HWTBL();

            if (PARM.IRR == 4){
                PARM.WTMU = PARM.WTMU+PARM.FNPI;
                PARM.SMM[25,PARM.MO] = PARM.SMM[25,PARM.MO]+PARM.FNPI;
                PARM.VAR[25] = PARM.FNPI;
                //Functions.HLGOON(JRT);
                if (PARM.RZSW-PARM.PAW < PARM.BIR && JRT == 0){
                    PARM.FNP = Math.Min(PARM.WTMU,PARM.CFNP*PARM.VLGI);
                    PARM.SMM[26,PARM.MO] = PARM.SMM[26,PARM.MO]+PARM.FNP;
                    PARM.VAR[26] = PARM.FNP;
                    PARM.WTMU = PARM.WTMU-PARM.FNP;
                    PARM.FNP = PARM.FNP/PARM.WSA;
                    TFLG = TFLG+PARM.FNP;
                    TILG = TILG+PARM.VLGI;
                    PARM.CFNP = 10.0*PARM.WTMU*PARM.WSA/PARM.VLG;
                    //Functions.NFERT(2,PARM.JT1);
                    AIR = PARM.VLGI;
                    //Functions.HIRG(AIR,1.0,0.0,JRT,PARM.JT1,1);
                    SAIR = SAIR+AIR;
                }
            }

            PARM.NFA = (int)PARM.VLGI+1;
            PARM.NII = PARM.NII+1;
            if (PARM.MNU > 0 && PARM.NFA >= PARM.IFA) {}//Functions.NFERT(2,PARM.JT1);
            double IRGX = 0;
            if (PARM.NYD == 0 && PARM.IDA == 60){
                NT1 = 1;
                goto lbl30;
            }
            int NB1 = PARM.KT;
            double KHV = 0;


            
            for (PARM.KT = NB1; PARM.KT < NN1; PARM.KT++){
                if (PARM.KOMP[PARM.KT] > 0) continue;
                PARM.XHSM = PARM.HSM/PARM.HSM;
                if (KTF == 0) KTF = PARM.KT;
                for (K = 0; K < PARM.LC; K++){
                    if (PARM.JH[PARM.IRO,PARM.KT] == PARM.KDC[K]) break;
                }
                if (K > PARM.LC) K = 1;
                PARM.JJK = K;
                if (PARM.IHUS > 0 && PARM.IY > 1) goto lbl443;
                if (PARM.IDA < PARM.ITL[PARM.IRO,PARM.KT]+NT1) goto lbl25;
        lbl443: if (PARM.KG[PARM.JJK] == 0 && PARM.JPL[PARM.JJK] == 0){
                    if (PARM.IY == 1 && PARM.IHC[PARM.KT] == PARM.NHC[2]) goto lbl590;
                }
                else{
                    PARM.XHSM = PARM.HU[PARM.JJK]/PARM.PHU[PARM.JJK,PARM.IHU[PARM.JJK]];
                }
                if (PARM.XHSM >= PARM.HUSC[PARM.IRO,PARM.KT]) goto lbl590;
                if (PARM.MO<PARM.MOFX || PARM.IDC[PARM.JJK] == PARM.NDC[7] || PARM.IDC[PARM.JJK] == PARM.NDC[8] || PARM.IDC[PARM.JJK] == PARM.NDC[10]) goto lbl25;
	            goto lbl121;
        lbl590: if (PARM.PDSW/PARM.FCSW < PARM.PRMT[59]) goto lbl121;
	            //WRITE(KW[1],589)PARM.IY,PARM.MO,PARM.KDA,PARM.PDSW,FCSW
                if (PARM.MO < PARM.MOFX) goto lbl25;
        lbl121: PARM.JT1 = PARM.LT[PARM.IRO,PARM.KT];
                PARM.KOMP[PARM.KT] = 1;
                if (PARM.KT > KTMX) KTMX = PARM.KT;
                double CSTX = PARM.COTL[PARM.JT1];
                double COX = PARM.COOP[PARM.JT1];
                if (PARM.IHC[PARM.JT1] == PARM.NHC[7]){
                    if (IRGX > 0){
                        PARM.KOMP[PARM.KT] = 0;
                        continue;
                    }
                    AIR = PARM.VIRR[PARM.IRO,PARM.KT];
                    double IRY = 0;
                    if (AIR > 0.0) IRY = 1;
                    //Functions.HIRG(AIR,PARM.EFM[PARM.JT1],PARM.TLD[PARM.JT1],JRT,PARM.JT1,IRY);
                    if (JRT != 0){
	                    PARM.KOMP[PARM.KT] = 0;
                        continue;
                    }
                    else{
                        IRGX = 1;
                        SAIR = SAIR+AIR;
                        goto lbl469;
                    }
                }

                if(PARM.IHC[PARM.JT1] == PARM.NHC[8]){
                    //Functions.NFERT(1,PARM.JT1);
                    if (PARM.IDA != J1 || PARM.NBT[PARM.JT1] > 0 || PARM.NBE[PARM.JT1] != J2){
                        J1 = PARM.IDA;
                        J2 = PARM.NBE[PARM.JT1];
                        goto lbl469;
                    }
                    else{
                        CSTX = 0.0;
                        COX = 0.0;
                    }
                }

                if (PARM.IHC[PARM.JT1] == PARM.NHC[6]){
                    if (PARM.QD>PARM.PRMT[57]){
                        //WRITE(KW[1],588)PARM.IY,PARM.MO,PARM.KDA,PARM.QD
                        PARM.KOMP[PARM.KT] = 0;
                        continue;
                    }
                    else{
	                    //Functions.PSTAPP();
	                    if (PARM.IDA == J1 && PARM.NBT[PARM.JT1] == 0 && PARM.NBE[PARM.JT1] == J2){
                            CSTX = 0.0;
                            COX = 0.0;
                        }
                    }
                }

                if (PARM.IHC[PARM.JT1] == PARM.NHC[19]){
                    PARM.KOMP[NB1] = 1;
	                KTF = KTF+1;
                    continue;
                }
                J1 = PARM.IDA;
                J2 = PARM.NBE[PARM.JT1];
        lbl469: if (PARM.LUN != 35) //Functions.TLOP(CSTX,COX,JRT);
                if (JRT>0) goto lbl25;
                PARM.SMM[95,PARM.MO] = PARM.SMM[95,PARM.MO]+PARM.TCEM[PARM.JT1];
                if (PARM.IFD != 0 && PARM.DKHL > 0.0){
                    PARM.DHT = PARM.DKHL;
                    if (PARM.NOP > 0) continue; //WRITE(KW[1],92)PARM.IYR,PARM.MO,PARM.KDA,PARM.DHT,DKIN,PARM.XHSM
                }
                PARM.COST = PARM.COST+CSTX;
                PARM.CSFX = PARM.CSFX+COX;
                PARM.JT2 = PARM.JT1;
            }

            KTF = NB1;
     lbl25: if (PARM.KTT > 0) PARM.IGZ = PARM.IGZ+1;
            PARM.KT = (int)KTF;
            PARM.BIR = PARM.TIR[PARM.IRO,(int)KTMX];
            PARM.EFI = PARM.QIR[PARM.IRO,(int)KTMX];
            PARM.CFMN = PARM.CFRT[PARM.IRO,(int)KTMX];
            PARM.JT1 = PARM.LT[PARM.IRO,PARM.KT];
            KTF = 0;
            if (Math.Abs(PARM.BIR) < Math.Pow(10, -5)) goto lbl30;
            if (PARM.BIR > 0.0) goto lbl122;
            if (PARM.RZSW-PARM.PAW > PARM.BIR) goto lbl30;
            goto lbl29;

    lbl122: if (PARM.BIR < 1.0){
                if (PARM.WS > PARM.BIR) goto lbl30;
            }   
            else{
                //Functions.SWTN();
                if (PARM.WTN < PARM.BIR) goto lbl30;
            }

	lbl29: if (PARM.IRR >= 3 && PARM.IRR != 5){
                if (PARM.IRR == 4 || PARM.NFA < PARM.IFA) goto lbl30;
                //Functions.NFERT(2,PARM.JT1);
                if (JRT > 0) goto lbl30;
            }
            //Functions.HIRG(AIR,PARM.EFM[IAUI],PARM.TLD[IAUI],JRT,IAUI,0);
            SAIR = SAIR+AIR;
     lbl30: if (PARM.LUN != 35) //Functions.NPCY();
            if (PARM.IDN == 0) //Functions.GASDF3();
            if (PARM.IRR == 1) PARM.RWO = PARM.RWO+SAIR;
            PARM.YEW = Math.Min(PARM.ER*(PARM.YSD[PARM.NDRV]+PARM.YW)/PARM.WT[PARM.LD1],.9);
            //Functions.NYON();
            PARM.SMM[42,PARM.MO] = PARM.SMM[42,PARM.MO]+PARM.YN;
            PARM.VAR[43] = PARM.YN;
            //Functions.NCQYL();
            if (PARM.NDP>0) //Functions.PSTCY();
            PARM.AGPM = 0.0;
            PARM.VAC = 0.0;
            PARM.CVP = 0.0;
            PARM.CV = PARM.RSD[PARM.LD1]+PARM.STDO;
            PARM.CVRS = PARM.RSD[PARM.LD1]+PARM.STDO;
            double IN0 = 0;
			double IN1 = IN0; // this does not exist in modparam so what is IN0??
            IN0 = IN0+1;
            if (IN0>PARM.IGO) IN0 = 1;
            PARM.WS = 1.0;
            int N1 = 1;
            PARM.VARS[8] = PARM.STDO;
            PARM.STV[8,PARM.MO1] = PARM.STDO;

            int L;
            for (int IN2 = 0; IN2 < PARM.IGO; IN2++){
                IN1 = IN1+1;
	            if (IN1>PARM.IGO) IN1 = 1;
                for (J = (int)IN1; J < 12; J++){
                    if (PARM.KG[J]>0) break;
                }

                if (J>12) goto lbl148;
                PARM.JJK = J;
                N1 = Math.Max(1,PARM.NCP[J]);
                if (PARM.JPL[PARM.JJK]>0){
                    PARM.HU[PARM.JJK] = PARM.HU[PARM.JJK]+Math.Max(0.0,PARM.DST0-PARM.TBSC[PARM.JJK]);
	                if (PARM.PDSW/PARM.FCSW<PARM.PRMT[10] || PARM.HU[PARM.JJK]<PARM.GMHU[PARM.JJK] && PARM.MO<PARM.MOFX)continue;
                    PARM.JPL[PARM.JJK] = 0;
                    if (PARM.NOP>0) continue; //WRITE(KW[1],89)PARM.IYR,PARM.MO,PARM.KDA,PARM.PDSW,PARM.HU[PARM.JJK],PARM.XHSM
                    PARM.HU[PARM.JJK] = 0.0;
                    PARM.IGMD[(int)N1,PARM.JJK] = PARM.IYR*10000+PARM.MO*100+PARM.KDA;
                }
                PARM.AGPM = PARM.AGPM+PARM.STD[PARM.JJK];
	  	        PARM.CVRS = PARM.CVRS+PARM.STD[PARM.JJK];
	            PARM.VAC = PARM.VAC+PARM.BWD[1,PARM.JJK]*PARM.STD[PARM.JJK];
                PARM.CV = PARM.CV+PARM.DM[PARM.JJK]-PARM.RW[PARM.JJK]+PARM.STD[PARM.JJK];
                // XX = PARM.PLL0[PARM.JJK]; // This is the first time PLL0 is used but is never defined...again
                PARM.CVP = Math.Max(PARM.CVP,XX/(XX+Math.Exp(PARM.SCRP[14,0]-PARM.SCRP[14,1]*XX)));
                PARM.VAC = PARM.VAC+PARM.BWD[0,PARM.JJK]*PARM.STL[PARM.JJK];
                PARM.AWC = PARM.AWC+PARM.RFV-PARM.QD;
                PARM.AQV = PARM.AQV+PARM.QD;

                
                for (L1 = 0; L1 < PARM.NBSL; L1++){
                    L = PARM.LID[L1];
                    PARM.U[L] = 0.0;
                    PARM.UN[L] = 0.0;
                    PARM.UK[L] = 0.0;
                    PARM.UP[L] = 0.0;
                }

                PARM.UNO3 = 0.0;
                PARM.UPP = 0.0;
                PARM.SU = 0.0;
                PARM.DDM[PARM.JJK] = 0.0;
                //Functions.CGROW(JRT);
                if (JRT == 0){
                    PARM.SUN = 0.0;
                    PARM.SUP = 0.0;
                    PARM.SUK = 0.0;
                    PARM.SAT = 0.0;
                    //Functions.HUSE();
                    //Functions.CROP();
                    //Functions.NUP();
                    //Functions.NPUP();
                    //Functions.NUK();
                    //Functions.NUSE();
                    //Functions.CSTRS();
                    PARM.VAR[49] = PARM.WFX;
                    PARM.SMM[12,PARM.MO] = PARM.SMM[12,PARM.MO]+PARM.SU;
                    AET = PARM.SU+AET;
                    if (PARM.HUI[PARM.JJK]>PARM.PRMT[3]){
                        PARM.SWH[PARM.JJK] = PARM.SWH[PARM.JJK]+PARM.SU;
                        PARM.SWP[PARM.JJK] = PARM.SWP[PARM.JJK]+PARM.EP[PARM.JJK];
                    }
                    PARM.VAR[12] = PARM.SU;
                    PARM.GSEP = PARM.GSEP+PARM.SU;
                    PARM.ACET[PARM.JJK] = PARM.ACET[PARM.JJK]+PARM.SU+PARM.ES;
                }
                //Functions.CAGRO();
                PARM.VARC[0,PARM.JJK] = PARM.HUI[PARM.JJK];
                PARM.VARC[1,PARM.JJK] = PARM.SLAI[PARM.JJK];
                PARM.VARC[2,PARM.JJK] = PARM.RD[PARM.JJK];
                PARM.VARC[3,PARM.JJK] = PARM.RW[PARM.JJK];
                PARM.VARC[4,PARM.JJK] = PARM.DM[PARM.JJK];
                PARM.VARC[5,PARM.JJK] = .42*PARM.DM[PARM.JJK];
                PARM.VARC[6,PARM.JJK] = PARM.STL[PARM.JJK];
                PARM.VARC[7,PARM.JJK] = PARM.CHT[PARM.JJK];
                PARM.VARC[8,PARM.JJK] = PARM.STD[PARM.JJK];
                PARM.VARC[9,PARM.JJK] = PARM.UN1[PARM.JJK];
                PARM.VARC[10,PARM.JJK] = PARM.UP1[PARM.JJK];
                PARM.VARC[11,PARM.JJK] = PARM.UK1[PARM.JJK];
                if (PARM.IDC[PARM.JJK] != PARM.NDC[6] && PARM.IDC[PARM.JJK] != PARM.NDC[7] && PARM.IDC[PARM.JJK] != PARM.NDC[9]) PARM.AGPM = PARM.AGPM+PARM.STL[PARM.JJK];
            if (PARM.NSTP == PARM.IDA && PARM.IY == PARM.IGSD) {}//Functions.REALC();
            }
	            
			
            if (PARM.KFL[12]>0){
                II = PARM.IY;
                if (PARM.NSTP>0){
                    II = IRLX;
                    if (PARM.IY != PARM.IGSD) goto lbl148;
                }
                //WRITE(KW[12],513)PARM.IYR,PARM.MO,PARM.KDA,II,PARM.CPNM[PARM.JJK],(PARM.CGSF[J,PARM.JJK],J = 1,7)
            }

    lbl148: //Functions.SCONT(0);
            SWGS = SWGS+PARM.SW;
            X1 = Math.Max(AET,PARM.EO*Math.Exp(-PARM.PRMT[41]*PARM.SCI/PARM.SMX));
            //!     X1 = PARM.EO*Math.Exp(-PARM.PRMT[42]*PARM.SCI/SMX)
            //! 	  PARM.SCI = SCI+X1-PARM.RFV+PARM.QD+PARM.PKRZ[LNS]+PARM.SST
            PARM.SCI = PARM.SCI+X1-PARM.RFV+PARM.QD;
            PARM.SCI = Math.Min(PARM.SCI,PARM.PRMT[72]*PARM.SMX);
            X1 = (PARM.ADRF-PARM.PRMT[8])/100.0;
            X2 = PARM.CV-PARM.PRMT[9];
            if (PARM.IGO>0 && PARM.NFA >= PARM.IFA){
                if (PARM.BFT >= 1.0 && PARM.TNOR<PARM.BFT) {}//Functions.NFERT(4,PARM.JT1);
            }
            PARM.IPST = PARM.IPST+1;
            if (PARM.TMN>0.0){
                if (X1>0.0 && X2>0.0) PARM.IPST = (int)PARM.IPST+(int)PARM.RHD*(int)PARM.TX;
            }
            else{
                PARM.IPST = (int)PARM.IPST+(int)PARM.TMN;
            }
            if (PARM.NSTP != PARM.IDA || PARM.IY != PARM.IGSD) goto lbl200;
            if (PARM.ICCD>0) goto lbl559;
            PARM.IGSD = PARM.IGSD+1;
            PARM.IYR = PARM.IYR-1;
            PARM.ICCD = 1;
            goto lbl200;
    lbl559: //Functions.REALS();
            if (PARM.ISTP == 1) goto lbl88;

    lbl200: //Functions.NEVN();
            //Functions.NEVP();
            //Functions.SLTEV();
            PARM.VAR[46] = PARM.SNMN;
            PARM.VAR[47] = PARM.SGMN;
            PARM.VAR[48] = PARM.SDN;
            PARM.VAR[88] = PARM.SN2;
            PARM.VAR[93] = 10.0*PARM.DFO2;
            PARM.VAR[94] = 10.0*PARM.DFCO2;
            PARM.VAR[55] = PARM.SMP;
            PARM.VAR[51] = PARM.SVOL;
            PARM.VAR[50] = PARM.SNIT;
            PARM.VAR[10] = AET;
            PARM.VAR[13] = PARM.QD;
            PARM.VAR[33] = PARM.YSD[1];
            PARM.VAR[43] = PARM.QNO3;
            PARM.VAR[45] = PARM.SSO3[(int)LNS];
            PARM.VAR[54] = PARM.QAP;
            PARM.VAR[44] = PARM.TSFN;
            PARM.VAR[70] = PARM.TSFS;
            PARM.VAR[79] = PARM.TSFK;
            PARM.VARS[0] = PARM.TNH3;
            PARM.VARS[1] = PARM.TNO3;
            PARM.VARS[2] = PARM.TAP;
            PARM.VARS[3] = PARM.TSK;
            PARM.VARS[4] = PARM.SNO;
            PARM.VARS[5] = PARM.RZSW;
            PARM.VARS[6] = PARM.WTBL;
            PARM.VARS[7] = PARM.GWST;
            PARM.VARS[10] = PARM.OCPD;
            PARM.VARS[11] = PARM.TOC;
            PARM.VARS[12] = PARM.ZLS;
            PARM.VARS[13] = PARM.ZLM;
            PARM.VARS[14] = PARM.ZLSL;
            PARM.VARS[15] = PARM.ZLSC;
            PARM.VARS[16] = PARM.ZLMC;
            PARM.VARS[17] = PARM.ZLSLC;
            PARM.VARS[18] = PARM.ZLSLNC;
            PARM.VARS[19] = PARM.ZBMC;
            PARM.VARS[20] = PARM.ZHSC;
            PARM.VARS[21] = PARM.ZHPC;
            PARM.VARS[22] = PARM.ZLSN;
            PARM.VARS[23] = PARM.ZLMN;
            PARM.VARS[24] = PARM.ZBMN;
            PARM.VARS[25] = PARM.ZHSN;
            PARM.VARS[26] = PARM.ZHPN;
            PARM.VARS[27] = PARM.TWN;
            PARM.VARS[28] = PARM.TSLT;
            PARM.VARS[29] = PARM.TNO2;
            //Functions.SPRNT(YTP);
            for (I = 0; I < PARM.NBSL; I++){
                J = PARM.LID[I];
                PARM.SMS[1,J] = PARM.SMS[1,J]+PARM.STMP[J];
            }
            if (PARM.KFL[14]>0) //Functions.SOCIOD(PARM.KDA);
            if (PARM.KFL[4]>0 && PARM.NDP>0){
                II = PARM.IY;
                if (PARM.NSTP != 0){
                    II = IRLX;
                    if (PARM.IY != PARM.IGSD) goto lbl561;
                }
                for (L = 0; L < PARM.NDP; L++){
                    X1 = 100.0*(PARM.VARP[1,L]+PARM.VARP[3,L])/(PARM.QD+PARM.SST+Math.Pow(10, -5));
                    //WRITE(KW[5],468)PARM.IYR,PARM.MO,PARM.KDA,II,PSTN[L],(VARP[K,L],K = 1,10),PARM.QD,PARM.SST,PARM.PKRZ[LNS],X1
                }
        lbl561: //PARM.VARP = 0.0;
				foreach (int x in PARM.VARP){
					foreach (int y in PARM.VARP){
						PARM.VARP[x, y] = 0.0;	
					}
				}
            }

            if (PARM.KFL[10]>0) {} //continue; //WRITE(KW[10],107)PARM.IYR,PARM.MO,PARM.KDA,DD,DST0,(PARM.STMP[PARM.LID[K]],K = 1,PARM.NBSL)
            X1 = PARM.PDSW/PARM.FCSW;
	        X2 = 1000.0*PARM.YLD[PARM.JJK];
            if (PARM.KFL[17]>0) {}//continue; //WRITE(KW[17],107)PARM.IYR,PARM.MO,PARM.KDA,X1,(PARM.VAR[KD[K]],K = 1,NKD),PARM.TNO3,WNO3[PARM.LD1],PARM.PKRZ[PARM.LD1],SSO3[PARM.LD1],PARM.HUI[PARM.JJK],PARM.SLAI[PARM.JJK],DM[PARM.JJK],X2,PARM.UN1[PARM.JJK],(PARM.VARS[K],K = 23,28)
            if (PARM.KFL[27]>0) {}//continue; //WRITE(KW[27],682)PARM.IYR,PARM.MO,PARM.KDA,PARM.VAR[4],PARM.VAR[10],PARM.VAR[11],PARM.VAR[13],PARM.VAR[14],(PARM.VAR[K],K = 16,20],(PARM.VARS[K],K = 6,8)
	        //Functions.SWN1530();
            if (PARM.KFL[28]>0) {}//continue; //WRITE(KW[28],682)PARM.IYR,PARM.MO,PARM.KDA,SW15,SW30,SNN15,SNN30,SNA15,SNA30,PARM.VAR[4],PARM.VAR[10],PARM.VAR[11],(PARM.VAR[K],K = 13,20),(PARM.VARS[K],K = 6,8),(Z[PARM.LID[K]],K = 1,PARM.NBSL),(ST[PARM.LID[K]],K = 1,PARM.NBSL),(PARM.U[PARM.LID[K]],K = 1,PARM.NBSL),(SEV[PARM.LID[K]],K = 1,PARM.NBSL),(PARM.PKRZ[PARM.LID[K]],K = 1,PARM.NBSL),(PARM.SSF[PARM.LID[K]],K = 1,PARM.NBSL),(WNO3[PARM.LID[K]],K = 1,PARM.NBSL),(PARM.UN[PARM.LID[K]],K = 1,PARM.NBSL),(SSO3[PARM.LID[K]],K = 1,PARM.NBSL),(PARM.STMP[PARM.LID[K]],K = 1,PARM.NBSL)
            double TNGS = PARM.TNO2+PARM.TNO3+PARM.TWN+PARM.TNH3;
	        PARM.SMGS[1] = PARM.SMGS[1]+TNGS;
            if (PARM.KFL[21]>0){
                //WRITE(KW[21],107)PARM.IYR,PARM.MO,PARM.KDA
	            //Functions.SPRNT(YTP);
                //Functions.SOLIO(YTP,21);
            }
            if (PARM.IPD<6) goto lbl44;
            if (PARM.IPD == 9){
                if (PARM.IGO == 0) goto lbl44;
            }
            if (PARM.IDA != IPC) goto lbl44;
            if (PARM.IPD == 8){
                if (PARM.RFV<1.0) goto lbl44;
            }
			int K1;
            if (PARM.IPD != 7){
            //!     PRINTOUT DAILY
                //WRITE(KW[1],532)PARM.IYR,PARM.MO,PARM.KDA,(HED[KD[K]],PARM.VAR[KD[K]],K = 1,NKD)
                for (I = 0; I < NN; I++){
                    K1 = PARM.LY[PARM.IRO,I];
                    if (PARM.KG[K1] == 0) continue;
                    //WRITE(KW[1],105)PARM.IYR,PARM.MO,PARM.KDA,PARM.CPNM[K1],(HEDC[K],PARM.VARC[K,K1],K = 1,19)
                }
                /*WRITE(KW[1],532)PARM.IYR,PARM.MO,PARM.KDA,(HEDS[K],PARM.VARS[K],K = 1,13)
                !     WRITE(KW[1],119)(PARM.LID[K],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(Z[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(ST[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(S15[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(FC[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PO[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(BD[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.WT[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.SAN[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.SIL[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.ROK[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],219)(HK[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(WNO3[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(WNH3[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(AP[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PMN[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(OP[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(RWT[PARM.LID[K],PARM.JJK],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(SSO3[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(WON[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.U[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(SEV[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.PKRZ[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.SSF[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.UN[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.UP[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(WP[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(FOP[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(WSLT[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],100)(PARM.RSD[PARM.LID[K]],K = 1,PARM.NBSL)
                !     WRITE(KW[1],107)PARM.IYR,PARM.MO,PARM.KDA,DD,DST0,(PARM.STMP[PARM.LID[K]],K = 1,PARM.NBSL)*/
            }
            else{
                //Functions.SPRNT(YTP);
                //WRITE(KW[1],107)PARM.IYR,PARM.MO,PARM.KDA
                //WRITE(KW[1],101)
                //Functions.SOLIO(YTP,1);
            }
            IPC = IPC+PARM.INP;
     lbl44: PARM.SMM[10,PARM.MO] = PARM.SMM[10,PARM.MO]+AET;
            PARM.SMM[46,PARM.MO] = PARM.SMM[46,PARM.MO]+PARM.SNMN;
            PARM.SMM[47,PARM.MO] = PARM.SMM[47,PARM.MO]+PARM.SGMN;
            PARM.SMM[48,PARM.MO] = PARM.SMM[48,PARM.MO]+PARM.SDN;
            PARM.SMM[88,PARM.MO] = PARM.SMM[88,PARM.MO]+PARM.SN2;
            PARM.SMM[55,PARM.MO] = PARM.SMM[55,PARM.MO]+PARM.SMP;
            PARM.SMM[51,PARM.MO] = PARM.SMM[51,PARM.MO]+PARM.SVOL;
            PARM.SMM[50,PARM.MO] = PARM.SMM[50,PARM.MO]+PARM.SNIT;
            PARM.SMM[13,PARM.MO] = PARM.SMM[13,PARM.MO]+PARM.QD;
            PARM.SMM[33,PARM.MO] = PARM.SMM[33,PARM.MO]+PARM.YSD[1];
            PARM.SMM[43,PARM.MO] = PARM.SMM[43,PARM.MO]+PARM.QNO3;
            PARM.SMM[45,PARM.MO] = PARM.SMM[45,PARM.MO]+PARM.SSO3[(int)LNS];
            PARM.SMM[54,PARM.MO] = PARM.SMM[54,PARM.MO]+PARM.QAP;
            PARM.SMM[44,PARM.MO] = PARM.SMM[44,PARM.MO]+PARM.TSFN;
            PARM.SMM[79,PARM.MO] = PARM.SMM[79,PARM.MO]+PARM.TSFK;
            PARM.SMM[70,PARM.MO] = PARM.SMM[70,PARM.MO]+PARM.TSFS;
            PARM.SMM[92,PARM.MO] = PARM.SMM[92,PARM.MO]+PARM.SN2O;
            PARM.SMM[93,PARM.MO] = PARM.SMM[93,PARM.MO]+10.0*PARM.DFO2;
            PARM.SMM[94,PARM.MO] = PARM.SMM[94,PARM.MO]+10.0*PARM.DFCO2;
            //!     //Functions.NCONT
            if (PARM.IDA == PARM.NSTP+NT1 && PARM.NGN == 0){
                PARM.IGN = PARM.IGN+100;
                int KK;
                for (KK = 0; KK < PARM.IGN; KK++){
                    for (J = 0; J < 20; J++){
                        XX = Functions.AUNIF(20);
                        PARM.IX[J] = PARM.IX[20];
                    }
                }
            }
            JRT = 0;
            if (PARM.YERO>Math.Pow(10, -5) && PARM.ISTA == 0 && PARM.Z[PARM.NBSL]>PARM.ZF && PARM.NBSL >= 3) {}//Functions.ESLOS(JRT);
            if (JRT>0) goto lbl88;
            PARM.JDA = PARM.IDA+1;
            //Functions.AXMON(PARM.JDA,PARM.MO);
            if (PARM.MO == PARM.MO1) goto lbl84;
            PARM.PMOEO = PARM.SMM[9,PARM.MO1];
            PARM.PMORF = PARM.SMM[3,PARM.MO1]-PARM.SMM[13,PARM.MO1];
            XX = PARM.IDA-JJ;
            PARM.STV[0,PARM.MO1] = PARM.TNH3;
            PARM.STV[1,PARM.MO1] = PARM.TNO3;
            PARM.STV[3,PARM.MO1] = PARM.TSK;
            PARM.STV[4,PARM.MO1] = PARM.SNO;
            PARM.STV[5,PARM.MO1] = PARM.RZSW;
            PARM.STV[6,PARM.MO1] = PARM.WTBL;
            PARM.STV[7,PARM.MO1] = PARM.GWST;
            PARM.STV[10,PARM.MO1] = PARM.OCPD;
            PARM.STV[11,PARM.MO1] = .001*PARM.TOC;
            PARM.STV[12,PARM.MO1] = .001*PARM.ZLS;
            PARM.STV[13,PARM.MO1] = .001*PARM.ZLM;
            PARM.STV[14,PARM.MO1] = .001*PARM.ZLSL;
            PARM.STV[15,PARM.MO1] = .001*PARM.ZLSC;
            PARM.STV[16,PARM.MO1] = .001*PARM.ZLMC;
            PARM.STV[17,PARM.MO1] = .001*PARM.ZLSLC;
            PARM.STV[18,PARM.MO1] = .001*PARM.ZLSLNC;
            PARM.STV[19,PARM.MO1] = .001*PARM.ZBMC;
            PARM.STV[20,PARM.MO1] = .001*PARM.ZHSC;
            PARM.STV[21,PARM.MO1] = .001*PARM.ZHPC;
            PARM.STV[22,PARM.MO1] = .001*PARM.ZLSN;
            PARM.STV[23,PARM.MO1] = .001*PARM.ZLMN;
            PARM.STV[24,PARM.MO1] = .001*PARM.ZBMN;
            PARM.STV[25,PARM.MO1] = .001*PARM.ZHSN;
            PARM.STV[26,PARM.MO1] = .001*PARM.ZHPN;
            PARM.STV[27,PARM.MO1] = .001*PARM.TWN;
            PARM.STV[28,PARM.MO1] = PARM.TSLT;
            PARM.STV[29,PARM.MO1] = PARM.TNO2;
            if (PARM.KFL[6] == 0)goto lbl472;
            I1 = PARM.IY;
            if (PARM.NSTP != 0){
                I1 = IRLX;
                if (PARM.IY != PARM.IGSD) goto lbl472;
            }
            //WRITE(KW[6],475)PARM.IYR,PARM.MO1,I1,(PARM.SMM[IFS[J],PARM.MO1],J = 1,NFS),(PARM.STV[K,PARM.MO1],K = 4,6)
    lbl472: //DO 118 K = 1,NN
            K1 = PARM.LY[PARM.IRO,K];
            if (PARM.KG[K1] == 0) goto lbl50;
            PARM.SMMC[0,K1,PARM.MO1] = PARM.HUI[K1];
            PARM.SMMC[1,K1,PARM.MO1] = PARM.SLAI[K1];
            PARM.SMMC[2,K1,PARM.MO1] = PARM.RD[K1];
            PARM.SMMC[3,K1,PARM.MO1] = PARM.RW[K1];
            PARM.SMMC[4,K1,PARM.MO1] = PARM.DM[K1];
            PARM.SMMC[5,K1,PARM.MO1] = .42*PARM.DM[K1];
            PARM.SMMC[6,K1,PARM.MO1] = PARM.STL[K1];
            PARM.SMMC[7,K1,PARM.MO1] = PARM.CHT[K1];
            PARM.SMMC[9,K1,PARM.MO1] = PARM.UN1[K1];
            PARM.SMMC[10,K1,PARM.MO1] = PARM.UP1[K1];
            PARM.SMMC[11,K1,PARM.MO1] = PARM.UK1[K1];
            PARM.TSTL[PARM.MO1] = PARM.TSTL[PARM.MO1]+PARM.STL[K1];
            if (PARM.KFL[10] == 0) goto lbl50;
            II = PARM.IY;
            if (PARM.NSTP != 0){
                II = IRLX;
                if (PARM.IY != PARM.IGSD) goto lbl50;
            }
            //WRITE(KW[11],523)PARM.IYR,PARM.MO1,II,PARM.CPNM[K1],(PARM.SFMO[J,K1],J = 1,7),PARM.RZSW,PARM.SMM[4,PARM.MO1],PARM.SMM[11,PARM.MO1],PARM.SMM[14,PARM.MO1],PARM.SMM[17,PARM.MO1],PARM.SMM[16,PARM.MO1]
    lbl50: if (PARM.KFL[22]>0){
                PARM.RNO3 = PARM.SMM[3,PARM.MO1]*PARM.RFNC;
                SUM = PARM.TNO2+PARM.TNO3+PARM.TNH3+PARM.TWN+PARM.STDN[K1]+PARM.STDON+PARM.UN1[K1];
	            //WRITE(KW[22],565)PARM.IYR,PARM.MO1,PARM.SMM[4,PARM.MO1],PARM.SMM[10,PARM.MO1],PARM.SMM[11,PARM.MO1],PARM.SMM[13,PARM.MO1],PARM.SMM[14,PARM.MO1],[PARM.SMM[J,PARM.MO1],J = 16,20],(PARM.STV[J,PARM.MO1],J = 6,8],RNO3,(PARM.SMM[J,PARM.MO1],J = 43,46],PARM.SMM[49,PARM.MO1],PARM.SMM[89,PARM.MO1],PARM.SMM[52,PARM.MO1],PARM.SMM[85,PARM.MO1],PARM.SMM[50,PARM.MO1],[PARM.SMM[J,PARM.MO1],J = 59,61],PARM.UN1[K1],PARM.YLNF[1,K1],PARM.CPNM[K1],PARM.YLD1[1,K1],SUM
            }
            PARM.SMMC[8,K1,PARM.MO1] = PARM.STD[K1];
            double ISM = 0;
            for (J = 0; J < 7; J++){
                KTP[J] = Math.Min(31.0,PARM.SFMO[J,K1]+.5);
                ISM = ISM+KTP[J];
                PARM.SFMO[J,K1] = 0.0;
            }
            if (ISM == 0) goto lbl118;
            //Functions.ASORTI(KTP,MNST,7);
            KDT[PARM.MO1,K1] = KTP[MNST[4]]+100*MNST[4]+1000*KTP[MNST[5]]+100000*MNST[5]+1000000*KTP[MNST[6]]+100000000*MNST[6];
    lbl118: //CONTINUE
            if (PARM.KFL[24]>0){
                if (PARM.IY == 1 && PARM.IDA == 1) {} //continue; //WRITE(KW[25],680)PARM.WSA
                YTP[0] = PARM.SMM[13,PARM.MO1]+PARM.SMM[15,PARM.MO1]+PARM.SMM[17,PARM.MO1];
                YTP[1] = PARM.SMM[PARM.NDVSS,PARM.MO1];
                YTP[2] = PARM.SMM[42,PARM.MO1];
                YTP[3] = PARM.SMM[53,PARM.MO1];
                YTP[4] = PARM.SMM[43,PARM.MO1]+PARM.SMM[44,PARM.MO1]+PARM.SMM[52,PARM.MO1];
                YTP[5] = PARM.SMM[54,PARM.MO1];
                //WRITE(KW[25],679)PARM.IY,PARM.MO1,YTP
            }
            PARM.STV[9,PARM.MO1] = PARM.RSD[PARM.LD1];
            PARM.VARS[9] = PARM.RSD[PARM.LD1];
            PARM.STV[2,PARM.MO1] = 1000.0*PARM.AP[PARM.LD1]/PARM.WT[PARM.LD1];
            PARM.TXMX[PARM.MO1] = PARM.TXMX[PARM.MO1]+PARM.SMM[0,PARM.MO1];
            PARM.TXMN[PARM.MO1] = PARM.TXMN[PARM.MO1]+PARM.SMM[1,PARM.MO1];
            PARM.TSR[PARM.MO1] = PARM.TSR[PARM.MO1]+PARM.SMM[2,PARM.MO1];
            PARM.SMM[1,PARM.MO1] = PARM.SMM[1,PARM.MO1]/XX;
            PARM.SMM[0,PARM.MO1] = PARM.SMM[0,PARM.MO1]/XX;
            PARM.SMM[2,PARM.MO1] = PARM.SMM[2,PARM.MO1]/XX;
            PARM.SMM[66,PARM.MO1] = PARM.SMM[66,PARM.MO1]/XX;
            PARM.SMM[67,PARM.MO1] = PARM.SMM[67,PARM.MO1]/XX;
            PARM.SMM[6,PARM.MO1] = PARM.SMM[6,PARM.MO1]/XX;
            PARM.TET[PARM.MO1] = PARM.TET[PARM.MO1]+PARM.SMM[6,PARM.MO1];
            PARM.SMM[7,PARM.MO1] = PARM.SMM[7,PARM.MO1]/XX;
            PARM.SMM[8,PARM.MO1] = PARM.SMM[8,PARM.MO1]/XX;
            PARM.SMM[38,PARM.MO1] = PARM.SMM[38,PARM.MO1]/XX;
            PARM.SMM[39,PARM.MO1] = PARM.SMM[39,PARM.MO1]/XX;
            X1 = PARM.NWDA-PARM.NWD0+Math.Pow(10, -5);
            PARM.TAMX[PARM.MO1] = PARM.TAMX[PARM.MO1]+X1;
            PARM.SMM[37,PARM.MO1] = PARM.SMM[37,PARM.MO1]/X1;
            PARM.SMM[40,PARM.MO1] = PARM.SMM[40,PARM.MO1]/X1;
            PARM.NWD0 = PARM.NWDA;
            PARM.SSW = PARM.SSW/XX;
            PARM.ASW[PARM.MO1] = PARM.ASW[PARM.MO1]+PARM.SSW;
            PARM.SSW = 0.0;
            PARM.TR[PARM.MO1] = PARM.TR[PARM.MO1]+PARM.SMM[3,PARM.MO1];
            PARM.TSN[PARM.MO1] = PARM.TSN[PARM.MO1]+PARM.SMM[16,PARM.MO1];
            PARM.TSY[PARM.MO1] = PARM.TSY[PARM.MO1]+PARM.SMM[PARM.NDVSS,PARM.MO1];
            PARM.RSY[PARM.MO1] = PARM.RSY[PARM.MO1]+PARM.SMM[35,PARM.MO1];
            PARM.TYW[PARM.MO1] = PARM.TYW[PARM.MO1]+PARM.SMM[41,PARM.MO1];
            PARM.TQ[PARM.MO1] = PARM.TQ[PARM.MO1]+PARM.SMM[13,PARM.MO1];
            PARM.SET[PARM.MO1] = PARM.SET[PARM.MO1]+PARM.SMM[9,PARM.MO1];
            PARM.TRHT[PARM.MO1] = PARM.TRHT[PARM.MO1]+PARM.SMM[38,PARM.MO1];
            JJ = PARM.IDA;
            for (K = 0; K < PARM.NSM; K++){
                PARM.SMY[K] = PARM.SMY[K]+PARM.SMM[K,PARM.MO1];
            }
            if (PARM.NDP>0){
                for (K = 0; K < PARM.NDP; K++){
                    PARM.SMMP[7,K,PARM.MO1] = PARM.PFOL[K];
                    for (K1 = 0; K1 < 7; K1++){
                        PARM.SMYP[K1,K] = PARM.SMYP[K1,K]+PARM.SMMP[K1,K,PARM.MO1];
                    }
                    PARM.SMYP[9,K] = PARM.SMYP[9,K]+PARM.SMMP[9,K,PARM.MO1];
                }
            }
            PARM.W[PARM.MO1] = PARM.W[PARM.MO1]+PARM.SMM[28,PARM.MO1];
            PARM.RCM[PARM.MO1] = PARM.RCM[PARM.MO1]+PARM.SMM[36,PARM.MO1];
            PARM.TEI[PARM.MO1] = PARM.TEI[PARM.MO1]+PARM.SMM[27,PARM.MO1];
            X1 = PARM.SMM[27,PARM.MO1]+Math.Pow(10, -10);
            PARM.SMM[28,PARM.MO1] = PARM.SMM[28,PARM.MO1]/X1;
            PARM.SMM[36,PARM.MO1] = PARM.SMM[36,PARM.MO1]/X1;
            PARM.SMM[82,PARM.MO1] = PARM.SMM[82,PARM.MO1]/X1;
            PARM.SMM[89,PARM.MO1] = PARM.SMM[89,PARM.MO1]/X1;
            PARM.SMM[90,PARM.MO1] = PARM.SMM[90,PARM.MO1]/X1;
            X1 = PARM.JCN-PARM.JCN0;
            if (X1>0.0) PARM.SMM[15,PARM.MO1] = PARM.SMM[14,PARM.MO1]/X1;
            X1 = PARM.NQP-PARM.NQP0;
            if (X1>0.0) PARM.SMM[57,PARM.MO1] = PARM.SMM[57,PARM.MO1]/X1;
            PARM.NQP0 = PARM.NQP;
            PARM.JCN0 = PARM.JCN;
            if (PARM.MASP>0){
                PPX[PARM.MO1] = PARM.SMM[43,PARM.MO1];
                //Functions.ACOUT(PPX[PARM.MO1],PARM.SMM[13,PARM.MO1],1000.0);
                PARM.SMM[43,PARM.MO1] = PPX[PARM.MO1];
                PPX[PARM.MO1] = PARM.SMM[44,PARM.MO1];
                //Functions.ACOUT(PPX[PARM.MO1],PARM.SMM[15,PARM.MO1],1000.0);
                PARM.SMM[44,PARM.MO1] = PPX[PARM.MO1];
                PPX[PARM.MO1] = PARM.SMM[45,PARM.MO1];
                //Functions.ACOUT(PPX[PARM.MO1],PARM.SMM[16,PARM.MO1],1000.0);
                PARM.SMM[45,PARM.MO1] = PPX[PARM.MO1];
                PPX[PARM.MO1] = PARM.SMM[54,PARM.MO1];
                //Functions.ACOUT(PPX[PARM.MO1],PARM.SMM[13,PARM.MO1],1000.0);
                PARM.SMM[54,PARM.MO1] = PPX[PARM.MO1];
            }
//!     WRITE PARM.MO VALUES AND SUM YEARLY VALUES
            if (PARM.MO>PARM.MO1) goto lbl83;
            if (PARM.LMS == 0){
                //Functions.NLIME();
                if (PARM.TLA>0.0){
                    X3 = PARM.TLA*PARM.COL;
                    PARM.COST = PARM.COST+X3;
                    X1 = PARM.COTL[PARM.IAUL];
                    X2 = X1-PARM.COOP[PARM.IAUL];
                    PARM.COST = PARM.COST+X1;
                    PARM.CSFX = PARM.CSFX+X2;
	                PARM.SMM[91,PARM.MO1] = PARM.SMM[91,PARM.MO1]+PARM.FULU[PARM.IAUL];
	                PARM.SMY[91] = PARM.SMY[91]+PARM.FULU[PARM.IAUL];
                    if (PARM.KFL[19]>0){
                        //WRITE(KW[20],567)PARM.IYR,PARM.MO1,PARM.KDA,KDC[PARM.JJK],X3,X3,PARM.TLA
                        //WRITE(KW[20],666)PARM.IYR,PARM.MO1,PARM.KDA,TIL[IAUL],KDC[PARM.JJK],PARM.IHC[IAUL],PARM.NBE[IAUL],NBT[IAUL],X1,X2,FULU[IAUL]
                    }
                }
            }
            PARM.SMM[65,PARM.MO1] = PARM.SMM[65,PARM.MO1]+PARM.TLA;
            PARM.SMY[65] = PARM.TLA;
            PARM.VAR[65] = PARM.TLA;
            PARM.SMY[0] = PARM.SMY[0]/12.0;
            PARM.SMY[1] = PARM.SMY[1]/12.0;
            PARM.SMY[2] = PARM.SMY[2]/12.0;
            PARM.SMY[6] = PARM.SMY[6]/12.0;
            PARM.SMY[7] = PARM.SMY[7]/12.0;
            PARM.SMY[8] = PARM.SMY[8]/12.0;
            PARM.SMY[66] = PARM.SMY[66]/12.0;
            PARM.SMY[67] = PARM.SMY[67]/12.0;
            PARM.SMY[38] = PARM.SMY[38]/12.0;
            PARM.SMY[39] = PARM.SMY[39]/12.0;
            PARM.SMY[40] = PARM.SMY[40]/12.0;
            for (K = 0; K < PARM.NSM; K++){
                PARM.SM[K] = PARM.SM[K]+PARM.SMY[K];
            }
            if (PARM.NDP>0){
                for (K = 0; K < PARM.NDP; K++){
                    for (L = 0; L < 7; L++){
                        PARM.SMAP[L,K] = PARM.SMAP[L,K]+PARM.SMYP[L,K];
                    }
                    PARM.SMAP[9,K] = PARM.SMAP[9,K]+PARM.SMYP[9,K];
                }
            }
            double DMX = Math.Min(PARM.PRMT[23],PARM.Z[PARM.LID[PARM.NBSL]]);
            if (DMX>PARM.Z[PARM.LD1] && PARM.LUN != 35) //Functions.TMIX(PARM.PRMT[24],DMX,1);
	        X1 = PARM.SMY[27]+Math.Pow(10, -10);
            PARM.SMY[28] = PARM.SMY[28]/X1;
            PARM.SMY[36] = PARM.SMY[36]/X1;
	        PARM.SMY[89] = PARM.SMY[89]/X1;
	        PARM.SMY[90] = PARM.SMY[90]/X1;
            if (PARM.MASP>0){
                PPX[0] = PARM.SMY[43];
                PPX[1] = PARM.SMY[44];
                PPX[2] = PARM.SMY[45];
                PPX[3] = PARM.SMY[48];
                //Functions.ACOUT(PPX[0],PARM.SMY[13],1000.0);
                //Functions.ACOUT(PPX[1],PARM.SMY[14],1000.0);
                //Functions.ACOUT(PPX[2],PARM.SMY[16],1000.0);
                //Functions.ACOUT(PPX[3],PARM.SMY[14],1000.0);
                PARM.SMY[43] = PPX[0];
                PARM.SMY[44] = PPX[1];
                PARM.SMY[45] = PPX[2];
                PARM.SMY[48] = PPX[3];
            }
            X1 = PARM.JCN-PARM.JCN1;
            PARM.SMY[14] = PARM.SMY[14]/(X1+Math.Pow(10, -20));
            PARM.JCN1 = PARM.JCN;
            X1 = PARM.NQP-PARM.NQP1;
            PARM.SMY[57] = PARM.SMY[57]/(X1+Math.Pow(10, -20));
            PARM.NQP1 = PARM.NQP;
            if (PARM.KFL[6]>0){
                for (K = 0; K < PARM.NDP; K++){
                    //WRITE(KW[7],462)PSTN[K]
                    for (L = 1; L < 5; L++){
                        //WRITE(KW[7],143)HEDP[L],(PARM.SMMP[L,K,J],J = 1,12),PARM.SMYP[L,K],HEDP[L]
                    }
                }
            }
			int JJX = 0;
			double XPR;
            if (PARM.KFL[25]>0 && PARM.NDP>0){
                for (J = 0; J < NN; J++){
                    JJX = PARM.LY[PARM.IRO,J];
                    if (PARM.TPAC[JJX,0]>0.0) break;
                }
                if (J>NN){
                    XPR = 0.0;
                    ANMX = ' ';
                }
                else{
                    XPR = PARM.TPAC[JJX,0];
                    // ANMX = PARM.CPNM[JJX]; // I'm thinking CPNM should be single 
                }
                //WRITE(KW[26],681)PARM.IYR,PARM.IY,PARM.SMY[14],PARM.SMY[16],PARM.SMY[17],PARM.SMY[18],PARM.SMY[NDVSS],PARM.SMY[77],PSTN[1],ANMX,PARM.XPR,[PARM.SMYP[J,1],J = 2,7),PARM.SMYP[10,1],APQC[2,1,PARM.IY]
                for (K = 1; K < PARM.NDP; K++){
                    for (J = 0; J < NN; J++){
                        JJX = PARM.LY[PARM.IRO,J];
                        if (PARM.TPAC[JJX,K]>0.0) break;
                    }
                    if (J>NN){
                        XPR = 0.0;
                        ANMX = ' ';
                    }
                    else{
                        XPR = PARM.TPAC[JJX,K];
                        //ANMX = PARM.CPNM[JJX];
                    }
                    //WRITE(KW[26],683)PSTN[K],ANMX,PARM.XPR,(PARM.SMYP[J,K],J = 2,7),PARM.SMYP[10,K],APQC[2,K,PARM.IY]
                }
            }
            II = 0;
            if (PARM.IY != PARM.IPY) goto lbl71;
            II = PARM.IPYI;
            if (PARM.IPD <= 2) goto lbl69;
	        if (PARM.NDP == 0 || PARM.MASP<0) goto lbl63;
            for (K = 0; K < PARM.NDP; K++){
                if (K == 6 || K == 1){
                    //Functions.APAGE(1);
                    //WRITE(KW[1],112)
                    //WRITE(KW[1],99)PARM.IYR,PARM.IY
                }
                // WRITE(KW[1],111)PSTN[K]
                if (PARM.MASP>0){
                    //WRITE(KW[1],113)HEDP[1],(PARM.SMMP[1,K,J],J = 1,12),PARM.SMYP[1,K],HEDP[1]
                    for (L = 0; L < 12; L++){
                        PPX[L] = PARM.SMMP[1,K,L];
                        //Functions.ACOUT(PPX[L],PARM.SMM[13,L],1.0);
                    }
                    PPX[12] = PARM.SMYP[1,K];
                    //Functions.ACOUT(PPX[12],PARM.SMY[13],1.0);
                    //WRITE(KW[1],109)HEDP[2],(PPX[J],J = 1,13),HEDP[2]
                    for (L = 0; L < 12; L++){
                        PPX[L] = PARM.SMMP[2,K,L];
                        //Functions.ACOUT(PPX[L],PARM.SMM[16,L],1.0);
                    }
                    PPX[12] = PARM.SMYP[2,K];
                    //Functions.ACOUT(PPX[12],PARM.SMY[16],1.0);
                    //WRITE(KW[1],109)HEDP[3],(PPX[J],J = 1,13),HEDP[3]
                    for (L = 0; L < 12; L++){
                        PPX[L] = PARM.SMMP[3,K,L];
                        //Functions.ACOUT(PPX[L],PARM.SMM[15,L],1.0);
                    }
                    PPX[12] = PARM.SMYP[3,K];
                    //Functions.ACOUT(PPX[12],PARM.SMY[15],1.0);
                    //WRITE(KW[1],109)HEDP[4],(PPX[J],J = 1,13),HEDP[4]
                    for (L = 4; L < 7; L++){
                        //WRITE(KW[1],113)HEDP[L],(PARM.SMMP[L,K,J],J = 1,12),PARM.SMYP[L,K],HEDP[L]
                    }
                    for (L = 7; L < 9; L++){
                        //WRITE(KW[1],114)HEDP[L],(PARM.SMMP[L,K,J],J = 1,12),HEDP[L]
                    }
                    for (L = 0; L < 12; L++){
                        PPX[L] = PARM.SMMP[9,K,L];
                        //Functions.ACOUT(PPX[L],PARM.SMM[17,L],1.0);
                    }
                    PPX[12] = PARM.SMYP[9,K];
                    //Functions.ACOUT(PPX[12],PARM.SMY[17],1.0);
                    //WRITE(KW[1],109)HEDP[10],(PPX[J],J = 1,13),HEDP[10]
                    continue;
                }
                for (L = 0; L < 7; L++){
            //!         PRINTOUT PESTICIDE MONTHLY
                    //WRITE(KW[1],143)HEDP[L],(PARM.SMMP[L,K,J],J = 1,12),PARM.SMYP[L,K],HEDP[L]
                }
                for (L = 7; L < 9; L++){
                    //WRITE(KW[1],146)HEDP[L],(PARM.SMMP[L,K,J],J = 1,12),HEDP[L]
                }
                //WRITE(KW[1],143)HEDP[10],(PARM.SMMP[10,K,J],J = 1,12),PARM.SMYP[10,K],HEDP[10]
            }

            /*DO K = 1,PARM.NDP
                WRITE(KW[1],462)PSTN[K]
                WRITE(KW[1],463)(APQ[I,K,PARM.IY],I = 1,5)
                WRITE(KW[1],464)(AQB[I,K,PARM.IY],I = 1,5)
                WRITE(KW[1],465)(APY[I,K,PARM.IY],I = 1,5)
                WRITE(KW[1],466)(AYB[I,K,PARM.IY],I = 1,5)
            END DO*/

     lbl63: //Functions.APAGE(1);
            //WRITE(KW[1],102)PARM.IYR,PARM.IY
            if (PARM.NKA>0){
                for (J = 0; J < PARM.NKA; J++){
                    K = PARM.KA[J];
    //!             PRINTOUT MONTHLY
                    //WRITE(KW[1],104)HED[K],(PARM.SMM[K,L],L = 1,12),PARM.SMY[K],HED[K]
                }
                //WRITE(KW[1],108)'WIDX',(PARM.IWIX[L],L = 1,12),'WIDX'
            }
            if (PARM.NJC>0){
                for (J = 0; J < PARM.NJC; J++){
                    K = PARM.JC[J];
                    //WRITE(KW[1],109)HED[K],(PARM.SMM[K,L],L = 1,12),PARM.SMY[K],HED[K];
                }
            }
            if (PARM.NKS == 0) goto lbl71;
            for (J = 0; J < PARM.NKS; J++){
                K = PARM.KS[J];
                //WRITE(KW[1],95)HEDS[K],(PARM.STV[K,L],L = 1,12),HEDS[K]
            }
            goto lbl71;
//!     PRINTOUT ANNUAL
     lbl69: //WRITE(KW[1],96)PARM.IYR,(HED[KYA[K]],PARM.SMY[KYA[K]],K = 1,NKYA)
     lbl71: K = 1;
            double N2 = NN;
            PARM.FGIS = 0.0;
            double YGIS = 0.0;
            double BGIS = 0.0;
            double WGIS = 0.0;
            double YLNG = 0.0;
            double YLPG = 0.0;
            double YLKG = 0.0;
            double HGIS = 0.0;
            double WSGS = 0.0;
            double SNGS = 0.0;
            double SPGS = 0.0;
            double STGS = 0.0;
            double SAGS = 0.0;
            double SSGS = 0.0;
            SUM = 0.0;
     lbl80: if (K == 1 || K == 6){
                //Functions.APAGE(1);
                //WRITE[KW[1],102)PARM.IYR,PARM.IY
            }
            J = PARM.LY[PARM.IRO,K];
            PARM.PLCX = PARM.SMMC[5,J,11];
            SUM = SUM+PARM.PLCX;
            PARM.IYH[J] = PARM.IYH[J]+1;
            N1 = Math.Max(1,PARM.NCP[J]);
            for (L = 0; L < 20; L++){
        //!         PRINTOUT CROP MONTHLY
                //WRITE(KW[1],95)HEDC[L],(PARM.SMMC[L,J,K1],K1 = 1,12),HEDC[L]
                for (K1 = 0; K1 < 12; K1++){
                    PARM.SMMC[L,J,K1] = 0.0;
                }
            }
            for (K1 = 0; K1 < N1; K1++){
                for (L = 0; L < 7; L++){
                    PARM.TSFC[L,J] = PARM.TSFC[L,J]+PARM.SF[L,K1,J];
                }
            }
            //WRITE(KW[1],108)'STRS',(KDT[L,J],L = 1,12),'STRS'
            for (J1 = N1; J1 > 1; J1--){
                if (PARM.CAW[J1,J]<1.0E-10){
                    PARM.CAW[J1,J] = PARM.AWC;
                    PARM.AWC = 0.0;
                    PARM.CRF[J1,J] = PARM.ARF;
                    PARM.ARF = 0.0;
                    PARM.CQV[J1,J] = PARM.AQV;
                    PARM.AQV = 0.0;
                    PARM.JP[J] = 0;
                }
                PARM.TCAW[J] = PARM.TCAW[J]+PARM.CAW[J1,J];
                PARM.TCQV[J] = PARM.TCQV[J]+PARM.CQV[J1,J];
                PARM.TCRF[J] = PARM.TCRF[J]+PARM.CRF[J1,J];
            }
            double PMTE = 0.0;
            for (J1 = 0; J1 < N1; J1++){
                if (PARM.ETG[J1,J]>0.0){
                    double ETGS = PARM.ETG[J1,J];
                    PARM.SMGS[2] = PARM.SMGS[2]+ETGS;
                }
                else{
                    PARM.ETG[J1,J] = PARM.ACET[J];
                }
                PMTE = PMTE+PARM.DM[J1]+PARM.STD[J1];
                XTP[0,J1,J] = PARM.YLD1[J1,J]*PARM.PRYG[J];
                XTP[1,J1,J] = PARM.YLD2[J1,J]*PARM.PRYF[J];
                XTP[3,J1,J] = PARM.WCYD;
                PARM.VALF1 = XTP[0,J1,J]+PARM.VALF1+XTP[1,J1,J];
	            if (PARM.ETG[J1,J]>.1){
	                XTP[2,J1,J] = 1000.0*(PARM.YLD1[J1,J]+PARM.YLD2[J1,J])/PARM.ETG[J1,J];
                }
                else{
	                XTP[2,J1,J] = 0.0;
                }
                XX = PARM.NPSF[J1,J];
                PARM.TPSF[J1,J] = PARM.TPSF[J1,J]/(XX+Math.Pow(10, -10));
                YGIS = Math.Max(YGIS,PARM.YLD1[J1,J]);
                PARM.SMGS[0] = PARM.SMGS[0]+YGIS;
                BGIS = Math.Max(BGIS,PARM.DMF[J1,J]);
                WGIS = Math.Max(WGIS,XTP[2,J1,J]);
                PARM.FGIS = Math.Max(PARM.FGIS,PARM.FRTN[J1,J]);
                PARM.SMGS[4] = PARM.SMGS[4]+PARM.FGIS;
                YLNG = Math.Max(YLNG,PARM.YLNF[J1,J]);
                YLPG = Math.Max(YLPG,PARM.YLPF[J1,J]);
                YLKG = Math.Max(YLKG,PARM.YLKF[J1,J]);
                HGIS = Math.Max(HGIS,PARM.HIF[J1,J]);
                PARM.SMGS[7] = PARM.SMGS[6]+HGIS;
                WSGS = Math.Max(WSGS,PARM.SF[0,J1,J]);
                SNGS = Math.Max(SNGS,PARM.SF[1,J1,J]);
                SPGS = Math.Max(SPGS,PARM.SF[2,J1,J]);
                STGS = Math.Max(STGS,PARM.SF[4,J1,J]);
                SAGS = Math.Max(SAGS,PARM.SF[5,J1,J]);
                SSGS = Math.Max(SSGS,PARM.SF[6,J1,J]);
                if (PARM.IY == PARM.IPY){
                    if (PARM.CSTF[J1,J] <= 0.0){
                        PARM.CSTF[J1,J] = PARM.COST;
                        PARM.CSOF[J1,J] = PARM.COST-PARM.CSFX;
                        PARM.COST = 0.0;
                        PARM.CSFX = 0.0;
                    }
                    //!         PRINTOUT CROP ANNUAL
                    X1 = 1000.0*PARM.YLCF[J1,J];
                    if (PARM.IDC[J] == PARM.NDC[6] || PARM.IDC[J] == PARM.NDC[7] || PARM.IDC[J] == PARM.NDC[9]){
                        // X2 = .0001*PLL0[J]; // PPL0 does not exist any where
                    }
                    else{
                        // X2 = PLL0[J]; // PPL0 does not exist any where
                    }
                    //WRITE(KW[1],106)PARM.CPNM[J],PARM.YLD1[J1,J],PARM.YLD2[J1,J],PARM.DMF[J1,J],PARM.YLNF[J1,J],PARM.YLPF[J1,J],PARM.YLKF[J1,J],X1,PARM.FRTN[J1,J],PARM.FRTP[J1,J],PARM.FRTK[J1,J],VIR[J1,J],PARM.VIL[J1,J],PARM.CAW[J1,J],PARM.ETG[J1,J],XTP[3,J1,J],X2,PARM.TPSF[J1,J],PARM.CSTF[J1,J],PARM.CSOF[J1,J],XTP[1,J1,J],XTP[2,J1,J],PARM.EK,PARM.REK,WK
                    //WRITE(KW[1],97)(PARM.SF[L,J1,J],L = 1,7)
                    TSMQ = 0.0;
                    TSMY = 0.0;
                }
                PARM.TDM[J] = PARM.TDM[J]+PARM.DMF[J1,J];
				// These are not defined any where, and they are also never used any where but here.
                //PARM.TLY1[J] = TLY1[J]+PARM.YLD1[J1,J];
                //PARM.TLY2[J] = PARM.TLY2[J]+PARM.YLD2[J1,J];
                //PARM.TLYN[J] = PARM.TLYN[J]+PARM.YLNF[J1,J];
                //PARM.TLYP[J] = PARM.TLYP[J]+PARM.YLPF[J1,J];
                //PARM.TLYK[J] = PARM.TLYK[J]+PARM.YLKF[J1,J];
                //PARM.TLYC[J] = PARM.TLYC[J]+PARM.YLCF[J1,J];
                if (PARM.YLNF[J1,J]>0.0){
                    PARM.NYLN[J] = PARM.NYLN[J]+1;
                    XX = PARM.NYLN[J];
                    X1 = XX+1.0;
					// BYLN doesn't exist any where but here again in the entire project
                    //X2 = Math.Max(1.0,PARM.BYLN[J],1.1*PARM.SF[1,J1,J]);
					PARM.UNA[J] = Math.Min(1000.0,PARM.UNA[J]*(PARM.PRMT[27]/(1.0-Math.Pow((PARM.SF[1,J1,J]/X2), 2))));
                    X3 = PARM.UNA[J];
                    PARM.ULYN[J] = (PARM.ULYN[J]*XX+PARM.YLNF[J1,J])/X1;
                    PARM.UNA[J] = PARM.PRMT[45]*PARM.UNA[J]+(1.0-PARM.PRMT[45])*PARM.ULYN[J];
                }
                PARM.TRD[J] = PARM.TRD[J]+PARM.RDF[J];
                PARM.THU[J] = PARM.THU[J]+PARM.HUF[J];
                PARM.TETG[J] = PARM.TETG[J]+PARM.ETG[J1,J];
                PARM.TVIR[J] = PARM.TVIR[J]+PARM.VIR[J1,J];
                PARM.TIRL[J] = PARM.TIRL[J]+PARM.VIL[J1,J];
                PARM.CST1 = PARM.CST1+PARM.CSTF[J1,J];
                PARM.CST1 = PARM.CST1+PARM.CSOF[J1,J];
                PARM.TFTN[J] = PARM.TFTN[J]+PARM.FRTN[J1,J];
                PARM.TFTP[J] = PARM.TFTP[J]+PARM.FRTP[J1,J];
                PARM.TFTK[J] = PARM.TFTK[J]+PARM.FRTK[J1,J];
                PARM.TCST[J] = PARM.TCST[J]+PARM.CSTF[J1,J];
                PARM.TCSO[J] = PARM.TCSO[J]+PARM.CSOF[J1,J];
                PARM.TVAL[J] = PARM.TVAL[J]+XTP[1,J1,J]+XTP[1,J1,J];
                PARM.PSTM[J] = PARM.PSTM[J]+PARM.TPSF[J1,J];
            }

            if (N1>1) N2 = N2-1;
            K = K+1;
            if (K <= N2) goto lbl80;
            PARM.DPLC = PARM.DPLC+SUM-PLC0;
            PLC0 = SUM;
            PARM.VIRT = 0.0;
            PARM.DARF = PARM.DARF+PARM.SMY[3]*PARM.SMY[3];
            if (PARM.SMY[3]>PARM.BARF){
                PARM.BARF = PARM.SMY[3];
            }
            else{
                if (PARM.SMY[3]<PARM.SARF) PARM.SARF = PARM.SMY[3];
            }
        //!     PRINTOUT ANNUAL FILE
            X1 = .001*PARM.TOC;
            if (PARM.NSTP == 0) goto lbl556;
	        if (PARM.IY != PARM.IGSD) goto lbl555;
            K = PARM.IRTC;
            L1 = PARM.LY[PARM.IRO,K];
            J = Math.Max(1,PARM.NHV[L1]);
	        //WRITE(KW[2],498)PARM.IYR,IRLX,PARM.SMY[4],PARM.SMY[10],PARM.SMY[11],PARM.SMY[14],PARM.SMY[16],PARM.SMY[17],PARM.SMY[29],PARM.SMY[33],PARM.SMY[42],PARM.SMY[48],PARM.SMY[47],PARM.SMY[50],PARM.SMY[51],PARM.SMY[52],PARM.SMY[49],PARM.SMY[43],PARM.SMY[44],PARM.SMY[45],PARM.SMY[46],PARM.SMY[56],PARM.SMY[54],PARM.SMY[55],PARM.SMY[57],PARM.TLA,PARM.OCPD,X1,PARM.APBC,TAP,PARM.TNO3
            if (PARM.KFL[18] == 0) goto lbl555;
            //WRITE(KW[19],558]PARM.IYR,IRLX,PARM.CPNM[L1],PARM.YLD1[J,L1],PARM.YLD2[J,L1],XTP[4,J,L1],Hif [J,L1],PARM.DMF[J,L1],RWF[J,L1],PARM.YLNF[J,L1],PARM.YLPF[J,L1],PARM.YLCF[J,L1],PARM.FRTN[J,L1],PARM.FRTP[J,L1],PARM.VIR[J,L1],PARM.VIL[J,L1],XTP[3,J,L1],PARM.ETG[J,L1],PARM.CAW[J,L1],PARM.CRF[J,L1],PARM.CQV[J,L1],PARM.CSTF[J,L1],PARM.CSOF[J,L1],XTP[1,J,L1],XTP[2,J,L1],PARM.TPSF[J,L1],[PARM.SF[L,J,L1],L = 1,7],PARM.PLL0[L1],PARM.IPLD[J,L1],PARM.IGMD[J,L1],IHVD[J,L1]
            if (J == 1) goto lbl555;
            J1 = J-1;
            PARM.IPLD[J1,L1] = PARM.IPLD[J,L1];
            PARM.IGMD[J1,L1] = PARM.IGMD[J,L1];
            goto lbl555;

    lbl556: if (PARM.KFL[1]>0) {} //continue; //WRITE(KW[2],498)PARM.IYR,PARM.IY,PARM.SMY[4],PARM.SMY[10],PARM.SMY[11],PARM.SMY[14],PARM.SMY[16],PARM.SMY[17],PARM.SMY[29],PARM.SMY[33],PARM.SMY[42],PARM.SMY[48],PARM.SMY[47],PARM.SMY[50],PARM.SMY[51],PARM.SMY[52],PARM.SMY[49],PARM.SMY[43],PARM.SMY[44],PARM.SMY[45],PARM.SMY[46],PARM.SMY[56],PARM.SMY[54],PARM.SMY[55],PARM.SMY[57],PARM.TLA,PARM.OCPD,X1,PARM.APBC,TAP,PARM.TNO3
            if (PARM.KFL[18] == 0) goto lbl555;
	        K = 1;
	        N2 = NN;
    lbl686: J = PARM.LY[PARM.IRO,K];
            N1 = Math.Max(1,PARM.NCP[J]);
            for (J1 = 0; J1 < N1; J1++){
                //WRITE(KW[19],558)PARM.IYR,PARM.IY,PARM.CPNM[J],PARM.YLD1[J1,J],PARM.YLD2[J1,J],XTP[4,J1,J],Hif [J1,J],PARM.DMF[J1,J],RWF[J1,J],PARM.YLNF[J1,J],PARM.YLPF[J1,J],PARM.YLCF[J1,J],PARM.FRTN[J1,J],PARM.FRTP[J1,J],PARM.VIR[J1,J],PARM.VIL[J1,J],XTP[3,J1,J],PARM.ETG[J1,J],PARM.CAW[J1,J],PARM.CRF[J1,J],PARM.CQV[J1,J],PARM.CSTF[J1,J],PARM.CSOF[J1,J],XTP[1,J1,J],XTP[2,J1,J],PARM.TPSF[J1,J],[PARM.SF[L,J1,J],L = 1,7],PARM.PLL0[J],PARM.IPLD[J1,J],PARM.IGMD[J1,J],IHVD[J1,J]
                if (J1 == 1) continue;
                L1 = J1-1;
                PARM.IPLD[L1,J] = PARM.IPLD[J1,J];
                PARM.IGMD[L1,J] = PARM.IGMD[J1,J];
            }
            if (N1>1) N2 = N2-1;
	        K = K+1;
	        if (K <= N2) goto lbl686;

    lbl555: if (PARM.KFL[22]>0){
                for (K = 0; K < NN; K++){
                    L1 = PARM.LY[PARM.IRO,K];
                    for (J = 0; J < PARM.NCP[L1]; J++){
                        continue;
                        //WRITE(KW[23],668)PARM.IYR,PARM.IY,M21,K21,PARM.CPNM[L1],DM1[L1],(RWTX(PARM.LID[I],L1),I = 1,PARM.NBSL),PARM.RWX[L1]
                    }
                }
            }
            if (PARM.KFL[21]>0){
                PARM.RNO3 = PARM.SMY[3]*PARM.RFNC;
                //WRITE(KW[22],566)PARM.IYR,PARM.SMY[4],PARM.SMY[10],PARM.SMY[11],PARM.SMY[13],PARM.SMY[14],[PARM.SMY[J],J = 16,20),RNO3,(PARM.SMY[J],J = 43,46),PARM.SMY[49],PARM.SMY[89],PARM.SMY[52],PARM.SMY[85],PARM.SMY[50],(PARM.SMY[J],J = 59,61)
            }
            PARM.APBC = .5*(PARM.SMY[0]+PARM.SMY[1]);
            if (PARM.KFL[7]>0) //WRITE(KW[8],293)IRO0,PARM.IYR,PARM.APBC,PARM.PMTE,(PARM.SMY[KYA[K]],K = 1,NKYA)
            //PARM.RWTX = 0.0;
			foreach (int x in PARM.RWTX){
				foreach (int y in PARM.RWTX){
					PARM.RWTX[x, y] = 0.0;	
				}
			}
            for (K = 0; K < NN; K++){
                J = PARM.LY[PARM.IRO,K];
                if (PARM.KFL[23]>0){
	                if (PARM.IDC[J] == PARM.NDC[6] || PARM.IDC[J] == PARM.NDC[7]) continue; //WRITE(KW[24],669)PARM.IYR,PARM.IY,PARM.CPNM[J],PARM.YLD2[K,J],DM[J],PARM.RW[J],PARM.SLAI[J],PARM.STD[J]
                }
                PARM.NCP[J] = 1;
                if (PARM.KG[J] == 0) PARM.NCP[J] = 0;
                PARM.NHV[J] = 0;
	            PARM.RWX[J] = 0.0;
                for (I = N1; I > 1; I--){
                    PARM.NPSF[I,J] = 0;
                    PARM.FRTN[I,J] = 0.0;
                    PARM.FRTP[I,J] = 0.0;
                    PARM.FRTK[I,J] = 0.0;
                    PARM.YLD1[I,J] = 0.0;
                    PARM.YLD2[I,J] = 0.0;
                    PARM.YLNF[I,J] = 0.0;
                    PARM.YLPF[I,J] = 0.0;
                    PARM.YLKF[I,J] = 0.0;
                    PARM.YLCF[I,J] = 0.0;
                    PARM.DMF[I,J] = 0.0;
                    PARM.RWF[I,J] = 0.0;
                    PARM.VIL[I,J] = 0.0;
                    PARM.VIR[I,J] = 0.0;
                    PARM.CAW[I,J] = 0.0;
                    PARM.CQV[I,J] = 0.0;
                    PARM.CRF[I,J] = 0.0;
                    PARM.TPSF[I,J] = 0.0;
                    PARM.CSOF[I,J] = 0.0;
                    PARM.CSTF[I,J] = 0.0;
                    PARM.ETG[I,J] = 0.0;
                }
	            PARM.HUF[J] = 0.0;
                PARM.RDF[J] = 0.0;
                // Another variable that doesn't exist any where
				//BYLN[J] = 0.0;
                if (PARM.IDC[J] == PARM.NDC[3] || PARM.IDC[J] == PARM.NDC[6] || PARM.IDC[J] == PARM.NDC[7] || PARM.IDC[J] == PARM.NDC[8] || PARM.IDC[J] == PARM.NDC[10]){
                    PARM.ANA[J] = 0.0;
	                PARM.SWH[J] = 0.0;
	                PARM.SWP[J] = 0.0;
                }
                if (PARM.IDC[J] != PARM.NDC[1] && PARM.IDC[J] != PARM.NDC[4]) PARM.DM1[J] = 0.0;
            }

            if (PARM.KFL[PARM.NGF] == 0) goto lbl499;
	        X2 = PARM.SMY[48]+PARM.SMY[51];
	        X3 = PARM.SMY[42]+PARM.SMY[43]+PARM.SMY[44]+PARM.SMY[45];
	        //SWGS = SWGS/REAL(PARM.ND) What's real do again
/*!     XLOG       =  SITE LONGITUDE
!     YLAT       =  SITE LATITUDE
!     YGIS       =  CROP PARM.YLD (t/ha)
!     PARM.WGIS       =  CROP PARM.YLD/GROWING SEASON ET (kg/mm)
!     PARM.ETGS       =  GROWING SEASON ET (mm)
!     GSEF       =  GROWING SEASON PLANT TRANSPIRATION (mm)
!     PARM.SMY(19)    =  IRRIGATION VOLUME (mm)
!     PARM.FGIS       =  N FERTILIZER APPLIED (kg/ha)
!     PARM.SMY(11)    =  ACTUAL ET (mm)
!     GSVF       =  GROWING SEASON VPD (kpa)
!     PARM.HGIS       =  CROP HARVEST INDEX
!     SRAF       =  GROWING SEASON PARM.SOLAR RADIATION (MJ/M^2)
!     PARM.YLNG       =  N CONTENT OF CROP PARM.YLD (kg/ha)
!     TNGS       =  N CONTENT OF SOIL (kg/ha)
!     X2         =  N LOST TO ATMOSPHERE (kg/ha)
!     X3         =  N LOST WITH RUNOFF, SUBSURFACE FLOW, AND EROSION
!     PARM.SMY(42)    =  WIND EROSION (t/ha)
!     PARM.SMY(NDVSS) =  WATER EROSION FOR DRIVING EQ (t/ha)
!     PARM.SMY(3)     =  PARM.SOLAR RADIATION (MJ/M^2)
!     PARM.BGIS       =  BIOMASS PRODUCTION (t/ha)
!     PARM.SF(1,      =  WATER STRESS (d)
!     PARM.SF(2,      =  N STRESS (d)
!     PARM.SF(3,      =  P STRESS (d)
!     PARM.SF(4,      =  K STRESS (d)
!     PARM.SF(5,      =  TEMPERATURE STRESS (d)
!     PARM.SF(6,      =  AERATION STRESS (d)
!     PARM.SF(7,      =  SALT STRESS (d)
!     PARM.SMY(46)    =  N LEACHED (kg/ha)
!     PARM.SMY(43)    =  ORGANIC N LOSS WITH SEDIMENT (kg/ha)
!     PARM.SMY(47)    =  NET MINERALIZATION (kg/ha)
!     PARM.SMY(49)    =  DENITRIFICATION (kg/ha)
!     PARM.SMY(50)    =  N FIXATION (kg/ha)
!     PARM.SMY(85)    =  N MINERALIZATION FROM HUMUS (kg/ha)
!     PARM.SMY(51)    =  NITRIFICATION (kg/ha)
!     PARM.SMY(52)    =  VOLATILIZATION (kg/ha)
!     PARM.SMY(53)    =  N LOSS IN DRAINAGE SYSTEM (kg/ha)
!     PARM.SMY(54)    =  P LOSS WITH SEDIMENT (kg/ha)
!     PARM.SMY(56)    =  P MINERALIZATION (kg/ha)
!     PARM.SMY(57)    =  P LEACHED (kg/ha)
!     PARM.SMY(58)    =  ENRICHMENT RATIO
!     PARM.SMY(59)    =  N FERT ORGANIC FORM (kg/ha)
!     PARM.SMY(60)    =  N FERT NO3 FORM (kg/ha)
!     PARM.SMY(61)    =  N FERT NH3 FORM(kg/ha)
!     PARM.SMY(62)    =  P FERT ORGANIC FORM (kg/ha)
!     PARM.SMY(63)    =  P FERT MINERAL FORM (kg/ha)
!     PARM.YLPG       =  P CONTENT OF CROP YIELD (kg/ha)
!     PARM.YLKG       =  K CONTENT OF CROP YIELD (kg/ha)
!     PARM.SMY(77)    =  ORGANIC C LOSS WITH SEDIMENT(kg/ha)
!     PARM.SMY(4)     =  PRECIPITATION (mm)
!     PARM.SMY(17)    =  PERCOLATION (mm)
!     PARM.SMY(14)    =  RUNOFF (mm)
!     PARM.SMY(5)     =  SNOWFALL (WATER EQUIVALENT mm)
!     PARM.SMY(6)     =  SNOWMELT (WATER EQUIVALENT mm)
!     PARM.SMY(10)    =  POTENTIAL ET (mm)
!     PARM.SMY(12)    =  POTENTIAL PLANT TRANSPIRATION (mm)
!     PARM.SMY(13)    =  ACTUAL PLANT TRANSPIRATION (mm)
!     PARM.SMY(16)    =  LATERAL SUBSURFACE FLOW (mm)
!     PARM.SMY(18)    =  DRAINAGE SYSTEM FLOW (mm)
!     PARM.SMY(15)    =  SCS CURVE NUMBER
!     PARM.SMY(20)    =  EXTERNAL INFLOW TO MAINTAIN WATER TABLE (mm)
!     SWGS       =  ROOT ZONE SOIL WATER (mm)
!     PARM.SMY(68)    =  SOIL WATER IN TOP 10 mm OF SOIL (mm)*/
            //WRITE(KW(IGIS),685)XLOG,YLAT,YGIS,PARM.WGIS,PARM.ETGS,GSEF,PARM.SMY(19),PARM.FGIS,PARM.SMY(11),GSVF,PARM.HGIS,SRAF,PARM.YLNG,TNGS,X2,X3,PARM.SMY(42),PARM.SMY(NDVSS),PARM.SMY(3),PARM.BGIS,(PARM.SF(L,1,1),L = 1,7),PARM.SMY(46],PARM.SMY(43],PARM.SMY[47],PARM.SMY[49],PARM.SMY[50],PARM.SMY[85],PARM.SMY[51],PARM.SMY[52],PARM.SMY[53],PARM.SMY[54],PARM.SMY[56],PARM.SMY[57],PARM.SMY[58],PARM.SMY[59],PARM.SMY[60],PARM.SMY[61],PARM.SMY[62],PARM.SMY[63],PARM.YLPG,PARM.YLKG,PARM.SMY[77],PARM.SMY[4],PARM.SMY[17],PARM.SMY[14],PARM.SMY[5],PARM.SMY[6],PARM.SMY[10],PARM.SMY[12],PARM.SMY[13],PARM.SMY[16],PARM.SMY[18],PARM.SMY[15],PARM.SMY[20],SWGS,PARM.SMY[68]
            PARM.SMGS[7] = PARM.SMGS[7]+SWGS;
    lbl499: double WB1 = PARM.SMY[33];
            double WB2 = PARM.SMY[41];
            //PARM.SF = 0.0;
			foreach (int x in PARM.SF){
				foreach (int y in PARM.SF){
					foreach (int z in PARM.SF){
						PARM.SF[x, y, z] = 0.0;	
					}
				}
			}
            //PARM.SMM = 0.0;
			foreach (int x in PARM.SMM){
				foreach (int y in PARM.SMM){
					PARM.SMM[x, y] = 0.0;	
				}
			}
            if (PARM.NDP == 0) goto lbl83;
            //PARM.SMYP = 0.0;
			foreach (int x in PARM.SMYP){
				foreach (int y in PARM.SMYP){
					PARM.SMYP[x, y] = 0.0;	
				}
			}
            //PARM.SMMP = 0.0;
			foreach (int x in PARM.SMMP){
				foreach (int y in PARM.SMMP){
					foreach (int z in PARM.SMMP){
						PARM.SMMP[x, y, z] = 0.0;	
					}
				}
			}
            //PARM.TPAC = 0.0;
			foreach (int x in PARM.TPAC){
				foreach (int y in PARM.TPAC){
					PARM.TPAC[x, y] = 0.0;	
				}
			}
     lbl83: PARM.MO1 = PARM.MO;
     lbl84: //continue; //CONTINUE
            PARM.IBD = 1;
            PARM.MO = 1;
            PARM.KT = 1;
            PARM.KC = 0;
            double IYR1 = PARM.IYR;
            PARM.IYR = PARM.IYR+1;
            PARM.IYX = PARM.IYX+1;
            PARM.NYD = 1;
            PARM.IPY = (int)(PARM.IPY+II);
            //Functions.SCONT(1);
	        if (PARM.ICOR0>0){
                for (J = 0; J < 21; J++){
                    PARM.IX[J] = PARM.IX0[J];
                    PARM.IDG[J] = J;
                }
            }
			double TPAW, Y1, XZ, ZZ, XY;
            if (PARM.ISW <= 3 || PARM.ISW == 7){
                XX = 0.0;
                K = 1;
                TPAW = 0.0;
                for (I = 0; I < PARM.NBSL; I++){
                    J = PARM.LID[I];
	                Y1 = .1*PARM.WOC[J]/PARM.WT[J];
	                XZ = .0172*Y1;
	                ZZ = 1.0-XZ;
                    X1 = 1000.0*(PARM.Z[J]-XX);
                    if (PARM.ISW != 0 && PARM.ISW != 2){
                        if (PARM.ISW == 7){
                            //Functions.SWNN(PARM.CLA[J],PARM.SAN[J],Y1,X2,X3);
                        }
                        else{
                            ZZ = .5*(XX+PARM.Z[J]);
                            //Functions.SWRTNB(CEM[J],PARM.CLA[J],Y1,PARM.SAN[J],X2,X3,ZZ);
                        }
					}
                    else{
                        //Functions.SWRTNR(PARM.CLA[J],PARM.SAN[J],Y1,X2,X3);
                    }
                    XY = 1.0-PARM.ROK[J]*.01;
                    XZ = XY*X1;
                    PARM.S15[J] = X2*XZ;
                    PARM.FC[J] = X3*XZ;
                    //Functions.SPOFC(J);
                    if (K>3) goto lbl670;
                    TPAW = TPAW+PARM.FC[J]-PARM.S15[J];
		    lbl672: if (TPAW<PARM.WCS[K]) goto lbl670;
                    //PZW does not exist any where but is used here before being intialized
					//ZCS[K] = XX+(PARM.Z[J]-XX)*((PARM.WCS[K]-PZW)/(TPAW-PZW));
                    K = K+1;
                    if (K<4) goto lbl672;
            lbl670: //PZW = TPAW;
                    XX = PARM.Z[J];
				}
			}

            if (PARM.ISTA>0){
                for (J = 0; J < PARM.NBSL; J++){
                    L = PARM.LID[J];
                    PARM.WHSC[L] = PARM.SOL[0,L];
                    PARM.WHPC[L] = PARM.SOL[1,L];
                    PARM.WLSC[L] = PARM.SOL[2,L];
                    PARM.WLMC[L] = PARM.SOL[3,L];
                    PARM.WBMC[L] = PARM.SOL[4,L];
                    PARM.WOC[L] = PARM.SOL[5,L];
                    PARM.WHSN[L] = PARM.SOL[6,L];
                    PARM.WHPN[L] = PARM.SOL[7,L];
                    PARM.WLSN[L] = PARM.SOL[8,L];
                    PARM.WLMN[L] = PARM.SOL[9,L];
                    PARM.WBMN[L] = PARM.SOL[10,L];
                    PARM.WON[L] = PARM.SOL[11,L];
                    PARM.PMN[L] = PARM.SOL[12,L];
                    PARM.WP[L] = PARM.SOL[13,L];
                    PARM.OP[L] = PARM.SOL[14,L];
                    PARM.EXCK[L] = PARM.SOL[15,L];
                    PARM.FIXK[L] = PARM.SOL[16,L];
                    //!             ST[L] = PARM.SOL[18,L]
                    PARM.WLS[L] = PARM.SOL[18,L];
                    PARM.WLM[L] = PARM.SOL[19,L];
                    PARM.WLSL[L] = PARM.SOL[20,L];
                    PARM.WLSLC[L] = PARM.SOL[21,L];
                    PARM.WLSLNC[L] = PARM.SOL[22,L];
                }
        //!         //Functions.SCONT(1)
            }
            //Functions.SPRNT(YTP);
	        if (PARM.KFL[13]>0) //Functions.SOCIOA(PARM.IYR1,12,PARM.KDA);
	        //PARM.SMY = 0.0;
			foreach (int x in PARM.SMY){
				PARM.SMY[x] = 0.0;	
			}
	        if (PARM.IPD == 2 || PARM.IPD == 4){
                //WRITE(KW[1],101)
                //Functions.SOLIO(YTP,1);
            }
            if (PARM.IGSD != 0 && PARM.IY == PARM.IGSD){
                //REWIND KR(7)
                //Functions.WREAD();
                PARM.NGN = PARM.NGN0;
                PARM.IYR = PARM.IYR0;
                PARM.IGSD = PARM.IGSD+PARM.NRO;
            }
            //Functions.ALPYR(PARM.IYR,PARM.NYD,PARM.LPYR);
	        IGIS = IGIS+1;

     lbl87: //continue; //CONTINUE
            PARM.IY = PARM.NBYR+1;
	 lbl88: return;
   /*88 RETURN
   89 FORMAT(1X,3I4,2X,'GERMINATION--0.2 m SW  =  ',F5.1,'mm',2X,'PARM.HU  =  ',&
      F4.0,'c',2X,'HUSC  =  ',F4.2)
   92 FORMAT(1X,3I4,2X,'RB FR DK',20X,'DKH = ',F5.0,'mm',2X,'DKI = ',F6.2&
     &,'m',2X,'HUSC = ',F4.2)
   94 FORMAT(//T5,'Y M D  OPERATION')
   95 FORMAT(1X,A4,12F9.2,11X,A4)
   96 FORMAT(//I5,9(2X,A4,F8.2)/(5X,9(2X,A4,F8.2)))
   97 FORMAT(T10,'STRESS DAYS (BIOM)     WATER = ',F5.1,2X,'N = ',F5.1,2X,&
     &'P = ',F5.1,2X,'K = ',F5.1,2X,'TEMP = ',F5.1,2X,'AIR = ',F5.1,2X,'SALT = ',&
     &F5.1)
   99 FORMAT(45X,'YR = ',I4,2X,'YR# = ',I4/T11,'JAN',9X,'FEB',9X,'MAR',9X,&
     &'APR',9X,'MAY',9X,'JUN',9X,'JUL',9X,'AUG',9X,'SEP',9X,'OCT',9X,&
     &'NOV',9X,'DEC',9X,' YR')
  100 FORMAT(10F8.2)
  101 FORMAT(T5,'SOIL DATA')
  102 FORMAT(45X,'YR = ',I4,2X,'YR# = ',I4/T11,'JAN',6X,'FEB',6X,'MAR',6X&
     &,'APR',6X,'MAY',6X,'JUN',6X,'JUL',6X,'AUG',6X,'SEP',6X,'OCT',6X,&
     &'NOV',6X,'DEC',6X,' YR')
  103 FORMAT(14X,8F6.0)
  104 FORMAT(1X,A4,13F9.2,2X,A4)
  105 FORMAT(1X,3I4,1X,A4,5(2X,A4,F8.3)/(10X,5(2X,A4,F8.3)))
  106 FORMAT(2X,A4,1X,'PARM.YLD = ',F5.1,'/',F5.1,2X,'BIOM = ',F5.1,'t/ha',2X,&
      'YLN = ',F5.0,2X,'YLP = ',F5.0,2X,'YLK = ',F5.0,2X,'YLC = ',F6.0,2X,'FN = ',&
      F5.0,2X,'FP = ',F5.0,2X,'FK = ',F5.0,'kg/ha'/T7,'IRGA = ',F5.0,2X,'IRDL = ',&
      F5.0,2X,'PARM.CAW = ',F5.0,'mm',2X,'GSET = ',F5.0,'mm',2X,'WUEF = ',F6.2,&
      'kg/mm',2X,'POP = ',F9.4,'p/m2',2X,'PSTF = ',F4.2/T7,'PARM.COST = ',F7.0,2X,&
      'PARM.COOP = ',F7.0,2X,'RTRN = ',F5.0,'/',F5.0,'$/ha',2X,'PARM.EK = ',F5.3,2X,&
      'PARM.REK = ',F5.3,2X,'WK = ',F5.3)
  107 FORMAT(1X,I4,2X,I2,2X,I2,50F10.3)
  108 FORMAT(1X,A4,12I9,11X,A4)
  109 FORMAT(1X,A4,13F9.4,2X,A4)
  111 FORMAT(34X,'-------------------------',A8,'-----------------------&
     &--')
  112 FORMAT(47X,'PESTICIDE SIMULATION (G/HA)')
  113 FORMAT(1X,A4,13F9.0,2X,A4)
  114 FORMAT(1X,A4,12F9.0,11X,A4)
  119 FORMAT(10I8)
  143 FORMAT(1X,A4,13F12.5,2X,A4)
  146 FORMAT(1X,A4,12F12.5,14X,A4)
  156 FORMAT(/T5,'YEAR ',I4,' OF ',I4,/)
  293 FORMAT(1X,2I4,42F8.2)
  465 FORMAT(8X,'ADSRB',5E13.5)
  462 FORMAT(5X,A16)
  464 FORMAT(8X,'Q+PARM.SSF',5E13.5)
  463 FORMAT(8X,'PARM.SOL  ',5E13.5)
  466 FORMAT(8X,'SED Y',5E13.5)
  468 FORMAT(1X,3I4,1X,I4,1X,A16,14F10.4)
  471 FORMAT(4X,'Y M D  RT#  PSTN',12X,10(4X,A4,2X),5X,'Q',8X,'PARM.SSF',6X,&
     &'PRK',4X,'ROCONC')
  474 FORMAT(4X,4I4)
  475 FORMAT(1X,I4,I2,I4,2X,40F8.1)
  477 FORMAT(4X,'Y M RT#',43(4X,A4))
  498 FORMAT(1X,2I4,6F8.1,F8.3,17F8.1,F8.2,F8.1,F8.2,2F8.1,10(A16,F8.0))
  505 FORMAT(A80)
  513 FORMAT(1X,3I4,1X,I4,4(4X,A4,7F8.3))
  516 FORMAT(4X,'Y   M   D  ',13(1X,A4,3X))
  517 FORMAT(T27,'|----------------------------------------------TEMP(C)&
      --------------------------------------------------|'/6X,'DATE',8X,&
      'DAMP',15X,'|_______________________________@ CENTER OF SOIL LAYER&
      S______________________________________|'/T5,'Y   M   D',3X,'DEPTH&
      (m)',5X,'SURF',7X,'1',9X,'2',9X,'3',9X,'4',9X,'5',9X,'6',9X,'7',9X&
      ,'8',9X,'9',8X,'10')
  520 FORMAT(4X,'Y   M  ',32(A4,4X))
  523 FORMAT(1X,I4,I2,I4,2X,A4,13F8.1)
  526 FORMAT(4X,'Y M RT#  PARM.CPNM',6X,'PARM.WS',6X,'NS',6X,'PS',6X,'KS',6X,'TS',&
      6X,'AS',6X,'SS',6(4X,A4))
  527 FORMAT(4X,'Y M D  RT#',4(4X,'PARM.CPNM',3X,'PARM.WS',6X,'NS',6X,'PS',6X,'KS'&
      ,6X,'TS',6X,'AS',6X,'SS'))
  532 FORMAT(1X,3I4,1X,5(2X,A4,F8.3)/(10X,5(2X,A4,F8.3)))
  533 FORMAT(5X,12I4)
  558 FORMAT(1X,2I4,1X,A4,6F8.3,6F8.1,2F8.3,4F8.1,4F8.2,9F8.3,3I9,&
      10(1X,A16,F8.0))
  562 FORMAT(1X,I4,I2,2X,60F8.1)
  565 FORMAT(1X,2I4,28F8.2,4X,A4,F8.2,F8.0)
  566 FORMAT(1X,I4,4X,10F8.2,24X,15F8.2,4X,A4,F8.2,F8.0)
  567 FORMAT(1X,3I4,2X,'LIME',14X,I4,6X,'   9',8X,F10.2,10X,2F10.2)
  582 FORMAT(T10,'ATMOS PARM.CO2  =  ',F5.0)
  588 FORMAT(5X,'!!!!!',3I4,' Q =  ',F6.1)
  589 FORMAT(5X,'^^^^^',3I4,' PARM.PDSW  =  ',F6.1,1X,'FCSW  =  ',F6.1)
  666 FORMAT(1X,3I4,2X,A8,8X,I6,6X,3I4,2F10.2,20X,F10.2)
  668 FORMAT(1X,4I4,1X,A4,20F8.2)
  669 FORMAT(1X,2I4,1X,A4,5F8.2)
  679 FORMAT(1X,I4,1X,I4,1X,13(1X,F10.3))
  680 FORMAT(10X,'WATERSHED AREA  =  ',F10.2,' HA'/18X,'Q',10X,'Y',9X,'PARM.YN'&
      ,9X,'YP',9X,'QN',9X,'PARM.QP'/2X,'YR    PARM.MO',7X,'(mm)',6X,'(t/ha)',3X,&
      '|-----------------(kg/ha)---------------|')
  681 FORMAT(1X,I4,1X,I4,6F8.2,1X,A16,1X,A4,8F8.2,E12.5)
  682 FORMAT(1X,I4,2X,I2,2X,I2,200F8.3)
  683 FORMAT(T60,A16,1X,A4,8F8.2,E12.5)
  685 FORMAT(1X,4F10.2,5F10.1,2F10.3,F10.0,50F10.2)
  731 FORMAT(4X,'Y',3X,'M',3X,'D',4X,'SW15',4X,'SW30',3X,'NO315',3X,&
      'NO330',3X,'NH315',3X,'NH330',3X,200(A4,4X))
      END*/
		}
	}
}

