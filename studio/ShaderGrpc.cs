// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Shader.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace NBPlayer {
  public static partial class Shader
  {
    static readonly string __ServiceName = "NBPlayer.Shader";

    static readonly grpc::Marshaller<global::NBPlayer.BuildShaderRequest> __Marshaller_NBPlayer_BuildShaderRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NBPlayer.BuildShaderRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::NBPlayer.BuildShaderReply> __Marshaller_NBPlayer_BuildShaderReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::NBPlayer.BuildShaderReply.Parser.ParseFrom);

    static readonly grpc::Method<global::NBPlayer.BuildShaderRequest, global::NBPlayer.BuildShaderReply> __Method_BuildShader = new grpc::Method<global::NBPlayer.BuildShaderRequest, global::NBPlayer.BuildShaderReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "BuildShader",
        __Marshaller_NBPlayer_BuildShaderRequest,
        __Marshaller_NBPlayer_BuildShaderReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::NBPlayer.ShaderReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Shader</summary>
    [grpc::BindServiceMethod(typeof(Shader), "BindService")]
    public abstract partial class ShaderBase
    {
      public virtual global::System.Threading.Tasks.Task<global::NBPlayer.BuildShaderReply> BuildShader(global::NBPlayer.BuildShaderRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for Shader</summary>
    public partial class ShaderClient : grpc::ClientBase<ShaderClient>
    {
      /// <summary>Creates a new client for Shader</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ShaderClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Shader that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ShaderClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ShaderClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ShaderClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::NBPlayer.BuildShaderReply BuildShader(global::NBPlayer.BuildShaderRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return BuildShader(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::NBPlayer.BuildShaderReply BuildShader(global::NBPlayer.BuildShaderRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_BuildShader, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::NBPlayer.BuildShaderReply> BuildShaderAsync(global::NBPlayer.BuildShaderRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return BuildShaderAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::NBPlayer.BuildShaderReply> BuildShaderAsync(global::NBPlayer.BuildShaderRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_BuildShader, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ShaderClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ShaderClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ShaderBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_BuildShader, serviceImpl.BuildShader).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ShaderBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_BuildShader, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::NBPlayer.BuildShaderRequest, global::NBPlayer.BuildShaderReply>(serviceImpl.BuildShader));
    }

  }
}
#endregion
