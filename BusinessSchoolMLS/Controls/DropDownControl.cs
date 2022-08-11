using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MRTD.Core.Models;

namespace BusinessSchoolMLS.Controls
{
    public class DropDownControl : IHtmlControl
    {
        private readonly int QuestionID;
        private readonly List<ModuleActivityAnswerModel> Lst_All_Answers;
        public DropDownControl(List<ModuleActivityAnswerModel> Lst_All_Answers, int QuestionID)
        {
            this.QuestionID = QuestionID;
            this.Lst_All_Answers = Lst_All_Answers;
        }

        public string GetControl()
        {
            string strOut = string.Empty;
            strOut += "<div class=\"business-school-mls-input-field\">";
            strOut += "<select name=\"" + QuestionID + "\" id=\"" + QuestionID + "\">";
            foreach (var answer in this.Lst_All_Answers)
            {
                if (answer.LookupSelected)
                {
                    strOut += "<option value=\"" + answer.LookupAnswerID + "\" selected=\"selected\">" + answer.LookupAnswerName + "</option>";
                }
                else
                {
                    strOut += "<option value=\"" + answer.LookupAnswerID + "\">" + answer.LookupAnswerName + "</option>";
                }
            }
            strOut += "</select>";
            strOut += "</div>";
            return strOut;
        }
    }
}
