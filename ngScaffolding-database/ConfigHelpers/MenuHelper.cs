using ngScaffolding.database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ngScaffolding.database.ConfigHelpers
{
    public class MenuHelper
    {
        public static MenuItem AddMenu(ngScaffoldingContext context, MenuItem menu) {
            MenuItem newMenu = null;

            if(context.MenuItems.Any(m => m.label == menu.label))
            {
                newMenu = context.MenuItems.First(m => m.label == menu.label);
            }
            else
            {
                newMenu = new MenuItem() { label = menu.label };
                context.MenuItems.Add(newMenu);
            }

            newMenu.ParentMenuItemId = menu.ParentMenuItemId;
            newMenu.ItemOrder = menu.ItemOrder;
            newMenu.JsonSerialized = menu.JsonSerialized;
            newMenu.command = menu.command;
            newMenu.routerLink = menu.routerLink;
            newMenu.routerLinkActiveOptions = menu.routerLinkActiveOptions;
            newMenu.target = menu.target;
            newMenu.separator = menu.separator;
            newMenu.badge = menu.badge;
            newMenu.badgeStyleClass = menu.badgeStyleClass;
            newMenu.style = menu.style;
            newMenu.styleClass = menu.styleClass;

            context.SaveChanges();

            return newMenu;
        }
    }
}
