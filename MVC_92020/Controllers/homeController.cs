using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using MVC_92020.Models;

namespace MVC_92020.Controllers
{
    public class homeController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString); 
        public ActionResult Index()
        {
            return View();
        }

        public void InsertUpdate(Emp_Insert obj)
        {
            if (obj.D == 0)
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertProc", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@u_name", obj.A);
                com.Parameters.AddWithValue("@u_email", obj.B);
                com.Parameters.AddWithValue("@u_password", obj.C);
                com.Parameters.AddWithValue("@countryid", obj.E);
                com.Parameters.AddWithValue("@stateid", obj.F);
                com.Parameters.AddWithValue("@gender", obj.G);
                com.Parameters.AddWithValue("@dob", obj.H);
                com.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand com = new SqlCommand("updateProc", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@u_name", obj.A);
                com.Parameters.AddWithValue("@u_email", obj.B);
                com.Parameters.AddWithValue("@u_password", obj.C);
                com.Parameters.AddWithValue("@u_id", obj.D);
                com.Parameters.AddWithValue("@countryid", obj.E);
                com.Parameters.AddWithValue("@stateid", obj.F);
                com.Parameters.AddWithValue("@gender", obj.G);
                com.Parameters.AddWithValue("@dob", obj.H);
                com.ExecuteNonQuery();
                con.Close();
            }
        }

        public JsonResult Display()
        {
            string data = "";
            con.Open();
            SqlCommand com = new SqlCommand("displayData", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult searchDataa(Emp_Insert obj)
        {
            string Searchdata = "";
            con.Open();
            SqlCommand com = new SqlCommand("searchProc", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ids", obj.A);
            com.Parameters.AddWithValue("@search", obj.B);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            Searchdata = JsonConvert.SerializeObject(dt);
            return Json(Searchdata, JsonRequestBehavior.AllowGet);
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

        public JsonResult BindState(Emp_Insert obj)
        {
            string statedata = "";
            con.Open();
            SqlCommand com = new SqlCommand("stateproc", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@countryid", obj.A);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            statedata = JsonConvert.SerializeObject(dt);
            return Json(statedata, JsonRequestBehavior.AllowGet);
        }

        public void Delete(Emp_Insert obj)
        {
            con.Open();
            SqlCommand com = new SqlCommand("Deleteproc", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@u_id", obj.A);
            com.ExecuteNonQuery();
            con.Close();
        }

        public JsonResult Edit(Emp_Insert obj)
        {
            string data = "";
            con.Open();
            SqlCommand com = new SqlCommand("Editproc", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@u_id", obj.B);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}