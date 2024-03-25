using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ResultDTO<T>
    {
        public List<string> Errors { get; set; } = new();

        public T Data { get; set; }

        [JsonIgnore]
        public bool IsValid { get => Errors.Count == 0; }


        public ResultDTO(T data)
        {
            Data = data;    
        }

        public ResultDTO(List<string> errors)
        {
            Errors = errors;
        }

        public ResultDTO(string error)
        {
            Errors.Add(error);
        }
       
    }
}
