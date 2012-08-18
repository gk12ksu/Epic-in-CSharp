using System.IO;
namespace Epic
{
    public class MODPARAM
    {
        /*
         * MODPARAM holds variables to be declared and used throughout Epic. 
         * Using the Singleton Pattern to pass the MODPARAM class to functions in place of Fortran's "Use" and "Module" commands.
		 * Last modified by Brian Dye, 6/30/2012
         */

        private static MODPARAM instance;

        private MODPARAM() { }

        public static MODPARAM Instance
        {
            get
            {
                if (instance == null)
                    instance = new MODPARAM();
                return instance;
            }
        }

        //CHARACTER(4)::TITLE(60),HEDS(30),HEDC(20),SID(16),HEDP(10) //Original declarations of some char arrays saved for error checking
        public char[,] TITLE = new char[60, 4], HEDS = new char[30, 4], HEDC = new char[20, 4], SID = new char[16, 4], HEDP = new char[10, 4];
        //public char[] ASTN = new char[8];
        public string ASTN;
        public char[] SITEFILE = new char[20], SOILFILE = new char[20];
        public string OPSCFILE;
        //public char[] FWTH = new char[80];
        public string FWTH;
        //CHARACTER(4),DIMENSION(:),ALLOCATABLE::CPNM,HED      
        //public char[,] CPNM;
        public string[] CPNM;
        public string[] HED = new string[98];
        //CHARACTER(8),DIMENSION(:),ALLOCATABLE::FTNM,TIL                                
        public char[,] FTNM, TIL;
        //CHARACTER(16),DIMENSION(:),ALLOCATABLE::PSTN  
        public char[,] PSTN;

        public int IAC, IAUF, IAUI, IAUL, IAZM, IBAT, IBD, IBDT, ICCD, ICDP, ICF, ICG, ICO2, ICOR0, ICV, IDA, IDAY, IDA0, IDF0, IDFP, IDN, IDR, IDRL, IDS, IDSP, IERT, IET, IFA, IFD, IGO, IGN, IGSD, IGS0, IGZ, IHUS, IHV, III, IMON, IMO0, IMW;
        public int INFL, INP, IOX, IPAT, IPD, IPL, IPST, IPY, IPYI, IRI, IRL, IRO, IRO0, IRR, IRUN, IRTC, ISAT, ISCN, ISG, ISL, ISTA, ISTP, ISW, ISX, ITYP, IT1, IT2, IT3, IUN, IWP5, IWTH, IY, IYER, IYR, IYR0, IYX, JCN, JCN0, JCN1, JD, JDA, JDHU, JJK, JT1, JT2, KC, KDA, KF, KHV, KI, KP, KP1, KT;
        public int KTT, K21, LBP, LC, LD1, LMS, LPYR, LRD, LUN, LW, M21, MASP, MFT, MNC, MNT, MNU, MO, MO1, MOFX, MPS, MRO, MSL, MSO, MXT, MYR, NBCL, NBDT, NBSL, NBYR, ND, NDF, NDP, NDRV, NDT, NDVSS, NEV, NFA, NFS, NGF, NGN, NGN0, NII, NJC, NKA, NKD, NKS, NKYA, NMW, NNE, NOFT, NOP, NQP, NQP0, NQP1, NRO, NSM, NSNN, NSTP, NSX, NUDK, NUPC, NVCN, NWDA, NWD0, NWI, NYD;

        public int[] NX = new int[43000], KDF1 = new int[8000], KDP1 = new int[8000], KDC1 = new int[800], IGY = new int[150], KDT2 = new int[100], KA = new int[100], IFS = new int[40], KD = new int[40], KYA = new int[40], KS = new int[40];
        public int[] NHC = new int[26], IDG = new int[21], IX = new int[21], IX0 = new int[21], NC = new int[13], IHRL = new int[12], IWIX = new int[12], IDC = new int[10], NDC = new int[10], IYS = new int[8], JX = new int[7], ISIX = new int[6], KGN = new int[5], JC = new int[4], IDFT = new int[2];
        //public int[] KW = new int[200], KR = new int[30];
        public FileStream[] KW = new FileStream[200], KR = new FileStream[30];

        public int[] IHU, IYH, JE, JP, JPL, KDC, KFL, KG, KOMP, LID, LORG, NBC, NBE, NBT, NCP, NCR, NHU, NHV, NPST, NTL, NYLN, ICUS, IHC, IHT, KDF, KDP;
        public int[,] ITL, LFT, LT, LYR, NGZ, LPC, JH, LY, NBCX, IGMD, IHVD, IPLD, NPSF;

