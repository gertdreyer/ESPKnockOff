using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

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

        public int DayOfMonthID { get; set; }

        public int StageID { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int TimeCodeID { get; set; }

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
}
