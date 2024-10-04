using System;
using System.Runtime.InteropServices;

namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// contains information about device
    /// </summary>
    public struct CorsairDeviceInfo
    {
        /// <summary>
        /// enum describing device type
        /// </summary>
        public CorsairDeviceType type;

        /// <summary>
        /// null terminated Unicode string that contains unique device identifier
        /// </summary>
        //public CorsairDeviceId id;
        public string id;

        /// <summary>
        /// null terminated Unicode string that contains device serial number. Can be empty, if serial number is not available for the device
        /// </summary>
        public string serial;

        /// <summary>
        /// null terminated Unicode string that contains device model (like “K95RGB”)
        /// </summary>
        public string model;

        /// <summary>
        /// number of controllable LEDs on the device
        /// </summary>
        public int ledCount;

        /// <summary>
        /// number of channels controlled by the device
        /// </summary>
        public int channelCount;



        /// <summary>
        /// The native protocol details
        /// </summary>
        internal CorsairDeviceInfoNative native;

        /// <summary>
        /// Creates a instance of CorsairDeviceInfo
        /// </summary>
        /// <param name="protocolDetailsNative">The native protocol details</param>
        internal CorsairDeviceInfo(CorsairDeviceInfoNative deviceInfoNative)
        {
            native = deviceInfoNative;
            type = (CorsairDeviceType)native.type;
            id = Marshal.PtrToStringAnsi(native.id);
            serial = Marshal.PtrToStringAnsi(native.serial);
            model = Marshal.PtrToStringAnsi(native.model);
            ledCount = native.ledCount;
            channelCount = native.channelCount;
        }
    }
}
