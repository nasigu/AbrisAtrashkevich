using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AbrisAtrashkevich.Model
{
    public partial class EntityJson<T>
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public Result<T> Result { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("usename")]
        public string Usename { get; set; }
    }

    public partial class Result<T>
    {
        [JsonProperty("data")]
        public T[] Data { get; set; }

        [JsonProperty("records")]
        public Record[] Records { get; set; }

        [JsonProperty("offset")]
        public object Offset { get; set; }

        [JsonProperty("sql")]
        public string Sql { get; set; }
    }

    public partial class Record
    {
        [JsonProperty("count")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Count { get; set; }
    }

    public partial class DatumArts
    {

        [JsonProperty("arts_key")]
        public Guid ArtsKey { get; set; }

        [JsonProperty("artschar_key")]
        public Guid ArtscharKey { get; set; }

        [JsonProperty("price_key")]
        public Guid PriceKey { get; set; }

        [JsonProperty("shop_id")]
        public Guid ShopId { get; set; }

        [JsonProperty("barcode_id")]
        public Guid BarcodeId { get; set; }

        [JsonProperty("arts_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ArtsId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("test")]
        public string Test { get; set; }
    }

    public class DatumArtsChar
    {
        [JsonProperty("artschar_key")]
        public Guid ArtscharKey { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }
    }

    
    public class DatumPrice
    {
        [JsonProperty("price_key")]
        public Guid ArtscharKey { get; set; }

        [JsonProperty("value")]
        public string Name { get; set; }

        [JsonProperty("price_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }
    }

    public class DatumShop
    {
        [JsonProperty("shop_key")]
        public Guid ArtscharKey { get; set; }

        [JsonProperty("address")]
        public string Name { get; set; }

        [JsonProperty("shop_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }
    }

    public class DatumBarcode
    {
        [JsonProperty("barcode_key")]
        public Guid ArtscharKey { get; set; }

        [JsonProperty("value")]
        public string Name { get; set; }

        [JsonProperty("barcode_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }
    }

    public partial class Datas
    {
        public DatumArts[] arts { get; set; }
        public DatumArtsChar[] artschar { get; set; }
        public DatumPrice[] price{ get; set; }
        public DatumShop[] shop{ get; set; }
        public DatumBarcode[] barcode{ get; set; }
    }

    public partial class Entity
    {
        public static Entity FromJson(string json) => JsonConvert.DeserializeObject<Entity>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Entity self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
