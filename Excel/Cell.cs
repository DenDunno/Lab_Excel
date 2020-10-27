using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel
{
    public class Cell
    {
        public string Name { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public string EvaluatingExpression;

        public string RealExpression;

        public double Value;


        public List<Cell> CellsIDependOn { get; private set; } = new List<Cell>();

        public List<Cell> CellsDependentOnMe { get; private set; } = new List<Cell>();

        public List<Cell> PossibleIDependOnCells { get; private set; } = new List<Cell>();

        


        public Cell()
        {
            Row = 0;
            Column = 0;
            Name = "";
            EvaluatingExpression = "";
            RealExpression = "";
            Value = 0;
            CellsIDependOn = null;
            CellsDependentOnMe = null;
        }
        

        public Cell(string position , int _row , int _col)
        {
            Name = position;
            Row = _row;
            Column = _col;
            EvaluatingExpression = "";
            RealExpression = "";
            Value = 0;
        }


        public List<Cell> SetCell (string expr)
        {
            PossibleIDependOnCells.Clear();
            EvaluatingExpression = expr.Replace(" ", ""); // з виразу прибираємо всі пробіли            
            
            try 
            {
                Calculator.changingCell = this;
                string result = Calculator.Evaluate(EvaluatingExpression);  // тут можливий exception

                if (CheckLoop()) // і тут
                    throw new MyExceptions.LoopException();

                // Якщо не було exception ,  продовжуємо
                RealExpression = expr;
                Value = Convert.ToDouble(result);
                return EditDependencies();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return null;
        }


        private bool CheckLoop()
        {
            bool loop = false;          

            // Був, наприклад, вираз: A1 + B2. Ми не знаємо, чи є тут цикл (A3 зсилається на A1, а A1 на A3), тому, 
            // поки що змінювана комірка МОЖЛИВО залежить від A1 і B2 
            foreach (Cell cell in PossibleIDependOnCells)    
            {                
                if (cell.Name == Name) // якщо комірка зсилається сама на себе
                { 
                    loop = true;
                    break;
                }
            }

            var IDependOn = PossibleIDependOnCells;

            CheckLoopInDependOnMeCells(CellsDependentOnMe);
            // рекурсивно проходимось по дереву залежностей. Якщо комірка, від якої залежить наша == комірці, яка залежить від нашої, тоді є цикл
            void CheckLoopInDependOnMeCells(List<Cell> dependOnMeCells) 
            {
                for (int i = 0; i < dependOnMeCells.Count && !loop; ++i)
                {
                    foreach (Cell IDependOnCell in IDependOn)
                    {
                        if (dependOnMeCells[i] == IDependOnCell)
                        {
                            loop = true;
                            break;
                        }
                    }

                    CheckLoopInDependOnMeCells(dependOnMeCells[i].CellsDependentOnMe);
                }
            }

            return loop;
        }


        private List<Cell> EditDependencies()
        {
            foreach (Cell cell in CellsIDependOn)  // коли успішно змінився expression, у елементів виразу чистимо зв'язок з нашою
            {
                cell.CellsDependentOnMe.Remove(this); 
            }
            CellsIDependOn.Clear();  // коли успішно змінився expression, чистимо залежность комірки від елементів виразу


            foreach (Cell newCell in PossibleIDependOnCells) // Possible вже не possible , а вже точно IDependOn
            {
                CellsIDependOn.Add(newCell); // додавання нових залежностей у змінювану комірку    
                newCell.CellsDependentOnMe.Add(this); // додавання залежної комірки
            }


            ChangeDependentCellsValues(CellsDependentOnMe);

            void ChangeDependentCellsValues(List<Cell> dependentCells)  // рекурсивно змінюємо залежності від заложнестей                     
            { 
                if (dependentCells.Count != 0)      
                {
                    foreach (Cell cell in dependentCells)
                    {
                        cell.Value = Convert.ToDouble(Calculator.Evaluate(cell.EvaluatingExpression));
                        Grid.cells[cell.Name] = cell;
                        ChangeDependentCellsValues(cell.CellsDependentOnMe);                        
                    }
                }
            }

            return CellsDependentOnMe;
        }
    }
}
