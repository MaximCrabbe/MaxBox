namespace MaxBox.Core.Services
{
    public interface IStringService
    {
        string GenerateString(int length, bool alphabetical = true, bool numeric = true, bool specialchars = false);
        string GenerateLorum(int length);
    }
}