using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BusinessLogicLayer
{
    public class Product
    {
        public string Prod_Num { get; set; }
        public string Prod_Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public double TotalPrice
        {
            get
            {
                return Price * Quantity;
            }
        }
        public bool IsStock
        {
            get
            {
                return (Quantity > 0);
            }
        }


        static public bool AddNewProduct(Product Prod)
        {
            ConnectionClass ConObj = new ConnectionClass();
            SqlConnection con=new SqlConnection();
            if(ConObj.ConnectionCheck(ref con))
            {
                var cmd = new SqlCommand("dbo.spAddNewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter();

                param = cmd.Parameters.AddWithValue("@ProdName", SqlDbType.VarChar);
                param.Value = Prod.Prod_Name;
                param.Direction = ParameterDirection.Input;

                param = cmd.Parameters.AddWithValue("@Description", SqlDbType.VarChar);
                param.Value = Prod.Description;
                param.Direction = ParameterDirection.Input;

                param = cmd.Parameters.AddWithValue("@Price", SqlDbType.Decimal);
                param.Value = Prod.Price;
                param.Direction = ParameterDirection.Input;

                param = cmd.Parameters.AddWithValue("@Quantity", SqlDbType.Int);
                param.Value = Prod.Quantity;
                param.Direction = ParameterDirection.Input;

                var affectedRows=cmd.ExecuteNonQuery();
                con.Close();
                return (affectedRows > 0);
            }
            else
            {
                return false;
            }
        }

        static public bool UpdateProduct(Product Prod)
        {
            ConnectionClass ConObj = new ConnectionClass();
            SqlConnection con = new SqlConnection();
            if (ConObj.ConnectionCheck(ref con))
            {
                var cmd = new SqlCommand("dbo.spUpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter();
                param = cmd.Parameters.AddWithValue("@ProdNo", SqlDbType.Int);
                param.Value =Convert.ToInt32(Prod.Prod_Num);
                param.Direction = ParameterDirection.Input;

                param = cmd.Parameters.AddWithValue("@ProdName", SqlDbType.VarChar);
                param.Value = Prod.Prod_Name;
                param.Direction = ParameterDirection.Input;

                param = cmd.Parameters.AddWithValue("@Description", SqlDbType.VarChar);
                param.Value = Prod.Description;
                param.Direction = ParameterDirection.Input;

                param = cmd.Parameters.AddWithValue("@Price", SqlDbType.Decimal);
                param.Value = Prod.Price;
                param.Direction = ParameterDirection.Input;

                param = cmd.Parameters.AddWithValue("@Quantity", SqlDbType.Int);
                param.Value = Prod.Quantity;
                param.Direction = ParameterDirection.Input;

                var affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return (affectedRows > 0);
            }
            else
            {
                return false;
            }
        }

        static public bool DeleteProduct(string ProdNum)
        {
            ConnectionClass ConObj = new ConnectionClass();
            SqlConnection con = new SqlConnection();
            if (ConObj.ConnectionCheck(ref con))
            {
                var cmd = new SqlCommand("dbo.spDeleteProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter();
                param = cmd.Parameters.AddWithValue("@ProdNo", SqlDbType.VarChar);
                param.Value = Convert.ToInt32(ProdNum);
                param.Direction = ParameterDirection.Input;

                var affectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return (affectedRows > 0);
            }
            else
            {
                return false;
            }
        }

        static public List<Product> DisplayProduct()
        {
            var prodList=new List<Product>();
            ConnectionClass ConObj = new ConnectionClass();
            SqlConnection con = new SqlConnection();
            if (ConObj.ConnectionCheck(ref con))
            {
                var cmd = new SqlCommand("dbo.spViewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    var prod = new Product();
                    prod.Prod_Num = reader["Prod_Num"].ToString();
                    prod.Prod_Name= reader["Prod_Name"].ToString();
                    prod.Description = reader["Description"].ToString();
                    prod.Price =Convert.ToDouble(reader["Price"]);
                    prod.Quantity = Convert.ToInt32(reader["Quantity"]);
                    prodList.Add(prod);
                }
                con.Close();
            }

            return prodList;
        }
    }
}
