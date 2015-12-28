using System;
using FluentAssertions;
using TestAttributes;

namespace Compose.Tests.Emission
{
	public class ExceptionTests : EmissionHelpers
	{
		public interface ThrowException { void Method(); }

		public class TestException : Exception { }

		public class ThrowExceptionImplementation : ThrowException
		{
			public void Method() { throw new TestException(); }
		}

		[Unit]
		public static void WhenImplementationThrowsExceptionThenProxyBubblesException()
		{
			var proxy = CreateProxy<ThrowException, ThrowExceptionImplementation>();
			Action act = proxy.Method;
			act.ShouldThrow<TestException>();
		}
	}
}
