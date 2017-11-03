using System.Collections.Generic;
using ngScaffolding.models.Models;

namespace ngScaffolding.database.Models
{
    public class MenuDetailModel
    {
        public bool waitForInput { get; set; }

        public string pageSize { get; set; }
        public bool infiniteScroll { get; set; }

        public string detailUrl { get; set; }
        public string detailTarget { get; set; }

        public bool serverPagination { get; set; }
        public bool serverSorting { get; set; }
        public bool serverGrouping { get; set; }

        public string defaultSort { get; set; }

        public ICollection<ColumnModel> columns { get; set; }
        public ICollection<ColumnModel> configuredColumns { get; set; }

        public bool inlineFilters { get; set; }

        public ICollection<InputDetailModel> filters { get; set; }

        public CommandModel selectCommand { get; set; }
        public CommandModel deleteCommand { get; set; }
        public CommandModel updateCommand { get; set; }
        public CommandModel insertCommand { get; set; }

        public ICollection<ActionModel> actions { get; set; }
    }

    public class ColumnModel
    {
        private string _field;

        public string field
        {
            get { return _field; }
            set
            {
                if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(headerName))
                {
                    headerName = value;
                }
                _field = value;
            }
        }

        public string cellClass { get; set; }
        public string filter { get; set; }
        public string tooltipField { get; set; }
        public string headerName { get; set; }
        public string headerTooltip { get; set; }
        public string pinned { get; set; } // left or right
        public bool suppressMenu { get; set; }
        public bool suppressFilter { get; set; }
        public bool suppressSorting { get; set; }
        //public int minWidth { get; set; }
        //public int maxWidth { get; set; }
        public string type { get; set; }
        public bool hide { get; set; }
        public string width { get; set; }

        public string cellFormatter { get; set; }
        public string cellClassRules { get; set; }

        //Link & Action Buttons
        public string destinationUrl { get; set; }
        public string target { get; set; }
        public string buttonTitle { get; set; }
        public string buttonIcon { get; set; }
    }

    public class CommandModel
    {
        public string connection { get; set; }
        public string sqlCommand { get; set; }
        public string testCommand { get; set; }
        public string confirmationMessage { get; set; }
        public string success { get; set; }
        public string failure { get; set; }
        public bool isStoredProc { get; set; }
        public bool isAudit { get; set; }
        public string flushDataSource { get; set; }
        public ICollection<ParameterDetailModel> parameters { get; set; }

        public ICollection<InputDetailModel> inputControls { get; set; }
    }

    public class ParameterDetailModel
    {
        public string name { get; set; }
        public string sqltype { get; set; }
        public object value { get; set; }
    }

    public class ActionModel
    {
        public class ActionTypes
        {
            public const string SqlCommand = "SQLCOMMAND";
            public const string Url = "URL";
            public const string Detail = "DETAIL";
            public const string AngularController = "ANGULARCONTROLLER";
        }
        public string title { get; set; }
        public string icon { get; set; }
        public string colour { get; set; }
        public bool columnButton { get; set; }
        public bool selectionRequired { get; set; }
        public string flushDataSource { get; set; }
        public bool multipleTarget { get; set; }
        public string confirmationMessage { get; set; }
        public string type { get; set; }
        public string idField { get; set; }
        public string idValue { get; set; }
        public string entityType { get; set; }
        public string additionalProperties { get; set; }
        public ICollection<InputDetailModel> inputControls { get; set; }
        public bool refresh { get; set; }

        public bool isAudit { get; set; }
        public string success { get; set; }
        public string error { get; set; }

        //SQL Action
        public CommandModel sqlCommand { get; set; }

        //Angular Controller content
        public string controller { get; set; }
        public string templateUrl { get; set; }

        //Standard Url
        public string url { get; set; }
        public string target { get; set; }

        //        _blank - URL is loaded into a new window.This is default
        //_parent - URL is loaded into the parent frame
        //_self - URL replaces the current page
        //_top - URL replaces any framesets that may be loaded
    }

}