using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web;
using System.Net.Http.Headers;
using System.Text;
using System.IO;
using APICenter.Areas.Milestone.Models.Interface;
using APICenter.Areas.Milestone.Models.Repositiry;
using APICenter.Areas.Milestone.Models;
using System.Diagnostics;
using Microsoft.ApplicationInsights;

namespace APICenter.Areas.Milestone.Controllers
{
    public class Milestone_UtitilyController : ApiController
    {

        private IRepository<SMMilestoneHistory> SMMilestoneHistoryRepository;
        private ReSM_Entities resdb = new ReSM_Entities();
        //private IRepository<ReSM_Entities> SMStationRepository;
        //private IRepository<SMCity> SMCityRepository;


        TelemetryClient client = new TelemetryClient();

        public Milestone_UtitilyController()
        {
            this.SMMilestoneHistoryRepository = new GenericRepository<SMMilestoneHistory>();

            //this.SMStationRepository = new GenericRepository<ReSM_Entities>();
            //this.SMCityRepository = new GenericRepository<SMCity>();
        }
    

        [HttpPost]
        public IHttpActionResult SaveMileStoneHistory(SaveParam param)
        {
            //Check Mandatory Field
            string Msg = string.Empty;
            if (string.IsNullOrEmpty(param.StationID))
                Msg = "StationID";
            if (string.IsNullOrEmpty(param.KeyValue))
                Msg += (Msg.Length == 0 ? "" : ",") + "KeyValue";
            if (param.MileStoneUnixTime == 0)
                Msg += (Msg.Length == 0 ? "" : ",") + "Milestone Time";
            if (param.CreatedUnixTime == 0)
                Msg += (Msg.Length == 0 ? "" : ",") + "Local Create Time";
            if (param.CreatedBy == null)
                Msg += (Msg.Length == 0 ? "" : ",") + "CreatedBy";

            if (!string.IsNullOrWhiteSpace(Msg))
            {
                return Content(HttpStatusCode.BadRequest, new { errorCode = -1, IsSuccess = false, errorMessage = Msg });
            }

            try
            {

                Stopwatch sw = new Stopwatch();
                sw.Reset();
                sw.Start();

                List<SMMilestoneHistory> listOld = this.SMMilestoneHistoryRepository.GetAll().Where(n => n.Status == param.ActiveStatus && n.keyValue == param.KeyValue && n.MilestoneID == param.MileStoneID).ToList();
                foreach (SMMilestoneHistory d in listOld)
                    d.Status = param.HistoryStatus;
                sw.Stop();

                Dictionary<string, string> data = new Dictionary<string, string>();

                data.Add("Select", sw.ElapsedMilliseconds.ToString());

                client.TrackEvent("Mileston Event Select MilestoneHistory", data);

                DateTime MilestoneTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(param.MileStoneUnixTime); //Change current var name by Ziv at 2019/1/3
                DateTime CreateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(param.CreatedUnixTime);
                var CityID = resdb.SMStation.Where(s => s.StationID == param.StationID && (s.Status.Contains("Active") || s.Status.Contains("Finance"))).Select(c => c.CityID).ToList().FirstOrDefault();
                var CityCode = resdb.SMCity.Where(x => x.HQID == CityID).Select(c => c.CityCode).FirstOrDefault();

                SMMilestoneHistory objNew = new SMMilestoneHistory();
                objNew.StationID = param.StationID;
                objNew.keyValue = param.KeyValue;
                objNew.MilestoneTime = MilestoneTime; // UTCTime; change current var name by Ziv at 2019/1/3
                objNew.MilestoneID = param.MileStoneID;
                objNew.CreatedUTCDT = DateTime.UtcNow;
                objNew.Status = param.ActiveStatus;
                objNew.CreatedBy = param.CreatedBy;
                objNew.LocalCityCode = param.LocalCityCode;
                objNew.LocalCreatedDT = CreateTime; //Save Local Time by Ziv at 2019/1/3
                objNew.Productline = param.ProductlineID;
                objNew.MilestoneCityCode = param.MilestoneCityCode == null ? CityCode : param.MilestoneCityCode;
                objNew.IrrReason = param.IrrReason;
                objNew.ApprovedBy = param.ApprovedBy;
                objNew.ExtraValue1 = param.ExtraValue1;
                objNew.ExtraValue2 = param.ExtraValue2;
                objNew.UTCOffSet = param.UTCOffSet;

                sw.Reset();
                sw.Start();
                SMMilestoneHistoryRepository.Create(objNew);
                SMMilestoneHistoryRepository.SaveChanges();
                sw.Stop();

                data = new Dictionary<string, string>();
                data.Add("Insert", sw.ElapsedMilliseconds.ToString());

                client.TrackEvent("Mileston Event Insert MilestoneHistory", data);

                SMMilestoneHistoryRepository.Dispose();
            }
            catch (Exception e)
            {
                //CommonLibrary.LogHelper.Write(e.Message)
                return Content(HttpStatusCode.InternalServerError, new { errorCode = -1, IsSuccess = false, errorMessage = e.Message });
            }

                return Ok(new { errorCode = 0, IsSuccess = true, errorMessage = "" });
            }
    }

    public struct SaveParam
    {
        public string StationID { get; set; }
        public string KeyValue { get; set; }
        public int MileStoneID { get; set; }
        public int MileStoneUnixTime { get; set; }
        public int CreatedUnixTime { get; set; }
        public string CreatedBy { get; set; }
        public string LocalCityCode { get; set; }
        public int ProductlineID { get; set; }
        public string MilestoneCityCode { get; set; }
        public string IrrReason { get; set; }
        public string ApprovedBy { get; set; }
        public string ExtraValue1 { get; set; }
        public string ExtraValue2 { get; set; }
        public int UTCOffSet { get; set; }
        public int ActiveStatus { get; set; }
        public int HistoryStatus { get; set; }
        public string OldKeyValue { get; set; }
        public string NewKeyValue { get; set; }
        public int NewStatus { get; set; }
        public int OldStatus { get; set; }
    }
}