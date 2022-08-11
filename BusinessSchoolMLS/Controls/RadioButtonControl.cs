using BusinessSchoolMLS.SchoolBusinessComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MRTD.Core.Models;

namespace BusinessSchoolMLS.Controls
{
    public class RadioButtonControl : IHtmlControl
    {
        private readonly int QuestionID;
        private readonly List<ModuleActivityAnswerModel> Lst_All_Answers;
        public RadioButtonControl(List<ModuleActivityAnswerModel> Lst_All_Answers, int QuestionID)
        {
            this.QuestionID = QuestionID;
            this.Lst_All_Answers = Lst_All_Answers;
        }

        public string GetControl()
        {
            string strOut = string.Empty;
            foreach (var answer in this.Lst_All_Answers)
            {
                strOut += "<div>";
                if (answer.LookupSelected)
                {
                    strOut += "<input type=\"radio\" name=\"" + this.QuestionID.ToString() + "\" id=\"" + answer.LookupAnswerID.ToString() + "\" value=\"" + answer.LookupAnswerID.ToString() + "\" checked=\"checked\"  />";
                }
                else
                {
                    strOut += "<input type=\"radio\" name=\"" + this.QuestionID.ToString() + "\" id=\"" + answer.LookupAnswerID.ToString() + "\" value=\"" + answer.LookupAnswerID.ToString() + "\"  />";
                }
                strOut += "<label for=\"" + answer.LookupAnswerID.ToString() + "\">" + answer.LookupAnswerName + "</label>";
                strOut += "</div>";
            }
            return strOut;
        }
    }
}
