// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: modules/common/proto/header.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Apollo.Common {

  /// <summary>Holder for reflection information generated from modules/common/proto/header.proto</summary>
  public static partial class HeaderReflection {

    #region Descriptor
    /// <summary>File descriptor for modules/common/proto/header.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static HeaderReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiFtb2R1bGVzL2NvbW1vbi9wcm90by9oZWFkZXIucHJvdG8SDWFwb2xsby5j",
            "b21tb24aJW1vZHVsZXMvY29tbW9uL3Byb3RvL2Vycm9yX2NvZGUucHJvdG8i",
            "4gEKBkhlYWRlchIVCg10aW1lc3RhbXBfc2VjGAEgASgBEhMKC21vZHVsZV9u",
            "YW1lGAIgASgJEhQKDHNlcXVlbmNlX251bRgDIAEoDRIXCg9saWRhcl90aW1l",
            "c3RhbXAYBCABKAQSGAoQY2FtZXJhX3RpbWVzdGFtcBgFIAEoBBIXCg9yYWRh",
            "cl90aW1lc3RhbXAYBiABKAQSDwoHdmVyc2lvbhgHIAEoDRInCgZzdGF0dXMY",
            "CCABKAsyFy5hcG9sbG8uY29tbW9uLlN0YXR1c1BiEhAKCGZyYW1lX2lkGAkg",
            "ASgJYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Apollo.Common.ErrorCodeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Apollo.Common.Header), global::Apollo.Common.Header.Parser, new[]{ "TimestampSec", "ModuleName", "SequenceNum", "LidarTimestamp", "CameraTimestamp", "RadarTimestamp", "Version", "Status", "FrameId" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Header : pb::IMessage<Header> {
    private static readonly pb::MessageParser<Header> _parser = new pb::MessageParser<Header>(() => new Header());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Header> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Apollo.Common.HeaderReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Header() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Header(Header other) : this() {
      timestampSec_ = other.timestampSec_;
      moduleName_ = other.moduleName_;
      sequenceNum_ = other.sequenceNum_;
      lidarTimestamp_ = other.lidarTimestamp_;
      cameraTimestamp_ = other.cameraTimestamp_;
      radarTimestamp_ = other.radarTimestamp_;
      version_ = other.version_;
      Status = other.status_ != null ? other.Status.Clone() : null;
      frameId_ = other.frameId_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Header Clone() {
      return new Header(this);
    }

    /// <summary>Field number for the "timestamp_sec" field.</summary>
    public const int TimestampSecFieldNumber = 1;
    private double timestampSec_;
    /// <summary>
    /// Message publishing time in seconds. It is recommended to obtain
    /// timestamp_sec from ros::Time::now(), right before calling
    /// SerializeToString() and publish().
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double TimestampSec {
      get { return timestampSec_; }
      set {
        timestampSec_ = value;
      }
    }

    /// <summary>Field number for the "module_name" field.</summary>
    public const int ModuleNameFieldNumber = 2;
    private string moduleName_ = "";
    /// <summary>
    /// Module name.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ModuleName {
      get { return moduleName_; }
      set {
        moduleName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "sequence_num" field.</summary>
    public const int SequenceNumFieldNumber = 3;
    private uint sequenceNum_;
    /// <summary>
    /// Sequence number for each message. Each module maintains its own counter for
    /// sequence_num, always starting from 1 on boot.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint SequenceNum {
      get { return sequenceNum_; }
      set {
        sequenceNum_ = value;
      }
    }

    /// <summary>Field number for the "lidar_timestamp" field.</summary>
    public const int LidarTimestampFieldNumber = 4;
    private ulong lidarTimestamp_;
    /// <summary>
    /// Lidar Sensor timestamp for nano-second.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong LidarTimestamp {
      get { return lidarTimestamp_; }
      set {
        lidarTimestamp_ = value;
      }
    }

    /// <summary>Field number for the "camera_timestamp" field.</summary>
    public const int CameraTimestampFieldNumber = 5;
    private ulong cameraTimestamp_;
    /// <summary>
    /// Camera Sensor timestamp for nano-second.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong CameraTimestamp {
      get { return cameraTimestamp_; }
      set {
        cameraTimestamp_ = value;
      }
    }

    /// <summary>Field number for the "radar_timestamp" field.</summary>
    public const int RadarTimestampFieldNumber = 6;
    private ulong radarTimestamp_;
    /// <summary>
    /// Radar Sensor timestamp for nano-second.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong RadarTimestamp {
      get { return radarTimestamp_; }
      set {
        radarTimestamp_ = value;
      }
    }

    /// <summary>Field number for the "version" field.</summary>
    public const int VersionFieldNumber = 7;
    private uint version_;
    /// <summary>
    /// data version
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Version {
      get { return version_; }
      set {
        version_ = value;
      }
    }

    /// <summary>Field number for the "status" field.</summary>
    public const int StatusFieldNumber = 8;
    private global::Apollo.Common.StatusPb status_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Apollo.Common.StatusPb Status {
      get { return status_; }
      set {
        status_ = value;
      }
    }

    /// <summary>Field number for the "frame_id" field.</summary>
    public const int FrameIdFieldNumber = 9;
    private string frameId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string FrameId {
      get { return frameId_; }
      set {
        frameId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Header);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Header other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TimestampSec != other.TimestampSec) return false;
      if (ModuleName != other.ModuleName) return false;
      if (SequenceNum != other.SequenceNum) return false;
      if (LidarTimestamp != other.LidarTimestamp) return false;
      if (CameraTimestamp != other.CameraTimestamp) return false;
      if (RadarTimestamp != other.RadarTimestamp) return false;
      if (Version != other.Version) return false;
      if (!object.Equals(Status, other.Status)) return false;
      if (FrameId != other.FrameId) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TimestampSec != 0D) hash ^= TimestampSec.GetHashCode();
      if (ModuleName.Length != 0) hash ^= ModuleName.GetHashCode();
      if (SequenceNum != 0) hash ^= SequenceNum.GetHashCode();
      if (LidarTimestamp != 0UL) hash ^= LidarTimestamp.GetHashCode();
      if (CameraTimestamp != 0UL) hash ^= CameraTimestamp.GetHashCode();
      if (RadarTimestamp != 0UL) hash ^= RadarTimestamp.GetHashCode();
      if (Version != 0) hash ^= Version.GetHashCode();
      if (status_ != null) hash ^= Status.GetHashCode();
      if (FrameId.Length != 0) hash ^= FrameId.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (TimestampSec != 0D) {
        output.WriteRawTag(9);
        output.WriteDouble(TimestampSec);
      }
      if (ModuleName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ModuleName);
      }
      if (SequenceNum != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(SequenceNum);
      }
      if (LidarTimestamp != 0UL) {
        output.WriteRawTag(32);
        output.WriteUInt64(LidarTimestamp);
      }
      if (CameraTimestamp != 0UL) {
        output.WriteRawTag(40);
        output.WriteUInt64(CameraTimestamp);
      }
      if (RadarTimestamp != 0UL) {
        output.WriteRawTag(48);
        output.WriteUInt64(RadarTimestamp);
      }
      if (Version != 0) {
        output.WriteRawTag(56);
        output.WriteUInt32(Version);
      }
      if (status_ != null) {
        output.WriteRawTag(66);
        output.WriteMessage(Status);
      }
      if (FrameId.Length != 0) {
        output.WriteRawTag(74);
        output.WriteString(FrameId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TimestampSec != 0D) {
        size += 1 + 8;
      }
      if (ModuleName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ModuleName);
      }
      if (SequenceNum != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(SequenceNum);
      }
      if (LidarTimestamp != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(LidarTimestamp);
      }
      if (CameraTimestamp != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(CameraTimestamp);
      }
      if (RadarTimestamp != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(RadarTimestamp);
      }
      if (Version != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Version);
      }
      if (status_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Status);
      }
      if (FrameId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FrameId);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Header other) {
      if (other == null) {
        return;
      }
      if (other.TimestampSec != 0D) {
        TimestampSec = other.TimestampSec;
      }
      if (other.ModuleName.Length != 0) {
        ModuleName = other.ModuleName;
      }
      if (other.SequenceNum != 0) {
        SequenceNum = other.SequenceNum;
      }
      if (other.LidarTimestamp != 0UL) {
        LidarTimestamp = other.LidarTimestamp;
      }
      if (other.CameraTimestamp != 0UL) {
        CameraTimestamp = other.CameraTimestamp;
      }
      if (other.RadarTimestamp != 0UL) {
        RadarTimestamp = other.RadarTimestamp;
      }
      if (other.Version != 0) {
        Version = other.Version;
      }
      if (other.status_ != null) {
        if (status_ == null) {
          status_ = new global::Apollo.Common.StatusPb();
        }
        Status.MergeFrom(other.Status);
      }
      if (other.FrameId.Length != 0) {
        FrameId = other.FrameId;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 9: {
            TimestampSec = input.ReadDouble();
            break;
          }
          case 18: {
            ModuleName = input.ReadString();
            break;
          }
          case 24: {
            SequenceNum = input.ReadUInt32();
            break;
          }
          case 32: {
            LidarTimestamp = input.ReadUInt64();
            break;
          }
          case 40: {
            CameraTimestamp = input.ReadUInt64();
            break;
          }
          case 48: {
            RadarTimestamp = input.ReadUInt64();
            break;
          }
          case 56: {
            Version = input.ReadUInt32();
            break;
          }
          case 66: {
            if (status_ == null) {
              status_ = new global::Apollo.Common.StatusPb();
            }
            input.ReadMessage(status_);
            break;
          }
          case 74: {
            FrameId = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code