using System;

namespace com.msc.domain.Reference
{
    public class Entity
    {
        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }
            private set
            {
                var validate = (
                    value < 0 ? "El id debe ser un entero positivo" : string.Empty
                    );
                if (!string.IsNullOrEmpty(validate))
                    throw new ArgumentException(validate);
                else _id = value;
            }
        }

        public Entity(int id)
        {
            Id = id;
        }
    }
}
