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

namespace VaccineReservePlatformTopicWebApi.Controllers
{
    public class NewsController : ApiController
    {

        public HttpResponseMessage Get()
        {
            String query = "select * from [News]";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query,connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dataTable);

            }
            return Request.CreateResponse(HttpStatusCode.OK, dataTable);
        }

        public HttpResponseMessage Get(int id)
        {
            String query = "select * from [News] where Id = @Id";
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id",id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dataTable);

            }
            return Request.CreateResponse(HttpStatusCode.OK, dataTable);
        }


    }
}