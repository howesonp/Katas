using CodeKatas.InstrumentProcessorKata;
using NSubstitute;
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
    }
}
