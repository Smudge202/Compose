using System;
using FluentAssertions;
using Moq;
using TestAttributes;
using System.Collections.Generic;

namespace Compose.Tests.Emission
{
	public class GenericMethodTests : EmissionHelpers
	{
		public interface InvokeWithGenericMethodArguments { void Method<T1, T2>(T1 arg1, T2 arg2); }

		public class InvokeWithGenericMethodArgumentsImplementation : InvokeWithGenericMethodArguments
		{
			public void Method<T1, T2>(T1 arg1, T2 arg2) { }
		}

		[Unit]
		public static void WhenMethodWithMethodLevelGenericArgumentsInvokedThenDoesNotThrowException()
		{
			var proxy = CreateProxy<InvokeWithGenericMethodArguments, InvokeWithGenericMethodArgumentsImplementation>();
			Action act = () => proxy.Method(Guid.NewGuid(), new object());
			act.ShouldNotThrow<Exception>();
		}

		public interface ReturnGenericMethodArgument { T Method<T>(T arg); }

		public class ReturnGenericMethodArgumentImplementation : ReturnGenericMethodArgument
		{
			public T Method<T>(T arg) => arg;
		}

		[Unit]
		public static void WhenMethodLevelGenericArgumentPassedThenIsReturned()
		{
			var proxy = CreateProxy<ReturnGenericMethodArgument, ReturnGenericMethodArgumentImplementation>();
			var value = Guid.NewGuid();
			proxy.Method(value)
				.Should().Be(value);
		}

		public interface InterfaceConstraint { }

		public interface InvokeGenericMethodWithInterfaceConstraint { void Method<T>(T arg) where T : InterfaceConstraint; }

		public class InvokeGenericMethodWithInterfaceConstraintImplementation : InvokeGenericMethodWithInterfaceConstraint
		{
			internal static bool Invoked { get; set; }
			public void Method<T>(T arg) where T : InterfaceConstraint => Invoked = true;
		}

		[Unit]
		public static void WhenMethodLevelGenericWithInterfaceConstraintCalledThenIsInvoked()
		{
			var proxy = CreateProxy<InvokeGenericMethodWithInterfaceConstraint, InvokeGenericMethodWithInterfaceConstraintImplementation>();
			InvokeGenericMethodWithInterfaceConstraintImplementation.Invoked = false;
			proxy.Method(new Mock<InterfaceConstraint>().Object);
			InvokeGenericMethodWithInterfaceConstraintImplementation.Invoked.Should().BeTrue();
		}

		public abstract class AbstractClassConstraint { }
		public interface InvokeGenericMethodWithAbstractClassConstraint { void Method<T>(T arg) where T : AbstractClassConstraint; }

		public class InvokeGenericMethodWithAbstractClassConstraintImplementation : InvokeGenericMethodWithAbstractClassConstraint
		{
			internal static bool Invoked { get; set; }
			public void Method<T>(T arg) where T : AbstractClassConstraint => Invoked = true;
		}

		[Unit]
		public static void WhenMethodLevelGenericWithAbstractClassConstrainedCalledThenIsInvoked()
		{
			var proxy = CreateProxy<InvokeGenericMethodWithAbstractClassConstraint, InvokeGenericMethodWithAbstractClassConstraintImplementation>();
			InvokeGenericMethodWithAbstractClassConstraintImplementation.Invoked = false;
			proxy.Method(new Mock<AbstractClassConstraint>().Object);
			InvokeGenericMethodWithAbstractClassConstraintImplementation.Invoked.Should().BeTrue();
		}

		public interface InvokeGenericMethodWithDefaultConstructorContraint { void Method<T>(T arg) where T : new(); }

		public class InvokeGenericMethodWithDefaultConstructorContraintImplementation : InvokeGenericMethodWithDefaultConstructorContraint
		{
			internal static bool Invoked { get; set; }
			public void Method<T>(T arg) where T : new() => Invoked = true;
		}

