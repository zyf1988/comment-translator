using System.ComponentModel;
using CommentTranslator.Resources.lang;
using CommentTranslator.Util;
using Framework;
using Microsoft.VisualStudio.Shell;

namespace CommentTranslator.Presentation
{
    /// <summary>
    /// 工具选项窗体
    /// </summary>
    public class OptionPageGrid : DialogPage
    {
        [Category("Server")]
        [LocalizedDisplayName("TranslateServer_DisplayName", typeof(Resource))]
        [LocalizedDescription("TranslateServer_Description", typeof(Resource))]
        public TranslateServerEnum TranslateServer { get; set; } = TranslateServerEnum.Google;

        /// <summary>
        /// Gets or sets 待翻译语言
        /// </summary>
        [Category("Translate")]
        [LocalizedDisplayName("TranslateFrom_DisplayName", typeof(Resource))]
        [LocalizedDescription("TranslateFrom_Description", typeof(Resource))]
        public LanguageEnum TranslateFrom { get; set; } = LanguageEnum.Auto;

        /// <summary>
        /// Gets or sets 目标语言
        /// </summary>
        [Category("Translate")]
        [LocalizedDisplayName("TranslatetTo_DisplayName", typeof(Resource))]
        [LocalizedDescription("TranslatetTo_Description", typeof(Resource))]
        public LanguageEnum TranslatetTo { get; set; } = GetCurrentCulture();

        /// <summary>
        /// 打开文件自动翻译
        /// </summary>
        [Category("Translate")]
        [LocalizedDisplayName("AutoTranslateComment_DisplayName", typeof(Resource))]
        [LocalizedDescription("AutoTranslateComment_Description", typeof(Resource))]
        public bool AutoTranslateComment { get; set; } = false;

        [Category("Translate")]
        [LocalizedDisplayName("AutoTextCopy_DisplayName", typeof(Resource))]
        [LocalizedDescription("AutoTextCopy_Description", typeof(Resource))]
        public bool AutoTextCopy { get; set; } = false;

        /// <summary>
        /// 翻译快速信息文本
        /// </summary>
        [Category("Translate")]
        [LocalizedDisplayName("AutoTranslateQuickInfo_DisplayName", typeof(Resource))]
        [LocalizedDescription("AutoTranslateQuickInfo_Description", typeof(Resource))]
        public bool AutoTranslateQuickInfo { get; set; } = true;

        protected override void OnApply(PageApplyEventArgs e)
        {
            base.OnApply(e);

            if (e.ApplyBehavior == ApplyKind.Apply)
            {
                SaveToSetting();
            }
        }


        /// <summary>
        /// 保存设置
        /// </summary>
        public void SaveToSetting()
        {
            CommentTranslatorPackage.Settings.ReloadSetting(this);
        }

        private static LanguageEnum GetCurrentCulture()
        {
            string currentCulture = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (currentCulture)
            {
                case "ja-JP":
                    return LanguageEnum.日本語;
                case "zh-CN":
                    return LanguageEnum.简体中文;
                case "zh-TW":
                    return LanguageEnum.繁體中文;
                case "en-US":
                    return LanguageEnum.English;
                default:
                    return LanguageEnum.简体中文;
            }
        }
    }
}