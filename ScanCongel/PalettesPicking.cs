using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanCongel
{
    public class PalettesPicking
    {
        public String PalletteSSCC { get; set; }
        public String OrderNo { get; set; }
        public int LineNo { get; set; }
        public decimal Qty { get; set; }
        public decimal QtyToPick { get; set; }
        public String ItemNo { get; set; }
        public String LotNo { get; set;  }
        public String BinNo { get; set; }
        public Boolean Picked { get; set; }
        public Boolean LotBloque { get; set; }

    }
}
