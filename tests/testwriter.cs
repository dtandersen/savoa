using Xunit.Abstractions;
using System.IO;

class TestWriter : StringWriter
{
    ITestOutputHelper helper;

    public TestWriter(ITestOutputHelper helper)
    {
        this.helper = helper;
    }

    override public void WriteLine(string line)
    {
        base.WriteLine(line);
        helper.WriteLine(line);
    }
}