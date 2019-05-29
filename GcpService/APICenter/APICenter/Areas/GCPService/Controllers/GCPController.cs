using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APICenter.Areas.GCPService.Models.Interface;
using APICenter.Areas.GCPService.Models.Services;
using APICenter.Areas.GCPService.Models;

namespace APICenter.Areas.GCPService.Controllers
{
    public class GCPController : ApiController
    {
        //private readonly IGcpSpreadSheet _IGcpSpreadSheet;

        //public GCPController()
        //{
        //}

        //public GCPController(IGcpSpreadSheet IGcpSpreadSheet)
        //{
        //    _IGcpSpreadSheet = IGcpSpreadSheet;
        //}

        [HttpPost]
        public IHttpActionResult GetGcpService( Models.CustomerList clist)
        {
            GcpSpreadSheetService Gss = new GcpSpreadSheetService();
            var service = Gss.OpenSheet();
            clist.orders.ToList().ForEach(n =>
            {
                InsertList ins = new InsertList();
                ins.Name = clist.Name;
                ins.Tel = clist.Tel;
                ins.Address = clist.Address;

                ins.Qty = n.Qty;
                ins.Sex = n.Sex;
                ins.IsCut = n.IsCut == "Y" ? "切" : "不切";
                ins.IsInternalOrgans = n.IsInternalOrgans == "Y" ? "要內臟" : "不要內臟";

                Gss.Update(service, ins);
            });


            return Ok(new { errorCode = 200, IsSuccess = true });
        }
    }
}
