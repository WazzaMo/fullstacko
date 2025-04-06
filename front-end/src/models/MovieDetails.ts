
/*

Example JSON response from the backend:
x = {
    "title": "Star Wars: Episode II - Attack of the Clones",
    "year": "2002",
    "rated": "PG", 
    "released": "16 May 2002",
    "runtime": "142 min",
    "genre": "Action, Adventure, Fantasy",
    "director": "George Lucas",
    "writer": "George Lucas (screenplay), Jonathan Hales (screenplay), George Lucas (story by)",
    "actors": "Ewan McGregor, Natalie Portman, Hayden Christensen, Christopher Lee",
    "plot": "Ten years after initially meeting, Anakin Skywalker shares a forbidden romance with Padmé, while Obi-Wan investigates an assassination attempt on the Senator and discovers a secret clone army crafted for the Jedi.",
    "language": "English",
    "country": "USA",
    "poster": "https://m.media-amazon.com/images/M/MV5BNDRkYzA4OGYtOTBjYy00YzFiLThhYmYtMWUzMDBmMmZkM2M3XkEyXkFqcGdeQXVyNDYyMDk5MTU@._V1_SX300.jpg",
    "metascore": "54",
    "rating": "6.7",
    "votes": "469,134",
    "id": "cw0121765",
    "type": "movie",
    "price": "12.5"
}
*/

export type MovieDetailsModel = {
    title: string;
    year: string;
    rated: string;
    released: string;
    runtime: string;
    genre: string;
    director: string;
    writer: string;
    actors: string;
    plot: string;
    language: string;
    country: string;
    poster: string;
    metascore: string;
    rating: string;
    votes: string;
    id: string;
    type: string;
    price: string;
}

