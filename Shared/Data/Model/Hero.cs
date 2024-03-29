﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Data.Model;

public class Hero : IModel
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Rank Rank { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; set; } = Status.Alive;

    public string? Address { get; set; }
    public string? Phone { get; set; }

    #region Relations

    [JsonIgnore] public virtual List<Sponsor> Sponsors { get; set; } = new();
    [JsonIgnore] public virtual List<Fight> Fights { get; set; } = new();

    #endregion
}