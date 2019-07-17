using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitment.Utils
{
    public class Utils
    {
        public static List<MenuModels> GetMenus(List<MenuModels> tempMenus, string roleId)
        {
            List<MenuModels> menus = new List<MenuModels>();
            foreach (MenuModels menuModels in tempMenus)
            {
                String[] roleArr = menuModels.RoleId.Split(',');
                if (roleArr.Contains(roleId))
                {
                    menus.Add(menuModels);
                }
            }

            return menus;
        }
    }
}