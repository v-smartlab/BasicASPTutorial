using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using BasicASPTutorial.Models.Reports;
using System.Xml;

namespace BasicASPTutorial.Controllers
{
    public class GenReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private IConfiguration Configuration;

        public GenReportController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            Configuration = configuration;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public DataTable GetData(string sqlstr)
        {
            string constr = Configuration.GetConnectionString("DefaultConnection");
            DataTable ds = new DataTable();
            //string sql = sqlstr; 
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sqlstr, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(ds);
            return ds;
        }

        public IActionResult StudentList(int repType,
            string idtxt,
            string nametxt,
            string Score,
            string DateUpdate)
        {   
            var _Id = "";
            if (!string.IsNullOrEmpty(idtxt))
            {
                _Id = $"and a.Id = {idtxt.Trim()}";
            }
            var _Name = ""; 
            if (!string.IsNullOrEmpty(nametxt))
            {
                _Name = $"and a.Name = '{nametxt.Trim()}'";
            }
            /*
            var prdStr = $"ระหว่างวันที่ : {amlStd} ถึง {amlLsd}";

            var _amlCode = "";
            if (!string.IsNullOrEmpty(amlCode))
            {
                _amlCode = $"and a.Code like '%{amlCode.Trim()}%'";
            }
            var _amlName = "";
            if (!string.IsNullOrEmpty(amlName))
            {
                _amlName = $"and a.Name like '%{amlName.Trim()}%'";
            }
            var _stdStr = "";
            if (!string.IsNullOrEmpty(amlStd))
            {
                string[] abc = amlStd.Split('/');
                var str = abc[2] + "-" + abc[1] + "-" + abc[0];
                _stdStr = $"and a.OpenDate >= '{str}'";
            }
            var _lsdStr = "";
            if (!string.IsNullOrEmpty(amlLsd))
            {
                string[] abc = amlLsd.Split('/');
                var str = abc[2] + "-" + abc[1] + "-" + abc[0];
                _lsdStr = $"and a.OpenDate <= '{str}'";
            }
            var _opcStr = "";
            if (!string.IsNullOrEmpty(amlOpenClose))
            {
                _opcStr = $"and a.OpenClose = '{amlOpenClose}'";
            } */

            var strCondition = $"{_Id} {_Name}";
          //  var strCondition = "";
            string sqlstr = "SELECT * " +
                         "FROM students a " +
                        $"WHERE 1 = 1 {strCondition} order by a.Id"; 
            var data = GetData(sqlstr);

           // var reportParam = GetReportAmulet(sqlstr, prdStr);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            LocalReport localReport;
            var path = "";
            var dt = new DataTable();
             dt = data;
             if (dt.Rows.Count > 0)
             {
                var string1 = "Search By {" + strCondition + "}";
                var string2 = "12345";
                parameters.Add("ReportParameter1", string1);
                 parameters.Add("ReportParameter2", string2); 

                 path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\" + "StudentList.rdlc";
                //path = $"C:\\inetpub\\AspNetCoreWebApps\\MainApp\\wwwroot\\Reports\\StudentList.rdlc";
                localReport = new LocalReport(path);
                 localReport.AddDataSource("DsStudent", dt);
             } 
             else 
             {
                //parameters.Add("ReportParameter1", "Parameter1");
                path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\blank.rdlc";
                 localReport = new LocalReport(path);
                 localReport.AddDataSource("", new DataTable());
             }
           

            var _renderType = new RenderType();
            string _contentType = "";
            switch (repType)
            {
                case 1:
                    _renderType = RenderType.Pdf;
                    _contentType = "application/pdf"; break;
                case 2:
                    _renderType = RenderType.Excel;
                    _contentType = "application/vnd.ms-excel"; break;
                case 3:
                    _renderType = RenderType.Word;
                    _contentType = "application/msword"; break;
            }
            string minetype = "";
            int extension = 1;
            var result = localReport.Execute(_renderType, extension, parameters, minetype);
            return File(result.MainStream, _contentType);
        }
        /*
        public ReportParams GetReportAmulet(string sqlstr, string prdStr)
        {
            ReportParams objReportParams = new ReportParams();
            var data = GetData(sqlstr);
            objReportParams.RptDataSource = data;
            objReportParams.RptTitle = "Amulet List";
            objReportParams.RptFileName = "AmuletList.rdlc";
            objReportParams.RptRenderType = "Pdf";
            objReportParams.RptDataSetName = "DataSet1";
            objReportParams.IsHasParams = true;
            // Set Report Parameter if objReportParams.IsHasParams = true;
            objReportParams.RptComName = "บริษัท ทดลอง จำกัด"; // GetComName();
            objReportParams.RptName = "รายงาน";
            objReportParams.RptDate = DateTime.Now.ToString("dd-MMM-yyyy");
            objReportParams.RptPeriod = prdStr;
            objReportParams.RptCategory = "ประเภท : ";
            return objReportParams;
        } */

    }
}
