using TechTalk.SpecFlow;
using BoDi;
using Microsoft.Extensions.Configuration;
using DemoTests.Models;

namespace DemoTests.Hooks
{
    [Binding]
    public class Hooks
    {
        IConfigurationRoot configuration;

        public Hooks()
        {
        }

        [BeforeScenario]
        public void Setup(IObjectContainer objectContainer)
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            objectContainer.RegisterInstanceAs(configuration);
            // objectContainer.RegisterInstanceAs(new Order());

        }
    }
}
