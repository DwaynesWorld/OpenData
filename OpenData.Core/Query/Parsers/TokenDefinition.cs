namespace OpenData.Core.Query.Parsers
{
    using System.Text.RegularExpressions;

    [System.Diagnostics.DebuggerDisplay("{tokenType}: {Regex}")]
    internal struct TokenDefinition
    {
        private readonly TokenType tokenType;

        internal TokenDefinition(TokenType tokenType, string expression)
            : this(tokenType, expression, false)
        {
        }

        internal TokenDefinition(TokenType tokenType, string expression, bool ignore)
        {
            this.tokenType = tokenType;
            this.Regex = new Regex(@"\G" + expression, RegexOptions.Singleline);
            this.Ignore = ignore;
        }

        internal bool Ignore { get; }

        internal Regex Regex { get; }

        internal Token CreateToken(Match match) => new Token(match.Value, this.tokenType);
    }
}