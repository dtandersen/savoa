namespace Savoa
{
    public interface Logger
    {
        void WriteLine(string message);
    }

    class NullLogger : Logger
    {
        public void WriteLine(string message)
        {
        }
    }
}
