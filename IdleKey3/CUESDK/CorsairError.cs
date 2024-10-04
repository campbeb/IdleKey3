namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// Contains shared list of all errors that could happen during calling of Corsair* functions.
    /// </summary>
    public enum CorsairError
    {
        /// <summary>
        /// If previously called function was completed successfully
        /// </summary>
        CE_Success,

        /// <summary>
        /// if iCUE is not running or was shut down or third-party control is disabled in iCUE settings (runtime error), or if developer did not call CorsairConnect after calling CorsairDisconnect or on app start (developer error)
        /// </summary>
        CE_NotConnected,

        /// <summary>
        /// If some other client has or took over exclusive control (runtime error)
        /// </summary>
        CE_NoControl,

        /// <summary>
        /// if developer is calling the function that is not supported by the server (either because protocol has broken by server or client or because the function is new and server is too old. Check CorsairSessionDetails for details) (developer error)
        /// </summary>
        CE_IncompatibleProtocol,

        /// <summary>
        /// if developer supplied invalid arguments to the function (for specifics look at function descriptions) (developer error)
        /// </summary>
        CE_InvalidArguments,

        /// <summary>
        /// if developer is calling the function that is not allowed due to current state (reading improper properties from device, or setting callback when it has already been set) (developer error)
        /// </summary>
        CE_InvalidOperation,

        /// <summary>
        /// if invalid device id has been supplied as an argument to the function (when device id refers to disconnected device) (runtime error)
        /// </summary>
        CE_DeviceNotFound,

        /// <summary>
        /// if specific functionality (key interception) is disabled in iCUE settings (runtime error)
        /// </summary>
        CE_NotAllowed
    }
}
