namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// contains list of properties identifiers which can be read from device
    /// </summary>
    public enum CorsairDevicePropertyId
    {
        /// <summary>
        /// dummy value
        /// </summary>
        CDPI_Invalid = 0,

        /// <summary>
        /// array of CorsairDevicePropertyId members supported by device
        /// </summary>
        CDPI_PropertyArray = 1,

        /// <summary>
        /// indicates Mic state (On or Off); used for headset, headset stand
        /// </summary>
        CDPI_MicEnabled = 2,

        /// <summary>
        /// indicates Surround Sound state (On or Off); used for headset, headset stand
        /// </summary>
        CDPI_SurroundSoundEnabled = 3,

        /// <summary>
        /// indicates Sidetone state (On or Off); used for headset (where applicable)
        /// </summary>
        CDPI_SidetoneEnabled = 4,

        /// <summary>
        /// the number of active equalizer preset (integer, 1 - 5); used for headset, headset stand
        /// </summary>
        CDPI_EqualizerPreset = 5,

        /// <summary>
        /// keyboard physical layout (see CorsairPhysicalLayout for valid values); used for keyboard
        /// </summary>
        CDPI_PhysicalLayout = 6,

        /// <summary>
        /// keyboard logical layout (see CorsairLogicalLayout for valid values); used for keyboard
        /// </summary>
        CDPI_LogicalLayout = 7,

        /// <summary>
        /// array of programmable G, M or S keys on device
        /// </summary>
        CDPI_MacroKeyArray = 8,

        /// <summary>
        /// battery level (0 - 100); used for wireless devices
        /// </summary>
        CDPI_BatteryLevel = 9,

        /// <summary>
        /// total number of LEDs connected to the channel
        /// </summary>
        CDPI_ChannelLedCount = 10,

        /// <summary>
        /// number of LED-devices (fans, strips, etc.) connected to the channel which is controlled by the DIY device
        /// </summary>
        CDPI_ChannelDeviceCount = 11,

        /// <summary>
        /// array of integers, each element describes the number of LEDs controlled by the channel device
        /// </summary>
        CDPI_ChannelDeviceLedCountArray = 12,

        /// <summary>
        /// array of CorsairChannelDeviceType members, each element describes the type of the channel device
        /// </summary>
        CDPI_ChannelDeviceTypeArray = 13
    }
}
