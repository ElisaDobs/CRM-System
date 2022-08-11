using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MRTD.Core.Models;

namespace BusinessSchoolMLS.Controls
{
    public class CheckBoxControl : IHtmlControl
    {
        private readonly int QuestionID;
        private readonly List<ModuleActivityAnswerModel> Lst_All_Answers;
        public CheckBoxControl(List<ModuleActivityAnswerModel> Lst_All_Answers, int QuestionID)
        {
            this.QuestionID = QuestionID;
            this.Lst_All_Answers = Lst_All_Answers;
        }

        public string GetControl()
        {
            string strOut = string.Empty;
            foreach (var answer in this.Lst_All_Answers)
            {
                strOut += "<div class=\"business-school-mls-input-field\">";
                if (answer.LookupSelected)
                {
                    strOut += "<input type=\"checkbox\" name=\"" + this.QuestionID.ToString() + "\" id=\"" + answer.LookupAnswerID.ToString() + "\" value=\"" + answer.LookupAnswerID.ToString() + "\" checked=\"checked\"  />";
                }
                else
                {
                    strOut += "<input type=\"checkbox\" name=\"" + this.QuestionID.ToString() + "\" id=\"" + answer.LookupAnswerID.ToString() + "\" value=\"" + answer.LookupAnswerID.ToString() + "\"  />";
                }
                strOut += "<label for=\"" + answer.LookupAnswerID.ToString() + "\">" + answer.LookupAnswerName + "</label>";
                strOut += "</div>";
            }
            return strOut;
        }
    }
}
