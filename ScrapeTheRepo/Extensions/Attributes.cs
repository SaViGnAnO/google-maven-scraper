using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapeTheRepo.Enums;

namespace ScrapeTheRepo.Extensions
{
    public class Attributes
    {
        public class StringValueAttribute : Attribute
        {
            public StringValueAttribute(string value)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
            }

            public string Value { get; }

            public override string ToString() => Value;
        }
    }
}
