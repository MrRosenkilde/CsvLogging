using System;
using System.Collections.Generic;
using System.Text;

namespace CsvLogging
{
    public readonly struct CsvConfig
    {
        public readonly char delimiter;
        public readonly CollectionStrategy collectionStrategy;

        public CsvConfig(char delimeter, CollectionStrategy collectionStrategy)
        {
            this.delimiter = delimeter;
            this.collectionStrategy = collectionStrategy;
        }
        public CsvConfig(char delimeter)
        {
            this.delimiter = delimeter;
            this.collectionStrategy = CollectionStrategy.Newline;
        }
    }
}
