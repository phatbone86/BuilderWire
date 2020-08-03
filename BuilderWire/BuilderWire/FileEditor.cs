using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace BuilderWire
{
    public class FileEditor
    {
        string[] Value;

        //Check if file exists
        public Boolean CheckFile(string strFile)
        {
            if (File.Exists(strFile))
                return true;
            else
                return false;
        }

        //Delete the file
        public Boolean DeleteFile(string strFile)
        {
            File.Delete(strFile);
            return true;
        }

        //Read Words file
        public Article ReadFile(Article article, string strFile)
        {
            using (StreamReader streamReader = File.OpenText(strFile))
            {
                article.arrWords = new ArrayList();
                string strText = System.IO.File.ReadAllText(strFile);
                //store in arraylist all words
                string line = "";
                while ((line = streamReader.ReadLine()) != null)
                {
                    article.arrWords.Add(line);
                }
                streamReader.Close();
            }
            return article;
        }

        //Count Word Occurence in file
        public Article CountWords(Article article)
        {
            article = SplitWords(article);

            string strWord = "";
            string strWord2 = "";

            using (StreamReader streamReader = File.OpenText(article.fileName))
            {
                Regex reg_exp = new Regex("[^a-z0-9]");

                //Populate arrWordCount
                article.arrWordCount = new ArrayList();
                article.arrSentenceCount = new ArrayList();
                foreach (object item in article.arrWords)
                {
                    article.arrWordCount.Add(0);
                    article.arrSentenceCount.Add("");
                }


                for (int i = 0; i < Value.Length; i++)                                       // Loop the splitted string  
                {
                    strWord2 = Value[i].ToLower();
                    strWord2 = reg_exp.Replace(strWord2, "");

                    for (int j = 0; j < article.arrWords.Count; j++)
                    {
                        strWord = article.arrWords[j].ToString();
                        strWord = reg_exp.Replace(strWord, "");
                        if (strWord.Equals(strWord2))
                        {
                            article.arrWordCount[j] = Convert.ToInt32(article.arrWordCount[j]) + 1;
                            if (article.arrSentences[j].ToString() != "")
                                article.arrSentenceCount[j] = article.arrSentenceCount[j] + "," + article.arrSentences[i].ToString();
                            else
                                article.arrSentenceCount[j] = article.arrSentences[i].ToString();
                            break;
                        }
                    }
                }

                streamReader.Close();
                return article;
            }
        }


        //Split the strings in the file
        public Article SplitWords(Article article)
        {
            using (StreamReader streamReader = File.OpenText(article.fileName))
            {
                string text = System.IO.File.ReadAllText(article.fileName);              // User ReadAllText to copy the file's text into a string
                Value = text.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);                                     // Split the string and remove the empty entries

                //Populate arrSentences
                article.arrSentences = new ArrayList();
                foreach (object item in Value)
                    article.arrSentences.Add(0);

                int ctr = 0;
                Boolean bSentence = false;
                char charBefore;
                char charAfter;
                string strPeriod;

                for (int i = 0; i < Value.Length; i++)                                       // Loop the splitted string  
                {
                    //check if word after period is uppercase
                    //check if word has period at the end 
                    //check if word before period is lowercase

                    //if first word
                    if (i == 0)
                    {
                        charAfter = Convert.ToChar(Value[i + 1].Substring(0, 1));
                        strPeriod = Value[i].Substring(Value[i].Length - 1);
                        if (strPeriod == "." && Char.IsUpper(charAfter))
                            bSentence = true;
                    }
                    //if last word
                    else if (i == Value.Length - 1)
                    {
                        charBefore = Convert.ToChar(Value[i - 1].Substring(0, 1));
                        strPeriod = Value[i].Substring(Value[i].Length - 1);
                        if (strPeriod == "." && Char.IsLower(charBefore))
                            bSentence = true;
                    }
                    else
                    {
                        charBefore = Convert.ToChar(Value[i - 1].Substring(0, 1));
                        charAfter = Convert.ToChar(Value[i + 1].Substring(0, 1));
                        strPeriod = Value[i].Substring(Value[i].Length - 1);
                        if (strPeriod == "." && Char.IsUpper(charAfter) && Char.IsLower(charBefore) && Value[i].Substring(0, 2) != "Mr")
                            bSentence = true;
                    }

                    //if a sentence
                    if (bSentence)
                    {
                        ctr++;
                        article.arrSentences[i] = ctr;
                        bSentence = false;
                    }

                }

                //Populate arrSentences
                int sentenceNo = 0;
                for (int j = Value.Length - 1; j >= 0; j--)
                {
                    if (Convert.ToInt32(article.arrSentences[j]) == 0)
                    {
                        if (sentenceNo == 0)
                            article.arrSentences[j] = ctr + 1;
                        else
                            article.arrSentences[j] = sentenceNo;
                    }
                    else
                    {
                        sentenceNo = Convert.ToInt32(article.arrSentences[j]);
                    }
                }

                streamReader.Close();
                return article;
            }
        }

        //Write to output file
        public void WriteOutput(Article article, string strFile)
        {
            using (var streamWriter = new StreamWriter(strFile))
            {
                for (int i = 0; i < article.arrWords.Count; i++)
                    streamWriter.WriteLine(" " + GetLetter(i) + ". " + article.arrWords[i] + " {" + article.arrWordCount[i] + ":" + article.arrSentenceCount[i].ToString().Substring(1, article.arrSentenceCount[i].ToString().Length - 1) + "}");
                streamWriter.Close();
            }
        }

        //Convert number to letters
        public string GetLetter(int index)
        {
            const string letters = "abcdefghijklmnopqrstuvwxyz";
            string value = "";
            char letter;
            int j = index;
            int ctr = 0;

            while (j > letters.Length - 1)
            {
                ctr++;
                j -= letters.Length;
            }

            letter = letters[index % letters.Length];
            for (int k = 0; k <= ctr; k++)
                value += letter;

            return value;
        }
    }
}
