using System;
using MongoDB.Bson.Serialization.Attributes;

namespace eTamir.Services.Address.Models
{
    public class Country
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("id")]
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int? CountryId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("iso3")]
        public string Iso3 { get; set; }

        [BsonElement("iso2")]
        public string Iso2 { get; set; }

        [BsonElement("numeric_code")]
        public string NumericCode { get; set; }

        [BsonElement("phone_code")]
        public string PhoneCode { get; set; }

        [BsonElement("capital")]
        public string Capital { get; set; }

        [BsonElement("currency")]
        public string Currency { get; set; }

        [BsonElement("currency_name")]
        public string CurrencyName { get; set; }

        [BsonElement("currency_symbol")]
        public string CurrencySymbol { get; set; }

        [BsonElement("tld")]
        public string Tld { get; set; }

        [BsonElement("native")]
        public string Native { get; set; }

        [BsonElement("region")]
        public string Region { get; set; }

        [BsonElement("region_id")]
        public int? RegionId { get; set; }

        [BsonElement("subregion")]
        public string Subregion { get; set; }

        [BsonElement("subregion_id")]
        public int? SubregionId { get; set; }

        [BsonElement("nationality")]
        public string Nationality { get; set; }

        [BsonElement("timezones")]
        public Timezone[] Timezones { get; set; }

        [BsonElement("translations")]
        public Translations Translations { get; set; }

        [BsonElement("latitude")]
        public string Latitude { get; set; }

        [BsonElement("longitude")]
        public string Longitude { get; set; }

        [BsonElement("emoji")]
        public string Emoji { get; set; }

        [BsonElement("emojiU")]
        public string EmojiU { get; set; }
    }

    public class Timezone
    {
        [BsonElement("zoneName")]
        public string ZoneName { get; set; }

        [BsonElement("gmtOffset")]
        public int? GmtOffset { get; set; }

        [BsonElement("gmtOffsetName")]
        public string GmtOffsetName { get; set; }

        [BsonElement("abbreviation")]
        public string Abbreviation { get; set; }

        [BsonElement("tzName")]
        public string TzName { get; set; }
    }

    public class Translations
    {
        [BsonElement("kr")]
        public string Kr { get; set; }

        [BsonElement("pt-BR")]
        public string PtBR { get; set; }

        [BsonElement("pt")]
        public string Pt { get; set; }

        [BsonElement("nl")]
        public string Nl { get; set; }

        [BsonElement("hr")]
        public string Hr { get; set; }

        [BsonElement("fa")]
        public string Fa { get; set; }

        [BsonElement("de")]
        public string De { get; set; }

        [BsonElement("es")]
        public string Es { get; set; }

        [BsonElement("fr")]
        public string Fr { get; set; }

        [BsonElement("ja")]
        public string Ja { get; set; }

        [BsonElement("it")]
        public string It { get; set; }

        [BsonElement("cn")]
        public string Cn { get; set; }

        [BsonElement("tr")]
        public string Tr { get; set; }
    }
}