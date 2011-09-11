using System;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Config;
using NUnit.Framework;
using Rhino.Mocks;

namespace KnowYourTurf.UnitTests.Rules
{
    public class EmployeeHasNoOutstandingTasksTester
    {
    }

    [TestFixture]
    public class when_calling_execute_on_EmployeeHasNoOutstandingTasks_when_there_are_future_tasks
    {
        private Employee _employee;
        private Task _task1;
        private Task _task2;
        private Task _task3;
        private SystemClock _systemClock;
        private EmployeeHasNoOutstandingTasks _SUT;
        private RuleResult _ruleResult;

        [SetUp]
        public void Setup()
        {
            Bootstrapper.BootstrapTest();
            _systemClock = SystemClock.For(DateTime.Parse("1/1/2020 8:00 AM"));
            _employee = ObjectMother.ValidEmployee("Raif").WithEntityId(1);
            _task1 = ObjectMother.ValidTask("task1").WithEntityId(1);
            _task1.ScheduledStartTime = DateTime.Parse("1/2/2020 8:00 AM");
            _task2 = ObjectMother.ValidTask("task1").WithEntityId(2);
            _task2.ScheduledStartTime = DateTime.Parse("1/3/2020 8:00 AM");
            _task3 = ObjectMother.ValidTask("task1").WithEntityId(3);
            _task3.ScheduledStartTime = DateTime.Parse("1/1/2010 8:00 AM");
            _employee.AddTask(_task1);
            _employee.AddTask(_task2);
            _employee.AddTask(_task3);
            _SUT = new EmployeeHasNoOutstandingTasks(_systemClock);
            _ruleResult = _SUT.Execute(_employee);
        }

        [Test]
        public void should_check_employeetasks_for_tasks_in_the_future_and_set_success_accordingly() 
        {
            _ruleResult.Success.ShouldBeFalse();
        }

        [Test]
        public void should_return_proper_error_message_if_there_are_future_tasks()
        {
            _ruleResult.Message.ShouldEqual(CoreLocalizationKeys.EMPLOYEE_HAS_TASKS_IN_FUTURE.ToFormat(2));
        }

    }

    [TestFixture]
    public class when_calling_execute_on_EmployeeHasNoOutstandingTasks_when_there_are_NO_future_tasks
    {
        private Employee _employee;
        private Task _task1;
        private Task _task2;
        private Task _task3;
        private SystemClock _systemClock;
        private EmployeeHasNoOutstandingTasks _SUT;
        private RuleResult _ruleResult;

        [SetUp]
        public void Setup()
        {
            Bootstrapper.BootstrapTest();
            _systemClock = SystemClock.For(DateTime.Parse("1/1/2020 8:00 AM"));
            _employee = ObjectMother.ValidEmployee("Raif").WithEntityId(1);
            _task1 = ObjectMother.ValidTask("task1").WithEntityId(1);
            _task1.ScheduledStartTime = DateTime.Parse("1/2/2010 8:00 AM");
            _task2 = ObjectMother.ValidTask("task1").WithEntityId(2);
            _task2.ScheduledStartTime = DateTime.Parse("1/3/2010 8:00 AM");
            _task3 = ObjectMother.ValidTask("task1").WithEntityId(3);
            _task3.ScheduledStartTime = DateTime.Parse("1/1/2010 8:00 AM");
            _employee.AddTask(_task1);
            _employee.AddTask(_task2);
            _employee.AddTask(_task3);
            _SUT = new EmployeeHasNoOutstandingTasks(_systemClock);
            _ruleResult = _SUT.Execute(_employee);
        }

        [Test]
        public void should_check_employeetasks_for_tasks_in_the_future_and_set_success_accordingly()
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