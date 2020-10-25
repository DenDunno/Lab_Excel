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
                Excel.Rows.RemoveAt(Excel.RowCount - 1);
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
                Excel.Columns.RemoveAt(Excel.ColumnCount - 1);
            }
        }


        
        private void Excel_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            InputTexbox.Text = GetSelectedCell().RealExpression;
            InputTexbox.Focus();
        }


        private void EvaluateButton_Click(object sender, EventArgs e)
        {
            Cell _cell = GetSelectedCell();
            List<Cell> cellsToBeChanged = _cell.SetCell(InputTexbox.Text);

            if (cellsToBeChanged != null)
                ShowChangedCells(_cell.CellsDependentOnMe);

            void ShowChangedCells(List<Cell> dependentCells)  // рекурсивно виводжу змінені залежності від заложнестей 
            {
                if (dependentCells.Count != 0)
                {
                    foreach (Cell cel in dependentCells)
                    {
                        Excel[cel.Column, cel.Row].Value = cel.Value;
                        ShowChangedCells(cel.CellsDependentOnMe);
                    }
                }
            }

            


            Excel[_cell.Column, _cell.Row].Value = _cell.Value;
        }


        private Cell GetSelectedCell()
        {
            int column = Excel.SelectedCells[0].ColumnIndex + 1;
            int row = Excel.SelectedCells[0].RowIndex + 1;

            return Grid.cells[_26Converter.ConvertTo26(column) + row];
        }
    }
}
