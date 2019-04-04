using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CodeCleanupOnSave
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideOptionPage(typeof(DialogPageProvider.General), "Environment\\Documents", "Code Cleanup", 0, 0, true, ProvidesLocalizedCategoryName = false)]
    [Guid("aee4d337-3c9a-4f9b-86ae-6dd7bd751bd5")]
    public sealed class CodeCleanupOnSavePackage : AsyncPackage
    {
        
    }
}
