using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Models;
using StackFlowTests;
using System;
using System.IO;

namespace StackFlow.Controllers.Tests
{
    [TestClass()]
    public class SessionSaveLoadControllerTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            TestForm testForm = new TestForm();
            SessionSaveLoadController controller = new SessionSaveLoadController();
            controller.Initialize(testForm);
        }
        [TestMethod()]
        public void SaveAndLoadControllerTest()
        {
            TestForm testForm = new TestForm();
            SessionSaveLoadController controller = new SessionSaveLoadController();
            controller.Initialize(testForm);
            StackFlowSession sesh = new StackFlowSession();
            testForm.SetActiveSession(sesh);
            string name = "test.dat";
            string path = Environment.CurrentDirectory;
            string fullpath = $"{path}\\{name}";
            //save
            testForm.InvokeSaveSession(new EventArgClasses.SessionSaveOrLoadEventArgs() { Folder = path, SessionName = name });
            Assert.IsTrue(File.Exists(fullpath));
            testForm.ActiveSession = null;
            testForm.InvokeLoadSession(new EventArgClasses.SessionSaveOrLoadEventArgs() { Folder = path, SessionName = name });
            Assert.IsNotNull(testForm.ActiveSession);
        }
    }
}