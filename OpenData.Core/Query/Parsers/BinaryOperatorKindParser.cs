namespace OpenData.Core.Query.Parsers
{
    using System;
    using System.Collections.Generic;
    using Expressions;

    internal static class BinaryOperatorKindParser
    {
        private static Dictionary<string, BinaryOperatorKind> operatorTypeMap = new Dictionary<string, BinaryOperatorKind>
        {
            ["add"] = BinaryOperatorKind.Add,
            ["and"] = BinaryOperatorKind.And,
            ["div"] = BinaryOperatorKind.Divide,
            ["eq"] = BinaryOperatorKind.Equal,
            ["ge"] = BinaryOperatorKind.GreaterThanOrEqual,
            ["gt"] = BinaryOperatorKind.GreaterThan,
            ["has"] = BinaryOperatorKind.Has,
            ["le"] = BinaryOperatorKind.LessThanOrEqual,
            ["lt"] = BinaryOperatorKind.LessThan,
            ["mul"] = BinaryOperatorKind.Multiply,
            ["mod"] = BinaryOperatorKind.Modulo,
            ["ne"] = BinaryOperatorKind.NotEqual,
            ["or"] = BinaryOperatorKind.Or,
            ["sub"] = BinaryOperatorKind.Subtract,
        };

        internal static BinaryOperatorKind ToBinaryOperatorKind(this string operatorType)
        {
            if (operatorTypeMap.TryGetValue(operatorType, out BinaryOperatorKind binaryOperatorKind))
            {
                return binaryOperatorKind;
            }

            throw new ArgumentException(Messages.UnknownOperator.FormatWith(operatorType), nameof(operatorType));
        }
    }
}