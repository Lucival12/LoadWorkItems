using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoadWorkItems
{
    class Program
    {
        static void Main(string[] args)
        {

            string ulrAzure = "https://lucivaldev.visualstudio.com";
            string teamProject = "LoadWorkItems";
            string tmp;

            do
            {
                Console.WriteLine("Deseja deixa as configurações padrões?(S/N)");
                tmp = Console.ReadLine();
                tmp = tmp.ToUpper();
            } while ((!tmp.Equals("S")) && (!tmp.Equals("N")));

            if (tmp.Equals("N"))
            {
                Console.WriteLine("Digite a URL do Azure DevOps ");
                ulrAzure = Console.ReadLine();

                Console.WriteLine("Digite o	Nome do projeto");
                teamProject = Console.ReadLine();

            }

            VssConnection connection = null;
            connection = new VssConnection(new Uri(ulrAzure), new VssClientCredentials());
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            string connectionString = String.Format(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WorkItems;Integrated Security=True");

            Wiql wiql = new Wiql();

            wiql.Query = "SELECT [System.Id] FROM WorkItems WHERE [System.TeamProject]='" + teamProject + "' AND [System.Id] >'"+ UltimaAtualizacao(connectionString) + "'";

            WorkItemQueryResult tasks = witClient.QueryByWiqlAsync(wiql).Result;

            if (tasks.WorkItems.Any())
            {
                
                IEnumerable<WorkItemReference> tasksRefs;
                tasksRefs = tasks.WorkItems;
                List<WorkItem> tasksList = witClient.GetWorkItemsAsync(tasksRefs.Select(wir => wir.Id)).Result;

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();


                        foreach (WorkItem task in tasksList)
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Item(Id,Title,Type,Data) VALUES (" +
                                   "@id,@title,@type,@data)", conn))
                            {
                                cmd.Parameters.AddWithValue("@Id", task.Id);
                                cmd.Parameters.AddWithValue("@title", task.Fields["System.Title"]);
                                cmd.Parameters.AddWithValue("@type", task.Fields["System.WorkItemType"]);
                                cmd.Parameters.AddWithValue("@data", task.Fields["System.CreatedDate"]);

                                int rows = cmd.ExecuteNonQuery();
                            }
                        }                       
                        conn.Close();
                       
                    }
                }
                catch (SqlException ex)
                {
                    //Log exception
                    //Display Error message
                }


            }
            Console.WriteLine("Working Itens Atualizados");
            Console.ReadLine();

        }

        private static int UltimaAtualizacao(string connectionString)
        {
            int idReturn = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmdMax = new SqlCommand("SELECT MAX(Id) from Item", conn);
                    var ret =  cmdMax.ExecuteScalar();

                    if(ret.GetType() != typeof(DBNull))
                        idReturn = Convert.ToInt32(cmdMax.ExecuteScalar());
                    conn.Close();
                    return idReturn;
                }
            }
            catch (SqlException ex)
            {
            }
                return idReturn;
        }
    }
}

