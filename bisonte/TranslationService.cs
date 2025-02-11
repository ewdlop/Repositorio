using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.ComponentModel;
using System.Threading;

namespace RuntimeTranslation
{
    public class TranslationService
    {
        private static TranslationService _instance;
        private readonly Dictionary<string, string> _translationTable;
        private CultureInfo _currentCulture;
        public event EventHandler LanguageChanged;

        private TranslationService()
        {
            _translationTable = new Dictionary<string, string>();
            _currentCulture = Thread.CurrentThread.CurrentUICulture;
        }

        public static TranslationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TranslationService();
                return _instance;
            }
        }

        public void LoadTranslations(Dictionary<string, string> translations)
        {
            _translationTable.Clear();
            foreach (var pair in translations)
            {
                _translationTable[pair.Key.ToLower()] = pair.Value;
            }
        }

        public string Translate(string key)
        {
            if (string.IsNullOrEmpty(key)) return key;

            key = key.ToLower();
            return _translationTable.TryGetValue(key, out string translation) ? translation : key;
        }

        public void SetLanguage(string cultureName)
        {
            var newCulture = new CultureInfo(cultureName);
            if (_currentCulture.Name != newCulture.Name)
            {
                _currentCulture = newCulture;
                Thread.CurrentThread.CurrentUICulture = newCulture;
                Thread.CurrentThread.CurrentCulture = newCulture;
                LanguageChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // Attribute to mark properties for translation
    [AttributeUsage(AttributeTargets.Property)]
    public class TranslatableAttribute : Attribute
    {
        public string Key { get; }
        public TranslatableAttribute(string key = null)
        {
            Key = key;
        }
    }

    // Base class for forms or controls that need translation
    public class TranslatableForm : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TranslationService.Instance.LanguageChanged += (s, args) => UpdateTranslations();
            UpdateTranslations();
        }

        protected virtual void UpdateTranslations()
        {
            foreach (var property in GetType().GetProperties())
            {
                var attr = property.GetCustomAttributes(typeof(TranslatableAttribute), true)
                    .FirstOrDefault() as TranslatableAttribute;

                if (attr != null)
                {
                    string key = attr.Key ?? property.Name;
                    string translatedValue = TranslationService.Instance.Translate(key);
                    property.SetValue(this, translatedValue);
                }
            }

            // Recursively update child controls
            foreach (Control control in Controls)
            {
                UpdateControlTranslations(control);
            }
        }

        private void UpdateControlTranslations(Control control)
        {
            // Update text property if it has a Tag marking it for translation
            if (control.Tag is string translationKey)
            {
                control.Text = TranslationService.Instance.Translate(translationKey);
            }

            // Recursively update child controls
            foreach (Control child in control.Controls)
            {
                UpdateControlTranslations(child);
            }
        }
    }
}
