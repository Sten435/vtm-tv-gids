using System;

namespace Domein
{
    public class ProductieLand
    {
        private string _code;

        public string Code
        {
            get => _code;
            set => _code = value;
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public ProductieLand(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}