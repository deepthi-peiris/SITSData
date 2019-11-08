using System;
using EntityCore.Data;
using EntityCore.Interfaces;
using EntityCore.Interfaces.DataImports;
using EntityCore.Repos;
using EntityCore.Repositories;
using SITSData.Data;
using EntityCore.Services;
using IAppDetails = EntityCore.Interfaces.IAppDetails;
using SITSData.Interfaces;
using EntityCore.Models.Test;
using System.Linq;

namespace SITSData
{
    public partial class BaseApplication
    {
        private readonly LocalDbContext ldbc;
        private readonly EntityDbContext edbc;
        private readonly TestDbContext tdbc;
        private readonly ExamsDbContext xdbc;
        private readonly IModuleRepo moduleRepo;
        private readonly IModuleMarkRepo moduleMarkRepo;
        private readonly IModuleMarkChangeLogRepo moduleMarkChangeLogRepo;
        private readonly IAppDetails app;
        private readonly IEmailSender emailSender;
        private readonly ICMPMarksImportRepo cmpMarksImportRepo;
        //Test Cases
        private readonly IStudentRepo studentRepo;

        public BaseApplication(
                               IEmailSender _emailSender,
                               LocalDbContext _ldbc,
                               EntityDbContext _edbc,
                               ExamsDbContext _xdbc,
                               IModuleRepo _moduleRepo,
                               IAppDetails _app,
                               IModuleMarkRepo _moduleMarkRepo,
                               IModuleMarkChangeLogRepo _moduleMarkChangeLogRepo,
                               ICMPMarksImportRepo _cmpMarksImportRepo,
                               TestDbContext _tdbc,
                               IStudentRepo _studentRepo
                                                        )
        {
            emailSender = _emailSender;
            ldbc = _ldbc;
            edbc = _edbc;
            tdbc = _tdbc;
            xdbc = _xdbc;
            moduleRepo = _moduleRepo;
            moduleMarkRepo = _moduleMarkRepo;
            moduleMarkChangeLogRepo = _moduleMarkChangeLogRepo;
            app = _app;
            cmpMarksImportRepo = _cmpMarksImportRepo;
            //Test cases
            studentRepo = _studentRepo;
        }


        public void Run(string[] args)
        {
            //App to run
            //DataMigrationApp();
            //RepoTest();
            //CSVTest();
            //ExcelTest();
            //CSVIELTSTest();
            //MigrationTest();
            //GSDSeatingTest();
            //ModuleTest();
            //GSDSeatingTest();
            //DepartmentMigration();
            //TestMail();
            TestStudentRepo();
        }

        private void TestStudentRepo()
        {

            var student = new Student
            {
                Id = 0, //New Record
                StudentId = 3,
                Name = "Deepthi"


            };

            studentRepo.AddStudent(student);
            studentRepo.ChangeToGSDStudent(student);
           

        }

        

    }
}
