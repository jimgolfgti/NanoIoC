﻿using NUnit.Framework;

namespace NanoIoC.Tests
{
    [TestFixture]
    public class RegisteredConcreteTypes
    {
        [Test]
        public void ShouldConstruct()
        {
            var container = new Container();
            container.Register<object, TestClass>();

            var instance = container.Resolve<object>();
            Assert.IsInstanceOf<TestClass>(instance);
        }

		[Test]
		public void ShouldAlwaysBeTheSameInstance()
		{
			var container = new Container();
			container.Register<object, TestClass>();

			var instance = container.Resolve<object>();
			var instance2 = container.Resolve<object>();
			Assert.AreSame(instance, instance2);
		}

        public class TestClass
        {
            
        }
    }
}