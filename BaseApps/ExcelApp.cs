using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using CsvHelper;
using EntityCore.Models;
using EntityCore.Models.Audits;
using EntityCore.Models.Exams;
using EntityCore.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;
using SITSData.Models.CSVImports;

namespace SITSData
{
    public partial class BaseApplication
    {
        internal void ExcelTest()
        {

            //Console.WriteLine($"Test Excel");

            //Read Excel Sheet

            string path = string.Format("./Excel/1920_3001_Student_List.xlsx");
            System.Data.DataTable tab = EntityCore.Data.ExcelFileAccess.GetDataTableFromExcel(path, true);

            List<StudentModuleList> list = new List<StudentModuleList>();
            foreach (DataRow dr in tab.Rows)
            {
                list.Add(new StudentModuleList
                {
                    UoPId = (int)dr[0],
                    StudentId = (int)(Int32.Parse(dr[1].ToString())),
                    FullName = dr["Student Name"].ToString(),
                    Department = dr["Department"].ToString(),
                    Group = dr["Group"].ToString(),
                    SectionId = (int?)(Int32.Parse(dr["SectionId"].ToString())),

                });
            }
            Console.WriteLine($"No of Rows: {list.Count()}");
            foreach (var rec in list)
            {
                Console.WriteLine($"{rec.StudentId},{rec.UoPId},{rec.Group},{rec.SectionId}");
            }
        }

    }
}
