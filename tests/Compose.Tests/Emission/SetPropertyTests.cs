using FluentAssertions;
using System;
using TestAttributes;

namespace Compose.Tests.Emission
{
	public class SetPropertyTests : EmissionHelpers
	{
		public interface SetProperty { string Property { set; } }

		public class SetPropertyImplementation : SetProperty
		{
			internal static string Id { get; set; }
			public string Property { set { Id = value; } }
		}

		[Unit]
		public static void WhenSettingPropertyOnProxyThenPropertyIsSet()
		{
			var proxy = CreateProxy<SetProperty, SetPropertyImplementation>();
			var value = Guid.NewGuid().ToString();
			proxy.Property = value;
			SetPropertyImplementation.Id.Should().Be(value);
		}
	}
}
