using System;
using System.ComponentModel.Composition;
using EnvDTE;
using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;

namespace CodeCleanupOnSave
{
    [Export(typeof(ICommandHandler))]
    [Name(nameof(SaveHandler))]
    [ContentType("csharp")]
    [ContentType("basic")]
    [ContentType("f#")]
    [TextViewRole(PredefinedTextViewRoles.PrimaryDocument)]
    public class SaveHandler : ICommandHandler<SaveCommandArgs>
    {
        public string DisplayName => nameof(SaveHandler);

        private readonly IEditorCommandHandlerServiceFactory _commandService;

        [ImportingConstructor]
        public SaveHandler(IEditorCommandHandlerServiceFactory commandService)
        {
            _commandService = commandService;
        }

        public bool ExecuteCommand(SaveCommandArgs args, CommandExecutionContext executionContext)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // Check if file is part of a project first.
            if (args.SubjectBuffer.Properties.TryGetProperty(typeof(ITextDocument), out ITextDocument textDoc))
            {
                var filePath = textDoc.FilePath;

                var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
                ProjectItem item = dte.Solution?.FindProjectItem(filePath);

                if (string.IsNullOrEmpty(item?.ContainingProject?.FullName))
                {
                    return true;
                }
            }

            // Then check if it's been enabled in the options.
            if (!GeneralOptions.Instance.Enabled)
            {
                return true;
            }

            try
            {
                IEditorCommandHandlerService service = _commandService.GetService(args.TextView);

                // Profile 1
                if (GeneralOptions.Instance.Profile == GeneralOptions.CodeCleanupProfile.Profile1)
                {
                    var cmd = new CodeCleanUpDefaultProfileCommandArgs(args.TextView, args.SubjectBuffer);
                    service.Execute((v, b) => cmd, null);
                }
                // Profile 2
                else if (GeneralOptions.Instance.Profile == GeneralOptions.CodeCleanupProfile.Profile2)
                {
                    var cmd = new CodeCleanUpCustomProfileCommandArgs(args.TextView, args.SubjectBuffer);
                    service.Execute((v, b) => cmd, null);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return true;
        }

        public CommandState GetCommandState(SaveCommandArgs args)
        {
            return CommandState.Available;
        }

    }
}
