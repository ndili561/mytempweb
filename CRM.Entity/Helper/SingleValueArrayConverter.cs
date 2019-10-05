using System;
using Newtonsoft.Json;

namespace CRM.Entity.Helper
{
    public class SingleValueArrayConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        //Convert single string into array
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object retVal = new Object();
            if (reader.TokenType == JsonToken.String)
            {
                var instance = (string)serializer.Deserialize(reader, typeof(string));
                retVal = new string[] { instance };
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                retVal = serializer.Deserialize(reader, objectType);
            }

            return retVal;
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }
}
