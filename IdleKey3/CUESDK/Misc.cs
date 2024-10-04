
using System.Runtime.InteropServices;

namespace Spectrum.CUE.SDK
{
    internal delegate void CorsairSessionStateChangedHandler(int context, CorsairSessionStateChanged eventData);

    public static class CUEConstants
    {
        public static int CORSAIR_STRING_SIZE_S = 64;        // small string length
        public static int CORSAIR_STRING_SIZE_M = 128;       // medium string length
        public static int CORSAIR_LAYER_PRIORITY_MAX = 255;  // maximum level of layer’s priority that can be used in CorsairSetLayerPriority
        public static int CORSAIR_DEVICE_COUNT_MAX = 64;     // maximum number of devices to be discovered
        public static int CORSAIR_DEVICE_LEDCOUNT_MAX = 512; // maximum number of LEDs controlled by device
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CorsairSessionStateChanged
    {
        /// <summary>
        /// iCUE-SDK: new session state which SDK client has been transitioned to
        /// </summary>
        internal CorsairSessionState state;

        /// <summary>
        /// iCUE-SDK: information about client/server versions
        /// </summary>
        internal CorsairSessionDetails details;
    };

    /// iCUE-SDK: contains information about SDK and iCUE versions
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CorsairSessionDetails
    {
        /// <summary>
        /// iCUE-SDK: version of SDK client (like {4,0,1}). Always contains valid value even if there was no iCUE found. Must comply with the semantic versioning rules.
        /// </summary>
        internal CorsairVersion clientVersion;

        /// <summary>
        /// iCUE-SDK: version of SDK server (like {4,0,1}) or empty struct ({0,0,0}) if the iCUE was not found. Must comply with the semantic versioning rules.
        /// </summary>
        internal CorsairVersion serverVersion;

        /// <summary>
        /// iCUE-SDK: version of iCUE (like {3,33,100}) or empty struct ({0,0,0}) if the iCUE was not found.
        /// </summary>
        internal CorsairVersion serverHostVersion;
    };

    /// iCUE-SDK: contains information about version that consists of three components
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CorsairVersion
    {
        #region Properties & Fields

        internal int major;
        internal int minor;
        internal int patch;

        #endregion

        #region Methods

        public override string ToString() => $"{major}.{minor}.{patch}";

        #endregion
    };


}

