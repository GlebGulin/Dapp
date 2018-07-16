using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Empoy
    {
        [Key]
        public int EmployId {get; set;}
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
    }
}
