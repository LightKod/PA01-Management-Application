using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string? VoucherCode { get; set; }

    public double? VoucherValue { get; set; }
}
