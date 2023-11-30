using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.Data
{
    public class ApplicationFile
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FileName { get; set; }

        public byte[] Content  { get; set; }

    }
}
