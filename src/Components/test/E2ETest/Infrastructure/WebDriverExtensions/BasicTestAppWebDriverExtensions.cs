// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.E2ETesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Microsoft.AspNetCore.Components.E2ETest
{
    internal static class BasicTestAppWebDriverExtensions
    {
        public static IWebElement MountTestComponent<TComponent>(this IWebDriver browser) where TComponent : IComponent
        {
            var componentType = typeof(TComponent);
            var componentTypeName = componentType.Assembly == typeof(BasicTestApp.Program).Assembly ?
                componentType.FullName :
                componentType.AssemblyQualifiedName;
            var testSelector = browser.WaitUntilTestSelectorReady();
            testSelector.SelectByValue("none");
            testSelector.SelectByValue(componentTypeName);
            return browser.Exists(By.TagName("app"));
        }

        public static SelectElement WaitUntilTestSelectorReady(this IWebDriver browser)
        {
            var elemToFind = By.CssSelector("#test-selector > select");
            return new SelectElement(browser.Exists(elemToFind));
        }
    }
}
