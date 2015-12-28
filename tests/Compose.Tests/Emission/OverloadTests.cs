using FluentAssertions;
using TestAttributes;

namespace Compose.Tests.Emission
{
	public class OverloadTests : EmissionHelpers
	{
		public interface Overloaded
		{
			void Method(int arg);
			void Method(decimal arg);
		}

		public class OverloadedImplementation : Overloaded
		{
			internal static bool InvokedWithInt { get; set; }
			public void Method(int arg) => InvokedWithInt = true;
			internal static bool InvokedWithDecimal { get; set; }
			public void Method(decimal arg) => InvokedWithDecimal = true;
		}

		[Unit]
		public static void WhenInvokingOverloadedMethodsThenCorrectMethodsAreCalled()
		{
			var proxy = CreateProxy<Overloaded, OverloadedImplementation>();
			OverloadedImplementation.InvokedWithInt = false;
			OverloadedImplementation.InvokedWithDecimal = false;
			proxy.Method(1);
			proxy.Method(1m);
			OverloadedImplementation.InvokedWithInt.Should().BeTrue();
			OverloadedImplementation.InvokedWithDecimal.Should().BeTrue();
		}
	}
}
