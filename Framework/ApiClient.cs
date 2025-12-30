using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework
{
    public class ApiClient
    {
        public async Task<ApiResponse> Execute(ApiRequest apiRequest)
        {
            var body = apiRequest.Body.Replace("\r\n", "\n");
            body = HumpUnfold(body);
            var fromLanguage = LanguageTransform(apiRequest.TranslateServer, apiRequest.FromLanguage);
            var toLanguage = LanguageTransform(apiRequest.TranslateServer, apiRequest.ToLanguage);
            var res = new ApiResponse
            {
                SourceText = apiRequest.Body
            };
            switch (apiRequest.TranslateServer)
            {
                case TranslateServerEnum.Google:
                    var googleFanyi = new GoogleFanyi();
                    res = await googleFanyi.Fanyi(body, fromLanguage, toLanguage, apiRequest.FromLanguage, apiRequest.ToLanguage);
                    break;
                case TranslateServerEnum.Bing:
                    var bingFanyi = new BingFanyi();
                    res = await bingFanyi.Fanyi(body, fromLanguage, toLanguage, apiRequest.FromLanguage, apiRequest.ToLanguage);
                    break;
                case TranslateServerEnum.百度:
                    break;
                case TranslateServerEnum.有道:
                    var youdaoFanyi = new YoudaoFanyi();
                    res = await youdaoFanyi.Fanyi(body, fromLanguage, toLanguage, apiRequest.FromLanguage, apiRequest.ToLanguage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return res;
        }

        /// <summary>
        ///  驼峰展开
        /// </summary>
        /// <param name="humpString"></param>
        /// <returns></returns>
        private static string HumpUnfold(string humpString)
        {
            var ss = humpString.Split(' ');
            var res = "";
            foreach (var s in ss)
            {
                var regex = new Regex("([A-Z]|^)[a-z]+");
                var matcher = regex.Matches(s);
                if (matcher.Count > 0)
                {
                    var sb = new StringBuilder();
                    foreach (Match match in matcher)
                    {
                        var g = match.Groups[0].Value;
                        sb.Append(g + " ");
                    }

                    res += sb.ToString().TrimEnd() + " ";
                    //return sb.ToString().TrimEnd();
                }
                else
                {
                    res += s + " ";
                }
            }

            return res;
        }

        private static string LanguageTransform(TranslateServerEnum translateServer, LanguageEnum language)
        {
            var s = "";
            switch (translateServer)
            {
                case TranslateServerEnum.Bing:
                    switch (language)
                    {
                        case LanguageEnum.Auto:
                            s = "auto-detect";
                            break;
                        case LanguageEnum.日本語:
                            s = "ja";
                            break;
                        case LanguageEnum.简体中文:
                            s = "zh-Hans";
                            break;
                        case LanguageEnum.繁體中文:
                            s = "zh-Hant";
                            break;
                        case LanguageEnum.English:
                            s = "en";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(language), language, null);
                    }
                    break;
                case TranslateServerEnum.Google:
                    switch (language)
                    {
                        case LanguageEnum.Auto:
                            s = "auto";
                            break;
                        case LanguageEnum.日本語:
                            s = "ja";
                            break;
                        case LanguageEnum.简体中文:
                            s = "zh-CN";
                            break;
                        case LanguageEnum.繁體中文:
                            s = "zh-TW";
                            break;
                        case LanguageEnum.English:
                            s = "en";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(language), language, null);
                    }
                    break;
                case TranslateServerEnum.百度:
                    break;
                case TranslateServerEnum.有道:
                    switch (language)
                    {
                        case LanguageEnum.Auto:
                            s = "auto";
                            break;
                        case LanguageEnum.日本語:
                            s = "ja";
                            break;
                        case LanguageEnum.简体中文:
                            s = "zh-CHS";
                            break;
                        case LanguageEnum.繁體中文:
                            s = "zh-CHS";
                            break;
                        case LanguageEnum.English:
                            s = "en";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(language), language, null);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(translateServer), translateServer, null);
            }

            return s;
        }
    }
}
