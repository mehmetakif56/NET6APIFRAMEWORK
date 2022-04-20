using Microsoft.Extensions.Localization;

namespace TTBS.Helper
{
    public class GenericSharedResourceService
    {
        public List<IStringLocalizer> _sharedLocalizers { get; set; } = new List<IStringLocalizer>();

        public string this[string key]
        {
            get
            {
                foreach (IStringLocalizer localizer in _sharedLocalizers)
                {
                    if (localizer == null)
                        continue;

                    if (key == null || localizer.GetString(key).ResourceNotFound)
                        continue;

                    return localizer[key];
                }
                return key;
            }
        }
    }
}
