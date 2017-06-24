namespace Savoa
{
    public interface Logger
    {
        void WriteLine(string message);
    }

    public class NullLogger : Logger
    {
        public void WriteLine(string message)
        {
        }
    }
}
