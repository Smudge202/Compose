using System;
using FluentAssertions;
using TestAttributes;

namespace Compose.Tests.Emission
{
	public class GetPropertyTests : EmissionHelpers
	{
		public interface GetProperty { string Property { get; } }

		public class GetPropertyImplementation : GetProperty
		{
			internal static string Id { get; set; }
			public string Property => Id;
		}

		[Unit]
		public static void WhenGettingPropertyFromProxyThenPropertyIsReturned()
		{
			var proxy = CreateProxy<GetProperty, GetPropertyImplementation>();
			proxy.Should().NotBeNull();
			GetPropertyImplementation.Id = Guid.NewGuid().ToString();
			proxy.Property.Should().Be(GetPropertyImplementation.Id);
		}
	}
}
