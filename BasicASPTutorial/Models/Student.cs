using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BasicASPTutorial.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณาป้อนชื่อนักเรียน")]
        [DisplayName("ชื่อนักเรียน")]
        public string Name { get; set; }

        [DisplayName("คะแนนสอบ")]
        [Range(0, 100, ErrorMessage = "กรุณาป้อนคะแนนสอบในช่วง 0-100")]
        public int Score { get; set; }

        /*[Required(ErrorMessage = "กรุณาป้อนวันที่ปรับปรุง")]*/
        [DisplayName("วันที่ปรับปรุง")]  
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "NULL")]
        public Nullable<System.DateTime> DateUpdate { get; set; }
        // public DateTime? DateUpdate { get; set; }
    }
}
