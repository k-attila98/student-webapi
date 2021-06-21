using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace student_webapi.Models
{
    public class GradeItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int Grade { get; set; }

        public long StudentId { get; set; }
        [JsonIgnore]
        public StudentItem Student { get; set; }
    }
}
