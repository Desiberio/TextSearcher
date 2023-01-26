using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace TextSearcher
{
    public partial class Form1 : Form
    {
        List<string> allFileNames = null;
        List<Thread> threads = new List<Thread>();
        double allTime = 0;
        int numberOfCheckedFiles = 0;
        bool isReadyToUpdateScroll = false;
        int currentMatch = 0, numberOfAllMatches = 0;
        int numberOfWords = 0;
        TextFileWorker textWorker = null;
        int[] matchIndexes;
        char[] endCharacters = {' ', '.', ',', '?', '!', '"', ':', '(', ')', ';', '\'', '\n', '\r', '\a', '\b', '\t', '\v', '\f', '»', '«', ' ' };

        public Form1()
        {
            InitializeComponent();
        }

        private void chooseFilesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = GetConfiguredFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                allFileNames = new List<string>(fileDialog.FileNames);
            }

        }

        private OpenFileDialog GetConfiguredFileDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Все файлы (*.*)|*.*|" +
                "Файлы Microsoft Word (*.doc, *.docx)|*.doc?|" +
                "Текстовые файлы (*.txt, *.rtf, *.html)|*.txt;*.rtf;*.html";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            fileDialog.Multiselect = true;
            fileDialog.Title = "Выберете файл(ы)";

            return fileDialog;
        }

        private void ChooseFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog folderDialog = new CommonOpenFileDialog();
            folderDialog.IsFolderPicker = true;
            folderDialog.Multiselect = false;

            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = folderDialog.FileName;
                allFileNames = SelectFilesFromFolders(path);
            }
        }

        private List<string> SelectFilesFromFolders(string path)
        {
            List<string> allFiles = new List<string>();
            allFiles.AddRange(Directory.GetFiles(path));

            string[] directories = Directory.GetDirectories(path);
            if (directories.Length == 0) return allFiles;
            foreach (string directory in directories)
            {
                allFiles.AddRange(SelectFilesFromFolders(directory));
            }

            return allFiles;
        }

        private void Search(List<string> paths)
        {
            var watch = new System.Diagnostics.Stopwatch();
            foreach (string path in paths)
            {
                try
                {
                    scaningProgressBar.Value++;
                    scaningProgressBar2.Value++;
                }
                catch { }
                
                watch.Start();
                long elapsedMs = 0;
                double elapsedS = 0;
                int num = 0;
                TextFileWorker worker = new TextFileWorker(path, searchBox.Text);
                if (worker.mainText == null) continue;

                num = worker.Search();
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                elapsedS = Convert.ToDouble(elapsedMs) / (double)1000;
                elapsedS = Math.Round(elapsedS, 3);
                watch.Reset();

                allTime += elapsedS;
                allTime = Math.Round(allTime, 3);
                numberOfCheckedFiles++;

                allTimeLabel.Text = allTime.ToString() + " сек.";
                numberOfCheckedFilesLabel.Text = numberOfCheckedFiles.ToString();

                dataTable.Rows.Add(path, num, elapsedS);
                if(num > 0) dataTable.Sort(dataTable.Columns[1], System.ComponentModel.ListSortDirection.Descending);
            }
            try
            {
                isReadyToUpdateScroll = true;
                dataTable.Sort(dataTable.Columns[1], System.ComponentModel.ListSortDirection.Descending);
                if(!scaningProgressBar2.Visible) SetupRichTextBox(dataTable.Rows[0].Cells[0].Value.ToString());
            }
            catch { }
            if (scaningProgressBar2.Visible == true) scaningProgressBar2.Visible = false;
            scaningProgressBar.Visible = false;
            scanLabel.Visible = false;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            if(searchBox.Text.Length < 3)
            {
                MessageBox.Show("Слово должно содержать по крайней мере 3 символа!");
                return;
            }
            if(allFileNames == null || allFileNames.Count == 0)
            {
                MessageBox.Show("Сначала выберите файл или папку!");
                return;
            }

            foreach (Thread thread in threads)
            {
                thread.Abort();
            }

            isReadyToUpdateScroll = false;
            fileTextBox.Text = string.Empty;
            scaningProgressBar.Visible = true;
            scaningProgressBar2.Visible = false;
            scanLabel.Visible = true;
            scaningProgressBar.Maximum = allFileNames.Count;
            scaningProgressBar2.Maximum = allFileNames.Count;
            scaningProgressBar.Value = 0;
            scaningProgressBar2.Value = 0;
            numberOfCheckedFiles = 0;
            allTime = 0;
            dataTable.Rows.Clear();
            //CheckForIllegalCrossThreadCalls = false; //Debug
            Thread searchThread = new Thread(() =>
            {
                dataTable.SuspendLayout();
                Search(allFileNames);
            })
            { IsBackground = true };
            threads.Add(searchThread);
            searchThread.Start();
            CheckIfReadyToUpdateScrollBar();
        }

        private async void CheckIfReadyToUpdateScrollBar()
        {
            while (!isReadyToUpdateScroll) await Task.Delay(50);
            dataTable.ScrollBars = ScrollBars.Both;
        }

        private void NextMatch_Click(object sender, EventArgs e)
        {
            currentMatch++;
            UpdateRichTextBox(true);
        }

        private void PrevMatch_Click(object sender, EventArgs e)
        {
            currentMatch--;
            UpdateRichTextBox(false);
        }

        private void UpdateRichTextBox(bool wasNext)
        {
            bool isEnd = false;
            int tempNumberOfWords = numberOfWords - 1;

            //creating some kind of loop
            if (currentMatch >= numberOfAllMatches) { currentMatch = 0; isEnd = true; }
            else if (currentMatch < 0) { currentMatch = numberOfAllMatches - 1; isEnd = true; }

            int firstIndexOfWord = matchIndexes[currentMatch];
            if (textWorker.mainText[firstIndexOfWord] == ' ') firstIndexOfWord++;
            string tempText = textWorker.mainText.Substring(firstIndexOfWord);
            int lastIndex = tempText.IndexOfAny(endCharacters);
            if (lastIndex == 0) lastIndex = tempText.IndexOfAny(endCharacters, lastIndex + 1);
            while (tempNumberOfWords != 0)
            {
                lastIndex = tempText.IndexOfAny(endCharacters, lastIndex + 1);
                tempNumberOfWords--;
            }

            fileTextBox.Select(firstIndexOfWord, lastIndex);
            fileTextBox.SelectionBackColor = Color.Orange;

            if (firstIndexOfWord > 10) firstIndexOfWord -= 10;
            fileTextBox.Select(firstIndexOfWord, lastIndex);
            fileTextBox.ScrollToCaret();
            fileTextBox.DeselectAll();

            if (isEnd == false)
            {
                if (wasNext) firstIndexOfWord = matchIndexes[currentMatch - 1];
                else firstIndexOfWord = matchIndexes[currentMatch + 1];
            }
            else
            {
                if (wasNext) firstIndexOfWord = matchIndexes[numberOfAllMatches - 1];
                else firstIndexOfWord = matchIndexes[0];
            }

            if (textWorker.mainText[firstIndexOfWord] == ' ') firstIndexOfWord++;
            tempNumberOfWords = numberOfWords - 1;
            tempText = textWorker.mainText.Substring(firstIndexOfWord);

            lastIndex = tempText.IndexOfAny(endCharacters);
            if (lastIndex == 0) lastIndex = tempText.IndexOfAny(endCharacters, lastIndex + 1);
            while (tempNumberOfWords != 0)
            {
                lastIndex = tempText.IndexOfAny(endCharacters, lastIndex + 1);
                tempNumberOfWords--;
            }

            fileTextBox.Select(firstIndexOfWord, lastIndex);
            fileTextBox.SelectionBackColor = Color.Yellow;
            fileTextBox.DeselectAll();

            matchesLabel.Text = $"{currentMatch + 1} из {numberOfAllMatches}";
        }

        private void DataTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isReadyToUpdateScroll) { scaningProgressBar2.Visible = true; scaningProgressBar.Visible = false; scanLabel.Visible = false; }
            try
            {
                string path = dataTable.Rows[e.RowIndex].Cells[0].Value.ToString();
                SetupRichTextBox(path);
            }
            catch { }
            
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) findButton.PerformClick();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if(Regex.IsMatch(searchBox.Text, @"[^а-яА-Я-\*\?a-zA-Z\-0-9\s\.]"))
            {
                searchBox.Text = searchBox.Text.Remove(searchBox.Text.Length - 1);
                searchBox.SelectionStart = searchBox.Text.Length;
            }
        }

        private void SetupRichTextBox(string path)
        {
            numberOfWords = Math.Max(searchBox.Text.Split(' ').Length, searchBox.Text.Split('.').Length);
            textWorker = new TextFileWorker(path, searchBox.Text);
            fileTextBox.Text = textWorker.mainText;
            matchIndexes = textWorker.GetAllMatchIndexes();
            numberOfAllMatches = matchIndexes.Length;
            for(int i = 1; i < numberOfAllMatches; i++)
            {
                int tempNumberOfWords = numberOfWords - 1;
                int firstIndexOfWord = matchIndexes[i];
                if (textWorker.mainText[firstIndexOfWord] == ' ') firstIndexOfWord++;

                string tempText = textWorker.mainText.Substring(firstIndexOfWord);

                int lastIndexOfWord = tempText.IndexOfAny(endCharacters);
                if (lastIndexOfWord == 0) lastIndexOfWord = tempText.IndexOfAny(endCharacters, lastIndexOfWord + 1);
                while (tempNumberOfWords != 0)
                {
                    lastIndexOfWord = tempText.IndexOfAny(endCharacters, lastIndexOfWord + 1);
                    tempNumberOfWords--;
                }
                fileTextBox.Select(firstIndexOfWord, lastIndexOfWord);
                fileTextBox.SelectionBackColor = Color.Yellow;
                fileTextBox.DeselectAll();
            }
            currentMatch = 0;
            matchesLabel.Text = $"{currentMatch + 1} из {numberOfAllMatches}";
            if (numberOfAllMatches == 0)
            {
                matchesLabel.Text = $"{currentMatch} из {numberOfAllMatches}";
                return;
            }
                
            UpdateRichTextBox(true);
        }
    }
}
