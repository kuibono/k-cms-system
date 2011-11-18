using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Voodoo.Model;

namespace Voodoo.DAL
{
    public partial class UserGroupView
    {
        public static UserForm GetUserForm(UserGroup group)
        {
            return UserFormView.GetModelByID(group.RegForm.ToS());
        }
    }
}
