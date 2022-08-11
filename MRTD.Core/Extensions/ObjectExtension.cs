using MRTD.Core.Common;
using MRTD.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MRTD.Core.Extensions
{
    public static class ObjectExtension
    {
        public static List<T> ToList<T>(this DataTable dtExtension)
        {
            string[] _colNames = dtExtension.Columns.Cast<DataColumn>().Select(col => col.ColumnName).ToArray();
            var _properties = typeof(T).GetProperties();
            List<T> _datableList = new List<T>();
            foreach (DataRow _row in dtExtension.Rows)
            {
                var _objT = Activator.CreateInstance<T>();
                foreach (var _property in _properties)
                {
                    if (_colNames.Contains(_property.Name))
                    {
                        PropertyInfo _propertyInfo = _objT.GetType().GetProperty(_property.Name);
                        _propertyInfo.SetValue(_objT, _row[_property.Name] == DBNull.Value ? null : Convert.ChangeType(_row[_property.Name], _propertyInfo.PropertyType));
                    }
                }
                _datableList.Add(_objT);
            }
            return _datableList;
        }

        public static ApplicationSession MappingParameters(this BaseModel baseModel)
        {
            ApplicationSession mapping = new ApplicationSession();
            PropertyInfo[] _properties = baseModel.GetType().GetProperties();

            foreach (PropertyInfo _property in _properties)
            {
                PropertyInfo _objQual = baseModel.GetType().GetProperty(_property.Name);
                mapping.Set(_property.Name, _objQual.GetValue(baseModel));
            }

            return mapping;
        }

        
        private static List<KeyValuePair<string, string>> GetUnitActivityByUnitID(List<ModuleActivityResultModel> moduleActivityResultModels, int UnitID)
        {
            return moduleActivityResultModels.Where(
                                                        units => units.ModuleID == UnitID
                                                   ).Select(
                                                        unit => new KeyValuePair<string, string>(string.Format("{0} {1}", unit.ActivityName, unit.SeqOrder), unit.ActivityDate)
                                                   ).ToList();
        }

        public static List<T> ChangeTo<T>(this List<ModuleActivityResultModel> moduleActivityResultModels)
        {
            var _colNames = typeof(ModuleActivityResultModel).GetProperties().Select(col => col.Name).ToList();
            var lst_rows = moduleActivityResultModels.GroupBy(unit => unit.ModuleID).Select(unit => unit.FirstOrDefault());
            var _properties = typeof(T).GetProperties();
            List<T> dataList = new List<T>();

            foreach (var row in lst_rows)
            {
                var _objT = Activator.CreateInstance<T>();
                foreach (var _property in _properties)
                {
                    if (_colNames.Contains(_property.Name))
                    {
                        PropertyInfo _rowPropertyInfo = row.GetType().GetProperty(_property.Name);
                        PropertyInfo _propertyInfo = _objT.GetType().GetProperty(_property.Name);
                        _propertyInfo.SetValue(_objT, _rowPropertyInfo.GetValue(row, null));
                    }
                    else
                    {
                        PropertyInfo _propertyInfo = _objT.GetType().GetProperty(_property.Name);
                        _propertyInfo.SetValue(_objT, GetUnitActivityByUnitID(moduleActivityResultModels, row.ModuleID));
                    }
                }
                dataList.Add(_objT);
            }

            return dataList;
        }

        public static string StudentHeader(this string html_string, StudentModel studentModel)
        {
            html_string = html_string.Replace("#FULLNAME#", studentModel.FullName);
            html_string = html_string.Replace("#STUDENTNO#", studentModel.StudentNumber);
            html_string = html_string.Replace("#QUALIFICATIONNAME#", studentModel.QualificationName);
            html_string = html_string.Replace("#ACADEMICYEAR#", studentModel.AcademicYear.ToString());
            html_string = html_string.Replace("#QUALIFICATIONSTATUS#", studentModel.QualificationStatus);

            return html_string;
        }

        public static string ReportHeader(this string html_string, StudentModel studentModel)
        {
            html_string = html_string.Replace("#FULLNAME#", studentModel.FullName);
            html_string = html_string.Replace("#STUDENTNO#", studentModel.StudentNumber);
            html_string = html_string.Replace("#QUALIFICATIONNAME#", studentModel.QualificationName);
            html_string = html_string.Replace("#ACADEMICYEAR#", studentModel.AcademicYear.ToString());
            html_string = html_string.Replace("#QUALIFICATIONSTATUS#", studentModel.QualificationStatus);

            return html_string;
        }

        public static string StudentAcademicRecords(this string html_string, List<AcademicRecordModel> academicRecordModels)
        {
            string rowString = string.Empty;

            foreach (var record_model in academicRecordModels)
            {
                rowString += "<tr><td>" + record_model.UnitCode + "</td>"
                          + "<td>" + record_model.ModuleName + "</td>"
                          + "<td>" + record_model.ModuleNQLevel + "</td>"
                          + "<td>" + record_model.AcademicYear + "</td>"
                          + "<td>" + record_model.Mark + "</td>"
                          + "<td>" + record_model.Remark + "</td></tr>";


            }

            html_string = html_string.Replace("#MODULEBODY#", rowString);

            return html_string;
        }


        public static string StudentAcademicRecords(this string html_string, List<MemberModuleActivityModel> academicRecordModels)
        {
            string rowString = string.Empty;

            html_string = html_string.Replace("#MODULE#", academicRecordModels?.FirstOrDefault().ModuleName);
            html_string = html_string.Replace("#ACTIVITY#", academicRecordModels?.FirstOrDefault().ActivityName);

            foreach (var record_model in academicRecordModels)
            {
                rowString += "<tr><td>" + record_model.FirstName + "</td>"
                          + "<td>" + record_model.LastName + "</td>"
                          + "<td>" + record_model.UnitCode + "</td>"
                          + "<td>" + record_model.ActivityName + "</td>"
                          + "<td>" + record_model.ActivityMark + "</td></tr>";
            }

            html_string = html_string.Replace("#MODULEBODY#", rowString);

            return html_string;
        }
    }
}
