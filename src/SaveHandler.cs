using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;

namespace CodeCleanupOnSave
{
    [Export(typeof(ICommandHandler))]
    [Name(nameof(SaveHandler))]
    [ContentType("code")]
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
