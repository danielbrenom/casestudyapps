using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Extensions;

namespace Applications
{
    public struct ComplexNumber
    {
        public float RealPart { get; }
        public float ImaginaryPart { get; }
        private ComplexSignal Signal => ImaginaryPart >= 0 ? ComplexSignal.Positive : ComplexSignal.Negative;
        private ComplexType Type => ImaginaryPart != 0 ? ComplexType.Imaginary : ComplexType.Real;
        public string Equation => $"{RealPart}{Signal.Description()}{ImaginaryPart}{Type.Description()}";

        private const string FullComplexPattern = @"([-+]?\d+\.?\d*|[-+]?\d*\.?\d+)\s*([-+])?\s*([-+]?\d+\.?\d*|[-+]?\d*\.?\d+)";
        private const string RealComplexPattern = @"([+-]?\d+";
        private const string ImaginaryComplexPattern = @"(^[+-]?\d+i)";
        private const string HalfComplexPattern = @"([+-]?\d+)\s*([-+])?\s*i";

        public ComplexNumber(object input)
        {
            Match matches;
            switch (input)
            {
                case int number:
                    RealPart = number;
                    ImaginaryPart = 0;
                    break;
                case string inputText when Regex.IsMatch(inputText, FullComplexPattern):
                    matches = Regex.Match(inputText, FullComplexPattern);
                    RealPart = float.Parse(matches.Groups[1].Value);
                    ImaginaryPart = float.Parse($"{matches.Groups[2].Value}{matches.Groups[3].Value}");
                    break;
                case string inputText when Regex.IsMatch(inputText, ImaginaryComplexPattern):
                    matches = Regex.Match(inputText, ImaginaryComplexPattern);
                    RealPart = 0;
                    ImaginaryPart = float.Parse($"{matches.Groups[1].Value}{matches.Groups[2].Value}");
                    break;
                case string inputText when Regex.IsMatch(inputText, RealComplexPattern):
                    matches = Regex.Match(inputText, RealComplexPattern);
                    RealPart = float.Parse(matches.Groups[1].Value);
                    ImaginaryPart = 0;
                    break;
                case string inputText when Regex.IsMatch(inputText, HalfComplexPattern):
                    matches = Regex.Match(inputText, HalfComplexPattern);
                    RealPart = float.Parse(matches.Groups[1].Value);
                    ImaginaryPart = matches.Groups[2].Value == "-" ? -1 : 1;
                    break;
                default:
                    RealPart = 0;
                    ImaginaryPart = 0;
                    break;
            }
        }

        public ComplexNumber(float a, float b)
        {
            RealPart = a;
            ImaginaryPart = b;
        }

        public ComplexNumber(double a, double b) : this((float)a, (float)b)
        {
        }

        public static implicit operator ComplexNumber(string valor) => new ComplexNumber(valor);
        public static implicit operator string(ComplexNumber complex) => complex.Equation;
        public static bool operator ==(string valor, ComplexNumber complex) => valor == complex.Equation;
        public static bool operator !=(string valor, ComplexNumber complex) => !(valor == complex);

        public static ComplexNumber operator +(ComplexNumber first, ComplexNumber second)
            => new ComplexNumber(first.RealPart + second.RealPart, first.ImaginaryPart + second.ImaginaryPart);

        public static ComplexNumber operator -(ComplexNumber first, ComplexNumber second)
            => new ComplexNumber(first.RealPart - second.RealPart, first.ImaginaryPart - second.ImaginaryPart);

        public static ComplexNumber operator *(ComplexNumber first, ComplexNumber second)
            => new ComplexNumber(first.RealPart * second.RealPart - first.ImaginaryPart * second.ImaginaryPart,
                                 first.RealPart * second.ImaginaryPart + first.ImaginaryPart * second.RealPart);

        public static ComplexNumber operator /(ComplexNumber first, ComplexNumber second)
            => new ComplexNumber((first.RealPart * second.RealPart + first.ImaginaryPart * second.ImaginaryPart) / (Math.Pow(second.RealPart, 2.0f) + Math.Pow(second.ImaginaryPart, 2.0f)),
                                 (first.ImaginaryPart * second.RealPart + first.RealPart * second.ImaginaryPart) / (Math.Pow(second.RealPart, 2.0f) + Math.Pow(second.ImaginaryPart, 2.0f)));

        public double ComplexModule() => Math.Sqrt(Math.Pow(RealPart, 2) + Math.Pow(ImaginaryPart, 2));

        public string ExponentialForm()
        {
            var angle = 180 * Math.Atan2(ImaginaryPart, RealPart) / Math.PI;
            var mod = ComplexModule();
            return $"{mod}*e^(i + {angle})";
        }

        public double ComplexSine() => ImaginaryPart / ComplexModule();
        public double ComplexCosine() => RealPart / ComplexModule();
        public double ComplexTangent() => ComplexSine() / ComplexCosine();

        public string ComplexSquareRoot()
        {
            var mod = ComplexModule();
            var angle = 180 * Math.Atan2(ImaginaryPart, RealPart) / Math.PI;
            return $"{Math.Sqrt(mod)}*e^(i{angle} + k*PI)";
        }

        public string ComplexExponentialLiteral() => $"e^{RealPart}+(cos{ImaginaryPart}+i*sin{ImaginaryPart})";

        public string ComplexLogaritmNatural()
        {
            var mod = ComplexModule();
            var angle = 180 * Math.Atan2(ImaginaryPart, RealPart) / Math.PI;
            var signal = angle >= 0 ? "+" : "";
            return $"{Math.Log(ComplexModule())}{signal}{angle}i";
        }

        public bool Equals(ComplexNumber other) => Equation == other.Equation;

        public override bool Equals(object other)
        {
            if (other is null) return false;
            return other is ComplexNumber complex && Equals(complex);
        }

        public override int GetHashCode() => Equation != string.Empty ? Equation.GetHashCode() : 0;
    }

    public enum ComplexSignal
    {
        [Description("+")]
        Positive,
        [Description("")]
        Negative
    }

    public enum ComplexType
    {
        [Description("")] Real,
        [Description("i")] Imaginary
    }
}