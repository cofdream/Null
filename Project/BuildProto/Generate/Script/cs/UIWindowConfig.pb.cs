// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: UIWindow_Config.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace DAProto {

  /// <summary>Holder for reflection information generated from UIWindow_Config.proto</summary>
  public static partial class UIWindowConfigReflection {

    #region Descriptor
    /// <summary>File descriptor for UIWindow_Config.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static UIWindowConfigReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVVSVdpbmRvd19Db25maWcucHJvdG8SC3BhY2thZ2VOYW1lIkUKD1VJV2lu",
            "ZG93X0NvbmZpZxIKCgJpZBgBIAEoBRISCgp3aW5kb3dOYW1lGAIgASgJEhIK",
            "CnByZWZhYlBhdGgYAyABKAkingEKFUV4Y2VsX1VJV2luZG93X0NvbmZpZxI6",
            "CgREYXRhGAEgAygLMiwucGFja2FnZU5hbWUuRXhjZWxfVUlXaW5kb3dfQ29u",
            "ZmlnLkRhdGFFbnRyeRpJCglEYXRhRW50cnkSCwoDa2V5GAEgASgFEisKBXZh",
            "bHVlGAIgASgLMhwucGFja2FnZU5hbWUuVUlXaW5kb3dfQ29uZmlnOgI4AUI2",
            "Chljb20uREFQcm90b2J1Zi5EQVByb3RvYnVmQg9VSVdpbmRvd19Db25maWeq",
            "AgdEQVByb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::DAProto.UIWindow_Config), global::DAProto.UIWindow_Config.Parser, new[]{ "Id", "WindowName", "PrefabPath" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::DAProto.Excel_UIWindow_Config), global::DAProto.Excel_UIWindow_Config.Parser, new[]{ "Data" }, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class UIWindow_Config : pb::IMessage<UIWindow_Config> {
    private static readonly pb::MessageParser<UIWindow_Config> _parser = new pb::MessageParser<UIWindow_Config>(() => new UIWindow_Config());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UIWindow_Config> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::DAProto.UIWindowConfigReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UIWindow_Config() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UIWindow_Config(UIWindow_Config other) : this() {
      id_ = other.id_;
      windowName_ = other.windowName_;
      prefabPath_ = other.prefabPath_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UIWindow_Config Clone() {
      return new UIWindow_Config(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "windowName" field.</summary>
    public const int WindowNameFieldNumber = 2;
    private string windowName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string WindowName {
      get { return windowName_; }
      set {
        windowName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "prefabPath" field.</summary>
    public const int PrefabPathFieldNumber = 3;
    private string prefabPath_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PrefabPath {
      get { return prefabPath_; }
      set {
        prefabPath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UIWindow_Config);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UIWindow_Config other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (WindowName != other.WindowName) return false;
      if (PrefabPath != other.PrefabPath) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (WindowName.Length != 0) hash ^= WindowName.GetHashCode();
      if (PrefabPath.Length != 0) hash ^= PrefabPath.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (WindowName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(WindowName);
      }
      if (PrefabPath.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(PrefabPath);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (WindowName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(WindowName);
      }
      if (PrefabPath.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PrefabPath);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UIWindow_Config other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.WindowName.Length != 0) {
        WindowName = other.WindowName;
      }
      if (other.PrefabPath.Length != 0) {
        PrefabPath = other.PrefabPath;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            WindowName = input.ReadString();
            break;
          }
          case 26: {
            PrefabPath = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Excel_UIWindow_Config : pb::IMessage<Excel_UIWindow_Config> {
    private static readonly pb::MessageParser<Excel_UIWindow_Config> _parser = new pb::MessageParser<Excel_UIWindow_Config>(() => new Excel_UIWindow_Config());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Excel_UIWindow_Config> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::DAProto.UIWindowConfigReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Excel_UIWindow_Config() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Excel_UIWindow_Config(Excel_UIWindow_Config other) : this() {
      data_ = other.data_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Excel_UIWindow_Config Clone() {
      return new Excel_UIWindow_Config(this);
    }

    /// <summary>Field number for the "Data" field.</summary>
    public const int DataFieldNumber = 1;
    private static readonly pbc::MapField<int, global::DAProto.UIWindow_Config>.Codec _map_data_codec
        = new pbc::MapField<int, global::DAProto.UIWindow_Config>.Codec(pb::FieldCodec.ForInt32(8), pb::FieldCodec.ForMessage(18, global::DAProto.UIWindow_Config.Parser), 10);
    private readonly pbc::MapField<int, global::DAProto.UIWindow_Config> data_ = new pbc::MapField<int, global::DAProto.UIWindow_Config>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<int, global::DAProto.UIWindow_Config> Data {
      get { return data_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Excel_UIWindow_Config);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Excel_UIWindow_Config other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!Data.Equals(other.Data)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= Data.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      data_.WriteTo(output, _map_data_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += data_.CalculateSize(_map_data_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Excel_UIWindow_Config other) {
      if (other == null) {
        return;
      }
      data_.Add(other.data_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            data_.AddEntriesFrom(input, _map_data_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
