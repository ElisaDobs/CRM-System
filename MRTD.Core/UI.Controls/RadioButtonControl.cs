using System;
using System.Collections.Generic;
using System.Text;
using ListForwardDeclaration = System.Collections.Generic.List<MRTD.Core.Models.ModuleActivityAnswerModel>;

namespace MRTD.Core.UI.Controls
{
    public class RadioButtonControl
    {
        private readonly int QuestionID;
        private readonly ListForwardDeclaration Lst_All_Answers;
        public RadioButtonControl(ListForwardDeclaration Lst_All_Answers, int QuestionID)
        {
            this.QuestionID = QuestionID;
            this.Lst_All_Answers = Lst_All_Answers;
        }

        public string GetControl()
        {
            StringBuilder strOut = new StringBuilder();
            foreach (var answer in this.Lst_All_Answers)
            {
                strOut.Append("<div>");
                if (answer.LookupSelected)
                {
                    strOut.Append("<input type=\"radio\" name=\"" + this.QuestionID.ToString() + "\" id=\"" 
                                 + answer.LookupAnswerID.ToString() + "\" value=\"" + answer.LookupAnswerID.ToString() + "\" checked=\"checked\"  />");
                }
                else
                {
                    strOut.Append("<input type=\"radio\" name=\"" + this.QuestionID.ToString() + "\" id=\"" 
                                 + answer.LookupAnswerID.ToString() + "\" value=\"" + answer.LookupAnswerID.ToString() + "\"  />");
                }
                strOut.Append("<label for=\"" + answer.LookupAnswerID.ToString() + "\">" + answer.LookupAnswerName + "</label>");
                strOut.Append("</div>");
            }
            return strOut.ToString();
        }
    }
}
