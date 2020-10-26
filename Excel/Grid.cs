using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel
{
    static class Grid
    {
        public static Dictionary<string, Cell> cells = new Dictionary<string, Cell>();

        public static void DeleteCellsFromDictionary(bool deleteColumn, int columns, int rows)
        {
            int size = deleteColumn ? rows : columns;

            string fullName;
            string columnsName = deleteColumn ? _26Converter.ConvertTo26(columns + 1) : "";

            for (int i = 0; i < size; ++i)
            {
                if (!deleteColumn)
                    columnsName = _26Converter.ConvertTo26(i + 1);

                fullName = deleteColumn ? (columnsName + (i + 1)) : (columnsName + rows + 1);

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


        public static void OlenGrid(string filepath, DataGridView excel)
        {
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                excel.ColumnCount = Convert.ToInt32(streamReader.ReadLine());
                excel.RowCount = Convert.ToInt32(streamReader.ReadLine());
            }
        }

    }
}