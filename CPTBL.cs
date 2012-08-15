using System;

namespace Epic
{
	public class CPTBL
	{
		public CPTBL ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program reads crop table to determine crop parameters
			
			// The fortran file uses global variables, and needs to be fixed
			// for C# style coding
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            string adum;
            double[] XTP = new double[57];
            if (PARM.NSTP > 0) goto lbl174;
            if (PARM.LC == 0) goto lbl174;
            int L;
            for (L = 0; L < PARM.LC; L++){
                if (PARM.KDC[L] == PARM.JX[6]) goto lbl179;
            }

    lbl174: PARM.LC = PARM.LC+1;
            PARM.KDC1[PARM.JX[5]] = PARM.LC;
            PARM.KDC[PARM.LC] = PARM.JX[5];
            PARM.XMTU[PARM.LC] = PARM.JX[6];

/*!     READ CROP DATA
!     CPNM = NAMES OF CROPS IN CROP PARAMETER TABLE
!  1  WA   = BIOMASS/ENERGY (kg/ha/MJ)(FOR CO2 = 330 ppm
!  2  HI   = HARVEST INDEX (CROP YIELD/ABOVE GROUND BIOMASS)
!  3  TOPC = OPTIMAL TEMP FOR PLANT GROWTH (C)
!  4  TBSC = MIN TEMP FOR PLANT GROWTH (C)
!  5  DMLA = MAX LEAF AREA INDEX
!  6  DLAI = FRACTION OF GROWING SEASON WHEN LEAF AREA STARTS DECLINING
!  7  DLAP(= LAI DEVELOPMENT PARMS--NUMBERS BEFORE DECIMAL = %
!  8   1,2) OF GROWING SEASON .  NUMBERS AFTER DDECIMAL = FRACTION OF
!            DMLA AT GIVEN %.
!  9  RLAD = LAI DECLINE RATE FACTOR.
! 10  RBMD = WA  DECLINE RATE FACTOR.
! 11  ALT  = ALUMINUM TOLERANCE INDEX(1-5)1=SENSITIVE, 5=TOLERANT
! 12  GSI  = MAX STOMATAL CONDUCTANCE (DROUGTH TOLERANT PLANTS HAVE
!            LOW VALUES.)
! 13  CAF  = CRITICAL AERATION FACTOR (SW/POR > CAF CAUSES AIR STRESS)
! 14  SDW  = SEEDING RATE (kg/ha)
! 15  HMX  = MAX CROP HEIGHT (m)
! 16  RDMX = MAX ROOT DEPTH (m)
! 17  WAC2 = NUMBER BEFORE DECIMAL = CO2 CONC IN FUTURE ATMOSPHERE
!            (ppm).  NUMBER AFTER DECIMAL = RESULTING WA VALUE.
! 18  CNY  = N FRACTION OF YIELD (KG/KG)
! 19  CPY  = P FRACTION OF YIELD (KG/KG)
! 20  CKY  = K FRACTION OF YIELD (KG/KG)
! 21  WSYF = LOWER LIMIT OF HARVEST INDEX
! 22  PST  = PEST(INSECTS,WEEDS,AND DISEASE)FACTOR (0-1)
! 23  CSTS = SEED COST ($/KG)
! 24  PRYG = PRICE FOR SEED YIELD ($/t)
! 25  PRYF = PRICE FOR FORAGE YIELD ($/t)
! 26  WCY  = FRACTION WATER IN YIELD.
!27-29BN   = N FRACTION IN PLANT WHEN GROWTH IS 0.,.5,1.0
!30-32BP   = P FRACTION IN PLANT WHEN GROWTH IS 0.,.5,1.
!33-35BK   = K FRACTION IN PLANT WHEN GROWTH IS 0.,.5,1.
!36-38BW   = WIND EROSION FACTORS FOR STANDING LIVE, STANDING DEAD,
!            AND FLAT RESIDUE
! 39  IDC  = CROP ID NUMBERS. USED TO PLACE CROPS IN CATEGORIES
!            (1 FOR WARM SEASON ANNUAL LEGUME
!             2 FOR COLD SEASON ANNUAL LEGUME
!             3 FOR PERENNIAL LEGUME
!             4 FOR WARM SEASON ANNUAL
!             5 FOR COLD SEASON ANNUAL
!             6 FOR PERENNIAL
!             7 FOR EVERGREEN TREES
!             8 FOR DECIDUOUS TREES
!             9 FOR COTTON
!            10 FOR DECIDUOUS TREES (LEGUME)
! 40/ FRST(= FROST DAMAGE PARMS--NUMBERS BEFORE DECIMAL = MIN TEMP(deg C
! 41   1,2) NUMBERS AFTER DECIMAL = FRACTION YLD LOST WHEN GIVEN MIN TE
!            IS EXPERIENCED.
! 42  WAVP = PARM RELATING VAPOR PRESSURE DEFFICIT TO WA
! 43  VPTH = THRESHOLD VPD (kpa)(F=1.
! 44  VPD2 = NUMBER BEFORE DECIMAL = VPD VALUE (kpa).  NUMBER AFTER
!            DECIMAL = F2 < 1.
! 45  RWPC1= ROOT WEIGHT/BIOMASS PARTITIONING COEF
! 46  RWPC2= ROOT WEIGHT/BIOMASS PARTITIONING COEF
! 47  GMHU = HEAT UNITS REQUIRED FOR GERMINATION
! 48  PPLP1= PLANT POP PARM--NUMBER BEFORE DECIMAL = # PLANTS
!            NO AFTER DEC = FRACTION OF MAX LAI
! 49  PPLP2= 2ND POINT ON PLANT POP-LAI CURVE. PPLP1<PPLP2--PLANTS/M2
!                                              PPLP1>PPLP2--PLANTS/HA
! 50  STX1 = YLD DECREASE/SALINITY INCREASE ((t/ha)/(MMHO/CM))
! 51  STX2 = SALINITY THRESHOLD (MMHO/CM)
! 52  BLG1 = LIGNIN FRACTION IN PLANT AT .5 MATURITY
! 53  BLG2 = LIGNIN FRACTION IN PLANT AT MATURITY
! 54  WUB  = WATER USE CONVERSION TO BIOMASS(T/MM)
! 55  FTO  = FRACTION TURNOUT (COTTON LINT/STRIPPER YLD)
! 56  FLT  = FRACTION LINT (COTTON LINT/PICKER YLD)
! 57  CCEM = CARBON EMISSION/SEED WEIGHT(KG/KG)
*/
      //READ(KR[4],1)ADUM
	  //READ(KR[4],1)ADUM
			
