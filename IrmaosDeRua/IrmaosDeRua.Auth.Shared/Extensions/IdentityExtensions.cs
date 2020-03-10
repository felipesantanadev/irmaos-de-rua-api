using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Shared.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetErrorString(this IEnumerable<IdentityError> errors)
        {
            var errorString = new StringBuilder();
            foreach (var error in errors)
                errorString.Append(error.Description).Append("\n");

            return errorString.ToString();
        }
    }
}
