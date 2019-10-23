namespace OpenData.Core
{
    using System.Runtime.Serialization;

    [DataContract]
    internal sealed class ODataError
    {
        [Newtonsoft.Json.JsonProperty("code", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 0)]
        public string Code
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("message", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 1)]
        public string Message
        {
            get;
            set;
        }

        [Newtonsoft.Json.JsonProperty("target", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 2)]
        public string Target
        {
            get;
            set;
        }
    }
}