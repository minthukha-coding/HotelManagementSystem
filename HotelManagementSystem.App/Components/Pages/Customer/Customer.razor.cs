﻿using HotelManagementSystem.Database.Db;
using HotelManagementSystem.Domain.Features.Admin.Customer;

namespace HotelManagementSystem.App.Components.Pages.Customer;

public partial class Customer
{
    private List<CustomerModel> Customers { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadCustomers();
    }

    private async Task LoadCustomers()
    {
        var result = await _customerService.GetCustomerUserList();
        if (result.IsSuccess)
        {
            Customers = result.Data!;
        }
        else
        {
        }
    }

    private async Task DeleteCustomer(string customerId)
    {
        await JS.InvokeVoidAsync("manageLoading", "show");
        var result = await _customerService.DeleteCustomer(customerId);
        await JS.InvokeVoidAsync("manageLoading", "remove");
        if (result.IsSuccess)
        {
            try
            {
                await JS.InvokeVoidAsync("notiflixNotify.success", result.Message!.ToString());
                await LoadCustomers();
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("notiflixNotify.succecss", ex.ToString());
            }
            return;
        }
        else
        {
            await JS.InvokeVoidAsync("notiflixNotify.error", result.Message!.ToString());
            return;
        }
    }
}
