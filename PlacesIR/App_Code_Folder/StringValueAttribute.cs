using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlacesIR
{
    // Summary:
    //     Defines an attribute containing a string representation of the member.
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StringValueAttribute : Attribute
    {
        // Summary:
        //     Creates a new string value attribute with the specified text.
        public StringValueAttribute(string text) 
        {
            Text = text;
        }

        // Summary:
        //     The text which belongs to this member.
        public string Text { get; set; }
    }
}