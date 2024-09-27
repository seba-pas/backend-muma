using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Application.Common
{
    public class OperationResult<T>
    {
        private List<string> Errors { get; set; } = new List<string>();

        internal void AddError(string errorMessage) => this.Errors.Add((errorMessage ?? "").Trim());

        public bool Success => !this.Errors.Any();

        public List<string> GetErrorsList() => this.Errors;

        private T Result;

        internal void SetResult(T result) => this.Result = result;

        public T GetResult() => this.Result;


    }
}
