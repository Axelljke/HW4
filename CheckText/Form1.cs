using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace CheckText
{
    public partial class Form1 : Form
    {
        string file_name = "dic.txt";
        List<string> words = new List<string>();
        int filelines = 0;//кол-во строк в файле
        public Form1()
        {
            InitializeComponent();
            StreamReader file = new StreamReader(file_name);
            filelines=GetLinesNumbers();
            GetDictionaryList(file);
            file.Close();
        }
        //Метод для вычисления колва строк в файле
        int GetLinesNumbers()
        {
            StreamReader testfile = new StreamReader(file_name);
            int count = 0;
            string line = String.Empty;
            while ((line = testfile.ReadLine()) != null)
            {
                count++;
            }
            testfile.Close();
            return count;
        }
        //Метод чтения строк из файла в List words
        void GetDictionaryList(StreamReader testfile)
        {
            string strbuffer = String.Empty;
            for (int i = 0; i < filelines; i++)
            {
                words.Add(testfile.ReadLine());
            }
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    if( ((words[i])[j]!=' ')&&((words[i])[j] != '\t') )
                    {
                        strbuffer += words[i][j];
                    }

                }
                words[i] = strbuffer;
                strbuffer = String.Empty;
            }

        }
        //Метод считывания слов из сроки ввода
        void GetWordsList()
        {
            int PosStart = 0;
            int PosEnd = 0;
            int textlength = richTextBox1.Text.Length;
            string strbuffer = String.Empty;
            char letter = ' ';
            for (int i = 0; i < textlength; i++)
            {
                letter = richTextBox1.Text[i];
                if ( (((letter >= (char) 1040)&&(letter <= (char) 1103))|| (letter == '-')) && (i != textlength-1))
                {
                    if(strbuffer== String.Empty) { PosStart = i; }
                    strbuffer += letter;
                }
                else
                {
                    PosEnd = i;
                    if (i == textlength - 1) { strbuffer += letter; }
                    if (strbuffer != String.Empty)
                    {
                        if (CheckWord(strbuffer) == true)
                        {
                            ColorSelect(richTextBox1, Color.Red, PosStart, PosEnd);
                        }
                        else
                        {
                            ColorSelect(richTextBox1, Color.Black, PosStart, PosEnd);
                        }
                    }
                    strbuffer = String.Empty;
                }
            }
        }
        //Метод проверки слов со словарем
        bool CheckWord(string word)
        {
            bool error = true;
            foreach (var word1 in words)
            {
                if (word == word1) { error = false; }
            }
            return error;
        }
        //Метод выделение ошибочных слов
        public void ColorSelect(RichTextBox r, Color c, int start, int end)
        {
            r.Select(start, end-start+1);
            r.SelectionColor = c;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetWordsList();
            richTextBox1.Select(richTextBox1.TextLength, 1);
            richTextBox1.SelectionColor = Color.Black;
        }
    }
}
