using System;
using System.Runtime.InteropServices;

namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// Contains information about SDK and CUE versions.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct CorsairDeviceInfoNative
    {
        /// <summary>
        /// enum describing device type
        /// </summary>
        public int type;

        /// <summary>
        /// null terminated Unicode string that contains unique device identifier
        /// </summary>
        public IntPtr id;

        /// <summary>
        /// null terminated Unicode string that contains device serial number. Can be empty, if serial number is not available for the device
        /// </summary>
        public IntPtr serial;

        /// <summary>
        /// null terminated Unicode string that contains device model (like “K95RGB”)
        /// </summary>
        public IntPtr model;

        /// <summary>
        /// number of controllable LEDs on the device
        /// </summary>
        public int ledCount;

        /// <summary>
        /// number of channels controlled by the device
        /// </summary>
        public int channelCount;
    }
}
