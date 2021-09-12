using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MemoryHelper
{
    public class Program
    {
        public static Dictionary<string, MemoryModel> Models = new Dictionary<string, MemoryModel>()
        {
            ["1.2.6400.27469"] = MemoryModel.V2("1.2.33-steam"),
            ["1.3.6794.32951"] = MemoryModel.V3("1.3.28-steam"),
            ["1.3.7853.31734"] = MemoryModel.V5("1.5.4-steam"),
        };

        public static void Main(string[] args)
        {
            Program program = new Program();
            if (!program.TryAttach()) return;

            Console.WriteLine("Computing field offsets...");
            foreach (var entry in program.Model.Fields)
            {
                int[] offsets = entry.Value.FindOffsets(program, out var failure);
                if (offsets == null)
                {
                    Console.WriteLine(entry.Key + " : " + failure);
                }
                else
                {
                    Console.WriteLine(entry.Key + " : " + entry.Value.ReadMethod + "( " + string.Join(", ", offsets) + " )");
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
            Console.WriteLine("Scanning for Stardew Valley...");

            while ((Scanner = MemoryScanner.TryAttach()) == null)
            {
                Thread.Sleep(1000);
            }

            Model = Models[Scanner.FileVersion];
            if (Model == null)
            {
                Console.WriteLine("Unknwon FileVersion " + Scanner.FileVersion);
                return false;
            }

            Console.WriteLine("Attaching to version " + Model.GameVersion + " ( " + Scanner.FileVersion + " )...");

            Game1 = Scanner.GetTypeByName("StardewValley.Game1");
            if (Game1 == null)
            {
                Console.WriteLine("Failed to find Game1");
                return false;
            }

            SignatureValueAddress = IntPtr.Zero;
            SignaturePointerAddress = IntPtr.Zero;
            ClrStaticField signatureValue = Game1.GetStaticFieldByName(Model.SignatureValue);
            ClrStaticField signaturePointer = Game1.GetStaticFieldByName(Model.SignaturePointer);
            if (signatureValue == null)
            {
                Console.WriteLine("Failed to find signature value " + Model.SignatureValue);
            }
            else if (!signatureValue.IsPrimitive)
            {
                Console.WriteLine("Signature " + Model.SignatureValue + " is not a value");
            }
            else
            {
                SignatureValueAddress = Scanner.GetAddress(signatureValue);
                if (SignatureValueAddress == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to find signature value address for " + Model.SignatureValue);
                }
            }
            if (signaturePointer == null)
            {
                Console.WriteLine("Unable to find signature pointer " + Model.SignaturePointer);
            }
            else if (signaturePointer.IsPrimitive)
            {
                Console.WriteLine("Signature " + Model.SignaturePointer + " is not a pointer");
            }
            else
            {
                SignaturePointerAddress = Scanner.GetAddress(signaturePointer);
                if (SignaturePointerAddress == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to find signature pointer address for " + Model.SignaturePointer);
                }
            }
            if (SignatureValueAddress == IntPtr.Zero || SignaturePointerAddress == IntPtr.Zero)
            {
                return false;
            }

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
                Console.WriteLine("Failed to find the CodeSignature");
                return false;
            }
            else if (hits != 1 || correct != 1)
            {
                Console.WriteLine("The CodeSignature is not unique, it was found " + hits + " times, of which " + correct + " where correct");
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

/*
 codeSignature: RemoveWhiteSpace(
                    "0fb6 15 vvvvvvvv",
                    "85d2",
                    "0f85 ????????",
                    "83 3d pppppppp 00")
                );
 * 
 * 8B C6 A2 vvvvvvvv
 * FF 15 ????????
 * 85 C0
 * 74 10
 * FF 15 ????????
 * 8B C8
 * 8B 01
 * 8B 40 28
 * FF 50 18
 * 85 F6
 * 83 FE 03
 * 0F 84 ????????
 * 8D 65 F8
 * 5E
 * 5F
 * 5D
 * C3
 * 33 FF
 * 83 3D pppppppp 00
 * 74 3C
 * StardewValley.Game1::setGameMode+22 - 8B C6                
StardewValley.Game1::setGameMode+24 - A2 1E703F03          
StardewValley.Game1::setGameMode+29 - FF 15 3C37B205       
StardewValley.Game1::setGameMode+2F - 85 C0                
StardewValley.Game1::setGameMode+31 - 74 10                
StardewValley.Game1::setGameMode+33 - FF 15 3C37B205       
StardewValley.Game1::setGameMode+39 - 8B C8                
StardewValley.Game1::setGameMode+3B - 8B 01                
StardewValley.Game1::setGameMode+3D - 8B 40 28             
StardewValley.Game1::setGameMode+40 - FF 50 18             
StardewValley.Game1::setGameMode+43 - 85 F6                
StardewValley.Game1::setGameMode+45 - 74 10                
StardewValley.Game1::setGameMode+47 - 83 FE 03             
StardewValley.Game1::setGameMode+4A - 0F84 A1000000        
StardewValley.Game1::setGameMode+50 - 8D 65 F8             
StardewValley.Game1::setGameMode+53 - 5E                   
StardewValley.Game1::setGameMode+54 - 5F                   
StardewValley.Game1::setGameMode+55 - 5D                   
StardewValley.Game1::setGameMode+56 - C3                   
StardewValley.Game1::setGameMode+57 - 33 FF                
StardewValley.Game1::setGameMode+59 - 83 3D 68386904 00    
StardewValley.Game1::setGameMode+60 - 74 3C                

 * 
 * 
 * 
 */