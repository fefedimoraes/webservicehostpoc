using System;
using FluentAssertions;
using WebServiceHostPoc.Attributes;
using Xunit;
using System.Reflection;

namespace WebServiceHostPoc.Unit.Tests
{
    public class GetAttributeUnitTests
    {
        public GetAttributeUnitTests()
        {
            GetAttribute = new GetAttribute("/");
        }

        public GetAttribute GetAttribute { get; }

        [Fact]
        public void GetAttributeClass_ShouldBeAnHttpMethodAttribute()
        {
            GetAttribute.Should().BeAssignableTo<HttpMethodAttribute>();
            GetAttribute.GetType().GetTypeInfo().GetCustomAttributes<AttributeUsageAttribute>(true)
                .Should().HaveCount(1)
                .And.ContainSingle(_ => !_.AllowMultiple && _.Inherited && _.ValidOn == AttributeTargets.Method);
        }
    }
}
