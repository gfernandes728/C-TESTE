using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteAgence.Models
{
    public interface IOcorrencia
    {
        EnumTiposDeOcorrencias Tipo { get; set; }
        TimeSpan HorasGeradas { get; set; }
    }

}
