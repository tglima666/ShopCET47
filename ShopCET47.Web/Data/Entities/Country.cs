using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.Web.Data.Entities
{
    public class Country : IEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
