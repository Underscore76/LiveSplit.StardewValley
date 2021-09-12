using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryHelper
{
    public class MemoryFinder
    {
        private readonly string[] Fields;
        private readonly string[] AsTypes;
        private readonly Type ValueType;

        public MemoryFinder(string[] fields, string[] asTypes, Type valueType)
        {
            Fields = fields;
            AsTypes = asTypes;
            ValueType = valueType;
        }

        public int[] FindOffsets(Program program, out string failure)
        {
            ClrStaticField baseField = program.Game1.GetStaticFieldByName(Fields[0]);
            if (baseField == null)
            {
                failure = "Failed to find static field " + Fields[0];
                return null;
            }
            IntPtr baseAddress = program.Scanner.GetAddress(baseField);
            if (baseAddress == IntPtr.Zero)
            {
                failure = "Failed to find the address of static field " + Fields[0];
                return null;
            }
            IntPtr referenceAddress = baseField.IsPrimitive ? program.SignatureValueAddress : program.SignaturePointerAddress;

            List<int> offsets = new List<int>();
            offsets.Add(baseAddress.ToInt32() - referenceAddress.ToInt32());

            ClrField field = baseField;
            for (int i = 1; i < Fields.Length; i++)
            {
                ClrType type;
                if (AsTypes[i - 1] == null)
                {
                    type = field.Type;
                    if (type == null)
                    {
                        failure = "Failed to find the type of field " + field.Name;
                        return null;
                    }
                }
                else
                {
                    type = program.Scanner.GetTypeByName(AsTypes[i - 1]);
                    if (type == null)
                    {
                        failure = "Failed to find type " + AsTypes[i - 1];
                        return null;
                    }
                }

                field = type.GetFieldByName(Fields[i]);
                if (field == null)
                {
                    failure = "Failed to find field " + Fields[i] + " in type " + type.Name;
                    return null;
                }
                if (field.Offset < 0)
                {
                    failure = "Failed to find the offset for field " + field.Name;
                    return null;
                }
                offsets.Add(MemoryScanner.ObjectHeaderSize + field.Offset);
            }

            // extra 0 simplifies code that reads the string
            if (ValueType == typeof(string))
            {
                offsets.Add(0);
            }

            failure = null;
            return offsets.ToArray();
        }

        public string ReadMethod
        {
            get
            {
                if (ValueType == typeof(IntPtr))
                {
                    return "ReadPointer";
                }
                else if (ValueType == typeof(string))
                {
                    return "ReadString";
                }
                else
                {
                    return "ReadValue<" + ValueType.Name + ">";
                }
            }
        }

        public static MemoryFinderBuilder GetStaticField(string staticField)
        {
            return new MemoryFinderBuilder(staticField);
        }
    }

    public class MemoryFinderBuilder
    {
        private List<string> Fields;
        private List<string> AsTypes;

        public MemoryFinderBuilder(string staticField)
        {
            Fields = new List<string>() { staticField };
            AsTypes = new List<string>();
        }

        public MemoryFinderBuilder GetField(string field)
        {
            AsTypes.AddRange(Enumerable.Repeat<string>(null, Fields.Count - AsTypes.Count));
            Fields.Add(field);
            return this;
        }

        public MemoryFinderBuilder AsType(string type)
        {
            AsTypes.Add(type);
            return this;
        }


        public MemoryFinder GetValue<T>()
        {
            AsTypes.AddRange(Enumerable.Repeat<string>(null, Fields.Count - AsTypes.Count - 1));
            return new MemoryFinder(Fields.ToArray(), AsTypes.ToArray(), typeof(T));
        }
    }
}
