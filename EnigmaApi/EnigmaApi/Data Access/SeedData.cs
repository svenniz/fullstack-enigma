﻿using EnigmaApi.Cards.Models;
using EnigmaApi.DeckCards.Models;
using EnigmaApi.Decks.Models;
using EnigmaApi.Decks.Services;
using EnigmaApi.Images.Models;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public static class SeedData
    {
        public static async Task SeedRealData(EnigmaDbContext context, IDeckService deckService)
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
                        new Image { Url = "https://cards.scryfall.io/normal/front/4/a/4a2e428c-dd25-484c-bbc8-2d6ce10ef42c.jpg?1559591808", AltText = "Black Lotus - Unlimited Art"},
                        new Image { Url = "https://cards.scryfall.io/normal/front/b/d/bd8fa327-dd41-4737-8f19-2cf5eb1f7cdd.jpg?1614638838", AltText = "Black Lotus - Vintage Masters Art"}
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
                        new Image { Url = "https://cards.scryfall.io/normal/front/d/5/d573ef03-4730-45aa-93dd-e45ac1dbaf4a.jpg?1559591645", AltText = "Lightning Bolt - Original Art from Alpha"},
                        new Image { Url = "https://cards.scryfall.io/normal/front/7/7/77c6fa74-5543-42ac-9ead-0e890b188e99.jpg?1706239968", AltText = "Lightning Bolt - Ravnica Clue Edition"}
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
                        new Image { Url = "https://cards.scryfall.io/normal/front/f/8/f8ac5006-91bd-4803-93da-f87cf196dd2f.jpg?1559591394", AltText = "Serra Angel - Original Alpha Art"},
                        new Image { Url = "https://cards.scryfall.io/normal/front/3/c/3cee9303-9d65-45a2-93d4-ef4aba59141b.jpg?1730489152", AltText = "Serra Angel - Foundations Art"}
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
                        new Image { Url = "https://cards.scryfall.io/normal/front/7/c/7c666b4b-c4ff-40ca-9d16-c76aafebaa83.jpg?1559592109", AltText = "Counterspell - Original Art"},
                        new Image { Url = "https://cards.scryfall.io/normal/front/4/f/4f616706-ec97-4923-bb1e-11a69fbaa1f8.jpg?1726837907", AltText = "Counterspell - Duskmourn Commander"}
                    }
                };

                // Add cards to the context
                context.Cards.AddRange(blackLotus, lightningBolt, serraAngel, counterspell);
                context.SaveChanges();
            }

            if (!context.Decks.Any())
            {
                var cards = context.Cards.ToList();
                var deck = new Deck
                {
                    Name = "Vintage Power",
                    Description = "A powerful vintage deck with classic cards."
                };

                // Add the deck to the context
                context.Decks.Add(deck);
                context.SaveChanges(); // Save to generate the deck ID

                // Add cards to the deck using AddCardToDeckAsync
                await deckService.AddCardToDeckAsync(deck.Id, cards.First(c => c.Name == "Black Lotus").Id); // Add Black Lotus
                await deckService.AddCardToDeckAsync(deck.Id, cards.First(c => c.Name == "Lightning Bolt").Id); // Add Lightning Bolt
                await deckService.AddCardToDeckAsync(deck.Id, cards.First(c => c.Name == "Serra Angel").Id); // Add Serra Angel
                await deckService.AddCardToDeckAsync(deck.Id, cards.First(c => c.Name == "Counterspell").Id); // Add Counterspell

                context.SaveChanges(); // Save the changes to the deck
            }
        }
    }
}
