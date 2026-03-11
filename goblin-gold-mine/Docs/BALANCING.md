# Goblin Gold Mine - Game Balancing

## Design Principles

1. **Exponential value curve:** Each tier should feel meaningfully more rewarding than the last
2. **Income/s scales ~1.6-2x per tier:** Creates clear progression incentive
3. **Higher tiers mine slower but yield more:** Patience is rewarded
4. **Chunks increase with tier:** More visual spectacle for rarer resources
5. **Durability decreases with tier:** Rare nodes deplete faster, creating scarcity
6. **Respawn increases with tier:** Downtime makes rare nodes feel special

---

## Balance Table

### All Resources (Rebalanced)

| Tier | Resource    | Value | Interval (s) | Chunks | Durability | Respawn (s) | Income/s | Total Yield |
|------|-------------|------:|-------------:|-------:|-----------:|------------:|---------:|------------:|
| 1    | Bronze      |     1 |          0.8 |      3 |          8 |           5 |     1.25 |           8 |
| 2    | Silver      |     3 |          1.2 |      4 |          6 |           8 |     2.50 |          18 |
| 3    | Gold        |     8 |          2.0 |      5 |          5 |          12 |     4.00 |          40 |
| 4    | Diamond     |    20 |          3.0 |      6 |          4 |          18 |     6.67 |          80 |
| 5    | Obsidian    |    50 |          4.5 |      7 |          3 |          25 |    11.11 |         150 |
| 6    | Mithril     |   120 |          6.0 |      8 |          3 |          35 |    20.00 |         360 |
| 7    | Dragonstone |   300 |          8.0 |      9 |          2 |          50 |    37.50 |         600 |
| 8    | Ethereal    |   750 |         12.0 |     10 |          2 |          75 |    62.50 |        1500 |
| 9    | Goblinite   |  2000 |         18.0 |     12 |          1 |         120 |   111.11 |        2000 |

> **CollectionAmount** is always 1 for all resources (one inventory increment per collection tick).

### Income/s Progression Curve

```
Tier  Income/s  Multiplier vs Previous
  1      1.25   —
  2      2.50   2.00x
  3      4.00   1.60x
  4      6.67   1.67x
  5     11.11   1.67x
  6     20.00   1.80x
  7     37.50   1.88x
  8     62.50   1.67x
  9    111.11   1.78x
```

Average multiplier: ~1.76x per tier.

### Total Yield Progression

```
Tier  Total Yield  Multiplier vs Previous
  1            8   —
  2           18   2.25x
  3           40   2.22x
  4           80   2.00x
  5          150   1.88x
  6          360   2.40x
  7          600   1.67x
  8         1500   2.50x
  9         2000   1.33x
```

---

## Previous Values (Before Rebalance)

| Resource | Value | Interval | Chunks | Durability | Respawn | Income/s |
|----------|------:|---------:|-------:|-----------:|--------:|---------:|
| Bronze   |     1 |      1.0 |     10 |          5 |       8 |     1.00 |
| Silver   |     5 |      2.0 |      5 |          4 |      12 |     2.50 |
| Gold     |    20 |      3.0 |      3 |          3 |      15 |     6.67 |
| Diamond  |    50 |      5.0 |      2 |          2 |      20 |    10.00 |
| Obsidian |   100 |     10.0 |      1 |          2 |      25 |    10.00 |

### Issues Fixed
- **Flat income ceiling:** Diamond and Obsidian had identical income/s (10/s) — no incentive to mine Obsidian
- **Inverted chunk count:** Chunks decreased with tier (10→1) — higher tiers felt less rewarding visually
- **Unused CollectionAmount:** Always 1, so it's kept at 1 but documented

---

## Player Base Stats

| Stat                    | Base Value | Unit          |
|-------------------------|-----------|---------------|
| Move Speed              | 5         | units/s       |
| Mining Interval         | 0.8       | seconds       |
| Max Carry Capacity      | 20        | resources     |

---

## Upgrade Cost Scaling

All upgrades use **exponential cost scaling**: `baseCost × multiplier^level`

### Buy Worker
| Level | Cost  |
|-------|-------|
| 0     | 50    |
| 1     | 125   |
| 2     | 313   |
| 3     | 781   |
| 4     | 1,953 |
| 5     | 4,883 |

Base cost: 50, multiplier: 2.5x

### Mining Speed
| Level | Cost | Mining Interval |
|-------|------|-----------------|
| 0     | 30   | 0.8s → 0.7s     |
| 1     | 60   | 0.7s → 0.6s     |
| 2     | 120  | 0.6s → 0.5s     |
| 3     | 240  | 0.5s → 0.4s     |
| 4     | 480  | 0.4s → 0.3s     |
| 5     | 960  | 0.3s → 0.2s     |

Base cost: 30, multiplier: 2.0x, -0.1s per level, min 0.2s

### Move Speed
| Level | Cost | Move Speed    |
|-------|------|---------------|
| 0     | 25   | 5 → 6 u/s    |
| 1     | 45   | 6 → 7 u/s    |
| 2     | 81   | 7 → 8 u/s    |
| 3     | 146  | 8 → 9 u/s    |
| 4     | 263  | 9 → 10 u/s   |
| 5     | 473  | 10 → 11 u/s  |

Base cost: 25, multiplier: 1.8x, +1 unit/s per level, max 15 units/s

### Carry Capacity
| Level | Cost | Carry Capacity |
|-------|------|----------------|
| 0     | 20   | 20 → 25       |
| 1     | 32   | 25 → 30       |
| 2     | 51   | 30 → 35       |
| 3     | 82   | 35 → 40       |
| 4     | 131  | 40 → 45       |
| 5     | 210  | 45 → 50       |

Base cost: 20, multiplier: 1.6x, +5 per level

---

## Region Unlock Costs

| Region          | Unlock Cost | Cumulative Cost |
|-----------------|-------------|-----------------|
| Silver Cavern   | 100         | 100             |
| Gold Depths     | 500         | 600             |
| Diamond Core    | 2,500       | 3,100           |
| Obsidian Abyss  | 10,000      | 13,100          |

---

## Resource Colors

| Resource    | Hex Color | Description        |
|-------------|-----------|---------------------|
| Bronze      | #CD7F32   | Warm copper-brown   |
| Silver      | #C0C0C0   | Classic silver      |
| Gold        | #FFD700   | Rich gold           |
| Diamond     | #B9F2FF   | Light ice blue      |
| Obsidian    | #3D3D3D   | Dark volcanic glass |
| Mithril     | #7EB8D8   | Blue-silver         |
| Dragonstone | #E84545   | Fiery red           |
| Ethereal    | #9B59B6   | Mystic purple       |
| Goblinite   | #2ECC71   | Glowing green       |
