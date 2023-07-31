namespace AwesomeShop.Services.Payments.Api.Events;

public class PaymentAccepted
{
    public PaymentAccepted(Guid id, string fullName, string email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }

    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}