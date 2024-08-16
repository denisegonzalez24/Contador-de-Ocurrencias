using System;
using System.Collections.Generic;

namespace WiW
{

	[Serializable]
	public class Dato
	{
		public int ocurrencia { get; set; }
		public string texto { get; set; } // String of symbols

		public string descripcion { get; set; } // String of symbols


        public Dato(int ocurrencia, string texto)
        {
            this.ocurrencia = ocurrencia;
            this.texto = texto;
        }

        public Dato(int ocurrencia, string texto, string descripcion)
		{
			this.ocurrencia = ocurrencia;
			this.texto = texto;
			this.descripcion = descripcion;
		}



		public override string ToString()
		{
			if (texto != null)
			{

				return "(" + ocurrencia + ") " + texto;

			}
			else
			{

				return "";
			}
		}

	}
}