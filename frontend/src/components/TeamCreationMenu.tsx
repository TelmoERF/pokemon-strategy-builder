"use client";

import React, { useMemo, useState } from "react";
import { Card, CardContent, CardHeader, CardTitle, CardDescription } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Switch } from "@/components/ui/switch";
import { Badge } from "@/components/ui/badge";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Separator } from "@/components/ui/separator";
import { Plus, Trash2, Sparkles } from "lucide-react";

const pokemonTypes = [
  "Normal",
  "Fire",
  "Water",
  "Electric",
  "Grass",
  "Ice",
  "Fighting",
  "Poison",
  "Ground",
  "Flying",
  "Psychic",
  "Bug",
  "Rock",
  "Ghost",
  "Dragon",
  "Dark",
  "Steel",
  "Fairy",
] as const;

const genders = ["Unknown", "Male", "Female"] as const;

type Gender = (typeof genders)[number];
type PokemonTypeName = (typeof pokemonTypes)[number];

type TeamPokemonSlot = {
  speciesName: string;
  nickname: string;
  level: number;
  item: string;
  ability: string;
  teraType: PokemonTypeName | "";
  isShiny: boolean;
  gender: Gender;
  hpEv: number;
  attackEv: number;
  defenseEv: number;
  specialAttackEv: number;
  specialDefenseEv: number;
  speedEv: number;
  moves: string[];
};

type TeamPayload = {
  name: string;
  pokemon: Array<
    Omit<TeamPokemonSlot, "teraType"> & {
      teraType: PokemonTypeName | null;
      moves: string[];
    }
  >;
};

type EvField =
  | "hpEv"
  | "attackEv"
  | "defenseEv"
  | "specialAttackEv"
  | "specialDefenseEv"
  | "speedEv";

const starterMoves: Record<string, string[]> = {
  charizard: ["flamethrower", "air-slash", "dragon-pulse", "roost"],
  garchomp: ["earthquake", "dragon-claw", "stone-edge", "swords-dance"],
  scizor: ["bullet-punch", "u-turn", "roost", "swords-dance"],
  "rotom-wash": ["hydro-pump", "volt-switch", "trick", "will-o-wisp"],
  dragonite: ["extreme-speed", "dragon-dance", "earthquake", "fire-punch"],
  lucario: ["close-combat", "extreme-speed", "meteor-mash", "swords-dance"],
};

const defaultSlot = (): TeamPokemonSlot => ({
  speciesName: "",
  nickname: "",
  level: 100,
  item: "",
  ability: "",
  teraType: "",
  isShiny: false,
  gender: "Unknown",
  hpEv: 0,
  attackEv: 0,
  defenseEv: 0,
  specialAttackEv: 0,
  specialDefenseEv: 0,
  speedEv: 0,
  moves: ["", "", "", ""],
});

const presetTeam: { name: string; pokemon: TeamPokemonSlot[] } = {
  name: "Balanced Team",
  pokemon: [
    {
      speciesName: "charizard",
      nickname: "Zard",
      level: 100,
      item: "Heavy-Duty Boots",
      ability: "Blaze",
      teraType: "Fire",
      isShiny: false,
      gender: "Male",
      hpEv: 0,
      attackEv: 0,
      defenseEv: 4,
      specialAttackEv: 252,
      specialDefenseEv: 0,
      speedEv: 252,
      moves: ["flamethrower", "air-slash", "dragon-pulse", "roost"],
    },
  ],
};

function evTotal(slot: TeamPokemonSlot): number {
  return [
    slot.hpEv,
    slot.attackEv,
    slot.defenseEv,
    slot.specialAttackEv,
    slot.specialDefenseEv,
    slot.speedEv,
  ].reduce((a, b) => a + Number(b || 0), 0);
}

