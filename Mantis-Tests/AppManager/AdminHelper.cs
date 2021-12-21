using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Collections.Generic;
using System.Text.RegularExpressions;



namespace Mantis_Tests
{
    public class AdminHelper : HelperBase
    {
        private readonly string baseUrl;

        public AdminHelper(ApplicationManager manager, string baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("table tbody tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement Link = row.FindElement(By.TagName("a"));
                string name = Link.Text;
                string id = Regex.Match(Link.GetAttribute("href"), @"\d+$").Value;
                accounts.Add(new AccountData()
                {
                    Name = name, Id = id
                });
            }
            return accounts;
        }

        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.CssSelector("input[value='Delete User']")).Click();
            driver.FindElement(By.CssSelector("input[value='Delete Account']")).Click();
        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "login_page.php";
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            return driver;
        }
    }
}