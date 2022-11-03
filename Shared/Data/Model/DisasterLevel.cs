namespace Shared.Data.Model;

/// <summary>
/// Designation given by the Hero's Association to rank threats by strength and scope of destruction.
/// </summary>
public enum DisasterLevel
{
    /// <summary>
    /// Any potential threat that poses a danger to an unknown degree.
    /// </summary>
    Wolf,

    /// <summary>
    /// Any threat to a large number of people.
    /// </summary>
    Tiger,

    /// <summary>
    /// Any threat to a city and its people.
    /// </summary>
    Demon,

    /// <summary>
    /// Any threat to multiple cities.
    /// </summary>
    Dragon,

    /// <summary>
    /// A threat endangering the survival of humanity in general.
    /// </summary>
    God,
}