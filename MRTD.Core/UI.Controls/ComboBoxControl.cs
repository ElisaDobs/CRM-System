using System;
using System.Collections.Generic;
using System.Text;
using ListForwardDeclaration = System.Collections.Generic.List<MRTD.Core.Models.ModuleActivityAnswerModel>;

namespace MRTD.Core.UI.Controls
{
    public class ComboBoxControl : IControl
    {
        private readonly int QuestionID;
        private readonly ListForwardDeclaration Lst_All_Answers;
        public ComboBoxControl(ListForwardDeclaration Lst_All_Answers, int QuestionID)
        {
            this.QuestionID = QuestionID;
            this.Lst_All_Answers = Lst_All_Answers;
        }

        public string GetControl()
        {
            StringBuilder strOut = new StringBuilder();
            strOut.Append("<div class=\"business-school-mls-input-field\">");
            strOut.Append("<select name=\"" + QuestionID + "\" id=\"" + QuestionID + "\">");
            foreach (var answer in this.Lst_All_Answers)
            {
                if (answer.LookupSelected)
                {
                    strOut.Append("<option value=\"" + answer.LookupAnswerID + "\" selected=\"selected\">" + answer.LookupAnswerName + "</option>");
                }
                else
                {
                    strOut.Append("<option value=\"" + answer.LookupAnswerID + "\">" + answer.LookupAnswerName + "</option>");
                }
            }
            strOut.Append("</select>");
            strOut.Append("</div>");
            return strOut.ToString();
        }
    }
}
