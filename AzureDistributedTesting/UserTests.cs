using System;
using Xunit;

namespace AzureDistributedTesting
{
    public class UserTests
    {
        [Fact]
        public void TestName()
        {
            var user = new { name = "Rich Hadley" };

            Assert.Equal("Rich Hadley1", user.name);
        }
    }
}
