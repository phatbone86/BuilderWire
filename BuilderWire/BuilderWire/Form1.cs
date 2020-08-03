using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BuilderWire
{
    public partial class Form1 : Form
    {
        string strFolder = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
        string strMessage;
        string strOutputFile;

        Article article = new Article();
        Words words = new Words();
        FileEditor fileEditor = new FileEditor();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            strMessage = ProcessFile();
            MessageBox.Show(strMessage);
        }

        private string ProcessFile()
        {
            words.fileName = strFolder + @"\Input\Words.txt";
            article.fileName = strFolder + @"\Input\Article.txt";
            strOutputFile = strFolder + @"\Output\Output.txt";

            if (fileEditor.CheckFile(words.fileName))
                article = fileEditor.ReadFile(article, words.fileName);
            else
                return "Words.txt is not in the Input folder.";

            if (fileEditor.CheckFile(article.fileName))
                article = fileEditor.CountWords(article);
            else
                return "Article.txt is not in the Input folder.";

            if (fileEditor.CheckFile(strOutputFile))
            {
                if (fileEditor.DeleteFile(strOutputFile))
                    fileEditor.WriteOutput(article, strOutputFile);
            }
            else
                fileEditor.WriteOutput(article, strOutputFile);

            return "Output file has been generated.";
        }
    }
}
