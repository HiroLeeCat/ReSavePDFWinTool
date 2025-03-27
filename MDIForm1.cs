using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
namespace ReSavePDFWinTool
{
    public partial class MDIForm1 : Form
	{
        #region 全域變數
        string gMsg = string.Empty;
        //2025/03/06;1.0.0.1;僅檢查最近7天內新增/修改的pdf檔
        int gLastModifiedDays = 0;
        #endregion 全域變數
                
        public MDIForm1()
		{
			InitializeComponent();
            this.Text = string.Format("檢查異常PDF讀取失敗，則嘗試將PDF執行另存 工具 (Ver:{0})", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

            //2025/03/06;1.0.0.1;僅檢查最近7天內新增/修改的pdf檔↓
            //過濾新增/修改幾天內的檔案
            string strParameterKey = "LastModifiedDays";
            string strParameterValue = "";//從config取得參數值
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                //獲取appSettings節點
                AppSettingsSection app = (AppSettingsSection)config.GetSection("appSettings");
                strParameterValue = app.Settings[strParameterKey].Value;
            }
            catch
            {
                //找不到系統參數就預設回傳空白
                strParameterValue = "";
            }
            if (strParameterValue.Trim().Length > 0)
            {
                int.TryParse(strParameterValue, out gLastModifiedDays);
            }
            //2025/03/06;1.0.0.1;僅檢查最近7天內新增/修改的pdf檔↑
        }

        #region 點選執行檢查
        private void btnReSavePDF_Click(object sender, EventArgs e)
		{
            //進度列預設大小為0
            txtResult.Text = "";
            prsBar.Minimum = 0;
            prsBar.Maximum = 0;
            prsBar.Value = 0;
            
            if (rbnFolder.Checked) {
                #region 選擇資料夾
                //檢查目的資料夾
            	gMsg = "資料夾路徑不存在，請重新選擇路徑";
                if (tbxFolder.Text == "")
                {
                    txtResult.Text += gMsg+"\r\n";
                    MessageBox.Show(gMsg);
                    return;
                }

                DirectoryInfo directoryinfo = new DirectoryInfo(tbxFolder.Text);
                if (!directoryinfo.Exists)
                {
                    MessageBox.Show(gMsg);
                    return;
                }

				tCommon.RunType="P";
				tCommon.IsHaveSub = cbxContainSubFolders.Checked;
                CheckPDFInFolder(tbxFolder.Text);
                #endregion 選擇資料夾
            }
            else if (rbnFiles.Checked) {
				tCommon.RunType="F";
                #region 選擇pdf檔案
                //檢查pdf檔案
                if (tbxFiles.Text == "")
                {
            		gMsg = "pdf檔案不存在，請重新選擇pdf檔案";
                    txtResult.Text += gMsg+"\r\n";
                    MessageBox.Show(gMsg);
                    return;
                }
				else {
					string[] aryMultiFile = tbxFiles.Text.Split('△');
                    prsBar.Minimum = 0;
                    prsBar.Maximum = aryMultiFile.Length;
                    prsBar.Value = 0;
                    foreach (string tFilePath in aryMultiFile)
					{
                        //txtResult.Text += gMsg;
                        
                        if (tFilePath == "")
                        {
                            ++prsBar.Value;
                            continue;
                        }

                        if (!File.Exists(tFilePath))
						{
							string strConditionFileNull = "【" + tFilePath + "】檔案不存在，請重新選擇pdf檔案";
							//MessageBox.Show(strConditionFileNull);
                            //txtResult.Text += strConditionFileNull;
                            //return;
                            ++prsBar.Value;
                            continue;
                        }

                        //CheckPDFInFile(tFilePath);
                        FileInfo file = new FileInfo(tFilePath);
                        CheckPDFInFile(file);

                        ++prsBar.Value;
                        Application.DoEvents();
                    }
                }
                #endregion 選擇pdf檔案
            }
		}
        #endregion 點選執行檢查




