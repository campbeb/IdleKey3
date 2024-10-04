namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// Contains list of available SDK access modes.
    /// </summary>
    public enum CorsairAccessLevel
    {
        /// <summary>
        /// shared mode (default)
        /// </summary>
        CAL_Shared = 0,

        /// <summary>
        /// exclusive lightings, but shared events
        /// </summary>
        CAL_ExclusiveLightingControl = 1,

        /// <summary>
        /// exclusive key events, but shared lightings
        /// </summary>
        CAL_ExclusiveKeyEventsListening = 2,

        /// <summary>
        /// exclusive mode
        /// </summary>
        CAL_ExclusiveLightingControlAndKeyEventsListening = 3
    }
}
