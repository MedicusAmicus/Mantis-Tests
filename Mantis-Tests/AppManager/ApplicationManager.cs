using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Text;
using System.Threading;
using SimpleBrowser.WebDriver;


namespace Mantis_Tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
       
        protected string baseURL;

        public RegHelper Registration { get; set; }
        public FtpHelper Ftp { get; set; }
        public AdminHelper Admin { get; set; }
        public APIHelper API { get; set; }

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new SimpleBrowserDriver();
            baseURL = "http://localhost/mantisbt-1.3.20/";
            Registration = new RegHelper(this);
            Ftp = new FtpHelper(this);
            Admin = new AdminHelper(this, baseURL);
            API = new APIHelper(this);
        }
        

        public static ApplicationManager GetInstance()
        {
            if(! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-1.3.20/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }
       

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }


        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }         
        }        
    }
}
