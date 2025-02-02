﻿using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using ObjectPrinting;
using ObjectPrinting.Serialization;
using ObjectPrintingTests.TestHelpers;

namespace ObjectPrintingTests
{
    [TestFixture]
    public class ObjectPrinterAcceptanceTests
    {
        [Test]
        public void Demo()
        {
            var person = new Person { Name = "Alex", Age = 19, Height = 180.5 };

            var printer = ObjectPrinter.For<Person>();

            var actualString = printer.Exclude<SubPerson>()
                .Printing(p => p.Age)
                .Using(age => (age + 1000).ToString())
                .Wrap(p => p + "1")
                .And.Printing<double>()
                .Using(CultureInfo.InvariantCulture)
                .And.Printing(p => p.Name)
                .Trim(1)
                .And.Exclude(p => p.PublicField)
                .OnMaxRecursion((_) => throw new ArgumentException())
                .PrintToString(person);

            actualString.Should().Be($"Person{Environment.NewLine}\tId = 00000000-0000-0000-0000-000000000000{Environment.NewLine}\tName = A{Environment.NewLine}\tHeight = 180.5{Environment.NewLine}\tAge = 10191{Environment.NewLine}");
        }
    }
}