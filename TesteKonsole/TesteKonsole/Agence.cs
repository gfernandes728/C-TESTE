using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteAgence.Models
{
    public class Agence
    {
        public List<AgenceModel> listaAgence = new List<AgenceModel>();

        public void CriaMarcacao(AgenceModel agenceModel)
        {
            TimeSpan tsEntrada = agenceModel._escala.Entrada;
            TimeSpan tsSaidaAlmoco = agenceModel._escala.SaidaAlmoco;
            TimeSpan tsRetornoAlmoco = agenceModel._escala.RetornoAlmoco;
            TimeSpan tsSaida = agenceModel._escala.Saida;

            TimeSpan tsDefault = getTimeSpanDefault("08:30", "12:30", "14:00", "18:00");
            TimeSpan tsMarcacao = getTimeSpan(tsEntrada, tsSaidaAlmoco, tsRetornoAlmoco, tsSaida);

            Escala escala = new Escala();
            escala.Entrada = tsEntrada;
            escala.SaidaAlmoco = tsSaidaAlmoco;
            escala.RetornoAlmoco = tsRetornoAlmoco;
            escala.Saida = tsSaida;

            var tipoDeOcorrencia = EnumTiposDeOcorrencias.Normal;
            if (tsDefault != tsMarcacao)
            {
                tipoDeOcorrencia = getTipoDeOcorrencia("08:30", tsEntrada);
                tipoDeOcorrencia = (tipoDeOcorrencia == EnumTiposDeOcorrencias.Normal) ? getTipoDeOcorrencia("18:00", tsSaida) : tipoDeOcorrencia;
                tipoDeOcorrencia = (tipoDeOcorrencia == EnumTiposDeOcorrencias.Normal) ? getTipoDeOcorrenciaAlmoco(tsSaidaAlmoco, tsRetornoAlmoco) : tipoDeOcorrencia;
            }

            AgenceModel getAgenceModel = new AgenceModel();
            getAgenceModel._escala = escala;
            getAgenceModel._horasGeradas = tsMarcacao;
            if (tipoDeOcorrencia != EnumTiposDeOcorrencias.Normal)
            {
                getAgenceModel._tipo = tipoDeOcorrencia;
            }
            listaAgence.Add(getAgenceModel);

            IList<DateTime> marcacoes = new List<DateTime>();
            marcacoes.Add(Convert.ToDateTime(TimeSpan.Parse("08:30")));
            marcacoes.Add(Convert.ToDateTime(TimeSpan.Parse("12:30")));
            marcacoes.Add(Convert.ToDateTime(TimeSpan.Parse("14:00")));
            marcacoes.Add(Convert.ToDateTime(TimeSpan.Parse("18:00")));

            //getAgenceModel.Interpretar(marcacoes, escala);
        }

        private EnumTiposDeOcorrencias getTipoDeOcorrencia(string sDefault, TimeSpan tsMarcacao, string sTipoAlmoco = "HoraExtra")
        {
            TimeSpan tsDefault = TimeSpan.Parse(sDefault);
            int iResult = TimeSpan.Compare(tsDefault, tsMarcacao);

            switch (sDefault)
            {
                case "08:30":
                    if (iResult < 0) // ex: 08:30 (default) < 09:30 (marcacao)
                        return EnumTiposDeOcorrencias.EntradaEmAtraso;
                    else if (iResult > 0) // ex: 08:30 (default) > 07:30 (marcacao)
                        return EnumTiposDeOcorrencias.HoraExtraEntrada;
                    break;
                case "12:30":
                    if (iResult < 0 && sTipoAlmoco == "HoraExtra") // ex: 12:30 (default) < 13:00 (marcacao)
                        return EnumTiposDeOcorrencias.HoraExtraIntervalo;
                    else if (iResult > 0 && sTipoAlmoco == "IntervaloIrregular") // ex: 12:30 (default) > 11:30 (marcacao)
                        return EnumTiposDeOcorrencias.IntervaloIrregular;
                    break;
                case "14:00":
                    if (iResult < 0 && sTipoAlmoco == "IntervaloIrregular") // ex: 14:00 (default) < 15:00 (marcacao)
                        return EnumTiposDeOcorrencias.IntervaloIrregular;
                    else if (iResult > 0 && sTipoAlmoco == "HoraExtra") // ex: 14:00 (default) > 13:30 (marcacao)
                        return EnumTiposDeOcorrencias.HoraExtraIntervalo;
                    break;
                case "18:00":
                    if (iResult < 0) // ex: 18:00 (default) < 19:00 (marcacao)
                        return EnumTiposDeOcorrencias.HoraExtraSaida;
                    else if (iResult > 0) // ex: 18:00 (default) > 17:00 (marcacao)
                        return EnumTiposDeOcorrencias.SaidaAntecipada;
                    break;
            }

            return EnumTiposDeOcorrencias.Normal;
        }

        private EnumTiposDeOcorrencias getTipoDeOcorrenciaAlmoco(TimeSpan tsSaidaAlmoco, TimeSpan tsRetornoAlmoco)
        {
            TimeSpan tsDefault = DateTime.Parse("14:00").Subtract(DateTime.Parse("12:30"));
            TimeSpan tsAlmoco = tsRetornoAlmoco.Subtract(tsSaidaAlmoco);

            if (tsDefault == tsAlmoco)
                return EnumTiposDeOcorrencias.Normal;

            string sTipoAlmoco = (tsDefault < tsAlmoco) ? "IntervaloIrregular" : "HoraExtra";

            var tipoDeOcorrencia = getTipoDeOcorrencia("12:30", tsSaidaAlmoco, sTipoAlmoco);
            return (tipoDeOcorrencia == EnumTiposDeOcorrencias.Normal) ? getTipoDeOcorrencia("14:00", tsRetornoAlmoco, sTipoAlmoco) : tipoDeOcorrencia;
        }

        private TimeSpan getTimeSpanDefault(string sEntrada, string sSaidaAlmoco, string sRetornoAlmoco, string sSaida)
        {
            TimeSpan ts1 = DateTime.Parse(sSaidaAlmoco).Subtract(DateTime.Parse(sEntrada));
            TimeSpan ts2 = DateTime.Parse(sSaida).Subtract(DateTime.Parse(sRetornoAlmoco));
            return ts1 + ts2;
        }

        private TimeSpan getTimeSpan(TimeSpan tsEntrada, TimeSpan tsSaidaAlmoco, TimeSpan tsRetornoAlmoco, TimeSpan tsSaida)
        {
            TimeSpan ts1 = tsSaidaAlmoco.Subtract(tsEntrada);
            TimeSpan ts2 = tsSaida.Subtract(tsRetornoAlmoco);
            return ts1 + ts2;
        }

    }
}