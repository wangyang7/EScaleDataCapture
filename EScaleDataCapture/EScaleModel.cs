using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EScaleDataCapture
{
    public class EScaleModel
    {
        public int RowNum { get; set;}
        public string Name { get; set; }

        public string PortName { get; set; }

        public string weight {  get; set; }

        public decimal Weight 
        { 
            get
            {
                return decimal.Parse(weight);
            }
         }

        public DateTime? OperationTime { get; set; }
        
    }
}
