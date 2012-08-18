using System;

namespace Epic
{
	public partial class Functions
	{
		public static void AINLZ ()
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program initializes variables
			
			// The fortran file uses global variables, and needs to be fixed
			// for C# style coding

            /* ADDITIONAL CHANGE
            * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
            */
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			PARM.ICV = 0;
			//PARM.ICUS = 0;
			foreach (int x in PARM.ICUS){
				PARM.ICUS[x] = 0;
			}
			PARM.IDFT[0] = PARM.IDF0;
			PARM.IDRL = 0;
			foreach (int x in PARM.IGMD){
				foreach (int y in PARM.IGMD){
					PARM.IGMD[x, y] = 0;	
				}
			}
			PARM.IGO = 0;
			foreach (int x in PARM.IHRL){
				PARM.IHRL[x] = 0;	
			}
			foreach (int x in PARM.IHT){
				PARM.IHT[x] = 0;	
			}
			foreach (int x in PARM.IHU){
				PARM.IHU[x] = 0;	
			}
			PARM.IHV = 0;
			foreach (int x in PARM.IHVD){
				foreach (int y in PARM.IHVD){
					PARM.IHVD[x, y] = 0;
				}
			}
			PARM.IPST = 0;
			PARM.IPY = 1;
			PARM.IPYI = 1;
			PARM.IRL = 0;
			foreach (int x in PARM.ISIX){
				PARM.ISIX[x] = 0;
			}
			PARM.ISX = 0;
			PARM.IYR = PARM.IYR0;
			PARM.JCN = 0;
			PARM.JCN0 = 0;
			PARM.JCN1 = 0;
			//PARM.JE = PARM.MNC+1;
			foreach (int x in PARM.JE){
				PARM.JE[x] = PARM.MNC+1;
			}
			//PARM.JPL = 0;
			foreach (int x in PARM.JPL){
				PARM.JPL[x] = 0;
			}
			//PARM.KDC1 = 0;
			foreach (int x in PARM.KDC1){
				PARM.KDC1[x] = 0;
			}
			//PARM.KDT2 = 0;
			foreach (int x in PARM.KDT2){
				PARM.KDT2[x] = 0;
			}
			//PARM.KG = 0;
			foreach (int x in PARM.KG){
				PARM.KG[x] = 0;
			}
			PARM.LC = 0;
			//PARM.LY = 0;
			foreach (int x in PARM.LY){
				foreach(int y in PARM.LY){
					PARM.LY[x, y] = 0;
				}
			}
			//PARM.LYR = 1;
			foreach (int x in PARM.LYR){
				foreach (int y in PARM.LYR){
					PARM.LYR[x, y] = 1;
				}
			}
			PARM.LW = 1;
			PARM.MXT = 0;
			//PARM.NBC = 0;
			foreach (int x in PARM.NBC){
				PARM.NBC[x] = 0;
			}
			//PARM.NBCX = 0;
			foreach (int x in PARM.NBCX){
				foreach (int y in PARM.NBCX){
					PARM.NBCX[x, y] = 0;
				}
			}
			//PARM.NCP = 0;
			foreach (int x in PARM.NCP){
				PARM.NCP[x] = 0;
			}
			//PARM.NCR = 0;
			foreach (int x in PARM.NCR){
				PARM.NCR[x] = 0;
			}
			PARM.NDT = 0;
			PARM.NDF = 0;
			PARM.NDP = 0;
			PARM.NDT = 0;
			PARM.NFA = 0;
			//PARM.NHU = 0;
			foreach (int x in PARM.NHU){
				PARM.NHU[x] = 0;
			}
			//PARM.NHV = 0;
			foreach (int x in PARM.NHV){
				PARM.NHV[x] = 0;
			}
			PARM.NMW = 0;
			//PARM.NPSF = 0;
			foreach (int x in PARM.NPSF){
				foreach (int y in PARM.NPSF){
					PARM.NPSF[x, y] = 0;
				}
			}
			PARM.NQP = 0;
			PARM.NQP0 = 0;
			PARM.NQP1 = 0;
			PARM.NWDA = 0;
			PARM.NWD0 = 0;
			//PARM.NYLN = 0;
			foreach (int x in PARM.NYLN){
				PARM.NYLN[x] = 0;
			}
			//PARM.ACET = 0.0;
			foreach (int x in PARM.ACET){
				PARM.ACET[x] = 0.0;
			}
			PARM.ADRF = 100;
			PARM.AFLG = 0;
			PARM.AGPM = 0;
			PARM.AILG = 0;
			//PARM.AJHI = 0;
			foreach (int x in PARM.AJHI){
				PARM.AJHI[x] = 0.0;
			}
			//PARM.ANA = 0;
			foreach (int x in PARM.ANA){
				PARM.ANA[x] = 0;
			}
			PARM.APB = 0;
			//PARM.APQ = 0;
			foreach (int x in PARM.APQ){
				foreach (int y in PARM.APQ){
					foreach (int z in PARM.APQ){
						PARM.APQ[x, y, z] = 0.0;
					}
				}
			}
			//PARM.APY = 0;
			foreach (int x in PARM.APY){
				foreach (int y in PARM.APY){
					foreach (int z in PARM.APY){
						PARM.APY[x, y, z] = 0.0;
					}
				}
			}
			//PARM.AQB = 0;
			foreach (int x in PARM.AQB){
				foreach (int y in PARM.AQB){
					foreach (int z in PARM.AQB){
						PARM.AQB[x, y, z] = 0.0;
					}
				}
			}
			//PARM.ASW = 0;
			foreach (int x in PARM.ASW){
				PARM.ASW[x] = 0.0;
			}
			//PARM.AYB = 0;
			foreach (int x in PARM.AYB){
				foreach (int y in PARM.AYB){
					foreach (int z in PARM.AYB){
						PARM.AYB[x, y, z] = 0.0;
					}
				}
			}
			PARM.BARF = 0;
			//PARM.BLYN = 0;
			foreach (int x in PARM.BLYN){
				PARM.BLYN[x] = 0.0;
			}
			PARM.CAP = 0;
			//PARM.CAW = 0;
			foreach (int x in PARM.CAW){
				foreach (int y in PARM.CAW){
					PARM.CAW[x, y] = 0;
				}
			}
			//PARM.CFRT = .05;
			foreach (int x in PARM.CFRT){
				foreach (int y in PARM.CFRT){
					PARM.CFRT[x, y] = .05;
				}
			}
			//PARM.CHT = .01;
			foreach (int x in PARM.CHT){
				PARM.CHT[x] = 0.01;
			}
			//PARM.CNDS = 0;
			foreach (int x in PARM.CNDS){
				PARM.CNDS[x] = 0.0;
			}
			PARM.COST = 0;
			//PARM.CRF = 0;
			foreach (int x in PARM.CRF){
				foreach (int y in PARM.CRF){
					PARM.CRF[x, y] = 0.0;
				}
			}
			//PARM.CQV = 0;
			foreach (int x in PARM.CQV){
				foreach (int y in PARM.CQV){
					PARM.CQV[x, y] = 0.0;
				}
			}
			PARM.CSFX = 0;
			PARM.CSO1 = 0;
			PARM.CST1 = 0;
			//PARM.CSTF = 0;
			foreach (int x in PARM.CSTF){
				foreach (int y in PARM.CSTF){
					PARM.CSTF[x, y] = 0.0;
				}
			}
			PARM.CV = 0;
			PARM.CVP = 0;
			PARM.CVRS = 0;
			//PARM.CX = 1*Math.Pow (10, -10);
			foreach (int x in PARM.CX){
				PARM.CX[x] = Math.Pow (10, -10);
			}
			PARM.CYAV = 0;
			PARM.CYMX = 0;
			PARM.CYSD = 0;
			PARM.DARF = 0;
			PARM.DHT = 0;
			PARM.DKHL = 0;
			//PARM.DM = 0;
			foreach (int x in PARM.DM){
				PARM.DM[x] = 0.0;
			}
			//PARM.DM1 = 0;
			foreach (int x in PARM.DM1){
				PARM.DM1[x] = 0.0;
			}
			//PARM.DMF = 0;
			foreach (int x in PARM.DMF){
				foreach (int y in PARM.DMF){
					PARM.DMF[x, y] = 0.0;
				}
			}
			//PARM.EP = 0;
			foreach (int x in PARM.EP){
				PARM.EP[x] = 0;
			}
			//PARM.ETG = 0;
			foreach (int x in PARM.ETG){
				foreach (int y in PARM.ETG){
					PARM.ETG[x, y] = 0.0;
				}
			}
			PARM.FCSW = 0;
			PARM.FGC = 0;
			//PARM.FRTK = 0;
			foreach (int x in PARM.FRTK){
				foreach (int y in PARM.FRTK){
					PARM.FRTK[x, y] = 0.0;
				}
			}
			//PARM.FRTN = 0;
			foreach (int x in PARM.FRTN){
				foreach (int y in PARM.FRTN){
					PARM.FRTN[x, y] = 0.0;
				}
			}
			//PARM.FRTP = 0;
			foreach (int x in PARM.FRTP){
				foreach (int y in PARM.FRTP){
					PARM.FRTP[x, y] = 0.0;
				}
			}
			//PARM.FULU = 0;  
			foreach (int x in PARM.FULU){
				PARM.FULU[x] = 0.0;
			}
			PARM.GSEF = 0;
			PARM.GSEP = 0;
			PARM.GSVF = 0;
			PARM.GSVP = 0;
			//PARM.HIF = 0;
			foreach (int x in PARM.HIF){
				foreach (int y in PARM.HIF){
					PARM.HIF[x, y] = 0.0;
				}
			}
			//PARM.HU = 0;
			foreach (int x in PARM.HU){
				PARM.HU[x] = 0.0;
			}
			//PARM.HUF = 0;
			foreach (int x in PARM.HUF){
				PARM.HUF[x] = 0.0;
			}
			//PARM.HUI = 0;
			foreach (int x in PARM.HUI){
				PARM.HUI[x] = 0.0;
			}
			PARM.OCPD = 0;
			PARM.PAW = 0;
			PARM.PDSW = 0;
			//PARM.PHU = 0;  
			foreach (int x in PARM.PHU){
				foreach (int y in PARM.PHU){
					PARM.PHU[x, y] = 0.0;
				}
			}
			PARM.PMOEO = 100;
			PARM.PMORF = 100;
			//PARM.PO = 0;
			foreach (int x in PARM.PO){
				PARM.PO[x] = 0.0;
			}
			//PARM.POP = 0;
			foreach (int x in PARM.POP){
				foreach (int y in PARM.POP){
					PARM.POP[x, y] = 0.0;
				}
			}
			//PARM.PPL0 = 0; 
			foreach (int x in PARM.PPL0){
				PARM.PPL0[x] = 0.0;
			}
			//PARM.PPLA = 0;
			foreach (int x in PARM.PPLA){
				foreach (int y in PARM.PPLA){
					PARM.PPLA[x, y] = 0.0;
				}
			}
			PARM.PRAV = 0;
			PARM.PRB = 0;
			PARM.PRSD = 0;
			//PARM.PSTF = 1;
			foreach (int x in PARM.PSTF){
				PARM.PSTF[x] = 1.0;
			}
			//PARM.PSTM = 0;
			foreach (int x in PARM.PSTM){
				PARM.PSTM[x] = 0.0;
			}
			//PARM.PSTN = ' ';	
			foreach (int x in PARM.PSTN){
				foreach (int y in PARM.PSTN){
					PARM.PSTN[x, y] = ' ';
				}
			}
			PARM.PSTS = 0;
			//PARM.PVQ = 0;
			foreach (int x in PARM.PVQ){
				foreach (int y in PARM.PVQ){
					PARM.PVQ[x, y] = 0.0;
				}
			}
			//PARM.PVY = 0;
			foreach (int x in PARM.PVY){
				foreach (int y in PARM.PVY){
					PARM.PVY[x, y] = 0.0;
				}
			}
			//PARM.QIN = 0;
			foreach (int x in PARM.QIN){
				PARM.QIN[x] = 0.0;
			}
			PARM.QPQB = 0;
			PARM.QPS = 0;
			PARM.RCF = 1;
			//PARM.RCM = 0;
			foreach (int x in PARM.RCM){
				PARM.RCM[x] = 0.0;
			}
			//PARM.RD = 0;
			foreach (int x in PARM.RD){
				PARM.RD[x] = 0.0;
			}
			//PARM.RDF = 0;
			foreach (int x in PARM.RDF){
				PARM.RDF[x] = 0.0;
			}
			//PARM.REG = 0;
			foreach (int x in PARM.REG){
				PARM.REG[x] = 0.0;
			}
			PARM.RFSM = 0;
			PARM.RHTT = 0;
			PARM.RGIN = 0;
			PARM.RRUF = .01;
			//PARM.RSPC = 0;
			foreach (int x in PARM.RSPC){
				PARM.RSPC[x] = 0.0;
			}
			//PARM.RSTK = 0;
			foreach (int x in PARM.RSTK){
				foreach (int y in PARM.RSTK){
					PARM.RSTK[x, y] = 0.0;
				}
			}
			//PARM.RSY = 0;
			foreach (int x in PARM.RSY){
				PARM.RSY[x] = 0.0;
			}
			//PARM.RUSM = 0;
			foreach (int x in PARM.RUSM){
				PARM.RUSM[x] = 0.0;
			}
			//PARM.RW = 0;
			foreach (int x in PARM.RW){
				PARM.RW[x] = 0.0;
			}
			//PARM.RWF = 0;
			foreach (int x in PARM.RWF){
				foreach (int y in PARM.RWF){
					PARM.RWF[x, y] = 0.0;
				}
			}
			//PARM.RWT = 0;
			foreach (int x in PARM.RWT){
				foreach (int y in PARM.RWT){
					PARM.RWT[x, y] = 0.0;
				}
			}
			//PARM.RWX = 0;
			foreach (int x in PARM.RWX){
				PARM.RWX[x] = 0.0;
			}
			PARM.RZSW = 0;
			PARM.SARF = 1*Math.Pow (10,10);
			//PARM.SET = 0;
			foreach (int x in PARM.SET){
				PARM.SET[x] = 0.0;
			}
			//PARM.SF = 0;
			foreach (int x in PARM.SF){
				foreach (int y in PARM.SF){
					foreach (int z in PARM.SF){
						PARM.SF[x, y, z] = 0.0;
					}
				}
			}
			//PARM.SFMO = 0;
			foreach (int x in PARM.SFMO){
				foreach (int y in PARM.SFMO){
					PARM.SFMO[x, y] = 0.0;
				}
			}
			PARM.SHRL = 1;
			//PARM.SLAI = 0;
			foreach (int x in PARM.SLAI){
				PARM.SLAI[x] = 0.0;
			}
			//PARM.SM = 0;
			foreach (int x in PARM.SM){
				PARM.SM[x] = 0.0;
			}
			//PARM.SMAP = 0;
			foreach (int x in PARM.SMAP){
				foreach (int y in PARM.SMAP){
					PARM.SMAP[x, y] = 0.0;
				}
			}
			//PARM.SMGS = 0;
			foreach (int x in PARM.SMGS){
				PARM.SMGS[x] = 0.0;
			}
			//PARM.SMM = 0;
			foreach (int x in PARM.SMM){
				foreach (int y in PARM.SMM){
					PARM.SMM[x, y] = 0.0;
				}
			}
			//PARM.SMMC = 0;
			foreach (int x in PARM.SMMC){
				foreach (int y in PARM.SMMC){
					foreach (int z in PARM.SMMC){
						PARM.SMMC[x, y, z] = 0.0;
					}
				}
			}
			//PARM.SMMP = 0;
			foreach (int x in PARM.SMMP){
				foreach (int y in PARM.SMMP){
					foreach (int z in PARM.SMMP){
						PARM.SMMP[x, y, z] = 0.0;
					}
				}
			}
			//PARM.SMS = 0;
			foreach (int x in PARM.SMS){
				foreach (int y in PARM.SMS){
					PARM.SMS[x, y] = 0.0;
				}
			}
			//PARM.SMY = 0;
			foreach (int x in PARM.SMY){
				PARM.SMY[x] = 0.0;
			}
			//PARM.SMYP = 0;
			foreach (int x in PARM.SMYP){
				foreach (int y in PARM.SMYP){
					PARM.SMYP[x, y] = 0.0;
				}
			}
			//PARM.SOT = 0;
			foreach (int x in PARM.SOT){
				PARM.SOT[x] = 0.0;
			}
			//PARM.SPQ = 0;
			foreach (int x in PARM.SPQ){
				foreach (int y in PARM.SPQ){
					PARM.SPQ[x, y] = 0.0;
				}
			}
			//PARM.SPY = 0;
			foreach (int x in PARM.SPY){
				foreach (int y in PARM.SPY){
					PARM.SMYP[x, y] = 0.0;
				}
			}
			//PARM.SQB = 0;
			foreach (int x in PARM.SQB){
				PARM.SQB[x] = 0.0;
			}
			PARM.SRA = 0;
			//PARM.SRD = 0;
			foreach (int x in PARM.SRD){
				PARM.SRD[x] = 0.0;
			}
			PARM.SSFK = 0;
			PARM.SSFN = 0;
			PARM.SST = 0;
			PARM.SSW = 0;
			//PARM.ST = 0;
			foreach (int x in PARM.ST){
				PARM.ST[x] = 0.0;
			}
			//PARM.STD = PARM.STD0;
			foreach (int x in PARM.STD){
				PARM.STD[x] = PARM.STD0;
			}
			//PARM.STDA = 0;
			foreach (int x in PARM.STDA){
				foreach (int y in PARM.STDA){
					PARM.STDA[x, y] = 0.0;
				}
			}
			//PARM.STDN = 0;
			foreach (int x in PARM.STDN){
				PARM.STDN[x] = 0.0;
			}
			PARM.STDO = 0;
			PARM.STDOK = 0;
			PARM.STDON = 0;
			PARM.STDOP = 0;
			//PARM.STL = 0;
			foreach (int x in PARM.STL){
				PARM.STL[x] = 0.0;
			}
			//PARM.STMP = 0;
			foreach (int x in PARM.STMP){
				PARM.STMP[x] = 0.0;
			}
			//PARM.STV = 0;
			foreach (int x in PARM.STV){
				foreach (int y in PARM.STV){
					PARM.STV[x, y] = 0.0;
				}
			}
			//PARM.SYB = 0;
			foreach (int x in PARM.SYB){
				PARM.SYB[x] = 0.0;
			}
			//PARM.TAL = 0;
			foreach (int x in PARM.TAL){
				PARM.TAL[x] = 0.0;
			}
			//PARM.TAMX = 0; 
			foreach (int x in PARM.TAMX){
				PARM.TAMX[x] = 0.0;
			}
			PARM.TAP = 0;
			PARM.TCAV = 0;
			//PARM.TCAW = 0;
			foreach (int x in PARM.TCAW){
				PARM.TCAW[x] = 0.0;
			}
			//PARM.TCQV = 0;
			foreach (int x in PARM.TCQV){
				PARM.TCQV[x] = 0.0;
			}
			//PARM.TCRF = 0;
			foreach (int x in PARM.TCRF){
				PARM.TCRF[x] = 0.0;
			}
			PARM.TCMX = 0;
			PARM.TCMN = 1*Math.Pow (10, 20);
			//PARM.TCSO = 0;
			foreach (int x in PARM.TCSO){
				PARM.TCSO[x] = 0.0;
			}
			//PARM.TCST = 0;
			foreach (int x in PARM.TCST){
				PARM.TCST[x] = 0.0;
			}
			//PARM.TDM = 0;
			foreach (int x in PARM.TDM){
				PARM.TDM[x] = 0.0;
			}
			//PARM.TEI = 0;
			foreach (int x in PARM.TEI){
				PARM.TEI[x] = 0.0;
			}
			PARM.TEK = 0;
			//PARM.TET = 0;
			foreach (int x in PARM.TET){
				PARM.TET[x] = 0.0;
			}
			//PARM.TETG = 0;
			foreach (int x in PARM.TETG){
				PARM.TETG[x] = 0.0;
			}
			PARM.TFK = 0;
			//PARM.TFTK = 0;
			foreach (int x in PARM.TFTK){
				PARM.TFTK[x] = 0.0;
			}
			//PARM.TFTN = 0;
			foreach (int x in PARM.TFTN){
				PARM.TFTN[x] = 0.0;
			}
			//PARM.TFTP = 0;
			foreach (int x in PARM.TFTP){
				PARM.TFTP[x] = 0.0;
			}
			PARM.THK = 0;
			//PARM.THU = 0;
			foreach (int x in PARM.THU){
				PARM.THU[x] = 0.0;
			}
			//PARM.TIRL = 0;
			foreach (int x in PARM.TIRL){
				PARM.TIRL[x] = 0.0;
			}
			PARM.TLMF = 0;
			PARM.TMP = 0;
			//PARM.TMXF = 0;
			foreach (int x in PARM.TMXF){
				PARM.TMXF[x] = 0.0;
			}
			PARM.TNOR = 0;
			PARM.TNO2 = 0;
			PARM.TNO3 = 0;
			PARM.TOC = 0;
			PARM.TOP = 0;
			PARM.TP = 0;
			//PARM.TPAC = 0;
			foreach (int x in PARM.TPAC){
				foreach (int y in PARM.TPAC){
					PARM.TPAC[x, y] = 0.0;
				}
			}
			//PARM.TPOR = 0;
			foreach (int x in PARM.TPOR){
				PARM.TPOR[x] = 0.0;
			}
			//PARM.TPSF = 0;
			foreach (int x in PARM.TPSF){
				foreach (int y in PARM.TPSF){
					PARM.TPSF[x, y] = 0.0;
				}
			}
			//PARM.TQ = 0;
			foreach (int x in PARM.TQ){
				PARM.TQ[x] = 0.0;
			}
			//PARM.TR = 0;
			foreach (int x in PARM.TR){
				PARM.TR[x] = 0.0;
			}
			//PARM.TRA = 0;
			foreach (int x in PARM.TRA){
				PARM.TRA[x] = 0.0;
			}
			//PARM.TRD = 0;
			foreach (int x in PARM.TRD){
				PARM.TRD[x] = 0.0;
			}
			//PARM.TRHT = 0;
			foreach (int x in PARM.TRHT){
				PARM.TRHT[x] = 0.0;
			}
			PARM.TRSD = 0;
			//PARM.TSFC = 0;
			foreach (int x in PARM.TSFC){
				foreach (int y in PARM.TSFC){
					PARM.TSFC[x, y] = 0.0;
				}
			}
			PARM.TSK = 0;
			PARM.TSLT = 0;
			//PARM.TSN = 0;
			foreach (int x in PARM.TSN){
				PARM.TSN[x] = 0.0;
			}
			PARM.TSNO = 0;
			//PARM.TSR = 0;
			foreach (int x in PARM.TSR){
				PARM.TSR[x] = 0.0; 
			}
			//PARM.TSTL = 0;
			foreach (int x in PARM.TSTL){
				PARM.TSTL[x] = 0.0;
			}
			//PARM.TSY = 0;
			foreach (int x in PARM.TSY){
				PARM.TSY[x] = 0.0;
			}
			//PARM.TVAL = 0;
			foreach (int x in PARM.TVAL){
				PARM.TVAL[x] = 0.0;
			}
			//PARM.TVIR = 0;
			foreach (int x in PARM.TVIR){
				PARM.TVIR[x] = 0.0;
			}
			PARM.TWN = 0;
			//PARM.TXMN = 0;
			foreach (int x in PARM.TXMN){
				PARM.TXMN[x] = 0.0;
			}
			//PARM.TXMX = 0;
			foreach (int x in PARM.TXMX){
				PARM.TXMX[x] = 0.0;
			}
			PARM.TYC = 0;
			PARM.TYK = 0;
			//PARM.TYL1 = 0;
			foreach (int x in PARM.TYL1){
				PARM.TYL1[x] = 0.0;
			}
			//PARM.TYL2 = 0;
			foreach (int x in PARM.TYL2){
				PARM.TYL2[x] = 0.0;
			}
			//PARM.TYLC = 0;
			foreach (int x in PARM.TYLC){
				PARM.TYLC[x] = 0.0;
			}
			//PARM.TYLN = 0;
			foreach (int x in PARM.TYLN){
				PARM.TYLN[x] = 0.0;
			}
			//PARM.TYLP = 0;
			foreach (int x in PARM.TYLP){
				PARM.TYLP[x] = 0.0;
			}
			//PARM.TYLK = 0;
			foreach (int x in PARM.TYLK){
				PARM.TYLK[x] = 0.0;
			}
			PARM.TYN = 0;
			PARM.TYN1 = 0;
			PARM.TYP = 0;
			//PARM.TYW = 0;     
			foreach (int x in PARM.TYW){
				PARM.TYW[x] = 0.0;
			}
			//PARM.U = 0;
			foreach (int x in PARM.U){
				PARM.U[x] = 0.0;
			}
			//PARM.U10MX = 0;
			foreach (int x in PARM.U10MX){
				PARM.U10MX[x] = 0.0;
			}
			//PARM.UK1 = 0;
			foreach (int x in PARM.UK1){
				PARM.UK1[x] = 0.0;
			}
			//PARM.UN1 = 0;
			foreach (int x in PARM.UN1){
				PARM.UN1[x] = 0.0;
			}
			//PARM.UP1 = 0;      
			foreach (int x in PARM.UP1){
				PARM.UP1[x] = 0.0;
			}
			PARM.VALF1 = 0;
			PARM.VAP = 0;
			//PARM.VARP = 0;
			foreach (int x in PARM.VARP){
				foreach (int y in PARM.VARP){
					PARM.VARP[x, y] = 0.0;
				}
			}
			//PARM.VFC = 0;
			foreach (int x in PARM.VFC){
				PARM.VFC[x] = 0.0;
			}
			PARM.VIRT = 0;	
			//PARM.VIL = 0;
			foreach (int x in PARM.VIL){
				foreach (int y in PARM.VIL){
					PARM.VIL[x, y] = 0.0;
				}
			}
			//PARM.VIR = 0;
			foreach (int x in PARM.VIR){
				foreach (int y in PARM.VIR){
					PARM.VIR[x, y] = 0.0;
				}
			}
			//PARM.VQ = 0;
			foreach (int x in PARM.VQ){
				PARM.VQ[x] = 0.0;
			}
			//PARM.VWC = 0;
			foreach (int x in PARM.VWC){
				PARM.VWC[x] = 0.0;
			}
			//PARM.VY = 0;
			foreach (int x in PARM.VY){
				PARM.VY[x] = 0.0;
			}
			//PARM.W = 0;
			foreach (int x in PARM.W){
				PARM.W[x] = 0.0;
			}
			//PARM.WBMC = 0;
			foreach (int x in PARM.WBMC){
				PARM.WBMC[x] = 0.0;
			}
			//PARM.WLV = 0;
			foreach (int x in PARM.WLV){
				PARM.WLV[x] = 0.0;
			}
			//PARM.WN2O = 0;
			foreach (int x in PARM.WN2O){
				PARM.WN2O[x] = 0.0;
			}
			//PARM.WNO2 = 0;
			foreach (int x in PARM.WNO2){
				PARM.WNO2[x] = 0.0;
			}
			//PARM.WNO3 = 0;
			foreach (int x in PARM.WNO3){
				PARM.WNO3[x] = 0.0;
			}
			PARM.WS = 1;
			PARM.WTN = 0;
			//PARM.XIM = 0;
			foreach (int x in PARM.XIM){
				PARM.XIM[x] = 0.0;
			}
			//PARM.XMTU = 0;
			foreach (int x in PARM.XMTU){
				PARM.XMTU[x] = 0.0;
			}
			PARM.YEW = 0;
			PARM.YLC = 0;
			//PARM.YLCF = 0;
			foreach (int x in PARM.YLCF){
				foreach (int y in PARM.YLCF){
					PARM.YLCF[x, y] = 0.0;
				}
			}
			//PARM.YLD = 0;
			foreach (int x in PARM.YLD){
				PARM.YLD[x] = 0.0;
			}
			//PARM.YLD1 = 0;
			foreach (int x in PARM.YLD1){
				foreach (int y in PARM.YLD1){
					PARM.YLD1[x, y] = 0.0;
				}
			}
			//PARM.YLD2 = 0;
			foreach (int x in PARM.YLD2){
				foreach (int y in PARM.YLD2){
					PARM.YLD2[x, y] = 0.0;
				}
			}
			PARM.YLK = 0;
			//PARM.YLKF = 0;
			foreach (int x in PARM.YLKF){
				foreach (int y in PARM.YLKF){
					PARM.YLKF[x, y] = 0.0;
				}
			}
			PARM.YLN = 0;
			//PARM.YLNF = 0;
			foreach (int x in PARM.YLNF){
				foreach (int y in PARM.YLNF){
					PARM.YLNF[x, y] = 0.0;
				}
			}
			PARM.YLP = 0;
			//PARM.YLPF = 0;  
			foreach (int x in PARM.YLPF){
				foreach (int y in PARM.YLPF){
					PARM.YLPF[x, y] = 0.0;
				}
			}
			PARM.ZBMC = 0;
			PARM.ZBMN = 0;
			PARM.ZHPC = 0;
			PARM.ZHPN = 0;
			PARM.ZHSC = 0;
			PARM.ZHSN = 0;
			PARM.ZLM = 0;
			PARM.ZLMC = 0;
			PARM.ZLMN = 0;
			PARM.ZLS = 0;
			PARM.ZLSC = 0;
			PARM.ZLSL = 0;
			PARM.ZLSLC = 0;
			PARM.ZLSN = 0;
			PARM.ZLSLNC = 0;

		}
	}
}