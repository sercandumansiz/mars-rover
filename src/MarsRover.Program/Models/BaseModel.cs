using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Program.Models
{
    public class BaseModel<TModel>
    {
        public BaseModel()
        {
            Errors = new List<string>();
        }
        public bool HasError => Errors.Any();
        public List<string> Errors { get; set; }
        public TModel Data { get; set; }
    }
}