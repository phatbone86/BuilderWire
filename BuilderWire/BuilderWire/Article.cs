using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BuilderWire
{
    public class Article: Words
    {
        public ArrayList arrSentences;
        public ArrayList arrSentenceCount;

        public ArrayList getSentences()
        {
            return arrSentences;
        }

        public ArrayList getSentenceCount()
        {
            return arrSentenceCount;
        }

    }
}
