using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using APICenter.Areas.GCPService.Models.Interface;
using APICenter.Areas.GCPService.Models;

namespace APICenter.Areas.GCPService.Models.Services
{
    public class GcpSpreadSheetService
    {
        static string ApplicationName = "SheetData";
        static String spreadsheetId = "19b67snFAXeTZgww5Kbu3IeGBxRIAwV9psV1cWhp20YU";
        static string sheetName = "Test123";

        public SheetsService OpenSheet()
        {
            UserCredential credential;
            string[] Scopes = { SheetsService.Scope.Spreadsheets };

            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Path.Combine
                    (System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                     ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                //存儲憑證到credPath
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

            }
            //建立一個API服務，設定請求參數
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }



        public void Update(SheetsService service, InsertList lises)
        {
            ValueRange rVR;
            string sRange;
            int rowNumber = 1;

            //設定讀取A欄最後一行位置
            sRange = String.Format("{0}!A:A", sheetName);
            SpreadsheetsResource.ValuesResource.GetRequest getReq
                = service.Spreadsheets.Values.Get(spreadsheetId, sRange);
            //到google讀取內容
            rVR = getReq.Execute();
            IList<IList<Object>> values = rVR.Values;//最後一行位置

            //寫入新資料

            //添加一行
            if (values != null && values.Count > 0) rowNumber = values.Count + 1;
            //指定寫入位置
            sRange = String.Format("{0}!A{1}:Z{1}", sheetName, rowNumber);

            //設定寫入
            ValueRange valueRange = new ValueRange();
            valueRange.Range = sRange;

            //ROWS或COLUMNS
            valueRange.MajorDimension = "ROWS";

            //取得當前時間
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            List<object> oblist = new List<object>() { String.Format("{0}", rowNumber - 1),lises.Name, lises.Tel,lises.Address,lises.Qty,lises.Sex,lises.IsCut,lises.IsInternalOrgans , dt.ToString("yyyy:HH:mm:ss") };
            //寫入時間
            valueRange.Values = new List<IList<object>> { oblist };
            //執行寫入動作
            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest
                = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, sRange);
            updateRequest.ValueInputOption
                = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            UpdateValuesResponse uUVR = updateRequest.Execute();
        }
    }
}