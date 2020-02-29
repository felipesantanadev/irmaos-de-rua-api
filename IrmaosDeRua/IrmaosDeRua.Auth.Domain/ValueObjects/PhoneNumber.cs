using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrmaosDeRua.Auth.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject<PhoneNumber>
    {
        private string DDD { get; }
        private string Number { get; }
        public string Value { get { return $"{DDD}{Number}"; } }

        private PhoneNumber(string ddd, string number)
        {
            DDD = ddd;
            Number = number;
        }

        public static Result<PhoneNumber> Create(string ddd, string number)
        {
            if (string.IsNullOrEmpty(ddd) || ddd.Length != 2)
                return Result.Failure<PhoneNumber>("Invalid phone DDD.");
            if(string.IsNullOrEmpty(number) || number.Length > 9 || number.Length < 8)
                return Result.Failure<PhoneNumber>("Invalid phone number.");

            return Result.Ok(new PhoneNumber(ddd, number));
        }

        protected override bool EqualsCore(PhoneNumber other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
