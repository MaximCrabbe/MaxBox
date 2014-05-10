﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MaxBox.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression Replace(this Expression expression, Expression searchEx, Expression replaceEx)
        {
            return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
        }

        public static Expression<Func<TFirstParam, TResult>> Compose<TFirstParam, TIntermediate, TResult>(this Expression<Func<TFirstParam, TIntermediate>> first, Expression<Func<TIntermediate, TResult>> second)
        {
            var param = Expression.Parameter(typeof(TFirstParam), "param");
            var newFirst = first.Body.Replace(first.Parameters[0], param);
            var newSecond = second.Body.Replace(second.Parameters[0], newFirst);
            return Expression.Lambda<Func<TFirstParam, TResult>>(newSecond, param);
        }
    }
    internal class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression _from, _to;
        public ReplaceVisitor(Expression from, Expression to)
        {
            this._from = from;
            this._to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == _from ? _to : base.Visit(node);
        }
    }
}
