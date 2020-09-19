using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleDotNet._2_Structural
{
    internal static class CompositeExample
    {
        public static void Run()
        {
            var drawing = new GraphicObject {Name = "My Drawing"};
            drawing.Children.Add(new Square {Color = "Red"});
            drawing.Children.Add(new Circle {Color = "Yellow"});

            var group = new GraphicObject();
            group.Children.Add(new Circle {Color = "Blue"});
            group.Children.Add(new Square {Color = "Blue"});
            drawing.Children.Add(group);

            Console.WriteLine(drawing);

            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            neuron1.ConnectTo(neuron2); // 1

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();

            neuron1.ConnectTo(layer1);
            layer1.ConnectTo(layer2);
        }

        internal class GraphicObject
        {
            public virtual string Name { get; set; } = "Group";
            public string Color;

            private readonly Lazy<List<GraphicObject>> _children = new Lazy<List<GraphicObject>>();
            public List<GraphicObject> Children => _children.Value;

            private void Print(StringBuilder sb, int depth)
            {
                sb.Append(new string('*', depth))
                    .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
                    .AppendLine(Name);
                foreach (var child in Children)
                {
                    child.Print(sb, depth + 1);
                }
            }


            public override string ToString()
            {
                var sb = new StringBuilder();
                Print(sb, 0);
                return sb.ToString();
            }
        }

        internal class Circle : GraphicObject
        {
            public override string Name => "Circle";
        }

        internal class Square : GraphicObject
        {
            public override string Name => "Square";
        }
    }

    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class NeuronLayer : Collection<Neuron>
    {
    }

    public class NeuronRing : List<Neuron>
    {
    }

    public static partial class ExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;

            foreach (var from in self)
            foreach (var to in other)
            {
                // from.Out.Add(to);
                // to.In.Add(from);
            }
        }
    }
}
