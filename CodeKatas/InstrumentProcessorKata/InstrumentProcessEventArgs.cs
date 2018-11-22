using System;

namespace CodeKatas.InstrumentProcessorKata
{
    public class InstrumentProcessEventArgs : EventArgs
    {
        public readonly string InstrumentProcessTask;

        public InstrumentProcessEventArgs(string instrumentProcessTask)
        {
            InstrumentProcessTask = instrumentProcessTask;
        }
    }
}
