using System;
using System.Security.Cryptography;
using System.Text;

namespace Ellp.Api.Application.Utilities
{
    public static class GenerateCertificateHash
    {
        public static string Generate(int studentId, int workshopId)
        {
            using (var sha256 = SHA256.Create())
            {
                var rawData = $"{studentId}-{workshopId}-{Guid.NewGuid()}";
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}




