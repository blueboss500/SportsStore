using System;
using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var repo = new FakeProductRespository();
            Assert.Equal(2, repo.Products.Count(p => p.Price < 100));
        }
    }
}
