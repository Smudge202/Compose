using System;
using FluentAssertions;
using TestAttributes;

namespace Compose.Tests.Emission
{
	public class InheritanceTests : EmissionHelpers
	{
		public interface InheritedInterface { void InheritedMethod(); }
		public interface ParentInterface : InheritedInterface {  void ParentMethod(); }

		public class ImplicitImplementation : ParentInterface
		{
			internal static bool InvokedInherited { get; set; }
			public void InheritedMethod() => InvokedInherited = true;
			internal static bool InvokedParent { get; set; }
			public void ParentMethod() => InvokedParent = true;
		}

		[Unit]
		public static void WhenInheritedImplicitlyImplementedInterfaceMethodCalledThenIsInvoked()
		{
			var proxy = CreateProxy<ParentInterface, ImplicitImplementation>();
			ImplicitImplementation.InvokedInherited = false;
			ImplicitImplementation.InvokedParent = false;
			proxy.InheritedMethod();
			proxy.ParentMethod();
			ImplicitImplementation.InvokedInherited.Should().BeTrue();
			ImplicitImplementation.InvokedParent.Should().BeTrue();
		}

		public class ExplicitImplementation : ParentInterface
		{
			internal static bool InvokedInherited { get; set; }
			void InheritedInterface.InheritedMethod() => InvokedInherited = true;
			internal static bool InvokedParent { get; set; }
			void ParentInterface.ParentMethod() => InvokedParent = true;
		}

		[Unit]

		public static void WhenInheritedExplicitlyImplementedInterfaceMethodCalledThenIsInvoked()
		{
			var proxy = CreateProxy<ParentInterface, ExplicitImplementation>();
			ExplicitImplementation.InvokedInherited = false;
			ExplicitImplementation.InvokedParent = false;
			proxy.InheritedMethod();
			proxy.ParentMethod();
			ExplicitImplementation.InvokedInherited.Should().BeTrue();
			ExplicitImplementation.InvokedParent.Should().BeTrue();
		}
	}
}
