using System;
using FluentAssertions;
using TestAttributes;

namespace Compose.Tests.Emission
{
	public class BasicTests : EmissionHelpers
	{
		public interface Blank { }
		public class BlankeImplementation : Blank { }

		[Unit]
		public static void WhenRequestingBasicProxyThenIsGenerated()
		{
			CreateProxy<Blank, BlankeImplementation>()
				.Should().NotBeNull();
		}
	}
}
