using System;
using System.ComponentModel.Design;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace CodeCleanupOnSave
{
    internal sealed class RunOnSave
    {
        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Assumes.Present(commandService);

            var menuCommandID = new CommandID(PackageGuids.guidCodeCleanupOnSavePackageCmdSet, PackageIds.RunOnSave);
            var menuItem = new OleMenuCommand(Execute, menuCommandID);
            menuItem.BeforeQueryStatus += MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private static void MenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            var button = (OleMenuCommand)sender;

            button.Checked = GeneralOptions.Instance.Enabled;
        }

        private static void Execute(object sender, EventArgs e)
        {
            var button = (OleMenuCommand)sender;

            GeneralOptions.Instance.Enabled = !button.Checked;
            GeneralOptions.Instance.Save();
        }
    }
}
