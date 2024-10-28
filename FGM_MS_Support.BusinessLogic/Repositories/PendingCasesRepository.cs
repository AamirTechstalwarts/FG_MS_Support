using FGM_MS_Support.BusinessLogic.Entities;
using FGM_MS_Support.BusinessLogic.Repositories.Interface;
using FGM_MS_Support.DattaAccess.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGM_MS_Support.BusinessLogic.Repositories
{
    public class PendingCasesRepository : IPendingCasesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PendingCasesRepository> _logger;
        public PendingCasesRepository(IConfiguration configuration, ILogger<PendingCasesRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public ResponseObject GetNBSyncPendingCases()
        {
            string ConnString = _configuration["ConnectionStrings:DefaultConnection"].ToString();
            ResponseObject responseObject = new ResponseObject();
            List<PendingCases> list = new List<PendingCases>();
            DataSet ds = new DataSet();
            try
            {
                ds = DBUtility.ExecuteProcedureReturnDataset(ConnString, "USP_FG_SUPPORT_FETCNBSYNCPENDINGAPP");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    _logger.LogInformation("Tables contains the Information");
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        list.Add(new PendingCases
                        {
                            ApplicationNumber = item["APPLICATION NUMBER"].ToString(),
                            JourneyStatus = item["JOURNEY STATUS"].ToString(),
                            SubmittedDate = item["SUBMITTED DATE"].ToString(),
                            CurrentDate = item["CURRENT DATE"].ToString(),
                            TimeDiffInMinutes = item["TIMEDIFF IN MINUTES"].ToString()


                        });

                    }
                    responseObject.Errorcode = "0";
                    responseObject.Issuccess = true;
                    responseObject.Message = "Success";
                    responseObject.ResponseBody = list;
                    _logger.LogInformation("Data retrived successfully");
                }

                else
                {
                    responseObject.Errorcode = "1";
                    responseObject.Issuccess = false;
                    responseObject.Message = "No Records Found";
                    responseObject.ResponseBody = "";
                    _logger.LogError("Data havent retrived from Database Reason:{ErrorMessage}", responseObject.Message);
                }
            }
            catch (Exception ex)
            {
                responseObject.Errorcode = "1";
                responseObject.Issuccess = false;
                responseObject.Message = "Error Exceptions";
                responseObject.ResponseBody = "GetNBSyncPendingCases :" + ex.Message.ToString();
                _logger.LogError("Exception Occured while fetching the data",responseObject.ResponseBody);
            }
            return responseObject;
        }
    }
}
