using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Forms;

namespace TextSearcher
{
    class TextFileWorker
    {
        private FileInfo _fileInfo;
        private string searchText = null;
        private string pattern = "";
        private string[] russianPrefixes = { "вы", "до", "об", "от", "по", "под", "пред", "на", "над", "не", "пере", "при", "со", "пре", "раз", "рас", "без"};
        public string mainText { get; private set; } = "";

        public TextFileWorker(string path, string match)
        {
            searchText = match;

            if (File.Exists(path))
            {
                _fileInfo = new FileInfo(path);
            }
            else
            {
                throw new Exception("File not found.");
            }

            mainText = GetText();
            pattern = GetPattern();
        }

        private string GetText()
        {
            string fileText = null;
            string path = _fileInfo.FullName;
            string extension = System.IO.Path.GetExtension(path);
            switch (extension)
            {
                case ".docx":
                case ".doc":
                    try
                    {
                        var wordFile = new Aspose.Words.Document(path);
                        fileText = wordFile.Range.Text;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Unknown file format: Unknown")) break;
                        MessageBox.Show(_fileInfo.Name + " " + ex.Message);
                    }
                    break;
                case ".pdf":
                    StringBuilder text = null;
                    using (PdfReader reader = new PdfReader(path))
                    {
                        text = new StringBuilder();

                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                        }
                    }
                    fileText = text.ToString();
                    break;
                case ".rtf":
                case ".html":
                case ".txt":
                case ".css":
                case ".log":
                case ".js":
                    fileText = File.ReadAllText(path);
                    break;
                default:
                    fileText = null;
                    break;
            }
            return fileText;
        }

        public int[] GetAllMatchIndexes()
        {
            List<int> indexes = new List<int>();

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
            foreach (Match match in regex.Matches(mainText))
            {
                indexes.Add(match.Index);
            }

            return indexes.ToArray();
        }

        private string GetPattern()
        {
            List<string> words = new List<string>(searchText.Split(' '));
            Array.Sort(russianPrefixes, (x, y) => y.Length.CompareTo(x.Length));

            if (Regex.IsMatch(searchText, @"[\*\?]"))
            {
                string temp = searchText;
                temp = Regex.Replace(temp, @"\*", @"[А-Яа-я\w]*");
                temp = Regex.Replace(temp, @"\.", @"\.");
                temp = @"(?<=\s|^)" + Regex.Replace(temp, @"\?", @"\w") + @"(?=\s|$)";
                return temp;
            }

            for(int i = 0; i < words.Count; i++)
            {
                string word = words[i];
                string prefix = Array.Find(russianPrefixes, x => word.StartsWith(x, true, null));
                if (prefix != null){
                    word = word.Replace(prefix, "");
                }
                if(word.Length > 4) words[i] = @"[А-Яа-я\w]*" + word.Remove(4) + @"[А-Яа-я\w]*";
                else words[i] = @"[А-Яа-я\w]*" + word + @"[А-Яа-я\w]*";
            }

            return String.Join(" ", words);
        }
        public int Search()
        {
            int matchesNumber = 0;
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
            MatchCollection matches = regex.Matches(mainText);
            matchesNumber += matches.Count;
            return matchesNumber;
        }
    }
}
