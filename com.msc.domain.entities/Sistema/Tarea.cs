using com.msc.domain.Reference;
using System;
using System.Text.RegularExpressions;

namespace com.msc.domain.entities.Sistema
{
    public class Tarea : AggregateRoot
    {
        private string _titulo;

        private string _descripcion;

        private bool _completado;

        public string Titulo
        {
            get
            {
                return _titulo;
            }
            private set
            {
                string validate;

                if (string.IsNullOrEmpty(value)) validate = "El titulo es un campo obligatorio";
                else if (value.Length > 30) validate = string.Format("El titulo no puede tener mas de {0} caracteres", "30");
                else if (!Regex.IsMatch(value, @"^[a-zA-Z0-9 ]+$")) validate = string.Format("El titulo solo puede contener los siguientes caracteres: {0}", "a-zA-Z0-9 ");
                else validate = string.Empty;

                if (!string.IsNullOrEmpty(validate))
                    throw new ArgumentException(validate);
                else _titulo = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            private set
            {
                string validate;

                if (string.IsNullOrEmpty(value)) validate = "La descripcion es un campo obligatorio";
                else if (value.Length > 255) validate = string.Format("La descripcion no puede tener mas de {0} caracteres", "255");
                else if (!Regex.IsMatch(value, @"^[a-zA-Z0-9 áéíóúAÁÉÍÓÚñÑ]+$")) validate = string.Format("La descripcion solo puede contener los siguientes caracteres: {0}", "a-zA-Z0-9 áéíóúAÁÉÍÓÚñÑ");

                else validate = string.Empty;

                if (!string.IsNullOrEmpty(validate))
                    throw new ArgumentException(validate);
                else _descripcion = value;
            }
        }

        public bool Completado
        {
            get
            {
                return _completado;
            }
            private set
            {
                _completado = value;
            }
        }

        public Tarea(int id, string titulo, string descripcion, bool completado) : base(id)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Completado = completado;
        }

    }
}
