using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataSpider
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String str;
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Vaccine_Reserve_Platform_Data;Integrated Security=True");
            try
            {
                HtmlWeb webClient = new HtmlWeb();
                for (int p = 1; p<=10; p++)
                {

                    HtmlDocument doc = webClient.Load($"https://www.cdc.gov.tw/Bulletin/List/MmgtpeidAR5Ooai4-fgHzQ?page={p}");
                    HtmlNodeCollection list = doc.DocumentNode.SelectNodes("/html/body/div[1]/section/div[2]/div[1]/div/div[3]/div/div");
                    Console.OutputEncoding = System.Text.Encoding.Unicode;
                    for (int i = 0; i<10; i++)
                    {
                        string link = list[i].SelectSingleNode("div/div/a").GetAttributeValue("href", "");
                        HtmlDocument news_doc = webClient.Load("https://www.cdc.gov.tw"+link);
                        String date = news_doc.DocumentNode.SelectSingleNode("/html/body/div[1]/section/div[2]/div/div/div[1]/div").InnerText.Replace("發佈日期：", "").Trim();
                        String title = news_doc.DocumentNode.SelectSingleNode("/html/body/div[1]/section/div[2]/div/div/h2/text()").InnerText.Trim();
                        String text = news_doc.DocumentNode.SelectSingleNode("/html/body/div[1]/section/div[2]/div/div/div[1]").InnerText.Replace("發佈日期："+date, "").Replace("&nbsp;", " ").Trim();

                        str = "INSERT INTO News VALUES (@title,@text,@date)";

                        SqlCommand command = new SqlCommand(str, connection);
                        try
                        {
                            connection.Open();
                            command.Parameters.AddWithValue("@title", title);
                            command.Parameters.AddWithValue("@text", text);
                            command.Parameters.AddWithValue("@date", date);
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR=" + ex.ToString());
            }
            Console.ReadLine();

        }
    }
}
