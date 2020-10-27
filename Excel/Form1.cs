using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Excel
{
    public partial class Form1 : Form
    {

        public static bool gridWasntSaved = true;
        public Form1()
        {
            InitializeComponent();
            InitializeExcel();
        }


        private void InitializeExcel()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Font = new Font("Times new roman", 12, FontStyle.Bold);
            columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Excel.ColumnHeadersVisible = true;
            Excel.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
           
            Excel.RowHeadersVisible = true;
            Excel.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            Excel.RowHeadersDefaultCellStyle = columnHeaderStyle;


            Excel.ColumnCount = 1;
            Excel.Columns[0].HeaderText = "A";

            Excel.RowCount = 1;
            Excel.Rows[0].HeaderCell.Value = "1";

            Grid.cells["A1"] = new Cell("A1", 0, 0);
        }



        private void AddRowButton_Click(object sender, EventArgs e)
        {
            int size = ++Excel.RowCount;
            string columnsName;

            for (int i = size - 1; i >= 0; i--)
            {
                Excel.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
            }

            for (int i = 0; i < Excel.ColumnCount; ++i)
            {
                columnsName = _26Converter.ConvertTo26(i + 1);
                string fullName = columnsName + size;

                Grid.cells[fullName] = new Cell (fullName , size - 1 , i);
            }
        }


        private void RemoveRowButton_Click(object sender, EventArgs e)
        {
            if (Excel.RowCount > 1)
            {
                RemoveColumnOrRow(false, Excel.ColumnCount);
                Grid.DeleteCellsFromDictionary(false, Excel.ColumnCount, Excel.RowCount);
            }
        }


        private void AddColumnButton_Click(object sender, EventArgs e)
        {

            int size = ++Excel.ColumnCount;
            string columnsName = _26Converter.ConvertTo26(size);

            Excel.Columns[size - 1].HeaderText = columnsName;

            for (int i = 0; i < Excel.RowCount; ++i)
            {
                string fullName = columnsName + (i + 1);

                Grid.cells[fullName] = new Cell (fullName , i , size - 1);
            }
        }


        private void RemoveColumnButton_Click(object sender, EventArgs e)
        {
            if (Excel.ColumnCount > 1)
            {
                RemoveColumnOrRow(true, Excel.RowCount);
                Grid.DeleteCellsFromDictionary(true, Excel.ColumnCount, Excel.RowCount);
            }
        }


        
        private void Excel_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            InputTexbox.Text = GetSelectedCell().RealExpression;
            InputTexbox.Focus();
        }



        private void EvaluateButton_Click(object sender, EventArgs e)
        {
            Cell cell = GetSelectedCell();
            var cellsToBeChanged  = cell.SetCell(InputTexbox.Text); // змінюємо комірку і отримуємо List залежних від неї

            Grid.ShowDependentCells(cellsToBeChanged , Excel);

            Excel[cell.Column, cell.Row].Value = cell.Value;
        }



        private Cell GetSelectedCell()
        {
            int column = Excel.SelectedCells[0].ColumnIndex + 1;
            int row = Excel.SelectedCells[0].RowIndex + 1;

            return Grid.cells[_26Converter.ConvertTo26(column) + row];
        }


        private void ReadMeButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "The operations you can use with cells:\n" +
                "addition = \"+\"\n" +
                "subtruction = \"-\"\n" +
                "multiplication = \"*\"\n" +
                "dividing = \"/\"\n" +
                "div = \"div\"\n" +
                "mod = \"mod\"\n" +
                "find max of 2 numbers = max(x1,x2)\n" +
                "find min of 2 numbers = min(x1,x2)\n" +
                "P.S. You dont need to write '=' at the beginning of expression.", "Help");
        }



        private void RemoveColumnOrRow (bool removeColumn , int size)
        {
            string fullName;
            bool OK = true;
            Cell cell;
            string columnsName = removeColumn ? _26Converter.ConvertTo26(Excel.ColumnCount): "";

            for (int i = 0; i < size && OK; ++i)
            {
                if (!removeColumn)
                columnsName = _26Converter.ConvertTo26(i + 1);

                fullName = removeColumn  ?  ( columnsName + (i + 1) )  :  (columnsName + Excel.RowCount);

                cell = Grid.cells[fullName];

                if (cell.CellsDependentOnMe.Count != 0)
                {
                    OK = false;
                    MessageBox.Show("You have dependencies. You cannot delete the column");
                }
            }

            if (OK)
            {
                if (removeColumn)
                    Excel.Columns.RemoveAt(Excel.ColumnCount - 1);
                
                else
                    Excel.Rows.RemoveAt(Excel.RowCount - 1);
            }
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TXT File|*.txt";
            saveFileDialog.Title = "Grid saving";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                Grid.SaveGrid(saveFileDialog.FileName, Excel);
        }




        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (gridWasntSaved)
            {
                DialogResult dialogResult = MessageBox.Show("You dont save your grid." +
                    "Continue?",
                "Load", MessageBoxButtons.YesNo);
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT File|*.txt";
            openFileDialog.Title = "Grid opening";
            openFileDialog.RestoreDirectory = true;


            if (openFileDialog.ShowDialog() == DialogResult.OK)
                Grid.OpenGrid(openFileDialog.FileName, Excel);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (gridWasntSaved)
            {
                var result = MessageBox.Show("Are you sure that you want to exit without saving?",
                    "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
