using BoDi;
using GoRestApi.AutomationFramework.Test.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace GoRestApi.AutomationFramework.Test
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer objectContainer;
        public Hooks(IObjectContainer container)
        {
            objectContainer = container;
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            var client = new RegresInApiClient();
            objectContainer.RegisterInstanceAs<IRegresInApiClient>(client);   
        }

        [AfterScenario]
        public void AfterScenario()
        {
          
        }
    }
}