        public double ABD, ACW, ADRF, AFN, AFLG, AGP, AGPM, AHSM, AILG, ALB, ALG, ALTC, AL5, AMSR, ANG, APB, APBC, APMU, AQV, ARF, ARMN, ARMX, AVT, AWC, BARF, BCV, BETA, BFT, BIG, BIR, BTK, BTN, BTNX, BTP, CAP, CDG, CEJ, CFE0, CFEM, CFMN, CFNP, CHL, CLF, CLG, CLT, CMN, CN, CNO3I, CN0, CN1, CN2, CN3, COIR, COST, COL, CON, COP, COS1, COWW, CO2, CRKF, CSFX, CSLT, CSO1, CST1, CV, CVF, CVP, CVRS, CYAV, CYMX, CYSD, DALG, DARF, DCFE, DD, DFCO2, DFO2, DFX, DHT, DKIN, DKHL, DN2O, DPLC, DR, DST0, DTG, DUR, DV, DZ, EAO2, ED, EFI, EI, EK, ELEV, EO, ER, ES, EXNN, EXPK, FCSW, FCV, FDSF, FFED, FGC, FGIS, FL, FMX, FNP, FNPI, FULP, FW, GFCO2, GFN2O, GFO2, GMA, GSEF, GSEP, GSVF, GSVP, GWMX, GWST, GX, GZLM, HCLD, HCLN, HLMN, HMN, HRLT, HR0, HR1, HSM, OCPD, PALB, PAW, PB, PDSW, PEC, PIT, PI2, PLCX, PMOEO, PMORF, PMX, PR;
        public double PRAV, PRB, PRSD, PRWM, PSTS, PSTX, QAP, QD, QNO3, QP, QPQB, QPR, QPS, QVOL, RAMX, RCF, RCN, REK, REP, RFNC, RFSD, RFSK, RFSM, RFTT, RFV, RFVM, RGIN, RGRF, RGSM, RHD, RHM, RHTT, RLF, RLSF, RM, RMNR, RNO3, ROSP, RPR, RRF, RRUF, RSF, RSK, RSLK, RUNT, RWO, RZ, RZSW, SALB, SARF, SAT, SATK, SCI, SCN, SDN, SEP, SGMN, SHM, SHRL, SIM, SIN1, SIP, SK, SL, SLR, SLRY, SLT0, SMER, SML, SMP, SMR, SMX, SN, SN2, SN2O, SNA15, SNA30, SNN15, SNN30, SNIT, SNMN, SNO, SNOF, SNPKT, SP, SQ, SRA, SRAD, SRAF, SRAM, SSFK, SSFN, SSLT, SSST, SST, SSW, STDK, STDL, STDO, STDOK, STDON, STDOP, STDP, STD0, ST0, SU, SUK, SUN, SUP, SUT, SVOL, SW, SW15, SW30, SWRZ, SX, S3, TA, TAP, TC, TCAV, TCC, TCMN, TCMX, TCS, TEK, TFK, TFNH3, TFNO, TFNO3, TFOP, TFPL, TFPO, TH, THK, TLA, TLGE, TLGQ;
        public double TLGW, TLMF, TMN, TMNM, TMP, TMX, TMXM, TNH3, TNO2, TNO3, TNOR, TNSD, TOC, TOP, TP, TRSD, TRSP, TSFK, TSFN, TSFS, TSK, TSLT, TSNO, TSRZ, TWN, TWN0, TX, TXSD, TXXM, TYC, TYK, TYN, TYN1, TYP, UB1, UNO3, UNT, UOB, UPK, UPP, UPS, UPSL, UPSQ, UPSX, USL, UST, USTRT, USTT, USTW, UX, UXP, U10, VAC, VALF1, VALF2, VAP, VF, VIMX, VIRT, VLG, VLGI, VLGM, VLGN, VMN, VPD, VSK, VSLT, V1, V3, WAGE, WB, WCF, WCYD, WDN, WDRM, WFX, WIM, WIP, WK, WMP, WS, WSA, WSA1, WTBL, WTMN, WTMU, WTMX, WTN, BRNG, XHSM, XNS, YERO, YEW, YLAT, YLK, YLN, YLP, YLTC, YLTS, YN, YP, YSLT, YSW, YTN, YTN1, YW, XLOG, ZBMC, ZBMN, ZF, ZHPC, ZHPN, ZHSC, ZHSN, ZLM, ZLMC, ZLMN, ZLS, ZLSC, ZLSL, ZLSLC, ZLSLNC, ZLSN, ZQT;

        public double[] VQ = new double[90], VY = new double[90], PRMT = new double[79], VARS = new double[30], ASW = new double[12], CX = new double[12], QIN = new double[12], RCM = new double[12], RNCF = new double[12], SET = new double[12], RSY = new double[12], SRD = new double[12], SRMX = new double[12], TAL = new double[12], TAMX = new double[12], TAV = new double[12], TEI = new double[12], TET = new double[12], THRL = new double[12], TMNF = new double[12], TMXF = new double[12], TQ = new double[12], TR = new double[12], TRHT = new double[12], TSN = new double[12], TSR = new double[12], TSTL = new double[12], TSY = new double[12], TXMN = new double[12], TXMX = new double[12], TYW = new double[12], U10MX = new double[12], UAVM = new double[12], W = new double[12], OPV = new double[9], SMGS = new double[8], YSD = new double[8], PSZ = new double[5], SQB = new double[5], SYB = new double[5], BUS = new double[4], RUSM = new double[3], WX = new double[3], XIM = new double[3], WCS = new double[3], XAV = new double[3], XDV = new double[3], XRG = new double[3], ZCS = new double[3];

