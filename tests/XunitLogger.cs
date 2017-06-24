using Xunit.Abstractions;

namespace Savoa
{
    class XunitLogger : Logger
    {
        private ITestOutputHelper helper;

        public XunitLogger(ITestOutputHelper helper)
        {
            this.helper = helper;
        }

        public void WriteLine(string message)
        {
            helper.WriteLine(message);
        }
    }
}
