// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Types.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Nbrpc {

  /// <summary>Holder for reflection information generated from Types.proto</summary>
  public static partial class TypesReflection {

    #region Descriptor
    /// <summary>File descriptor for Types.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgtUeXBlcy5wcm90bxIFbmJycGMqmQIKC1VuaWZvcm1UeXBlEgsKB3Vua25v",
            "d24QABILCgdib29sZWFuEAESCwoHaW50ZWdlchACEggKBHJlYWwQAxIICgR2",
            "ZWMyEAQSCAoEdmVjMxAFEggKBHZlYzQQBhIJCgVpdmVjMhAHEgkKBWl2ZWMz",
            "EAgSCQoFaXZlYzQQCRIJCgVidmVjMhAKEgkKBWJ2ZWMzEAsSCQoFYnZlYzQQ",
            "DBIKCgZtYXQyeDIQDRIKCgZtYXQyeDMQDhIKCgZtYXQyeDQQDxIKCgZtYXQz",
            "eDIQEBIKCgZtYXQzeDMQERIKCgZtYXQzeDQQEhIKCgZtYXQ0eDIQExIKCgZt",
            "YXQ0eDMQFBIKCgZtYXQ0eDQQFRINCglzdHJ1Y3R1cmUQFmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Nbrpc.UniformType), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum UniformType {
    [pbr::OriginalName("unknown")] Unknown = 0,
    [pbr::OriginalName("boolean")] Boolean = 1,
    [pbr::OriginalName("integer")] Integer = 2,
    [pbr::OriginalName("real")] Real = 3,
    [pbr::OriginalName("vec2")] Vec2 = 4,
    [pbr::OriginalName("vec3")] Vec3 = 5,
    [pbr::OriginalName("vec4")] Vec4 = 6,
    [pbr::OriginalName("ivec2")] Ivec2 = 7,
    [pbr::OriginalName("ivec3")] Ivec3 = 8,
    [pbr::OriginalName("ivec4")] Ivec4 = 9,
    [pbr::OriginalName("bvec2")] Bvec2 = 10,
    [pbr::OriginalName("bvec3")] Bvec3 = 11,
    [pbr::OriginalName("bvec4")] Bvec4 = 12,
    [pbr::OriginalName("mat2x2")] Mat2X2 = 13,
    [pbr::OriginalName("mat2x3")] Mat2X3 = 14,
    [pbr::OriginalName("mat2x4")] Mat2X4 = 15,
    [pbr::OriginalName("mat3x2")] Mat3X2 = 16,
    [pbr::OriginalName("mat3x3")] Mat3X3 = 17,
    [pbr::OriginalName("mat3x4")] Mat3X4 = 18,
    [pbr::OriginalName("mat4x2")] Mat4X2 = 19,
    [pbr::OriginalName("mat4x3")] Mat4X3 = 20,
    [pbr::OriginalName("mat4x4")] Mat4X4 = 21,
    [pbr::OriginalName("structure")] Structure = 22,
  }

  #endregion

}

#endregion Designer generated code
