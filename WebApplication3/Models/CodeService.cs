using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace WebApplication3.Models
{
    public class CodeService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString.ToString();
        }

        public List<SelectListItem> GetProduct()
        {
            DataTable dt = new DataTable();
            string sql = @"Select ProductID As CodeId,ProductName As CodeName From Production.Products";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        public List<SelectListItem> GetEmployee()
        {
            DataTable dt = new DataTable();
            string sql = @"Select EmployeeID As CodeId,Lastname+'-'+Firstname As CodeName FROM HR.Employees";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        public List<SelectListItem> GetShipper()
        {
            DataTable dt = new DataTable();
            string sql = @"Select ShipperID As CodeId,CompanyName As CodeName FROM Sales.Shippers";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        public List<SelectListItem> GetCustomer()
        {
            DataTable dt = new DataTable();
            string sql = @"Select CustomerID As CodeId,CompanyName As CodeName FROM Sales.Customers";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapCodeData(dt);
        }

        private List<SelectListItem> MapCodeData(DataTable dt)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            result.Add(new SelectListItem()
            {
                Text = "",
                Value = ""
            });
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row["CodeName"].ToString(),
                    Value = row["CodeId"].ToString()
                });
            }
            return result;
        }
    }
}