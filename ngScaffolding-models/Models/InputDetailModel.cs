using ngScaffolding.models.Models;

namespace ngScaffolding.database.Models
{
    public class InputDetailModel
    {
       
        //Shared
        public string name { get; set; }
        public string type { get; set; } //textbox, email, textarea, select, multiselect, date, datetime
        public string sqltype { get; set; } //textbox, email, textarea, select, multiselect, date, datetime
        public string label { get; set; }
        public string placeholder { get; set; }
        public string help { get; set; }
        public string comparison { get; set; }
        public bool allowcomparisonchange { get; set; }
        public bool required { get; set; }
        public string classes { get; set; }
        public string hidden { get; set; }

        public string mask { get; set; } //999-999

        public string datasource { get; set; } //Used for select items
        public string datasourceSeedName { get; set; } //set to name, when changed use this value in search

        public string value { get; set; }
    }
}