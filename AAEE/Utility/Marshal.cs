using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

using AAEE.Configurations;
using AAEE.DataPacks;
using AAEE.Utility;


namespace AAEE.Utility
{
    /// <summary>Reads Ruby Marshal format partially to obtain various data for display.
    /// Also reads and writes cfg files in a modified Marshal format.</summary>
    public static partial class Marshal
    {
        #region Fields

        private static int colorIndex;
        private static int tableIndex;
        private static int first;
        private static FileStream stream;

        #endregion

        #region Load RMXP Data
        public static void LoadRMXPData(List<string> names, List<int> ids, FileStream newStream)
        {
            stream = newStream;
            byte[] result = new byte[1];
            int i = 1, tmp = 0, namec = 0, size;
            tableIndex = colorIndex = first = 0;
            string name;
            stream.Read(result, 0, 1);
            stream.Read(result, 0, 1);
            stream.Read(result, 0, 1);
            size = analyzeNumber();
            switch ((char)result[0])
            {
                case '[':
                    stream.Read(result, 0, 1);
                    for (; i < size; i++)
                    {
                        ids.Add(i);
                    }
                    break;
                case '{':
                    stream.Read(result, 0, 1);
                    ids.Add(analyzeNumber());
                    namec = (int)stream.Position;
                    stream.Read(result, 0, 1);
                    stream.Read(result, 0, 1);
                    analyzeString(analyzeNumber());
                    analyzeObject(analyzeNumber());
                    for (; i < size; i++)
                    {
                        stream.Read(result, 0, 1);
                        ids.Add(analyzeNumber());
                        stream.Read(result, 0, 1);
                        stream.Read(result, 0, 1);
                        analyzeNumber();
                        analyzeObject(analyzeNumber());
                    }
                    stream.Seek(namec, SeekOrigin.Begin);
                    namec = 0;
                    break;
            }
            stream.Read(result, 0, 1);
            stream.Read(result, 0, 1);
            analyzeString(analyzeNumber());
            analyzeNumber();
            while (stream.Position < stream.Length)
            {
                stream.Read(result, 0, 1);
                char modeChar = (char)result[0];
                bool condition = true;
                if (modeChar == ':')
                {
                    if (names.Count == 0) namec++;
                    name = analyzeString(analyzeNumber());
                    condition = (name == "@name");
                }
                else if (modeChar == ';')
                {
                    tmp = analyzeNumber();
                    condition = (tmp == namec);
                }
                stream.Read(result, 0, 1);
                switch ((char)result[0])
                {
                    case '"':
                        if (condition)
                        {
                            names.Add(analyzeString(analyzeNumber()));
                        }
                        else
                        {
                            analyzeString(analyzeNumber());
                        }
                        break;
                    case 'i': analyzeNumber(); break;
                    case '[': analyzeArray(analyzeNumber()); break;
                    case 'u': determineTableColor(); break;
                    case 'o':
                        stream.Read(result, 0, 1);
                        if (modeChar == ':' || (char)result[0] == ':')
                        {
                            analyzeString(analyzeNumber());
                            analyzeObject(analyzeNumber());
                        }
                        else if (modeChar == ';' && (char)result[0] == ';')
                        {
                            if (analyzeNumber() == 0)
                            {
                                analyzeNumber();
                            }
                            else
                            {
                                analyzeObject(analyzeNumber());
                            }
                        }
                        break;
                }
            }
        }
        #endregion

        #region Analyzers
        private static int analyzeNumber()
        {
            byte[] tmp = new byte[1];
            stream.Read(tmp, 0, 1);
            if (tmp[0] == 0) return 0;
            if (tmp[0] < 6)
            {
                int i, result = 0, input = (int)tmp[0];
                for (i = 0; i < input; i++)
                {
                    stream.Read(tmp, 0, 1);
                    result += (int)tmp[0] * (int)MathProvider.Pow(256, i);
                }
                return result;
            }
            return ((int)tmp[0] - 5);
        }

        private static string analyzeString(int length)
        {
            byte[] tmp = new byte[length];
            int i;
            string result = "";
            stream.Read(tmp, 0, length);
            for (i = 0; i < length; i++)
            {
                result += (char)tmp[i];
            }
            return result;
        }

        private static void analyzeTable()
        {
            byte[] calc = new byte[16];
            analyzeNumber();
            stream.Read(calc, 0, 16);
            stream.Read(calc, 0, 4);
            int tmp = ((int)calc[0] + (int)calc[1] * 256 + (int)calc[2] * 256 * 256 +
                (int)calc[3] * 256 * 256 * 256);
            calc = new byte[tmp];
            stream.Read(calc, 0, tmp);
            stream.Read(calc, 0, tmp);
        }

        private static void analyzeObject(int attributes)
        {
            byte[] result = new byte[1];
            for (int i = 0; i < attributes; i++)
            {
                stream.Read(result, 0, 1);
                switch ((char)result[0])
                {
                    case ':': analyzeString(analyzeNumber()); break;
                    case ';': analyzeNumber(); break;
                }
                checkNext();
            }
        }

        private static void analyzeArray(int number)
        {
            byte[] result = new byte[1];
            for (int i = 0; i < number; i++)
            {
                checkNext();
            }
        }
        #endregion

        #region Utilities
        private static void checkNext()
        {
            byte[] result = new byte[1];
            stream.Read(result, 0, 1);
            switch ((char)result[0])
            {
                case '"': analyzeString(analyzeNumber()); break;
                case 'i': analyzeNumber(); break;
                case '[': analyzeArray(analyzeNumber()); break;
                case 'u': determineTableColor(); break;
                case 'o':
                    stream.Read(result, 0, 1);
                    switch ((char)result[0])
                    {
                        case ':': analyzeString(analyzeNumber()); break;
                        case ';': analyzeNumber(); break;
                    }
                    analyzeObject(analyzeNumber());
                    break;
            }
        }

        private static void determineTableColor()
        {
            byte[] result = new byte[1];
            string name;
            int tmp;
            stream.Read(result, 0, 1);
            switch ((char)result[0])
            {
                case ':':
                    name = analyzeString(analyzeNumber());
                    switch (name)
                    {
                        case "Table":
                            if (first == 0)
                            {
                                first = 1;
                            }
                            analyzeTable();
                            break;
                        case "Color":
                            if (first == 0)
                            {
                                first = 2;
                            }
                            byte[] remove = new byte[33];
                            stream.Read(remove, 0, 33);
                            break;
                    }
                    break;
                case ';':
                    tmp = analyzeNumber();
                    if (tableIndex == 0 && (first == 1 || first == 3) || tableIndex == tmp)
                    {
                        tableIndex = tmp;
                        first = ((first == 3 || first == 5) ? 5 : 4);
                        analyzeTable();
                    }
                    else if (colorIndex == 0 && (first == 2 || first == 4) || colorIndex == tmp)
                    {
                        colorIndex = tmp;
                        first = ((first == 4 || first == 5) ? 5 : 3);
                        byte[] remove = new byte[33];
                        stream.Read(remove, 0, 33);
                    }
                    break;
            }
        }
        #endregion
    }
}