        public double[,] XSP = new double[43000, 5], TDAC = new double[24, 30], TDAN = new double[24, 30], TDAO = new double[24, 30], STV = new double[30, 12], DIR = new double[12, 16], SCRP = new double[30, 2], OBMX = new double[6, 12], OBMN = new double[6, 12], OBSL = new double[6, 12], PCF = new double[6, 12], RH = new double[6, 12], RMO = new double[6, 12], SDTMN = new double[6, 12], SDTMX = new double[6, 12], WFT = new double[6, 12], WI = new double[6, 12];

        public double[, ,] CQP = new double[8, 17, 4], RST = new double[3, 6, 12], PRW = new double[2, 6, 12];

        public double[] ACO2C, AFP, AN2OC, AO2C, CGCO2, CGN2O, CGO2, CLCO2, CLN2O, CLO2, DCO2GEN, DHN, DN2G, DN2OG, DO2CONS, FC, HKPC, HKPN, HKPO, QCO2, RSPC, S15, SOT, TPOR, VFC, VWC, WBMC, WN2O, WNO2, WNO3, XN2O, ZC;
        public double[] ALS, AP, BD, BDD, BDM, BDP, BPC, CAC, CEC, CEM, CLA, CNDS, CN3F, CPRH, CPRV, DRWX, ECND, EQKE, EQKS, EXCK, FIXK, FOP, HCL, HK, OBC, PH, OP, PKRZ, PMN, PO, PSP, RNMN, ROK, RSD, RWTZ, SAN, SATC, SEV, SIL, SMB, SOLK, SSF, SSO3, ST, STD, STDN, STFR, STMP, U, UK, UN, UP, WBMN, WHPC, WHPN, WHSC, WHSN, WLM, WLMC, WLMN, WLS, WLSC, WLSL, WLSLC, WLSLNC, WLSN, WNH3, WOC, WON, WP, WSLT, WT, Z;
        public double[] ANA, CAF, CKY, CNY, CPY, CSTS, FNMX, HUF, PST, PSTM, RDF, SDW, TOPC, TFTN, TFTP, WCY;
        public double[] ACET, AJHI, AJWA, ALT, BLYN, CCEM, CHT, DDM, DLAI, DM, DMLA, DMLX, DM1, EP, FLT, FTO, GMHU, GSI, HI, HMX, HU, HUI, PPL0, PRYG, PRYF, PSTF, RBMD, RD, RDMX, REG, RLAD, RW, RWX, SLAI, SMSL, STL, SWH, SWP, TCAW, TCQV, TCRF, TCSO, TCST, TDM;
        public double[] TETG, TFTK, TBSC, THU, TIRL, TRA, TRD, TVAL, TVIR, TYLC, TYLK, TYLN, TYLP, TYL1, TYL2, UK1, UK2, ULYN, UNA, UN1, UP1, UN2, UP2, VPD2, VPTH, WA, WAVP, WLV, WSYF, WUB, XDLA0, XDLAI, XLAI, XMTU, YLD;
        public double[] COOP, COTL, DKH, DKI, EFM, EMX, FCEM, FCST, FK, FN, FNH3, FNO, FOC, FP, FPO, FPOP, FRCP, FSLT, FULU, HE, HMO, ORHI, PCEM, PCST, PFOL, PHLF, PHLS, PKOC, PLCH, PSOL, PWOF, RHT, RIN, SSPS, TCEM, TLD, RR, SM, SMY, VAR;
        public double[,] PPCF, PPLP, PSTE, PSTR, PVQ, PVY, RSTK, SMM, PHU, POP, PPLA, PSTZ, RWF, RWPC, TPAC, SMAP, SMYP, VARP, SMS, SPQ, SPQC, SPY, SOIL, STDA, BK, BN, BP, BLG, BWD;
        public double[,] VARC, RWT, RWTX, FRTK, FRTN, FRTP, TPSF, CAW, CQV, CRF, CSOF, CSTF, DLAP, DMF, ETG, FRST, HIF, VIL, VIR, WAC2, YLCF, YLD1, YLD2, YLKF, YLNF, YLPF, SOL, STX, TSFC, CGSF, SFMO;
        public double[,] CFRT, CND, HUSC, HWC, QIR, TIR, VIRR, WFA;
        public double[, ,] APQ, APQC, APY, AQB, AYB, SMMP, SMMC, SF; 
    }
}
