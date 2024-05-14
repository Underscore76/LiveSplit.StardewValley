using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MemoryHelper
{
    public class Program
    {
        public static Dictionary<string, MemoryModel> Models = new Dictionary<string, MemoryModel>()
        {
            ["1.2.6400.27469"] = MemoryModel.V2("1.2.33-steam"),
            ["1.3.6794.32951"] = MemoryModel.V3("1.3.28-steam"),
            ["1.3.7114.34001"] = MemoryModel.V3("1.3.36-steam"),
            ["1.3.7269.37809"] = MemoryModel.V3("1.4.0-steam"), // it's basically 1.3.36, so the signature is the same
            ["1.3.7346.34283"] = MemoryModel.V3("1.4.5-steam"),
            ["1.3.7853.31734"] = MemoryModel.V5("1.5.4-steam"),
            ["1.3.37.0"] = MemoryModel.V5_5("1.5.5-steam"),
            ["1.5.6.21356"] = MemoryModel.V5_5("1.5.6-steam"),
            ["1.5.6.22018"] = MemoryModel.V5_6("1.5.6-steam"),
            ["1.3.8053.40424"] = MemoryModel.V5_6_x86("1.5.6-steam-compart"),
            ["1.6.3.24087"] = MemoryModel.V6_3("1.6.3-steam"),
        };

        public static void Main(string[] args)
        {
            Program program = new Program();
            if (!program.TryAttach())
                return;

            Debug.WriteLine("Computing field offsets...");
            foreach (var entry in program.Model.Fields)
            {
                Int64[] offsets = entry.Value.FindOffsets(program, out var failure);
                if (offsets == null)
                {
                    Debug.WriteLine(entry.Key + " : " + failure);
                }
                else
                {
                    Debug.WriteLine(entry.Key + " : " + entry.Value.ReadMethod + "( " + string.Join(", ", offsets) + " )");
                }
            }
        }

        public MemoryScanner Scanner { get; private set; }
        public MemoryModel Model { get; private set; }
        public ClrType Game1 { get; private set; }
        public IntPtr SignatureValueAddress { get; private set; }
        public IntPtr SignaturePointerAddress { get; private set; }

        public bool TryAttach()
        {
            Debug.WriteLine("Scanning for Stardew Valley...");

            while ((Scanner = MemoryScanner.TryAttach()) == null)
            {
                Thread.Sleep(1000);
            }

            Model = Models[Scanner.FileVersion];
            if (Model == null)
            {
                Debug.WriteLine("Unknwon FileVersion " + Scanner.FileVersion);
                return false;
            }

            Debug.WriteLine("Attaching to version " + Model.GameVersion + " ( " + Scanner.FileVersion + " )...");

            Game1 = Scanner.GetTypeByName("StardewValley.Game1");
            if (Game1 == null)
            {
                Debug.WriteLine("Failed to find Game1");
                return false;
            }

            SignatureValueAddress = IntPtr.Zero;
            SignaturePointerAddress = IntPtr.Zero;
            ClrStaticField signatureValue = Game1.GetStaticFieldByName(Model.SignatureValue);
            ClrStaticField signaturePointer = Game1.GetStaticFieldByName(Model.SignaturePointer);
            if (signatureValue == null)
            {
                Debug.WriteLine("Failed to find signature value " + Model.SignatureValue);
            }
            //else if (!signatureValue.IsPrimitive)
            //{
            //    Console.WriteLine("Signature " + Model.SignatureValue + " is not a value");
            //}
            else
            {
                SignatureValueAddress = Scanner.GetAddress(signatureValue);
                if (SignatureValueAddress == IntPtr.Zero)
                {
                    Debug.WriteLine("Failed to find signature value address for " + Model.SignatureValue);
                }
            }
            if (signaturePointer == null)
            {
                Debug.WriteLine("Unable to find signature pointer " + Model.SignaturePointer);
            }
            else if (signaturePointer.IsPrimitive)
            {
                Debug.WriteLine("Signature " + Model.SignaturePointer + " is not a pointer");
            }
            else
            {
                SignaturePointerAddress = Scanner.GetAddress(signaturePointer);
                if (SignaturePointerAddress == IntPtr.Zero)
                {
                    Debug.WriteLine("Failed to find signature pointer address for " + Model.SignaturePointer);
                }
            }
            if (SignatureValueAddress == IntPtr.Zero || SignaturePointerAddress == IntPtr.Zero)
            {
                return false;
            }
            Console.WriteLine($"{SignaturePointerAddress.ToString("x")} {SignatureValueAddress.ToString("x")}");
            return true;
            int valueIndex = Model.CodeSignature.IndexOf('v') / 2;
            int pointerIndex = Model.CodeSignature.IndexOf('p') / 2;
            int hits = 0;
            int correct = 0;
            foreach (IntPtr hit in Scanner.ScanFor(Model.CodeSignature))
            {
                hits++;
                if (Scanner.ReadPointer(hit + valueIndex) == SignatureValueAddress
                    && Scanner.ReadPointer(hit + pointerIndex) == SignaturePointerAddress)
                {
                    correct++;
                }
            }
            if (hits == 0)
            {
                Debug.WriteLine("Failed to find the CodeSignature");
                return false;
            }
            else if (hits != 1 || correct != 1)
            {
                Debug.WriteLine("The CodeSignature is not unique, it was found " + hits + " times, of which " + correct + " where correct");
                return false;
            }

            return true;
        }

        public static long Offset(IntPtr a, IntPtr b)
        {
            return a.ToInt64() - b.ToInt64();
        }
    }
}
