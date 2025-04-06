import React, { useState, useEffect } from 'react';
import {
    Spinner,
    Stack
} from 'react-bootstrap';
import ErrorBox from './ErrorBox';
import { MovieDetailsModel } from './models/MovieDetails';


export default function MovieDetails({ movieId }: { movieId: string }) {
    const [movieDetails, setMovieDetails] = useState<MovieDetailsModel | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchMovieDetails = async () => {
            try {
                const response = await fetch(`http://localhost:8080/api/Movies/${movieId}`);
                const data: MovieDetailsModel = await response.json();
                setMovieDetails(data);
            } catch (error) {
                console.error('Error fetching movie details:', error);
                setError('Failed to fetch movie details');
                setTimeout(() => {
                    setError(null);
                }, 2000);
            } finally {
                setLoading(false);
            }
        };
        fetchMovieDetails();
    }, [movieId]);

    if (loading) {
        return <Spinner />;
    }

    if (error) {
        return <ErrorBox message={error} />;
    }

    return (
        <Stack gap={2}>
            <div><h3>{movieDetails?.title}</h3></div>
            <Stack direction="horizontal" gap={2}>
                <p>Plot:</p><p>{movieDetails?.plot}</p>
            </Stack>
            <Stack direction="horizontal" gap={2}>
                <p>Rating:</p><p>{movieDetails?.rating}</p>
            </Stack>
            <Stack direction="horizontal" gap={2}>
                <p>Votes:</p><p>{movieDetails?.votes}</p>
            </Stack>
            <Stack direction="horizontal" gap={2}>
                <p>Price:</p><p>{movieDetails?.price}</p>
            </Stack>
        </Stack>
    );
}

