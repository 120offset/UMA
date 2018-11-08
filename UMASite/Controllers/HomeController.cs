using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMASite.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UMASite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Location()
        {
            string markers = "[";
            string CS = ConfigurationManager.ConnectionStrings["UMADB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetMap", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    markers += "{";
                    markers += string.Format("'title': '{0}',", sdr["Tag"]);
                    markers += string.Format("'lat': '{0}',", sdr["Latitude"]);
                    markers += string.Format("'lng': '{0}',", sdr["Longitude"]);
                    markers += string.Format("'description': '{0}'", sdr["Description"]);
                    markers += "},";
                }
            }
            markers += "];";
            ViewBag.Markers = markers;
            return View();
        }
        [HttpPost]
        public ActionResult Location(Locations location)
        {
            if (ModelState.IsValid)
            {
                string CS = ConfigurationManager.ConnectionStrings["UMADB"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spAddNewLocation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Tag", location.Tag);
                    cmd.Parameters.AddWithValue("@Latitude", location.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", location.Longitude);
                    cmd.Parameters.AddWithValue("@Description", location.Description);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {

            }
            return RedirectToAction("Location");
        }
    }
}