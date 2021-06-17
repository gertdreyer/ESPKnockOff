using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ESPKnockOff.Models
{
    public class Province
    {
        [DataMember]
        [Key]
        public int ProvinceID { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ProvinceID.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                return hash;
            }
        }
    }

    public class Municipality
    {
        [DataMember]
        [Key]
        public int MunicipalityID { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public int ProvinceID { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + MunicipalityID.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + ProvinceID.GetHashCode();
                return hash;
            }
        }
    }

    public class Suburb
    {
        [DataMember]
        [Key]
        public int SuburbID { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public int SuburbClusterID { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public int MunicipalityID { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + SuburbID.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                hash = hash * 23 + SuburbClusterID.GetHashCode();
                hash = hash * 23 + MunicipalityID.GetHashCode();
                return hash;
            }
        }
    }

    public class SuburbCluster
    {
        [DataMember]
        [Key]
        public int SuburbClusterID { get; set; }

    }

    public class LoadSheddingSlot
    {
        [DataMember]
        [Key]
        public int LoadSheddingSlotID { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        [Display(Name = "DayOfMonthID")]
        public int DayOfMonthID { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        [Display(Name = "StageID")]
        public int StageID { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public int TimeCodeID { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public int SuburbClusterID { get; set; }
    }

    public class Schedule
    {
        public int ScheduleID { get; set; }

        public int Day { get; set; }

        public int Stage { get; set; }

        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan StartTime { get; set; }

        [JsonConverter(typeof(TimespanConverter))]
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public TimeSpan EndTime { get; set; }

        public int SuburbClusterID { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ScheduleID.GetHashCode();
                hash = hash * 23 + Day.GetHashCode();
                hash = hash * 23 + Stage.GetHashCode();
                hash = hash * 23 + StartTime.GetHashCode();
                hash = hash * 23 + EndTime.GetHashCode();
                hash = hash * 23 + SuburbClusterID.GetHashCode();
                return hash;
            }
        }
    }

    public class TimeCode
    {
        [DataMember]
        [Key]
        public int TimeCodeID { get; set; }

        [DataMember]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [DataMember]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }
    }

    public class TimespanConverter : JsonConverter<TimeSpan>
    {
        public const string TimeSpanFormatString = @"hh\:mm";

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            var timespanFormatted = $"{value.ToString(TimeSpanFormatString)}";
            writer.WriteValue(timespanFormatted);
        }

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            TimeSpan parsedTimeSpan;
            TimeSpan.TryParseExact((string)reader.Value, TimeSpanFormatString, null, out parsedTimeSpan);
            return parsedTimeSpan;
        }
    }
}
