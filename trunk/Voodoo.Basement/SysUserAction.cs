using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo;
using Voodoo.Model;
using Voodoo.DAL;
using Voodoo.Security;

namespace Voodoo.Basement
{
    /// <summary>
    /// 用户行为 登陆 等操作
    /// </summary>
    public class SysUserAction
    {
        #region 用户登陆
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="UserName">账号</param>
        /// <param name="PassWord">密码</param>
        /// <param name="Question">问题</param>
        /// <param name="Answer">答案</param>
        /// <returns></returns>
        public static Result UserLogin(string UserName, string PassWord, string Question, string Answer)
        {
            Result r = new Result();

            SysUser user = SysUserView.Find(string.Format("UserName='{0}'", UserName));
            if (user.UserPass == null || user.UserPass != Encrypt.Md5(PassWord))
            {
                r.Success = false;
                r.Text = "账号或密码错误";
                return r;
            }
            else
            {
                //验证问答
                if (user.SafeQuestion != Question || user.SafeAnswer != Answer)
                {
                    r.Success = false;
                    r.Text = "问题或者回答错误！";
                    return r;
                }
                else
                {
                    if (user.Enabled == false)
                    {
                        r.Success = false;
                        r.Text = "用户账号已经停用！";
                        return r;
                    }
                    else
                    {
                        //更新登陆记录
                        user.Logincount++;
                        user.LastLoginIP = WS.GetIP();
                        user.LastLoginTime = DateTime.Now;
                        SysUserView.Update(user);

                        //写入Session

                        System.Web.HttpContext.Current.Session["sys_user"] = user.ID;

                        r.Success = true;
                        r.Text = "登陆成功！";
                        return r;
                    }
                }
            }


        }
        #endregion

        #region 当前系统用户
        /// <summary>
        /// 当前系统用户
        /// </summary>
        public SysUser LocalUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["sys_user"] != null)
                {
                    return SysUserView.GetModelByID(System.Web.HttpContext.Current.Session["sys_user"].ToS());
                }
                else
                {
                    SysUser u = new SysUser();
                    u.ID = int.MinValue;
                    return u;
                }
            }
        }
        #endregion

        #region 用户增加
        /// <summary>
        /// 用户增加
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Result UserAdd(SysUser user)
        {
            Result r = new Result();
            if (SysUserView.Exist(string.Format("UserName = '{0}'", user.UserName)))
            {
                r.Success = false;
                r.Text = "这个账号已经存在，请重新选择一个账号！";
                return r;
            }
            else
            {
                SysUserView.Insert(user);
                r.Success = true;
                r.Text = "添加用户成功！";
                return r;
            }
        }
        #endregion

        #region 通过ID获取群组
        /// <summary>
        /// 通过ID获取群组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SysGroup GetGroupNameByID(int id)
        {
            if (Voodoo.Cache.Cache.GetCache("_SysGroup") == null)
            {
                Voodoo.Cache.Cache.SetCache("_SysGroup", SysGroupView.GetModelList(), 10);
            }
            return ((List<SysGroup>)Voodoo.Cache.Cache.GetCache("_SysGroup")).Where(p => p.ID == id).First();
        }
        #endregion

        #region 根据ID获得部门名
        /// <summary>
        /// 根据ID获得部门名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetDepartmentByID(int id)
        {
            if (Voodoo.Cache.Cache.GetCache("_SysDepartment") == null)
            {
                Voodoo.Cache.Cache.SetCache("_SysDepartment", SysDepartmentView.GetModelList(), 10);
            }
            return ((List<SysDepartment>)Voodoo.Cache.Cache.GetCache("_SysDepartment")).Where(p => p.ID == id).First().DepartName;
        }
        #endregion

        #region 获取用户群组列表
        /// <summary>
        /// 获取用户群组列表
        /// </summary>
        /// <returns></returns>
        public static List<SysGroup> GetSysGroup()
        {
            if (Voodoo.Cache.Cache.GetCache("_SysGroup") == null)
            {
                Voodoo.Cache.Cache.SetCache("_SysGroup", SysGroupView.GetModelList(), 10);
            }
            return ((List<SysGroup>)Voodoo.Cache.Cache.GetCache("_SysGroup"));
        }
        #endregion

        #region 系统部门
        /// <summary>
        /// 系统部门
        /// </summary>
        /// <returns></returns>
        public static List<SysDepartment> GetSysDepartment()
        {
            if (Voodoo.Cache.Cache.GetCache("_SysDepartmentList") == null)
            {
                Voodoo.Cache.Cache.SetCache("_SysDepartmentList", SysDepartmentView.GetModelList(), 10);
            }
            return (List<SysDepartment>)Voodoo.Cache.Cache.GetCache("_SysDepartmentList");
        }
        #endregion
    }
}