		[Unit]
		public static void WhenMethodLevelGenericWithDefaultConstructorConstraintCalledThenIsInvoked()
		{
			var proxy = CreateProxy<InvokeGenericMethodWithDefaultConstructorContraint, InvokeGenericMethodWithDefaultConstructorContraintImplementation>();
			InvokeGenericMethodWithDefaultConstructorContraintImplementation.Invoked = false;
			proxy.Method(new object());
			InvokeGenericMethodWithDefaultConstructorContraintImplementation.Invoked.Should().BeTrue();
		}

		public interface InvokeGenericMethodWithClasConstraint {  void Method<T>(T arg) where T : class; }

		public class InvokeGenericMethodWithClasConstraintImplementation : InvokeGenericMethodWithClasConstraint
		{
			internal static bool Invoked { get; set; }
			public void Method<T>(T arg) where T : class => Invoked = true;
		}

		[Unit]
		public static void WhenMethodLevelGenericWithClassConstraintCalledThenIsInvoked()
		{
			var proxy = CreateProxy<InvokeGenericMethodWithClasConstraint, InvokeGenericMethodWithClasConstraintImplementation>();
			InvokeGenericMethodWithClasConstraintImplementation.Invoked = false;
			proxy.Method(new object());
			InvokeGenericMethodWithClasConstraintImplementation.Invoked.Should().BeTrue();
		}

		public interface InvokeGenericMethodWithStructConstraint { void Method<T>(T arg) where T : struct; }

		public class InvokeGenericMethodWithStructConstraintImplementation : InvokeGenericMethodWithStructConstraint
		{
			internal static bool Invoked { get; set; }
			public void Method<T>(T arg) where T : struct => Invoked = true;
		}

		[Unit]
		public static void WhenMethodLevelGenericWithStructConstraintCalledThenIsInvoked()
		{
			var proxy = CreateProxy<InvokeGenericMethodWithStructConstraint, InvokeGenericMethodWithStructConstraintImplementation>();
			InvokeGenericMethodWithStructConstraintImplementation.Invoked = false;
			proxy.Method(Guid.NewGuid());
			InvokeGenericMethodWithStructConstraintImplementation.Invoked.Should().BeTrue();
		}

		public interface InvokeGenericMethodWithMethodLevelGenericConstraint { void Method<TBase, TDerived>(TBase arg1, TDerived arg2) where TDerived : TBase; }

		public class InvokeGenericMethodWithMethodLevelGenericConstraintImplementation : InvokeGenericMethodWithMethodLevelGenericConstraint
		{
			internal static bool Invoked { get; set; }

			public void Method<TBase, TDerived>(TBase arg1, TDerived arg2) where TDerived : TBase => Invoked = true;
		}

		[Unit]
		public static void WhenMethodLevelGenericWithMethodLevelGenericConstraintCalledThenIsInvoked()
		{
			var proxy = CreateProxy<InvokeGenericMethodWithMethodLevelGenericConstraint, InvokeGenericMethodWithMethodLevelGenericConstraintImplementation>();
			InvokeGenericMethodWithMethodLevelGenericConstraintImplementation.Invoked = false;
			proxy.Method(new object(), Guid.NewGuid());
			InvokeGenericMethodWithMethodLevelGenericConstraintImplementation.Invoked.Should().BeTrue();
		}

		public interface InvokeGenericMethodWithNestGenericArgument { void Method<T>(List<List<T>> arg); }

		public class InvokeGenericMethodWithNestGenericArgumentImplementation : InvokeGenericMethodWithNestGenericArgument
		{
			internal static bool Invoked { get; set; }
			public void Method<T>(List<List<T>> arg) => Invoked = true;
		}

		[Unit]
		public static void WhenMethodLevelGenericWithNestedGenericArgumentCalledThenIsInvoked()
		{
			var proxy = CreateProxy<InvokeGenericMethodWithNestGenericArgument, InvokeGenericMethodWithNestGenericArgumentImplementation>();
			InvokeGenericMethodWithNestGenericArgumentImplementation.Invoked = false;
			proxy.Method(new List<List<object>>());
			InvokeGenericMethodWithNestGenericArgumentImplementation.Invoked.Should().BeTrue();
		}
	}
}
