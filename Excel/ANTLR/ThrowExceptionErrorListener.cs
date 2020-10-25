using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Excel
{
    class ThrowExceptionErrorListener : BaseErrorListener, IAntlrErrorListener<int>
    {
        //BaseErrorListener implementation
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            throw new ArgumentException($"Eror {0}" , msg , e);
        }

        //IAntlrErrorListener&lt;int&gt; implementation
        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg,
        RecognitionException e)
        {
            throw new ArgumentException($"Eror {0}", msg, e);
        }
    }
}
