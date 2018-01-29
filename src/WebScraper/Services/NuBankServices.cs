using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace WebScraper.Services
{
    public class NuBankServices
    {
        private readonly string Username = "username";
        private readonly string Password = "input_001";
        private readonly string ButtonSubmit = "//button[@class='nu-button stroke ng-binding']";
        private readonly string Website = "https://app.nubank.com.br/#/login";

        public List<string[]> Login(string Cpf, string Pass)
        {
            using (var driver = new ChromeDriver())
            {
                // Go to the home page
                driver.Navigate().GoToUrl(this.Website);

                // Delay to open website
                Thread.Sleep(5000);

                // Get the page elements                
                var userNameField = driver.FindElementById(this.Username);
                var userPasswordField = driver.FindElementById(this.Password);
                var loginButton = driver.FindElementByXPath(this.ButtonSubmit);

                // Add cpf and password
                userNameField.SendKeys(Cpf);
                userPasswordField.SendKeys(Pass);

                // Click the login button
                loginButton.Click();               

                // Delay to login in to the website
                Thread.Sleep(10000);

                // Get data of website
                List<string[]> lista = new List<string[]>();

                foreach (var element in driver.FindElementsByXPath("//div[@class='event-card transaction card_present']"))
                {
                    string[] data = ReplaceText((string)element.Text);

                    lista.Add(data);
                }

                driver.Quit();
                driver.Dispose();                
                return lista;
            }
        }       

        public string[] ReplaceText(string value)
        {
            value = value.Replace('\r', ' ');
            string[] data = value.Split('\n');

            return data;
        }

    }
}