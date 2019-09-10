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
            #region Lectura del archivo, contar frecuencias y calcular probabilidades

            const int bufferLength = 100;
          
            var CantidadTotalDeCaracteres = 0;
            List<ComponentesLecturaInicial> listaDeCaracteres = new List<ComponentesLecturaInicial>();

            //cambiar "PruebaLecturas por lo que sí se usará"
            using (var stream = new FileStream("PruebaLecturas.txt", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {                   
                    CantidadTotalDeCaracteres = Convert.ToInt32(reader.BaseStream.Length);
                    var byteBurffer = new byte[bufferLength];
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        byteBurffer = reader.ReadBytes(bufferLength);
                        for (int i = 0; i < byteBurffer.Length; i++)
                        {
                            ComponentesLecturaInicial caracterParaLista = new ComponentesLecturaInicial();
                            caracterParaLista = listaDeCaracteres.Find(x => x.caracter == byteBurffer[i]);
                            if (caracterParaLista == null)
                            {
                                caracterParaLista = new ComponentesLecturaInicial();
                                caracterParaLista.caracter = byteBurffer[i];
                                caracterParaLista.frecuencia = 1;
                                listaDeCaracteres.Add(caracterParaLista);
                            }
                            else
                            {
                                caracterParaLista.frecuencia++;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < listaDeCaracteres.Count; i++)
            {
                listaDeCaracteres[i].probabilidad = Convert.ToDouble(listaDeCaracteres[i].frecuencia) / Convert.ToDouble( CantidadTotalDeCaracteres);
            }

            #endregion


            #region armar nodos a insertar en el árbol

            listaDeCaracteres.Sort((comp1, comp2) => comp1.probabilidad.CompareTo(comp2.probabilidad));
            var nodosParaHuffman = new List<ComponentesDeCadaNodo>();           

            for (int i = 0; i < listaDeCaracteres.Count; i++)
            {
                var nodoTransicion = new ComponentesDeCadaNodo();
                nodoTransicion.datosDelCaracter = listaDeCaracteres[i];
                nodosParaHuffman.Add(nodoTransicion);
            }

            #endregion


            #region crear árbol y prefijos

            listaDeCaracteres = null;
            var algoritmoDeHuffman = new Huffman();
            algoritmoDeHuffman.EnsambladoDeHuffman(nodosParaHuffman);
            algoritmoDeHuffman.llenarDictDePrefijos();
            #endregion

            #region comprimir archivo
            using (var stream = new FileStream("PruebaLecturas.txt", FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {    
                    using(var writeStream = new FileStream("PruebaLecturasComp.huff", FileMode.OpenOrCreate))
                    {
                        using (var writer = new BinaryWriter(writeStream))
                        {
                            var byteBuffer = new byte[bufferLength];
                           

                            //vacío el buffer y empiezo a escribir el texto codificado
                            byteBuffer = new byte[bufferLength];
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                byteBuffer = algoritmoDeHuffman.codificarBuffer(reader.ReadBytes(bufferLength), bufferLength);
                                writer.Write(byteBuffer);
                            }

                             //meto el diccionario de prefijos al buffer y lo escribo 
                            //byteBuffer = algoritmoDeHuffman.recuperarDictDePrefijos();//completar este método
                            //writer.Write(byteBuffer);
                        }
                    }                      
                }
            }
            #endregion
        }
    }

}
