using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class EntidadeBase
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Datacriacao { get; set; } = DateTime.Now;
    }
}
