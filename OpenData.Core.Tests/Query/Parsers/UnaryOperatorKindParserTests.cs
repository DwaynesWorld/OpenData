namespace OpenData.Core.Tests.Query.Parsers
{
    using System;
    using OpenData.Core;
    using OpenData.Core.Query.Expressions;
    using OpenData.Core.Query.Parsers;
    using Xunit;

    public class UnaryOperatorKindParserTests
    {
        [Fact]
        public void ToUnaryOperatorKindReturnsNotForNot()
        {
            Assert.Equal(UnaryOperatorKind.Not, "not".ToUnaryOperatorKind());
        }

        [Fact]
        public void ToUnaryOperatorKindThrowsArgumentExceptionForUnsupportedOperatorKind()
        {
            var exception = Assert.Throws<ArgumentException>(() => "wibble".ToUnaryOperatorKind());

            Assert.Equal(Messages.UnknownOperator.FormatWith("wibble") + "\r\nParameter name: operatorType", exception.Message);
        }
    }
}