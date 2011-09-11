using System;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Config;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests.Rules
{
    public class FieldHasNoOutstandingTasksTester
    {
    }

    [TestFixture]
    public class when_calling_execute_on_FieldHasNoOutstandingTasks_when_there_are_future_tasks
    {
        private Field _field;
        private Task _task1;
        private Task _task2;
        private Task _task3;
        private SystemClock _systemClock;
        private FieldHasNoOutstandingTasks _SUT;
        private RuleResult _ruleResult;

        [SetUp]
        public void Setup()
        {
            Bootstrapper.BootstrapTest();
            _field = ObjectMother.ValidField("Raif").WithEntityId(1);
            _task1 = ObjectMother.ValidTask("task1").WithEntityId(1);
            _task1.ScheduledStartTime = DateTime.Parse("1/2/2020 8:00 AM");
            _task2 = ObjectMother.ValidTask("task1").WithEntityId(2);
            _task2.ScheduledStartTime = DateTime.Parse("1/3/2020 8:00 AM");
            _task3 = ObjectMother.ValidTask("task1").WithEntityId(3);
            _task3.ScheduledStartTime = DateTime.Parse("1/1/2010 8:00 AM");
            _field.AddPendingTask(_task1);
            _field.AddPendingTask(_task2);
            _field.AddCompletedTask(_task3);
            _SUT = new FieldHasNoOutstandingTasks();
            _ruleResult = _SUT.Execute(_field);
        }

        [Test]
        public void should_check_fieldtasks_for_tasks_in_the_future_and_set_success_accordingly() 
        {
            _ruleResult.Success.ShouldBeFalse();
        }

        [Test]
        public void should_return_proper_error_message_if_there_are_future_tasks()
        {
            _ruleResult.Message.ShouldEqual(CoreLocalizationKeys.FIELD_HAS_TASKS_IN_FUTURE.ToFormat(2));
        }

    }

    [TestFixture]
    public class when_calling_execute_on_FieldHasNoOutstandingTasks_when_there_are_NO_future_tasks
    {
        private Field _field;
        private Task _task1;
        private Task _task2;
        private Task _task3;
        private SystemClock _systemClock;
        private FieldHasNoOutstandingTasks _SUT;
        private RuleResult _ruleResult;

        [SetUp]
        public void Setup()
        {
            Bootstrapper.BootstrapTest();
            _field = ObjectMother.ValidField("Raif").WithEntityId(1);
            _task1 = ObjectMother.ValidTask("task1").WithEntityId(1);
            _task1.ScheduledStartTime = DateTime.Parse("1/2/2010 8:00 AM");
            _task2 = ObjectMother.ValidTask("task1").WithEntityId(2);
            _task2.ScheduledStartTime = DateTime.Parse("1/3/2010 8:00 AM");
            _task3 = ObjectMother.ValidTask("task1").WithEntityId(3);
            _task3.ScheduledStartTime = DateTime.Parse("1/1/2010 8:00 AM");
            _field.AddCompletedTask(_task1);
            _field.AddCompletedTask(_task2);
            _field.AddCompletedTask(_task3);
            _SUT = new FieldHasNoOutstandingTasks();
            _ruleResult = _SUT.Execute(_field);
        }

        [Test]
        public void should_check_fieldtasks_for_tasks_in_the_future_and_set_success_accordingly()
        {
            _ruleResult.Success.ShouldBeTrue();
        }

        [Test]
        public void should_return_proper_error_message_if_there_are_future_tasks()
        {
            _ruleResult.Message.ShouldBeNull();
        }

    }
}