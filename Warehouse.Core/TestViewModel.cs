using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.CustomAttributes;

namespace Warehouse.Core
{
    public class TestViewModel
    {
        public DateTime FirstDate { get; set; }
        [IsBefore(nameof(FirstDate), errorMessage:"test")]
        public DateTime SecondDate { get; set; }
    }
}
