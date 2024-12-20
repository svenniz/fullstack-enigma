﻿// <auto-generated />
using System;
using EnigmaApi.Data_Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnigmaApi.Migrations
{
    [DbContext(typeof(EnigmaDbContext))]
    partial class EnigmaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EnigmaApi.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArtistName")
                        .HasColumnType("longtext");

                    b.Property<string>("Cmc")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Language")
                        .HasColumnType("longtext");

                    b.Property<string>("ManaCost")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Power")
                        .HasColumnType("int");

                    b.Property<string>("Rarity")
                        .HasColumnType("longtext");

                    b.Property<string>("ReleasedAt")
                        .HasColumnType("longtext");

                    b.Property<string>("SetCode")
                        .HasColumnType("longtext");

                    b.Property<string>("SetName")
                        .HasColumnType("longtext");

                    b.Property<int?>("Toughness")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("EnigmaApi.Models.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("EnigmaApi.Models.DeckCard", b =>
                {
                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("DeckId", "CardId");

                    b.HasIndex("CardId");

                    b.ToTable("DeckCards");
                });

            modelBuilder.Entity("EnigmaApi.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AltText")
                        .HasColumnType("longtext");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("EnigmaApi.Models.DeckCard", b =>
                {
                    b.HasOne("EnigmaApi.Models.Card", "Card")
                        .WithMany("DeckCards")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnigmaApi.Models.Deck", "Deck")
                        .WithMany("DeckCards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("EnigmaApi.Models.Image", b =>
                {
                    b.HasOne("EnigmaApi.Models.Card", "Card")
                        .WithMany("Images")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("EnigmaApi.Models.Card", b =>
                {
                    b.Navigation("DeckCards");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("EnigmaApi.Models.Deck", b =>
                {
                    b.Navigation("DeckCards");
                });
#pragma warning restore 612, 618
        }
    }
}
