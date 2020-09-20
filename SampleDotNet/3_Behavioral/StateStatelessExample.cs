using System;
using System.Text;
using Stateless;

namespace SampleDotNet._3_Behavioral
{
    internal static class StateStatelessExample
    {
        public static void Run()
        {
            var machine = new StateMachine<Health, Activity>(Health.NonReproductive);
            machine.Configure(Health.NonReproductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive);
            machine.Configure(Health.Reproductive)
                .Permit(Activity.Historectomy, Health.NonReproductive)
                .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant, () => ParentsNotWatching);
            machine.Configure(Health.Pregnant)
                .Permit(Activity.GiveBirth, Health.Reproductive)
                .Permit(Activity.HaveAbortion, Health.Reproductive);
        }

        public static bool ParentsNotWatching { get; set; }

        public enum Health
        {
            NonReproductive,
            Pregnant,
            Reproductive
        }

        public enum Activity
        {
            GiveBirth,
            ReachPuberty,
            HaveAbortion,
            HaveUnprotectedSex,
            Historectomy
        }
    }
}
