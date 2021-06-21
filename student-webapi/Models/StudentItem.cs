using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace student_webapi.Models
{
    public class StudentItem
    {
        public StudentItem()
        {
            Grades = new HashSet<GradeItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Born { get; set; }
        [JsonIgnore]
        public ICollection<GradeItem> Grades { get; set; }
    }
}
