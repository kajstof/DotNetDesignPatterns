using System;
using System.Text;
using JetBrains.Annotations;

namespace SampleDotNet._3_Behavioral.Visitor._4_Dynamic
{
    public static class Example
    {
        public static void Run()
        {
            Console.WriteLine($"== {typeof(Example).FullName} ==");
            Expression e = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                    new DoubleExpression(2),
                    new DoubleExpression(3)));

            var ep = new ExpressionPrinter();
            var sb = new StringBuilder();
            ep.Print((dynamic) e, sb);
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

    public class ExpressionPrinter
    {
        public void Print(AdditionExpression ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic) ae.Left, sb);
            sb.Append("+");
            Print((dynamic) ae.Right, sb);
            sb.Append(")");
        }

        public void Print(DoubleExpression de, StringBuilder sb)
        {
            sb.Append(de.Value);
        }
    }
}
