
namespace OpenData.Core
{
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ExpressionExtensions
    {
        internal static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambdaExpression = (LambdaExpression)expression;

            var memberExpression = lambdaExpression.Body is UnaryExpression unaryExpression
                ? (MemberExpression)unaryExpression.Operand
                : (MemberExpression)lambdaExpression.Body;

            return memberExpression.Member;
        }
    }
}