using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DA.AssetsBundle
{
    public enum VerifyBy
    {
        Size,
        Hash
    }

    public enum PatchId
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5
    }

    public class VPatch
    {
        public PatchId id;
        public List<int> files = new List<int>();

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((byte)id);
            writer.Write(files.Count);
            foreach (var file in files)
            {
                writer.Write(file);
            }
        }

        public void Deserialize(BinaryReader reader)
        {
            id = (PatchId)reader.ReadByte();
            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var file = reader.ReadInt32();
                files.Add(file);
            }
        }
    }
    public class VFile
    {
        public string hash { get; set; }
        public long length { get; set; }
        public string name { get; set; }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(name);
            writer.Write(length);
            writer.Write(hash);
        }

        public void Deserialize(BinaryReader reader)
        {
            name = reader.ReadString();
            length = reader.ReadInt64();
            hash = reader.ReadString();
        }
    }

    public class Version
    {
        public int version;
        public List<VFile> files;
        public List<VPatch> patches;

        private Dictionary<string, VFile> dataFiles;
        private Dictionary<PatchId, VPatch> dataPatches;

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(version);

            writer.Write(files.Count);
            foreach (var file in files)
                file.Serialize(writer);

            writer.Write(patches.Count);
            foreach (var patch in patches)
            {
                writer.Write((byte)patch.id);
                writer.Write(patch.files.Count);
                foreach (var bundleId in patch.files)
                {
                    writer.Write(bundleId);
                }
            }

        }
        public void Deserialize(BinaryReader reader)
        {
            files = new List<VFile>();
            patches = new List<VPatch>();
            dataFiles = new Dictionary<string, VFile>();
            dataPatches = new Dictionary<PatchId, VPatch>();

            version = reader.ReadInt32();
            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var file = new VFile();
                file.Deserialize(reader);
                files.Add(file);
                dataFiles[file.name] = file;
            }
            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var patch = new VPatch();
                patch.Deserialize(reader);
                patches.Add(patch);
                dataPatches[patch.id] = patch;
            }
        }

        public VFile GetFile(string path)
        {
            VFile file;
            dataFiles.TryGetValue(path, out file);
            return file;
        }
        public List<VFile> GetFiles(PatchId patchId)
        {
            List<VFile> vFiles = new List<VFile>();
            VPatch patch;
            if (dataPatches.TryGetValue(patchId, out patch))
            {
                foreach (var index in patch.files)
                {
                    vFiles.Add(files[index]);
                }
            }
            return vFiles;
        }
    }


    public static class Versions
    {
        public const string FileName = "version";
        public static readonly VerifyBy verifyBy = VerifyBy.Hash;

        public static Version serverVersion;
        public static Version localVersion;

        public static Version LoadFullVersion(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                var reader = new BinaryReader(stream);
                var ver = new Version();
                ver.Deserialize(reader);
                return ver;
            }
        }

        internal static void BuildVersion(string outputPath, List<BundleRef> bundleRefs, List<VPatch> patches, int version)
        {
            var path = outputPath + "/" + FileName;
            if (File.Exists(path)) File.Delete(path);
            var files = new List<VFile>();

            foreach (var bundleRef in bundleRefs)
            {
                files.Add(new VFile()
                {
                    name = bundleRef.name,
                    hash = bundleRef.crc32Hash,
                    length = bundleRef.length,
                });
            }

            patches.Sort((x, y) => x.id.CompareTo(y.id));
            if (patches.Count > 0)
            {
                patches[0].files.Add(bundleRefs.Count - 1);
            }

            var ver = new Version();
            ver.version = version;
            ver.files = files;
            ver.patches = patches;

            using (FileStream stream = File.OpenWrite(path))
            {
                var writer = new BinaryWriter(stream);
                ver.Serialize(writer);
            }
        }

        public static bool IsNew(string path, long length, string hash)
        {
            if (!File.Exists(path)) return true;

            // 检测本地版本
            if (localVersion != null)
            {
                var key = Path.GetFileName(path);
                var file = localVersion.GetFile(key);
                if (file != null &&
                    file.length == length &&
                    file.hash.Equals(hash, StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            // 基于服务器的版本去检测文件是否需要更新
            using (var stream = File.OpenRead(path))
            {
                if (stream.Length != length) return true;
                if (verifyBy == VerifyBy.Hash)
                    return !Utility.GetCRC32Hash(stream).Equals(hash, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public static List<VFile> GetNewFiles(PatchId patchId, string path)
        {
            var newFiles = new List<VFile>();

            var files = serverVersion.GetFiles(patchId);
            foreach (var file in files)
            {
                if (IsNew(path + file.name, file.length, file.hash))
                {
                    newFiles.Add(file);
                }
            }
            return newFiles;
        }
    }
}