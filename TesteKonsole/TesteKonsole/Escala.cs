using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TesteAgence.Models
{
    public class Escala
    {
        public TimeSpan Entrada { get; set; }
        public TimeSpan Saida { get; set; }
        public TimeSpan SaidaAlmoco { get; set; }
        public TimeSpan RetornoAlmoco { get; set; }

    }
}