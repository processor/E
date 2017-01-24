using System.Collections.Generic;
using System.IO;

namespace D.Parsing.Tests
{
    public class TestBase
    {
        public T Parse<T>(string text)
        {
            using (var parser = new Parser(text))
            {
                return (T)parser.Next();
            }
        }


       
        private static readonly string RootDirectory = Directory.GetCurrentDirectory();

        public FileInfo GetDocument(string name)
            => new FileInfo(RootDirectory + "..\\..\\..\\modules\\" + name);


        public IEnumerable<FileInfo> ReadDocuments(string path)
        {
            return new DirectoryInfo(RootDirectory + "..\\..\\..\\modules\\" + path).EnumerateFiles();
        }

        public string ReadDocument(string name)
             => GetDocument(name).OpenText().ReadToEnd();

    }
}
