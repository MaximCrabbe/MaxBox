using System;

namespace MaxBox.Core.Services
{


    [CLSCompliant(true)]
    public interface IRngService
    {
        string GenerateString(int length, bool alphabetical = true, bool numeric = true, bool specialchars = false);
        string GenerateLorum(int length);
    }
}