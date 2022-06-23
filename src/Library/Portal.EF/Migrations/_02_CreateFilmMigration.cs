using FluentMigrator;

namespace Portal.EF.Migrations;

[Migration(02)]
public class _02_CreateFilmMigration : Migration
{

    public override void Up()
    {
        CreateFilm();
    }


    public override void Down()
    {
        Delete.Table("Films");
    }

    private void CreateFilm()
    {
        Create.Table("Films")
               .WithColumn("Id").AsGuid().PrimaryKey().Unique()
               .WithColumn("Title").AsString(250).NotNullable()
               .WithColumn("Body").AsString(int.MaxValue).NotNullable()
               .WithColumn("Tags").AsString().Nullable()
               .WithColumn("ImageUrl").AsString(500).Nullable()
               .WithColumn("Budget").AsDecimal().Nullable()
               .WithColumn("Revenue").AsDecimal().Nullable()
               .WithColumn("ReleaseDate").AsDateTime2().Nullable()
               .WithColumn("Runtime").AsDateTime2().Nullable()
               .WithColumn("Score").AsInt16().Nullable()
               .WithColumn("IMDBLink").AsString(250).Nullable()
               .WithColumn("HomepageLink").AsString(250).Nullable();


    }
}
