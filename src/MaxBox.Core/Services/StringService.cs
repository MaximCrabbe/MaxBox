using System;

namespace MaxBox.Core.Services
{
    public class StringService : IStringService
    {
        private readonly Random _rng = new Random();
        public string GenerateString(int length, bool alphabetical = true, bool numeric = true, bool specialchars = false)
        {
            string allowedCharacters = "";
            if (alphabetical == false && numeric == false && specialchars == false)
                throw new ArgumentException("Atleast 1 parameter boolean has to be true.");
            if(length<1)
                throw new ArgumentException("Stringlength must atleast be 1 or higher.");

            if (alphabetical)
                allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            if (numeric)
                allowedCharacters += "0123456789";
            
            if (specialchars)
                allowedCharacters += "/*-+._)(";
            
           var newString = new char[length];

            for (int i = 0; i < length; i++)
            {
                newString[i] = allowedCharacters[_rng.Next(allowedCharacters.Length)];
            }
            return new string(newString);
        }
        public string GenerateLorum(int length)
        {
            return "ok.";
        }
    }
}