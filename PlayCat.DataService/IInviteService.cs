namespace PlayCat.DataService
{
    public interface IInviteService
    {
        string GenerateInvite();
        bool IsInviteValid(string key);
    }
}