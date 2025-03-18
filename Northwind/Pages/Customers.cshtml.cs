using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

public class CustomersModel : PageModel{

    //Attribute to hold the list of customers at Northwind
    public List<Customer> Customers { get; set; }

    //Implement the onGet method - this method is going to get the list of customers
    //from the database and populate the page
    public void OnGet(){
        //Create a new empty list to hold customers
        Customers = new List<Customer>();

        //Create a conenction string to connect to the database
        string connectionString = "Server=localhost;Database=Northwind;UserId=sa;Password=P@ssw0rd; ; TrustServerCertificate=True;";
    
        //Create our database connection
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            //Open a connectio
            connection.Open();

            //String to hold the SQL statement that we are executing
            string sql = "SELECT CustomerID, CompanyName, ContactName, Country FROM Customers";

            //Create a SQL command to execute our SQL statement
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                //SQL Data Reader reads the data from the database
                using (SqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()) {
                        Customers.Add(new Customer
                        {
                            CustomerID = reader.GetString(0),
                            CompanyName = reader.GetString(1),
                            ContactName = reader.GetString(2),
                            Country = reader.GetString(3)
                        });
                    }//end while
                }//end SQL Reader
            }//end SQL Command

        }//end SQL Connection
    }//end onGet method

}//end class

//Customer class is a blueprint for a customer object & it represents a customer in our database
public class Customer{
    //Attributes (variables that hold the values that describe a customer)
    public string CustomerID { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string Country { get; set; }
}