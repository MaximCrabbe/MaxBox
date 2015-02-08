using System;

namespace MaxBox.Core.Services
{
    [CLSCompliant(true)]
    public interface IRngService
    {
        string GenerateLorum(int length);

        string GenerateString(int length, bool lowerAlphabetical = true, bool upperAlphabetical = true,
            bool numeric = true, bool specialchars = false);
    }
}