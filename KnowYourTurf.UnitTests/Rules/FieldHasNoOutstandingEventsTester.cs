using System;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Config;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests.Rules
{
    public class FieldHasNoOutstandingEventsTester
    {
    }

    [TestFixture]
    public class when_calling_execute_on_FieldHasNoOutstandingEvents_when_there_are_future_events
    {
        private Field _field;
        private Event _event1;
        private Event _event2;
        private Event _event3;
        private SystemClock _systemClock;
        private FieldHasNoOutstandingEvents _SUT;
        private RuleResult _ruleResult;

        [SetUp]
        public void Setup()
        {
            Bootstrapper.BootstrapTest();
            _systemClock = SystemClock.For(DateTime.Parse("1/1/2020 8:00 AM"));
            _field = ObjectMother.ValidField("Raif").WithEntityId(1);
            _event1 = ObjectMother.ValidEvent("event1").WithEntityId(1);
            _event1.StartTime = DateTime.Parse("1/2/2020 8:00 AM");
            _event2 = ObjectMother.ValidEvent("event1").WithEntityId(2);
            _event2.StartTime = DateTime.Parse("1/3/2020 8:00 AM");
            _event3 = ObjectMother.ValidEvent("event1").WithEntityId(3);
            _event3.StartTime = DateTime.Parse("1/1/2010 8:00 AM");
            _field.AddEvent(_event1);
            _field.AddEvent(_event2);
            _field.AddEvent(_event3);
            _SUT = new FieldHasNoOutstandingEvents(_systemClock);
            _ruleResult = _SUT.Execute(_field);
        }

        [Test]
        public void should_check_fieldevents_for_events_in_the_future_and_set_success_accordingly() 
        {
            _ruleResult.Success.ShouldBeFalse();
        }

        [Test]
        public void should_return_proper_error_message_if_there_are_future_events()
        {
            _ruleResult.Message.ShouldEqual(CoreLocalizationKeys.FIELD_HAS_EVENTS_IN_FUTURE.ToFormat(2));
        }

    }

    [TestFixture]
    public class when_calling_execute_on_FieldHasNoOutstandingEvents_when_there_are_NO_future_events
    {
        private Field _field;
        private Event _event1;
        private Event _event2;
        private Event _event3;
        private SystemClock _systemClock;
        private FieldHasNoOutstandingEvents _SUT;
        private RuleResult _ruleResult;

        [SetUp]
        public void Setup()
        {
            Bootstrapper.BootstrapTest();
            _systemClock = SystemClock.For(DateTime.Parse("1/1/2020 8:00 AM"));
            _field = ObjectMother.ValidField("Raif").WithEntityId(1);
            _event1 = ObjectMother.ValidEvent("event1").WithEntityId(1);
            _event1.StartTime = DateTime.Parse("1/2/2010 8:00 AM");
            _event2 = ObjectMother.ValidEvent("event1").WithEntityId(2);
            _event2.StartTime = DateTime.Parse("1/3/2010 8:00 AM");
            _event3 = ObjectMother.ValidEvent("event1").WithEntityId(3);
            _event3.StartTime = DateTime.Parse("1/1/2010 8:00 AM");
            _field.AddEvent(_event1);
            _field.AddEvent(_event2);
            _field.AddEvent(_event3);
            _SUT = new FieldHasNoOutstandingEvents(_systemClock);
            _ruleResult = _SUT.Execute(_field);
        }

        [Test]
        public void should_check_fieldevents_for_events_in_the_future_and_set_success_accordingly()
        {
            _ruleResult.Success.ShouldBeTrue();
        }

        [Test]
        public void should_return_proper_error_message_if_there_are_future_events()
        {
            _ruleResult.Message.ShouldBeNull();
        }

    }
}