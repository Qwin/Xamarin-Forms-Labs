﻿using System.Diagnostics.CodeAnalysis;

namespace Xamarin.Forms.Labs.Services.Serialization.ServiceStackV3
{
    using ServiceStack.Text;

    using IStringSerializer = Xamarin.Forms.Labs.Services.Serialization.IStringSerializer;
    using JsConfig = ServiceStack.Text.JsConfig;
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE
    using JsonDateHandler = ServiceStack.Text.JsonDateHandler;
#else
#endif
    using Serializer = ServiceStack.Text.JsonSerializer;

    /// <summary>
    /// JSON serializer using ServiceStack.Text library.
    /// </summary>
    /// <remarks>
    /// 
    /// ServiceStack.Text copyright information.
    /// 
    /// Copyright (c) 2007-2011, Demis Bellot, ServiceStack.
    /// http://www.servicestack.net
    /// All rights reserved.
    /// 
    /// https://github.com/ServiceStack/ServiceStack.Text/blob/master/LICENSE
    /// 
    /// </remarks>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1644:DocumentationHeadersMustNotContainBlankLines", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class JsonSerializer : IJsonSerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        public JsonSerializer()
        {
            JsConfig.EmitCamelCaseNames = true;
            #if __ANDROID__ || __IOS__ || WINDOWS_PHONE
            JsConfig.DateHandler = JsonDateHandler.ISO8601;
#else
            JsConfig.DateHandler = DateHandler.ISO8601;
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        /// <param name="emitCamelCaseNames">Emit camelcase names</param>
        /// <param name="accurateDateTime">Set to true if you need DateTime's to not lose accuracy</param>
        public JsonSerializer(bool emitCamelCaseNames, bool accurateDateTime)
        {
            JsConfig.EmitCamelCaseNames = emitCamelCaseNames;

            if (accurateDateTime)
            {
            #if __ANDROID__ || __IOS__ || WINDOWS_PHONE
                JsConfig.DateHandler = JsonDateHandler.ISO8601;
#else
                JsConfig.DateHandler = DateHandler.ISO8601;
#endif
            }
        }

        public static void SetSerializeDelegate<T>(System.Func<T,string> serializerDelegate)
        {
            ServiceStack.Text.JsConfig<T>.SerializeFn = serializerDelegate;
        }

        public static void SetDeserializeDelegate<T>(System.Func<string, T> deserializerDelegate)
        {
            ServiceStack.Text.JsConfig<T>.DeSerializeFn = deserializerDelegate;
        }

        #region ISerializer Members

        public SerializationFormat Format
        {
            get { return SerializationFormat.Json; }
        }

        public void Flush()
        {
            global::ServiceStack.Text.JsConfig.Reset();
        }

        #endregion

        #region IByteSerializer Members

        public byte[] SerializeToBytes<T>(T obj)
        {
            return (this as IStringSerializer).GetSerializedBytes(obj);
        }

        public T Deserialize<T>(byte[] data)
        {
            return (this as IStringSerializer).DeserializeFromBytes<T>(data);
        }

        #endregion

        #region IStreamSerializer Members

        public void Serialize<T>(T obj, System.IO.Stream stream)
        {
            Serializer.SerializeToStream(obj, stream);
        }

        public T Deserialize<T>(System.IO.Stream stream)
        {
            return Serializer.DeserializeFromStream<T>(stream);
        }

        #endregion

        #region IStringSerializer Members

        string IStringSerializer.Serialize<T>(T obj)
        {
            return Serializer.SerializeToString(obj);
        }

        public T Deserialize<T>(string data)
        {
            return Serializer.DeserializeFromString<T>(data);
        }

        #endregion
    }
}
