using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Excel
{
    public static class Calculator
    {
        public static Cell changingCell;
        public static string Evaluate(string expression)
        {
            expression = ReplaceExpression(expression);

            var lexer = new ExcelLexer(new AntlrInputStream(expression));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowExceptionErrorListener());

            var tokens = new CommonTokenStream(lexer);
            var parser = new ExcelParser(tokens);
            var tree = parser.compileUnit();
            var visitor = new ExcelVisitor();

            string result = Convert.ToString(visitor.Visit(tree));

            if (result == "∞" || result == "не число")
                throw new MyExceptions.DivideByZero();

            return result;
        }

        private static string ReplaceExpression(string expression)
        {
            Regex regex = new Regex("[A-Z]+[0-9]+");
            MatchEvaluator evaluator = new MatchEvaluator(ReferenceToValue);
            string newExpr = regex.Replace(expression, evaluator);
            return newExpr;
        }

        private static string ReferenceToValue(Match match)
        {
            string referenceName = match.Value;

            if (Grid.cells.ContainsKey(referenceName))
            {
                Cell cell = Grid.cells[referenceName];
                changingCell.PossibleIDependOnCells.Add(cell);

                return Grid.cells[referenceName].Value.ToString();
            }

            else
            {
                throw new MyExceptions.NonExistentCellException();
            }
        }
    }
}
