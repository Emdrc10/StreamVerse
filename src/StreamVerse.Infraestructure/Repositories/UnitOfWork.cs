namespace StreamVerse.Infraestructure.Repositories
{
    public class UnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context,
           MovieRepository movie,
           SerieRepository serie,
           GenreRepository genre,
           UserRepository user)
        {
            _context = context;
            Movie = movie;
            Serie = serie;
            Genre = genre;
            User = user;
        }

        public MovieRepository Movie { get; private set; }
        public SerieRepository Serie { get; private set; }
        public GenreRepository Genre { get; private set; }

        public UserRepository User { get; private set; }

        public void complete()
        {
            _context.SaveChanges();
        }

        public void BeginTransaction() 
        {
            _context.Database.BeginTransaction();
        }
        public void CommitTransaction() 
        {
            _context.Database.CommitTransaction();
        }
        public void RollbackTransition() 
        {
            _context.Database.RollbackTransaction();
        }
    }
}
