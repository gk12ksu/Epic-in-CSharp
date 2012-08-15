using System;

namespace Epic
{
	public class ARESET
	{
		public int[] init_int_arr(int[] arr){
			foreach (int x in arr){
				arr[x] = 0;
			}
			return arr;
		}
		
		public double[,,] init_doub3_arr(double[,,] arr){
			foreach (int x in arr){
				foreach (int y in arr){
					foreach (int z in arr){
						arr[x, y, z] = 0.0;
					}
				}
			}
			return arr;
		}
		
		public double[] init_doub_arr(double[] arr){
			foreach (int x in arr){
				arr[x] = 0.0;	
			}
			return arr;
		}
		
		public double[,] init_doub2_arr(double[,] arr){
			foreach (int x in arr){
				foreach (int y in arr){
					arr[x, y] = 0.0;	
				}
			}
			return arr;
		}
		
		public ARESET ()
		{
			// EPICv0810
			// Translated by Brian Cain
			// It seems as though this program just resets all the global variables?
			// Don't know if this will be needed in the future
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			PARM.IDRL = 0;
			//PARM.IHRL = 0;
			PARM.IHRL = init_int_arr(PARM.IHRL);
			//PARM.JE = PARM.MNC+1;
			foreach (int x in PARM.JE){
				PARM.JE[x] = PARM.MNC+1;
			}
			PARM.JPL = init_int_arr(PARM.JPL);
			PARM.IRL = 0;
			Functions.ADAJ(PARM.NC,PARM.IBD,PARM.IMO0,PARM.IDA0,PARM.NYD);
			PARM.JCN = 0;
			PARM.JCN0 = 0;
			PARM.JCN1 = 0;
			PARM.LW = 1;
			PARM.MO = PARM.IMO0;
			PARM.MO1 = PARM.MO;
			PARM.NCR = init_int_arr(PARM.NCR);
			PARM.NQP = 0;
			PARM.NQP0 = 0;
			PARM.NQP1 = 0;
			PARM.NWDA = 0;
			PARM.NWD0 = 0;
			PARM.NYLN = init_int_arr(PARM.NYLN);
			PARM.APQ = init_doub3_arr(PARM.APQ);
			PARM.APY = init_doub3_arr(PARM.APQ);
			PARM.AQB = init_doub3_arr(PARM.APQ);
			PARM.ASW = init_doub_arr(PARM.ASW);
			PARM.AYB = init_doub3_arr(PARM.AYB);
			PARM.CST1 = 0;
			//PARM.CX = 1*Math.Pow (10, -10);
			foreach (int x in PARM.CX){
				PARM.CX[x] = Math.Pow (10, -10);
			}
			PARM.CYAV = 0;
			PARM.CYSD = 0;
			PARM.CYMX = 0;
			PARM.EP = init_doub_arr(PARM.EP);
			PARM.PRAV = 0;
			PARM.PRB = 0;
			PARM.PRSD = 0;
			PARM.PVQ = init_doub2_arr(PARM.PVQ);
			PARM.PVY = init_doub2_arr(PARM.PVY);
			PARM.QIN = init_doub_arr(PARM.QIN);
			PARM.QPQB = 0;
			PARM.QPS = 0;
			PARM.RCF = 1;
			PARM.RCM = init_doub_arr(PARM.RCM);
			PARM.REG = init_doub_arr(PARM.REG);
			PARM.RSY = init_doub_arr(PARM.RSY);
			PARM.SET = init_doub_arr(PARM.SET);
			PARM.SFMO = init_doub2_arr(PARM.SFMO);
			PARM.SM = init_doub_arr(PARM.SM);
			PARM.SMAP = init_doub2_arr(PARM.SMAP);
			PARM.SMM = init_doub2_arr(PARM.SMM);
			PARM.SMMP = init_doub3_arr(PARM.SMMP);
			PARM.SMY = init_doub_arr(PARM.SMY);
			PARM.SMYP = init_doub2_arr(PARM.SMYP);
			PARM.SPQ = init_doub2_arr(PARM.SPQ);
			PARM.SPY = init_doub2_arr(PARM.SPY);
			PARM.SQB = init_doub_arr(PARM.SQB);
			PARM.SRD = init_doub_arr(PARM.SRD);
			PARM.STDA = init_doub2_arr(PARM.STDA);
			PARM.SYB = init_doub_arr(PARM.SYB);
			PARM.TAL = init_doub_arr(PARM.TAL);
			PARM.TAMX = init_doub_arr(PARM.TAMX);
			PARM.TCAV = 0;
			PARM.TCAW = init_doub_arr(PARM.TCAW);
			PARM.TCMN = 1*Math.Pow (10, 20);
			PARM.TCMX = 0;
			PARM.TCQV = init_doub_arr(PARM.TCQV);
			PARM.TCRF = init_doub_arr(PARM.TCRF);
			PARM.TDM = init_doub_arr(PARM.TDM);
			PARM.TEI = init_doub_arr(PARM.TEI);
			PARM.TET = init_doub_arr(PARM.TET);
			PARM.TETG = init_doub_arr(PARM.TETG);
			PARM.TFTN = init_doub_arr(PARM.TFTN);
			PARM.TFTP = init_doub_arr(PARM.TFTP);
			PARM.THU = init_doub_arr(PARM.THU);
			PARM.TQ = init_doub_arr(PARM.TQ);
			PARM.TR = init_doub_arr(PARM.TR);
			PARM.TRA = init_doub_arr(PARM.TRA);
			PARM.TRD = init_doub_arr(PARM.TRD);
			PARM.TRHT = init_doub_arr(PARM.TRHT);
			PARM.TSFC = init_doub2_arr(PARM.TSFC);
			PARM.TSN = init_doub_arr(PARM.TSN);
			PARM.TSR = init_doub_arr(PARM.TSR);
			PARM.TSTL = init_doub_arr(PARM.TSTL);
			PARM.TSY = init_doub_arr(PARM.TSY);
			PARM.TUN1 = init_doub_arr(PARM.TUN1);
			PARM.TVIR = init_doub_arr(PARM.TVIR);
			PARM.TXMX = init_doub_arr(PARM.TXMX);
			PARM.TXMN = init_doub_arr(PARM.TXMN);
			PARM.TYC = 0;
			PARM.TYK = 0;
			PARM.TYL1 = init_doub_arr(PARM.TYL1);
			PARM.TYL2 = init_doub_arr(PARM.TYL2);
			PARM.TYLC = init_doub_arr(PARM.TYLC);
			PARM.TYLK = init_doub_arr(PARM.TYLK);
			PARM.TYLN = init_doub_arr(PARM.TYLN);
			PARM.TYLP = init_doub_arr(PARM.TYLP);
			PARM.TYN = 0;
			PARM.TYP = 0;
			PARM.TYW = init_doub_arr(PARM.TYW);
			PARM.U10MX = init_doub_arr(PARM.U10MX);
			PARM.VALF1 = 0;
			PARM.VARP = init_doub2_arr(PARM.VARP);
			PARM.W = init_doub_arr(PARM.W);
			PARM.XIM = init_doub_arr(PARM.XIM);
			PARM.YLC = init_doub_arr(PARM.YLC);
			PARM.YLK = 0;
			PARM.YLN = 0;
			PARM.YLP = 0;
			int j;
			for (j = 0; j <= 21; j++){
				PARM.IX[j] = PARM.IX0[j];
				PARM.IDG[j] = j;
			}
			return;
		}
	}
}