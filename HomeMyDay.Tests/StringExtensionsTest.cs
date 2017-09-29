using HomeMyDay.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
	public class StringExtensionsTest
	{
		[Fact]
		public void TestTrimControllerNameWithControllerInName()
		{
			string testString = "HomeController";

			string result = testString.TrimControllerName();

			Assert.Equal("Home", result);
		}

		[Fact]
		public void TestTrimControllerNameWithoutController()
		{
			string testString = "HomeControl";

			string result = testString.TrimControllerName();

			Assert.Equal(testString, result);
		}

		[Fact]
		public void TestTrimControllerNameEmptyString()
		{
			string testString = "";

			string result = testString.TrimControllerName();

			Assert.Equal(testString, result);
		}

		[Fact]
		public void TestTruncateLengthLargerThanString()
		{
			string testString = "TEst string for truncation";

			string result = testString.Truncate(10);

			Assert.Equal("TEst strin...", result);
			Assert.True(result.Length == 13);
		}

		[Fact]
		public void TestTruncateLengthShorterThanString()
		{
			string testString = "TEst string for truncation";

			string result = testString.Truncate(100);

			Assert.Equal(testString, result);
		}

		[Fact]
		public void TestTruncateEmptyString()
		{
			string testString = "";

			string result = testString.Truncate(10);

			Assert.Equal(testString, result);
		}

		[Fact]
		public void TestDashedEmptyString()
		{
			string testString = "";

			string result = testString.Dashed();

			Assert.Equal(testString, result);
		}

		[Fact]
		public void TestDashedStringWithSpaces()
		{
			string testString = "Test ahsdf adshfjk";

			string result = testString.Dashed();

			Assert.Equal("Test-ahsdf-adshfjk", result);
		}

		[Fact]
		public void TestDashedStringWithSpacesAtStartAndEnd()
		{
			string testString = "   Test ahsdf adshfjk  ";

			string result = testString.Dashed();

			Assert.Equal("Test-ahsdf-adshfjk", result);
		}
	}
}
