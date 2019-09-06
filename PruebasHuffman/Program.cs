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
            //const int buffersLength = 1000;
            //using (var stream = new FileStream("", FileMode.Open))
            //{
            //    using (var reader = new BinaryReader(stream))
            //    {
            //        using (var writeStream = new FileStream("", FileMode.Open))
            //        {
            //            using (var writer = new BinaryWriter(writeStream))
            //            {
            //                var byteBurffer = new byte[buffersLength];
            //                while (reader.BaseStream.Position != reader.BaseStream.Length)
            //                {
            //                    byteBurffer = reader.ReadBytes(buffersLength);
            //                    writer.Write(byteBurffer);
            //                }
            //            }
            //        }
            //    }
            //}

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
                listaDeCaracteres[contador].probabilidad = Convert.ToDouble(listaDeCaracteres[contador].frecuencia) / Convert.ToDouble( CantidadTotalDeCaracteres);
                contador++;
            }
            //una vez leido y con las probabilidades ya se comienza el Huffman :v
            listaDeCaracteres.Sort((comp1, comp2) => comp1.probabilidad.CompareTo(comp2.probabilidad));
            var nodosParaHuffman = new List<ComponentesDeCadaNodo>();
            contador = 0;
            while (contador < listaDeCaracteres.Count)
            {
                var nodoTransicion = new ComponentesDeCadaNodo();
                nodoTransicion.datosDelCaracter = listaDeCaracteres[contador];
                nodosParaHuffman.Add(nodoTransicion);
                contador++;
            }
            listaDeCaracteres = null;
            var algoritmoDeHuffman = new Huffman();
            algoritmoDeHuffman.EnsambladoDeHuffman(nodosParaHuffman);
            algoritmoDeHuffman.Prefijos();
            contador = 0;
        }



    }

}
