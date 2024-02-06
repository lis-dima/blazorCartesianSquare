using Shared.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Squares
{
    public class Square
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        public List<Record> Records { get; set; } = new();
        public Square() { }
        public Square(CreateSquareDto data)
        {
            Title = data.Title;
        }
    }
}
