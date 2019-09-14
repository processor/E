using System;
using System.Collections.Generic;
using System.IO;

namespace D.Parsing.Tests
{
    public class TestBase
    {
        public static T Parse<T>(string text)
        {
            using var parser = new Parser(text);

            return (T)parser.Next();
        }

        private static readonly string RootDirectory = new DirectoryInfo(AppContext.BaseDirectory).Parent.Parent.Parent.Parent.Parent.FullName;

        public FileInfo GetDocument(string name)
        {
            return new FileInfo(Path.Combine(RootDirectory, "modules", name));
        }

        public IEnumerable<FileInfo> ReadDocuments(string path)
            => new DirectoryInfo(Path.Combine(RootDirectory, "modules", path)).EnumerateFiles();

        public string ReadDocument(string name) => GetDocument(name).OpenText().ReadToEnd();
    }
}
