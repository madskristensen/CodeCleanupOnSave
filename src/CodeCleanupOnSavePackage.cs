using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using static Microsoft.VisualStudio.VSConstants;
using Task = System.Threading.Tasks.Task;

namespace CodeCleanupOnSave
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideOptionPage(typeof(DialogPageProvider.General), "Environment", "Code Cleanup on Save", 0, 0, true, new[] { "Code Cleanup on Save" }, ProvidesLocalizedCategoryName = false)]
    [ProvideProfile(typeof(DialogPageProvider.General), "Environment", Vsix.Name, 0, 0, true)]
    [ProvideAutoLoad(UICONTEXT.CSharpProject_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UICONTEXT.VBProject_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UICONTEXT.FSharpProject_string, PackageAutoLoadFlags.BackgroundLoad)]
    [Guid(PackageGuids.guidCodeCleanupOnSavePackageString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class CodeCleanupOnSavePackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await RunOnSave.InitializeAsync(this);
        }
    }
}
