using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace crud1.Pages.Clients
{
    public class IndexModel : PageModel
    { 
        public List<ClientInfo>listClients = new List<ClientInfo>();
    // we need to fill  list on get method
        public void OnGet()   // and in this method we need to access the database and read data from client table
        {
            try // we need  to connect to the database
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crud;Integrated Security=True";
                //sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //create the sql  query that allows us to read the data from  the client  
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))// create the sql command
                    {// this command  help us to execute the sql query
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);

                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();
                                // we can read the data from the table  by using the while loop
                                listClients.Add(clientInfo);
                            }


                        }// read all the rows from the  client table
                    }
                }
            }
            catch (Exception ex)
            {
                // add exception that show error in console  if we have any exception
            }
        }
    }
    public class ClientInfo {   // this class allow to store data of only one client
        // and to store the data of all the clients we need to make list into the model
    
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
        

    }
}
