using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationManagerService.Application.Common.Validations;

public static class ValidationMessages
{
    public static string InvalidEmptyValue { get; set; } = "Value can't be empty";
    public static string InvalidLengthValue(int min, int max) => $"Value must be between {min} and {max} length";
}
