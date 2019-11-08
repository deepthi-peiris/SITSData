using System;
using System.Linq;
using EntityCore.Models;
using Microsoft.EntityFrameworkCore;

namespace SITSData
{
    public partial class BaseApplication
    {

        private void DepartmentMigration()
        {
            var list = edbc.Staffs.ToList();
            Console.WriteLine($"Nos: {list.Count()}");
            foreach (var rec in list)
            {
                var newrec = rec.Clone() as Staff;
                newrec.Id = 0;
                if (xdbc.Staffs.FirstOrDefault(s =>s.StaffEmail == rec.StaffEmail) == null)
                {
                    xdbc.Staffs.Add(newrec);
                    xdbc.SaveChanges();
                }
                
                
            }
        }
        //private void MigrationTest()
        //{
        //    Console.WriteLine($"{edbc.StudentRegisters.Count()}");
        //    StudentRegisterMigration();
        //}

        private void DataMigrationApp()
        {
            var srcList = edbc.Modules.ToList();
            foreach (var src in srcList)
            {
                //Console.WriteLine($"{src.Ay},{src.MCode}, {src.MCEmail}");
                var tgt = xdbc.Modules.FirstOrDefault(m => m.Ay == src.Ay && m.MCode == src.MCode);
                if (tgt == null)
                {
                    var newRec = new Module
                    {
                        Ay = src.Ay,
                        MCode = src.MCode,
                        DeptCode = src.DeptCode,
                        UoPCode = src.UoPCode,
                        ShortCode = src.ShortCode,
                        ModuleTitle = src.ModuleTitle,
                        Elective = src.Elective,
                        Credits = src.Credits,
                        MCEmail = src.MCEmail,
                        SMXEmail = src.SMXEmail,
                        SMXName = src.SMXName,
                        IsActive = src.IsPF,
                        NoOfArtefacts = src.NoOfArtefacts,
                        EECode = src.EECode,
                        HasFinalExam = src.HasFinalExam,
                        TermId = src.TermId,
                        IsPF = src.IsPF

                    };
                    xdbc.Modules.Add(newRec);
                    Console.WriteLine($"Record Added for {src.Ay}, {src.MCode}");
                }
                else
                {
                    Console.WriteLine($"Record Exists for {src.Ay}, {src.MCode}");
                }


                xdbc.SaveChanges();
            }
        }

        //private void StudentRegisterMigration()
        //{
        //    var importList = edbc.StudentRegisters.ToList();
        //    foreach (var rec in importList)
        //    {
                
        //        if(tdbc.StudentRegisters.FirstOrDefault(s =>s.StudentId == rec.StudentId && s.LevelId == rec.LevelId) != null )
        //        {
        //            //Record exits, skip
        //            Console.WriteLine($"Record exits for {rec.StudentId}, {rec.LevelId}, {rec.Ay}");
        //            continue;
        //        }
        //        var clone = (StudentRegister)rec.Clone();
        //        clone.Id = 0;
        //        tdbc.StudentRegisters.Add(clone);
        //        Console.WriteLine($"Record Added for {rec.StudentId}, {rec.LevelId}, {rec.Ay}");
        //        tdbc.SaveChanges();
        //    }
        //}



    }
}
