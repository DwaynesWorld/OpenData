namespace OpenData.Core.Query.Validators
{
    using System;

    /// <summary>
    /// An enumeration which represents the arithmetic operators allowed in the $filter query option of an OData query.
    /// </summary>
    [Flags]
    public enum AllowedArithmeticOperators
    {
        /// <summary>
        /// Specifies that no arithmetic operators are allowed in the $filter query option.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies that the 'add' logical operator is allowed in the $filter query option.
        /// </summary>
        Add = 1,

        /// <summary>
        /// Specifies that the 'sub' logical operator is allowed in the $filter query option.
        /// </summary>
        Subtract = 2,

        /// <summary>
        /// Specifies that the 'mul' logical operator is allowed in the $filter query option.
        /// </summary>
        Multiply = 4,

        /// <summary>
        /// Specifies that the 'div' logical operator is allowed in the $filter query option.
        /// </summary>
        Divide = 8,

        /// <summary>
        /// Specifies that the 'mod' logical operator is allowed in the $filter query option.
        /// </summary>
        Modulo = 10,

        /// <summary>
        /// Specifies that all logical arithmetic are allowed in the $filter query option.
        /// </summary>
        All = Add | Subtract | Multiply | Divide | Modulo,
    }
}