﻿using System;
using DotNetCore.API.Template.Recurso;

namespace DotNetCore.API.Template.Compartilhado.ObjetosDeValor
{
    public struct EnumInputData<T>
        : IFormattable, IComparable, IConvertible,
        IComparable<EnumInputData<T>>, IComparable<T>,
        IEquatable<EnumInputData<T>>, IEquatable<T>
        where T: struct
    {
        public EnumInputData(string input)
        {
            TryParse(input, out EnumInputData<T> output);
            this = output;
        }

        public EnumInputData(T input)
        {
            _inptValue = input.ToString();
            _value = input;
            _isValid = true;
        }

        private string _inptValue;
        private T _value;
        private bool _isValid;

        public static implicit operator string(EnumInputData<T> input) => input.ToString();
        public static implicit operator EnumInputData<T>(string input) => new EnumInputData<T>(input);

        public static implicit operator T(EnumInputData<T> input) => input._value;
        public static implicit operator EnumInputData<T>(T input) => new EnumInputData<T>(input);

        /// <summary>
        /// Return value string.Empty
        /// </summary>
        public static readonly EnumInputData<T> Empty = new EnumInputData<T> { _inptValue = string.Empty };

        public static void Parse(string input, out EnumInputData<T> output)
        {
            if (TryParse(input, out EnumInputData<T> result))
            {
                output = result;
            }
            else
            {
                if (input == null)
                    throw new ArgumentException(
                        nameof(input), AvisosResx.NaoPodeSerNulo);
                else
                    throw new ArgumentException(
                        nameof(input), AvisosResx.FormatoInvalido);
            }
        }

        public static bool TryParse(string input, out EnumInputData<T> output)
        {
            input = input?.Trim();
            bool result = Enum.TryParse(input, true, out T value);
            output = new EnumInputData<T>
            {
                _isValid = result,
                _inptValue = result ? value.ToString() : input,
                _value = value
            };

            return result;
        }

        public bool IsValid() => _isValid;

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _inptValue;
        }

        public override int GetHashCode()
        {
            return $"{_inptValue}:{GetType()}".GetHashCode();
        }

        public bool Equals(EnumInputData<T> other)
        {
            return _inptValue == other._inptValue;
        }

        public bool Equals(T other)
        {
            return _inptValue == other.ToString();
        }

        public override bool Equals(object obj)
        {
            return (obj is EnumInputData<T> typeA && Equals(typeA))
                || (obj is T typeB && Equals(typeB));
        }

        public int CompareTo(EnumInputData<T> other)
        {
            return _inptValue.CompareTo(other._inptValue);
        }

        public int CompareTo(T other)
        {
            return _inptValue.CompareTo(other.ToString());
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 1;
            }

            if (obj is EnumInputData<T> typeA)
            {
                return CompareTo(typeA);
            }

            if (obj is T typeB)
            {
                return CompareTo(typeB);
            }

            throw new ArgumentException(
                nameof(obj), AvisosResx.TipoInvalido);
        }

        public static bool operator ==(EnumInputData<T> left, EnumInputData<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EnumInputData<T> left, EnumInputData<T> right)
        {
            return !(left == right);
        }

        public static bool operator >(EnumInputData<T> left, EnumInputData<T> right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(EnumInputData<T> left, EnumInputData<T> right)
        {
            return left.CompareTo(right) == -1;
        }

        #region IConvertible implementation

        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        /// <internalonly/>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return _inptValue;
        }

        /// <internalonly/>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_inptValue);
        }

        /// <internalonly/>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_inptValue);
        }

        /// <internalonly/>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_inptValue);
        }

        /// <internalonly/>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_inptValue);
        }

        /// <internalonly/>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_inptValue);
        }

        /// <internalonly/>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_inptValue);
        }

        /// <internalonly/>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_inptValue);
        }

        /// <internalonly/>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_inptValue);
        }

        /// <internalonly/>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_inptValue);
        }

        /// <internalonly/>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_inptValue);
        }

        /// <internalonly/>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_inptValue);
        }

        /// <internalonly/>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_inptValue);
        }

        /// <internalonly/>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_inptValue);
        }

        /// <internalonly/>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_inptValue);
        }

        /// <internalonly/>
        object IConvertible.ToType(System.Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(this, type, provider);
        }

        #endregion
    }
}