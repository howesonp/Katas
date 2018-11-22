using System;
using CodeKatas.InstrumentProcessorKata;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class InstrumentProcessorShould
    {
        [Test]
        public void CallGetTaskOnTheTaskDispatcher_WhenProcessingTask()
        {
            var taskDispatcher = Substitute.For<ITaskDispatcher>();
            var instrument = Substitute.For<IInstrument>();
            var instrumentProcessor = new InstrumentProcessor(taskDispatcher, instrument);

            instrumentProcessor.Process();

            taskDispatcher.Received(1).GetTask();
        }

        [Test]
        public void ThrowANullReferenceException_WhenGivenANullTask()
        {
            var taskDispatcher = Substitute.For<ITaskDispatcher>();
            taskDispatcher.GetTask().Returns((string) null);
            var instrument = Substitute.For<IInstrument>();
            instrument.When(x => x.Execute(null)).Do(x => throw new NullReferenceException());
            var instrumentProcessor = new InstrumentProcessor(taskDispatcher, instrument);

            Action process = () => instrumentProcessor.Process();

            process.Should().Throws<ArgumentNullException>();
        }

        [Test]
        public void ReturnTaskFinished_WhenProcessing_ASuccessfulTask()
        {
            var taskDispatcher = Substitute.For<ITaskDispatcher>();
            taskDispatcher.GetTask().Returns("Success");
            var instrument = Substitute.For<IInstrument>();
            instrument.When(i => i.Execute("Success")).Do(i => instrument.Finished += Raise.EventWith(new InstrumentProcessEventArgs("Success")));
            var instrumentProcessor = new InstrumentProcessor(taskDispatcher, instrument);

            instrumentProcessor.Process();

            instrument.Received(1).Execute("Success");
        }

        [Test]
        public void RaiseErrorEvent_WhenProcessing_AnTaskWhichShouldError()
        {
            var taskDispatcher = Substitute.For<ITaskDispatcher>();
            taskDispatcher.GetTask().Returns("ShouldError");
            var instrument = Substitute.For<IInstrument>();
            instrument.When(i => i.Execute("ShouldError")).Do(i => instrument.Error += Raise.EventWith(new InstrumentProcessEventArgs("ShouldError")));
            var instrumentProcessor = new InstrumentProcessor(taskDispatcher, instrument);

            instrumentProcessor.Process();

            instrument.Received(1).Execute("ShouldError");
        }

        [Test]
        public void CallTaskDispatcher_WithCorrectTaskName_WhenSuccessfullyFinishingATask()
        {
            var taskDispatcher = Substitute.For<ITaskDispatcher>();
            taskDispatcher.GetTask().Returns("GoodTask");
            var instrument = Substitute.For<IInstrument>();
            instrument.When(i => i.Execute("GoodTask")).Do(i => instrument.Finished += Raise.EventWith(new InstrumentProcessEventArgs("GoodTask")));
            var instrumentProcessor = new InstrumentProcessor(taskDispatcher, instrument);

            instrumentProcessor.Process();

            instrument.Received(1).Execute("GoodTask");
            taskDispatcher.Received(1).FinishedTask("GoodTask");
        }
    }
}
