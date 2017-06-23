using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication3.Models
{
    public class OrderService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<Models.Order> GetOrderByCondtioin(Models.OrderSearchArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT 
                    CustomerID,CompanyName,ContactName,ContactTitle
					from Sales.Customers
					Where (CustomerID = @CustomerID Or @CustomerID='') And 
						  (CompanyName = @CompanyName Or @CompanyName='') And 
						  (ContactName = @ContactName Or @ContactName='') And 
						  (ContactTitle = @ContactTitle Or @ContactTitle='')";


            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", arg.CustomerID == null ? string.Empty : arg.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", arg.CompanyName == null ? string.Empty : arg.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@ContactName", arg.ContactName == null ? string.Empty : arg.ContactName));
                cmd.Parameters.Add(new SqlParameter("@ContactTitle", arg.ContactTitle == null ? string.Empty : arg.ContactTitle));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            return this.MapOrderDataToList(dt);
        }

        private List<Models.Order> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();
            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    CustomerID = row["CustomerID"].ToString(),
                    CompanyName = row["CompanyName"].ToString(),
                    ContactName = row["ContactName"].ToString(),
                    ContactTitle = row["ContactTitle"].ToString(),
                });
            }
            return result;
        }

        public void DeleteOrderById(string Delete)
        {
            try
            {
                string sql = "Delete FROM Sales.Customers Where CustomerID=@CustomerID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@CustomerID", Delete));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int InsertOrder(Models.Order order)
        {
            string sql = @"Insert INTO Sales.Customers
						 (
                            CompanyName,ContactName,ContactTitle,CreationDate,Country,Region,PostalCode,
                            Address,Phone,Fax,CustomerID
						 )
						 VALUES
						 (
                            @CompanyName,@ContactName,@ContactTitle,@CreationDate,@Country,@Region,@PostalCode,
					        @Address,@Phone,@Fax,@CustomerID
						 )
                         Select SCOPE_IDENTITY() as CustomerID";
            int OrderID;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID", order.CustomerID));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", order.CompanyName));
                cmd.Parameters.Add(new SqlParameter("@ContactName", order.ContactName));
                cmd.Parameters.Add(new SqlParameter("@ContactTitle", order.ContactTitle));
                cmd.Parameters.Add(new SqlParameter("@CreationDate", order.CreationDate));
                cmd.Parameters.Add(new SqlParameter("@Country", order.Country));
                cmd.Parameters.Add(new SqlParameter("@Region", order.Region));
                cmd.Parameters.Add(new SqlParameter("@PostalCode", order.PostalCode));
                cmd.Parameters.Add(new SqlParameter("@Address", order.Address));
                cmd.Parameters.Add(new SqlParameter("@Phone", order.Phone));
                cmd.Parameters.Add(new SqlParameter("@Fax", order.Fax));
                OrderID = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return OrderID;
        }

    }
}