            // Continue
	        double J2 = -1;
	  /*DO WHILE(J2/=PARM.JX[6])
          READ(KR[4],333,IOSTAT=NFL)J2,CPNM[PARM.LC],(XTP[L],L=1,57)
          IF(NFL/=0)THEN
              IF(IBAT==0)THEN
                  WRITE(*,*)'CROP NO = ',PARM.JX[6],' NOT IN CROP LIST FILE'
                  PAUSE
              ELSE
	              WRITE(KW[MSO],'(A,A8,A,I4,A)')' !!!!! ',ASTN,&
	              &'CROP NO = ',PARM.JX[6],' NOT IN CROP LIST FILE'
	          END IF
              STOP	              
	      END IF       
      END DO*/


            PARM.WA[PARM.LC] = XTP[0];
            PARM.HI[PARM.LC] = XTP[1];
            PARM.TOPC[PARM.LC] = XTP[2];
            PARM.TBSC[PARM.LC] = XTP[3];
            PARM.DMLA[PARM.LC] = XTP[4];
            PARM.DLAI[PARM.LC] = XTP[5];
            PARM.DLAP[0,PARM.LC] = XTP[6];
            PARM.DLAP[1,PARM.LC] = XTP[7];
            PARM.RLAD[PARM.LC] = XTP[8];
            PARM.RBMD[PARM.LC] = XTP[9];
            PARM.ALT[PARM.LC] = XTP[10];
            PARM.GSI[PARM.LC] = XTP[11];
            PARM.CAF[PARM.LC] = XTP[12];
            PARM.SDW[PARM.LC] = XTP[13];
            PARM.HMX[PARM.LC] = XTP[14];
            PARM.RDMX[PARM.LC] = XTP[15];
            PARM.WAC2[1,PARM.LC] = XTP[16];
            PARM.CNY[PARM.LC] = XTP[17];
            PARM.CPY[PARM.LC] = XTP[18];
            PARM.CKY[PARM.LC] = XTP[19];
            PARM.WSYF[PARM.LC] = XTP[20];
            PARM.PST[PARM.LC] = XTP[21];
            PARM.CSTS[PARM.LC] = XTP[22];
            PARM.PRYG[PARM.LC] = XTP[23];
            PARM.PRYF[PARM.LC] = XTP[24];
            PARM.WCY[PARM.LC] = XTP[25];
            PARM.BN[0,PARM.LC] = XTP[26];
            PARM.BN[1,PARM.LC] = XTP[27];
            PARM.BN[2,PARM.LC] = XTP[28];
            PARM.BP[0,PARM.LC] = XTP[29];
            PARM.BP[1,PARM.LC] = XTP[30];
            PARM.BP[2,PARM.LC] = XTP[31];
            PARM.BK[0,PARM.LC] = XTP[32];
            PARM.BK[1,PARM.LC] = XTP[33];
            PARM.BK[2,PARM.LC] = XTP[34];
            PARM.BWD[0,PARM.LC] = XTP[35];
            PARM.BWD[1,PARM.LC] = XTP[36];
            PARM.BWD[2,PARM.LC] = XTP[37];
            PARM.IDC[PARM.LC] = (int)XTP[38];
            PARM.FRST[0,PARM.LC] = XTP[39];
            PARM.FRST[1,PARM.LC] = XTP[40];
            PARM.WAVP[PARM.LC] = XTP[41];
            PARM.VPTH[PARM.LC] = XTP[42];
            PARM.VPD2[PARM.LC] = XTP[43];
            PARM.RWPC[0,PARM.LC] = XTP[44];
            PARM.RWPC[1,PARM.LC] = XTP[45];
            PARM.GMHU[PARM.LC] = XTP[46];
            PARM.PPLP[0,PARM.LC] = XTP[47];
            PARM.PPLP[1,PARM.LC] = XTP[48];
            PARM.STX[0,PARM.LC] = XTP[49];
            PARM.STX[1,PARM.LC] = XTP[50];
            PARM.BLG[0,PARM.LC] = XTP[51];
            PARM.BLG[1,PARM.LC] = XTP[52];
            PARM.WUB[PARM.LC] = XTP[53];
            PARM.FTO[PARM.LC] = XTP[54];
            PARM.FLT[PARM.LC] = XTP[55];
            PARM.CCEM[PARM.LC] = XTP[56];
            //REWIND KR[4]; // What am I suppose to do here, goto statements?