        public void CheckPDFInFolder(string pFolderPath)
        {
            //進度列預設大小為0
            prsBar.Minimum = 0;
            prsBar.Maximum = 0;
            prsBar.Value = 0;

            DirectoryInfo directoryinfo = new DirectoryInfo(pFolderPath);
            if (directoryinfo.Exists)
            {
                //if (cbxContainSubFolders.Checked)
                if (tCommon.IsHaveSub)
                {
                    //先處理子資料夾
                    DirectoryInfo[] dirSubs = directoryinfo.GetDirectories();
                    foreach (DirectoryInfo SubDir in dirSubs)
                    {
                        //跳過用來備份原pdf的bakPDF資料夾，裡頭的pdf都是異常檔案，就不用進去檢查了
                        if (SubDir.Name.IndexOf("bakPDF") == 0)
                            continue;
                        string tempPath = System.IO.Path.Combine(pFolderPath, SubDir.Name);
                        CheckPDFInFolder(tempPath);
                    }
                }

                // 取得所有 PDF 檔案
                //string[] files = Directory.GetFiles(pFolderPath, "*.pdf", SearchOption.AllDirectories);

                //FileInfo[] aryFileInfo = directoryinfo.GetFiles();
                FileInfo[] aryFileInfo = directoryinfo.GetFiles();
                int intFileCount = aryFileInfo.Length;
                int intPDFFileNum = 0;
                try
                {
                    for (int i = 0; i < intFileCount; i++)
                    {
                        if (aryFileInfo[i].ToString().ToLower().EndsWith(".pdf"))
                        {
                            ++intPDFFileNum;
                        }
                    }

                    if (intPDFFileNum > 0)
                    {
                        //gMsg = string.Format("檢查資料夾路徑 {0} start\r\n", pFolderPath);
                        gMsg = string.Format("檢查資料夾路徑：{0} \r\n", pFolderPath);
    					WriteLog(gMsg);
                        txtResult.Text += gMsg;

                        prsBar.Minimum = 0;
                        prsBar.Maximum = intPDFFileNum;
                        prsBar.Value = 0;
                        for (int i = 0; i < intFileCount; i++)
                        {
                            if (aryFileInfo[i].ToString().ToLower().EndsWith(".pdf"))
                            {
                                //2025/03/06;1.0.0.1;僅檢查最近7天內新增/修改的pdf檔↓
                                if (gLastModifiedDays != 0)
                                {
                                    DateTime sevenDaysAgo = DateTime.Now.AddDays(-gLastModifiedDays);
                                    // 檢查檔案是否在最近幾天內新增或修改過
                                    if (aryFileInfo[i].CreationTime >= sevenDaysAgo || aryFileInfo[i].LastWriteTime >= sevenDaysAgo)
                                    {
                                        //recentPdfFiles.Add(file);
                                    }
                                    else
                                    {
										gMsg = string.Format("跳過{0}檔 \r\n", aryFileInfo[i].ToString());
										WriteLog(gMsg);
                                        ++prsBar.Value;
                                        continue;
                                    }
                                }
                                //2025/03/06;1.0.0.1;僅檢查最近7天內新增/修改的pdf檔↑

                                CheckPDFInFile(aryFileInfo[i]);
                                ++prsBar.Value;
                            }
                            Application.DoEvents();
                        }





		                //gMsg = string.Format("檢查資料夾路徑 {0} end\r\n", pFolderPath);
		                //txtResult.Text += gMsg;
                    }
                }
                catch (Exception ex)
                {
                    gMsg = "檢查資料夾路徑下的PDF檔發生錯誤：" + ex.Message + "\r\n";
                    txtResult.Text += gMsg;
                    //改成不要跳警示視窗，讓它繼續執行即可
                    //MessageBox.Show(gMsg);
                    WriteLog(gMsg);
                }
            }
        }


        public void CheckPDFInFile(FileInfo pFile)
        {
            //gMsg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") +
            //"↓開始檢查pdf檔案【" + pFile.FullName + "】↓" + "\r\n";
	        //txtResult.Text += gMsg;
            //WriteLog(gMsg);

            gMsg = "檢查pdf檔案【" + pFile.FullName + "】";
            WriteLog(gMsg);

            bool bMustResave = false;
            PdfSharp.Pdf.PdfDocument sharpDoc = new PdfSharp.Pdf.PdfDocument();
            try
            {
            	//將檢查的pdf檔案，取消「唯讀」屬性
                File.SetAttributes(pFile.FullName, File.GetAttributes(pFile.FullName) & ~FileAttributes.ReadOnly);

                //部份pdf出現「Object already in table」，解法：請用adobe reader開啟再另存新檔把附件換掉即可
                sharpDoc = PdfSharp.Pdf.IO.PdfReader.Open(pFile.FullName);//PDF來源檔
                //txtResult.Text += "PdfShart開啟[" + pTargetFilePath + "]成功，該檔案不需另存新檔！\r\n";
            }
            catch (Exception ex)
            {
                gMsg = "使用PdfSharp讀取失敗！filepath=[" + pFile.FullName + "],err=[" + ex.Message + "]\r\n";
                txtResult.Text += gMsg;
                WriteLog(gMsg);
                bMustResave = true;
            }
            finally
            {
                sharpDoc.Close();
                sharpDoc = null;
            }
            //2025/03/12;1.0.0.2;挑選檔案模式，就強制用iText執行另存↓
            //if (bMustResave)
            if (bMustResave || rbnFiles.Checked)
            //2025/03/12;1.0.0.2;挑選檔案模式，就強制用iText執行另存↑
            {
                ReSavePDF(pFile);
            }

            //gMsg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
            //    "↑結束檢查pdf檔案【" + pFile.FullName + "】↑" + "\r\n";
	        //txtResult.Text += gMsg;
            //WriteLog(gMsg);
        }

