using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Lib.UnitTests.FakeViewModels
{
    public class CheckModel
    {
        public string Id { get; set; }
        public string Index { get; set; }
        public string InvoiceNum { get; set; }
        public string ABA { get; set; }
        public string AccountNumber { get; set; }
        public string CheckNumber { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }

        public Image FrontImage { get; set; }
        public Image BackImage { get; set; }
    }

    public class DepositModel 
    {
        public string Id { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public int Status { get; set; }
        public int SUCCESS = 0;

    }
    public class ScannerViewModel
    {
        public string Id { get; set; }
        public bool buttonScan { get; set; }
        public string Text { get; set; }
    }
}
