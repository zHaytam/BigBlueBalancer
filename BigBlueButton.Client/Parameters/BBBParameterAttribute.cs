using System;

namespace BigBlueButton.Client.Parameters
{
    public class BBBParameterAttribute : Attribute
    {
        public string Name { get; set; }
        public bool Required { get; set; }

        public BBBParameterAttribute()
        {

        }

        public BBBParameterAttribute(string name, bool required = false)
        {
            Name = name;
            Required = required;
        }
    }
}
