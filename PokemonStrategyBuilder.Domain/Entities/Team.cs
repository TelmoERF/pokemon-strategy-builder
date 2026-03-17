using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Domain.Entities;

public class Team
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private readonly List<TeamPokemon> _pokemon = [];
    public IReadOnlyCollection<TeamPokemon> Pokemon => _pokemon.AsReadOnly();

    private Team()
    {
    }

    public Team(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Team name cannot be empty.", nameof(name));
        }

        Name = name.Trim();
    }

    public TeamPokemon AddPokemon(
    Pokemon pokemon,
    string nickname,
    int level,
    string item,
    string ability,
    PokemonType? teraType,
    bool isShiny,
    PokemonGender gender,
    int hpEv,
    int attackEv,
    int defenseEv,
    int specialAttackEv,
    int specialDefenseEv,
    int speedEv)
{
    if (_pokemon.Count >= 6)
    {
        throw new InvalidOperationException("A team cannot have more than 6 Pokémon.");
    }

    if (_pokemon.Any(x => x.PokemonId == pokemon.Id))
    {
        throw new InvalidOperationException("This Pokémon is already in the team.");
    }

    var teamPokemon = new TeamPokemon(
        pokemonId: pokemon.Id,
        nickname: nickname,
        level: level,
        item: item,
        ability: ability,
        teraType: teraType,
        isShiny: isShiny,
        gender: gender,
        hpEv: hpEv,
        attackEv: attackEv,
        defenseEv: defenseEv,
        specialAttackEv: specialAttackEv,
        specialDefenseEv: specialDefenseEv,
        speedEv: speedEv);

    _pokemon.Add(teamPokemon);

    return teamPokemon;
}


    public void ReplacePokemon(IEnumerable<(Pokemon Pokemon, TeamPokemon Slot)> pokemonSlots)
{
    var slotList = pokemonSlots.ToList();

    if (slotList.Count == 0)
    {
        throw new InvalidOperationException("A team must contain at least one Pokémon.");
    }

    if (slotList.Count > 6)
    {
        throw new InvalidOperationException("A team cannot have more than 6 Pokémon.");
    }

    if (slotList.Select(x => x.Pokemon.Id).Distinct().Count() != slotList.Count)
    {
        throw new InvalidOperationException("A team cannot contain duplicate Pokémon.");
    }

    _pokemon.Clear();

    foreach (var slot in slotList)
    {
        var newSlot = new TeamPokemon(
            pokemonId: slot.Pokemon.Id,
            nickname: slot.Slot.Nickname,
            level: slot.Slot.Level,
            item: slot.Slot.Item,
            ability: slot.Slot.Ability,
            teraType: slot.Slot.TeraType,
            isShiny: slot.Slot.IsShiny,
            gender: slot.Slot.Gender,
            hpEv: slot.Slot.HpEv,
            attackEv: slot.Slot.AttackEv,
            defenseEv: slot.Slot.DefenseEv,
            specialAttackEv: slot.Slot.SpecialAttackEv,
            specialDefenseEv: slot.Slot.SpecialDefenseEv,
            speedEv: slot.Slot.SpeedEv);

        var moves = slot.Slot.Moves
            .OrderBy(m => m.Slot)
            .Select(m => m.Move)
            .ToList();

        newSlot.SetMoves(moves);

        _pokemon.Add(newSlot);
    }
}
}
