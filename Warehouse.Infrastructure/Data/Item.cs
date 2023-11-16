using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.Data
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(30)]
        public string Barcode { get; set; }


        [Required]

        [StringLength(100)]
        public string Label { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateFrom { get; set; } = (DateTime.Today);

        [Column(TypeName = "date")]
        public DateTime? DateTo { get; set; }

        public IList<Rack> Racks { get; set; } = new List<Rack>();

        [Required]
        public Guid CategoryId { get; set; }



        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

    }
}
