using System;
using System.Text;
using JetBrains.Annotations;

namespace SampleDotNet._3_Behavioral.Visitor._3_Classic
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

            var ep = new ExpressionPrinter();
            ep.Visit(e);
            Console.WriteLine(ep);

            var calc = new ExpressionCalculator();
            calc.Visit(e);
            Console.WriteLine($"{ep} = {calc.Result}");
        }
    }

    public interface IExpressionVisitor
    {
        void Visit(DoubleExpression de);
        void Visit(AdditionExpression ae);
    }

    public abstract class Expression
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            this.Value = value;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            // double dispatch
            visitor.Visit(this);
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

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class ExpressionPrinter : IExpressionVisitor
    {
        StringBuilder sb = new StringBuilder();

        public void Visit(DoubleExpression de)
        {
            sb.Append(de.Value);
        }

        public void Visit(AdditionExpression ae)
        {
            sb.Append("(");
            ae.Left.Accept(this);
            sb.Append("+");
            ae.Right.Accept(this);
            sb.Append(")");
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    public class ExpressionCalculator : IExpressionVisitor
    {
        public double Result;

        public void Visit(DoubleExpression de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionExpression ae)
        {
            ae.Left.Accept(this);
            var a = Result;
            ae.Right.Accept(this);
            var b = Result;
            Result = a + b;
        }
    }
}
