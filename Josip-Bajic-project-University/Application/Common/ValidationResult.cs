using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class ValidationResult
    {
        public bool IsSuccess => !ValidationItems.Any();
        public List<string> ValidationItems { get; set; } = new();
    }
}
