using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace APICenter.Areas.GCPService.Models.Interface
{
    public interface IGcpSpreadSheet
    {
        SheetsService OpenSheet();
        void Update(SheetsService service , CustomerList clist);
    }

    public struct CustomerList
    {
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public List<OrderList> orders { get; set; }
    }
    public struct OrderList
    {
        public string Sex { get; set; }
        public int Qty { get; set; }
        public string IsCut { get; set; }
        public string IsInternalOrgans { get; set; }
    }
}