using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CarDetailsDto :IDto
    {
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public String ColorName { get; set; }
        public decimal Dailyprice { get; set; }
    }
}
