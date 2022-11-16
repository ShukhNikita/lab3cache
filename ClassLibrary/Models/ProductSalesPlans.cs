using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Models
{
    public class ProductSalesPlans
    {

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ProductionTypeId { get; set; }
        public double PlannedImplementationVolume { get; set; }
        public double ActualImplementationVolume { get; set; }
        public int QuarterInfo { get; set; }
        public int YearInfo { get; set; }
    }
}
