using System;

namespace Epic
{
	public class ATIMER
	{
		public ATIMER (int ITR)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program sets date and time for output and
			// calculates elapsed time
			
			// This file is a mess...not done
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			if (ITR == 0){
				// These calls in fortran might set the parameters, and then they are used as
				// variables in this project. I haven't found any documentation to support this
				// other than the clues later on in the file when they use them. Still don't know
				// if that means they are global varriables or not
				
				// CALL GETDAT(IYER, IMON, IDAY)
				// I have no idea what IYER, IMON, and IDAY is...are they global variables?? >_<
				// Are IYER, IMON, IDAY being set to Year, Month, Day?
				DateTime today = DateTime.Today;
				// CALL GETTIM(IT1, IT2, IT3, I100)
				// There is a lack of documentation as to what GETTIM really even does,
				// so I just guessed on the format here.....
				// I also have no idea what IT1....ect are either...again...global variables
				string time = DateTime.Now.ToString("HH:mm:ss tt");
				return;
			}
			DateTime today_2 = DateTime.Today;
			string time_2 = DateTime.Now.ToString("HH:mm:ss tt");
			// More global variables....
			//Functions.ALPYR(PARM.IY, PARM.NYD, PARM.LPYR);
			//Functions.ADAJ(PARM.NC, PARM.IEDT, PARM.MO, PARM.IDA, NYD);
			//Functions.ALPYR(IYER, NYD, LPYR);
			//Functions.ADAJ(NC, IBDX, IMON, IDAY, NYD);
			
			//double I1 = 86400*((PARM.IY-PARM.IYER)*(366-PARM.NYD)+PARM.IEDT-PARM.IBDX);
			double IBT = PARM.IT1*3600+PARM.IT2*60+PARM.IT3;
			double IEX = 0;//PARM.IEH*3600+IEM*60+IES;
			
			// Write....?
			double II = IEX-IBT; //I1+IEX-IBT;
			double ITS = II%60;
			II = (II-ITS)/60;
			double ITM = II%60;
			double ITH = (II-ITM)/60;
			// Write...
			return;
			// Format....
		}
	}
}

