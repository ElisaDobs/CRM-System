using System;
using System.Collections.Generic;
using System.Text;

namespace MRTD.Core.UI.Controls
{
    public class Option {
        public string Value { get; set; }

        public string Text { get; set; }

        public bool Selected { get; set; }
    }
    public static class TippConnecHtmlOptions
    {
        public static string GenerateHtmlOptions(IEnumerable<Option> options)
        {
            string optionHtml = "<option value=\"0\"><option>";

            foreach (var option in options)
            {
                if (option.Selected)
                {
                    optionHtml = optionHtml + $"<option value=\"{option.Value}\" selected=\"selected\">{option.Text}<option>";
                }
                else
                {
                    optionHtml = optionHtml + $"<option value=\"{option.Value}\">{option.Text}<option>";
                }
            }

            return optionHtml;
        }
    }
}
