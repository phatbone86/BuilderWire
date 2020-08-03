using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BuilderWire
{
    public class Words
    {
        public string fileName;
        public ArrayList arrWords;
        public ArrayList arrWordCount;

        public String getFilename()
        {
            return fileName;
        }

        public ArrayList getWords()
        {
            return arrWords;
        }

        public ArrayList getWordCount()
        {
            return arrWordCount;
        } 
    }
}
