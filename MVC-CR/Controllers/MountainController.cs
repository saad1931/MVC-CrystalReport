using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CR.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;
using System.Reflection;

namespace MVC_CR.Controllers
{
    public class MountainController : Controller
    {
        private Database1Entities db = new Database1Entities();
        // GET: Mountain
        public ActionResult Index()
        {
            return View(db.Tables.ToList());
        }

        public ActionResult ExportMountains()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport1.rpt"));
            rd.SetDataSource(ListToDataTable(db.Tables.ToList()));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "MountainList.pdf");
            }
            catch
            {
                throw;
            }

        }
        private DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable DataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(PropertyInfo prop in props)
            {
                DataTable.Columns.Add(prop.Name);
            }
            foreach(T item in items)
            {
                var values = new object[props.Length];
                for(int i=0 ; i<props.Length ;  i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                DataTable.Rows.Add(values);
            }
            return DataTable;
        }
    }
}