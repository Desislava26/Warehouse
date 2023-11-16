using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Core.Models
{     /// <summary>
     /// Поръчка
     /// <summary>
    public class CustomerOrder
    {
        /// <summary>
        /// Клиентски номер
        /// <summary>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// Списък с поръчки
        /// <summary>
        public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();
    }
}
