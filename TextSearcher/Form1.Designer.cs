
namespace TextSearcher
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.findButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chooseFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.chooseFilesButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numberOfCheckedFilesLabel = new System.Windows.Forms.Label();
            this.allTimeLabel = new System.Windows.Forms.Label();
            this.dataTable = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scanLabel = new System.Windows.Forms.Label();
            this.scaningProgressBar = new System.Windows.Forms.ProgressBar();
            this.matchesLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nextMatch = new System.Windows.Forms.Button();
            this.prevMatch = new System.Windows.Forms.Button();
            this.fileTextBox = new System.Windows.Forms.RichTextBox();
            this.scaningProgressBar2 = new System.Windows.Forms.ProgressBar();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfMatches = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElapsedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(6, 90);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 0;
            this.findButton.Text = "Найти";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chooseFolder);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.searchBox);
            this.groupBox1.Controls.Add(this.findButton);
            this.groupBox1.Controls.Add(this.chooseFilesButton);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 122);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исходные данные";
            // 
            // chooseFolder
            // 
            this.chooseFolder.Location = new System.Drawing.Point(137, 21);
            this.chooseFolder.Name = "chooseFolder";
            this.chooseFolder.Size = new System.Drawing.Size(109, 23);
            this.chooseFolder.TabIndex = 6;
            this.chooseFolder.Text = "Выбрать папку";
            this.chooseFolder.UseVisualStyleBackColor = true;
            this.chooseFolder.Click += new System.EventHandler(this.ChooseFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Слово или фраза:";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(6, 62);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(240, 22);
            this.searchBox.TabIndex = 1;
            this.searchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchBox_KeyDown);
            // 
            // chooseFilesButton
            // 
            this.chooseFilesButton.Location = new System.Drawing.Point(6, 21);
            this.chooseFilesButton.Name = "chooseFilesButton";
            this.chooseFilesButton.Size = new System.Drawing.Size(109, 23);
            this.chooseFilesButton.TabIndex = 3;
            this.chooseFilesButton.Text = "Выбрать файл(ы)";
            this.chooseFilesButton.UseVisualStyleBackColor = true;
            this.chooseFilesButton.Click += new System.EventHandler(this.chooseFilesButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(15, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Проверено файлов:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(15, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Общее время поиска:";
            // 
            // numberOfCheckedFilesLabel
            // 
            this.numberOfCheckedFilesLabel.AutoSize = true;
            this.numberOfCheckedFilesLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numberOfCheckedFilesLabel.Location = new System.Drawing.Point(160, 146);
            this.numberOfCheckedFilesLabel.Name = "numberOfCheckedFilesLabel";
            this.numberOfCheckedFilesLabel.Size = new System.Drawing.Size(15, 17);
            this.numberOfCheckedFilesLabel.TabIndex = 6;
            this.numberOfCheckedFilesLabel.Text = "0";
            // 
            // allTimeLabel
            // 
            this.allTimeLabel.AutoSize = true;
            this.allTimeLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.allTimeLabel.Location = new System.Drawing.Point(160, 172);
            this.allTimeLabel.Name = "allTimeLabel";
            this.allTimeLabel.Size = new System.Drawing.Size(15, 17);
            this.allTimeLabel.TabIndex = 7;
            this.allTimeLabel.Text = "0";
            // 
            // dataTable
            // 
            this.dataTable.AllowUserToAddRows = false;
            this.dataTable.AllowUserToDeleteRows = false;
            this.dataTable.AllowUserToResizeRows = false;
            this.dataTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.NumberOfMatches,
            this.ElapsedTime});
            this.dataTable.Location = new System.Drawing.Point(279, 13);
            this.dataTable.MultiSelect = false;
            this.dataTable.Name = "dataTable";
            this.dataTable.ReadOnly = true;
            this.dataTable.RowHeadersVisible = false;
            this.dataTable.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataTable.Size = new System.Drawing.Size(517, 189);
            this.dataTable.TabIndex = 8;
            this.dataTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataTable_CellClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.scaningProgressBar2);
            this.panel1.Controls.Add(this.scanLabel);
            this.panel1.Controls.Add(this.scaningProgressBar);
            this.panel1.Controls.Add(this.matchesLabel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.nextMatch);
            this.panel1.Controls.Add(this.prevMatch);
            this.panel1.Controls.Add(this.fileTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 206);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 211);
            this.panel1.TabIndex = 9;
            // 
            // scanLabel
            // 
            this.scanLabel.AutoSize = true;
            this.scanLabel.BackColor = System.Drawing.SystemColors.Control;
            this.scanLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scanLabel.Location = new System.Drawing.Point(325, 99);
            this.scanLabel.Name = "scanLabel";
            this.scanLabel.Size = new System.Drawing.Size(151, 25);
            this.scanLabel.TabIndex = 12;
            this.scanLabel.Text = "Сканирование...";
            this.scanLabel.Visible = false;
            // 
            // scaningProgressBar
            // 
            this.scaningProgressBar.Location = new System.Drawing.Point(201, 131);
            this.scaningProgressBar.Name = "scaningProgressBar";
            this.scaningProgressBar.Size = new System.Drawing.Size(398, 23);
            this.scaningProgressBar.TabIndex = 11;
            this.scaningProgressBar.Visible = false;
            // 
            // matchesLabel
            // 
            this.matchesLabel.AutoSize = true;
            this.matchesLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.matchesLabel.Location = new System.Drawing.Point(174, 8);
            this.matchesLabel.Name = "matchesLabel";
            this.matchesLabel.Size = new System.Drawing.Size(43, 17);
            this.matchesLabel.TabIndex = 10;
            this.matchesLabel.Text = "0 из 0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(98, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Упоминание";
            // 
            // nextMatch
            // 
            this.nextMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nextMatch.Location = new System.Drawing.Point(50, 6);
            this.nextMatch.Name = "nextMatch";
            this.nextMatch.Size = new System.Drawing.Size(43, 23);
            this.nextMatch.TabIndex = 2;
            this.nextMatch.Text = "→";
            this.nextMatch.UseVisualStyleBackColor = true;
            this.nextMatch.Click += new System.EventHandler(this.NextMatch_Click);
            // 
            // prevMatch
            // 
            this.prevMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prevMatch.Location = new System.Drawing.Point(3, 6);
            this.prevMatch.Name = "prevMatch";
            this.prevMatch.Size = new System.Drawing.Size(43, 23);
            this.prevMatch.TabIndex = 1;
            this.prevMatch.Text = "←";
            this.prevMatch.UseVisualStyleBackColor = true;
            this.prevMatch.Click += new System.EventHandler(this.PrevMatch_Click);
            // 
            // fileTextBox
            // 
            this.fileTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fileTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileTextBox.Location = new System.Drawing.Point(0, 32);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.ReadOnly = true;
            this.fileTextBox.Size = new System.Drawing.Size(800, 179);
            this.fileTextBox.TabIndex = 0;
            this.fileTextBox.Text = "";
            // 
            // scaningProgressBar2
            // 
            this.scaningProgressBar2.Location = new System.Drawing.Point(341, 3);
            this.scaningProgressBar2.Name = "scaningProgressBar2";
            this.scaningProgressBar2.Size = new System.Drawing.Size(398, 23);
            this.scaningProgressBar2.TabIndex = 13;
            this.scaningProgressBar2.Visible = false;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileName.FillWeight = 8F;
            this.FileName.HeaderText = "Файл";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // NumberOfMatches
            // 
            this.NumberOfMatches.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NumberOfMatches.FillWeight = 2F;
            this.NumberOfMatches.HeaderText = "Количество упоминаний";
            this.NumberOfMatches.MinimumWidth = 30;
            this.NumberOfMatches.Name = "NumberOfMatches";
            this.NumberOfMatches.ReadOnly = true;
            // 
            // ElapsedTime
            // 
            this.ElapsedTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ElapsedTime.FillWeight = 2F;
            this.ElapsedTime.HeaderText = "Время проверки";
            this.ElapsedTime.MinimumWidth = 30;
            this.ElapsedTime.Name = "ElapsedTime";
            this.ElapsedTime.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 417);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataTable);
            this.Controls.Add(this.allTimeLabel);
            this.Controls.Add(this.numberOfCheckedFilesLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "TextSeacher";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button chooseFilesButton;
        private System.Windows.Forms.Button chooseFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label numberOfCheckedFilesLabel;
        private System.Windows.Forms.Label allTimeLabel;
        private System.Windows.Forms.DataGridView dataTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox fileTextBox;
        private System.Windows.Forms.Button prevMatch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button nextMatch;
        private System.Windows.Forms.Label matchesLabel;
        private System.Windows.Forms.Label scanLabel;
        private System.Windows.Forms.ProgressBar scaningProgressBar;
        private System.Windows.Forms.ProgressBar scaningProgressBar2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfMatches;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElapsedTime;
    }
}

