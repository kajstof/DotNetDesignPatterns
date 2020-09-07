using System;

namespace SampleDotNet
{
    internal static class DynamicDecoratorCompositionExample
    {
        public static void Run()
        {
            var square = new Square(1.23f);
            Console.WriteLine(square.AsString());

            var redSquare = new ColoredShape(square, "red");
            Console.WriteLine(redSquare.AsString());

            var redHalfTransparent = new TransparentShape(redSquare, 0.5f);
            Console.WriteLine(redHalfTransparent.AsString());

            var redSquare2 = new ColoredShape2<Square2>("red");
            Console.WriteLine(redSquare2.AsString());

            var circle2 = new TransparentShape2<ColoredShape2<Circle2>>(0.4f);
            Console.WriteLine(circle2.AsString());
        }

        public interface IShape
        {
            string AsString();
        }

        public class Circle : IShape
        {
            private float radius;

            public Circle(float radius)
            {
                this.radius = radius;
            }

            public void Resize(float factor)
            {
                radius *= factor;
            }

            public string AsString() => $"A circle with radius {radius}";
        }

        public class Square : IShape
        {
            private float side;

            public Square(float side)
            {
                this.side = side;
            }

            public string AsString() => $"A square with side {side}";
        }

        public class ColoredShape : IShape
        {
            private IShape shape;
            private string color;

            public ColoredShape(IShape shape, string color)
            {
                this.shape = shape ?? throw new ArgumentNullException(nameof(shape));
                this.color = color ?? throw new ArgumentNullException(nameof(color));
            }

            public string AsString() => $"{shape.AsString()} has the color {color}";
        }

        public class TransparentShape : IShape
        {
            private IShape shape;
            private float transparency;

            public TransparentShape(IShape shape, float transparency)
            {
                this.transparency = transparency;
                this.shape = shape;
            }

            public string AsString() => $"{shape.AsString()} has {transparency * 100.0}% transparency";
        }


        // C++ approach
        // public class ColoredShape<T> : T // CRTP - Curiously Recurring Template Pattern
        // TransparentShape<ColoredShape<Square>> shape()

        // Static

        public abstract class Shape
        {
            public abstract string AsString();
        }

        public class Circle2 : Shape
        {
            private float radius;

            public Circle2() : this(0.0f)
            {
            }

            public Circle2(float radius)
            {
                this.radius = radius;
            }

            public void Resize(float factor)
            {
                radius *= factor;
            }

            public override string AsString() => $"A circle with radius {radius}";
        }

        public class Square2 : Shape
        {
            private float side;

            public Square2() : this(0.0f)
            {
            }

            public Square2(float side)
            {
                this.side = side;
            }

            public override string AsString() => $"A square with side {side}";
        }

        // public class ColoredShape<T> : T // CRTP - Curiously Recurring Template Pattern
        // TransparentShape<ColoredShape<Square>> shape()

        public class ColoredShape2<T> : Shape where T : Shape, new()
        {
            private string color;
            private T shape = new T();

            public ColoredShape2() : this("black")
            {
            }

            public ColoredShape2(string color)
            {
                this.color = color ?? throw new ArgumentNullException(nameof(color));
            }

            public override string AsString() => $"{shape.AsString()} has the color {color}";
        }

        public class TransparentShape2<T> : Shape where T : Shape, new()
        {
            private float transparency;
            private T shape = new T();

            public TransparentShape2() : this(.0f)
            {
            }

            public TransparentShape2(float transparency)
            {
                this.transparency = transparency;
            }

            public override string AsString() => $"{shape.AsString()} has {transparency * 100.0f}% transparency";
        }
    }
}
