using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.System.UserProfile;

namespace WinDash.Models
{
    public class LanguageInfo : ObservableObject
    {
        private Dictionary<string, string> _languages;

        private List<String> _languagesList;
        public List<String> Languages
        {
            get { return _languagesList; }
            set { Set(ref _languagesList, value); }
        }


        public LanguageInfo()
        {
            _languages = ApplicationLanguages.ManifestLanguages.Select(tag =>
           {
               var lang = new Language(tag);
               return new KeyValuePair<string, string>(lang.NativeName, lang.LanguageTag);
           }).ToDictionary(keyitem => keyitem.Key, valueItem => valueItem.Value);
        }

        public String CurrentLanguage
        {
            get
            {
                var langTag = ApplicationLanguages.PrimaryLanguageOverride;
                if (String.IsNullOrEmpty(langTag))
                {
                    langTag = GlobalizationPreferences.Languages[0];
                }
                var lang = new Language(langTag);

                return lang.NativeName;
            }
            set
            {
                var currentLang = ApplicationLanguages.PrimaryLanguageOverride;
                string newLang;
                _languages.TryGetValue(value, out newLang);

                if (newLang != null)
                    if (currentLang != newLang)
                    {
                        ApplicationLanguages.PrimaryLanguageOverride = newLang;
                        ResourceContext.GetForCurrentView().Reset();
                        new System.Threading.ManualResetEvent(false).WaitOne(100);

                        RaisePropertyChanged();
                    }

            }
        }
    }
}
