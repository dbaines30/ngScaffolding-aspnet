using System.Collections.Generic;
using ngScaffolding.models.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ngScaffolding.database.Models
{
       
    public class MenuElement: BaseEntity
    {
        public enum Types
        {
            Folder, GridView, Dashboard, View
        }

        public int? ItemOrder { get; set; }
        public string JsonSerialized { get; set; }

        // Following are copied from PrimeNG MenuItem

        public string label { get; set; }
        public string icon { get; set; }
        public string command { get; set; }
        public string url { get; set; }
        public string routerLink { get; set; }
        
        public string target { get; set; }
        public string routerLinkActiveOptions { get; set; }
        public bool separator { get; set; }
        public string badge { get; set; }
        public string badgeStyleClass { get; set; }
        public string style { get; set; }
        public string styleClass { get; set; }
    }

    public class MenuItem : MenuElement
    {
        public MenuItem()
        {
            items = new List<MenuItem>();
        }

        public MenuItem(MenuItem menu):base()
        {
            label = menu.label;
            ParentMenuItemId = menu.ParentMenuItemId;
            ItemOrder = menu.ItemOrder;
            JsonSerialized = menu.JsonSerialized;
            command = menu.command;
            routerLink = menu.routerLink;
            routerLinkActiveOptions = menu.routerLinkActiveOptions;
            target = menu.target;
            separator = menu.separator;
            badge = menu.badge;
            badgeStyleClass = menu.badgeStyleClass;
            style = menu.style;
            styleClass = menu.styleClass;
        }
        // Following are copied from PrimeNG MenuItem

        public bool expanded { get; set; }
        public bool disabled { get; set; }
        public bool visible { get; set; }

        public int? ParentMenuItemId { get; set; }
        [ForeignKey("ParentMenuItemId")]
        public virtual MenuItem ParentMenuItem { get; set; }

    public IEnumerable<MenuItem> items { get; set; }
    }


}