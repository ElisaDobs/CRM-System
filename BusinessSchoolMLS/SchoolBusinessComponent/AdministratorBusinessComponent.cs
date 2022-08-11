using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.DAL.MRTDDataAccess;

namespace BusinessSchoolMLS.SchoolBusinessComponent
{
    public class AdministratorBusinessComponent
    {
        public AdministratorBusinessComponent()
        {
        }

        public List<MenuModel> GetMenuListByNemberID(int MemberID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("MemberID", MemberID);
                return (List<MenuModel>)CommonDataAccess.Process<MenuModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<MenuModel> GetSubMenuByParentMenuID(int ParentID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("ParentID", ParentID);
                return (List<MenuModel>)CommonDataAccess.Process<MenuModel>(model);
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<RoleModel> GetStaffRoleModels()
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter = new ApplicationSession();
                return (List<RoleModel>)CommonDataAccess.Process<RoleModel>(model);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public TemplateModel GetTemplateByTempleteID(int TemplateID)
        {
            try
            {
                ApplicationFunctionalityModel model = (ApplicationFunctionalityModel)Session.AppFunctionality[MethodBase.GetCurrentMethod().Name];
                model.ApplicationParameter = new ApplicationSession();
                model.ReturnType = DataReturnType.Fill;
                model.CommandType = CommandType.StoredProcedure;
                model.ApplicationParameter.Set("TemplateID", TemplateID);
                var lst_template = (List<TemplateModel>)CommonDataAccess.Process<TemplateModel>(model);

                return lst_template?.FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
