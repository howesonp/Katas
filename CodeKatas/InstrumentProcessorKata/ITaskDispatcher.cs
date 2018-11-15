namespace CodeKatas.InstrumentProcessorKata
{
    public interface ITaskDispatcher
    {
        string GetTask();
        void FinishedTask(string task);
    }
}