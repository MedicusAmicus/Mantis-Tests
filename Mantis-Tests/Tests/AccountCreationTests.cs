using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Mantis_Tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void SetupConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            string localPath = TestContext.CurrentContext.TestDirectory + @"\\config_inc.php";

            using (Stream localFile = File.Open(localPath, FileMode.Open))
            {
                app.Ftp.UploadFile("/config_inc.php", localFile);
            };
                       
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testUser",
                Pass = "password",
                Email = "TestUser@localhvost.localdomain"
            };

            app.Registration.Register(account);

        }

        [OneTimeTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreFromBackup("/config_inc.php");
        }
    }
}
