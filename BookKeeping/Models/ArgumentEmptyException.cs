using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookKeeping.Models
{
    public class ArgumentEmptyException : Exception
    {
        public ArgumentEmptyException() : base("No items found")
        {
        }

        public ArgumentEmptyException(string message) : base(message)
        {
        }

        public ArgumentEmptyException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}