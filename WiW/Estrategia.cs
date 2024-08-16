
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;

namespace WiW
{
	//C:\Users\usuario\Desktop\SharpOcurrencias\WiW\datasets\preguntas.csv
	public class Estrategia
	{
		public string Consulta1(List<string> datos)
		{
//			Dictionary<string, int> contadorOcurrencias = new Dictionary<string, int>();
//			string s = "";
//			for (int i =0; i< datos.Count; i++  )
//			{
//
//				if (contadorOcurrencias.ContainsKey(datos[i]))
//				{
//					contadorOcurrencias[datos[i]]++;
//				}
//				else
//				{
//					contadorOcurrencias.Add(datos[i], 1);
//				}
//
//			}
//			s = contadorOcurrencias.Count.ToString();
//			return s;
			
			
			
			
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();


			List<Dato> collectedHeap = new List<Dato>();
			BuscarConHeap(datos, 5, collectedHeap);

			stopwatch.Stop();

			string tiempoHeap = string.Format("{0}",stopwatch.ElapsedMilliseconds);

			stopwatch.Restart();
			List<Dato> collectedOtro = new List<Dato>();
			BuscarConOtro(datos,5 , collectedOtro);
			stopwatch.Stop();
			string tiempoOtro = string.Format("{0}", stopwatch.ElapsedMilliseconds);

			return string.Format( "Tiempo BuscarConHeap: {0} ms\nTiempo BuscarConOtro: {1} ms", tiempoHeap, tiempoOtro);
			
			
		}

		
		// Consulta2: Retornar el camino a la hoja más izquierda de la Heap
		public string Consulta2(List<string> datos)
		{
			Dictionary<string, int> contadorOcurrencias = new Dictionary<string, int>();
			
			for (int i =0; i< datos.Count; i++  )
			{
				
				if (contadorOcurrencias.ContainsKey(datos[i]))
				{
					contadorOcurrencias[datos[i]]++;
				}
				else
				{
					contadorOcurrencias.Add(datos[i], 1);
				}
			}
			
			
			Dato[] arrayHeap = new Dato[contadorOcurrencias.Count];
			int index = 0;
			foreach (var item in contadorOcurrencias)
			{
				arrayHeap[index++] = new Dato(item.Value, item.Key);
			}
			MaxHeap heap = new MaxHeap(arrayHeap);
			
			int posicion = 1; //comienza en la raiz
			int hijo_izq;
			string camino = "";
			
			while (true)
			{
				camino += heap.Heap[posicion].texto; // Agregar el valor del nodo actual al camino
				camino += ":\n";

				// Calcular el índice del hijo izquierdo
				hijo_izq = 2 * posicion;

				// Verificar si el hijo izquierdo está dentro del rango de la matriz del heap
				if (hijo_izq <= heap.Tamaño)
				{
					// Mover al hijo izquierdo y continuar el camino
					posicion = hijo_izq;
				}
				else
				{
					// Si no hay hijo izquierdo, terminar el bucle
					break;
				}
			}

			return camino;
			
			
		}
		
		// Consulta3: Retornar los datos de la Heap con niveles explícitos
		public string Consulta3(List<string> datos)
		{
			List<Dato> heap = new List<Dato>();
			BuscarConHeap(datos, 5, heap);

			string result = "";
			int nivelAnterior = -1;

			for (int i = 0; i < heap.Count; i++)
			{
				// Calcular el nivel actual del nodo
				int nivelActual = (int)Math.Floor(Math.Log(i + 1, 2));

				// Si cambiamos de nivel, imprimir el encabezado del nuevo nivel
				if (nivelActual != nivelAnterior)
				{
					result += "\nNivel " + nivelActual + ":\n";
					nivelAnterior = nivelActual;
				}

				// Imprimir el dato junto con su nivel
				result += heap[i].texto + " - Nivel: " + nivelActual + "\n";
			}

			return result;
			
		}
		
		
		
		
		public List<Dato> BuscarConOtro(List<string> datos, int cantidad, List<Dato> collected)
		{
//			//cola para contar las ocurrencias de cada cadena en la lista de datos
//			Cola<Dato> c = new Cola<Dato>();
//
			Dictionary<string, int> contadorOcurrencias = new Dictionary<string, int>();
			foreach (var dato in datos)
			{
				if (contadorOcurrencias.ContainsKey(dato))
				{
					contadorOcurrencias[dato]++;
				}
				else
				{
					contadorOcurrencias[dato] = 1;
				}
			}
			

			// Ordenar el diccionario por el número de ocurrencias y tomar los 'cantidad' primeros elementos
			var mayorOcurrencias = contadorOcurrencias.OrderByDescending(key => key.Value).Take(cantidad);

			// Llenar la lista collected con los elementos encontrados
			collected.Clear();
			foreach (var key in mayorOcurrencias)
			{
				collected.Add(new Dato(key.Value, key.Key));
			}
			return collected;
		}

		
		
		public List<Dato> BuscarConHeap(List<string> datos, int cantidad, List<Dato> collected)
		{
			Dictionary<string, int> contadorOcurrencias = new Dictionary<string, int>();
			
			for (int i =0; i< datos.Count; i++  )
			{
				
				if (contadorOcurrencias.ContainsKey(datos[i]))
				{
					contadorOcurrencias[datos[i]]++;
				}
				else
				{
					contadorOcurrencias.Add(datos[i], 1);
				}
			}
//			for(int i =0; i < contadorOcurrencias.Count; i++){
//				Debug.WriteLine( contadorOcurrencias.ElementAt(i));
//			}
			
			Dato[] arrayHeap = new Dato[contadorOcurrencias.Count];
			int posicion = 0;
			foreach (var item in contadorOcurrencias)
			{
				arrayHeap[posicion++] = new Dato(item.Value, item.Key);
			}
			MaxHeap maxHeap = new MaxHeap(arrayHeap);
			
			for(int i = 0; i < cantidad && !maxHeap.esVacio(); i++)
			{
				collected.Add(maxHeap.retornarMax()); // Agregar el máximo elemento (raíz del Max-Heap)
			}
			
			return collected;

		}
	}
}