using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using EntityCore.Models;
using EntityCore.Models.Audits;
using EntityCore.Models.Exams;
using EntityCore.Models.IELTS;
using EntityCore.Models.LogModels;
using EntityCore.Models.Seating;
using EntityCore.ViewModels.Admin;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SITSData.Models.CSVImports;

namespace SITSData
{
    public partial class BaseApplication
    {
        internal void GSDSeatingTest()
        {

            //var list = ldbc.GSDSeatings.Where(m => m.MCode == "MTCG1016").ToList();
            //Console.WriteLine($"No of Students: {list.Count}");

            //GSDSeatAllocationEmail(list);
            //GSDEmailTest();
            //var testSeat = new GSDSeating
            //{
            //    Ay = 1920,
            //    StudentId = 12345,
            //    SeatNumber = "C10",
            //    ExamTitle = "Test",
            //    ExamDate = new DateTime(2019, 12, 1)

            //};
            //emailSender.SendMessage(testSeat);
            GSDEmailTest();

        }

        internal void GSDEmailTest()
        {
            var email = new EmailLog();
            email.AppName = "Test App";
            email.FromAddress = "Deepthi_Peiris@hotmail.com";
            email.FromName = "Hotmail (Deepthi)";
            email.EmailName = "Deepthi Peiris";
            email.EmailAddress = "dpeiris@gmail.com";
            email.Subject = "Test";
            email.Body = $"This is a test subject email";
            email.IsEmailSent = false;

            Console.WriteLine("I am here");
            //emailSender.SendMessageByHotmail(email);
            emailSender.SendMessage(email);
            //var msg = new MimeMessage();
            //msg.To.Add(new MailboxAddress("Deepthi","Deepthi.Peiris@mtc.edu.om"));
            //msg.From.Add(new MailboxAddress("Exams", "Exams@mtc.edu.om"));
            //msg.Body = new TextPart("html")
            //{
            //    Text = "Test Message"
            //};
            //msg.Subject = "Test Subject";
            //try
            //{
            //    using (var client = new SmtpClient())
            //    {
            //        client.Connect("mail.mtc.edu.om", 25, false);
            //        client.Send(msg);
            //        client.Disconnect(true);
            //    }

            //}
            //catch
            //{
            //    Console.WriteLine("Sent failed");

            //}

        }

        internal void GSDSeatAllocationEmail(List<GSDSeating> list)
        {
            foreach (var seat in list)
            {

                var msg = new MimeMessage();

                msg.To.Add(new MailboxAddress($"{seat.StudentId}", $"{seat.StudentId}@mtc.edu.om"));
                msg.To.Add(new MailboxAddress($"Exams (Copy)", $"Exams@mtc.edu.om"));
                msg.From.Add(new MailboxAddress("Automated Email", "noreply@mtc.edu.om"));
                msg.Subject = $"Seat Allocation {seat.ExamTitle}";
                StringBuilder sb = new StringBuilder($"<h4>{seat.ExamTitle} Examinatitons</h4>");
                sb.Append(@"<table border= 1px,solid width = 500px cellpadding = 5px >");
                sb.Append(@"<tr>");
                sb.Append($"<td>Student ID</td><td>{seat.StudentId}</td>");
                sb.Append(@"</tr>");
                sb.Append(@"<tr>");
                sb.Append($"<td>Module Code</td><td>{seat.MCode}</td>");
                sb.Append(@"</tr>");
                sb.Append(@"<tr>");
                sb.Append($"<td>Exam Hall</td><td>{seat.ExamHall}</td>");
                sb.Append(@"</tr>");
                sb.Append(@"<tr>");
                sb.Append($"<td>Date</td><td>{seat.DisplayDate}</td>");
                sb.Append(@"</tr>");
                sb.Append(@"<tr>");
                sb.Append($"<td>Time</td><td>{seat.Time}</td>");
                sb.Append(@"</tr>");
                sb.Append(@"<tr>");
                sb.Append($"<td>Seat No</td><td>{seat.SeatNumber}</td>");
                sb.Append(@"</tr>");
                sb.Append(@"</table>");
                sb.Append(@"<hr>Notes to students<ul>
                        <li>you must report to Exams Venue 15 Minutes before the start time.</li>
                        <li>you must bring your MTC ID card.</li>
                        <li>you are not allowed to share the calculators.</li>
                        </ul>");
                sb.Append(@"<I>This is an automatically generatted email.  Please do not reply to this email</I> ");

                msg.Body = new TextPart("html")
                {
                    Text = sb.ToString()
                };
                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect("mail.mtc.edu.om", 25, false);
                        client.Send(msg);
                        client.Disconnect(true);
                    }

                }
                catch
                {
                    Console.WriteLine("Sent failed");

                }
            }
        }


        internal void GSDSeatingReadTest()
        {
            var list = ReadGSDSeating(1920, 1, "1015", "MT");
            Console.WriteLine(list.Count());
            UpdateGSDSeating(list);

        }


        internal List<GSDSeating> ReadGSDSeating(int ay, int term, string MCode, string ex)
        {
            List<GSDSeating> ulist = new List<GSDSeating>();
            using (var reader = new StreamReader($"./CSV/GSDSeating/{ay}_{MCode}_T{term}_{ex}_Seating.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<GSDSeatingMap>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.HasHeaderRecord = true;
                ulist = csv.GetRecords<GSDSeating>().ToList();
            }

            return ulist;
        }

        internal void UpdateGSDSeating(List<GSDSeating> seatList)
        {
            //Note StudenttModuleList represents the data read from CSV.
            //Skip if a record exits

            foreach (var rec in seatList)
            {
                //e.HasAlternateKey(p => new { p.Ay, p.StudentId, p.MCode, p.ExamDate, p.Time});
                var result = ldbc.GSDSeatings.FirstOrDefault(m => m.Ay == rec.Ay && m.StudentId == rec.StudentId && m.MCode == rec.MCode && m.ExamDate == rec.ExamDate && m.Time == rec.Time);
                if (result != null)
                {
                    Console.WriteLine($"Record Exits for {rec.StudentId},{rec.MCode},{rec.ExamHall}, {rec.DisplayDate}");
                    continue;
                }
                var replica = (GSDSeating)rec.Clone();
                replica.Id = 0;
                ldbc.GSDSeatings.Add(replica);
                Console.WriteLine($"Record Added for {rec.StudentId},{rec.MCode},{rec.ExamHall}, {rec.DisplayDate}");
                ldbc.SaveChanges();
            }

        }

    }
}
