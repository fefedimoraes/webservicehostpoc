using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using WebServiceHostPoc;
using WebServiceHostPoc.Attributes;
using Xunit;

namespace Tests
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
            Expression<Func<AttributeUsageAttribute, bool>> expectedAttributeUsage = attribute =>
                !attribute.AllowMultiple &&
                attribute.Inherited &&
                attribute.ValidOn == AttributeTargets.Method;

            GetAttribute.Should().BeAssignableTo<HttpMethodAttribute>();
            GetAttribute.GetType().Should().BeDecoratedWith(expectedAttributeUsage);
        }
    }
}
