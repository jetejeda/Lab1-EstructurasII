using System;
using System.Collections.Generic;
using System.Text;

namespace PruebasHuffman
{
    public class Huffman
    {
        
        private ComponentesDeCadaNodo raiz;
        private Dictionary<string, byte> diccionarioDePrefijos = new Dictionary<string, byte>();


        public void EnsambladoDeHuffman(List<ComponentesDeCadaNodo> ListaDeCaracteres)
        {

            while (ListaDeCaracteres.Count > 1)
            {
                var nuevoNodo = new ComponentesDeCadaNodo();
                nuevoNodo = armarNodo(ListaDeCaracteres[0], ListaDeCaracteres[1]);
                ListaDeCaracteres.RemoveAt(0);
                ListaDeCaracteres.RemoveAt(0);



                var nuevaPosicionNodoHuffamn = ListaDeCaracteres.FindIndex(x => x.datosDelCaracter.probabilidad >=nuevoNodo.datosDelCaracter.probabilidad);
                if (nuevaPosicionNodoHuffamn == -1)
                {
                    ListaDeCaracteres.Add(nuevoNodo);
                }
                else
                {
                    ListaDeCaracteres.Insert(nuevaPosicionNodoHuffamn, nuevoNodo);
                }

            }
            raiz = ListaDeCaracteres[0];
            ListaDeCaracteres.Clear();
            ListaDeCaracteres = null;

        }

        ComponentesDeCadaNodo armarNodo(ComponentesDeCadaNodo nodo1, ComponentesDeCadaNodo nodo2)
        {

            var padre = new ComponentesDeCadaNodo();
            var informacionNuevoPadre = new ComponentesLecturaInicial();
            padre.datosDelCaracter = informacionNuevoPadre;
            padre.hijoIzquierdo = nodo1;
            padre.hijoDerecho = nodo2;
            padre.datosDelCaracter.probabilidad = nodo1.datosDelCaracter.probabilidad + nodo2.datosDelCaracter.probabilidad;
            return padre;
        }

        public void Prefijos()
        {
            asignacionDeCodigoPrefijo(raiz, "");
        }

        void asignacionDeCodigoPrefijo(ComponentesDeCadaNodo NodoActual, string codigoPrefijo)
        {
            if (NodoActual.hijoDerecho != null && NodoActual.hijoIzquierdo !=null)
            {
                asignacionDeCodigoPrefijo(NodoActual.hijoIzquierdo, codigoPrefijo + "0");
                asignacionDeCodigoPrefijo(NodoActual.hijoDerecho, codigoPrefijo + "1");
            }
            else
            {
                NodoActual.datosDelCaracter.codigoPrefijo = codigoPrefijo;
                diccionarioDePrefijos.Add(NodoActual.datosDelCaracter.codigoPrefijo, NodoActual.datosDelCaracter.caracter);
            }

        }
    }

}
