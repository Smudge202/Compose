namespace Compose.Tests.Emission
{
	// https://github.com/compose-net/compose/issues/106
	public class OpenGenericTests
	{
		// We do not currently support open generics.
		// The following commented out tests are the from compose 0.1.15-beta that relied on support for open generics
		// and will need re-implementing as part of issue 106 linked above.

		#region CanGenerateProxyWithInterfaceConstrainedGenericArguments
		//public interface IInterfaceConstraint { }
		//public interface IInterfaceConstrainedGenericArgument<T> where T : IInterfaceConstraint { }
		//internal class InterfaceConstrainedGenericArgument<T> : IInterfaceConstrainedGenericArgument<T> where T : IInterfaceConstraint { }
		//[Unit]
		//public void CanGenerateProxyWithInterfaceConstrainedGenericArguments()
		//{
		//	Action act = () => CreateProxy(typeof(IInterfaceConstrainedGenericArgument<>), typeof(InterfaceConstrainedGenericArgument<>));

		//	act.ShouldNotThrow();
		//}
		#endregion

		#region CanGenerateProxyWithBaseClassConstrainedGenericArguments
		//public interface IInterfaceWithBaseClassConstrainedGenericArgument<T> where T : Base { }
		//internal class InterfaceWithBaseClassConstrainedGenericArgument<T> : IInterfaceWithBaseClassConstrainedGenericArgument<T> where T : Derivative { }
		//[Unit]
		//public void CanGenerateProxyWithBaseClassConstrainedGenericArguments()
		//{
		//	Action act = () => CreateProxy(typeof(IInterfaceWithBaseClassConstrainedGenericArgument<>), typeof(InterfaceWithBaseClassConstrainedGenericArgument<>));

		//	act.ShouldNotThrow();
		//}
		#endregion

		#region CanGenerateProxyWithDefaultConstructorConstrainedGenericArguments
		//public interface IInterfaceWithDefaultConstructorConstrainedGenericArgument<T> where T : new() { }
		//internal class InterfaceWithDefaultConstructorConstrainedGenericArgument<T> : IInterfaceWithDefaultConstructorConstrainedGenericArgument<T> where T : new() { }
		//[Unit]
		//public void CanGenerateProxyWithDefaultConstructorConstrainedGenericArguments()
		//{
		//	Action act = () => CreateProxy(typeof(IInterfaceWithDefaultConstructorConstrainedGenericArgument<>), typeof(InterfaceWithDefaultConstructorConstrainedGenericArgument<>));

		//	act.ShouldNotThrow();
		//}
		#endregion

		#region CanGenerateProxyWithClassConstrainedGenericArguments
		//public interface IInterfaceWithClassConstrainedGenericArgument<T> where T : class { }
		//internal class InterfaceWithClassConstrainedGenericArgument<T> : IInterfaceWithClassConstrainedGenericArgument<T> where T : class { }
		//[Unit]
		//public void CanGenerateProxyWithClassConstrainedGenericArguments()
		//{
		//	Action act = () => CreateProxy(typeof(IInterfaceWithClassConstrainedGenericArgument<>), typeof(InterfaceWithClassConstrainedGenericArgument<>));

		//	act.ShouldNotThrow();
		//}
		#endregion

		#region CanGenerateProxyWithStructConstrainedGenericArguments
		//public interface IInterfaceWithStructConstrainedGenericArgument<T> where T : struct { }
		//internal class InterfaceWithStructConstrainedGenericArgument<T> : IInterfaceWithStructConstrainedGenericArgument<T> where T : struct { }
		//[Unit]
		//public void CanGenerateProxyWithStructConstrainedGenericArguments()
		//{
		//	Action act = () => CreateProxy(typeof(IInterfaceWithStructConstrainedGenericArgument<>), typeof(InterfaceWithStructConstrainedGenericArgument<>));

		//	act.ShouldNotThrow();
		//}
		#endregion

		#region CanGenerateProxyWithCovariantConstrainedGenericsArguments
		//public interface IInterfaceWithCovariantConstrainedGenericArguments<in TIn, out TOut> { }
		//internal class InterfaceWithCovariantConstrainedGenericArguments<TIn, TOut> : IInterfaceWithCovariantConstrainedGenericArguments<TIn, TOut> { }
		//[Unit]
		//public void CanGenerateProxyWithCovariantConstrainedGenericsArguments()
		//{
		//	Action act = () => CreateProxy(typeof(IInterfaceWithCovariantConstrainedGenericArguments<,>), typeof(InterfaceWithCovariantConstrainedGenericArguments<,>));

		//	act.ShouldNotThrow();
		//}
		#endregion
	}
}
