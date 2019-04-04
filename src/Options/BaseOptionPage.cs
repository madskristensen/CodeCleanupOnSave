using Microsoft.VisualStudio.Shell;

namespace CodeCleanupOnSave
{
    /// <summary>
    /// A base class for a DialogPage to show in Tools -> Options.
    /// </summary>
    internal class BaseOptionPage<T> : DialogPage where T : BaseOptionModel<T>, new()
    {
        private readonly BaseOptionModel<T> _model;

        public BaseOptionPage()
        {
#pragma warning disable VSTHRD102 // Implement internal logic asynchronously
            _model = ThreadHelper.JoinableTaskFactory.Run(BaseOptionModel<T>.CreateAsync);
#pragma warning restore VSTHRD102 // Implement internal logic asynchronously
        }

        public override object AutomationObject => _model;

        public override void LoadSettingsFromStorage()
        {
            _model.Load();
        }

        public override void SaveSettingsToStorage()
        {
            _model.Save();
        }
    }
}
