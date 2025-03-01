using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using JWTAuthCoreAPIRestful.Data;
using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models.StudentResultModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace JWTAuthCoreAPIRestful.Repository
{
    public class StudentReportRepository : IStudentReportRepository
    {
        private readonly JWTAuthCRUDContext _dbcontext;
        public StudentReportRepository(JWTAuthCRUDContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task BeginTransaction()
        {
            await _dbcontext.Database.BeginTransactionAsync();
        }
        public async Task RollBackTrasaction()
        {
            if (_dbcontext.Database.CurrentTransaction != null)
            {
                await _dbcontext.Database.RollbackTransactionAsync();
            }
        }

        public async Task CommitTransaction()
        {
            await _dbcontext.Database.CommitTransactionAsync();
        }


        public async Task<IEnumerable<Division>> GetAllDivisionAsync()
        {
            return await _dbcontext.Division.ToListAsync();
        }

        public async Task<Division> GetDivisionByIdAsync(int divisionid)
        {
            return await _dbcontext.Division.FindAsync(divisionid);
        }
        public async Task<Division> GetDivisionByNameAsync(string strDivision)
        {
            return await _dbcontext.Division.FirstOrDefaultAsync(s => s.DivisionName == strDivision);
        }

        public async Task AddDivisionAsync(Division division)
        {
            await _dbcontext.Division.AddAsync(division);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateDivisionAync(Division division)
        {
            _dbcontext.Update(division);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteDivisionAsync(int divisionid)
        {
            var division = await _dbcontext.Division.FindAsync(divisionid);
            if (division != null)
            {
                _dbcontext.Division.Remove(division);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Standard>> GetAllStandardAsync()
        {
            return await _dbcontext.Standard.ToListAsync();
        }

        public async Task<Standard> GetStandardByIdAsync(int standardid)
        {
            return await _dbcontext.Standard.FindAsync(standardid);
        }

        public async Task AddStandardAsync(Standard standard)
        {
            await _dbcontext.Standard.AddAsync(standard);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStandardAync(Standard standard)
        {
            _dbcontext.Update(standard);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStandardAsync(int standardid)
        {
            var standard = await _dbcontext.Standard.FindAsync(standardid);
            if (standard != null)
            {
                _dbcontext.Standard.Remove(standard);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StreamType>> GetAllStreamTypeAsync()
        {
            return await _dbcontext.StreamType.ToListAsync();
        }

        public async Task<StreamType> GetStreamTypeByIdAsync(int streamTypeid)
        {
            return await _dbcontext.StreamType.FindAsync(streamTypeid);
        }

        public async Task AddStreamTypeAsync(StreamType streamType)
        {
            await _dbcontext.StreamType.AddAsync(streamType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStreamTypeAync(StreamType streamType)
        {
            _dbcontext.Update(streamType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStreamTypeAsync(int streamTypeid)
        {
            var streamType = await _dbcontext.StreamType.FindAsync(streamTypeid);
            if (streamType != null)
            {
                _dbcontext.StreamType.Remove(streamType);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentAsync()
        {
            return await _dbcontext.Student.ToListAsync();
        }

        public async Task<IEnumerable<StudentDTO>> GetStudentByStandardAndDivisionAsync(int divisionId, int standadId)
        {
            //return await _dbcontext.Student.ToListAsync();
            List<Student> listStud =await _dbcontext.Student.ToListAsync();

            List<Standard> listStan = await _dbcontext.Standard.ToListAsync();

            List<Division> listDiv = await _dbcontext.Division.ToListAsync();

            var result = from stud in listStud
                         join stand in listStan on stud.StandId equals stand.Id
                         join div in listDiv on stud.DivisionId equals div.Id
                         where stand.Id == standadId && div.Id == divisionId
                         select new StudentDTO
                         {
                             Name = stud.Name,
                             RollNo = stud.RollNo
                         };
            return result.ToList();
        }

        public async Task<Student> GetStudentByIdAsync(int studentid)
        {
            return await _dbcontext.Student.FindAsync(studentid);
        }

        public async Task<Student> GetStudentByNameAsync(string studentName)
        {
            return await _dbcontext.Student.FirstOrDefaultAsync(s => s.Name == studentName);
        }

        public async Task<Student?> GetStudentByNameStandardAndDivisionAsync(string studentName, int standardId, int divisionId)
        {
            return await _dbcontext.Student.FirstOrDefaultAsync(s => s.Name == studentName && s.DivisionId == divisionId && s.StandId == standardId);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _dbcontext.Student.AddAsync(student);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStudentAync(Student student)
        {
           // _dbcontext.Entry(student).State = EntityState.Modified;
            _dbcontext.Update(student);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int studentid)
        {
            var student = await _dbcontext.Student.FindAsync(studentid);
            if (student != null)
            {
                _dbcontext.Student.Remove(student);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StudentMark>> GetAllStudentMarkAsync()
        {
            return await _dbcontext.StudentMark.ToListAsync();
        }

        public async Task<StudentMark> GetStudentMarkByIdAsync(int studentMarkid)
        {
            return await _dbcontext.StudentMark.FindAsync(studentMarkid);
        }
        public async Task<StudentMark> GetExistingStudentMarkIfAny(int subjectid,int studentid, int testTypeId, int monthId, int yearId, int standardId, int divisionId)
        {
            var data = from s in _dbcontext.StudentMark
                       where s.StudentId == studentid && s.TestTypeId == testTypeId && s.MonthId == monthId && s.YearId == yearId
                           && s.StandardId == standardId && s.DivisionId == divisionId && s.SubjectId == subjectid
                       select s;
            return await data.FirstOrDefaultAsync();
        }
        public async Task AddStudentMarkAsync(StudentMark studentMark)
        {
            await _dbcontext.StudentMark.AddAsync(studentMark);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStudentMarkAync(StudentMark studentMark)
        {
            _dbcontext.Update(studentMark);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStudentMarkAsync(int studentMarkid)
        {
            var studentMark = await _dbcontext.StudentMark.FindAsync(studentMarkid);
            if (studentMark != null)
            {
                _dbcontext.StudentMark.Remove(studentMark);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectAsync()
        {
            return await _dbcontext.Subject.ToListAsync();
        }

        public async Task<Subject> GetSubjectByIdAsync(int subjectid)
        {
            return await _dbcontext.Subject.FindAsync(subjectid);
        }

        public async Task<Subject> GetSubjectByNameAsync(string subjectName)
        {
            return await _dbcontext.Subject.FirstOrDefaultAsync(s => s.SubjectName == subjectName);
        }

       
        
        public async Task AddSubjectAsync(Subject subject)
        {
            await _dbcontext.Subject.AddAsync(subject);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateSubjectAync(Subject subject)
        {
            _dbcontext.Update(subject);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteSubjectAsync(int subjectid)
        {
            var subject = await _dbcontext.Subject.FindAsync(subjectid);
            if (subject != null)
            {
                _dbcontext.Subject.Remove(subject);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TestHeldOfMarkDTO>> GetAllTestHeldOfMarkAsync()
        {
            var allresult = await _dbcontext.TestHeldOfMark.ToListAsync();
            var result = (from t in allresult
                          select new TestHeldOfMarkDTO
                          {
                              Id = t.Id,
                              OutOfMark = t.OutOfMark
                          }).ToList();
            return result;

        }

        public async Task<TestHeldOfMark> GetTestHeldOfMarkByIdAsync(int testHeldOfMarkid)
        {
            return await _dbcontext.TestHeldOfMark.FindAsync(testHeldOfMarkid);
        }

        public async Task AddTestHeldOfMarkAsync(TestHeldOfMark testHeldOfMark)
        {
            await _dbcontext.TestHeldOfMark.AddAsync(testHeldOfMark);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateTestHeldOfMarkAync(TestHeldOfMark testHeldOfMark)
        {
            _dbcontext.Update(testHeldOfMark);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteTestHeldOfMarkAsync(int testHeldOfMarkid)
        {
            var testHeldOfMark = await _dbcontext.TestHeldOfMark.FindAsync(testHeldOfMarkid);
            if (testHeldOfMark != null)
            {
                _dbcontext.TestHeldOfMark.Remove(testHeldOfMark);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TestType>> GetAllTestTypeAsync()
        {
            return await _dbcontext.TestType.ToListAsync();
        }

        public async Task<TestType> GetTestTypeByIdAsync(int testTypeid)
        {
            return await _dbcontext.TestType.FindAsync(testTypeid);
        }

        public async Task<TestType> GetTestTypeByNameAsync(string testType)
        {
            return await _dbcontext.TestType.FirstOrDefaultAsync(s => s.TestTypeName == testType);
        }

        public async Task AddTestTypeAsync(TestType testType)
        {
            await _dbcontext.TestType.AddAsync(testType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateTestTypeAync(TestType testType)
        {
            _dbcontext.Update(testType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteTestTypeAsync(int testTypeid)
        {
            var testType = await _dbcontext.TestType.FindAsync(testTypeid);
            if (testType != null)
            {
                _dbcontext.TestType.Remove(testType);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Month>> GetMonthAsync()
        {
            return await _dbcontext.Month.ToListAsync();
        }
        public async Task<IEnumerable<Year>> GetYearAsync()
        {
            return await _dbcontext.Year.ToListAsync();
        }


        //RANK By Class
        public async Task<IEnumerable<TopRankInClassDTO>> GetTopThreeRankInClass(int testTypeId, int monthId, int yearId, int standardId, int divisionId, int streamId)
        {
            List<Student> listStud = await _dbcontext.Student.ToListAsync();
            List<StudentMark> listMark = await _dbcontext.StudentMark.ToListAsync();
            List<Subject> listSubject = await _dbcontext.Subject.ToListAsync();

            var result = (from stud in listStud
                          join mark in listMark on stud.Id equals mark.StudentId
                          join sub in listSubject on mark.SubjectId equals sub.Id
                          where mark.StandardId == standardId && mark.DivisionId == divisionId
                          && mark.TestTypeId == testTypeId && mark.MonthId == monthId && mark.YearId == yearId && mark.StreamId == streamId
                          group new { stud, mark } by new { stud.Name, stud.RollNo } into g
                          select new TopRankInClassDTO
                          {
                              RollNo = g.Key.RollNo,
                              Name = g.Key.Name,
                              Rank = 0 ,// Placeholder for rank
                              Total = (int)g.Sum(x => x.mark.Marks)
                          }).OrderByDescending(x => x.Total).ToList();

            var top3Marks = (from m in result
                             orderby m.Total descending
                             select new
                             {
                                 top3mark = m.Total
                             }).Take(3).Distinct().ToList();
            var top3Result = (from m in result
                              join t in top3Marks on m.Total equals t.top3mark
                              select m).Distinct().ToList();
            int pevmark = 0;
            int rank = 1;
            // Assign ranks
            for (int i = 0; i < top3Result.Count; i++)
            {
               if (pevmark < result[i].Total)
                {
                    result[i].Rank = rank;
                    pevmark = result[i].Total;
                }
               else if(pevmark > result[i].Total)
                {
                    rank++;
                    result[i].Rank = rank;
                    pevmark = result[i].Total;
                }
                else if (pevmark == result[i].Total)
                {
                    result[i].Rank = rank;
                    pevmark = result[i].Total;
                }

            }

            return top3Result;
        }
        public async Task<IEnumerable<TopRankInClassBySubjectNoRank>> GetTopRankBySubjectInClass(int testTypeId, int monthId, int yearId, int standardId, int divisionId, int streamId)
        {
            List<Student> listStud = await _dbcontext.Student.ToListAsync();
            List<StudentMark> listMark = await _dbcontext.StudentMark.ToListAsync();
            List<Subject> listSubject = await _dbcontext.Subject.ToListAsync();

            var result = (from stud in listStud
                          join mark in listMark on stud.Id equals mark.StudentId
                          join sub in listSubject on mark.SubjectId equals sub.Id
                          where mark.StandardId == standardId && mark.DivisionId == divisionId
                          && mark.TestTypeId == testTypeId && mark.MonthId == monthId && mark.YearId == yearId && mark.StreamId == streamId
                          group new { stud, mark, sub } by new { stud.Name, stud.RollNo, sub.SubjectName } into g
                          select new TopRankInClassBySubject
                          {
                              RollNo = g.Key.RollNo,
                              Name = g.Key.Name,
                              Subject = g.Key.SubjectName,
                              Total = (int)g.Max(x => x.mark.Marks),
                              Rank = 0 // Placeholder for rank
                          }).OrderByDescending(x => x.Total).ToList();

            // Assign ranks
            var groupedResult = from p in result
                                group p by new { p.Subject } into g
                                select g.OrderByDescending(x => x.Total);
            foreach (var group in groupedResult)
            {
                int rank = 1;
                var mxMark = group.Max(x => x.Total);
                foreach (var item in group)
                {
                   if(item.Total == mxMark)
                        item.Rank = rank;
                   else
                        item.Rank = 0;
                }
                
            }

            //return result.Where(x => x.Rank == 1);
            var result1= result.OrderByDescending(y => y.Total).ThenBy(x => x.Subject).ToList().Where(x => x.Rank == 1);
            var fresult = (from r in result1
                           select new TopRankInClassBySubjectNoRank
                           {
                               RollNo = r.RollNo,
                               Name = r.Name,
                               Subject = r.Subject,
                               Marks = r.Total
                           });
            return fresult;
        }

        //Rank By AllDivision
        public async Task<IEnumerable<TopThreeRankInAllDivision>> GetTopThreeRankInAllDivision(int testTypeId, int monthId, int yearId, int standardId, int streamId)
        {
            List<Student> listStud = await _dbcontext.Student.ToListAsync();
            List<StudentMark> listMark = await _dbcontext.StudentMark.ToListAsync();
            List<Subject> listSubject = await _dbcontext.Subject.ToListAsync();
            List<Division> listDivision = await _dbcontext.Division.ToListAsync();

            var result = (from stud in listStud
                          join mark in listMark on stud.Id equals mark.StudentId
                          join sub in listSubject on mark.SubjectId equals sub.Id
                          join div in listDivision on stud.DivisionId equals div.Id
                          where mark.StandardId == standardId 
                          && mark.TestTypeId == testTypeId && mark.MonthId == monthId && mark.YearId == yearId && mark.StreamId == streamId
                          group new { stud, mark, div} by new { stud.Name, stud.RollNo, div.DivisionName } into g
                          select new TopThreeRankInAllDivision
                          {
                              RollNo = g.Key.RollNo,
                              Name = g.Key.Name,
                              Div = g.Key.DivisionName,
                              Rank = 0,// Placeholder for rank
                              Total = (int)g.Sum(x => x.mark.Marks)
                          }).OrderByDescending(x => x.Total).ToList();

            var top3Marks = (from m in result
                             orderby m.Total descending
                             select new
                             {
                                 top3mark = m.Total
                             }).Take(3).Distinct().ToList();
            var top3Result = (from m in result
                              join t in top3Marks on m.Total equals t.top3mark
                              select m).Distinct().ToList();
            int pevmark = 0;
            int rank = 1;
            // Assign ranks
            for (int i = 0; i < top3Result.Count; i++)
            {
                if (pevmark < result[i].Total)
                {
                    result[i].Rank = rank;
                    pevmark = result[i].Total;
                }
                else if (pevmark > result[i].Total)
                {
                    rank++;
                    result[i].Rank = rank;
                    pevmark = result[i].Total;
                }
                else if (pevmark == result[i].Total)
                {
                    result[i].Rank = rank;
                    pevmark = result[i].Total;
                }

            }

            return top3Result;
        }
        public async Task<IEnumerable<TopRankInAllDivisionBySubjectNoRank>> GetTopRankBySubjectInAllDivision(int testTypeId, int monthId, int yearId, int standardId, int streamId)
        {
            List<Student> listStud = await _dbcontext.Student.ToListAsync();
            List<StudentMark> listMark = await _dbcontext.StudentMark.ToListAsync();
            List<Subject> listSubject = await _dbcontext.Subject.ToListAsync();
            List<Division> listDivision = await _dbcontext.Division.ToListAsync();

            var result = (from stud in listStud
                          join mark in listMark on stud.Id equals mark.StudentId
                          join sub in listSubject on mark.SubjectId equals sub.Id
                          join div in listDivision on stud.DivisionId equals div.Id
                          where mark.StandardId == standardId
                          && mark.TestTypeId == testTypeId && mark.MonthId == monthId && mark.YearId == yearId && mark.StreamId == streamId
                          group new { stud, mark, div, sub } by new { stud.Name, stud.RollNo, div.DivisionName, sub.SubjectName } into g
                          select new TopRankInAllDivisionBySubject
                          {
                              RollNo = g.Key.RollNo,
                              Name = g.Key.Name,
                              Div = g.Key.DivisionName,
                              Subject = g.Key.SubjectName,
                              Marks = (int)g.Max(x => x.mark.Marks),
                              Rank = 0 // Placeholder for rank
                          }).OrderByDescending(x => x.Marks).ToList();
            // Assign ranks
            var groupedResult = from p in result
                                group p by new { p.Subject } into g
                                select g.OrderByDescending(x => x.Marks);
            foreach (var group in groupedResult)
            {
                int rank = 1;
                var mxMark = group.Max(x => x.Marks);
                foreach (var item in group)
                {
                    if (item.Marks == mxMark)
                        item.Rank = rank;
                    else
                        item.Rank = 0;
                }

            }

            var result1 = result.OrderByDescending(y => y.Marks).ThenBy(x => x.Subject).ToList().
                        Where(x => x.Rank == 1);
            var fresult = (from r in result1
                           select new TopRankInAllDivisionBySubjectNoRank
                           {
                               RollNo = r.RollNo,
                               Name = r.Name,
                               Div = r.Div,
                               Subject = r.Subject,
                               Marks = r.Marks
                           });
            return fresult;
        }

        public async Task<IEnumerable<TopThreeRankInAllDivision>> GetFirstSecondThirdRankInAllDivision(int testTypeId, int monthId, int yearId, int standardId, int streamId)
        {
            List<Student> listStud = await _dbcontext.Student.ToListAsync();
            List<StudentMark> listMark = await _dbcontext.StudentMark.ToListAsync();
            List<Subject> listSubject = await _dbcontext.Subject.ToListAsync();
            List<Division> listDivision = await _dbcontext.Division.ToListAsync();

            var result = (from stud in listStud
                          join mark in listMark on stud.Id equals mark.StudentId
                          join sub in listSubject on mark.SubjectId equals sub.Id
                          join div in listDivision on stud.DivisionId equals div.Id
                          where mark.StandardId == standardId
                          && mark.TestTypeId == testTypeId && mark.MonthId == monthId && mark.YearId == yearId && mark.StreamId == streamId
                          group new { stud, mark, div } by new { stud.Name, stud.RollNo, div.DivisionName } into g
                          select new TopThreeRankInAllDivision
                          {
                              RollNo = g.Key.RollNo,
                              Name = g.Key.Name,
                              Div = g.Key.DivisionName,
                              Rank = 0,// Placeholder for rank
                              Total = (int)g.Sum(x => x.mark.Marks)
                          }).OrderByDescending(x => x.Total).ToList();

            var distinctDivision = result.DistinctBy(x => x.Div).ToList().Select(y => y.Div).ToList();
            Dictionary<string, List<TopThreeRankInAllDivision>> mydictmydict = new Dictionary<string, List<TopThreeRankInAllDivision>>();
            for (int i = 0; i < distinctDivision.Count; i++)
            {
                var test = result.Where(x => x.Div == distinctDivision[i])
                            .OrderByDescending(x => x.Total).Take(3).ToList();
                    int cnt = 1;
                    for (int j = 0; j < test.Count(); j++)
                    {
                        test[j].Rank = cnt++;
                    }
                mydictmydict.Add(distinctDivision[i], test);
            }


            return result.Where(x => x.Rank > 0).OrderBy(y => y.Rank).ThenBy(d=>d.Div).ToList();
            
        }

        public async Task<IEnumerable<TopRankInAllDivisionBySubjectNoRank>> GetHighestInAllSubjectInAllDivision(int testTypeId, int monthId, int yearId, int standardId, int streamId)
        {
            List<Student> listStud = await _dbcontext.Student.ToListAsync();
            List<StudentMark> listMark = await _dbcontext.StudentMark.ToListAsync();
            List<Subject> listSubject = await _dbcontext.Subject.ToListAsync();
            List<Division> listDivision = await _dbcontext.Division.ToListAsync();

            var result = (from stud in listStud
                          join mark in listMark on stud.Id equals mark.StudentId
                          join sub in listSubject on mark.SubjectId equals sub.Id
                          join div in listDivision on stud.DivisionId equals div.Id
                          where mark.StandardId == standardId
                          && mark.TestTypeId == testTypeId && mark.MonthId == monthId && mark.YearId == yearId && mark.StreamId == streamId
                          group new { stud, mark, div, sub } by new { stud.Name, stud.RollNo, div.DivisionName, sub.SubjectName } into g
                          select new TopRankInAllDivisionBySubject
                          {
                              RollNo = g.Key.RollNo,
                              Name = g.Key.Name,
                              Div = g.Key.DivisionName,
                              Subject = g.Key.SubjectName,
                              Marks = (int)g.Max(x => x.mark.Marks),
                              Rank = 0 // Placeholder for rank
                          }).OrderByDescending(x => x.Marks).ToList();
            // Assign ranks
            var groupedResult = from p in result
                                group p by new {p.Div, p.Subject } into g
                                select g.OrderByDescending(x => x.Marks);
            foreach (var group in groupedResult)
            {
                int rank = 1;
                var mxMark = group.Max(x => x.Marks);
                foreach (var item in group)
                {
                    if (item.Marks == mxMark)
                        item.Rank = rank;
                    else
                        item.Rank = 0;
                }

            }

            var result1 = result.OrderByDescending(y => y.Marks).ThenBy(x => x.Subject).ToList().Where(x => x.Rank == 1);
            var fresult = (from r in result1
                           select new TopRankInAllDivisionBySubjectNoRank
                           {
                               RollNo = r.RollNo,
                               Name = r.Name,
                               Div = r.Div,
                               Subject = r.Subject,
                               Marks = r.Marks
                           });
            return fresult;
        }

        public async Task<IEnumerable<IEnumerable<Marks5PercentFinalDTO>>> GetSubjectWise_5Percent_Marks_total(string strGroupA, string strGroupB, int standardId)
        {
            List<Student> listStud = await _dbcontext.Student.ToListAsync();
            List<StudentMark> listMark = await _dbcontext.StudentMark.ToListAsync();
            List<Subject> listSubject = await _dbcontext.Subject.ToListAsync();
            List<TestType> listTestType = await _dbcontext.TestType.ToListAsync();
            List<Division> listDivision = await _dbcontext.Division.ToListAsync();
            List<Standard> listStandard = await _dbcontext.Standard.ToListAsync();

            List<List<Marks5PercentFinalDTO>> objMarks = new List<List<Marks5PercentFinalDTO>>();
            string[] aTypeGroupA = strGroupA.Split(",");
            string[] aTypeGroupB= strGroupB.Split(",");
            StringBuilder sb = new StringBuilder();
            StringBuilder sbTestA = new StringBuilder();
            StringBuilder sbTestB = new StringBuilder();
            StringBuilder sbDivName = new StringBuilder();

            var standardresult = listStandard.Where(x => x.Id == standardId).FirstOrDefault();
            var divlistresult = (from m in listMark
                                 join d in listDivision on m.DivisionId equals d.Id
                                 where m.StandardId == standardId
                                 select new Division
                                 {
                                     Id = d.Id,
                                     DivisionName = d.DivisionName
                                 }).DistinctBy(y=>y.Id).ToList();
            var sublistresult = (from m in listMark
                                 join s in listSubject on m.SubjectId equals s.Id
                                 where m.StandardId == standardId
                                 select new Subject
                                 {
                                     Id = s.Id,
                                     SubjectName = s.SubjectName
                                 }).DistinctBy(y => y.Id).ToList();
            foreach (string aType in aTypeGroupA)
            {
                var typeresult = (from p in listTestType
                                  where p.Id == Convert.ToInt32(aType)
                                  select p).FirstOrDefault();
                if (typeresult != null)
                {
                    sbTestA.Append(typeresult.TestTypeName + "_");
                }
            }
            foreach (string aType in aTypeGroupB)
            {
                var typeresult = (from p in listTestType
                                  where p.Id == Convert.ToInt32(aType)
                                  select p).FirstOrDefault();
                if (typeresult != null)
                {
                    sbTestB.Append(typeresult.TestTypeName + "_");
                }
            }

            foreach (var divname in divlistresult)
            {
                sbDivName.Append(divname.DivisionName + "-");
                foreach (var subname in sublistresult)
                {
                    
                    sb = sb.Append(standardresult.StandardName + "-" + divname.DivisionName + "-" + subname.SubjectName + "~");
                    
                    List<TestType> listTestTypeA = new List<TestType>();
                    List<TestType> listTestTypeB = new List<TestType>();
                    List<Marks5Percent1DTO> obj5DTO = new List<Marks5Percent1DTO>();
                    foreach (string aType in aTypeGroupA)
                    {
                        var typeresult = (from p in listTestType
                                          where p.Id == Convert.ToInt32(aType)
                                          select p).FirstOrDefault();
                        if (typeresult != null)
                        {
                            listTestTypeA.Add(typeresult);
                        }
                        var result = (from mark in listMark
                                      join sub in listSubject on mark.SubjectId equals sub.Id
                                      join ttype in listTestType on mark.TestTypeId equals ttype.Id
                                      join stud in listStud on mark.StudentId equals stud.Id
                                      where sub.SubjectName == subname.SubjectName && ttype.Id == Convert.ToInt32(aType)
                                      && mark.DivisionId == divname.Id
                                      select new Marks5Percent1DTO
                                      {
                                          SubjectName = sub.SubjectName,
                                          RollNo = stud.RollNo,
                                          Name = stud.Name,
                                          TestTypeName = ttype.TestTypeName,
                                          Marks = mark.Marks
                                      }).ToList();
                        obj5DTO.AddRange(result);
                    }

                    DataTable pivot = new DataTable();
                    pivot.Columns.Add("SubjectName", typeof(string));
                    pivot.Columns.Add("RollNo", typeof(int));
                    pivot.Columns.Add("Name", typeof(string));
                    foreach (TestType testtype in listTestTypeA)
                    {
                        pivot.Columns.Add(testtype.TestTypeName, typeof(int));
                    }
                    pivot.Columns.Add("Best", typeof(int));

                    var groups = obj5DTO.GroupBy(x => new { subjectname = x.SubjectName, rollno = x.RollNo, name = x.Name }).ToList();

                    foreach (var group in groups)
                    {
                        DataRow newRow = pivot.Rows.Add();
                        newRow["SubjectName"] = group.Key.subjectname;
                        newRow["RollNo"] = group.Key.rollno;
                        newRow["Name"] = group.Key.name;

                        foreach (var row in group)
                        {
                            newRow[row.TestTypeName] = row.Marks;
                        }
                        newRow["Best"] = group.Max(x => x.Marks);
                    }
                    //
                    obj5DTO = new List<Marks5Percent1DTO>();
                    foreach (string aType in aTypeGroupB)
                    {
                        var typeresult = (from p in listTestType
                                          where p.Id == Convert.ToInt32(aType)
                                          select p).FirstOrDefault();
                        if (typeresult != null)
                        {
                            listTestTypeB.Add(typeresult);
                        }
                        var result = (from mark in listMark
                                      join sub in listSubject on mark.SubjectId equals sub.Id
                                      join ttype in listTestType on mark.TestTypeId equals ttype.Id
                                      join stud in listStud on mark.StudentId equals stud.Id
                                      where sub.SubjectName == subname.SubjectName && ttype.Id == Convert.ToInt32(aType)
                                      && mark.DivisionId == divname.Id
                                      select new Marks5Percent1DTO
                                      {
                                          SubjectName = sub.SubjectName,
                                          RollNo = stud.RollNo,
                                          Name = stud.Name,
                                          TestTypeName = ttype.TestTypeName,
                                          Marks = mark.Marks
                                      }).ToList();
                        obj5DTO.AddRange(result);
                    }

                    DataTable pivot1 = new DataTable();
                    pivot1.Columns.Add("SubjectName", typeof(string));
                    pivot1.Columns.Add("RollNo", typeof(int));
                    pivot1.Columns.Add("Name", typeof(string));
                    foreach (TestType testtype in listTestTypeB)
                    {
                        pivot1.Columns.Add(testtype.TestTypeName, typeof(int));
                    }
                    pivot1.Columns.Add("Best1", typeof(int));

                    var groups1 = obj5DTO.GroupBy(x => new { subjectname = x.SubjectName, rollno = x.RollNo, name = x.Name }).ToList();

                    foreach (var group in groups1)
                    {
                        DataRow newRow = pivot1.Rows.Add();
                        newRow["SubjectName"] = group.Key.subjectname;
                        newRow["RollNo"] = group.Key.rollno;
                        newRow["Name"] = group.Key.name;

                        foreach (var row in group)
                        {
                            newRow[row.TestTypeName] = row.Marks;
                        }
                        newRow["Best1"] = group.Max(x => x.Marks);
                    }
                    List<Marks5Test1DTO> lstUnit1 = new List<Marks5Test1DTO>();
                    foreach (DataRow row in pivot.Rows)
                    {
                        Marks5Test1DTO obj = new Marks5Test1DTO();
                        foreach (DataColumn col in pivot.Columns)
                        {

                            //string? stra = row[col];
                            if (col.ColumnName == "SubjectName")
                                obj.SubjectName = Convert.ToString(row[col]);
                            if (col.ColumnName == "RollNo")
                                obj.RollNo = Convert.ToInt32(row[col]);
                            if (col.ColumnName == "Name")
                                obj.Name = Convert.ToString(row[col]);
                            if (col.ColumnName.ToLower().IndexOf("unit") > 0 && col.Ordinal == 3)
                            {
                                obj.Unit1 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }
                            if (col.ColumnName.ToLower().IndexOf("unit") > 0 && col.Ordinal == 4)
                            {
                                obj.Unit2 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }
                            if (col.ColumnName.ToLower().IndexOf("unit") > 0 && col.Ordinal == 5)
                            {
                                obj.Unit3 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }
                            if (col.ColumnName.ToLower().IndexOf("unit") > 0 && col.Ordinal == 6)
                            {
                                obj.Unit4 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }
                            if (col.ColumnName == "Best")
                            {
                                obj.Best = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }

                        }
                        lstUnit1.Add(obj);
                    }
                    List<Marks5Test2DTO> lstUnit2 = new List<Marks5Test2DTO>();
                    foreach (DataRow row in pivot1.Rows)
                    {
                        Marks5Test2DTO obj = new Marks5Test2DTO();
                        foreach (DataColumn col in pivot1.Columns)
                        {
                            //string? stra = row[col];

                            if (col.ColumnName == "SubjectName")
                                obj.SubjectName = Convert.ToString(row[col]);
                            if (col.ColumnName == "RollNo")
                                obj.RollNo = Convert.ToInt32(row[col]);
                            if (col.ColumnName == "Name")
                                obj.Name = Convert.ToString(row[col]);
                            if (col.ColumnName.ToLower().IndexOf("unit") > 0 && col.Ordinal == 3)
                            {
                                obj.Unit5 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }
                            if (col.ColumnName.ToLower().IndexOf("unit") > 0 && col.Ordinal == 4)
                            {
                                obj.Unit6 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }
                            if (col.ColumnName.ToLower().IndexOf("unit") > 0 && col.Ordinal == 5)
                            {
                                obj.Unit7 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }
                            if (col.ColumnName.ToLower().IndexOf("unit") > 0 && col.Ordinal == 6)
                            {
                                obj.Unit8 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }
                            if (col.ColumnName == "Best1")
                            {
                                obj.Best1 = row[col] == DBNull.Value ? 0 : Convert.ToDecimal(row[col]);
                            }

                        }
                        lstUnit2.Add(obj);
                    }
                    
                    var mergeResult = (from U1 in lstUnit1
                                       join U2 in lstUnit2 on U1.RollNo equals U2.RollNo
                                       select new Marks5PercentFinalDTO
                                       {
                                           sheetsNames = "",
                                           typeNames = "",
                                           RollNo = U1.RollNo,
                                           Name = U1.Name,
                                           Unit1 = U1.Unit1,
                                           Unit2 = U1.Unit2,
                                           Unit3 = U1.Unit3,
                                           Unit4 = U1.Unit4,
                                           Best = U1.Best,
                                           Unit5 = U2.Unit5,
                                           Unit6 = U2.Unit6,
                                           Unit7 = U2.Unit7,
                                           Unit8 = U2.Unit8,
                                           Best1 = U2.Best1,
                                           First_UNIT_5Percent = ((U1.Best * 5) / 25),
                                           Second_UNIT_5Percent = ((U2.Best1 * 5) / 25),
                                           CW = 5,
                                           HW = 5,
                                           Total = ((U1.Best * 5) / 25) + ((U2.Best1 * 5) / 25) + 5 + 5,
                                           TOTAL_ROUND_OFF = Math.Round((((U1.Best * 5) / 25) + ((U2.Best1 * 5) / 25) + 5 + 5), 0)
                                       }).ToList();
                    
                    objMarks.Add(mergeResult);

                }
            }
            sbTestA = sbTestA.Remove(sbTestA.Length - 1, 1);
            sbTestB = sbTestB.Remove(sbTestB.Length - 1, 1);
            sb = sb.Remove(sb.Length - 1, 1);
            sbDivName = sbDivName.Remove(sbDivName.Length - 1, 1);
            var mergeResult1 = new Marks5PercentFinalDTO
            {
                sheetsNames = sb.ToString(),
                typeNames = sbTestA.ToString() + "~" + sbTestB.ToString() + "~" + standardresult.StandardName + "-" + sbDivName.ToString(),
                RollNo = 0,
                Name = "",
                Unit1 = 0,
                Unit2 = 0,
                Unit3 = 0,
                Unit4 = 0,
                Best = 0,
                Unit5 = 0,
                Unit6 = 0,
                Unit7 = 0,
                Unit8 = 0,
                Best1 = 0,
                First_UNIT_5Percent = 0,
                Second_UNIT_5Percent = 0,
                CW = 0,
                HW = 0,
                TOTAL_ROUND_OFF = 0
            };
            List<Marks5PercentFinalDTO> objF = new List<Marks5PercentFinalDTO>();
            objF.Add(mergeResult1);
            objMarks.Add(objF);
            return objMarks;
        }
    }
}
