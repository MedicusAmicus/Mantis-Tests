using Mantis_Tests.Mantis;



namespace Mantis_Tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager){}

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new 
                MantisConnectPortTypeClient("MantisConnectPort", "http://localhost/mantisbt-1.3.20/api/soap/mantisconnect.php?wsdl");


            Mantis.IssueData issue = new Mantis.IssueData();

            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;

            client.mc_issue_add(account.Name, account.Pass, issue);
        }
    }
}
 