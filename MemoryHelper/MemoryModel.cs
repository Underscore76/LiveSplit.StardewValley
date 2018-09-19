using System.Collections.Generic;

namespace MemoryHelper
{
    public class MemoryModel
    {
        public readonly string GameVersion;
        public readonly string CodeSignature;
        public readonly string SignatureValue;
        public readonly string SignaturePointer;
        public readonly Dictionary<string, MemoryFinder> Fields;

        public MemoryModel(string gameVersion, string signatureValue, string signaturePointer, string codeSignature)
        {
            GameVersion = gameVersion;
            CodeSignature = codeSignature;
            SignatureValue = signatureValue;
            SignaturePointer = signaturePointer;
            Fields = new Dictionary<string, MemoryFinder>();
        }

        protected static string RemoveWhiteSpace(params string[] str)
        {
            return string.Concat(str).Replace(" ", "");
        }

        public static MemoryModel V2(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhiteSpace(
                    "0fb6 15 vvvvvvvv",
                    "85d2",
                    "0f85 ????????",
                    "83 3d pppppppp 00")
                );
            model.Fields["IsSaving"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("_isSaving")
                .GetValue<bool>();
            model.Fields["IsConstructingGraphics"] = MemoryFinder
                .GetStaticField("graphics")
                .GetField("inDeviceTransition")
                .GetValue<bool>();
            return model;
        }

        public static MemoryModel V3(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhiteSpace(
                    "0fb6 15 vvvvvvvv",
                    "85d2",
                    "0f85 ????????",
                    "83 3d pppppppp 00")
                );
            model.Fields["IsPaused"] = MemoryFinder
                .GetStaticField("netWorldState")
                .GetField("value")
                .AsType("StardewValley.Network.NetWorldState")
                .GetField("isPaused")
                .GetField("value")
                .GetValue<bool>();
            model.Fields["IsSaving"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("_isSaving")
                .GetValue<bool>();
            model.Fields["IsConstructingGraphics"] = MemoryFinder
                .GetStaticField("graphics")
                .GetField("inDeviceTransition")
                .GetValue<bool>();
            return model;
        }
    }
}