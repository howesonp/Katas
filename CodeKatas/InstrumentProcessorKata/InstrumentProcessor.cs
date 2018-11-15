using System;

namespace CodeKatas.InstrumentProcessorKata
{
    public class InstrumentProcessor : IInstrumentProcessor
    {
        private readonly ITaskDispatcher _taskDispatcher;
        private readonly IInstrument _instrument;

        public InstrumentProcessor(ITaskDispatcher taskDispatcher, IInstrument instrument)
        {
            _taskDispatcher = taskDispatcher;
            _instrument = instrument;
        }

        public void Process()
        {
            _taskDispatcher.GetTask();
        }
    }
}
