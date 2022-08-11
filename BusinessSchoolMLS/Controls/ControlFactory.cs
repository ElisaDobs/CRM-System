using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MRTD.Core.Models;

namespace BusinessSchoolMLS.Controls
{
    public class ControlFactory
    {
        private readonly int QuestionID;
        private readonly List<ModuleActivityAnswerModel> Lst_All_Answers;
        public ControlFactory(List<ModuleActivityAnswerModel> Lst_All_Answers, int QuestionID)
        {
            this.QuestionID = QuestionID;
            this.Lst_All_Answers = Lst_All_Answers;
        }

        public IHtmlBuilder FindControl(FieldType fieldType)
        {
            IHtmlBuilder htmlBuilder = null;
            switch (fieldType)
            {
                case FieldType.RADIOBUTTON:
                    htmlBuilder = new ControlBuilder(new RadioButtonControl(Lst_All_Answers, QuestionID));
                    break;
                case FieldType.CHECKBOX:
                    htmlBuilder = new ControlBuilder(new CheckBoxControl(Lst_All_Answers, QuestionID));
                    break;
                case FieldType.DROPDOWN:
                    htmlBuilder = new ControlBuilder(new DropDownControl(Lst_All_Answers, QuestionID));
                    break;
            }
            return htmlBuilder;
        }
    }
}
