﻿namespace OpenData.Core.Query.Validators
{
    using System.Net;

    /// <summary>
    /// A class which validates the $filter query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class FilterQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.Filter == null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Filter) != AllowedQueryOptions.Filter)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedQueryOption.FormatWith("$filter"));
            }

            ValidateFunctions(queryOptions, validationSettings);
            ValidateStringFunctions(queryOptions, validationSettings);
            ValidateDateFunctions(queryOptions, validationSettings);
            ValidateMathFunctions(queryOptions, validationSettings);
            ValidateLogicalOperators(queryOptions, validationSettings);
            ValidateArithmeticOperators(queryOptions, validationSettings);
        }

        private static void ValidateArithmeticOperators(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedArithmeticOperators == AllowedArithmeticOperators.All)
            {
                return;
            }

            var rawFilterValue = queryOptions.RawValues.Filter;

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Add) != AllowedArithmeticOperators.Add
                && rawFilterValue.Contains(" add "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("add"));
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Divide) != AllowedArithmeticOperators.Divide
                && rawFilterValue.Contains(" div "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("div"));
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Modulo) != AllowedArithmeticOperators.Modulo
                && rawFilterValue.Contains(" mod "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("mod"));
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Multiply) != AllowedArithmeticOperators.Multiply
                && rawFilterValue.Contains(" mul "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("mul"));
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Subtract) != AllowedArithmeticOperators.Subtract
                && rawFilterValue.Contains(" sub "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("sub"));
            }
        }

        private static void ValidateDateFunctions(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedFunctions == AllowedFunctions.AllFunctions
                || validationSettings.AllowedFunctions == AllowedFunctions.AllDateTimeFunctions)
            {
                return;
            }

            var rawFilterValue = queryOptions.RawValues.Filter;

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Year) != AllowedFunctions.Year
                && rawFilterValue.Contains("year("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("year"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Month) != AllowedFunctions.Month
                && rawFilterValue.Contains("month("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("month"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Day) != AllowedFunctions.Day
                && rawFilterValue.Contains("day("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("day"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Hour) != AllowedFunctions.Hour
                && rawFilterValue.Contains("hour("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("hour"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Minute) != AllowedFunctions.Minute
                && rawFilterValue.Contains("minute("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("minute"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Second) != AllowedFunctions.Second
                && rawFilterValue.Contains("second("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("second"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.FractionalSeconds) != AllowedFunctions.FractionalSeconds
                && rawFilterValue.Contains("fractionalseconds("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("fractionalseconds"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Now) != AllowedFunctions.Now
                && rawFilterValue.Contains("now("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("now"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.MinDateTime) != AllowedFunctions.MinDateTime
                && rawFilterValue.Contains("mindatetime("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("mindatetime"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.MaxDateTime) != AllowedFunctions.MaxDateTime
                && rawFilterValue.Contains("maxdatetime("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("maxdatetime"));
            }
        }

        private static void ValidateFunctions(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedFunctions == AllowedFunctions.AllFunctions)
            {
                return;
            }

            var rawFilterValue = queryOptions.RawValues.Filter;

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Cast) != AllowedFunctions.Cast
                && rawFilterValue.Contains("cast("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("cast"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.IsOf) != AllowedFunctions.IsOf
                && rawFilterValue.Contains("isof("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("isof"));
            }
        }

        private static void ValidateLogicalOperators(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedLogicalOperators == AllowedLogicalOperators.All)
            {
                return;
            }

            var rawFilterValue = queryOptions.RawValues.Filter;

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.And) != AllowedLogicalOperators.And
                && rawFilterValue.Contains(" and "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("and"));
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.Or) != AllowedLogicalOperators.Or
                && rawFilterValue.Contains(" or "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("or"));
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.Equal) != AllowedLogicalOperators.Equal
                && rawFilterValue.Contains(" eq "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("eq"));
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.NotEqual) != AllowedLogicalOperators.NotEqual
                && rawFilterValue.Contains(" ne "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("ne"));
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.GreaterThan) != AllowedLogicalOperators.GreaterThan
                && rawFilterValue.Contains(" gt "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("gt"));
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.GreaterThanOrEqual) != AllowedLogicalOperators.GreaterThanOrEqual
                && rawFilterValue.Contains(" ge "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("ge"));
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.LessThan) != AllowedLogicalOperators.LessThan
                && rawFilterValue.Contains(" lt "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("lt"));
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.LessThanOrEqual) != AllowedLogicalOperators.LessThanOrEqual
                && rawFilterValue.Contains(" le "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("le"));
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.Has) != AllowedLogicalOperators.Has
                && rawFilterValue.Contains(" has "))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedOperator.FormatWith("has"));
            }
        }

        private static void ValidateMathFunctions(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedFunctions == AllowedFunctions.AllFunctions
                || validationSettings.AllowedFunctions == AllowedFunctions.AllMathFunctions)
            {
                return;
            }

            var rawFilterValue = queryOptions.RawValues.Filter;

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Round) != AllowedFunctions.Round
                && rawFilterValue.Contains("round("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("round"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Ceiling) != AllowedFunctions.Ceiling
                && rawFilterValue.Contains("ceiling("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("ceiling"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Floor) != AllowedFunctions.Floor
                && rawFilterValue.Contains("floor("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("floor"));
            }
        }

        private static void ValidateStringFunctions(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedFunctions == AllowedFunctions.AllFunctions
                || validationSettings.AllowedFunctions == AllowedFunctions.AllStringFunctions)
            {
                return;
            }

            var rawFilterValue = queryOptions.RawValues.Filter;

            if ((validationSettings.AllowedFunctions & AllowedFunctions.EndsWith) != AllowedFunctions.EndsWith
                && rawFilterValue.Contains("endswith("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("endswith"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.StartsWith) != AllowedFunctions.StartsWith
                && rawFilterValue.Contains("startswith("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("startswith"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Contains) != AllowedFunctions.Contains
                && rawFilterValue.Contains("contains("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("contains"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.ToLower) != AllowedFunctions.ToLower
                && rawFilterValue.Contains("tolower("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("tolower"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.ToUpper) != AllowedFunctions.ToUpper
                && rawFilterValue.Contains("toupper("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("toupper"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Trim) != AllowedFunctions.Trim
                && rawFilterValue.Contains("trim("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("trim"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Length) != AllowedFunctions.Length
                && rawFilterValue.Contains("length("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("length"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.IndexOf) != AllowedFunctions.IndexOf
                && rawFilterValue.Contains("indexof("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("indexof"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Replace) != AllowedFunctions.Replace
                && rawFilterValue.Contains("replace("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("replace"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Substring) != AllowedFunctions.Substring
                && rawFilterValue.Contains("substring("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("substring"));
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Concat) != AllowedFunctions.Concat
                && rawFilterValue.Contains("concat("))
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedFunction.FormatWith("concat"));
            }
        }
    }
}