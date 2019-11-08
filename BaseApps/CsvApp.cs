using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using EntityCore.Models;
using EntityCore.Models.Audits;
using EntityCore.Models.Exams;
using EntityCore.Models.IELTS;
using EntityCore.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;
using SITSData.Models.CSVImports;

namespace SITSData
{
    public partial class BaseApplication
    {
        internal void CSVIELTSTest()
        {
            //Import External IELTS results from 1314 to 1819
            int[] years = { 1314, 1415, 1516, 1617,1718, 1819 };

            string type = "E";
            //int ay = 1314;

            //foreach (int ay in years)
            //{
            //    var elist = ReadExternalIELTSResults(type, ay);
            //    Console.WriteLine("No of  Recs: {0}", elist.Count());
            //    Console.WriteLine($"Updating Ay: {ay}");
            //    UpdateExternalIELTSResults(elist);
                
            //}

            //Re-set ay fields
            int ay2 = 1617; type = "I1";
            var ilist = ReadInternalIELTSResults(type, ay2);
            Console.WriteLine("No of  Recs: {0}", ilist.Count());
            UpdateInternalIELTSResults(ilist);

        }



        internal void CSVTest()
        {
            string mc = "3001";
            var mlist = ReadModuleStudentList(mc);
            Console.WriteLine("No of  Recs: {0}", mlist.Count());
            //Console.WriteLine("No of  Exisiting Records: {0}", edbc.studentModuleRegisters.Count());
            string mcode = ldbc.Modules.FirstOrDefault(m => m.Ay == app.CurrentYear && m.MCode.Substring(4, 4).ToUpper() == mc.ToUpper()).MCode;
            Console.WriteLine($"Updating Module List for {mcode}");
            UpdateModuleRegisterList(mlist, mcode);

        }

        internal List<IELTSResults> ReadInternalIELTSResults(string type, int ay = 1819)
        {
            List<IELTSResults> ulist = new List<IELTSResults>();
            using (var reader = new StreamReader($"./CSV/IELTS_RESULTS/IELTS_{ay}_{type}.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<IELTSInternalMap>();
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.HeaderValidated = null;
                ulist = csv.GetRecords<IELTSResults>().ToList();
            }

            return ulist;
        }

        internal List<IELTSResults> ReadExternalIELTSResults(string type, int ay = 1819)
        {
            List<IELTSResults> ulist = new List<IELTSResults>();
            using (var reader = new StreamReader($"./CSV/IELTS_RESULTS/IELTS_{ay}_{type}.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<IELTSExternalMap>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HasHeaderRecord = true;
                ulist = csv.GetRecords<IELTSResults>().ToList();
            }

            return ulist;
        }

        internal void UpdateExternalIELTSResults(List<IELTSResults> csvList)
        {
            //Note StudenttModuleList represents the data read from CSV.
            //Skip if a record exits

            foreach (var rec in csvList)
            {
                var result = ldbc.IELTSResults.FirstOrDefault(m => m.StudentId == rec.StudentId && m.DateTaken == rec.DateTaken);
                if (result != null)
                {
                    Console.WriteLine($"Record Exits for {rec.StudentId},{rec.DateTaken.ToString("yyyy-MM-dd")}");
                    continue;
                }
                var replica = (IELTSResults)rec.Clone();
                replica.Type = "E"; //Set External Type field
                ldbc.IELTSResults.Add(replica);
                Console.WriteLine($"Record Added for {rec.StudentId},{rec.DateTaken.ToString("yyyy-MM-dd")}");
                ldbc.SaveChanges();
            }

        }

        internal void UpdateInternalIELTSResults(List<IELTSResults> csvList)
        {
            //Note StudenttModuleList represents the data read from CSV.
            //Skip if a record exits

            foreach (var rec in csvList)
            {
                var result = ldbc.IELTSResults.FirstOrDefault(m => m.StudentId == rec.StudentId && m.DateTaken == rec.DateTaken);
                if (result != null)
                {
                    Console.WriteLine($"Record Exits for {rec.StudentId},{rec.DateTaken.ToString("yyyy-MM-dd")}");
                    continue;
                }
                var replica = (IELTSResults)rec.Clone();
                replica.Type = "I"; //Set External Type field
                ldbc.IELTSResults.Add(replica);
                Console.WriteLine($"Record Added for {rec.StudentId},{rec.DateTaken.ToString("yyyy-MM-dd")}");
                ldbc.SaveChanges();
            }

        }


        internal void UpdateModuleRegisterList(List<StudentModuleList> csvList, string mCode)
        {
            //Note StudenttModuleList represents the data read from CSV.
            foreach (var rec in csvList)
            {
                var studentModuleRegisisterRecord = edbc.studentModuleRegisters.FirstOrDefault(m => m.Ay == app.CurrentYear && m.StudentId == rec.StudentId && m.MCode == mCode);
                if (studentModuleRegisisterRecord != null)
                {
                    Console.WriteLine($"Record Exits for {rec.StudentId},{mCode}");
                    continue;
                }
                int termId = (int)ldbc.Modules.FirstOrDefault(m => m.Ay == app.CurrentYear && m.MCode == mCode)?.TermId;
                edbc.studentModuleRegisters.Add(new StudentModuleRegister
                {
                    Ay = app.CurrentYear,
                    StudentId = rec.StudentId,
                    MCode = mCode,
                    IsActive = true,
                    TermId = termId
                });
            }
            edbc.SaveChanges();
        }

        internal List<StudentModuleList> ReadModuleStudentList(string filename)
        {
            List<StudentModuleList> ulist = new List<StudentModuleList>();
            using (var reader = new StreamReader($"./CSV/StudentModuleLists/1920_{filename}_Student_List.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<StudentModuleListMap>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HasHeaderRecord = true;
                ulist = csv.GetRecords<StudentModuleList>().ToList();
            }

            return ulist;
        }





        internal void ReadStudentList()
        {
            List<StudentListView> ulist = new List<StudentListView>();
            using (var reader = new StreamReader("./CSV/1920_STUDENT_LIST.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<StudentListViewMap>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HasHeaderRecord = true;
                ulist = csv.GetRecords<StudentListView>().ToList();


            }
            Console.WriteLine("No of  Recs: {0}", ulist.Count());
            //var list = edbc.Students;
            foreach (var u in ulist)
            {

                var rec = edbc.Students.FirstOrDefault(m => m.StudentId == u.StudentId);
                if (rec != null)
                {
                    //Update the details
                    rec.UnitName = u.Unit;
                    rec.FullName = u.FullName;
                    rec.FirstName = u.FirstName;
                    rec.Surname = u.Surname;
                    rec.CivilId = u.CivilId;
                    //rec.Dep
                    //Console.WriteLine($"Record of {u.StudentId},{u.FullName} updated.");
                    continue;

                }

                else
                {
                    Console.WriteLine($"Record for {u.StudentId},{u.FullName} added");
                    edbc.Students.Add(new StudentImport
                    {
                        StudentId = u.StudentId,
                        UnitName = u.Unit,
                        FullName = u.FullName,
                        Surname = u.Surname,
                        IsActive = true,
                        CivilId = u.CivilId,
                        FirstName = u.FirstName,
                        UnitId = -1

                    });
                }
            }
            edbc.SaveChanges();


        }

    }
}
