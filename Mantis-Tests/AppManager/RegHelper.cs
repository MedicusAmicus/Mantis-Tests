using OpenQA.Selenium;

namespace Mantis_Tests
{
    public class RegHelper :  HelperBase
    {
        public RegHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
        }

        private void OpenRegistrationForm()
        {
            manager.Driver.Url = "http://localhost/mantisbt-1.3.20/signup_page.php";
        }
    
        public void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector("input.button")).Click();
        }

        public void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        public void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-1.3.20/login_page.php";
        }
    }
}
