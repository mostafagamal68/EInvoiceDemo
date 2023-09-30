using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoiceDemo.Shared.Helpers;

public class Utilites
{
    public static decimal GetNewDecimalId() => DateTime.Now.Ticks;
}
