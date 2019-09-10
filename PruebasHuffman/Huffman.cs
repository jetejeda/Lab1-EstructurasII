using System;
using System.Collections.Generic;
using System.Text;


namespace PruebasHuffman
{
    public class Huffman
    {
        
        private ComponentesDeCadaNodo raiz;
        private Dictionary<byte, string> diccionarioDePrefijos = new Dictionary<byte, string>();


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

        ComponentesDeCadaNodo armarNodo(ComponentesDeCadaNodo nodoMenor, ComponentesDeCadaNodo nodoMayor)
        {

            var padre = new ComponentesDeCadaNodo();
            var informacionNuevoPadre = new ComponentesLecturaInicial();
            padre.datosDelCaracter = informacionNuevoPadre;
            padre.hijoIzquierdo = nodoMenor;
            padre.hijoDerecho = nodoMayor;
            padre.datosDelCaracter.probabilidad = nodoMenor.datosDelCaracter.probabilidad + nodoMayor.datosDelCaracter.probabilidad;
            return padre;
        }

        public Dictionary<byte, string> llenarDictDePrefijos()
        {
            asignacionDeCodigoPrefijo(raiz, "");
            ordenarDiccionario();
            return diccionarioDePrefijos;
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
                diccionarioDePrefijos.Add(NodoActual.datosDelCaracter.caracter, NodoActual.datosDelCaracter.codigoPrefijo);
            }

        }

        void ordenarDiccionario()
        {
            Dictionary<string, byte> dictParaOrdenar = new Dictionary<string, byte>();
            //implementar orden de diccionario para eficiencia por length de llave
        }

        public byte[] recuperarDictDePrefijos (int bufferLenght)
        {
            byte[] bufferCodificado = new byte[bufferLenght];
            return null;
        }

        public byte[] codificarBuffer(byte[] bufferDescodificado, int bufferLenght)
        {
            byte[] bufferCodificado = new byte[bufferLenght];
            /*Este método tiene un problema de lógica, sí codifica y al final escribe, pero el problema es
             en la descompresión, al pasar de binario a decimal, para hacer el proceso inverso pierdo los 
             ceros iniciales, al pasar de decimal a binario ya no voy a tener los ceros del prefijo
             y por ende no sabré cuándo termina la cadena, tenemos que platicar esto*/
            for (int i = 0; i < bufferDescodificado.Length && i < bufferLenght; i++)
            {
                bufferCodificado[i] = Convert.ToByte(binarioADecimal(diccionarioDePrefijos[bufferDescodificado[i]]));
            }           


            return bufferCodificado;
        }

        public byte[] decodificarBuffer(byte[] buffercodificado, int bufferLenght)
        {
            //implementar lógica
            return null;
        }


        int binarioADecimal(string codPrefijo)
        {
            char[] codPrefijoArr = codPrefijo.ToCharArray();
          
            Array.Reverse(codPrefijoArr);
            int Decimal = 0; 

            for(int i = 0; i < codPrefijoArr.Length; i++)
            {
                if (codPrefijoArr[i] == '1')
                {
                   
                    Decimal += (int)Math.Pow(2, i);
                }
            }
            return Decimal;
        }

        string decimalABinario (string Decimal)
        {
            string binario = "0"; 



            return binario;
        }

        
    }

}
