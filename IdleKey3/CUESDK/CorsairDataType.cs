namespace Spectrum.CUE.SDK
{
    /// <summary>
    /// 
    /// </summary>
    public enum CorsairDataType
    {
        /// <summary>
        /// for property of type Boolean
        /// </summary>
        CT_Boolean = 0,

        /// <summary>
        /// for property of type Int32 or Enumeration
        /// </summary>
        CT_Int32 = 1,

        /// <summary>
        /// for property of type Float64
        /// </summary>
        CT_Float64 = 2,

        /// <summary>
        /// for property of type String
        /// </summary>
        CT_String = 3,

        /// <summary>
        /// for array of Boolean
        /// </summary>
        CT_Boolean_Array = 16,

        /// <summary>
        /// for array of Int32
        /// </summary>
        CT_Int32_Array = 17,

        /// <summary>
        /// for array of Float64
        /// </summary>
        CT_Float64_Array = 18,

        /// <summary>
        /// for array of String
        /// </summary>
        CT_String_Array = 19
    }
}
