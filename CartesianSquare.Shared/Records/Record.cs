using CartesianSquare.Shared.Squares;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CartesianSquare.Shared.Records
{
    public class Record
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Value { get; set; } = 1;
        public int ParentBlock { get; set; }
        public int SquareId { get; set; } = 0;

        [JsonIgnore]
        public Square Square { get; set; }
        public Record() { }
    }
}
