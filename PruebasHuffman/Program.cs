using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace PruebasHuffman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el texto que quiera comprimir");
            string textoIngresado;
            textoIngresado = Console.ReadLine();
            int contador = 0;
            List<ComponentesLecturaInicial> listaDeCaracteres = new List<ComponentesLecturaInicial>();
            int CantidadTotalDeCaracteres = textoIngresado.Length;
            //tambien cuando ya sean archivos esta se va a tener que aumentar por cada linea
            while (contador < textoIngresado.Length)
            {
                //para que recorra caracter por caracter del string, cuando ya sean archivos tiene que ser anidado porque
                //tiene que hacer esto por cada linea del archivo
                ComponentesLecturaInicial caracterParaLista = new ComponentesLecturaInicial();
                caracterParaLista = listaDeCaracteres.Find(x => x.caracter == textoIngresado[contador]);
                if (caracterParaLista == null)
                {
                    caracterParaLista = new ComponentesLecturaInicial();
                    caracterParaLista.caracter = textoIngresado[contador];
                    caracterParaLista.frecuencia = 1;
                    listaDeCaracteres.Add(caracterParaLista);
                }
                else
                {
                    caracterParaLista.frecuencia++;
                }
                contador++;
            }
            //2 opciones para el calculo de las probabilidades....
            //o lo vamos calculando por cada vez que entre a cada condicion del ciclo que tenemos arriba o
            //cuando ya esten todos los datos en la lista recorrer cada posicion de la lista para sacar la probabilidad
            //yo preferiria el 2do la verdad porque se me hace mas optimo
            contador = 0;
            while (contador < listaDeCaracteres.Count)
            {
                listaDeCaracteres[contador].probabilidad = listaDeCaracteres[contador].frecuencia / CantidadTotalDeCaracteres;
                contador++;
            }
            //una vez leido y con las probabilidades ya se comienza el Huffman :v
            ComponentesDeCadaNodo[] nodosParaHuffman = new ComponentesDeCadaNodo[listaDeCaracteres.Count];
            contador = 0;
            while (contador < listaDeCaracteres.Count)
            {
                ComponentesDeCadaNodo nodoTransicion = new ComponentesDeCadaNodo();
                nodoTransicion.datosDelCaracter = listaDeCaracteres[contador];
                nodosParaHuffman[contador] = nodoTransicion;
                contador++;
            }
            contador = 0;
            //Ahora toca ordenar el vector, ya con el vector ordenado ya lo ponemos en huffman
        }
        public void EnsambladoDeHuffman(ComponentesDeCadaNodo[] vectorDeCaracteres)
        {
            while (vectorDeCaracteres.Length > 1)
            {
                ComponentesDeCadaNodo nuevoNodo = new ComponentesDeCadaNodo();
                nuevoNodo = sumaDeProbabilidades(vectorDeCaracteres[0], vectorDeCaracteres[1]);
                vectorDeCaracteres[0] = null;
                vectorDeCaracteres[1] = null;
                vectorDeCaracteres[0] = nuevoNodo;

            }
        }
        public void OrdenamientoDelVector(ComponentesDeCadaNodo[] vectorAOrdenar)
        {

        }
        public ComponentesDeCadaNodo sumaDeProbabilidades(ComponentesDeCadaNodo nodo1, ComponentesDeCadaNodo nodo2)
        {
            ComponentesDeCadaNodo padre = new ComponentesDeCadaNodo();
            padre.hijoIzquierdo = nodo1;
            padre.hijoDerecho = nodo2;
            padre.datosDelCaracter.probabilidad = nodo1.datosDelCaracter.probabilidad + nodo2.datosDelCaracter.probabilidad;
            return padre;
        }
    }

}
