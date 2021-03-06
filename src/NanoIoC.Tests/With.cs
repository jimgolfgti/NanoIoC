﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NanoIoC.Tests
{
    [TestFixture]
    public class With
    {
        [Test]
        public void ShouldResolveSingletonReplacement()
        {
            var container = new Container();
        	container.Register<InterfaceA, ClassA1>();

        	var x = container.With<InterfaceA>(new ClassA2()).Resolve<ClassB>();

			Assert.IsInstanceOf<ClassA2>(x.A);
        }

		[Test]
		public void ShouldResolveInheritedSingletonReplacement()
		{
			var container = new Container();
			container.Register<InterfaceA, ClassA1>();

			var x = container.With<InterfaceA>(new ClassA2()).Resolve<InterfaceA>();

			Assert.IsInstanceOf<ClassA2>(x);
		}

		[Test]
		public void ShouldResolveHybridReplacement()
		{
			var container = new Container();
			container.Register<InterfaceA, ClassA1>(Lifecycle.HttpContextOrThreadLocal);

			var x = container.With<InterfaceA>(new ClassA2()).Resolve<InterfaceA>();

			Assert.IsInstanceOf<ClassA2>(x);
		}

		[Test]
		public void OriginalContainerShouldBeUnaffected()
		{
			var container = new Container();
			container.Register<InterfaceA, ClassA1>();

			var x = container.With<InterfaceA>(new ClassA2()).Resolve<InterfaceA>();
			var y = container.Resolve<InterfaceA>();

			Assert.IsInstanceOf<ClassA1>(y);
		}

		[Test]
		public void ContainerShouldBeResolvable()
		{
			var container = new Container();
			container.Register<InterfaceA, ClassA1>();

			var x = container.With<InterfaceA>(new ClassA2()).Resolve<InterfaceA>();
			var y = container.Resolve<IContainer>();
			var z = container.Resolve<IContainer>();

			Assert.IsInstanceOf<IContainer>(y);
			Assert.AreSame(y, z);
		}

		public interface InterfaceA
		{
			
		}

        public class ClassA1 : InterfaceA
        {
        }

		public class ClassA2 : InterfaceA
		{
		}

		public class ClassB
		{
			public readonly InterfaceA A;

			public ClassB(InterfaceA a)
			{
				this.A = a;
			}
		}
    }
}
