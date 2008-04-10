using System.Reflection;

[assembly: AssemblyVersion(ThisAssembly.Version)]
[assembly: AssemblyFileVersion(ThisAssembly.Version)]

[assembly: AssemblyProduct(ThisAssembly.Product)]
[assembly: AssemblyCopyright(ThisAssembly.Copyright)]

internal static partial class ThisAssembly
{
    public const string Version = "2.0.0.0";
    public const string Product = "Windows Forms Aero controls";

    public const string Copyright = "Copyright \u00a9 2007-2008 The Windows Vista Controls Project\n" +
                                    "http://www.CodePlex.com/VistaControls/";

    internal const string DesignAssemblyName = "WindowsFormsAero.Design, " +
                                               "Version=" + Version + ", " +
                                               "Culture=neutral, " +
                                               "PublicKeyToken=" + PublicKeyToken;

    internal const string PublicKeyToken = "f1c7e2ffebe61981";
}