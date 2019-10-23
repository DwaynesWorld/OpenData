namespace OpenData.Core.Query.Parsers
{
    [System.Diagnostics.DebuggerDisplay("{TokenType}: {Value}")]
    internal struct Token
    {
        internal Token(string value, TokenType tokenType)
        {
            this.Value = value;
            this.TokenType = tokenType;
        }

        internal TokenType TokenType { get; }

        internal string Value { get; }
    }
}