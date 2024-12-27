using System.Linq.Expressions;

namespace EInvoiceDemo.Shared.Helpers;

public static class ReflectionExtensions
{
    public static MemberExpression? GetMemberExpression<T, TValue>(this Expression<Func<T, TValue>> expr)
    {
        if (expr.Body is UnaryExpression unary)
            return unary.Operand as MemberExpression;

        return expr.Body as MemberExpression;
    }
}
