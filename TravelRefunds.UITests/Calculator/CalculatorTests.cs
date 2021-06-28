using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TravelRefunds.UITests.Calculator
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class CalculatorTests
    {
        IApp app;
        readonly Platform platform;

        static readonly Func<AppQuery, AppQuery> CalculateButton = c => c.Marked("CalculateButton");
        static readonly Func<AppQuery, AppQuery> FromEntry = c => c.Marked("FromEntry");
        static readonly Func<AppQuery, AppQuery> ToEntry = c => c.Marked("ToEntry");
        static readonly Func<AppQuery, AppQuery> ResultLabel = c => c.Marked("ResultLabel").Text("4");
        static readonly Func<AppQuery, AppQuery> CalculatorShellItem = c => c.Marked("CalculatorShellItem");


        public CalculatorTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TestTravel()
        {

        }

        [Test]
        public void AppLaunches()
        {
#if DEBUG
            // The following method will trap the UI test into REPL tool window CLI
            app.Repl();
            // if you want to quit the REPL tool, type quit at the REPL prompt
#endif
            // Arrange - Nothing to do because the queries have already been initialized.

            // AppResult[] result = app.Query(FromEntry);
            // Assert.IsFalse(result.Any(), "The initial message string isn't correct - maybe the app wasn't re-started?");

            // Act

            // Navigate
            app.Tap(CalculatorShellItem);

            // Compile Entries.
            app.WaitForElement(FromEntry);
            app.Tap(FromEntry);
            app.EnterText("2");
            static AppQuery fromEntryCompiled(AppQuery c) => c.Marked("FromEntry").Text("2");
            var compiled = app.Query(fromEntryCompiled);
            Assert.IsTrue(compiled.Any(), "The From Entry is not being edited");

            app.Tap(ToEntry);
            app.EnterText("2");

            // Calculate
            app.Tap(CalculateButton);

            app.Screenshot("After Calculation");

            // Assert
            var results = app.Query(ResultLabel);
            Assert.IsTrue(results.Any(), "The 'clicked' message is not being displayed.");
        }

    }
}
