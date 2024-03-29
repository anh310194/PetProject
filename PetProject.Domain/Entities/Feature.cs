﻿using PetProject.Domain.Common;

namespace PetProject.Domain.Entities;

public class Feature : BaseEntity
{
    public string? FeatureName { get; set; }
    public string? Description { get; set; }
    public int FeatureType { get; set; }
    public bool? IsFeature { get; set; }
    public byte Status { get; set; }
    public int Sequence { get; set; }
    public int? ParentId { get; set; }
    public IEnumerable<RoleFeature>? RoleFeatures { get; set; }
}