export default function TeamCreationMenu() {
  const [teamName, setTeamName] = useState<string>(presetTeam.name);
  const [slots, setSlots] = useState<TeamPokemonSlot[]>(presetTeam.pokemon);
  const [activeIndex, setActiveIndex] = useState<number>(0);

  const activeSlot: TeamPokemonSlot = slots[activeIndex] ?? defaultSlot();

  const teamPayload = useMemo<TeamPayload>(
    () => ({
      name: teamName,
      pokemon: slots.map((slot: TeamPokemonSlot) => ({
        ...slot,
        teraType: slot.teraType || null,
        moves: slot.moves.filter(Boolean),
      })),
    }),
    [teamName, slots]
  );

  const updateSlot = (index: number, patch: Partial<TeamPokemonSlot>) => {
    setSlots((current: TeamPokemonSlot[]) =>
      current.map((slot: TeamPokemonSlot, i: number) =>
        i === index ? { ...slot, ...patch } : slot
      )
    );
  };

  const updateMove = (index: number, moveIndex: number, value: string) => {
    setSlots((current: TeamPokemonSlot[]) =>
      current.map((slot: TeamPokemonSlot, i: number) => {
        if (i !== index) return slot;
        const moves = [...slot.moves];
        moves[moveIndex] = value;
        return { ...slot, moves };
      })
    );
  };

  const addSlot = () => {
    if (slots.length >= 6) return;
    const next = [...slots, defaultSlot()];
    setSlots(next);
    setActiveIndex(next.length - 1);
  };

  const removeSlot = (index: number) => {
    const next = slots.filter((_, i) => i !== index);
    setSlots(next);
    setActiveIndex(Math.max(0, Math.min(activeIndex, next.length - 1)));
  };

  const evFields: Array<[EvField, string]> = [
    ["hpEv", "HP"],
    ["attackEv", "Attack"],
    ["defenseEv", "Defense"],
    ["specialAttackEv", "Sp. Attack"],
    ["specialDefenseEv", "Sp. Defense"],
    ["speedEv", "Speed"],
  ];

  return (
    <div className="min-h-screen bg-slate-50 p-6">
      <div className="mx-auto max-w-7xl space-y-6">
        <div className="flex items-start justify-between gap-4">
          <div>
            <h1 className="text-3xl font-bold tracking-tight">Pokémon Team Builder</h1>
            <p className="mt-1 text-slate-600">
              Create competitive team slots with items, abilities, EVs, tera types, and moves.
            </p>
          </div>
          <Badge className="rounded-xl px-3 py-1 text-sm">Phase 1 UI</Badge>
        </div>

        <div className="grid gap-6 lg:grid-cols-[320px_1fr]">
          <Card className="rounded-2xl shadow-sm">
            <CardHeader>
              <CardTitle>Team Overview</CardTitle>
              <CardDescription>
                Manage your team slots and quickly switch between Pokémon.
              </CardDescription>
            </CardHeader>
            <CardContent className="space-y-4">
              <div className="space-y-2">
                <Label htmlFor="team-name">Team name</Label>
                <Input
                  id="team-name"
                  value={teamName}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => setTeamName(e.target.value)}
                  placeholder="Balanced Team"
                />
              </div>

              <Separator />

              <div className="space-y-2">
                {slots.map((slot: TeamPokemonSlot, index: number) => (
                  <button
                    key={index}
                    onClick={() => setActiveIndex(index)}
                    className={`w-full rounded-2xl border p-3 text-left transition ${
                      activeIndex === index
                        ? "border-slate-900 bg-slate-900 text-white"
                        : "border-slate-200 bg-white hover:bg-slate-50"
                    }`}
                  >
                    <div className="flex items-center justify-between gap-3">
                      <div>
                        <div className="font-semibold">Slot {index + 1}</div>
                        <div
                          className={`text-sm ${
                            activeIndex === index ? "text-slate-200" : "text-slate-500"
                          }`}
                        >
                          {slot.nickname || slot.speciesName || "Empty slot"}
                        </div>
                      </div>
                      <Button
                        type="button"
                        variant="ghost"
                        size="icon"
                        className={`rounded-xl ${
                          activeIndex === index ? "hover:bg-slate-800" : "hover:bg-slate-100"
                        }`}
                        onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
                          e.stopPropagation();
                          removeSlot(index);
                        }}
                      >
                        <Trash2 className="h-4 w-4" />
                      </Button>
                    </div>
                  </button>
                ))}
              </div>

              <Button className="w-full rounded-2xl" onClick={addSlot} disabled={slots.length >= 6}>
                <Plus className="mr-2 h-4 w-4" />
                Add Pokémon
              </Button>
            </CardContent>
          </Card>

          <div className="space-y-6">
            <Card className="rounded-2xl shadow-sm">
              <CardHeader>
                <CardTitle>Slot Editor</CardTitle>
                <CardDescription>
                  Configure species, metadata, EV spread, and moves for the selected Pokémon.
                </CardDescription>
              </CardHeader>
              <CardContent>
                <Tabs defaultValue="basic" className="w-full">
                  <TabsList className="grid w-full grid-cols-3 rounded-2xl">
                    <TabsTrigger value="basic">Basic</TabsTrigger>
                    <TabsTrigger value="evs">EVs</TabsTrigger>
                    <TabsTrigger value="moves">Moves</TabsTrigger>
                  </TabsList>

                  <TabsContent value="basic" className="mt-6 space-y-4">
                    <div className="grid gap-4 md:grid-cols-2">
                      <div className="space-y-2">
                        <Label>Species name</Label>
                        <Input
                          value={activeSlot.speciesName}
                          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                            updateSlot(activeIndex, { speciesName: e.target.value })
                          }
                          placeholder="charizard"
                        />
                      </div>

                      <div className="space-y-2">
                        <Label>Nickname</Label>
                        <Input
                          value={activeSlot.nickname}
                          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                            updateSlot(activeIndex, { nickname: e.target.value })
                          }
                          placeholder="Zard"
                        />
                      </div>

                      <div className="space-y-2">
                        <Label>Level</Label>
                        <Input
                          type="number"
                          min={1}
                          max={100}
                          value={activeSlot.level}
                          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                            updateSlot(activeIndex, { level: Number(e.target.value) })
                          }
                        />
                      </div>

                      <div className="space-y-2">
                        <Label>Gender</Label>
