using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

namespace MVC_92020.Controllers
{
    public class homeController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString); 
        public ActionResult Index()
        {
            return View();
        }

        public void InsertUpdate(string A, string B, string C,int D,int E,int F)
        {
            if (D == 0)
            {
                con.Open();
                SqlCommand com = new SqlCommand("procuser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@action", "insert");
                com.Parameters.AddWithValue("@u_name", A);
                com.Parameters.AddWithValue("@u_email", B);
                com.Parameters.AddWithValue("@u_password", C);
                com.Parameters.AddWithValue("@countryid", E);
                com.Parameters.AddWithValue("@stateid", F);
                com.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand com = new SqlCommand("procuser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@action", "update");
                com.Parameters.AddWithValue("@u_name", A);
                com.Parameters.AddWithValue("@u_email", B);
                com.Parameters.AddWithValue("@u_password", C);
                com.Parameters.AddWithValue("@u_id", D);
                com.Parameters.AddWithValue("@countryid", E);
                com.Parameters.AddWithValue("@stateid", F);
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public JsonResult Display()
        {
            string data = "";
            con.Open();
            SqlCommand com = new SqlCommand("procuser", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@action", "display");
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindCountry()
        {
            string countrydata = "";
            con.Open();
            SqlCommand com = new SqlCommand("contryProc", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            countrydata = JsonConvert.SerializeObject(dt);
            return Json(countrydata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindState(int A)
        {
            string statedata = "";
            con.Open();
            SqlCommand com = new SqlCommand("stateproc", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@countryid", A);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            statedata = JsonConvert.SerializeObject(dt);
            return Json(statedata, JsonRequestBehavior.AllowGet);
        }

        public void Delete(int A)
        {
            con.Open();
            SqlCommand com = new SqlCommand("procuser", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@action", "delete");
            com.Parameters.AddWithValue("@u_id", A);
            com.ExecuteNonQuery();
            con.Close();
        }

        public JsonResult Edit(int B)
        {
            string data = "";
            con.Open();
            SqlCommand com = new SqlCommand("procuser", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@action", "edit");
            com.Parameters.AddWithValue("@u_id", B);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}