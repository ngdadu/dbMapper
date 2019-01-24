/* CSVReader - a simple open source C# class library to read CSV data
 * by Andrew Stellman - http://www.stellman-greene.com/CSVReader
 * 
 * StringConverter.cs - Class to convert strings to typed objects
 * 
 * download the latest version: http://svn.stellman-greene.com/CSVReader
 * 
 * (c) 2008, Stellman & Greene Consulting
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of Stellman & Greene Consulting nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY STELLMAN & GREENE CONSULTING ''AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL STELLMAN & GREENE CONSULTING BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

using System;
using System.Collections.Generic;

namespace Com.StellmanGreene.CSVReader
{
    /// <summary>
    /// Static class to convert strings to typed values
    /// </summary>
    public static class StringConverter
    {
        public static Type ConvertString(string value, out object convertedValue)
        {
            BuildTypeMap();
            // First check the whole number types, because floating point types will always parse whole numbers
            // Start with the smallest types
            byte byteResult;
            if (byte.TryParse(value, out byteResult))
            {
                convertedValue = byteResult;
                return typeof(byte);
            }

            short shortResult;
            if (short.TryParse(value, out shortResult))
            {
                convertedValue = shortResult;
                return typeof(short);
            }

            int intResult;
            if (int.TryParse(value, out intResult))
            {
                convertedValue = intResult;
                return typeof(int);
            }

            long longResult;
            if (long.TryParse(value, out longResult))
            {
                convertedValue = longResult;
                return typeof(long);
            }

            ulong ulongResult;
            if (ulong.TryParse(value, out ulongResult))
            {
                convertedValue = ulongResult;
                return typeof(ulong);
            }

            // No need to check the rest of the unsigned types, which will fit into the signed whole number types

            // Next check the floating point types
            float floatResult;
            if (float.TryParse(value, out floatResult))
            {
                convertedValue = floatResult;
                return typeof(float);
            }


            // It's not clear that there's anything that double.TryParse() and decimal.TryParse() will parse 
            // but which float.TryParse() won't
            double doubleResult;
            if (double.TryParse(value, out doubleResult))
            {
                convertedValue = doubleResult;
                return typeof(double);
            }

            decimal decimalResult;
            if (decimal.TryParse(value, out decimalResult))
            {
                convertedValue = decimalResult;
                return typeof(decimal);
            }

            // It's not a number, so it's either a bool, char or string
            bool boolResult;
            if (bool.TryParse(value, out boolResult))
            {
                convertedValue = boolResult;
                return typeof(bool);
            }

            char charResult;
            if (char.TryParse(value, out charResult))
            {
                convertedValue = charResult;
                return typeof(char);
            }

            convertedValue = value;
            return typeof(string);
        }

        /// <summary>
        /// Compare two types and find a type that can fit both of them
        /// </summary>
        /// <param name="typeA">First type to compare</param>
        /// <param name="typeB">Second type to compare</param>
        /// <returns>The type that can fit both types, or string if they're incompatible</returns>
        public static Type FindCommonType(Type typeA, Type typeB)
        {
            // Build the singleton type map (which will rebuild it in a typesafe manner
            // if it's not already built).
            BuildTypeMap();

            if (!typeMap.ContainsKey(typeA))
                return typeof(string);

            if (!typeMap[typeA].ContainsKey(typeB))
                return typeof(string);

            return typeMap[typeA][typeB];
        }


        // Dictionary to map two types to a common type that can hold both of them
        private static Dictionary<Type, Dictionary<Type, Type>> typeMap = null;

        // Locker object to build the singleton typeMap in a typesafe manner
        private static object locker = new object();

        public static void DumpTypeMap()
        {
            foreach (KeyValuePair<Type, Dictionary<Type, Type>> kvp in typeMap)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("// comparing {0}", kvp.Key.Name));
                foreach (KeyValuePair<Type, Type> kvp2 in kvp.Value)
                {
                    System.Diagnostics.Debug.WriteLine(String.Format("AddTypeMap(typeof({0}), typeof({1}), typeof({2}));", kvp.Key.Name, kvp2.Key.Name, kvp2.Value.Name));
                }
            }
        }
        private static void AddTypeMap(Type key1, Type key2, Type value2)
        {
            if (typeMap.ContainsKey(key1))
            {
                Dictionary<Type, Type> v = typeMap[key1];
                v.Add(key2, value2);
            }
            else
            {
                Dictionary<Type, Type> vd = new Dictionary<Type, Type>();
                vd.Add(key2, value2);
                typeMap.Add(key1, vd);
            }
        }
        /// <summary>
        /// Build the singleton type map in a typesafe manner.
        /// This map is a dictionary that maps a pair of types to a common type.
        /// So typeMap[typeof(float)][typeof(uint)] will return float, while
        /// typemap[typeof(char)][typeof(bool)] will return string.
        /// </summary>
        private static void BuildTypeMap()
        {
            lock (locker)
            {
                if (typeMap == null)
                {
                    typeMap = new Dictionary<Type, Dictionary<Type, Type>>();
                    // comparing Byte
                    AddTypeMap(typeof(Byte), typeof(Byte), typeof(Byte));
                    AddTypeMap(typeof(Byte), typeof(Int16), typeof(Int16));
                    AddTypeMap(typeof(Byte), typeof(Int32), typeof(Int32));
                    AddTypeMap(typeof(Byte), typeof(Int64), typeof(Int64));
                    AddTypeMap(typeof(Byte), typeof(UInt64), typeof(UInt64));
                    AddTypeMap(typeof(Byte), typeof(Single), typeof(Single));
                    AddTypeMap(typeof(Byte), typeof(Double), typeof(Double));
                    AddTypeMap(typeof(Byte), typeof(Decimal), typeof(Decimal));
                    AddTypeMap(typeof(Byte), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(Byte), typeof(Char), typeof(String));
                    AddTypeMap(typeof(Byte), typeof(String), typeof(String));
                    // comparing Int16
                    AddTypeMap(typeof(Int16), typeof(Byte), typeof(Int16));
                    AddTypeMap(typeof(Int16), typeof(Int16), typeof(Int16));
                    AddTypeMap(typeof(Int16), typeof(Int32), typeof(Int32));
                    AddTypeMap(typeof(Int16), typeof(Int64), typeof(Int64));
                    AddTypeMap(typeof(Int16), typeof(UInt64), typeof(UInt64));
                    AddTypeMap(typeof(Int16), typeof(Single), typeof(Single));
                    AddTypeMap(typeof(Int16), typeof(Double), typeof(Double));
                    AddTypeMap(typeof(Int16), typeof(Decimal), typeof(Decimal));
                    AddTypeMap(typeof(Int16), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(Int16), typeof(Char), typeof(String));
                    AddTypeMap(typeof(Int16), typeof(String), typeof(String));
                    // comparing Int32
                    AddTypeMap(typeof(Int32), typeof(Byte), typeof(Int32));
                    AddTypeMap(typeof(Int32), typeof(Int16), typeof(Int32));
                    AddTypeMap(typeof(Int32), typeof(Int32), typeof(Int32));
                    AddTypeMap(typeof(Int32), typeof(Int64), typeof(Int64));
                    AddTypeMap(typeof(Int32), typeof(UInt64), typeof(UInt64));
                    AddTypeMap(typeof(Int32), typeof(Single), typeof(Single));
                    AddTypeMap(typeof(Int32), typeof(Double), typeof(Double));
                    AddTypeMap(typeof(Int32), typeof(Decimal), typeof(Decimal));
                    AddTypeMap(typeof(Int32), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(Int32), typeof(Char), typeof(String));
                    AddTypeMap(typeof(Int32), typeof(String), typeof(String));
                    // comparing Int64
                    AddTypeMap(typeof(Int64), typeof(Byte), typeof(Int64));
                    AddTypeMap(typeof(Int64), typeof(Int16), typeof(Int64));
                    AddTypeMap(typeof(Int64), typeof(Int32), typeof(Int64));
                    AddTypeMap(typeof(Int64), typeof(Int64), typeof(Int64));
                    AddTypeMap(typeof(Int64), typeof(UInt64), typeof(UInt64));
                    AddTypeMap(typeof(Int64), typeof(Single), typeof(Single));
                    AddTypeMap(typeof(Int64), typeof(Double), typeof(Double));
                    AddTypeMap(typeof(Int64), typeof(Decimal), typeof(Decimal));
                    AddTypeMap(typeof(Int64), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(Int64), typeof(Char), typeof(String));
                    AddTypeMap(typeof(Int64), typeof(String), typeof(String));
                    // comparing UInt64
                    AddTypeMap(typeof(UInt64), typeof(Byte), typeof(UInt64));
                    AddTypeMap(typeof(UInt64), typeof(Int16), typeof(UInt64));
                    AddTypeMap(typeof(UInt64), typeof(Int32), typeof(UInt64));
                    AddTypeMap(typeof(UInt64), typeof(Int64), typeof(UInt64));
                    AddTypeMap(typeof(UInt64), typeof(UInt64), typeof(UInt64));
                    AddTypeMap(typeof(UInt64), typeof(Single), typeof(Single));
                    AddTypeMap(typeof(UInt64), typeof(Double), typeof(Double));
                    AddTypeMap(typeof(UInt64), typeof(Decimal), typeof(Decimal));
                    AddTypeMap(typeof(UInt64), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(UInt64), typeof(Char), typeof(String));
                    AddTypeMap(typeof(UInt64), typeof(String), typeof(String));
                    // comparing Single
                    AddTypeMap(typeof(Single), typeof(Byte), typeof(Single));
                    AddTypeMap(typeof(Single), typeof(Int16), typeof(Single));
                    AddTypeMap(typeof(Single), typeof(Int32), typeof(Single));
                    AddTypeMap(typeof(Single), typeof(Int64), typeof(Single));
                    AddTypeMap(typeof(Single), typeof(UInt64), typeof(Single));
                    AddTypeMap(typeof(Single), typeof(Single), typeof(Single));
                    AddTypeMap(typeof(Single), typeof(Double), typeof(Double));
                    AddTypeMap(typeof(Single), typeof(Decimal), typeof(Decimal));
                    AddTypeMap(typeof(Single), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(Single), typeof(Char), typeof(String));
                    AddTypeMap(typeof(Single), typeof(String), typeof(String));
                    // comparing Double
                    AddTypeMap(typeof(Double), typeof(Byte), typeof(Double));
                    AddTypeMap(typeof(Double), typeof(Int16), typeof(Double));
                    AddTypeMap(typeof(Double), typeof(Int32), typeof(Double));
                    AddTypeMap(typeof(Double), typeof(Int64), typeof(Double));
                    AddTypeMap(typeof(Double), typeof(UInt64), typeof(Double));
                    AddTypeMap(typeof(Double), typeof(Single), typeof(Double));
                    AddTypeMap(typeof(Double), typeof(Double), typeof(Double));
                    AddTypeMap(typeof(Double), typeof(Decimal), typeof(Decimal));
                    AddTypeMap(typeof(Double), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(Double), typeof(Char), typeof(String));
                    AddTypeMap(typeof(Double), typeof(String), typeof(String));
                    // comparing Decimal
                    AddTypeMap(typeof(Decimal), typeof(Byte), typeof(Decimal));
                    AddTypeMap(typeof(Decimal), typeof(Int16), typeof(Decimal));
                    AddTypeMap(typeof(Decimal), typeof(Int32), typeof(Decimal));
                    AddTypeMap(typeof(Decimal), typeof(Int64), typeof(Decimal));
                    AddTypeMap(typeof(Decimal), typeof(UInt64), typeof(Decimal));
                    AddTypeMap(typeof(Decimal), typeof(Single), typeof(Decimal));
                    AddTypeMap(typeof(Decimal), typeof(Double), typeof(Decimal));
                    AddTypeMap(typeof(Decimal), typeof(Decimal), typeof(Decimal));
                    AddTypeMap(typeof(Decimal), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(Decimal), typeof(Char), typeof(String));
                    AddTypeMap(typeof(Decimal), typeof(String), typeof(String));
                    // comparing Boolean
                    AddTypeMap(typeof(Boolean), typeof(Byte), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(Int16), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(Int32), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(Int64), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(UInt64), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(Single), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(Double), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(Decimal), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(Boolean), typeof(Boolean));
                    AddTypeMap(typeof(Boolean), typeof(Char), typeof(String));
                    AddTypeMap(typeof(Boolean), typeof(String), typeof(String));
                    // comparing Char
                    AddTypeMap(typeof(Char), typeof(Byte), typeof(String));
                    AddTypeMap(typeof(Char), typeof(Int16), typeof(String));
                    AddTypeMap(typeof(Char), typeof(Int32), typeof(String));
                    AddTypeMap(typeof(Char), typeof(Int64), typeof(String));
                    AddTypeMap(typeof(Char), typeof(UInt64), typeof(String));
                    AddTypeMap(typeof(Char), typeof(Single), typeof(String));
                    AddTypeMap(typeof(Char), typeof(Double), typeof(String));
                    AddTypeMap(typeof(Char), typeof(Decimal), typeof(String));
                    AddTypeMap(typeof(Char), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(Char), typeof(Char), typeof(Char));
                    AddTypeMap(typeof(Char), typeof(String), typeof(String));
                    // comparing String
                    AddTypeMap(typeof(String), typeof(Byte), typeof(String));
                    AddTypeMap(typeof(String), typeof(Int16), typeof(String));
                    AddTypeMap(typeof(String), typeof(Int32), typeof(String));
                    AddTypeMap(typeof(String), typeof(Int64), typeof(String));
                    AddTypeMap(typeof(String), typeof(UInt64), typeof(String));
                    AddTypeMap(typeof(String), typeof(Single), typeof(String));
                    AddTypeMap(typeof(String), typeof(Double), typeof(String));
                    AddTypeMap(typeof(String), typeof(Decimal), typeof(String));
                    AddTypeMap(typeof(String), typeof(Boolean), typeof(String));
                    AddTypeMap(typeof(String), typeof(Char), typeof(String));
                    AddTypeMap(typeof(String), typeof(String), typeof(String));

                    #region VS2010
                    //typeMap = new Dictionary<Type, Dictionary<Type, Type>>()
                    //{
                    //    // Comparing byte
                    //    {typeof(byte), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(byte) },
                    //        { typeof(short), typeof(short) },
                    //        { typeof(int), typeof(int) },
                    //        { typeof(long), typeof(long) },
                    //        { typeof(ulong), typeof(ulong) },
                    //        { typeof(float), typeof(float) },
                    //        { typeof(double), typeof(double) },
                    //        { typeof(decimal), typeof(decimal) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing short
                    //    {typeof(short), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(short) },
                    //        { typeof(short), typeof(short) },
                    //        { typeof(int), typeof(int) },
                    //        { typeof(long), typeof(long) },
                    //        { typeof(ulong), typeof(ulong) },
                    //        { typeof(float), typeof(float) },
                    //        { typeof(double), typeof(double) },
                    //        { typeof(decimal), typeof(decimal) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing int
                    //    {typeof(int), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(int) },
                    //        { typeof(short), typeof(int) },
                    //        { typeof(int), typeof(int) },
                    //        { typeof(long), typeof(long) },
                    //        { typeof(ulong), typeof(ulong) },
                    //        { typeof(float), typeof(float) },
                    //        { typeof(double), typeof(double) },
                    //        { typeof(decimal), typeof(decimal) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing long
                    //    {typeof(long), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(long) },
                    //        { typeof(short), typeof(long) },
                    //        { typeof(int), typeof(long) },
                    //        { typeof(long), typeof(long) },
                    //        { typeof(ulong), typeof(ulong) },
                    //        { typeof(float), typeof(float) },
                    //        { typeof(double), typeof(double) },
                    //        { typeof(decimal), typeof(decimal) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing ulong
                    //    {typeof(ulong), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(ulong) },
                    //        { typeof(short), typeof(ulong) },
                    //        { typeof(int), typeof(ulong) },
                    //        { typeof(long), typeof(ulong) },
                    //        { typeof(ulong), typeof(ulong) },
                    //        { typeof(float), typeof(float) },
                    //        { typeof(double), typeof(double) },
                    //        { typeof(decimal), typeof(decimal) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing float
                    //    {typeof(float), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(float) },
                    //        { typeof(short), typeof(float) },
                    //        { typeof(int), typeof(float) },
                    //        { typeof(long), typeof(float) },
                    //        { typeof(ulong), typeof(float) },
                    //        { typeof(float), typeof(float) },
                    //        { typeof(double), typeof(double) },
                    //        { typeof(decimal), typeof(decimal) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing double
                    //    {typeof(double), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(double) },
                    //        { typeof(short), typeof(double) },
                    //        { typeof(int), typeof(double) },
                    //        { typeof(long), typeof(double) },
                    //        { typeof(ulong), typeof(double) },
                    //        { typeof(float), typeof(double) },
                    //        { typeof(double), typeof(double) },
                    //        { typeof(decimal), typeof(decimal) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing decimal
                    //    {typeof(decimal), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(decimal) },
                    //        { typeof(short), typeof(decimal) },
                    //        { typeof(int), typeof(decimal) },
                    //        { typeof(long), typeof(decimal) },
                    //        { typeof(ulong), typeof(decimal) },
                    //        { typeof(float), typeof(decimal) },
                    //        { typeof(double), typeof(decimal) },
                    //        { typeof(decimal), typeof(decimal) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing bool
                    //    {typeof(bool), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(string) },
                    //        { typeof(short), typeof(string) },
                    //        { typeof(int), typeof(string) },
                    //        { typeof(long), typeof(string) },
                    //        { typeof(ulong), typeof(string) },
                    //        { typeof(float), typeof(string) },
                    //        { typeof(double), typeof(string) },
                    //        { typeof(decimal), typeof(string) },
                    //        { typeof(bool), typeof(bool) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing char
                    //    {typeof(char), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(string) },
                    //        { typeof(short), typeof(string) },
                    //        { typeof(int), typeof(string) },
                    //        { typeof(long), typeof(string) },
                    //        { typeof(ulong), typeof(string) },
                    //        { typeof(float), typeof(string) },
                    //        { typeof(double), typeof(string) },
                    //        { typeof(decimal), typeof(string) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(char) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //    // Comparing string
                    //    {typeof(string), new Dictionary<Type, Type>() {
                    //        { typeof(byte), typeof(string) },
                    //        { typeof(short), typeof(string) },
                    //        { typeof(int), typeof(string) },
                    //        { typeof(long), typeof(string) },
                    //        { typeof(ulong), typeof(string) },
                    //        { typeof(float), typeof(string) },
                    //        { typeof(double), typeof(string) },
                    //        { typeof(decimal), typeof(string) },
                    //        { typeof(bool), typeof(string) },
                    //        { typeof(char), typeof(string) },
                    //        { typeof(string), typeof(string) },
                    //    }},

                    //};
                    #endregion
                }
            }
            //DumpTypeMap();
        }
    }
}
