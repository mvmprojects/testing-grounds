using System;
using System.Collections.Generic;
using System.Text;

namespace TestingGrounds
{
    public static class SimpleConstants
    {
        // using const instead of readonly comes with certain dangers
        // if the const is used outside of its own assembly
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}
