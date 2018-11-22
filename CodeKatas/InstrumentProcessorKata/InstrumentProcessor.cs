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

        private void OnFinished(object sender, InstrumentProcessEventArgs e)
        {
            _taskDispatcher.FinishedTask(e.InstrumentProcessTask);
        }

        private void OnError(object sender, InstrumentProcessEventArgs e)
        {
            Console.WriteLine("Error occured");
        }

        public void Process()
        {
            var task = _taskDispatcher.GetTask();
            _instrument.Execute(task);
        }
    }
}
