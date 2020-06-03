using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.BaseTypes
{
    public class DataTypes
    {
        private Dictionary<string, Type> _typeMappings = new Dictionary<string, Type>
        {
            {Int, typeof(int)},
            {String, typeof(string)},
            {DateTime, typeof(DateTime)},
            {Long, typeof(long)},
            {Guid, typeof(Guid)},
            {Bool, typeof(bool)}
        };

        public const string Int = "I";
        public const string String = "S";
        public const string DateTime = "D";
        public const string Long = "L";
        public const string Guid = "G";
        public const string Bool = "B";

        public Type GetDataType(string type)
        {
            var result = _typeMappings.TryGetValue(type, out var dataType);

            if (result)
            {
                return dataType;
            }

            throw new Exception($"Data type not found for key '{type}'");
        }
    }
}
