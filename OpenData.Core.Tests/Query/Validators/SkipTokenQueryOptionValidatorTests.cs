﻿namespace OpenData.Core.Tests.Query.Validators
{
    using System.Net;
    using System.Net.Http;
    using OpenData.Core;
    using OpenData.Core.Query;
    using OpenData.Core.Query.Validators;
    using OData.Model;
    using Xunit;

    public class SkipTokenQueryOptionValidatorTests
    {
        public class WhenTheSkipTokenQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            public WhenTheSkipTokenQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$skiptoken=5"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => SkipTokenQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedQueryOption.FormatWith("$skiptoken"), exception.Message);
            }
        }

        public class WhenTheSkipTokenQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.SkipToken
            };

            public WhenTheSkipTokenQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$skiptoken=5"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                SkipTokenQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }
    }
}