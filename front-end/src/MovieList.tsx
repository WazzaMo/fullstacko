import React, { useEffect, useState, useRef } from 'react';

import {
    Image,
    Accordion, AccordionItem, AccordionHeader, AccordionBody,
    Spinner,
} from 'react-bootstrap';

import { MovieBanner } from './models/MovieBanner';
import Nothing from './Nothing.webp';
import ErrorBox from './ErrorBox';
import MovieDetails from './MovieDetails';

export default function MovieList() {
    const url = 'http://localhost:8080/api/Movies';
    const [movies, setMovies] = useState<MovieBanner[] | undefined>([]);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await fetch(url);
                if (response.ok) {
                    const data: Array<MovieBanner> = await response.json();
                    setMovies(data);
                } else {
                    setMovies(undefined);
                }
            } catch (error) {
                console.error('Error fetching movies:', error);
                setMovies(undefined);
                setTimeout(() => {
                    setMovies([]);
                }, 2000);
            }
        };
        fetchMovies();
    }, []);
    const [show, setShow] = useState(false);

    const movieList = movies?.map((movie, idx) => {
        return (
            <AccordionItem key={movie.id} eventKey={idx.toString()}>
                <AccordionHeader>
                    <h2>{movie.title}</h2>
                </AccordionHeader>
                <AccordionBody>
                    <p>{movie.year}</p>
                    <Image src={movie.poster} height={64} width={64} onError={(e) => {
                        e.currentTarget.src = Nothing;
                    }} />
                    <MovieDetails movieId={movie.id} />
                </AccordionBody>
            </AccordionItem>
        );
    });

    const control = movies === undefined
        ? (
            <ErrorBox message="No movies found" />
        )
            : movies.length > 0
        ? (
            <Accordion defaultActiveKey="0">
                {movieList}
            </Accordion>
        )
        : (
            <Spinner animation="border" variant="primary" />
        );

    return (
        <div>
            {control}
        </div>
    );
}