    lbl179: PARM.JJK = PARM.KDC1[PARM.JX[5]];
            PARM.IHU[PARM.JJK] = PARM.IHU[PARM.JJK]+1;
            PARM.PHU[PARM.JJK,PARM.IHU[PARM.JJK]] = PARM.OPV[0];
            if (PARM.XMTU[PARM.JJK]>0) PARM.PHU[PARM.JJK,PARM.IHU[PARM.JJK]] = 0.0;//Functions.CAHU(1,365,PARM.TBSC[PARM.JJK],0)*PARM.XMTU[PARM.JJK];

            double Y1 = PARM.PPLP[0,PARM.JJK];
            double Y2 = PARM.PPLP[1,PARM.JJK];
            
            double X4, X5;
            if (Y2 > Y1){
                X4 = Y1;
                X5 = Y2;
            }
            else{
                X4 = Y2;
                X5 = Y1;
            }

            double X1 = Functions.ASPLT(ref X4);
            double X2 = Functions.ASPLT(ref X5);
	        Functions.ASCRV(ref X4,ref X5,ref X1,ref X2);
            PARM.PPCF[0,PARM.JJK] = X4;
            PARM.PPCF[1,PARM.JJK] = X5;
			
			double X3;
            if (PARM.OPV[5] > 0.0){
                X3 = PARM.OPV[4];
            }
            else{
                double G1 = X2;
                int IT;
                double Z1, Z2, FU, DFDU;
                for (IT = 0; IT < 10; IT++){
                    Z1 = Math.Exp(X4-X5*G1);
	                Z2 = G1+Z1;
                    FU = G1/Z2-.9;
                    if (Math.Abs(FU) < Math.Pow(10, -5)) break;
	                DFDU = Z1*(1.0+X5*G1)/(Z2*Z2);
	                G1 = G1-FU/DFDU;
                }
                //IF(IT>10)WRITE(KW[1],5)
                X3 = G1;
            }

            PARM.PPLA[PARM.JJK,PARM.IHU[PARM.JJK]] = PARM.DMLA[PARM.JJK]*X3/(X3+Math.Exp(X4-X5*X3));
            PARM.POP[PARM.JJK,PARM.IHU[PARM.JJK]] = X3;
            if (PARM.OPV[5] > 0.0) PARM.FMX = PARM.OPV[5];
            PARM.FNMX[PARM.JJK] = PARM.FMX;
            return;
   /* 1 FORMAT(A12)
    5 FORMAT(5X,'!!!!! PLANT POP DID NOT CONVERGE')
  333 FORMAT(1X,I4,1X,A4,57F8.0)
      END*/
		}
	}
}

