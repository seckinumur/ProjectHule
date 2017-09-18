using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.VM
{
   public class VMRAM
    {
        public static List<string> RamData { get; set; }
        public static List<string> RamData2 { get; set; }
        public static List<string> RamData3 { get; set; }
        public static VMSection SecData { get; set; }
    }
}
