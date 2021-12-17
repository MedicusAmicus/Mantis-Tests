using System;
using System.Collections.Generic;
using System.Text;
//using System.Text.RegularExpressions;
using System.Threading;
//using System.Threading.Tasks;
//using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Support.UI;

namespace Mantis_Tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;

        public RegHelper Registration { get; set; }
        public FtpHelper Ftp { get; set; }

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";
            Registration = new RegHelper(this);
            Ftp = new FtpHelper(this); 
        }

        public static ApplicationManager GetInstance()
        {
            if(! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-2.25.2/login_page.php";
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
