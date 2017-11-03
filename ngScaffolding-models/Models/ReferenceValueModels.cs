﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ngScaffolding.models.Models;

namespace ngScaffolding.database.Models
{
    public class ReferenceValue : BaseEntity
    {
        public const string GroupNames_Application = "Application";

        public const string Types_SingleValue = "SingleValue";
        public const string Types_DatabaseQuery = "DatabaseQuery";
        public const string Types_List = "List";

        public ReferenceValue()
        {
            ReferenceValueItems = new List<ReferenceValueItem>();
        }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string GroupName { get; set; }

        [StringLength(10)]
        public string Type { get; set; }
        public string Value { get; set; }

        [StringLength(50)]
        public string ConnectionName { get; set; }
        public int? CacheSeconds { get; set; }
        public bool Authorisation { get; set; }
        public string InputDetails { get; set; }

        public virtual ICollection<ReferenceValueItem> ReferenceValueItems { get; set; }
    }

    public class ReferenceValueItem : BaseEntity
    {
        [StringLength(100)]
        public string Display { get; set; }

        [StringLength(1000)]
        public string Value { get; set; }

        public int? ItemOrder { get; set; }

        public int ReferenceValueId { get; set; }
        [ForeignKey("ReferenceValueId")]
        public virtual ReferenceValue ReferenceValue { get; set; }
    }
}