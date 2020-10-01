using System;
using System.Text;
using JetBrains.Annotations;

namespace SampleDotNet._3_Behavioral.Visitor._1_Intrusive
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
            e.Print(sb);
            Console.WriteLine(sb);
        }
    }

    public abstract class Expression
    {
        public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression : Expression
    {
        private double value;

        public DoubleExpression(double value)
        {
            this.value = value;
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append(value);
        }
    }

    public class AdditionExpression : Expression
    {
        private Expression left, right;

        public AdditionExpression([NotNull] Expression left, [NotNull] Expression right)
        {
            this.left = left ?? throw new ArgumentNullException(nameof(left));
            this.right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            left.Print(sb);
            sb.Append("+");
            right.Print(sb);
            sb.Append(")");
        }
    }
}
