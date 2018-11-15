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
            _instrument.Error += OnError;
            _instrument.Finished += OnFinished;
        }

        private void OnFinished(object sender, EventArgs e)
        {
            _taskDispatcher.FinishedTask(e.ToString());
        }

        private void OnError(object sender, EventArgs e)
        {
            Console.WriteLine($"Error with task {e}");
        }

        public void Process()
        {
            _taskDispatcher.GetTask();
        }
    }
}
