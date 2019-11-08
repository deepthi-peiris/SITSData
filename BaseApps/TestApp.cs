using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using EntityCore.Models;
using EntityCore.Models.Audits;
using EntityCore.Models.Exams;
using EntityCore.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;

namespace SITSData
{
    public partial class BaseApplication
    {
        private void TestMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.mtc.edu.om");
                SmtpServer.Port = 25;
                mail.From = new MailAddress("Exams@mtc.edu.om","Exams");
                //mail.To.Add("dpeiris@gmail.com");
                mail.To.Add(new MailAddress("Deepthi.Peiris@mtc.edu.om", "Deepthi"));
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail ";
                //SmtpServer.Credentials = new System.Net.NetworkCredential("Deepthi.Peiris", "Chandra@4525");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
                Console.WriteLine("Mail Sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void ModuleMarkCloneTest()
        {

            //AddUpdateModuleMarkRepoTest();
            CMPModuleMarkTest2();
        }

        private void ModuleTest()
        {
            var module = moduleRepo.GetModule("MTCS4001",1819);

            Console.WriteLine($"{module.ModuleTitle}");
            var list = from m in ldbc.Modules
                       from mk in m.ModuleMarks
                       where (m.MCode == module.MCode && mk.IsSelected == true) 
                       select new
                       {
                           m.MCode,
                           mk.StudentId,
                           mk.Attempt,
                           mk.FinalMark,
                           mk.Outcome,
                           mk.Remarks,
                           mk.AdminRemarks

                       };
            foreach (var mod in list.ToList())
            {
                Console.WriteLine($"{mod.MCode},{mod.StudentId},{mod.Attempt},{mod.FinalMark},{mod.Outcome}, {mod.Remarks},{mod.AdminRemarks}");
            }
        }

        private void GenerateModuleCodes(string dept, int academicLevel = 3)
        {
            var modules = moduleRepo.CurrentYearModules.Where(m => m.AcademicLevel == academicLevel && m.DeptCode == dept);
            foreach (var rec in modules)
            {

                Console.WriteLine($"{rec.Ay}, {rec.MCode}, {rec.MCEmail}, {rec.PassMark}, {rec.AcademicLevel}");
            }
        }

        private void AddUpdateModuleMarkRepoTest()
        {

            var mark = moduleMarkRepo.Find(2);
            var clone = (ModuleMark)mark.Clone();
            clone.Attempt = 9;
            //Set ID to zero
            clone.Id = 100;
            clone.AdminRemarks = "Created Repeat Mark Record";
            moduleMarkRepo.AddOrUpdateModuleMark(clone, ModuleMarkChangeReason.NewModuleRecord);
        }

        private void CMPModuleMarkTest2()
        {
            //Console.WriteLine($"{edbc.ModuleMarkImports.Where(m=>m.Ay == 1819 && m.MABRef == "M").Count()}");
            ImportMABRecords(1718, "M");
        }

        private void ImportMABRecords(int ay, string mabRef, int level = 3)
        {
            var importList = edbc.ModuleMarkImports
                                .Where(m => m.Ay == ay && m.MABRef == mabRef)
                                .OrderBy(m =>m.MCode.Substring(4,4))
                                .ThenBy(m =>m.StudentId)
                                //.Take(10)
                                .ToList();
            Console.WriteLine($"No of Records: {importList.Count}");
            if (mabRef == "M")
            {
                
                //This is a main Board Results, therefore add to the repository
                foreach (var rec in importList)
                {
                    moduleMarkRepo.AddOrUpdateModuleMark(new ModuleMark {
                                                                Id = 0,
                                                                Ay = rec.Ay,
                                                                MCode = rec.MCode,
                                                                StudentId = rec.StudentId,
                                                                Attempt = 1,
                                                                FinalMark = (int)rec.FinalMark,
                                                                Outcome = rec.ModuleOutcome,
                                                                RawMark = (double)rec.RawMark,
                                                                PassMark = 40,
                                                                IsSelected = true, //Redundant as default is true
                                                                Remarks = !String.IsNullOrEmpty(rec.MABDecision)?rec.MABDecision:"",
                                                                AdminRemarks = !String.IsNullOrEmpty(rec.AdminRemarks)?rec.AdminRemarks:"",
                                                          },ModuleMarkChangeReason.AdminError);
                }
            }

            if (mabRef == "R")
            {
                //Mark the IsSelected field of Attempt 1 to failse,
                foreach (var rec in importList)
                {
                    var firstRec = moduleMarkRepo.Find(rec.Ay, rec.MCode, rec.StudentId, 1);
                    if (firstRec != null) { firstRec.IsSelected = false; }
                    moduleMarkRepo.UpdateSelected(firstRec);
                    moduleMarkRepo.AddOrUpdateModuleMark(new ModuleMark
                    {
                        Id = 0,
                        Ay = rec.Ay,
                        MCode = rec.MCode,
                        StudentId = rec.StudentId,
                        Attempt = 2,
                        FinalMark = (int)rec.FinalMark,
                        Outcome = rec.ModuleOutcome,
                        RawMark = (double)rec.RawMark,
                        PassMark = 40,
                        IsSelected = true, //Redundant as default is true
                        Remarks = !String.IsNullOrEmpty(rec.MABDecision) ? rec.MABDecision : "",
                        AdminRemarks = !String.IsNullOrEmpty(rec.AdminRemarks) ? rec.AdminRemarks : "",
                    }, ModuleMarkChangeReason.AdminError);

                }
                
            }
        }

        private void CMPModuleMarkTest()
        {

            var list = cmpMarksImportRepo.CMPModuleMarks.ToList();
            Console.WriteLine($"Length: {list.Count()}");
            foreach (var rec in list)
            {
                int attempt = 0;
                switch (rec.MABRef)
                {
                    case "M":
                        attempt = 1;
                        break;
                    case "R":
                        attempt = 2;
                        break;
                    default:
                        attempt = 2;
                        break;
                }
                var mrec = new ModuleMark
                {
                    //Since new module, consider Id as 0
                    Id = 0,
                    Ay = rec.Ay,
                    MCode = rec.MCode,
                    StudentId = rec.StudentId,
                    Attempt = attempt,
                    FinalMark = rec.FinalMark,
                    Outcome = rec.Outcome,
                    PassMark = 50,
                    Remarks = !String.IsNullOrEmpty(rec.Remarks) ? rec.Remarks : "",
                    AdminRemarks = !String.IsNullOrEmpty(rec.AdminRemarks) ? rec.Remarks : "",

                };
                if (mrec != null)
                {
                    //Console.WriteLine($"{mrec.Ay}, {mrec.MCode}, {mrec.MCode}, {mrec?.FinalMark ?? 0}, {mrec.Outcome}");
                    //Console.WriteLine($"{rec.Ay}, {rec.MCode}, {rec.MCode}, {rec?.FinalMark ?? 0}, {rec.Outcome}");
                    moduleMarkRepo.AddOrUpdateModuleMark(mrec, ModuleMarkChangeReason.NewModuleRecord);
                }

            }
        }

        

    }


}
