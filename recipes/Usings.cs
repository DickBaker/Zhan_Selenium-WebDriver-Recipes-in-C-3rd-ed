global using System;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using OpenQA.Selenium;

// Replace the version to match the Chrome version
// ensure matching TestHelper.ChromeBrowserVersion
#if LATESTVERSIONS // as of 27/3/2024
global using Domains = OpenQA.Selenium.DevTools.V123.DevToolsSessionDomains;
global using Network = OpenQA.Selenium.DevTools.V123.Network;
global using Emulation = OpenQA.Selenium.DevTools.V123.Emulation;
global using Browser = OpenQA.Selenium.DevTools.V123.Browser;
global using Page = OpenQA.Selenium.DevTools.V123.Page;
global using Security = OpenQA.Selenium.DevTools.V123.Security;
#else       // use ones as per printed book (although well outdated!)
global using Domains = OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains;
global using Network = OpenQA.Selenium.DevTools.V115.Network;
global using Emulation = OpenQA.Selenium.DevTools.V115.Emulation;
global using Browser = OpenQA.Selenium.DevTools.V115.Browser;
global using Page = OpenQA.Selenium.DevTools.V115.Page;
global using Security = OpenQA.Selenium.DevTools.V115.Security;
#endif