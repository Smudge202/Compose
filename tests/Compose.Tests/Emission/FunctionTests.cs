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

		public interface InvokeWithByRefArgument { void Method(ref string arg); }

		public class InvokeWithByRefArgumentImplementation : InvokeWithByRefArgument
		{
			internal static string InvocationArgument { get; set; }
			public void Method(ref string argument) => InvocationArgument = argument;
		}

		[Unit]
		public static void WhenInvokingMethodWithByRefArgumentThenArgumentIsPassed()
		{
			var proxy = CreateProxy<InvokeWithByRefArgument, InvokeWithByRefArgumentImplementation>();
			var value = Guid.NewGuid().ToString();
			proxy.Method(ref value);
			InvokeWithByRefArgumentImplementation.InvocationArgument.Should().Be(value);
		}

		public interface InvokeWithOutArgument { void Method(out string arg); }

		public class InvokeWithOutArgumentImplementation : InvokeWithOutArgument
		{
			internal static string OutArgument { get; set; }
			public void Method(out string argument) => argument = OutArgument;
		}

		[Unit]
		public static void WhenInvokingMethodWithOutArgumentThenArgumentIsSet()
		{
			var proxy = CreateProxy<InvokeWithOutArgument, InvokeWithOutArgumentImplementation>();
			InvokeWithOutArgumentImplementation.OutArgument = Guid.NewGuid().ToString();
			string value = null;
			proxy.Method(out value);
			value.Should().Be(InvokeWithOutArgumentImplementation.OutArgument);
		}

		public interface InvokeWithParamsArgument { void Method(params Guid[] args); }

		public class InvokeWithParamsArgumentImplementation : InvokeWithParamsArgument
		{
			internal static Guid[] InvocationArguments { get; set; }
			public void Method(params Guid[] args) => InvocationArguments = args;
		}

		[Unit]
		public static void WhenInvokingMethodWithParamsArgumentsThenArgumentsArePassed()
		{
			var proxy = CreateProxy<InvokeWithParamsArgument, InvokeWithParamsArgumentImplementation>();
			var arg1 = Guid.NewGuid();
			var arg2 = Guid.NewGuid();
			proxy.Method(arg1, arg2);
			var args = InvokeWithParamsArgumentImplementation.InvocationArguments;
			args.Should().NotBeNull();
			args.Should().HaveCount(2);
			args[0].Should().Be(arg1);
			args[1].Should().Be(arg2);
		}
	}
}
