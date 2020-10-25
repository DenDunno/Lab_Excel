namespace Excel
{
    partial class Form1
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
            this.Excel = new System.Windows.Forms.DataGridView();
            this.InputTexbox = new System.Windows.Forms.TextBox();
            this.EvaluateButton = new System.Windows.Forms.Button();
            this.RowLable = new System.Windows.Forms.Label();
            this.AddRowButton = new System.Windows.Forms.Button();
            this.RemoveRowButton = new System.Windows.Forms.Button();
            this.ColumnLable = new System.Windows.Forms.Label();
            this.AddColumnButton = new System.Windows.Forms.Button();
            this.RemoveColumnButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Excel)).BeginInit();
            this.SuspendLayout();
            // 
            // Excel
            // 
            this.Excel.AllowUserToAddRows = false;
            this.Excel.AllowUserToDeleteRows = false;
            this.Excel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Excel.Location = new System.Drawing.Point(12, 61);
            this.Excel.Name = "Excel";
            this.Excel.ReadOnly = true;
            this.Excel.RowHeadersWidth = 51;
            this.Excel.RowTemplate.Height = 24;
            this.Excel.Size = new System.Drawing.Size(989, 444);
            this.Excel.TabIndex = 0;
            this.Excel.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Excel_CellClick);
            // 
            // InputTexbox
            // 
            this.InputTexbox.Location = new System.Drawing.Point(12, 26);
            this.InputTexbox.Name = "InputTexbox";
            this.InputTexbox.Size = new System.Drawing.Size(251, 22);
            this.InputTexbox.TabIndex = 1;
            // 
            // EvaluateButton
            // 
            this.EvaluateButton.Location = new System.Drawing.Point(293, 25);
            this.EvaluateButton.Name = "EvaluateButton";
            this.EvaluateButton.Size = new System.Drawing.Size(101, 28);
            this.EvaluateButton.TabIndex = 2;
            this.EvaluateButton.Text = "Enter";
            this.EvaluateButton.UseVisualStyleBackColor = true;
            this.EvaluateButton.Click += new System.EventHandler(this.EvaluateButton_Click);
            // 
            // RowLable
            // 
            this.RowLable.AutoSize = true;
            this.RowLable.Location = new System.Drawing.Point(453, 30);
            this.RowLable.Name = "RowLable";
            this.RowLable.Size = new System.Drawing.Size(35, 17);
            this.RowLable.TabIndex = 3;
            this.RowLable.Text = "Row";
            // 
            // AddRowButton
            // 
            this.AddRowButton.Location = new System.Drawing.Point(495, 30);
            this.AddRowButton.Name = "AddRowButton";
            this.AddRowButton.Size = new System.Drawing.Size(42, 23);
            this.AddRowButton.TabIndex = 4;
            this.AddRowButton.Text = "+";
            this.AddRowButton.UseVisualStyleBackColor = true;
            this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            // 
            // RemoveRowButton
            // 
            this.RemoveRowButton.Location = new System.Drawing.Point(543, 30);
            this.RemoveRowButton.Name = "RemoveRowButton";
            this.RemoveRowButton.Size = new System.Drawing.Size(42, 23);
            this.RemoveRowButton.TabIndex = 4;
            this.RemoveRowButton.Text = "-";
            this.RemoveRowButton.UseVisualStyleBackColor = true;
            this.RemoveRowButton.Click += new System.EventHandler(this.RemoveRowButton_Click);
            // 
            // ColumnLable
            // 
            this.ColumnLable.AutoSize = true;
            this.ColumnLable.Location = new System.Drawing.Point(609, 31);
            this.ColumnLable.Name = "ColumnLable";
            this.ColumnLable.Size = new System.Drawing.Size(55, 17);
            this.ColumnLable.TabIndex = 3;
            this.ColumnLable.Text = "Column";
            // 
            // AddColumnButton
            // 
            this.AddColumnButton.Location = new System.Drawing.Point(670, 30);
            this.AddColumnButton.Name = "AddColumnButton";
            this.AddColumnButton.Size = new System.Drawing.Size(42, 23);
            this.AddColumnButton.TabIndex = 4;
            this.AddColumnButton.Text = "+";
            this.AddColumnButton.UseVisualStyleBackColor = true;
            this.AddColumnButton.Click += new System.EventHandler(this.AddColumnButton_Click);
            // 
            // RemoveColumnButton
            // 
            this.RemoveColumnButton.Location = new System.Drawing.Point(718, 30);
            this.RemoveColumnButton.Name = "RemoveColumnButton";
            this.RemoveColumnButton.Size = new System.Drawing.Size(42, 23);
            this.RemoveColumnButton.TabIndex = 4;
            this.RemoveColumnButton.Text = "-";
            this.RemoveColumnButton.UseVisualStyleBackColor = true;
            this.RemoveColumnButton.Click += new System.EventHandler(this.RemoveColumnButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(830, 25);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 30);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(926, 25);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 30);
            this.LoadButton.TabIndex = 5;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1013, 517);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.RemoveColumnButton);
            this.Controls.Add(this.AddColumnButton);
            this.Controls.Add(this.RemoveRowButton);
            this.Controls.Add(this.ColumnLable);
            this.Controls.Add(this.AddRowButton);
            this.Controls.Add(this.RowLable);
            this.Controls.Add(this.EvaluateButton);
            this.Controls.Add(this.InputTexbox);
            this.Controls.Add(this.Excel);
            this.Name = "Form1";
            this.Text = "Excel";
            ((System.ComponentModel.ISupportInitialize)(this.Excel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Excel;
        private System.Windows.Forms.TextBox InputTexbox;
        private System.Windows.Forms.Button EvaluateButton;
        private System.Windows.Forms.Label RowLable;
        private System.Windows.Forms.Button AddRowButton;
        private System.Windows.Forms.Button RemoveRowButton;
        private System.Windows.Forms.Label ColumnLable;
        private System.Windows.Forms.Button AddColumnButton;
        private System.Windows.Forms.Button RemoveColumnButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
    }
}

