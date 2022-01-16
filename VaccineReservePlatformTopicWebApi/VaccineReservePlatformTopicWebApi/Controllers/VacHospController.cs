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

namespace VaccineReservePlatformTopicWebApi.Controllers
{

    public class VacHospController : ApiController
    {

        public HttpResponseMessage Get()
        {
            String query = "select distinct City from[Covid19VacHosp]; ";
            DataTable dataTable = new DataTable();
            string[] arrray;
            using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dataTable);
                arrray = dataTable.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

            }
            return Request.CreateResponse(HttpStatusCode.OK, arrray );
        }

        public HttpResponseMessage Get([FromUri] string city)
        {
            String query = "select distinct Dist from[Covid19VacHosp] where City = @City; ";
            DataTable dataTable = new DataTable();
            string[] arrray;
            using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@City", city);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dataTable);
                arrray = dataTable.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

            }
            return Request.CreateResponse(HttpStatusCode.OK, arrray);
        }


        public HttpResponseMessage Get([FromUri] string city , [FromUri] string dist , [FromUri] string vaccine)
        {
            String query = "select Name from[Covid19VacHosp] where City = @City and Vaccine = @Vaccine and Dist = @Dist; ";
            DataTable dataTable = new DataTable();
            string[] arrray;
            using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@Vaccine", vaccine);
                command.Parameters.AddWithValue("@Dist", dist);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dataTable);
                arrray = dataTable.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

            }
            return Request.CreateResponse(HttpStatusCode.OK, arrray);
        }

    }
}