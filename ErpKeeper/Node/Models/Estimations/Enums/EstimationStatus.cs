using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeper.Node.Models.Estimations.Enums
{

    public enum QuoteStatus
    {
        Quote = 0,
        Ordered = 1,
        Close = 2,
        Paid = 3,
        Void = 99

    }
}
