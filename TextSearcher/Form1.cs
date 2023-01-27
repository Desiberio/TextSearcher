using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace TextSearcher
{
    public partial class MainForm : Form
    {
        List<string> allFileNames = null;
        double allTime = 0;
        int numberOfCheckedFiles = 0;
        bool isReadyToUpdateScroll = false;
        int numberOfAllMatches = 0;
        TextFileWorker textWorker = null;
        TextNavigator textNavigator = null;

        public MainForm()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false; //debug
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

        private async void Search(List<string> paths)
        {
            await Task.Run(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                foreach (string path in paths)
                {
                    scaningProgressBar.Value++;
                    scaningProgressBar2.Value++;
                    int numberOfMatches = 0;

                    stopwatch = Stopwatch.StartNew();
                    TextFileWorker worker = new TextFileWorker(path, searchBox.Text);
                    if (string.IsNullOrWhiteSpace(worker.mainText)) continue;

                    numberOfMatches = worker.Search();
                    stopwatch.Stop();

                    double elapsedS = CalculateTime(stopwatch);
                    allTime += elapsedS;
                    numberOfCheckedFiles++;

                    allTimeLabel.Text = allTime.ToString() + " сек.";
                    numberOfCheckedFilesLabel.Text = numberOfCheckedFiles.ToString();

                    dataTable.Rows.Add(path, numberOfMatches, elapsedS);
                    if (numberOfMatches > 0) dataTable.Sort(dataTable.Columns[1], System.ComponentModel.ListSortDirection.Descending);
                }
            });
            allTime = Math.Round(allTime, 3);

            isReadyToUpdateScroll = true;
            dataTable.Sort(dataTable.Columns[1], System.ComponentModel.ListSortDirection.Descending);
            if (!scaningProgressBar2.Visible) SetupRichTextBox(dataTable.Rows[0].Cells[0].Value.ToString());
            HideProgressBar();
        }

        private void HideProgressBar()
        {
            if (scaningProgressBar2.Visible) scaningProgressBar2.Visible = false;
            scaningProgressBar.Visible = false;
            scanLabel.Visible = false;
        }

        private static double CalculateTime(Stopwatch stopwatch)
        {
            double elapsedS = stopwatch.Elapsed.TotalSeconds;
            elapsedS = Math.Round(elapsedS, 3);
            return elapsedS;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            if (searchBox.Text.Length < 3)
            {
                MessageBox.Show("Слово должно содержать по крайней мере 3 символа!");
                return;
            }
            if (allFileNames == null || allFileNames.Count == 0)
            {
                MessageBox.Show("Сначала выберите файл или папку!");
                return;
            }
            SetupUI();
            //dataTable.SuspendLayout();
            Search(allFileNames);
            CheckIfReadyToUpdateScrollBar();
        }

        private void SetupUI()
        {
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
        }

        private async void CheckIfReadyToUpdateScrollBar()
        {
            while (!isReadyToUpdateScroll) await Task.Delay(50);
            dataTable.ScrollBars = ScrollBars.Both;
        }

        private void NextMatch_Click(object sender, EventArgs e)
        {
            textNavigator.MoveNext();
            UpdateRichTextBox();
            matchesLabel.Text = $"{textNavigator.CurrentWordPosition + 1} из {numberOfAllMatches}";
        }

        private void PrevMatch_Click(object sender, EventArgs e)
        {
            textNavigator.MoveBack();
            UpdateRichTextBox();
            matchesLabel.Text = $"{textNavigator.CurrentWordPosition + 1} из {numberOfAllMatches}";
        }

        private void UpdateRichTextBox()
        {
            if(numberOfAllMatches == 0)
            {
                textNavigator.CurrentWordPosition = -1;
                MessageBox.Show("В данном файле вхождений не обнаружено!", "Text Searcher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int wordIndex = textNavigator.CurrentWordIndex;
            int wordLength = textNavigator.CurrentWordLength;

            fileTextBox.Select(wordIndex, wordLength);
            fileTextBox.SelectionBackColor = Color.Orange;

            if (wordIndex > 10) wordIndex -= 10; //sub firstIndex to position highlighted word more or less in the center
            fileTextBox.Select(wordIndex, wordLength);
            fileTextBox.ScrollToCaret();
            fileTextBox.DeselectAll();

            fileTextBox.Select(textNavigator.PreviousWordIndex, textNavigator.PreviousWordLength);
            fileTextBox.SelectionBackColor = Color.Yellow;
            fileTextBox.DeselectAll();
        }

        private void DataTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isReadyToUpdateScroll) { scaningProgressBar2.Visible = true; scaningProgressBar.Visible = false; scanLabel.Visible = false; }
            string path = dataTable.Rows[e.RowIndex].Cells[0].Value.ToString();
            SetupRichTextBox(path);
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) findButton.PerformClick();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            //validation
            if(Regex.IsMatch(searchBox.Text, @"[^а-яА-Я-\*\?a-zA-Z\-0-9\s\.]"))
            {
                searchBox.Text = searchBox.Text.Remove(searchBox.Text.Length - 1);
                searchBox.SelectionStart = searchBox.Text.Length;
            }
        }

        private void SetupRichTextBox(string path)
        {
            textWorker = new TextFileWorker(path, searchBox.Text);
            fileTextBox.Text = textWorker.mainText;
            //fileTextBox.Text = textWorker.mainText;
            List<int> matchIndexes = textWorker.GetAllMatchIndexes();
            numberOfAllMatches = matchIndexes.Count;
            if (numberOfAllMatches == 0) return;

            textNavigator = new TextNavigator(textWorker);
            foreach (int index in matchIndexes)
            {
                fileTextBox.Select(textNavigator.CurrentWordIndex, textNavigator.CurrentWordLength);
                fileTextBox.SelectionBackColor = Color.Yellow;
                fileTextBox.DeselectAll();
                textNavigator.MoveNext();
            }
            textNavigator.ResetPosition();
            
            matchesLabel.Text = numberOfAllMatches == 0 ? $"{textNavigator.CurrentWordPosition} из {numberOfAllMatches}" : $"{textNavigator.CurrentWordPosition + 1} из {numberOfAllMatches}";

            UpdateRichTextBox();
        }
    }
}
