using System;
using System.Dynamic;
using Autofac;
using ImpromptuInterface;

namespace SampleDotNet._3_Behavioral
{
    internal static class NullObjectExample
    {
        public static void Run()
        {
            var log = new ConsoleLog();
            var ba = new BankAccount(log);
            ba.Deposit(100);

            var cb = new ContainerBuilder();
            // cb.RegisterType<BankAccount>();
            // cb.RegisterInstance((ILog) null);
            // cb.Register(ctx => new BankAccount(null));
            cb.RegisterType<BankAccount>();
            cb.RegisterType<NullLog>().As<ILog>();
            using (var c = cb.Build())
            {
                var ba2 = c.Resolve<BankAccount>();
            }

            var log3 = Null<ILog>.Instance;
            log3.Info("dafasdfas");
            var ba3 = new BankAccount(log3);
            ba3.Deposit(100);
        }

        class ConsoleLog : ILog
        {
            public void Info(string msg)
            {
                Console.WriteLine(msg);
            }

            public void Warn(string msg)
            {
                Console.WriteLine("Warning!!! " + msg);
            }
        }

        class NullLog : ILog
        {
            public void Info(string msg)
            {
            }

            public void Warn(string msg)
            {
            }
        }

        public class Null<TInterface> : DynamicObject where TInterface : class
        {
            public static TInterface Instance => new Null<TInterface>().ActLike<TInterface>();

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = Activator.CreateInstance(binder.ReturnType);
                return true;
            }
        }

        public class BankAccount
        {
            private ILog log;
            private int balance;

            // public BankAccount([CanBeNull] ILog log)
            public BankAccount(ILog log)
            {
                // this.log = log ?? throw new ArgumentNullException(nameof(log));
                this.log = log;
            }

            public void Deposit(int amount)
            {
                balance = amount;
                log.Info($"Deposited {amount}, balance is now {balance}");
            }
        }
    }

    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }
}
