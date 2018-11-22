using System;

namespace CodeKatas.InstrumentProcessorKata
{
    public interface IInstrument
    {
        void Execute(string task);
        event EventHandler<InstrumentProcessEventArgs> Finished;
        event EventHandler<InstrumentProcessEventArgs> Error;
    }
}