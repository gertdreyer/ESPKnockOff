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


        [IgnoreDataMember]
        public virtual ICollection<Municipality> Municipality { get; set; }
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
        [IgnoreDataMember]
        [ForeignKey("ProvinceID")]
        public virtual Province Province { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Suburb> Suburb { get; set; }
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
        [IgnoreDataMember]
        [ForeignKey("SuburbClusterID")]
        public virtual SuburbCluster SuburbCluster { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public int MunicipalityID { get; set; }
        [IgnoreDataMember]
        [ForeignKey("MunicipalityID")]
        public virtual Municipality Municipality { get; set; }
    }

    public class SuburbCluster
    {
        [DataMember]
        [Key]
        public int SuburbClusterID { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Suburb> Suburb { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<LoadSheddingSlot> LoadSheddingSlot { get; set; }
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
        [IgnoreDataMember]
        [ForeignKey("TimeCodeID")]
        public virtual TimeCode TimeCode { get; set; }

        [DataMember(IsRequired = true)]
        [Required]
        public int SuburbClusterID { get; set; }
        [IgnoreDataMember]
        [ForeignKey("SuburbClusterID")]
        public virtual SuburbCluster SuburbCluster { get; set; }
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
