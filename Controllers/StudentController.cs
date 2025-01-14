using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
using System.Threading;

namespace JWTAuthCoreAPIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [Route("UploadStudentData")]
        [HttpPost]
        public async Task<IActionResult> UploadStudentMarkExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty.");
            }

            var data = new List<Dictionary<string, string>>();
            var test_month_year = new List<Dictionary<string, string>>();
            var std_div_stream = new List<Dictionary<string, string>>();
            var subject_remark = new List<Dictionary<string, string>>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0; // Reset the stream position to the beginning

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheets.First();
                    var range = worksheet.RangeUsed();
                    if (range == null)
                    {
                        return BadRequest("No data found in the Excel file.");
                    }
                    var rows = range.RowsUsed();

                    var headerRow = rows.First(); // Assumes the first row is the header row
                    var headers = headerRow.Cells().Select(c => c.Value.ToString()).ToList();
                    //Added


                    foreach (var row in rows.Skip(1))
                    {
                        var rowData = new Dictionary<string, string>();
                        foreach (var cell in row.Cells())
                        {
                            var header = headers[cell.Address.ColumnNumber - 1];
                            rowData[header] = cell.Value.ToString();
                        }
                        data.Add(rowData);
                    }
                }
            }

            return Ok(data);
        }
        
    }
}