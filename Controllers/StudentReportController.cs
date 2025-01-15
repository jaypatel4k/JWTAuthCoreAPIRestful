using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models.StudentResultModel;
using JWTAuthCoreAPIRestful.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;

namespace JWTAuthCoreAPIRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentReportController : ControllerBase
    {
        private readonly IStudentReportRepository _studentReportRepository;
        public StudentReportController(IStudentReportRepository studentReportRepository)
        {
            _studentReportRepository = studentReportRepository;
        }


        [Route("UploadStudentMarks")]
        [HttpPost]
        public async Task<IActionResult> InsertStudentMarks(IFormFile file, int testTypeId,int monthId,int yearId,int standardId, int divisionId,
            int streamId,int testHeldOfMarkId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty.");
            }

            try
            {
                var data = new List<Dictionary<string, string>>();
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
                        DataTable dtMarks = CreateTempTable();
                        DataRow dataRow;
                        var rowDataSubject = new Dictionary<int, string>();
                        int cnt = 0;
                        int studentID = 0;
                        foreach (var row in rows.Skip(3))
                        {
                            var student = new Student();
                            var studentMark = new StudentMark();
                            var studentsubject = new Subject();
                            StringBuilder strRemark =  new StringBuilder();
                            cnt = 3;
                            foreach (var cell in row.Cells())
                            {
                                if (row.RowNumber() == 4 && cell.Address.ColumnNumber > 3 && !cell.Value.IsBlank && cell.Value.GetText() != string.Empty)
                                {
                                    cnt = cnt + 1;
                                    rowDataSubject.Add(cnt, cell.Value.ToString());
                                }
                                if(row.RowNumber() > 5 )
                                {
                                    if (cell.Address.ColumnNumber == 2)
                                    {
                                        student = await _studentReportRepository.GetStudentByNameAsync(cell.Value.GetText().ToUpper());
                                        if (student == null)
                                        {
                                            return NotFound("Student not exist in databse. == " + cell.Value.GetText());
                                        }
                                        else
                                        {
                                            
                                            studentID = student.Id;
                                        }
                                    }
                                    if (cell.Address.ColumnNumber > 3 && rowDataSubject[cell.Address.ColumnNumber] != "REMARKS")
                                    {
                                        studentsubject = await _studentReportRepository.GetSubjectByNameAsync(rowDataSubject[cell.Address.ColumnNumber]);
                                        if (studentsubject == null)
                                            return NotFound( rowDataSubject[cell.Address.ColumnNumber] + "not exist");
                                        studentMark.Remarks = string.Empty;
                                        studentMark.StudentId = studentID;
                                        studentMark.SubjectId = studentsubject.Id;
                                        studentMark.TestTypeId = testTypeId;
                                        studentMark.MonthId = monthId;
                                        studentMark.YearId = yearId;
                                        studentMark.StandardId = standardId;
                                        studentMark.DivisionId = divisionId;
                                        studentMark.StreamId = streamId;//cell.Value.GetText() == "AB"
                                        studentMark.TestHeldOfMarkId = testHeldOfMarkId;
                                        if (cell.Value.IsNumber)
                                        {
                                            studentMark.Marks = (decimal)cell.Value.GetNumber();
                                        }
                                        else if (cell.Value.IsText)
                                        {
                                            studentMark.Marks = 0;
                                            studentMark.Remarks = cell.Value.GetText();
                                        }
                                        else if(cell.Value.IsBlank)
                                        {
                                            studentMark.Marks = 0;
                                        }
                                        dataRow = dtMarks.NewRow();
                                        dataRow["TestTypeId"] = studentMark.TestTypeId;
                                        dataRow["MonthId"] = studentMark.MonthId;
                                        dataRow["YearId"] = studentMark.YearId;
                                        dataRow["StandardId"] = studentMark.StandardId;
                                        dataRow["DivisionId"] = studentMark.DivisionId;
                                        dataRow["StreamId"] = studentMark.StreamId;
                                        dataRow["StudentId"] = studentMark.StudentId;
                                        dataRow["SubjectId"] = studentMark.SubjectId;
                                        dataRow["TestHeldOfMarkId"] = studentMark.TestHeldOfMarkId;
                                        dataRow["Marks"] = studentMark.Marks;
                                        dataRow["Remarks"] = studentMark.Remarks;

                                        dtMarks.Rows.Add(dataRow);
                                    }
                                    
                                }
                                
                            }// End Of Cell for each
                            
                        }//End Of Row for each
                        await _studentReportRepository.BeginTransaction();
                        StudentMark studMarks;
                        if (dtMarks.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtMarks.Rows)
                            {
                                studMarks = new StudentMark();
                                studMarks.TestTypeId = Convert.ToInt32(row["TestTypeId"]);
                                studMarks.MonthId = Convert.ToInt32(row["MonthId"]);
                                studMarks.YearId = Convert.ToInt32(row["YearId"]);
                                studMarks.StandardId = Convert.ToInt32(row["StandardId"]);
                                studMarks.DivisionId = Convert.ToInt32(row["DivisionId"]);
                                studMarks.StreamId = Convert.ToInt32(row["StreamId"]);
                                studMarks.StudentId = Convert.ToInt32(row["StudentId"]);
                                studMarks.SubjectId = Convert.ToInt32(row["SubjectId"]);
                                studMarks.TestHeldOfMarkId = Convert.ToInt32(row["TestHeldOfMarkId"]);
                                studMarks.Marks = Convert.ToDecimal(row["Marks"]);
                                studMarks.Remarks = row["Remarks"] as string;
                                await  _studentReportRepository.AddStudentMarkAsync(studMarks);
                            }
                        }
                        await _studentReportRepository.CommitTransaction();
                    }
                }
                return Ok();
            }
            catch(Exception ex)
            {
                await _studentReportRepository.RollBackTrasaction();
                return BadRequest(ex.Message);
            }
        }

        private DataTable CreateTempTable()
        {
            DataTable markTable = new("tblMarks");
            DataColumn dtColumn;

            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "TestTypeId",
            };
            markTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "MonthId",
            };
            markTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "YearId",
            };
            markTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "StandardId",
            };
            markTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "DivisionId",
            };
            markTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "StreamId",
            };
            markTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "StudentId",
            };
            markTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "SubjectId",
            };
            markTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn
            {
                DataType = typeof(Int32),
                ColumnName = "TestHeldOfMarkId",
            };
            markTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn
            {
                DataType = typeof(decimal),
                ColumnName = "Marks",
            };
            markTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn
            {
                DataType = typeof(String),
                ColumnName = "Remarks",
            };
            markTable.Columns.Add(dtColumn);

            return markTable;
        }

        [Route("UploadStudent")]
        [HttpPost]
        public async Task<IActionResult> InsertUpdateStudent(IFormFile file,[FromQuery] string divisionId,[FromQuery] string standadId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty.");
            }
            try
            {
               await _studentReportRepository.BeginTransaction();

                var data = new List<Dictionary<string, string>>();

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

                        int cnt = 0;
                        foreach (var row in rows.Skip(1))
                        {
                            var rowData = new Dictionary<int, string>();
                            var student = new Student();
                            cnt = 0;
                            foreach (var cell in row.Cells())
                            {
                                cnt = cnt + 1;
                                rowData.Add(cnt, cell.Value.ToString());
                            }
                            student.Name = rowData[1];
                            student.RollNo =Convert.ToInt32(rowData[2]);
                            student.DOB = DateTime.Now;
                            student.DivisionId =Convert.ToInt32(divisionId);
                            student.StandId = Convert.ToInt32(standadId);

                            var studentExist =await _studentReportRepository.GetStudentByNameAsync(student.Name);
                            if(studentExist == null)
                            {
                               await _studentReportRepository.AddStudentAsync(student);
                            }
                        }
                    }
                }
                await _studentReportRepository.CommitTransaction();
                return Ok("{\"success\": \"Data uploaded Successfully\"}");
            }
            catch (Exception ex)
            {
                await _studentReportRepository.RollBackTrasaction();
                return BadRequest(ex.Message);
            }
        }

    }
}
