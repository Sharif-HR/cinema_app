namespace Views
{
    public class AboutUs : ViewTemplate
    {
        public AboutUs() : base("About us.") { }

        public override void Render()
        {
            base.Render();
            Console.WriteLine(@"Welcome to Silver Cinema!

Silver Cinema is a premier cinema located in the heart of the city. We pride ourselves on providing an 
exceptional movie-watching experience for all cinephiles. With state-of-the-art facilities and comfortable
seating, we aim to transport you into the captivating world of cinema.

At Silver Cinema, we offer a wide range of movie genres,
including the latest blockbusters, classic favorites, and critically acclaimed films.
Whether you're a fan of action, romance, comedy, or adventure, we have something to cater to every taste.
Our theaters are equipped with cutting-edge audio and visual technology, ensuring that you immerse yourself
in the magic of the big screen.");

            Console.WriteLine();
            Console.WriteLine(@"Operating Hours:

- Monday to Thursday: 10:00 AM - 11:00 PM
- Friday to Sunday: 10:00 AM - 12:00 AM");

            Helpers.Continue("Press enter to return to the home page");
        }


    }
}