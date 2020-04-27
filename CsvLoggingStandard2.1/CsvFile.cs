using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CsvLogging
{
    public class CsvFile<T> : IAsyncDisposable
    {
        private StreamWriter FileWriter { get; }
        private CsvConfig config;
        public CsvFile(string path,
            bool writeHeader = true,
            int buffer = 3600,
            bool append = true
        ) : this(path, Encoding.UTF8, writeHeader, buffer, append) { }


        public CsvFile(string path,
            Encoding encoding,
            bool writeHeader = true,
            int buffer = 3600,
            bool append = true,
            char delimeter = ';'
        )
        {
            FileWriter = new StreamWriter(
               path,
               append,
               encoding,
               buffer
           );
            config = new CsvConfig(delimeter);
            if (writeHeader)
                FileWriter.WriteLine(
                    CsvFunctions.ToHeader(
                        typeof(T),
                        config
                    )
                );
        }
        public Task WriteLineAsync(T lineElement)
        {
            return FileWriter.WriteLineAsync(
                CsvFunctions.ToLine(
                    lineElement,
                    config
                )
            );
        }

        public async Task WriteLinesAsync(IEnumerable<T> elements) 
        {
            var enumerator = elements.GetEnumerator();
            while (enumerator.MoveNext())
                await FileWriter.WriteLineAsync(
                    CsvFunctions.ToLine(
                        enumerator.Current,
                        config
                    )
                );
        }

        public async ValueTask DisposeAsync()
        {
            await FileWriter.DisposeAsync();
            FileWriter.Close();
        }
    }
}
