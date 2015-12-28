using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TestAttributes;

namespace Compose.Tests
{
	public class EmissionTests
	{
		// TODO: Smudge - come back to this once drunk (GL noob!)
		#region CanInvokeWithClassGenericConstrainedGenericArguments
		public interface IInvokeWithClassGenericConstrainedGenericArguments<TBase> { void Method<TDerivative>(TDerivative derivative) where TDerivative : TBase; }
		internal class InvokeWithClassGenericConstrainedGenericArguments<TBase> : IInvokeWithClassGenericConstrainedGenericArguments<TBase>
		{
			public void Method<TDerivative>(TDerivative derivative) where TDerivative : TBase { }
		}
		[Unit]
		public void CanInvokeWithClassGenericConstrainedGenericArguments()
		{
			var service = SetupProxy<IInvokeWithClassGenericConstrainedGenericArguments<Base>, InvokeWithClassGenericConstrainedGenericArguments<Base>>()();
			service.Method(new Derivative());
		}
		#endregion

		// TODO: Smudge - and this :'(
		#region CanInvokeWithAllGenericConstraints
		public interface IInvokeWithAllGenericConstraints<TClass>
		{
			void Method<TMethodBase, TMethodDerivative>(ref TMethodBase arg1, out TMethodDerivative arg2, params TMethodDerivative[] arg3)
				where TMethodBase : TClass where TMethodDerivative : class, TMethodBase, IDisposable, new();
		}
		internal class InvokeWithAllGenericConstraints<TClass> : IInvokeWithAllGenericConstraints<TClass>
		{
			public void Method<TMethodBase, TMethodDerivative>(ref TMethodBase arg1, out TMethodDerivative arg2, params TMethodDerivative[] arg3)
				where TMethodBase : TClass where TMethodDerivative : class, TMethodBase, IDisposable, new()
			{ arg2 = new TMethodDerivative(); }
		}
		internal class LowerDerivative : Derivative, IDisposable { public void Dispose() { } }
		[Unit]
		public void CanInvokeWithAllGenericConstraints()
		{
			var service = SetupProxy<IInvokeWithAllGenericConstraints<Base>, InvokeWithAllGenericConstraints<Base>>()();
			var arg1 = new Derivative();
			var arg2 = new LowerDerivative();
			service.Method(ref arg1, out arg2);
		}
		#endregion

		// TODO: Smudge - ffs...
		#region CanInvokeWithClassDefinedGenericArgument
		public interface IInvokeWithClassDefinedGenericArgument<T> { void Method(T arg); }
		internal class InvokeWithClassDefinedGenericArgument<T> : IInvokeWithClassDefinedGenericArgument<T>
		{
			public void Method(T arg) { }
		}
		[Unit]
		public void CanInvokeWithClassDefinedGenericArgument()
		{
			var service = SetupProxy<IInvokeWithClassDefinedGenericArgument<int>, InvokeWithClassDefinedGenericArgument<int>>()();
			service.Method(1);
		}
		#endregion

		#region CanInvokeWithByRefArguments
		public interface IInvokeWithByRefArgument { void Method(ref int arg); }
		internal class InvokeWithByRefArgument : IInvokeWithByRefArgument
		{
			public void Method(ref int arg) { }
		}
		[Unit]
		public void CanInvokeWithByRefArguments()
		{
			var service = SetupProxy<IInvokeWithByRefArgument, InvokeWithByRefArgument>()();
			var arg = 0;
			service.Method(ref arg);
		}
		#endregion

		#region CanInvokeWithOutArguments
		public interface IInvokeWithOutArguments { void Method(out int arg); }
		internal class InvokeWithOutArguments : IInvokeWithOutArguments
		{
			public void Method(out int arg) { arg = 4; }
		}
		[Unit]
		public void CanInvokeWithOutArguments()
		{
			var service = SetupProxy<IInvokeWithOutArguments, InvokeWithOutArguments>()();
			var arg = 0;
			service.Method(out arg);
		}
		#endregion

		#region CanInvokeWithParamsArguments
		public interface IInvokeWithParamsArguments { void Method<T>(params T[] arg); }
		internal class InvokeWithParamsArguments : IInvokeWithParamsArguments
		{
			public void Method<T>(params T[] arg) { }
		}
		[Unit]
		public void CanInvokeWithParamsArguments()
		{
			var service = SetupProxy<IInvokeWithParamsArguments, InvokeWithParamsArguments>()();
			service.Method(1, 2, 3);
		}
		#endregion

		#region CanInvokeInheritedInterfaceMethods
		public interface INestedInterface { void NestedMethod(); }
		public interface IParentInterface : INestedInterface { void ParentMethod(); }
		internal class InvokeInheritedInterfaceMethods : IParentInterface
		{
			public void NestedMethod() { }

			public void ParentMethod() { }
		}
		[Unit]
		public void CanInvokeInheritedInterfaceMethods()
		{
			var service = SetupProxy<IParentInterface, InvokeInheritedInterfaceMethods>()();
			service.ParentMethod();
			service.NestedMethod();
		}
		#endregion

		#region CanInvokeInheritedInterfaceExplcitlyImplementedMethods
		public interface IExplicitInterface1 { void Method(); }
		public interface IExplicitInterface2 { void Method(); }
		public interface IInvokeInheritedInterfaceExplcitlyImplementedMethods : IExplicitInterface1, IExplicitInterface2 { }
		internal class InvokeInheritedInterfaceExplcitlyImplementedMethods : IInvokeInheritedInterfaceExplcitlyImplementedMethods
		{
			void IExplicitInterface1.Method() { }
			void IExplicitInterface2.Method() { }
		}
		[Unit]
		public void CanInvokeInheritedInterfaceExplcitlyImplementedMethods()
		{
			var service = SetupProxy<IInvokeInheritedInterfaceExplcitlyImplementedMethods, InvokeInheritedInterfaceExplcitlyImplementedMethods>()();
			((IExplicitInterface1)service).Method();
			((IExplicitInterface2)service).Method();
		}
		#endregion

