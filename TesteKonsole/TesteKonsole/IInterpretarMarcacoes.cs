using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteAgence.Models
{
    public interface IInterpretarMarcacoes : IOcorrencia
    {
        IList<IOcorrencia> Interpretar(IList<DateTime> marcacoes, Escala escala);
    }

}
