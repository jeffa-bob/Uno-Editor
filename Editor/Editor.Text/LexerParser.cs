using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;

namespace Editor.Text
{
    class TextLexer : Lexer
    {
        Recognizer<IToken, LexerATNSimulator> RuleNames { get; set; }
        Recognizer<IToken, LexerATNSimulator> Vocabulary { get; set; }
        Recognizer<IToken, LexerATNSimulator> GrammarFileName { get; set; }

        public TextLexer(ICharStream text):base(text){}
    }
    class TextParser : Parser
    {
        Recognizer<IToken, ParserATNSimulator> RuleNames { get; set; }
        Recognizer<IToken, ParserATNSimulator> Vocabulary { get; set; }
        Recognizer<IToken, ParserATNSimulator> GrammarFileName { get; set; }

        public TextParser(ITokenStream text) : base(text)
        {
        }
    }