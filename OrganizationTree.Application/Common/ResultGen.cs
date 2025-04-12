using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Common
{
    public class ResultGen<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }

        protected ResultGen(bool isSuccess, T? value, string? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        // Успех
        public static ResultGen<T> Success(T value) => new(true, value, null);

        // Ошибка
        public static ResultGen<T> Failure(string error) => new(false, default, error);

        // Оператор неявного преобразования (для удобства)
        public static implicit operator ResultGen<T>(T value) => Success(value);
    }
}