<Select
  value={activeSlot.gender}
  onValueChange={(value: Gender | null) =>
    updateSlot(activeIndex, { gender: value ?? "Unknown" })
  }
>
                          <SelectTrigger>
                            <SelectValue placeholder="Select gender" />
                          </SelectTrigger>
                          <SelectContent>
                            {genders.map((gender) => (
                              <SelectItem key={gender} value={gender}>
                                {gender}
                              </SelectItem>
                            ))}
                          </SelectContent>
                        </Select>
                      </div>

                      <div className="space-y-2">
                        <Label>Item</Label>
                        <Input
                          value={activeSlot.item}
                          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                            updateSlot(activeIndex, { item: e.target.value })
                          }
                          placeholder="Heavy-Duty Boots"
                        />
                      </div>

                      <div className="space-y-2">
                        <Label>Ability</Label>
                        <Input
                          value={activeSlot.ability}
                          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                            updateSlot(activeIndex, { ability: e.target.value })
                          }
                          placeholder="Blaze"
                        />
                      </div>

                      <div className="space-y-2">
                        <Label>Tera type</Label>
                        <Select
  value={activeSlot.teraType || "none"}
  onValueChange={(value: string | null) =>
    updateSlot(activeIndex, {
      teraType: !value || value === "none" ? "" : (value as PokemonTypeName),
    })
  }
>
                          <SelectTrigger>
                            <SelectValue placeholder="Select tera type" />
                          </SelectTrigger>
                          <SelectContent>
                            <SelectItem value="none">None</SelectItem>
                            {pokemonTypes.map((type) => (
                              <SelectItem key={type} value={type}>
                                {type}
                              </SelectItem>
                            ))}
                          </SelectContent>
                        </Select>
                      </div>

                      <div className="flex items-end gap-3 rounded-2xl border p-4">
                        <Switch
                          checked={activeSlot.isShiny}
                          onCheckedChange={(checked: boolean) =>
                            updateSlot(activeIndex, { isShiny: checked })
                          }
                        />
                        <div>
                          <div className="font-medium">Shiny</div>
                          <div className="text-sm text-slate-500">Toggle alternate shiny variant</div>
                        </div>
                      </div>
                    </div>
                  </TabsContent>

                  <TabsContent value="evs" className="mt-6 space-y-4">
                    <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
                      {evFields.map(([field, label]) => (
                        <div key={field} className="space-y-2">
                          <Label>{label}</Label>
                          <Input
                            type="number"
                            min={0}
                            max={252}
                            value={activeSlot[field]}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                              updateSlot(activeIndex, { [field]: Number(e.target.value) } as Partial<TeamPokemonSlot>)
                            }
                          />
                        </div>
                      ))}
                    </div>

                    <div
                      className={`rounded-2xl border p-4 text-sm ${
                        evTotal(activeSlot) > 510
                          ? "border-red-300 bg-red-50 text-red-700"
                          : "border-slate-200 bg-white text-slate-600"
                      }`}
                    >
                      Total EVs: <span className="font-semibold">{evTotal(activeSlot)} / 510</span>
                    </div>
                  </TabsContent>

                  <TabsContent value="moves" className="mt-6 space-y-4">
                    <div className="grid gap-4 md:grid-cols-2">
                      {activeSlot.moves.map((move: string, moveIndex: number) => (
                        <div key={moveIndex} className="space-y-2">
                          <Label>Move {moveIndex + 1}</Label>
                          <Input
                            value={move}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                              updateMove(activeIndex, moveIndex, e.target.value)
                            }
                            placeholder={starterMoves[activeSlot.speciesName]?.[moveIndex] || "earthquake"}
                          />
                        </div>
                      ))}
                    </div>
                  </TabsContent>
                </Tabs>
              </CardContent>
            </Card>

            <Card className="rounded-2xl shadow-sm">
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Sparkles className="h-5 w-5" />
                  API Preview
                </CardTitle>
                <CardDescription>
                  This is the JSON your backend endpoints can receive right now.
                </CardDescription>
              </CardHeader>
              <CardContent>
                <pre className="max-h-[420px] overflow-auto rounded-2xl bg-slate-950 p-4 text-sm text-slate-100">
                  {JSON.stringify(teamPayload, null, 2)}
                </pre>
              </CardContent>
            </Card>
          </div>
        </div>
      </div>
    </div>
  );
}