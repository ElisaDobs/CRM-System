
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MRTD.Core.Models;
using BusinessSchoolMLS.SchoolBusinessComponent;

namespace BusinessSchoolMLS.Controls
{
    public class HtmlGenerator
    {
        private readonly int MemberID;
        private readonly int ActivityID;
        public HtmlGenerator(int MemberID, int ActivityID)
        {
            this.MemberID = MemberID;
            this.ActivityID = ActivityID;
        }

        public string GetGeneratedHtmlBody()
        {
            string outStr = string.Empty;
            OnlineModuleActivityBusinessComponent onlineQuestionnaireComponent = new OnlineModuleActivityBusinessComponent();
            var lst_all_questions = onlineQuestionnaireComponent.GetModuleActivityQuestionByModuleActivityID(ActivityID);
            int question_counter = 1;
            foreach (var questionModel in lst_all_questions)
            {
                var lst_all_answers = onlineQuestionnaireComponent.GetOnlineModuleAnswersByQuestionID(
                                                                                                        this.MemberID, 
                                                                                                        questionModel.ActivityQuestionID
                                                                                                     );
                var ctrlFactory = new ControlFactory(
                                                        lst_all_answers, 
                                                        questionModel.ActivityQuestionID
                                                    );
                outStr += "<div class=\"business-school-question-description\"><span style=\"font-weight: bold;\">" + question_counter.ToString() + "&nbsp;&nbsp;</span>"
                            + questionModel.QuestionName + "</div>";
                outStr += "<div class=\"business-school-question-input-field\">" 
                            + ctrlFactory.FindControl((FieldType)Enum.Parse(typeof(FieldType), questionModel.FieldTypeID.ToString())).Get() + "</div>";

                question_counter++;
            }
            return outStr;
        }
    }
}
