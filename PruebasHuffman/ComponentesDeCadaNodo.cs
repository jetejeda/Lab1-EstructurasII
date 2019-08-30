using System;
using System.Collections.Generic;
using System.Text;

namespace PruebasHuffman
{
    public class ComponentesDeCadaNodo
    {
        public ComponentesLecturaInicial datosDelCaracter { get; set; }
        public ComponentesDeCadaNodo hijoIzquierdo { get; set; }
        public ComponentesDeCadaNodo hijoDerecho { get; set; }
    }
}
