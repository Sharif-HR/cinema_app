namespace Views
{
    public class SelectedMovie : ViewTemplate
    {
        private int _movieId;
        private MovieLogic _movieLogic = new();

        public SelectedMovie(int movieId) : base("Movie reservation")
        {
            this._movieId = movieId;
        }

        public override void Render()
        {
            base.Render();
            PrintSelectedMovie(_movieId);
        }



        private void PrintSelectedMovie(int movieId)
        {
            var movies = _movieLogic.GetMovies();
            Helpers.WarningMessage("Selected movie:");
            Console.WriteLine($@"Title: {movies[movieId].Title}
Duration: {movies[movieId].Duration}
Summary: {movies[movieId].Summary}
Genres: {Helpers.ListToString(movies[movieId].Genres)}
Director: {movies[movieId].Director}
Release date: {movies[movieId].ReleaseDate}
Show time: {movies[movieId].ShowTime}");


            
            Helpers.Continue("Press any key to return to the Movie selection menu.");
        }
    }
}