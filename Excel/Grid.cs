using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel
{
    public static class Grid
    {
        public static Dictionary<string, Cell> cells = new Dictionary<string, Cell>();

        public static void ShowDependentCells(List<Cell> cellsToBeChanged , DataGridView Excel) // рекурсивно виводжу змінені залежні від залежних комірки
        {
            if (cellsToBeChanged != null)
            {
                if (cellsToBeChanged.Count != 0)
                {
                    foreach (Cell cel in cellsToBeChanged)
                    {
                        Excel[cel.Column, cel.Row].Value = cel.Value;
                        ShowDependentCells(cel.CellsDependentOnMe, Excel);
                    }
                }
            }
        }


        public static void DeleteCellsFromDictionary(bool deleteColumn, int columns, int rows)
        {
            int size = deleteColumn ? rows : columns;

            string fullName;
            string columnsName = deleteColumn ? _26Converter.ConvertTo26(columns + 1) : "";

            for (int i = 0; i < size; ++i)
            {
                if (!deleteColumn)
                    columnsName = _26Converter.ConvertTo26(i + 1);

                fullName = deleteColumn ? (columnsName + (i + 1)) : (columnsName + (rows + 1));
                Cell cell = cells[fullName];

                foreach (Cell IDependOnCell in cell.CellsIDependOn) // чистимо залежності
                    IDependOnCell.CellsDependentOnMe.Remove(cell);

                foreach (Cell CelllDependOnMe in cell.CellsDependentOnMe)
                    CelllDependOnMe.CellsIDependOn.Remove(cell);

                cells.Remove(fullName);
            }
        }


        public static void SaveGrid(string filepath, DataGridView excel)
        {
            using (StreamWriter streamWriter = new StreamWriter(filepath))
            {
                streamWriter.WriteLine(excel.ColumnCount);
                streamWriter.WriteLine(excel.RowCount);

                foreach (Cell cell in cells.Values)
                {
                    streamWriter.WriteLine(cell.Name);
                    streamWriter.WriteLine(cell.Value);
                    streamWriter.WriteLine(cell.RealExpression);
                }

                Form1.gridWasntSaved = false;
            }
        }


        public static void OpenGrid(string filepath, DataGridView excel)
        {
            StreamReader streamReader = null;

            try
            {
                streamReader = new StreamReader(filepath);
            }
            catch
            {
                MessageBox.Show("Something wrong with your file :c");
                return;
            }

            using (streamReader)
            {
                SetGrid(streamReader.ReadLine(), streamReader.ReadLine() , excel);

                while (!streamReader.EndOfStream) // зчитуємо інформаціємо і записуємо її в cells
                {
                    string name = streamReader.ReadLine();
                    string value = streamReader.ReadLine();
                    string expression = streamReader.ReadLine();

                    var position = _26Converter.Split(name);

                    cells[name] = new Cell(name, --position[0], --position[1]); // position повертає по суті назву A1 (тобто 1,1). А справжня позиція 0 , 0
                    cells[name].Value = Convert.ToDouble(value);
                    cells[name].RealExpression = expression;
                    cells[name].EvaluatingExpression = expression.Replace(" ", "");
                }

                ConnectCellsWithEachOther(excel);
            }
        }



        private static void SetGrid(string columns , string rows , DataGridView excel)
        {
            cells.Clear();

            excel.ColumnCount = Convert.ToInt32(columns); // створення таблиці
            excel.RowCount = Convert.ToInt32(rows);

            for (int i = 0; i < excel.ColumnCount; ++i)
                excel.Columns[i].HeaderText = _26Converter.ConvertTo26(i + 1);

            for (int i = 0; i < excel.RowCount; ++i)
                excel.Rows[i].HeaderCell.Value = Convert.ToString(i + 1);
        }



        private static void ConnectCellsWithEachOther (DataGridView excel) // відновляємо зв'язки між комірками та виводимо
        {
            foreach (Cell cell in cells.Values)
            {
                List<string> IDependOnCellsNames = FindDependencies(cell.RealExpression);

                foreach (string name in IDependOnCellsNames)
                {
                    cell.CellsIDependOn.Add(cells[name]);
                    cells[name].CellsDependentOnMe.Add(cell);
                }
            }

            for (int i = 0; i < excel.ColumnCount; ++i) // виводимо всі ініціалізовані комірки
            {
                for (int j = 0; j < excel.RowCount; ++j)
                {
                    Cell cell = cells[_26Converter.ConvertTo26(i + 1) + (j + 1)];
                    excel[i, j].Value = (cell.RealExpression == "")   ?   ""   :  cell.Value.ToString();
                }
            }
        }


        private static List<string> FindDependencies(string expression) // розбиваємо вираз та беремо комірки, від яких залежить наша
        {
            Regex regex = new Regex("[A-Z]+[0-9]+");
            MatchCollection matchCollection = regex.Matches(expression); 

            List<string> IDependOnCells = new List<string>();

            foreach (var match in matchCollection)
                IDependOnCells.Add(match.ToString());

            return IDependOnCells;
        }
    }
}