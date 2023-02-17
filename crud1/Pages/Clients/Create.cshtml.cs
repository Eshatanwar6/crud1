using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace crud1.Pages.Clients
{
    public class CreateModel : PageModel

    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";


        public void OnGet()
        {
        }
        //we have to make new onpost method which when executed when we send the data  of ythis form using this submit button

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "all the fields  are required";
                return;
            }
            //in this method we can read the data of the form and we can  put this data into this object

            // save the newclient into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=crud;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Clients" +
                        "(name,email,phone,address)  VALUES " + "(@name,@email,@phone,@address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
         

            // save the new client into the database

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
           
            successMessage = "New client added correctly";
			Response.Redirect("/Clients/");

		}
       
    }


}
