namespace OpenData.Core
{
    using System.Runtime.Serialization;

    [DataContract]
    internal sealed class ODataErrorContent
    {
        [Newtonsoft.Json.JsonProperty("error", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 0)]
        public ODataError Error
        {
            get;
            set;
        }
    }
}