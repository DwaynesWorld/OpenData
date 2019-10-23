namespace OpenData.Core.Query.Parsers
{
    using System;
    using Expressions;

    internal static class UnaryOperatorKindParser
    {
        internal static UnaryOperatorKind ToUnaryOperatorKind(this string operatorType)
        {
            switch (operatorType)
            {
                case "not":
                    return UnaryOperatorKind.Not;

                default:
                    throw new ArgumentException(Messages.UnknownOperator.FormatWith(operatorType), nameof(operatorType));
            }
        }
    }
}