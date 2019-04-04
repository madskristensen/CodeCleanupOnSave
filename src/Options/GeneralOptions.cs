using System.ComponentModel;

namespace CodeCleanupOnSave
{
    internal class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        [Category("General")]
        [DisplayName("Enabled")]
        [Description("Specifies whether to run Code Clean up automatically on save or not.")]
        [DefaultValue(true)]
        public bool Enabled { get; set; } = true;

        [Category("General")]
        [DisplayName("Profile")]
        [Description("Specifies whether to run Code Clean up automatically on save or not.")]
        [DefaultValue(CodeCleanupProfile.Profile1)]
        [TypeConverter(typeof(EnumConverter))]
        public CodeCleanupProfile Profile { get; set; } = CodeCleanupProfile.Profile1;

        public enum CodeCleanupProfile
        {
            Profile1,
            Profile2
        }
    }
}
