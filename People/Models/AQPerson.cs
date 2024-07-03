using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace People.Models
{
    [Table("people")]
    public class AQPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
