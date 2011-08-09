using System;

namespace WindowsFormsAero.InteropServices
{
    [Serializable]
    internal enum DeviceCapability
    {
        /// <summary>
        ///	 Device driver version                    
        /// </summary>
        DriverVersion = 0,
        /// <summary>
        ///	 Device classification                    
        /// </summary>
        Technology = 2,
        /// <summary>
        ///	 Horizontal size in millimeters           
        /// </summary>
        HorizontalSize = 4,
        /// <summary>
        ///	 Vertical size in millimeters             
        /// </summary>
        VerticalSize = 6,
        /// <summary>
        ///	 Horizontal width in pixels               
        /// </summary>
        HorizontalResolution = 8,
        /// <summary>
        ///	 Vertical height in pixels                
        /// </summary>
        VerticalResolution = 10,
        /// <summary>
        ///	 Number of bits per pixel                 
        /// </summary>
        BitsPerPixel = 12,
        /// <summary>
        ///	 Number of planes                         
        /// </summary>
        NumberOfPlanes = 14,
        /// <summary>
        ///	 Number of brushes the device has         
        /// </summary>
        NumberOfBrushes = 16,
        /// <summary>
        ///	 Number of pens the device has            
        /// </summary>
        NumberOfPens = 18,
        /// <summary>
        ///	 Number of markers the device has         
        /// </summary>
        NumberOfMarkers = 20,
        /// <summary>
        ///	 Number of fonts the device has           
        /// </summary>
        NumberOfFonts = 22,
        /// <summary>
        ///	 Number of colors the device supports     
        /// </summary>
        NumberOfColors = 24,
        /// <summary>
        ///	 Size required for device descriptor      
        /// </summary>
        PDeviceSize = 26,
        /// <summary>
        ///	 Curve capabilities                       
        /// </summary>
        CurveCapabilities = 28,
        /// <summary>
        ///	 Line capabilities                        
        /// </summary>
        LineCapabilities = 30,
        /// <summary>
        ///	 Polygonal capabilities                   
        /// </summary>
        PolygonalCapabilities = 32,
        /// <summary>
        ///	 Text capabilities                        
        /// </summary>
        TextCapabilities = 34,
        /// <summary>
        ///	 Clipping capabilities                    
        /// </summary>
        ClippingCapabilities = 36,
        /// <summary>
        ///	 Bitblt capabilities                      
        /// </summary>
        RasterCapabilities = 38,
        /// <summary>
        ///	 Length of the X leg                      
        /// </summary>
        AspectX = 40,
        /// <summary>
        ///	 Length of the Y leg                      
        /// </summary>
        AspectY = 42,
        /// <summary>
        ///	 Length of the hypotenuse                 
        /// </summary>
        AspectXY = 44,

        /// <summary>
        ///	 Logical pixels/inch in X                 
        /// </summary>
        LogPixelsX = 88,
        /// <summary>
        ///	 Logical pixels/inch in Y                 
        /// </summary>
        LogPixelsY = 90,

        /// <summary>
        ///	 Number of entries in physical palette    
        /// </summary>
        PaletteSize = 104,
        /// <summary>
        ///	 Number of reserved entries in palette    
        /// </summary>
        NumReserved = 106,
        /// <summary>
        ///	 Actual color resolution                  
        /// </summary>
        ColorResolution = 108,

        // Printing related DeviceCaps. These replace the appropriate Escapes

        /// <summary>
        ///	 Physical Width in device units           
        /// </summary>
        PhysicalWidth = 110,
        /// <summary>
        ///	 Physical Height in device units          
        /// </summary>
        PhysicalHeight = 111,
        /// <summary>
        ///	 Physical Printable Area x margin         
        /// </summary>
        PhysicalOffsetX = 112,
        /// <summary>
        ///	 Physical Printable Area y margin         
        /// </summary>
        PhysicalOffsetY = 113,
        /// <summary>
        ///	 Scaling factor x                         
        /// </summary>
        ScalingFactorX = 114,
        /// <summary>
        ///	 Scaling factor y                         
        /// </summary>
        ScalingFactorY = 115,

        // Display driver specific

        /// <summary>
        ///	 Current vertical refresh rate of the    
        ///	 display device (for displays only) in Hz
        /// </summary>
        VerticalRefresh = 116,
        /// <summary>
        ///	 Horizontal width of entire desktop in pixels
        /// </summary>
        DesktopVerticalResolution = 117,
        /// <summary>
        ///	 Vertical height of entire desktop in pixels 
        /// </summary>
        DesktopHorizontalResolution = 118,
        /// <summary>
        ///	 Preferred blt alignment                 
        /// </summary>
        BltAlignment = 119,
        /// <summary>
        ///	 Shading and blending caps               
        /// </summary>
        ShadingBlendingCapabilites = 120,
        /// <summary>
        ///	 Color Management caps                   
        /// </summary>
        ColorManagementCapabilites = 121,

    }
}
