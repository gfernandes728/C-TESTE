using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace TesteAgence.Models
{
    public class AgenceModel : IInterpretarMarcacoes
    {
        public Escala _escala { get; set; }

        public EnumTiposDeOcorrencias _tipo { get; set; }

        public TimeSpan _horasGeradas { get; set; }

        EnumTiposDeOcorrencias IOcorrencia.Tipo { get; set; }

        TimeSpan IOcorrencia.HorasGeradas { get; set; }

        IList<IOcorrencia> IInterpretarMarcacoes.Interpretar(IList<DateTime> marcacoes, Escala escala)
        {
            throw new NotImplementedException();
        }
    }
}