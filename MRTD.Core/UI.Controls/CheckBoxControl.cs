using System;
using System.Text;
using MRTD.Core.Models;
using ListForwardDeclaration = System.Collections.Generic.List<MRTD.Core.Models.ModuleActivityAnswerModel>;

namespace MRTD.Core.UI.Controls
{
    public class CheckBoxControl : IControl
    {
        private readonly int QuestionID;
        private readonly ListForwardDeclaration Lst_All_Answers;
        public CheckBoxControl(ListForwardDeclaration Lst_All_Answers, int QuestionID)
        {
            this.QuestionID = QuestionID;
            this.Lst_All_Answers = Lst_All_Answers;
        }

        public string GetControl()
        {
            StringBuilder strOut = new StringBuilder();
            foreach (var answer in this.Lst_All_Answers)
            {
                strOut.Append("<div class=\"business-school-mls-input-field\">");
                if (answer.LookupSelected)
                {
                    strOut.Append("<input type=\"checkbox\" name=\"" 
                                   + this.QuestionID.ToString() + "\" id=\"" + answer.LookupAnswerID.ToString() 
                                   + "\" value=\"" + answer.LookupAnswerID.ToString() + "\" checked=\"checked\"  />");
                }
                else
                {
                    strOut.Append("<input type=\"checkbox\" name=\"" 
                                   + this.QuestionID.ToString() + "\" id=\"" + answer.LookupAnswerID.ToString() 
                                   + "\" value=\"" + answer.LookupAnswerID.ToString() + "\"  />");
                }
                strOut.Append("<label for=\"" + answer.LookupAnswerID.ToString() + "\">" + answer.LookupAnswerName + "</label>");
                strOut.Append("</div>");
            }
            return strOut.ToString();
        }
    }
}
