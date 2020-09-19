using System;
using Autofac;

namespace SampleDotNet._2_Structural
{
    internal static class BridgeExample
    {
        public static void Run()
        {
            // IRenderer renderer = new RasterRenderer();
            var renderer = new VectorRenderer();
            var circle = new Circle(renderer, 5);
            circle.Draw();
            circle.Resize(2);
            circle.Draw();

            var cb = new ContainerBuilder();
            cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
            cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));
            using (var c = cb.Build())
            {
                var circle2 = c.Resolve<Circle>(new PositionalParameter(0, 5.0f));
                circle2.Draw();
                circle2.Resize(2.0f);
                circle2.Draw();
            }
        }

        public interface IRenderer
        {
            void RenderCircle(float radius);
        }

        public class VectorRenderer : IRenderer
        {
            public void RenderCircle(float radius)
            {
                Console.WriteLine($"Drawing a circle of radius {radius}");
            }
        }

        public class RasterRenderer : IRenderer
        {
            public void RenderCircle(float radius)
            {
                Console.WriteLine($"Drawing pixels for circle with radius {radius}");
            }
        }

        public abstract class Shape
        {
            protected IRenderer _renderer;

            protected Shape(IRenderer renderer)
            {
                _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            }

            public abstract void Draw();
            public abstract void Resize(float factor);
        }

        public class Circle : Shape
        {
            private float _radius;

            public Circle(IRenderer renderer, float radius) : base(renderer)
            {
                this._radius = radius;
            }

            public override void Draw()
            {
                _renderer.RenderCircle(_radius);
            }

            public override void Resize(float factor)
            {
                _radius *= factor;
            }
        }
    }
}
