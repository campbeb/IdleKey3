namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// contains a list of all possible session states
    /// </summary>
    public enum CorsairSessionState
    {
        /// <summary>
        /// dummy value
        /// </summary>
        CSS_Invalid,

        /// <summary>
        /// client not initialized or client closed connection (initial state)
        /// </summary>
        CSS_Closed,

        /// <summary>
        /// client initiated connection but not connected yet
        /// </summary>
        CSS_Connecting,

        /// <summary>
        /// server did not respond, sdk will try again
        /// </summary>
        CSS_Timeout,

        /// <summary>
        /// server did not allow connection
        /// </summary>
        CSS_ConnectionRefused,

        /// <summary>
        /// server closed connection
        /// </summary>
        CSS_ConnectionLost,

        /// <summary>
        /// successfully connected
        /// </summary>
        CSS_Connected
    }
}
