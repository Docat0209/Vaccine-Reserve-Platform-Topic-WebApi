using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineReservePlatformTopicWebApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System;
using Newtonsoft.Json;

namespace VaccineReservePlatformTopicWebApi.Controllers
{
    public class ReserveController : ApiController
    {

        public HttpResponseMessage Get([FromUri] string rocId)
        {
            try
            {
                String query = "select * from[Reserve] where ROCId = @ROCId; ";
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ROCId", rocId);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);

                }
                if (dataTable.Rows.Count >= 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, dataTable);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject("{'Result':'Denied'}"));
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.DeserializeObject("{'Result':'Error'}"));
            }
        }


        public HttpResponseMessage Put(Reserve reserve)
        {
            try
            {
                String query = "insert into [Reserve] values (@Name,@Phone,@ROCId,@VaccineType,@City,@Dist,@HospName)";
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ROCId", reserve.ROCId);
                    command.Parameters.AddWithValue("@Name", reserve.Name);
                    command.Parameters.AddWithValue("@Phone", reserve.Phone);
                    command.Parameters.AddWithValue("@City", reserve.City);
                    command.Parameters.AddWithValue("@Dist", reserve.Dist);
                    command.Parameters.AddWithValue("@HospName", reserve.HospName);
                    command.Parameters.AddWithValue("@VaccineType", reserve.VaccineType);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject("{'Result':'Access'}"));
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

        public HttpResponseMessage Delete([FromBody]StringId rocId)
        {
            try
            {
                String query = "select * from[Reserve] where ROCId = @ROCId; ";
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ROCId", rocId.ROCId);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);

                }
                if (dataTable.Rows.Count == 1)
                {
                    query = "delete from [Reserve] where ROCId = @Id";
                    using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", rocId.ROCId);
                        command.ExecuteNonQuery();
                        return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject("{'Result':'Access'}"));
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.DeserializeObject("{'Result':'Denied'}"));
                }

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.DeserializeObject("{'Result':'Error'}"));
            }

        }
    }
}