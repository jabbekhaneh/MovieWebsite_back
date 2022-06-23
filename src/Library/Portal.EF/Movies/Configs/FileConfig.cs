using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Movies;

namespace Portal.EF.Movies.Configs;

public class FileConfig : IEntityTypeConfiguration<Film>
{
    public void Configure(EntityTypeBuilder<Film> _)
    {
        _.HasKey(_=>_.Id);

        _.Property(_ => _.Id).IsRequired();
        _.Property(_ => _.Body);
        _.Property(_=>_.Title).IsRequired();
        _.Property(_ => _.IMDBLink).HasMaxLength(250);
        _.Property(_ => _.Revenue).HasMaxLength(250);
        _.Property(_ => _.ReleaseDate);
        _.Property(_ => _.Score);
        _.Property(_ => _.Budget);
        _.Property(_ => _.HomepageLink).HasMaxLength(250);
        _.Property(_ => _.ImageUrl);
        _.Property(_ => _.Runtime);

    }
}
