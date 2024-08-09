// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
// <copyright file = "MLCameraNativeBindings.cs" company="Magic Leap, Inc">
//
// Copyright (c) 2018-present, Magic Leap, Inc. All Rights Reserved.
//
// </copyright>
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

#if UNITY_MAGICLEAP || UNITY_ANDROID

// Disable warnings about missing documentation for native interop.
#pragma warning disable 1591

namespace UnityEngine.XR.MagicLeap
{
    using System;

    /// <summary>
    /// MLCamera class exposes static functions to query camera related
    /// functions. Most functions are currently a direct pass through functions to the
    /// native C-API functions and incur no overhead.
    /// </summary>
    public sealed partial class MLCamera
    {
        /// <summary>
        /// This class defines the C# interface to the C functions/structures in "ml_camera.h".
        /// </summary>
        internal partial class NativeBindings
        {
            public delegate void DeviceAvailabilityStatusDelegate(ref MLCameraDeviceAvailabilityInfo info);

            /// <summary>
            /// A generic delegate for camera events.
            /// </summary>
            /// <param name="data">Custom data returned when the callback is triggered, user metadata.</param>
            public delegate void OnDataCallback(IntPtr data);

            /// <summary>
            /// A delegate for camera error events.
            /// </summary>
            /// <param name="error">The type of error that was reported.</param>
            /// <param name="data">Custom data returned when the callback is triggered, user metadata.</param>
            public delegate void OnErrorDataCallback(MLCamera.ErrorType error, IntPtr data);

            /// <summary>
            /// A delegate for image buffer events.
            /// </summary>
            /// <param name="output">The camera output type.</param>
            /// <param name="data">Custom data returned when the callback is triggered, user metadata.</param>
            public delegate void OnOutputRefDataCallback(ref MLCameraOutput output, IntPtr data);

            /// <summary>
            /// A delegate for camera preview events.
            /// </summary>
            /// <param name="metadataHandle">A handle to the metadata.</param>
            /// <param name="data">Custom data returned when the callback is triggered, user metadata.</param>
            public delegate void OnHandleDataCallback(ulong metadataHandle, IntPtr data);

            /// <summary>
            /// A delegate for camera capture events.
            /// </summary>
            /// <param name="extra">A structure containing extra result information.</param>
            /// <param name="data">Custom data returned when the callback is triggered, user metadata.</param>
            public delegate void OnResultExtrasRefDataCallback(ref MLCameraResultExtras extra, IntPtr data);

            /// <summary>
            /// A delegate for camera capture events with additional information.
            /// </summary>
            /// <param name="metadataHandle">A handle to the metadata.</param>
            /// <param name="extra">A structure containing extra result information.</param>
            /// <param name="data">Custom data returned when the callback is triggered, user metadata.</param>
            public delegate void OnHandleAndResultExtrasRefDataCallback(ulong metadataHandle,
                ref MLCameraResultExtras extra, IntPtr data);

            public delegate void OnCaptureFailedDelegate(ref MLCameraResultExtras extra, IntPtr data);

            public delegate void OnCaptureAbortedDelegate(IntPtr data);

            public delegate void OnPreviewBufferAvailableDelegate(ulong bufferHandle, ulong metadataHandle,
                ref MLCameraResultExtras extra, IntPtr data);
            
            public delegate void OnDeviceStreamingDelegate(IntPtr data);

            public delegate void OnDeviceIdleDelegate(IntPtr data);
            
            public delegate void OnImageBufferAvailableDelegate(ref MLCameraOutput output, ulong metadataHandle,
                ref MLCameraResultExtras extra, IntPtr data);

            public delegate void OnVideoBufferAvailableDelegate(ref MLCameraOutput output, ulong metadataHandle,
                ref MLCameraResultExtras extra, IntPtr data);

            public delegate void OnDeviceErrorDelegate(ErrorType error, IntPtr data);

            public delegate void OnDeviceDisconnectedDelegate(DisconnectReason reason, IntPtr data);

            public delegate void OnCaptureCompletedDelegate(ulong metadataHandle, ref MLCameraResultExtras extra, IntPtr data);
        }
    }
}
#endif
