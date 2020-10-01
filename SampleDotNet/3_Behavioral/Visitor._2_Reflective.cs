using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace SampleDotNet._3_Behavioral.Visitor._2_Reflective
{
    public static class Example
    {
        public static void Run()
        {
            Console.WriteLine($"== {typeof(Example).FullName} ==");
            var e = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                    new DoubleExpression(2),
                    new DoubleExpression(3)));

            var sb = new StringBuilder();
            ExpressionPrinter.Print(e, sb);
            Console.WriteLine(sb);
        }
    }

    public abstract class Expression
    {
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            this.Value = value;
        }
    }

    public class AdditionExpression : Expression
    {
        public Expression Left;
        public Expression Right;

        public AdditionExpression([NotNull] Expression left, [NotNull] Expression right)
        {
            this.Left = left ?? throw new ArgumentNullException(nameof(left));
            this.Right = right ?? throw new ArgumentNullException(nameof(right));
        }
    }

    public static class ExpressionPrinter
    {
        private static Dictionary<Type, Action<Expression, StringBuilder>> actions =
            new Dictionary<Type, Action<Expression, StringBuilder>>
            {
                [typeof(DoubleExpression)] = (e, sb) =>
                {
                    var de = (DoubleExpression) e;
                    sb.Append(de.Value);
                },
                [typeof(AdditionExpression)] = (e, sb) =>
                {
                    var ae = (AdditionExpression) e;
                    sb.Append("(");
                    Print(ae.Left, sb);
                    sb.Append("+");
                    Print(ae.Right, sb);
                    sb.Append(")");
                }
            };

        public static void Print(Expression e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
        }

        // public static void Print(Expression e, StringBuilder sb)
        // {
        //     if (e is DoubleExpression de)
        //     {
        //         sb.Append(de.Value);
        //     }
        //     else if (e is AdditionExpression ae)
        //     {
        //         sb.Append("(");
        //         Print(ae.Left, sb);
        //         sb.Append("+");
        //         Print(ae.Right, sb);
        //         sb.Append(")");
        //     }
        // }
    }
}
