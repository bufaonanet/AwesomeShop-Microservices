﻿namespace AwesomeShop.Services.Payments.Api.Core.Entities;

public class Invoice
{
    public Invoice(decimal totalPrice, Guid orderId, string cardNumber)
    {
        TotalPrice = totalPrice;
        OrderId = orderId;
        CardNumber = "**-" + cardNumber.Substring(cardNumber.Length - 4);
        PaidAt = DateTime.Now;
    }

    public Guid Id { get; set; }
    public decimal TotalPrice { get; private set; }
    public Guid OrderId { get; private set; }
    public string CardNumber { get; private set; }
    public DateTime PaidAt { get; private set; }
}