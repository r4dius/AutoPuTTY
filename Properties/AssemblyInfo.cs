using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AutoPuTTY")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("r4dius")]
[assembly: AssemblyProduct("AutoPuTTY")]
[assembly: AssemblyCopyright("Copyright © r4dius 2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("f6f12a9d-918b-464c-9844-45e7a30629db")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//

[assembly: AssemblyVersion(Info.version)]
[assembly: AssemblyFileVersion(Info.version)]

public static class Info
{
    public const string version = "0.50";
}
