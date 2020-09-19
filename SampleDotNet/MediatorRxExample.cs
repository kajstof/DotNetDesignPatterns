﻿using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Autofac;

namespace SampleDotNet
{
    internal static class MediatorRxExample
    {
        public static void Run()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<FootballCoach>();
            cb.Register((c, p) => new FootballPlayer(
                c.Resolve<EventBroker>(), p.Named<string>("name")));

            using (var c = cb.Build())
            {
                var coach = c.Resolve<FootballCoach>();
                var player1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "John"));
                var player2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));

                player1.Score();
                player1.Score();
                player1.Score(); // notification should be ignore
                player2.AssaultReferee();
                player2.Score();
            }
        }

        public class Actor
        {
            protected EventBroker broker;

            public Actor(EventBroker broker)
            {
                this.broker = broker ?? throw new ArgumentNullException(nameof(broker));
            }
        }

        public class FootballPlayer : Actor
        {
            public string Name { get; set; }
            public int GoalsScored { get; set; } = 0;

            public FootballPlayer(EventBroker broker, string name) : base(broker)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));

                broker.OfType<PlayerScoredEvent>()
                    .Where(ps => !ps.Name.Equals(name))
                    .Subscribe(ps =>
                        Console.WriteLine($"{name}: Nicely done, {ps.Name}! It's your {ps.GoalsScored} goal."));

                broker.OfType<PlayerSentOffEvent>()
                    .Where(ps => !ps.Name.Equals(name))
                    .Subscribe(ps => Console.WriteLine($"{name}: see you in the lockers, {ps.Name}."));
            }

            public void Score()
            {
                GoalsScored++;
                broker.Publish(new PlayerScoredEvent {Name = Name, GoalsScored = GoalsScored});
            }

            public void AssaultReferee()
            {
                broker.Publish(new PlayerSentOffEvent {Name = Name, Reason = "violence"});
            }
        }

        public class FootballCoach : Actor
        {
            public FootballCoach(EventBroker broker) : base(broker)
            {
                broker.OfType<PlayerScoredEvent>()
                    .Subscribe(pe =>
                    {
                        if (pe.GoalsScored < 3)
                            Console.WriteLine($"Coach: well done, {pe.Name}!");
                    });

                broker.OfType<PlayerSentOffEvent>()
                    .Subscribe(pe =>
                    {
                        if (pe.Reason == "violence")
                            Console.WriteLine($"Coach: how could you, {pe.Name}.");
                    });
            }
        }

        public class PlayerEvent
        {
            public string Name { get; set; }
        }

        public class PlayerScoredEvent : PlayerEvent
        {
            public int GoalsScored { get; set; }
        }

        public class PlayerSentOffEvent : PlayerEvent
        {
            public string Reason { get; set; }
        }

        public class EventBroker : IObservable<PlayerEvent>
        {
            Subject<PlayerEvent> subscriptions = new Subject<PlayerEvent>();

            public IDisposable Subscribe(IObserver<PlayerEvent> observer)
            {
                return subscriptions.Subscribe(observer);
            }

            public void Publish(PlayerEvent pe)
            {
                subscriptions.OnNext(pe);
            }
        }
    }
}
