using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineReservePlatformTopicWebApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using Newtonsoft.Json;

namespace VaccineReservePlatformTopicWebApi.Controllers
{

    public class EasyCheckController : ApiController
    {

        public HttpResponseMessage Post(EasyCheck easyCheck)
        {
            try
            {
                String query = "select * from [EasyCheck] where ROCId = @ROCId and Year = @Year";
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ROCId", easyCheck.ROCId);
                    command.Parameters.AddWithValue("@Year", easyCheck.Year);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);

                }
                if (dataTable.Rows.Count == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject("{'Result':'Access'}"));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject("{'Result':'Denied'}"));
                }
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY"))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.DeserializeObject("{'Result':'ROCID_HAS_EXISTS'}"));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            
        }

        public HttpResponseMessage Put(EasyCheck easyCheck)
        {
            try
            {
                String query = "insert into [EasyCheck] values (@ROCId,@Year)";
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ROCId", easyCheck.ROCId);
                    command.Parameters.AddWithValue("@Year", easyCheck.Year);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject("{'Result':'Access'}"));
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.DeserializeObject("{'Result':'Error'}"));
            }


        }

    }
}