using System.Collections.Generic;
using System.Linq;
using SampleDotNet._1_Creational;
using SampleDotNet._2_Structural;
using SampleDotNet._3_Behavioral;

namespace SampleDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Creational
            // BuilderAbstractExample.Run();
            // FacetedBuilderExample.Run();
            // FactoryExample.Run();
            // AbstractFactoryExample.Run();
            // PrototypeExample.Run();
            // SingletonExample.Run();

            // 2. Structural
            // AdapterExample.Run();
            // BridgeExample.Run();
            // CompositeExample.Run();
            // DecoratorExample.Run();
            // AdapterDecoratorExample.Run();
            // DecoratorMultipleInheritanceExample.Run();
            // DynamicDecoratorCompositionExample.Run();
            // FlyweightExample.Run();
            // FlyweightFormatTextExample.Run();
            // ProtectionProxyExample.Run();
            // PropertyProxyExample.Run();
            // DynamicProxyExample.Run();

            // 3. Behavioral
            // ChainOfResponsibilityExample.Run();
            // ChainOfResponsibilityExample2.Run();
            // CommandExample.Run();
            // InterpreterExample.Run();
            // IteratorExample.Run();
            // MediatorExample.Run();
            // MediatorRxExample.Run();
            // MementoExample.Run();
            // NullObjectExample.Run();
            // ObserverExample.Run();
            // ObserverExample2.Run();
            // ObserverExample3.Run();
            // StateHandmadeExample.Run();
            // StateSwitchBasedExample.Run();
            // StateStatelessExample.Run();
            // StrategyStaticExample.Run();
            // StrategyDynamicExample.Run();
            // TemplateMethodExample.Run();
            _3_Behavioral.Visitor._1_Intrusive.Example.Run();
            _3_Behavioral.Visitor._2_Reflective.Example.Run();
            _3_Behavioral.Visitor._3_Classic.Example.Run();
            _3_Behavioral.Visitor._4_Dynamic.Example.Run();
            _3_Behavioral.Visitor._5_Acyclic.Example.Run();
        }
    }
}
