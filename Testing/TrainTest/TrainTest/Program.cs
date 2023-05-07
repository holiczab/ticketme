using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;
using System.Diagnostics.Metrics;
using OpenQA.Selenium.Support.UI;
using System.Reflection.Emit;

public class JSON
{
    public JSON(string json)
    {
        JObject jObject = JObject.Parse(json);
        JToken jUser = jObject["test_suite"];
        input_list = jUser["input_list"].ToArray();
        output_list = jUser["output_list"].ToArray();
    }
    public Array input_list { get; set; }
    public Array output_list { get; set; }
}
internal static class Program
{
    static int counter = 0;
    // Functions for the edges
    static void edgeFunctions(string edge, WindowsDriver<WindowsElement> train)
    {
        if (edge == "started/login_page")
        {
            if (counter != 0) train.LaunchApp();
            counter++;
            Console.WriteLine("started/login_page");
            return;
        }
        else if (edge == "login_user/user_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("username").SendKeys("jegzvasarlo");
            train.FindElementByAccessibilityId("password").SendKeys("jegzvasarlo1");
            WindowsElement comboBoxElement = train.FindElementByAccessibilityId("type");
            comboBoxElement.Click();
            comboBoxElement.SendKeys(OpenQA.Selenium.Keys.Down);
            comboBoxElement.SendKeys(OpenQA.Selenium.Keys.Enter);
            train.FindElementByAccessibilityId("button1").Click();
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByName("OK").Click();
            counter++;
            Console.WriteLine("login_user/user_page");
            return;
        }
        else if (edge == "info_clicked/info")
        {
            /*
            train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement info = train.FindElementByName("File");
            info.Click();
            info.SendKeys(OpenQA.Selenium.Keys.Down);
            info.SendKeys(OpenQA.Selenium.Keys.Enter);
            train.FindElementByClassName("Button").Click();*/
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button2").Click();
            train.FindElementByClassName("Button").Click();
            counter++;
            Console.WriteLine("info_clicked/info");
            return;
        }
        else if (edge == "change_lang_eu/english_user_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button4").Click();
            counter++;
            Console.WriteLine("change_lang_eu/english_user_page");
            return;
        }
        else if (edge == "log_out/login_page")
        {
            /*train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement info = train.FindElementByName("File");
            info.Click();
            info.SendKeys(OpenQA.Selenium.Keys.Down);
            info.SendKeys(OpenQA.Selenium.Keys.Down);
            info.SendKeys(OpenQA.Selenium.Keys.Enter);*/
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button6").Click();
            counter++;
            Console.WriteLine("log_out/login_page");
            return;
        }
        else if (edge == "data_entered/login_data")
        {
            counter++;
            Console.WriteLine("data_entered/login_data");
            return;
        }
        else if (edge == "login_admin/admin_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("username").SendKeys("rendsyergayda");
            train.FindElementByAccessibilityId("password").SendKeys("rendsyergayda1");
            WindowsElement comboBoxElement = train.FindElementByAccessibilityId("type");
            comboBoxElement.Click();
            comboBoxElement.SendKeys(OpenQA.Selenium.Keys.Down);
            comboBoxElement.SendKeys(OpenQA.Selenium.Keys.Down);
            comboBoxElement.SendKeys(OpenQA.Selenium.Keys.Enter);
            train.FindElementByAccessibilityId("button1").Click();
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByClassName("Button").Click();
            counter++;
            Console.WriteLine("login_admin/admin_page");
            return;
        }
        else if (edge == "total_income_pertype_clicked/total_income_pertype")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement tol = train.FindElementsByClassName("Edit")[1];
            WindowsElement ig = train.FindElementsByClassName("Edit")[0];
            if (tol.Text != "" && ig.Text != "")
            {
                train.SwitchTo().Window(train.WindowHandles.First());
                train.FindElementByAccessibilityId("button1").Click();
            }
            counter++;
            Console.WriteLine("total_income_pertype_clicked/total_income_pertype");
            return;
        }
        else if (edge == "change_lang_ea/english_admin_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement ig = train.FindElementsByClassName("Edit")[0];
            if (ig.Enabled)
            {
                train.SwitchTo().Window(train.WindowHandles.First());
                train.FindElementByAccessibilityId("button4").Click();
            }
            counter++;
            Console.WriteLine("change_lang_ea/english_admin_page");
            return;
        }
        else if (edge == "info_clicked_2/info")
        {
            /*train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement info = train.FindElementByName("File");
            info.Click();
            info.SendKeys(OpenQA.Selenium.Keys.Down);
            info.SendKeys(OpenQA.Selenium.Keys.Enter);
            train.FindElementByClassName("Button").Click();*/
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button2").Click();
            train.FindElementByClassName("Button").Click();
            counter++;
            Console.WriteLine("info_clicked_2/info");
            return;
        }
        else if (edge == "total_income_clicked/total_income")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement tol = train.FindElementsByClassName("Edit")[1];
            WindowsElement ig = train.FindElementsByClassName("Edit")[0];
            if (tol.Text != "" && ig.Text != "")
            {
                train.SwitchTo().Window(train.WindowHandles.First());
                train.FindElementByAccessibilityId("button5").Click();
            }
            counter++;
            Console.WriteLine("total_income_clicked/total_income");
            return;
        }
        else if (edge == "change_lang/hungarian_admin_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button3").Click();
            counter++;
            Console.WriteLine("change_lang/hungarian_admin_page");
            return;
        }
        else if (edge == "total_income_pertype_clicked_2/total_income_pertype")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement tol = train.FindElementsByClassName("Edit")[1];
            WindowsElement ig = train.FindElementsByClassName("Edit")[0];
            if (tol.Text != "" && ig.Text != "")
            {
                train.SwitchTo().Window(train.WindowHandles.First());
                train.FindElementByAccessibilityId("button1").Click();
            }
            counter++;
            Console.WriteLine("total_income_pertype_clicked_2/total_income_pertype");
            return;
        }
        else if (edge == "exit_clicked/-")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("Close").Click();
            counter++;
            Console.WriteLine("exit_clicked/-");
            return;
        }
        else if (edge == "change_lang_hu/hungarian_user_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button3").Click();
            counter++;
            Console.WriteLine("change_lang_hu/hungarian_user_page");
            return;
        }
        else if (edge == "change_lang_ha/hungarian_admin_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button3").Click();
            counter++;
            Console.WriteLine("change_lang_ha/hungarian_admin_page");
            return;
        }
        else if (edge == "data_checked/can_login")
        {
            counter++;
            Console.WriteLine("data_checked/can_login");
            return;
        }
        else if (edge == "change_lang/hungarian_user_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button3").Click();
            counter++;
            Console.WriteLine("change_lang/hungarian_user_page");
            return;
        }
        else if (edge == "check_user/seat_page")
        {
            counter++;
            Console.WriteLine("check_user/seat_page");
            return;
        }
        else if (edge == "info_clicked_1/info")
        {
            /*train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement info = train.FindElementByName("File");
            info.Click();
            info.SendKeys(OpenQA.Selenium.Keys.Down);
            info.SendKeys(OpenQA.Selenium.Keys.Enter);
            train.FindElementByClassName("Button").Click();*/
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button2").Click();
            train.FindElementByClassName("Button").Click();
            counter++;
            Console.WriteLine("info_clicked_1/info");
            return;
        }
        else if (edge == "total_income_pertype_clicked_1/total_income_pertype")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement tol = train.FindElementsByClassName("Edit")[1];
            WindowsElement ig = train.FindElementsByClassName("Edit")[0];
            if (tol.Text != "" && ig.Text != "")
            {
                train.SwitchTo().Window(train.WindowHandles.First());
                train.FindElementByAccessibilityId("button1").Click();
            }
            counter++;
            Console.WriteLine("total_income_pertype_clicked_1/total_income_pertype");
            return;
        }
        else if (edge == "ticket_bought/user_page")
        {
            WindowsElement name = train.FindElementByAccessibilityId("vnev");
            WindowsElement seat = train.FindElementByAccessibilityId("b6");
            if (name.Text != "" && seat.Selected==true)
            {
                train.SwitchTo().Window(train.WindowHandles.First());
                train.FindElementByAccessibilityId("button1").Click();
            }
            counter++;
            Console.WriteLine("ticket_bought/user_page");
            return;
        }
        else if (edge == "change_lang/english_user_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button4").Click();
            counter++;
            Console.WriteLine("change_lang/english_user_page");
            return;
        }
        else if (edge == "change_lang/english_admin_page")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("button4").Click();
            counter++;
            Console.WriteLine("change_lang/english_admin_page");
            return;
        }
        else if (edge == "name_coupon_seat_entered/name_data")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            train.FindElementByAccessibilityId("vnev").SendKeys("Példa Péter");
            train.FindElementByAccessibilityId("kupon").SendKeys("K5");

            string[] seats = { "c6", "c7", "c8", "cc1", "cc2", "cc3", "b5", "b6", "b7", "b8", "bb1", "bb2", "bb3", "bb4" };
            Random rand = new Random();
            int index = rand.Next(seats.Length);
            train.FindElementByAccessibilityId(seats[index]).Click();

            counter++;
            Console.WriteLine("name_coupon_seat_entered/name_data");
            return;
        }
        else if (edge == "from_to_entered/data")
        {
            train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement tol = train.FindElementsByClassName("Edit")[1];
            tol.Click();
            tol.SendKeys(OpenQA.Selenium.Keys.Down);
            tol.Click();

            train.SwitchTo().Window(train.WindowHandles.First());
            WindowsElement ig = train.FindElementsByClassName("Edit")[0];
            ig.Click();
            ig.SendKeys(OpenQA.Selenium.Keys.Down);
            ig.SendKeys(OpenQA.Selenium.Keys.Down);
            ig.Click();

            counter++;
            Console.WriteLine("from_to_entered/data");
            return;
        }
    }
    static void Main()
    {
        Console.WriteLine("Enter ARIAN2_Jegyfoglalo.exe path: ");
        string path = Console.ReadLine();
        //C:\Users\HB-PC\Desktop\Modelling and testing\ticketme-main\ARIAN2_Jegyfoglalo\ARIAN2_Jegyfoglalo\bin\Debug\ARIAN2_Jegyfoglalo.exe
        Console.WriteLine("Enter json file path: ");
        string path2 = Console.ReadLine();
        //.\results\TrainTicket-AS_suite.json
        //Program testing
        WindowsDriver<WindowsElement> train;
        AppiumOptions desiredCapabilities = new AppiumOptions();
        desiredCapabilities
            .AddAdditionalCapability("app", @""+path);
        train = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/"), desiredCapabilities);
        if (train == null)
        {
            Console.WriteLine("App not started.");
            return;
        }
        //Reading JSO
        string json = File.ReadAllText(@""+path2);
        JSON io = new JSON(json);
        List<string> edges = new List<string>();
        for (int i = 0; i < io.input_list.Length; i++)
        {
            edges.Add(io.input_list.GetValue(i) + "/" + io.output_list.GetValue(i));
        }
        counter = 0;
        List<string> noDupesEdges = edges.Distinct().ToList();

        var watch = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine(edges.Count);
        for (int i = 0; i < edges.Count; i++)
        {
            System.Threading.Thread.Sleep(500);
            Console.Write(i+" - ");
            edgeFunctions(edges[i], train);
        }
        train.Quit();
        watch.Stop();

        var elapseds = watch.Elapsed.TotalSeconds;
        Console.WriteLine(counter+"/"+ edges.Count+" test completed - took "+ elapseds+" s");
    }
}