using System;
using System.Linq.Expressions;

namespace EmailClient.UI.VM.Core
{
    public static class SymbolHelpers
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }
    }
}