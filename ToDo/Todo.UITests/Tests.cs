using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Threading;

namespace Todo.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        readonly Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

  
        [Test]
        public void ToDoAddNew()
        {

            WaitForElement(c => c.Marked("ListView"));
            //make sure we are on the list view
            Assert.IsTrue(app.Query(c => c.Marked("ListView")).Any(), "not on the to do list view");

            if (AppInitializer.CurrentPlatform == Platform.Android)
            {
                app.Tap(c => c.Marked("+"));
            }
            else
            {
                app.Tap(c => c.Marked("AddNew"));
            }

            WaitForElement(c => c.Marked("NameField"));
            app.EnterText(c => c.Marked("NameField"), "Demo Todo");
            app.DismissKeyboard();
            app.EnterText(c => c.Marked("NotesField"), "Demo Todo Notes");
            app.DismissKeyboard();
            WaitForElement(c => c.Marked("TodoSave"));
            app.Tap(c => c.Marked("TodoSave"));
            WaitForElement(c => c.Marked("NameField"));

            Assert.IsTrue(app.Query(c => c.Text("Demo Todo")).Any(), "Missing New To Do on the list");
            Thread.Sleep(3000);

        }

        [TearDown]
        public void AfterEachTest()
        {
            app = null;
        }

        internal void WaitForElement(Func<AppQuery, AppQuery> q)
        {
            try
            {
                app.WaitForElement(q, timeout: TimeSpan.FromSeconds(3));
            }
            catch
            {
                Console.WriteLine("\t\tElement is not present.");
            }
        }

    }
}
