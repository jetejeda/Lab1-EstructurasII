using System;
using System.Collections.Generic;
using System.Text;

namespace PruebasHuffman
{
    public class Huffman
    {
        private ComponentesDeCadaNodo raiz;
        public void EnsambladoDeHuffman(List<ComponentesDeCadaNodo> ListaDeCaracteres)
        {

            while (ListaDeCaracteres.Count > 1)
            {
                var nuevoNodo = new ComponentesDeCadaNodo();
                nuevoNodo = sumaDeProbabilidades(ListaDeCaracteres[0], ListaDeCaracteres[1]);
                ListaDeCaracteres.RemoveAt(0);
                ListaDeCaracteres.RemoveAt(0);//Cero aqui porque al momento de borrar la que estaba antes en 0, la que estaba en 1 pasa a ser la 0



                var nuevaPosicionNodoHuffamn = ListaDeCaracteres.FindIndex(x => x.datosDelCaracter.probabilidad >=nuevoNodo.datosDelCaracter.probabilidad);
                if (nuevaPosicionNodoHuffamn == -1)
                {//Si regresa -1 es porque no hay ninguno mayor que el que se desea incertar, por lo que simplemente se agrega al final
                    ListaDeCaracteres.Add(nuevoNodo);
                }
                else
                {
                    ListaDeCaracteres.Insert(nuevaPosicionNodoHuffamn, nuevoNodo);
                }
                
                //Tengo que recorrer mi vector hasta encontrar la posicion exacta donde tiene que ir

            }
            raiz = ListaDeCaracteres[0];
            ListaDeCaracteres.Clear();
            ListaDeCaracteres = null;

        }



        ComponentesDeCadaNodo sumaDeProbabilidades(ComponentesDeCadaNodo nodo1, ComponentesDeCadaNodo nodo2)
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
            }//cout << nNodo->sInformacion.iNumero << "-";

        }
    }

}
