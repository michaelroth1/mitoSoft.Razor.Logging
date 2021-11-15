using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mitoSoft.Razor.Logging;
using System;

namespace UnitTests
{
    [TestClass]
    public class LogLineTests
    {
        [TestMethod]
        public void Test1()
        {
            var logLine = new LogLine(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc), LogLevel.Warning, "TestMessage", "Category1");

            var s = logLine.ToString("[{date:yyyyMMdd HH:mm:ssZ}] [{level}] {message}");

            Assert.AreEqual("[19820307 06:00:00Z] [warn] TestMessage", s);
        }

        [TestMethod]
        public void Test2()
        {
            var logLine = new LogLine(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc), LogLevel.Warning, "TestMessage", "Category1");

            var s = logLine.ToString("[{Date:yyyyMMdd HH:mm:ssZ}] [{level}] {message}");

            Assert.AreEqual("[19820307 06:00:00Z] [warn] TestMessage", s);
        }

        [TestMethod]
        public void Test3()
        {
            var logLine = new LogLine(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc), LogLevel.Warning, "TestMessage", "Category1");

            var s = logLine.ToString("[{   Date :   yyyyMMdd HH:mm:ss}] [{level}] {message}");

            Assert.AreEqual("[19820307 06:00:00] [warn] TestMessage", s);
        }

        [TestMethod]
        public void Test4()
        {
            var logLine = new LogLine(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc), LogLevel.Warning, "TestMessage", "Category1");

            var s = logLine.ToString("[{   Date :   yyyyMMdd} {date:HH:mm:ss fffZ}] [{level}] {message}");

            Assert.AreEqual("[19820307 06:00:00 000Z] [warn] TestMessage", s);
        }

        [TestMethod]
        public void Test5()
        {
            var logLine = new LogLine(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc), LogLevel.Warning, "TestMessage", "Category1");

            var s = logLine.ToString("[{date:dd.MM.yyyy HH:mm:ss}] [{level}] {message} {MESSAge}");

            Assert.AreEqual("[07.03.1982 06:00:00] [warn] TestMessage TestMessage", s);
        }

        [TestMethod]
        public void Test6()
        {
            var logLine = new LogLine(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc), LogLevel.Warning, "TestMessage", "Category1");

            var s = logLine.ToString("[{ date  }] [{level}] {message}");

            Assert.AreEqual("[1982-03-07 06:00:00 000] [warn] TestMessage", s);
        }

        [TestMethod]
        public void Test7()
        {
            var logLine = new LogLine(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc), LogLevel.Warning, "TestMessage", "Category1");

            var s = logLine.ToString("[{ date  }] [{loglevel}] {message}");

            Assert.AreEqual("[1982-03-07 06:00:00 000] [warn] TestMessage", s);

            s = logLine.ToString("[{ date  }] [{LogLevel}] {message}");

            Assert.AreEqual("[1982-03-07 06:00:00 000] [warn] TestMessage", s);
        }
    }
}