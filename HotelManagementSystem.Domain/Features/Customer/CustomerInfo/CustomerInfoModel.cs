using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Features.Customer.CustomerInfo;

public class CustomerInfoModel
{
    public string CustomerId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
}
