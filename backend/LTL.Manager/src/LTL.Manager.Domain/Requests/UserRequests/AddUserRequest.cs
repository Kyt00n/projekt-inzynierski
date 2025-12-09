using LTL.Manager.Domain.Entities;

namespace LTL.Manager.Domain.Requests.UserRequests;

public class AddUserRequest : User
{
    public string Name { get;set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PasswordHash {get; set; }
}
