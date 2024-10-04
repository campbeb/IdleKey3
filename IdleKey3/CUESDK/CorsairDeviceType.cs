namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// 
    /// </summary>
    public enum CorsairDeviceType : long
    {
        /// <summary>
        /// for unknown/invalid devices
        /// </summary>
        CDT_Unknown = 0x0000,

        /// <summary>
        /// for keyboards
        /// </summary>
        CDT_Keyboard = 0x0001,

        /// <summary>
        /// for mice
        /// </summary>
        CDT_Mouse = 0x0002,

        /// <summary>
        /// for mousemats
        /// </summary>
        CDT_Mousemat = 0x0004,

        /// <summary>
        /// for headsets
        /// </summary>
        CDT_Headset = 0x0008,

        /// <summary>
        /// for headset stands
        /// </summary>
        CDT_HeadsetStand = 0x0010,

        /// <summary>
        /// for DIY-devices like Commander PRO
        /// </summary>
        CDT_FanLedController = 0x0020,

        /// <summary>
        /// for DIY-devices like Lighting Node PRO
        /// </summary>
        CDT_LedController = 0x0040,

        /// <summary>
        /// for memory modules
        /// </summary>
        CDT_MemoryModule = 0x0080,

        /// <summary>
        /// for coolers
        /// </summary>
        CDT_Cooler = 0x0100,

        /// <summary>
        /// for motherboards
        /// </summary>
        CDT_Motherboard = 0x0200,

        /// <summary>
        /// for graphics cards
        /// </summary>
        CDT_GraphicsCard = 0x0400,

        /// <summary>
        /// for touchbars
        /// </summary>
        CDT_Touchbar = 0x0800,

        /// <summary>
        /// for all devices
        /// </summary>
        CDT_All = 0xFFFFFFFF
    }
}
