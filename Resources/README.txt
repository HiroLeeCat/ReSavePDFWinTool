這是一款開源（Open Source）AGPL v3 授權的 修復PDF開啟失敗，則嘗試另存新檔的工具。
專為檢查PDF檔，如使用 PdfSharp 元件讀取PDF失敗，則改用另一個元件(例 iText )讀取後另存 PDF檔，進而提高 PdfShartp 元件讀取PDF成功的機率。
讀取失敗的PDF，會在該PDF路徑下創建bak資料夾以儲存原本的PDF檔。
如需測試其他元件，請自行修改使用的元件及程式段即可。

該工具有兩種執行方式：

一、人工點擊ReSavePDFWinTool.exe，即可自行選擇「資料夾路徑」 或 「挑選PDF檔」進行檢查。

二、透過排程呼叫ReSavePDFWinTool.exe，傳入參數，第一個參數請設定 P (資料夾模式) 或 F (檔案模式)，第二個參數請設定 資料夾路徑，或用△符號分隔的PDF檔路徑，如第一個參數為 P(資料夾模式)，可傳入第三個參數 Y/true 或 N/false，是否包含子資料夾。
