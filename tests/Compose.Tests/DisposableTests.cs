﻿using Microsoft.Framework.DependencyInjection;
using System;
using Xunit;

namespace Compose.Tests
{
    public class DisposableTests
	{
		[Fact]
		public void WhenTransitioningAwayFromDirectlyImplementedDisposableThenDisposesCurrentService()
		{
			var app = new Fake.Application();
			app.UseServices(services => services
				.AddTransitional<IDependency, DirectlyDisposableDependency>()
				.AddTransient<Dependency, Dependency>()
			);
			app.OnExecute<IDependency>(dependency =>
			{
				Action act = app.Transition<IDependency, Dependency>;
				Assert.Throws<NotImplementedException>(act);
			});
		}

		[Fact]
		public void WhenTransitioningAwayFromIndirectlyImplementedDisposableThenDisposesCurrentService()
		{
			var app = new Fake.Application();
			app.UseServices(services => services
				.AddTransitional<IDependency, IndirectlyDisposableDependency>()
				.AddTransient<Dependency, Dependency>()
			);
			app.OnExecute<IDependency>(dependency =>
			{
				Action act = app.Transition<IDependency, Dependency>;
				Assert.Throws<NotImplementedException>(act);
			});
		}

		[Fact]
		public void WhenSnapshottingAwayFromDirectlyImplementedDisposableThenDisposesCurrentService()
		{
			var app = new Fake.Application();
			app.UseServices(services => services
				.AddTransitional<IDependency, DirectlyDisposableDependency>()
				.AddTransient<Dependency, Dependency>()
			);
			app.OnExecute<IDependency>(dependency =>
			{
				Action act = app.Snapshot;
				Assert.Throws<NotImplementedException>(act);
			});
		}

		[Fact]
		public void WhenSnapshottingAwayFromIndirectlyImplementedDisposableThenDisposesCurrentService()
		{
			var app = new Fake.Application();
			app.UseServices(services => services
				.AddTransitional<IDependency, IndirectlyDisposableDependency>()
				.AddTransient<Dependency, Dependency>()
			);
			app.OnExecute<IDependency>(dependency =>
			{
				Action act = app.Snapshot;
				Assert.Throws<NotImplementedException>(act);
			});
		}

		[Fact]
		public void WhenRestoringAwayFromDirectlyImplementedDisposableThenDisposesCurrentService()
		{
			var app = new Fake.Application();
			app.UseServices(services => services
				.AddTransitional<IDependency, DirectlyDisposableDependency>()
				.AddTransient<Dependency, Dependency>()
			);
			app.OnExecute<IDependency>(dependency =>
			{
				Action act = app.Restore;
				Assert.Throws<NotImplementedException>(act);
			});
		}

		[Fact]
		public void WhenRestoringAwayFromIndirectlyImplementedDisposableThenDisposesCurrentService()
		{
			var app = new Fake.Application();
			app.UseServices(services => services
				.AddTransitional<IDependency, IndirectlyDisposableDependency>()
				.AddTransient<Dependency, Dependency>()
			);
			app.OnExecute<IDependency>(dependency =>
			{
				Action act = app.Restore;
				Assert.Throws<NotImplementedException>(act);
			});
		}

		internal interface IDependency { }

		private class Dependency : IDependency { }

		private class DirectlyDisposableDependency : IDependency, IDisposable
		{
			public void Dispose() { throw new NotImplementedException(); }
		}

		private interface IDisposableDependency : IDependency, IDisposable { }

		private class IndirectlyDisposableDependency : IDisposableDependency
		{
			public void Dispose() { throw new NotImplementedException(); }
		}
	}
}
