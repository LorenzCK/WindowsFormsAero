using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(ThisAssembly.DesignAssemblyName + ", PublicKey=" + ThisAssembly.PublicKey)]

internal static class ThisAssembly
{
    public const string Version = "2.0.0.0";
    public const string Product = "Windows Forms Aero controls";

    public const string Copyright = "Copyright \u00a9 2007-2008 The Windows Vista Controls Project\n" +
                                    "http://www.CodePlex.com/VistaControls/";

    internal const string DesignAssemblyName = "WindowsFormsAero.Design";
    internal const string DesignAssemblyFullName = DesignAssemblyName + ", " +
                                                   "Version=" + Version + ", " +
                                                   "Culture=neutral, " +
                                                   "PublicKeyToken=" + PublicKeyToken;

    internal const string PublicKeyToken = "f1c7e2ffebe61981";

    internal const string PublicKey = "00240000048000009400000006020000002400005253413100040000010001002102ad41008b67" +
                                      "003854266ed2c022d10967e79537f264888570b8a8b5e8685e1c11430f22ce4a675753ebcd8c53" +
                                      "52b012c5681e7a07c0170e2699151f814f278f6c00cacf2ae43452c890654808f611bc27332c64" +
                                      "a7b09b522ea278bc3d57255427765305b487daa99dabcd2cebca697e5233c39065fe4c87b19fad" +
                                      "8ebc68d3";
}