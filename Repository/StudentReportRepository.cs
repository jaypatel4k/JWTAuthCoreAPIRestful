using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using JWTAuthCoreAPIRestful.Data;
using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models.StudentResultModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

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
            await _dbcontext.Database.RollbackTransactionAsync();
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


        //RANK
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
                              TotalMarks = (int)g.Sum(x => x.mark.Marks)
                          }).OrderByDescending(x => x.TotalMarks).ToList();

            var top3Marks = (from m in result
                             orderby m.TotalMarks descending
                             select new
                             {
                                 top3mark = m.TotalMarks
                             }).Take(3).Distinct().ToList();
            var top3Result = (from m in result
                              join t in top3Marks on m.TotalMarks equals t.top3mark
                              select m).Distinct().ToList();
            int pevmark = 0;
            int rank = 1;
            // Assign ranks
            for (int i = 0; i < top3Result.Count; i++)
            {
               if (pevmark < result[i].TotalMarks)
                {
                    result[i].Rank = rank;
                    pevmark = result[i].TotalMarks;
                }
               else if(pevmark > result[i].TotalMarks)
                {
                    rank++;
                    result[i].Rank = rank;
                    pevmark = result[i].TotalMarks;
                }
                else if (pevmark == result[i].TotalMarks)
                {
                    result[i].Rank = rank;
                    pevmark = result[i].TotalMarks;
                }

            }

            return top3Result;
        }
        public async Task<IEnumerable<TopRankInClassBySubject>> GetTopRankBySubjectInClass(int testTypeId, int monthId, int yearId, int standardId, int divisionId, int streamId)
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
                              SubjectName = g.Key.SubjectName,
                              TotalMarks = (int)g.Max(x => x.mark.Marks),
                              Rank = 0 // Placeholder for rank
                          }).OrderByDescending(x => x.TotalMarks).ToList();

            // Assign ranks
            var groupedResult = from p in result
                                group p by new { p.SubjectName } into g
                                select g.OrderByDescending(x => x.TotalMarks);
            foreach (var group in groupedResult)
            {
                int rank = 1;
                var mxMark = group.Max(x => x.TotalMarks);
                foreach (var item in group)
                {
                   if(item.TotalMarks == mxMark)
                        item.Rank = rank;
                   else
                        item.Rank = 0;
                }
                
            }

            //return result.Where(x => x.Rank == 1);
            return result.OrderByDescending(y => y.TotalMarks).ThenBy(x => x.SubjectName).ToList().Where(x => x.Rank == 1);
        }

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
                              Division = g.Key.DivisionName,
                              Rank = 0,// Placeholder for rank
                              TotalMarks = (int)g.Sum(x => x.mark.Marks)
                          }).OrderByDescending(x => x.TotalMarks).ToList();

            var top3Marks = (from m in result
                             orderby m.TotalMarks descending
                             select new
                             {
                                 top3mark = m.TotalMarks
                             }).Take(3).Distinct().ToList();
            var top3Result = (from m in result
                              join t in top3Marks on m.TotalMarks equals t.top3mark
                              select m).Distinct().ToList();
            int pevmark = 0;
            int rank = 1;
            // Assign ranks
            for (int i = 0; i < top3Result.Count; i++)
            {
                if (pevmark < result[i].TotalMarks)
                {
                    result[i].Rank = rank;
                    pevmark = result[i].TotalMarks;
                }
                else if (pevmark > result[i].TotalMarks)
                {
                    rank++;
                    result[i].Rank = rank;
                    pevmark = result[i].TotalMarks;
                }
                else if (pevmark == result[i].TotalMarks)
                {
                    result[i].Rank = rank;
                    pevmark = result[i].TotalMarks;
                }

            }

            return top3Result;
        }
        public async Task<IEnumerable<TopRankInAllDivisionBySubject>> GetTopRankBySubjectInAllDivision(int testTypeId, int monthId, int yearId, int standardId,int streamId)
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
                              Division = g.Key.DivisionName,
                              SubjectName = g.Key.SubjectName,
                              TotalMarks = (int)g.Max(x => x.mark.Marks),
                              Rank = 0 // Placeholder for rank
                          }).OrderByDescending(x => x.TotalMarks).ToList();
            // Assign ranks
            var groupedResult = from p in result
                                group p by new { p.SubjectName } into g
                                select g.OrderByDescending(x => x.TotalMarks);
            foreach (var group in groupedResult)
            {
                int rank = 1;
                var mxMark = group.Max(x => x.TotalMarks);
                foreach (var item in group)
                {
                    if (item.TotalMarks == mxMark)
                        item.Rank = rank;
                    else
                        item.Rank = 0;
                }

            }

            return result.OrderByDescending(y => y.TotalMarks).ThenBy(x => x.SubjectName).ToList().Where(x => x.Rank == 1);
            //return result.Where(x => x.Rank == 1);
        }


    }
}
