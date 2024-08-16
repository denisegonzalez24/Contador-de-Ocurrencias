/*
 * Created by SharpDevelop.
 * User: usuario
 * Date: 9/6/2024
 * Time: 20:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace WiW
{
	/// <summary>
	/// Description of MaxHeap.
	/// </summary>
	public class MaxHeap
	{
		//implementar filtrado hacia abajo la ser maxheap recursivo
		private Dato[] heap;
		private int tamaño;

		// constructor
		public MaxHeap(Dato[] array)
		{
			tamaño = array.Length;
			heap = new Dato[tamaño + 1]; // tamaño del heap comienza desde el índice 1
			Array.Copy(array, 0, heap, 1, tamaño); // copio los elementos del array dado al array, comenzando desde el índice 1

			ConstruirHeap();
		}
		
		public MaxHeap(List<string> list)
		{
			tamaño = list.Count;
			heap = new Dato[tamaño + 1]; // tamaño del heap comienza desde el índice 1
			string[] array = list.ToArray();
			Array.Copy(array, 0, heap, 1, tamaño);

			ConstruirHeap();
		}

		// metodo para construir el MinHeap
		private void ConstruirHeap()
		{
			// itero sobre los nodos que tienen hijos, comenzando desde la última posición con hijos
			for (int i = tamaño / 2; i >= 1; i--) {
				ajustarHeap(i); // Llamar al método AjustarHeap en cada nodo para garantizar que el subárbol sea un heap válido
			}
		}

		// ajustar el heap desde el nodo dado hacia abajo (para mantener la propiedad del heap)
		
		private void ajustarHeap(int indice)
		{
			int mayor = indice;         // índice del mayor valor como el índice dado
			int izquierdo = 2 * indice; // hijo izquierdo
			int derecho = 2 * indice + 1;   // hijo derecho

			if (izquierdo <= tamaño && heap[izquierdo].ocurrencia > heap[mayor].ocurrencia) {
				mayor = izquierdo;
			}

			if (derecho <= tamaño && heap[derecho].ocurrencia > heap[mayor].ocurrencia) {
				mayor = derecho;
			}

			if (mayor != indice) {
				intercambiar(indice, mayor); // intercambio los valores
				ajustarHeap(mayor);          // llamo a ajustarHeap recursivamente
			}
		}
		


		//metodo para intercambiar dos elementos en el heap
		private void intercambiar(int i, int j)
		{
			Dato auxiliar = heap[i];
			heap[i] = heap[j];
			heap[j] = auxiliar;
		}

		// metodo para imprimir el MaxHeap
		public void ImprimirHeap()
		{
			Console.WriteLine("MeanHeap: ");
			for (int i = 1; i <= tamaño; i++) {
				Console.Write(heap[i] + " ");
			}
			Console.WriteLine();
		}
		
		//metodo para devolver el elemento maximo de la maxheap
		public Dato retornarMax()
		{
			Dato max = heap[1]; 
			heap[1] = heap[tamaño]; 
			tamaño--; 
			ajustarHeap(1); // reorganizar el heap desde la raíz
			return max;

		}
		
		public int Tamaño{get { return tamaño;}}
		
		public Dato[] Heap {get { return heap;}}
	
		private void reorganizarHeap(int indice)
		{
			ajustarHeap(indice);
		}
		
		public bool esVacio()
		{
			return tamaño == 0;
		}
	}
}
