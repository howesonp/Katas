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
            taskDispatcher.GetTask().Returns((string)null);
            var instrument = Substitute.For<IInstrument>();
            instrument.Execute("Success");
            instrument.Finished += Raise.EventWith(new EventArgs());
            var instrumentProcessor = new InstrumentProcessor(taskDispatcher, instrument);

            instrumentProcessor.Process();

            taskDispatcher.Received(1).FinishedTask("Success");
        }
    }
}