        private void ReSavePDF(FileInfo pFile)
        {
            string tOriFilePath = pFile.FullName;
            string tBakFilePath = tOriFilePath;
            string tBakFolder = "bakPDF";
            if (!Directory.Exists(pFile.DirectoryName + @"\"+ tBakFolder))
            {
                Directory.CreateDirectory(pFile.DirectoryName + @"\" + tBakFolder);
            }

            if (!File.Exists(pFile.DirectoryName + @"\"+ tBakFolder + "\\" + pFile.Name))
            {
                tBakFilePath = pFile.DirectoryName + @"\" + tBakFolder + "\\" + pFile.Name;
                pFile.MoveTo(tBakFilePath);
            }
            else
            {
                //如備份區已存在相同檔名，則另存備份區
                int iTmp = 1;
                bool bLoop = true;
                while (bLoop)
                {
                    tBakFolder = "bakPDF" + iTmp.ToString();
                    iTmp++;
                    if (!Directory.Exists(pFile.DirectoryName + @"\"+ tBakFolder))
                    {
                        Directory.CreateDirectory(pFile.DirectoryName + @"\" + tBakFolder);
                    }
                    if (!File.Exists(pFile.DirectoryName + @"\"+ tBakFolder + "\\" + pFile.Name))
                    {
                        tBakFilePath = pFile.DirectoryName + "\\" + tBakFolder + "\\" + pFile.Name;
                        pFile.MoveTo(tBakFilePath);
                        bLoop = false;
                    }
                }
            }

            pFile = null;//取消關聯該檔案，避免等下沒辦法重存該檔案
            try
            {
                using (iText.Kernel.Pdf.PdfReader reader = new iText.Kernel.Pdf.PdfReader(tBakFilePath))
                {
                    using (iText.Kernel.Pdf.PdfWriter writer = new iText.Kernel.Pdf.PdfWriter(tOriFilePath))
                    {
                        iText.Kernel.Pdf.PdfDocument itextDoc = new iText.Kernel.Pdf.PdfDocument(reader, writer);
                        itextDoc.Close();
                        txtResult.Text += "改用iText開啟並另存pdf成功！\r\n";
                        WriteLog("改用iText開啟並另存pdf成功！\r\n");
                    }
                }
            }
            catch (Exception ex3)
            {
                txtResult.Text += "改用iText開啟並另存pdf失敗, err=[" + ex3.ToString() + "]\r\n";
                WriteLog("改用iText開啟並另存pdf失敗, err=[" + ex3.ToString() + "]\r\n");
                File.Move(tBakFilePath, tOriFilePath);//將備份的pdf，再搬回來
            }
        }


        #region 選擇資料夾路徑
        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.ShowDialog();
            tbxFolder.Text = fbDialog.SelectedPath;
            gMsg = "資料夾路徑不存在，請重新選擇路徑";
            if (tbxFolder.Text == "")
            {
                //MessageBox.Show(gMsg);
                MessageBox.Show("資料夾路徑不存在，請重新選擇路徑");
                return;
            }
            else
            {
                DirectoryInfo directoryinfo = new DirectoryInfo(tbxFolder.Text);
                if (!directoryinfo.Exists)
                {
                    //MessageBox.Show(gMsg);
                    MessageBox.Show("資料夾路徑不存在，請重新選擇路徑");
                    return;
                }
                else
                {
                    if (!cbxContainSubFolders.Checked) {
                        //如果不包含子路徑，增加檢查該路徑下是否存在pdf檔
                        FileInfo[] aryFileInfo = directoryinfo.GetFiles();
                        int intFileCount = aryFileInfo.Length;
                        int intPDFFileNum = 0;
                        try
                        {
                            for (int i = 0; i < intFileCount; i++)
                            {
                                if (aryFileInfo[i].ToString().ToLower().EndsWith(".pdf"))
                                {
                                    ++intPDFFileNum;
                                }
                            }

                            if (intPDFFileNum == 0)
                            {
                                MessageBox.Show("檔案路徑內不存在PDF檔，請重新選擇路徑");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("選擇檔案路徑出現錯誤：" + ex.Message);
                        }
                    }
                }
            }
        }
        #endregion 選擇資料夾路徑

        #region 選擇檔案來源
        private void btnFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "*.pdf|*.pdf";
            DialogResult dialogResult = ofd.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                tbxFiles.Text = "";
                foreach (String file in ofd.FileNames)
                {
                    if (file == "")
                        continue;
                    tbxFiles.Text += file + "△";
                }
                tbxFiles.Text = tbxFiles.Text.TrimEnd('△');
            }

            if(tbxFiles.Text == "")
            {
                MessageBox.Show("尚未挑選pdf檔案，請重新選擇PDF檔案");
                return;
            }
        }
		#endregion 選擇檔案來源


        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            //自動顯示最後一行
            if (txtResult.Text.Length > 300)
            {
                txtResult.SelectionStart = txtResult.Text.Length;
                txtResult.ScrollToCaret();
            }
        }


        /// <summary>
        /// 寫 log 方法
        /// </summary>
        /// <param name="pLogs">欲寫入的訊息</param>
        private void WriteLog(string pLogs)
        {
            string strLogFileName = "ReSavePDFWinTool_"+ DateTime.Now.ToString("yyyyMMdd") + "Log.log";
            //檢查Log檔案是否已存在
            if (chkFile(strLogFileName))
            {
                using (FileStream fs = new FileStream(Application.StartupPath + @"\Logs\" + strLogFileName, FileMode.Append, FileAccess.Write))
                {
                    //寫入檔案
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                    {
                        //sw.WriteLine(DateTime.Now);
                        sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                        sw.WriteLine(pLogs);
                        sw.Close();
                    }
                    fs.Close();
                }
            }
        }

        private bool chkFile(string pLogFileName)
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\Logs"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\Logs");
                }
                if (!File.Exists(Application.StartupPath + @"\Logs\" + pLogFileName))
                {
                    FileStream fs = new FileStream(Application.StartupPath + @"\Logs\" + pLogFileName, FileMode.CreateNew, FileAccess.Write);
                    fs.Close();
                }
                return true;
            }
            catch (System.UnauthorizedAccessException ue)   //沒有寫入權限
            {
                MessageBox.Show("新建Log檔案失敗，失敗原因：沒有寫入權限. " + ue.Message);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("新建Log檔案失敗，失敗原因：" + ex.Message);
                return false;
            }
        }


		#region 不開啟WinForm工具執行
		public void RunBatResavePDF() {
			gMsg = "RunType參數【" + tCommon.RunType + "】, FolderOrFilesPath參數【" + tCommon.FolderOrFilesPath + "】, IsHaveSub參數【" + tCommon.IsHaveSub.ToString() + "】\r\n";
			gMsg += string.Format("檢查{0}天內新增/修改的pdf檔 \r\n", gLastModifiedDays);
    		WriteLog(gMsg);
			if((tCommon.RunType!="P" && tCommon.RunType!="F") || tCommon.FolderOrFilesPath==""){
				gMsg = "參數錯誤，無法執行！";
    			WriteLog(gMsg);
				return;
			}


			if(tCommon.RunType=="P"){
				CheckPDFInFolder(tCommon.FolderOrFilesPath);
			}
			else if(tCommon.RunType=="F"){
				string[] aryMultiFile = tCommon.FolderOrFilesPath.Split('△');
	            foreach (string tFilePath in aryMultiFile)
				{
	                if (tFilePath == "")
	                {
	                    continue;
	                }

	                if (!File.Exists(tFilePath))
					{
						gMsg = "【" + tFilePath + "】檔案不存在";
                		WriteLog(gMsg);
	                    continue;
	                }

	                //CheckPDFInFile(tFilePath);
	                FileInfo file = new FileInfo(tFilePath);
	                CheckPDFInFile(file);
	            }
			}
		}
        #endregion 不開啟WinForm工具執行

        private void button1_Click(object sender, EventArgs e)
        {
            txtResult.Text = File.ReadAllText("Resources/README.txt", Encoding.UTF8);
        }
    }

    public static class tCommon
	{
		//P:資料夾路徑, F:挑選多檔案
		public static string RunType = "P";

		//資料夾或檔案路徑
		public static string FolderOrFilesPath = "";

		//是否包含子路徑
		public static bool IsHaveSub = false;
	}
}
