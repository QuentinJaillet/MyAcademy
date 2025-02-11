namespace MyAcademy.Identity.Domain;

public class UserInformations
{
    public string Id { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}