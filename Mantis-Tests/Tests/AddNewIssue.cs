using Mantis_Tests.Mantis;
using NUnit.Framework;

namespace Mantis_Tests
{
    [TestFixture]
    public class AddNewIssue : TestBase
    {
        [Test]
        public void AddNewIssueTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator", Pass = "root"
            };

            ProjectData project = new ProjectData()
            {
                Id = "1"
            };

            IssueData issue = new IssueData()
            {
                Summary = "some_summary",
                Description = "some_description",
                Category = "General"
        };
            app.API.CreateNewIssue(account, project, issue);
        }
    }
}
