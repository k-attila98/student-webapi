using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_webapi.Models
{
    public class StudentWrapper
    {

        public StudentWrapper()
        {
        }
        public StudentWrapper(string studentName, string phoneNumber, int year, DateTime born, float average, long count, int bestGrade)
        {
            this.Name = studentName;
            this.PhoneNumber = phoneNumber;
            this.Year = year;
            this.Born = born;
            this.GradeAverage = average;
            this.GradeCount = count;
            this.BestGrade = bestGrade;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int Year { get; set; }
        public DateTime Born { get; set; }
        public float GradeAverage { get; set; }
        public long GradeCount { get; set; }
        public int FailCount { get; set; }
        public int BestGrade { get; set; }
    }
}