		#region CanGenerateCovariantProxies
		public interface ICovariant<out T> { T Method(); }
		internal class Covariant : ICovariant<string>
		{
			public string Method() { return null; }
		}
		[Unit]
		public void CanGenerateCovariantProxies()
		{
			Action act = () => InvokeProxy<ICovariant<string>, Covariant>();

			act.ShouldNotThrow();
		}
		#endregion

		#region CanGenerateProxyForSystemInterfaces
		internal class Disposable : IDisposable
		{
			public void Dispose() { }
		}
		[Unit]
		public void CanGenerateProxyForSystemInterfaces()
		{
			Action act = () => InvokeProxy<IDisposable, Disposable>();

			act.ShouldNotThrow();
		}
		#endregion

		#region CanGenerateProxyForUntypedGenerics
		public interface IUntypedGeneric<T> { void Method(); }
		internal class UntypedGeneric<T> : IUntypedGeneric<T>
		{
			public void Method() { }
		}
		[Unit]
		public void CanGenerateProxyForUntypedGenerics()
		{
			Action act = () => InvokeProxy<IUntypedGeneric<string>>(typeof(IUntypedGeneric<>), typeof(UntypedGeneric<>));

			act.ShouldNotThrow();
		}
		#endregion

		#region CanThrowInformativeExceptionWhenInterfaceIsNotVisible
		private interface IInformativeExceptionThrownForInvisibleInterface { }
		internal class InformativeExceptionThrownForInvisibleInterface : IInformativeExceptionThrownForInvisibleInterface { }
		[Unit]
		public void CanThrowInformativeExceptionWhenInterfaceIsNotVisible()
		{
			Action act = () => InvokeProxy<IInformativeExceptionThrownForInvisibleInterface, InformativeExceptionThrownForInvisibleInterface>();

			act.ShouldThrow<InaccessibleTypeException>();
		}
		#endregion

		#region CanThrowInformativeExceptionWhenGenericTypeIsNotVisible
		public interface IInformativeExceptionThrownForInvisibleGeneric<T> { }
		private class InformativeExceptionThrownForInvisibleInterfaceGeneric { }
		private class InformativeExceptionThrownForInvisibleGeneric
			: IInformativeExceptionThrownForInvisibleGeneric<InformativeExceptionThrownForInvisibleInterfaceGeneric>
		{ }
		[Unit]
		public void CanThrowInformativeExceptionWhenGenericTypeIsNotVisible()
		{
			Action act = () => InvokeProxy<IInformativeExceptionThrownForInvisibleGeneric<InformativeExceptionThrownForInvisibleInterfaceGeneric>, InformativeExceptionThrownForInvisibleGeneric>();

			act.ShouldThrow<InaccessibleTypeException>();
		}
		#endregion

		#region CanGenerateGenericProxy
		public interface IGeneric<T> { }
		internal class Generic<T> : IGeneric<T> { }
		[Unit]
		public void CanGenerateGenericProxy()
		{
			Action act = () => CreateProxy(typeof(IGeneric<>), typeof(Generic<>));

			act.ShouldThrow<Exception>();
		}
		#endregion

		#region CanResolveGenericProxy
		[Unit]
		public void CanResolveGenericProxy()
		{
			Action act = () => InvokeProxy<IGeneric<object>>(typeof(IGeneric<>), typeof(Generic<>));

			act.ShouldNotThrow();
		}

		#endregion

		private static Func<object> CreateProxy(Type serviceType, Type implementationType)
		{
			var app = new Fake.FakeExecutable();
			app.UseServices(services => services.AddTransient(serviceType, implementationType));
			var providerInfo = typeof(TertiaryProviderExtensions).GetMethod("UseProvider", new[] { typeof(Application), typeof(Action<IServiceCollection>) }).MakeGenericMethod(serviceType);
			Action<IServiceCollection> serviceAction = services => services.AddTransient(serviceType, implementationType);
			providerInfo.Invoke(app, new object[] { app, serviceAction });
			return () => app.ApplicationServices.GetRequiredService(serviceType);
		}

		private static Action InvokeProxy<TInterface, TImplementation>()
			where TImplementation : TInterface where TInterface : class
		{
			return () => SetupProxy<TInterface>(typeof(TImplementation))();
		}

		private static Action InvokeProxy<T>(Type implementationType) where T : class
		{
			return () => SetupProxy<T>(implementationType)();
		}

		private static Action InvokeProxy<T>(Type interfaceType, Type implementationType) where T : class
		{
			return () => SetupProxy<T>(interfaceType, implementationType);
		}

		private static Func<TInterface> SetupProxy<TInterface, TImplementation>()
			where TImplementation : TInterface where TInterface : class
		{
			return SetupProxy<TInterface>(typeof(TImplementation));
		}

		private static Func<T> SetupProxy<T>(Type implementationType) where T : class
		{
			return SetupProxy<T>(typeof(T), implementationType);
		}

		private static Func<T> SetupProxy<T>(Type interfaceType, Type implementationType) where T : class
		{
			return () => (T)CreateProxy(interfaceType, implementationType)();
		}

		#region Common Classes
		public abstract class Base { }

		internal class Derivative : Base { }
		#endregion
	}
}
