﻿namespace AssetManagement.Domain.Common.Models
{
    public class BaseEntity
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public string? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDisable { get; set;} = false;
    }
}