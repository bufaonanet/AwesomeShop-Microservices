namespace AwesomeShop.Services.Orders.Application.Dtos;

public class GenericHandlerResult
{
    public string Message { get; private set; }
    public object Data { get; private set; }
    public bool Success { get; private set; }
    public List<ValidationObject> Validations { get; private set; }

    public GenericHandlerResult(string message, object data, bool success, List<ValidationObject> validations)
    {
        Message = message;
        Data = data;
        Success = success;
        Validations = validations;
    }
}

public class ValidationObject
{
    public string PropertyName { get; private set; }
    public string Message { get; private set; }

    public ValidationObject(string propertyName, string message)
    {
        PropertyName = propertyName;
        Message = message;  
    }
}