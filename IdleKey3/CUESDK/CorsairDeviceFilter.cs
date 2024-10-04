using System;
using System.Runtime.InteropServices;

namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// contains device search filter
    /// </summary>
    public struct CorsairDeviceFilter
    {
        /// <summary>
        /// mask that describes device types, formed as logical “or” of CorsairDeviceType enum values
        /// </summary>
        public int deviceTypeMask;

        /// <summary>
        /// Creates a instance of CorsairDeviceFilter
        /// </summary>
        /// <param name="protocolDetailsNative">The native protocol details</param>
        internal CorsairDeviceFilter(int _deviceTypeMask)
        {
            deviceTypeMask = _deviceTypeMask;
        }
    }
}
