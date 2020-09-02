using System;
using FluentMigrator;

namespace ProductsFacadeApi.DAL.Migrations
{
    [Migration(2020010101)]
    public class AddProductsTable : Migration
    {
        public override void Up()
        {
            Create.Table("products")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("title").AsString()
                .WithColumn("description").AsString().Nullable()
                .WithColumn("price").AsDecimal().WithDefaultValue(0);

            SeedProducts();
        }

        public override void Down()
        {
            Delete.Table("products");
        }

        private void SeedProducts()
        {
            var productsList = new object[] {
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 1", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Duo Ring 2", description = "Classic yet modern, the Répertoire Duo Ring stands out with its bold simplicity. Cast from 18-karat gold, this timeless piece is a must have and would look great alone or stacked with other rings.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Tiara Hoops 3", description = "Cast from polished 18-karat gold, the Répertoire Tiara hoops are set with luminous diamonds. Minimalistic yet elegant, these contemporary hoops are perfect day to night.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 4 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 5 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 6 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 7", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 8", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Tiara Band Ring 9", description = "Casted from 18-karat gold, the delicate Répertoire Tiara Band Ring is set with sparkling round diamonds. Simple yet timeless, this modern ring looks great alone or stacked with other rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 10", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Duo Ring 12", description = "Classic yet modern, the Répertoire Duo Ring stands out with its bold simplicity. Cast from 18-karat gold, this timeless piece is a must have and would look great alone or stacked with other rings.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Tiara Hoops 13", description = "Cast from polished 18-karat gold, the Répertoire Tiara hoops are set with luminous diamonds. Minimalistic yet elegant, these contemporary hoops are perfect day to night.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 14 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 15 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 16 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 17", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 18", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Tiara Band Ring 19", description = "Casted from 18-karat gold, the delicate Répertoire Tiara Band Ring is set with sparkling round diamonds. Simple yet timeless, this modern ring looks great alone or stacked with other rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 20", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Duo Ring 21", description = "Classic yet modern, the Répertoire Duo Ring stands out with its bold simplicity. Cast from 18-karat gold, this timeless piece is a must have and would look great alone or stacked with other rings.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Tiara Hoops 23", description = "Cast from polished 18-karat gold, the Répertoire Tiara hoops are set with luminous diamonds. Minimalistic yet elegant, these contemporary hoops are perfect day to night.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 24 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 25 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Diadem Band Ring 26 (Diamond)", description = "Modern yet timeless, the Répertoire Diadem band ring is cast from 18-karat gold and encrusted with shimmering round-cut diamonds that accentuate the ring’s distinctive pattern.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 27", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Crown Ring 28", description = "The Répertoire Crown Ring is handcrafted from 18-karat gold and is accentuated with round dazzling diamonds that illuminate its bold pattern. Wear it solo or mix-match with similar rings from the collection.", price = 1020.30 },
                new { id = Guid.NewGuid(), title = "Répertoire Tiara Band Ring 29", description = "Casted from 18-karat gold, the delicate Répertoire Tiara Band Ring is set with sparkling round diamonds. Simple yet timeless, this modern ring looks great alone or stacked with other rings from the collection.", price = 1020.30 },
                
            };

            foreach (var product in productsList)
            {
                Insert.IntoTable("products").Row(product);
            }
        }
    }
}