using System;

namespace Epic
{
	public class SPLA
	{
		public SPLA (ref int I, ref int I1, ref int K, ref int L, ref double RTO)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program has no description
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            if (L>0) PARM.Z[K] = (PARM.Z[I]+PARM.Z[I1])*.5;
            PARM.PSP[K] = PARM.PSP[I];
            PARM.BD[K] = PARM.BD[I];
            PARM.BDM[K] = PARM.BDM[I];
            PARM.CLA[K] = PARM.CLA[I];
            PARM.SIL[K] = PARM.SIL[I];
            PARM.SAN[K] = PARM.SAN[I];
            PARM.ROK[K] = PARM.ROK[I];
            PARM.SATC[K] = PARM.SATC[I];
            PARM.HCL[K] = PARM.HCL[I];
            PARM.STFR[K] = PARM.STFR[I];
            PARM.PH[K] = PARM.PH[I];
            PARM.STMP[K] = PARM.STMP[I];
            PARM.BDD[K] = PARM.BDD[I];
            PARM.BPC[K] = PARM.BPC[I];
            PARM.BDP[K] = PARM.BDP[I];
            PARM.SMB[K] = PARM.SMB[I];
            PARM.CEC[K] = PARM.CEC[I];
            PARM.CGO2[K] = PARM.CGO2[I];
            PARM.CGCO2[K] = PARM.CGCO2[I];
            PARM.CGN2O[K] = PARM.CGN2O[I];
            if (PARM.ISW == 1 || PARM.ISW == 3 || PARM.ISW == 5) PARM.CEM[K] = PARM.CEM[I];
            PARM.CAC[K] = PARM.CAC[I];
            PARM.ALS[K] = PARM.ALS[I];
            PARM.ECND[K] = PARM.ECND[I];
//            PARM.WT[K] = Functions.EAJL(PARM.WT[I],RTO);
//            PARM.WNO3[K] = Functions.EAJL(PARM.WNO3[I],RTO);
//            PARM.WP[K] = Functions.EAJL(PARM.WP[I],RTO);
//            PARM.WHPN[K] = Functions.EAJL(PARM.WHPN[I],RTO);
//            PARM.WHSN[K] = Functions.EAJL(PARM.WHSN[I],RTO);
//            PARM.WBMN[K] = Functions.EAJL(PARM.WBMN[I],RTO);
//            PARM.WLSN[K] = Functions.EAJL(PARM.WLSN[I],RTO);
//            PARM.WLMN[K] = Functions.EAJL(PARM.WLMN[I],RTO);
//            PARM.WHPC[K] = Functions.EAJL(PARM.WHPC[I],RTO);
//            PARM.WHSC[K] = Functions.EAJL(PARM.WHSC[I],RTO);
//            PARM.WBMC[K] = Functions.EAJL(PARM.WBMC[I],RTO);
//            PARM.WLS[K] = Functions.EAJL(PARM.WLS[I],RTO);
//            PARM.WLM[K] = Functions.EAJL(PARM.WLM[I],RTO);
//            PARM.WLSL[K] = Functions.EAJL(PARM.WLSL[I],RTO);
            PARM.WLSC[K] = .42*PARM.WLS[K];
            PARM.WLMC[K] = .42*PARM.WLM[K];
            PARM.WLSLC[K] = .42*PARM.WLSL[K];
            PARM.WLSLNC[K] = PARM.WLSC[K]-PARM.WLSLC[K];
            PARM.RSD[K] = .001*(PARM.WLS[K]+PARM.WLM[K]);
            PARM.WLSC[I] = .42*PARM.WLS[I];
            PARM.WLMC[I] = .42*PARM.WLM[I];
            PARM.WLSLC[I] = .42*PARM.WLSL[I];
            PARM.WLSLNC[I] = PARM.WLSC[I]-PARM.WLSLC[I];
            PARM.RSD[I] = .001*(PARM.WLS[I]+PARM.WLM[I]);
            PARM.WOC[I] = PARM.WBMC[I]+PARM.WHPC[I]+PARM.WHSC[I]+PARM.WLMC[I]+PARM.WLSC[I];
            PARM.WOC[K] = PARM.WBMC[K]+PARM.WHPC[K]+PARM.WHSC[K]+PARM.WLMC[K]+PARM.WLSC[K];
            PARM.WON[I] = PARM.WBMN[I]+PARM.WHPN[I]+PARM.WHSN[I]+PARM.WLMN[I]+PARM.WLSN[I];
            PARM.WON[K] = PARM.WBMN[K]+PARM.WHPN[K]+PARM.WHSN[K]+PARM.WLMN[K]+PARM.WLSN[K];
//            PARM.AP[K] = Functions.EAJL(PARM.AP[I],RTO);
//            PARM.PMN[K] = Functions.EAJL(PARM.PMN[I],RTO);
//            PARM.FOP[K] = Functions.EAJL(PARM.FOP[I],RTO);
//            PARM.OP[K] = Functions.EAJL(PARM.OP[I],RTO);
//            PARM.SOLK[K] = Functions.EAJL(PARM.SOLK[I],RTO);
//            PARM.EXCK[K] = Functions.EAJL(PARM.EXCK[I],RTO);
//            PARM.FIXK[K] = Functions.EAJL(PARM.FIXK[I],RTO);
//            PARM.EQKS[K] = Functions.EAJL(PARM.EQKS[I],RTO);
//            PARM.EQKE[K] = Functions.EAJL(PARM.EQKE[I],RTO);
//            PARM.WSLT[K] = Functions.EAJL(PARM.WSLT[I],RTO);
//            PARM.S15[K] = Functions.EAJL(PARM.S15[I],RTO);
//            PARM.FC[K] = Functions.EAJL(PARM.FC[I],RTO);
//            PARM.PO[K] = Functions.EAJL(PARM.PO[I],RTO);
//            PARM.ST[K] = Functions.EAJL(PARM.ST[I],RTO);
            return;
		}
	}
}

