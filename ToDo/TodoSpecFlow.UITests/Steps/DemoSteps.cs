using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;
using System;

namespace TodoSpecFlow.UITests.Steps
{
    [Binding]
    public class Demo
    {

        public class ToDo
        {
            public string Name { get; set; }
            public string Notes { get; set; }

        }


        [When("I select add new to do")]
        public void WhenISelectAddNewToDo()
        {
            if (AppInitializer.CurrentPlatform == Platform.Android)
            {
                SpecFlowEvents.app.Tap(c => c.Marked("+"));
            }
            else
            {
                SpecFlowEvents.app.Tap(c => c.Marked("AddNew"));
            }
        }

        [Then("I should be navigated to the new to do view")]
        public void ThenIShouldBeNavigatedToTheNewToDoView()
        {
           
        }

        [When("I enter new To Do as following")]
        public void WhenIEnterNewToDoAsFollowing(Table t)
        {
            var toDo = t.CreateInstance<ToDo>();
            SpecFlowEvents.app.EnterText(c => c.Marked("NameField"), toDo.Name);
            SpecFlowEvents.app.DismissKeyboard();
            SpecFlowEvents.app.EnterText(c => c.Marked("NotesField"), toDo.Notes);
            SpecFlowEvents.app.DismissKeyboard();
        }

        [When("I choose to save the new To Do")]
        public void WhenIChooseToSaveTheNewToDo()
        {
            SpecFlowEvents.app.Tap(c => c.Marked("TodoSave"));
        }

        [Then("I should be navigated to the To Do list view")]
        public void ThenIShouldBeNavigatedToTheToDoListView()
        {
            //var query = new Func<AppQuery, AppQuery>(c => c.Marked("AddNew"));
            WaitForElement(c => c.Marked("AddNew"));
            Assert.IsTrue(SpecFlowEvents.app.Query(c => c.Marked("AddNew")).Any());
        }

        [Then("I should see (.*) in the list")]
        public void ThenIShouldSeeToDoInTheList(string name)
        {
            Assert.IsTrue(SpecFlowEvents.app.Query(c => c.Marked(name)).Any());

        }

        internal void WaitForElement(Func<AppQuery, AppQuery> q)
        {
            try
            {
                SpecFlowEvents.app.WaitForElement(q, timeout: TimeSpan.FromSeconds(3));
            }
            catch
            {
                Console.WriteLine("\t\tElement is not present.");
            }
        }
    }
}
