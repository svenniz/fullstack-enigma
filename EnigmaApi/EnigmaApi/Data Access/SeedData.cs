using EnigmaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public static class SeedData
    {
        public static void SeedRealData(EnigmaDbContext context)
        {
            if (!context.Cards.Any())
            {
                var blackLotus = new Card
                {
                    Name = "Black Lotus",
                    ManaCost = "{0}",
                    Type = "Artifact",
                    SetCode = "LEA",
                    Rarity = "Rare",
                    Power = null,
                    Toughness = null,
                    Description = "Tap, Sacrifice Black Lotus: Add three mana of any one color.",
                    Images = new List<Image>
                    {
                        new Image { Url = "https://example.com/images/black_lotus_1.jpg", AltText = "Black Lotus - Original Art"},
                        new Image { Url = "https://example.com/images/black_lotus_2.jpg", AltText = "Black Lotus - Alternate Art"}
                    }
                };

                var lightningBolt = new Card
                {
                    Name = "Lightning Bolt",
                    ManaCost = "{R}",
                    Type = "Instant",
                    SetCode = "LEB",
                    Rarity = "Common",
                    Power = null,
                    Toughness = null,
                    Description = "Lightning Bolt deals 3 damage to any target.",
                    Images = new List<Image>
                    {
                        new Image { Url = "https://example.com/images/lightning_bolt_1.jpg", AltText = "Lightning Bolt - Original Art"},
                        new Image { Url = "https://example.com/images/lightning_bolt_2.jpg", AltText = "Lightning Bolt - Promo"}
                    }
                };

                var serraAngel = new Card
                {
                    Name = "Serra Angel",
                    ManaCost = "{3}{W}",
                    Type = "Creature",
                    SetCode = "LEA",
                    Rarity = "Rare",
                    Power = 4,
                    Toughness = 4,
                    Description = "Flying, Vigilance. A 4/4 Angel that is always ready for battle.",
                    Images = new List<Image>
                    {
                        new Image { Url = "https://example.com/images/serra_angel_1.jpg", AltText = "Serra Angel - Original Art"},
                        new Image { Url = "https://example.com/images/serra_angel_2.jpg", AltText = "Serra Angel - Alternate Art"}
                    }
                };

                var counterspell = new Card
                {
                    Name = "Counterspell",
                    ManaCost = "{U}{U}",
                    Type = "Instant",
                    SetCode = "LEB",
                    Rarity = "Uncommon",
                    Power = null,
                    Toughness = null,
                    Description = "Counter target spell.",
                    Images = new List<Image>
                    {
                        new Image { Url = "https://example.com/images/counterspell_1.jpg", AltText = "Counterspell - Original Art"},
                        new Image { Url = "https://example.com/images/counterspell_2.jpg", AltText = "Counterspell - Promo"}
                    }
                };

                // Add cards to the context
                context.Cards.AddRange(blackLotus, lightningBolt, serraAngel, counterspell);
                context.SaveChanges();

                // Add images to the Image table (if images are added separately)
                context.Images.AddRange(new List<Image>
                {
                    // Black Lotus images
                    new Image { CardId = blackLotus.Id, Url = "https://example.com/images/black_lotus_1.jpg", AltText = "Black Lotus - Original Art"},
                    new Image { CardId = blackLotus.Id, Url = "https://example.com/images/black_lotus_2.jpg", AltText = "Black Lotus - Alternate Art"},

                    // Lightning Bolt images
                    new Image { CardId = lightningBolt.Id, Url = "https://example.com/images/lightning_bolt_1.jpg", AltText = "Lightning Bolt - Original Art"},
                    new Image { CardId = lightningBolt.Id, Url = "https://example.com/images/lightning_bolt_2.jpg", AltText = "Lightning Bolt - Promo"},

                    // Serra Angel images
                    new Image { CardId = serraAngel.Id, Url = "https://example.com/images/serra_angel_1.jpg", AltText = "Serra Angel - Original Art"},
                    new Image { CardId = serraAngel.Id, Url = "https://example.com/images/serra_angel_2.jpg", AltText = "Serra Angel - Alternate Art"},

                    // Counterspell images
                    new Image { CardId = counterspell.Id, Url = "https://example.com/images/counterspell_1.jpg", AltText = "Counterspell - Original Art"},
                    new Image { CardId = counterspell.Id, Url = "https://example.com/images/counterspell_2.jpg", AltText = "Counterspell - Promo"}
                });
                context.SaveChanges();
            }

            if (!context.Decks.Any())
            {
                var cards = context.Cards.ToList();
                var deck = new Deck
                {
                    Name = "Vintage Power",
                    Description = "A powerful vintage deck with classic cards.",
                    DeckCards = new List<DeckCard>
                    {
                        new DeckCard { Card = cards.First(c => c.Name == "Black Lotus") },
                        new DeckCard { Card = cards.First(c => c.Name == "Lightning Bolt") },
                        new DeckCard { Card = cards.First(c => c.Name == "Serra Angel") },
                        new DeckCard { Card = cards.First(c => c.Name == "Counterspell") }
                    }
                };

                // Add deck to the context
                context.Decks.Add(deck);
                context.SaveChanges();
            }
        }
    }
}
