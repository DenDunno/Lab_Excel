using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Excel
{
    class ExcelVisitor : ExcelBaseVisitor<double>
    {
        Dictionary<string, double> tableIdentifier = new Dictionary<string, double>();

        public override double VisitCompileUnit(ExcelParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }


        public override double VisitNumberExpr(ExcelParser.NumberExprContext context)
        {
            return double.Parse(context.GetText());
        }


        public override double VisitIdentifierExpr(ExcelParser.IdentifierExprContext context)
        {
            var expr = context.GetText();
            double result;
            
            if (tableIdentifier.TryGetValue(expr.ToString(), out result))
                return result;

            else            
                return 0.0;
        }


        public override double VisitParenthesizedExpr(ExcelParser.ParenthesizedExprContext context)
        {
            if (context.GetText()[0] == '-')
                return -Visit(context.expression());
            
            else
                return Visit(context.expression());
        }


        public override double VisitAdditiveExpr(ExcelParser.AdditiveExprContext context)
        {
            var left = TakeLeft(context);
            var right = TakeRight(context);

            if (context.operatorToken.Type == ExcelLexer.ADD)
                return left + right;
            
            else //ExcelLexer.SUBTRACT
                return left - right;
        }

      
        public override double VisitMultiplicativeExpr(ExcelParser.MultiplicativeExprContext context)
        {
            var left = TakeLeft(context);
            var right = TakeRight(context);

            if (context.operatorToken.Type == ExcelLexer.MULTIPLY)
                return left * right;
            
            else //ExcelLexer.DIVIDE
                return left / right;
        }


        public override double VisitModDiv([NotNull] ExcelParser.ModDivContext context)
        {
            var left = TakeLeft(context);
            var right = TakeRight(context);

            if (context.operatorToken.Type == ExcelLexer.MOD)
                return left % right;

            else //ExcelLexer.DIVIDE
                return (int)(left / right);
        }


        public override double VisitMinMaxExpr([NotNull] ExcelParser.MinMaxExprContext context)
        {
            var expr = context.GetText();

            int start = 4; // MAX(132,2654)   expr[4] = 1
            int length = expr.IndexOf(')') - start; // length = Length ("132,2564")

            expr = expr.Substring(start, length); // MAX(132,2654) ->  132,2654

            string[] numbers = expr.Split(',');  // "132,2564" -> [0] = "132" [1] = "2564"

            double first = Convert.ToDouble(numbers[0]);
            double second = Convert.ToDouble(numbers[1]);

            if (context.operatorToken.Type == ExcelLexer.MAX)
                return Math.Max(first, second);

            else
                return Math.Min(first, second);
        }


        private double TakeLeft(ExcelParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<ExcelParser.ExpressionContext>(0));
        }
        private double TakeRight(ExcelParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<ExcelParser.ExpressionContext>(1));
        }
    }
}

