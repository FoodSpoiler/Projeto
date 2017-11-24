using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgilFood.Core.models
{
    public class Photo
    {
        public int Id { get; set; }
        public int FornecedorId { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
    }
}
