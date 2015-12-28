using System;
using FluentAssertions;
using TestAttributes;

namespace Compose.Tests.Emission
{
	public class FunctionTests : EmissionHelpers
	{
		public interface InvokeWithoutArguments { void Method(); }

		public class InvokeWithoutArgumentsImplementation : InvokeWithoutArguments
		{
			internal static bool Invoked { get; set; }
			public void Method() {  Invoked = true ; }
		}

		[Unit]
		public static void WhenInvokingMethodOnProxyWithoutArgumentsThenIsInvoked()
		{
			var proxy = CreateProxy<InvokeWithoutArguments, InvokeWithoutArgumentsImplementation>();
			InvokeWithoutArgumentsImplementation.Invoked = false;
			proxy.Method();
			InvokeWithoutArgumentsImplementation.Invoked.Should().BeTrue();
		}

		public interface InvokeWithArguments { void Method(string arg); }

		public class InvokeWithArgumentsImplementation : InvokeWithArguments
		{
			internal static string InvocationArgument { get; set; }
			public void Method(string arg) => InvocationArgument = arg;
		}

		[Unit]
		public static void WhenInvokingMethodOnProxyWithArgumentsThenArgumentsArePassed()
		{
			var proxy = CreateProxy<InvokeWithArguments, InvokeWithArgumentsImplementation>();
			var value = Guid.NewGuid().ToString();
			proxy.Method(value);
			InvokeWithArgumentsImplementation.InvocationArgument.Should().Be(value);
		}

		public interface ReturnFromInvoke { string Method(); }

		public class ReturnFromInvokeImplementation : ReturnFromInvoke
		{
			internal static string Return { get; set; }
			public string Method() => Return;
		}

		[Unit]
		public static void WhenInvokingMethodWithReturnValueThenValueIsReturned()
		{
			var proxy = CreateProxy<ReturnFromInvoke, ReturnFromInvokeImplementation>();
			ReturnFromInvokeImplementation.Return = Guid.NewGuid().ToString();
			proxy.Method()
				.Should().Be(ReturnFromInvokeImplementation.Return);
		}
	}
}
