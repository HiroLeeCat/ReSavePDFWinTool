using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReSavePDFWinTool
{
	static class Program
	{
		/// <summary>
		/// 應用程式的主要進入點。
		/// </summary>
		[STAThread]
        static void Main(string[] args)
        {
            if(args.Length>1)
            {
            	//tCommon.IsRunBat=true;
                tCommon.RunType = args[0];
                tCommon.FolderOrFilesPath = args[1];
                if(args.Length>2)
	                tCommon.IsHaveSub = (args[2]=="Y" || args[2].ToUpper()=="TRUE");
                MDIForm1 objClass = new MDIForm1();
                objClass.RunBatResavePDF();
            }
            else
            {
	            Application.EnableVisualStyles();
	            Application.SetCompatibleTextRenderingDefault(false);
	            Application.Run(new MDIForm1());
	        }
        }
    }
}