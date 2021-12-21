using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

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
            List<AccountData> accounts = app.Admin.GetAllAccounts();

            AccountData account = new AccountData()
            {
                Name = "testUser6",
                Pass = "password6",
                Email = "TestUser7@localhvost.localdomain"
            };

            AccountData ExistingAccount = accounts.Find(x => x.Name == account.Name);
            if (ExistingAccount != null)
            {
                app.Admin.DeleteAccount(ExistingAccount);
            }
            
            app.Registration.Register(account);

        }

        [OneTimeTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreFromBackup("/config_inc.php");
        }
    }
}
